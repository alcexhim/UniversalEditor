using System;
using System.Runtime.InteropServices;

namespace UniversalEditor.Engines.GTK.Internal.GLib
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "gobject-2.0";

		public static void g_signal_connect(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr data)
		{
			g_signal_connect_data (instance, detailed_signal, c_handler, data, null, Constants.GConnectFlags.None);
		}

		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_signal_connect_data(IntPtr instance, string detailed_signal, Delegates.GCallback c_handler, IntPtr data, Delegates.GClosureNotify destroy_data, Constants.GConnectFlags connect_flags);
	}
}

