using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.AppIndicator
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "appindicator";

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr app_indicator_new(string id, string icon_name, Constants.AppIndicatorCategory category);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_status(IntPtr /*AppIndicator*/ handle, Constants.AppIndicatorStatus value);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_menu(IntPtr /*AppIndicator*/ handle, IntPtr /*GtkMenu*/ value);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_label(IntPtr /*AppIndicator*/ handle, string label, string guide);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_title(IntPtr /*AppIndicator*/ handle, string value);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_icon(IntPtr /*AppIndicator*/ handle, string value);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void app_indicator_set_attention_icon(IntPtr /*AppIndicator*/ handle, string value);

	}
}

