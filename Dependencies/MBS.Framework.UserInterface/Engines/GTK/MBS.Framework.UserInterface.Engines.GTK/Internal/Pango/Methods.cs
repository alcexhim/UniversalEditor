using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.Pango
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "pango-1.0";

		[DllImport(LIBRARY_FILENAME)]
		public static extern string pango_font_family_get_name(IntPtr handle);
		[DllImport(LIBRARY_FILENAME)]
		public static extern string pango_font_face_get_face_name(IntPtr handle);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr pango_attr_list_new();

		[DllImport(LIBRARY_FILENAME)]
		public static extern void pango_attr_list_insert(IntPtr /*PangoAttrList*/ list, IntPtr /*PangoAttribute*/ attr);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr /*PangoAttribute*/ pango_attr_scale_new(double scale_factor);
	}
}

