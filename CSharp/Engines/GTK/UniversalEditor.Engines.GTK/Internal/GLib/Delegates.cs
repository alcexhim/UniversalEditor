using System;

namespace UniversalEditor.Engines.GTK.Internal.GLib
{
	internal static class Delegates
	{
		public delegate void GCallback();
		public delegate void GClosureNotify(IntPtr data, IntPtr closure);
	}
}

