using System;
using System.Security.Cryptography;
using System.Text;

namespace ns0
{
    internal static class PasswordGenerator
    {
        private const int PasswordLength = 18;
        private const int Pbkdf2Iterations = 600000;
        private const string UpperChars = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        private const string LowerChars = "abcdefghjkmnpqrstuvwxyz";
        private const string DigitChars = "23456789";

        public static string GenerateRandom()
        {
            var result = new char[PasswordLength];
            var allChars = UpperChars + LowerChars + DigitChars;

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] buf = new byte[4];

                for (int i = 0; i < PasswordLength; i++)
                {
                    rng.GetBytes(buf);
                    uint val = BitConverter.ToUInt32(buf, 0);
                    result[i] = allChars[(int)(val % (uint)allChars.Length)];
                }

                rng.GetBytes(buf);
                uint pick = BitConverter.ToUInt32(buf, 0);
                int upperPos = (int)(pick % PasswordLength);
                result[upperPos] = UpperChars[(int)((pick >> 8) % (uint)UpperChars.Length)];

                rng.GetBytes(buf);
                pick = BitConverter.ToUInt32(buf, 0);
                int digitPos = (int)(pick % PasswordLength);
                while (digitPos == upperPos)
                {
                    rng.GetBytes(buf);
                    digitPos = (int)(BitConverter.ToUInt32(buf, 0) % PasswordLength);
                }
                result[digitPos] = DigitChars[(int)((pick >> 8) % (uint)DigitChars.Length)];
            }

            return new string(result);
        }

        public static byte[] DeriveKeyFromPassword(string password, byte[] salt, int iterations = Pbkdf2Iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        public static byte[] HashPassword(string password, byte[] salt, int iterations = Pbkdf2Iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        public static bool SecureEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
} 
