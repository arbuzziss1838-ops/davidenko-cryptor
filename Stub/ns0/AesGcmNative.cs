// language: C#, file: ns0/AesGcmNative.cs, runtime: .NET Framework 4.8, no NuGet
// AES-256-GCM через CNG (BCrypt) с fallback на manual GCM.
// .NET Framework не имеет встроенного AesGcm, а GCM нужен
// OperatorConfig и C2-конфигу. nonce=12, tag=16.
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ns0
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

        [DllImport("bcrypt.dll")]
        private static extern uint BCryptDecrypt(IntPtr hKey, byte[] pbInput, uint cbInput, ref BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO pPaddingInfo, byte[] pbIV, uint cbIV, byte[] pbOutput, uint cbOutput, out uint pcbResult, uint dwFlags);

        private static IntPtr OpenGcm(byte[] key)
        {
            IntPtr hAlg;
            if (BCryptOpenAlgorithmProvider(out hAlg, "AES", null, 0) != 0)
                throw new InvalidOperationException("BCryptOpenAlgorithmProvider failed");

            byte[] modeValue = System.Text.Encoding.Unicode.GetBytes("GCM\0");
            if (BCryptSetProperty(hAlg, "ChainingMode", modeValue, (uint)modeValue.Length, 0) != 0)
                throw new InvalidOperationException("BCryptSetProperty ChainingMode failed");

            uint objLen, res;
            BCryptGetProperty(hAlg, "ObjectLength", null, 0, out objLen, 0);
            byte[] keyObject = new byte[objLen];

            IntPtr hKey;
            if (BCryptGenerateSymmetricKey(hAlg, out hKey, keyObject, objLen, key, (uint)key.Length, 0) != 0)
                throw new InvalidOperationException("BCryptGenerateSymmetricKey failed");

            BCryptCloseAlgorithmProvider(hAlg, 0);
            return hKey;
        }

        // void-форма (совместима с OperatorConfig): plain должен быть уже нужной длины.
        public static void Decrypt(byte[] key, byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plain)
        {
            byte[] result = Decrypt(key, nonce, ciphertext, tag);
            Buffer.BlockCopy(result, 0, plain, 0, Math.Min(result.Length, plain.Length));
        }

        // Возвращает plaintext. Бросает при несовпадении тега.
        public static byte[] Decrypt(byte[] key, byte[] nonce, byte[] ciphertext, byte[] tag)
        {
            try
            {
                return BcryptDecrypt(key, nonce, ciphertext, tag);
            }
            catch
            {
                return ManualDecrypt(key, nonce, ciphertext, tag);
            }
        }

        private static byte[] BcryptDecrypt(byte[] key, byte[] nonce, byte[] ciphertext, byte[] tag)
        {
            IntPtr hKey = OpenGcm(key);
            try
            {
                BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO info = MakeInfo(nonce, tag);
                try
                {
                    byte[] output = new byte[ciphertext.Length];
                    uint written;
                    uint status = BCryptDecrypt(hKey, ciphertext, (uint)ciphertext.Length, ref info,
                        null, 0, output, (uint)output.Length, out written, 0);
                    if (status != 0)
                        throw new System.Security.Cryptography.CryptographicException("AES-GCM auth failed: " + status);
                    Array.Resize(ref output, (int)written);
                    return output;
                }
                finally { FreeInfo(ref info); }
            }
            finally { BCryptDestroyKey(hKey); }
        }

        // Возвращает ciphertext и tag отдельно.
        public static void Encrypt(byte[] key, byte[] nonce, byte[] plain, out byte[] ciphertext, out byte[] tag)
        {
            try
            {
                BcryptEncrypt(key, nonce, plain, out ciphertext, out tag);
            }
            catch
            {
                ManualEncrypt(key, nonce, plain, out ciphertext, out tag);
            }
        }

        private static void BcryptEncrypt(byte[] key, byte[] nonce, byte[] plain, out byte[] ciphertext, out byte[] tag)
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

        // --- Manual AES-GCM fallback (uses .NET Aes class for AES-ECB) ---

        private static void ManualEncrypt(byte[] key, byte[] nonce, byte[] plaintext, out byte[] ciphertext, out byte[] tag)
        {
            ciphertext = new byte[plaintext.Length];
            tag = new byte[16];
            ManualGcm.GcmEncrypt(key, nonce, plaintext, null, ciphertext, tag);
        }

        private static byte[] ManualDecrypt(byte[] key, byte[] nonce, byte[] ciphertext, byte[] tag)
        {
            byte[] plaintext = new byte[ciphertext.Length];
            byte[] computedTag = new byte[16];
            ManualGcm.GcmDecrypt(key, nonce, ciphertext, null, plaintext, computedTag);

            for (int i = 0; i < 16; i++)
            {
                if (computedTag[i] != tag[i])
                    throw new System.Security.Cryptography.CryptographicException("AES-GCM auth failed (manual)");
            }

            return plaintext;
        }

        private static class ManualGcm
        {
            public static void GcmEncrypt(byte[] key, byte[] nonce, byte[] plaintext, byte[] aad, byte[] ciphertext, byte[] tag)
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

                    byte[] Y = ComputeGhash(H, aad, ciphertext);

                    for (int i = 0; i < 16; i++) tag[i] = (byte)(Y[i] ^ E0[i]);

                    enc.Dispose();
                }
            }

            public static void GcmDecrypt(byte[] key, byte[] nonce, byte[] ciphertext, byte[] aad, byte[] plaintext, byte[] computedTag)
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
                    int ctOff = 0;
                    while (ctOff < ciphertext.Length)
                    {
                        enc.TransformBlock(counter, 0, 16, E, 0);
                        int chunk = Math.Min(16, ciphertext.Length - ctOff);
                        for (int i = 0; i < chunk; i++)
                            plaintext[ctOff + i] = (byte)(ciphertext[ctOff + i] ^ E[i]);
                        ctOff += chunk;
                        Inc32(counter);
                    }

                    byte[] Y = ComputeGhash(H, aad, ciphertext);

                    for (int i = 0; i < 16; i++) computedTag[i] = (byte)(Y[i] ^ E0[i]);

                    enc.Dispose();
                }
            }

            private static byte[] ComputeGhash(byte[] H, byte[] aad, byte[] ciphertext)
            {
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

                return Y;
            }

            private static void Inc32(byte[] counter)
            {
                for (int i = 15; i >= 12; i--)
                {
                    if (++counter[i] != 0) break;
                }
            }

            private static ulong SwapEndian(ulong value)
            {
                byte[] b = BitConverter.GetBytes(value);
                Array.Reverse(b);
                return BitConverter.ToUInt64(b, 0);
            }

            private static void Gfmul(byte[] H, byte[] X, byte[] output)
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
}
