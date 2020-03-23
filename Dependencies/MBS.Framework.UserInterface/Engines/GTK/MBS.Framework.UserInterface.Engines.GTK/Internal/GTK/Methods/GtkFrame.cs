using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK.Methods
{
	internal static class GtkFrame
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_frame_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_frame_set_label(IntPtr /*GtkWidget*/ frame, string label);
	}
}
