using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GDK
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "gdk-3";

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gdk_display_get_default();

		[DllImport(LIBRARY_FILENAME)]
		public static extern int gdk_screen_get_n_monitors(IntPtr screen);
		[DllImport(LIBRARY_FILENAME)]
		public static extern int gdk_screen_get_primary_monitor(IntPtr screen);
		
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gdk_screen_get_default();

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkCursor*/ gdk_cursor_new_from_name(IntPtr /*GdkDisplay*/ display, string name);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkDisplay*/ gdk_window_get_display(IntPtr /*GdkWindow*/ window);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkCursor*/ gdk_window_get_cursor(IntPtr /*GdkWindow*/ window);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gdk_window_set_cursor(IntPtr /*GdkWindow*/ window, IntPtr /*GdkCursor*/ cursor);

		// pixbuf
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gdk_pixbuf_new(Constants.GdkColorspace colorspace, bool has_alpha, int bits_per_sample, int width, int height);

		// pixbuf loader
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkPixbufLoader*/ gdk_pixbuf_loader_new();
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkPixbufLoader*/ gdk_pixbuf_loader_new_with_type(string image_type, ref IntPtr /*GError*/ error);
		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gdk_pixbuf_loader_write(IntPtr /*GdkPixbufLoader*/ loader, byte[] buffer, int count, ref IntPtr /*GError*/ error);
		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gdk_pixbuf_loader_close(IntPtr /*GdkPixbufLoader*/ loader, ref IntPtr /*GError*/ error);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GdkPixbuf*/ gdk_pixbuf_loader_get_pixbuf(IntPtr /*GdkPixbufLoader*/ loader);

	}
}

