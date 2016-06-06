using System;

namespace UniversalEditor.Engines.GTK.Internal.GLib
{
	internal static class Delegates
	{
		public delegate void GCallback(IntPtr sender);
		public delegate void GClosureNotify(IntPtr data, IntPtr closure);
	}
}

