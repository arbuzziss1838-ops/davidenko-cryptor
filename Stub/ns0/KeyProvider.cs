using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ns0
{
    internal static class KeyProvider
    {
        private const string ResourceName = BuildSecrets.PublicKeyResourceName;
        private static string _cachedKey;

        public static string GetPublicKeyXml()
        {
            if (_cachedKey != null) return _cachedKey;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                Stream stream = assembly.GetManifestResourceStream(ResourceName);
                if (stream == null)
                {
                    string fullName = assembly.GetName().Name + "." + ResourceName;
                    stream = assembly.GetManifestResourceStream(fullName);
                }

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        _cachedKey = reader.ReadToEnd().Trim();
                        return _cachedKey;
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        public static bool IsKeyValid()
        {
            string key = GetPublicKeyXml();
            if (string.IsNullOrEmpty(key)) return false;
            if (key.Contains("REPLACE_WITH_OPERATOR_PUBLIC_KEY_MODULUS")) return false;
            if (!key.Contains("<Modulus>") || !key.Contains("</Modulus>")) return false;
            if (!key.Contains("<Exponent>") || !key.Contains("</Exponent>")) return false;
            return true;
        }
    }
}