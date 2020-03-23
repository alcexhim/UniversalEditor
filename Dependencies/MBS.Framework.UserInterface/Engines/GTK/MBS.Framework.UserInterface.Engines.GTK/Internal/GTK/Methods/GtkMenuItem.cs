//
//  GtkMenuItem.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK.Methods
{
	internal class GtkMenuItem
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_item_new();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_menu_item_get_use_underline(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_use_underline(IntPtr handle, bool value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_menu_item_get_label(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_label(IntPtr handle, string text);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_menu_item_get_submenu(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_submenu(IntPtr handle, IntPtr submenu);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_menu_item_get_right_justified(IntPtr menu_item);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_right_justified(IntPtr menu_item, bool value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_menu_item_get_accel_path(IntPtr menu_item);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_menu_item_set_accel_path(IntPtr menu_item, string accel_path);
	}
}

