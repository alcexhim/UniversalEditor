using System;
using System.Runtime.InteropServices;

namespace UniversalEditor.Engines.GTK.Internal.GTK
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME_V2 = "gtk-x11-2.0";
		public const string LIBRARY_FILENAME_V3 = "gtk-3";

		public const string LIBRARY_FILENAME = LIBRARY_FILENAME_V3;

		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_main();
		
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_main_quit();

		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_init (ref int argc, ref string[] argv);
		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gtk_init_check (ref int argc, ref string[] argv);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_window_new(Constants.GtkWindowType windowType);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_window_set_screen (IntPtr window, IntPtr screen);

		[DllImport(LIBRARY_FILENAME)]
		public static extern string gtk_window_get_title(IntPtr window);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_window_set_title(IntPtr window, string title);

		#region Widget 
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_widget_set_size_request(IntPtr widget, uint width, uint height);

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gtk_widget_get_visible(IntPtr widget);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_widget_set_visible(IntPtr widget, bool visible);

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_widget_get_parent(IntPtr widget);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_widget_set_parent(IntPtr widget, IntPtr parent);
		
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_widget_show(IntPtr widget);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_widget_show_all(IntPtr widget);
		#endregion

		#region Container
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_container_add(IntPtr container, IntPtr widget);
		#endregion

		#region Box
		[DllImport(LIBRARY_FILENAME_V2)]
		private static extern IntPtr gtk_hbox_new (bool homogenous, int spacing);
		[DllImport(LIBRARY_FILENAME_V2)]
		private static extern IntPtr gtk_vbox_new (bool homogenous, int spacing);

		[DllImport(LIBRARY_FILENAME_V3, EntryPoint="gtk_box_new")]
		private static extern IntPtr gtk_box_new_v3 (Constants.GtkBoxOrientation orientation, int spacing);

		public static IntPtr gtk_box_new(Constants.GtkBoxOrientation orientation, int spacing = 0)
		{
			return gtk_box_new (orientation, true, spacing);
		}
		public static IntPtr gtk_box_new(Constants.GtkBoxOrientation orientation, bool homogenous = true, int spacing = 0)
		{
			if (LIBRARY_FILENAME == LIBRARY_FILENAME_V2) {
				switch (orientation)
				{
					case Constants.GtkBoxOrientation.Horizontal:
					{
						return gtk_hbox_new (homogenous, spacing);
					}
					case Constants.GtkBoxOrientation.Vertical:
					{
						return gtk_vbox_new (homogenous, spacing);
					}
				}
			} else if (LIBRARY_FILENAME == LIBRARY_FILENAME_V3) {
				return gtk_box_new_v3 (orientation, spacing);
			}
			return IntPtr.Zero;
		}

		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_box_pack_start (IntPtr box, IntPtr child, bool expand, bool fill, int padding);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_box_pack_end (IntPtr box, IntPtr child, bool expand, bool fill, int padding);
		#endregion

		#region Menu Shell
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_shell_append(IntPtr shell, IntPtr child);
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_shell_insert(IntPtr shell, IntPtr child, int position);
		#endregion

		#region Menu Bar
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_bar_new();
		#endregion

		#region Menu
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_new();
		[DllImport(LIBRARY_FILENAME)]
		public static extern string gtk_menu_get_title(IntPtr handle);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_menu_set_title(IntPtr handle, string title);
		#endregion

		#region Menu Item
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_item_new();

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gtk_menu_item_get_use_underline(IntPtr handle);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_use_underline(IntPtr handle, bool value);

		[DllImport(LIBRARY_FILENAME)]
		public static extern string gtk_menu_item_get_label(IntPtr handle);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_label(IntPtr handle, string text);
		
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_item_get_submenu(IntPtr handle);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_submenu(IntPtr handle, IntPtr submenu);
		#endregion

		#region Separator
		[DllImport(LIBRARY_FILENAME)]
		public static extern IntPtr gtk_separator_new(GtkBoxOrientation orientation);
		#endregion
	}
}

