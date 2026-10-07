// language: C#, file: ns0/CryptoKeys.cs, runtime: .NET Framework 4.8, no external deps
// *HKDF-SHA256 per RFC 5869 — expand-only, extract не нужен: session key уже псевдослучайный*
using System;
using System.Security.Cryptography;

namespace ns0
{
    internal static class CryptoKeys
    {
        public const int KeySize = 32;       // 256-bit
        public const int NonceSize = 12;     // GCM standard
        public const int TagSize = 16;       // GCM tag

        // HKDF-Expand: PRK -> OKM. info разводит домены использования.
        public static byte[] Expand(byte[] prk, string info, int length)
        {
            if (prk == null || prk.Length != KeySize)
                throw new ArgumentException("PRK must be 32 bytes", nameof(prk));

            byte[] infoBytes = System.Text.Encoding.UTF8.GetBytes(info);
            byte[] okm = new byte[length];
            byte[] t = new byte[0];
            int pos = 0;
            byte counter = 1;

            using (var hmac = new HMACSHA256(prk))
            {
                while (pos < length)
                {
                    int tLen = t.Length;
                    byte[] block = new byte[tLen + infoBytes.Length + 1];
                    Buffer.BlockCopy(t, 0, block, 0, tLen);
                    Buffer.BlockCopy(infoBytes, 0, block, tLen, infoBytes.Length);
                    block[block.Length - 1] = counter;

                    t = hmac.ComputeHash(block);

                    int take = Math.Min(t.Length, length - pos);
                    Buffer.BlockCopy(t, 0, okm, pos, take);
                    pos += take;
                    counter++;

                    Array.Clear(block, 0, block.Length);
                }
            }

            Array.Clear(t, 0, t.Length);
            Array.Clear(infoBytes, 0, infoBytes.Length);
            return okm;
        }

        // Доменные ключи из одного session key. Ключ AES и ключ HMAC независимы.
        public static void DeriveDomainKeys(byte[] sessionKey, out byte[] aesKey, out byte[] hmacKey)
        {
            aesKey = Expand(sessionKey, "DVDCRPT1/aes256", KeySize);
            hmacKey = Expand(sessionKey, "DVDCRPT1/hmacsha256", KeySize);
        }
    }
}