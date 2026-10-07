using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ns0
{
    internal static class IntentionGate
    {
        private static readonly string PersistenceDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            BuildSecrets.PersistenceDirName);

        private const int MaxAttempts = 10;

        private static readonly string IntentLogPath =
            Path.Combine(PersistenceDir, ".dvdcrpt_intent");

        private static readonly string ConfirmationPath =
            Path.Combine(PersistenceDir, ".dvdcrpt_intent_confirm");

        public static int GetMarkedAttempts()
        {
            try
            {
                if (!File.Exists(IntentLogPath)) return 0;
                string[] lines = File.ReadAllLines(IntentLogPath);
                int count = 0;
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line)) count++;
                }
                return count;
            }
            catch { }
            return 0;
        }

        public static void MarkAttempt(int attemptNumber)
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                byte[] salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                string timestamp = DateTime.UtcNow.ToString("o");
                string entry = string.Format("{0}|{1}|{2}",
                    attemptNumber, timestamp, BitConverter.ToString(salt).Replace("-", ""));
                using (var fs = new FileStream(IntentLogPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(entry);
                }
                try { File.SetAttributes(IntentLogPath, FileAttributes.Hidden | FileAttributes.System); } catch { }
            }
            catch { }
        }

        public static void MarkFinalConfirmation()
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                File.WriteAllText(ConfirmationPath, DateTime.UtcNow.ToString("o"));
                try { File.SetAttributes(ConfirmationPath, FileAttributes.Hidden | FileAttributes.System); } catch { }
            }
            catch { }
        }

        public static bool IsFinalConfirmationPresent()
        {
            try { return File.Exists(ConfirmationPath); } catch { }
            return false;
        }

        public static bool IsFullyIntentional()
        {
            try
            {
                if (GetMarkedAttempts() < MaxAttempts) return false;
                return IsFinalConfirmationPresent();
            }
            catch { }
            return false;
        }

        public static void Reset()
        {
            try
            {
                if (File.Exists(IntentLogPath))
                {
                    File.SetAttributes(IntentLogPath, FileAttributes.Normal);
                    File.Delete(IntentLogPath);
                }
                if (File.Exists(ConfirmationPath))
                {
                    File.SetAttributes(ConfirmationPath, FileAttributes.Normal);
                    File.Delete(ConfirmationPath);
                }
            }
            catch { }
        }

        public static void MarkDestroyPending()
        {
            try
            {
                Directory.CreateDirectory(PersistenceDir);
                File.SetAttributes(DestructionPendingPath, FileAttributes.Hidden | FileAttributes.System);
            }
            catch { }
        }

        private static readonly string DestructionPendingPath =
            Path.Combine(PersistenceDir, ".dvdcrpt_destroy_pending_intent");

        public static bool IsDestroyPending()
        {
            try { return File.Exists(DestructionPendingPath); } catch { }
            return false;
        }

        public static void ClearDestroyPending()
        {
            try
            {
                if (File.Exists(DestructionPendingPath))
                {
                    File.SetAttributes(DestructionPendingPath, FileAttributes.Normal);
                    File.Delete(DestructionPendingPath);
                }
            }
            catch { }
        }
    }
}
