using System;
using System.Runtime.InteropServices;

namespace UniversalEditor.Engines.GTK.Internal.GDK
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "gdk-3";

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gdk_screen_get_default();
		
		[DllImport(LIBRARY_FILENAME)]
		public static extern double gdk_screen_get_resolution(IntPtr screen);

	}
}

