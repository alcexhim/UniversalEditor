using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GLib
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "glib-2.0";

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_variant_type_new (string type_string);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_list_nth_data(IntPtr /*GList*/ list, uint n);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_list_foreach(IntPtr /*GList*/ list, Delegates.GFunc func, IntPtr user_data);
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_list_length(IntPtr /*GList*/ list);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ g_list_prepend(IntPtr /*GList*/ list, IntPtr data);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ g_list_reverse(IntPtr /*GList*/ list);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_list_free(IntPtr /*GList*/ list);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_slist_free	(IntPtr /*GSList*/ list);

		[DllImport(LIBRARY_FILENAME)]
		public static extern uint g_slist_length (IntPtr /*GSList*/ list);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr g_slist_nth_data (IntPtr /*GSList*/ list, uint n);
		
		/// <summary>
		/// Save some memory by interning strings to GQuarks
		/// </summary>
		/// <param name="value">Value.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern string g_intern_string (string value);
		/// <summary>
		/// Save some memory by interning strings to GQuarks
		/// </summary>
		/// <param name="value">Value.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern string g_intern_static_string (string value);

		[DllImport(LIBRARY_FILENAME)]
		public static extern string g_strlcat(System.Text.StringBuilder dest, string src, int dest_size);


		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern char* g_utf8_to_utf16(IntPtr native_str, IntPtr len, IntPtr items_read, ref IntPtr items_written, out IntPtr error);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern IntPtr g_utf16_to_utf8(char* native_str, IntPtr len, IntPtr items_read, IntPtr items_written, out IntPtr error);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_free(IntPtr mem);
	}
}

