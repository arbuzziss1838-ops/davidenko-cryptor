using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ns0
{
    public static class Decryptor
    {
        private const string MagicHeader = "DVDCRPT1";
        private const int ChunkSize = 1024 * 1024;
        private const int FileVersion = 2;

        public static bool DecryptFile(string filePath, string privateKeyXml, string outputPath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;
            if (string.IsNullOrEmpty(privateKeyXml)) return false;
            if (!File.Exists(filePath)) return false;

            try
            {
                using (var fin = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, ChunkSize, FileOptions.SequentialScan))
                {
                    byte[] magic = new byte[8];
                    if (ReadExactly(fin, magic, 0, 8) != 8) return false;
                    if (Encoding.ASCII.GetString(magic) != MagicHeader) return false;

                    int version = fin.ReadByte();
                    // v1 не поддерживается: отказ сразу на заголовке, симметрично с
                    // HybridCrypto.DecryptFileInternal — одинаковый ответ на v1 в обоих путях.
                    if (version != FileVersion) return false;

                    byte[] keyLenBytes = new byte[4];
                    if (ReadExactly(fin, keyLenBytes, 0, 4) != 4) return false;
                    int keyLen = BitConverter.ToInt32(keyLenBytes, 0);

                    byte[] keyBlob = new byte[keyLen];
                    if (ReadExactly(fin, keyBlob, 0, keyLen) != keyLen) return false;

                    byte[] chunkCountBytes = new byte[4];
                    if (ReadExactly(fin, chunkCountBytes, 0, 4) != 4) return false;
                    int chunkCount = BitConverter.ToInt32(chunkCountBytes, 0);
                    byte[] sessionKey;
                    if (keyBlob != null)
                    {
                        sessionKey = DecryptSessionKey(keyBlob, privateKeyXml);
                        if (sessionKey == null) return false;
                    }
                    else
                    {
                        return false;
                    }

                    using (var aes = new AesCryptoServiceProvider())
                    {
                        aes.KeySize = 256;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.Key = sessionKey;

                        using (var hmac = new HMACSHA256(sessionKey))
                        using (var fout = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, ChunkSize))
                        {
                            for (int i = 0; i < chunkCount; i++)
                            {
                                byte[] iv = new byte[16];
                                if (ReadExactly(fin, iv, 0, 16) != 16) return false;

                                int maxCtLen = ChunkSize + 16;
                                byte[] ciphertext = new byte[maxCtLen];
                                int ctRead = ReadExactly(fin, ciphertext, 0, maxCtLen);
                                if (ctRead != maxCtLen)
                                {
                                    if (ctRead < 16) return false;
                                    Array.Resize(ref ciphertext, ctRead);
                                }

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
            catch
            {
                return false;
            }
        }

        public static int DecryptDirectory(string directoryPath, string privateKeyXml, bool recursive)
        {
            if (string.IsNullOrEmpty(directoryPath)) return 0;
            if (!Directory.Exists(directoryPath)) return 0;
            if (string.IsNullOrEmpty(privateKeyXml)) return 0;

            int count = 0;
            try
            {
                SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                string[] files = Directory.GetFiles(directoryPath, "*.dvdcrpt", searchOption);

                foreach (string filePath in files)
                {
                    string outputPath = filePath.Substring(0, filePath.Length - 8);
                    if (DecryptFile(filePath, privateKeyXml, outputPath))
                    {
                        count++;
                    }
                }
            }
            catch { }
            return count;
        }

        private static byte[] DecryptSessionKey(byte[] encryptedKeyBlob, string privateKeyXml)
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider(2048))
                {
                    rsa.FromXmlString(privateKeyXml);
                    return rsa.Decrypt(encryptedKeyBlob, true);
                }
            }
            catch
            {
                return null;
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
    }
}
