using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL
{
	public static class Methods
	{
		private const string LIBRARY_X11 = "libX11.so";
		
		[DllImport(LIBRARY_X11)]
		public static extern void XOpenDisplay(string name);
		
		[DllImport(LIBRARY_X11)]
		public static extern int XDefaultScreen(IntPtr display);
	}
}

