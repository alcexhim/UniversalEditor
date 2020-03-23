using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.System.Windows
{
    public static class Methods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("gdi32.dll")]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref Structures.PIXELFORMATDESCRIPTOR pfd);
        [DllImport("gdi32.dll")]
        public static extern int SetPixelFormat(IntPtr hdc, int iPixelFormat, ref Structures.PIXELFORMATDESCRIPTOR pfd);

        [DllImport("ntdll.dll")]
        public static extern void RtlZeroMemory(ref Internal.System.Windows.Structures.PIXELFORMATDESCRIPTOR pfd, int length);

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpszClassName, string lpszWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, Constants.WindowLongIndex nIndex);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, Constants.WindowLongIndex nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, Constants.SetWindowPosFlags uFlags);
    }
}
