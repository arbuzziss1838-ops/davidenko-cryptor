// language: C#, file: ns0/NativeMethods.cs, runtime: .NET Framework 4.8
// Централизованные P/Invoke объявления WinAPI.

using System;
using System.Runtime.InteropServices;

namespace ns0
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
    }
}