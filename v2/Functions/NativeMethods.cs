using System;
using System.Runtime.InteropServices;

namespace EduXpress.Functions
{
    // ----- Structure and API declarations.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DOCINFOW
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;
    }
    internal static class NativeMethods
    {
        // ----- Imports for single-instance support.
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_EduXpressNotify =
            //RegisterWindowMessage("ACME.Library.ForceToFront");
            RegisterWindowMessage("EduXpress.ForceToFront");

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "RegisterWindowMessageW", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int RegisterWindowMessage(string message);

        // ----- Imports for raw printing support.
        [DllImport("winspool.drv", EntryPoint = "OpenPrinterW", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool OpenPrinter(string src, ref IntPtr hPrinter, int pd);

        [DllImport("winspool.drv", EntryPoint = "ClosePrinter", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartDocPrinterW", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool StartDocPrinter(IntPtr hPrinter, Int32 level, ref DOCINFOW pDI);

        [DllImport("winspool.drv", EntryPoint = "EndDocPrinter", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "StartPagePrinter", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "EndPagePrinter", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", EntryPoint = "WritePrinter", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, ref Int32 dwWritten);
    }
}
