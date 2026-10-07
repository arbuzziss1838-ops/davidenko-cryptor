using System;
using System.Security.Cryptography;

namespace ns0
{
    public sealed class RamKey : IDisposable
    {
        private byte[] _masked;
        private byte[] _mask;
        private bool _disposed;

        public RamKey(byte[] key)
        {
            if (key == null || key.Length == 0)
                throw new ArgumentException("key");

            _mask = new byte[key.Length];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(_mask);

            _masked = new byte[key.Length];
            for (int i = 0; i < key.Length; i++)
                _masked[i] = (byte)(key[i] ^ _mask[i]);
            Array.Clear(key, 0, key.Length);
        }

        public int Length
        {
            get { return _masked != null ? _masked.Length : 0; }
        }

        public byte[] GetCopy()
        {
            if (_disposed || _masked == null) return null;
            byte[] copy = new byte[_masked.Length];
            for (int i = 0; i < _masked.Length; i++)
                copy[i] = (byte)(_masked[i] ^ _mask[i]);
            return copy;
        }

        public static void Wipe(byte[] data)
        {
            if (data != null)
                Array.Clear(data, 0, data.Length);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_masked != null) Array.Clear(_masked, 0, _masked.Length);
            if (_mask != null) Array.Clear(_mask, 0, _mask.Length);
            _masked = null;
            _mask = null;
        }
    }
}