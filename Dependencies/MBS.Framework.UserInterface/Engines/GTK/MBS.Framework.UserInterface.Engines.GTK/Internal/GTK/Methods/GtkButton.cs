//
//  GtkButton.cs
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
	internal class GtkButton
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_button_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_button_get_label(IntPtr button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_label(IntPtr button, IntPtr label);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_button_get_always_show_image(IntPtr button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_always_show_image(IntPtr button, bool value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_image(IntPtr /*GtkButton*/ button, IntPtr /*GtkWidget*/ image);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_button_get_image(IntPtr /*GtkButton*/ button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_image_position (IntPtr /*GtkButton*/ button, Constants.GtkPositionType position);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkPositionType gtk_button_get_image_position (IntPtr /*GtkButton*/ button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_button_get_use_underline(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_use_underline(IntPtr handle, bool value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_button_get_use_stock(IntPtr button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_use_stock(IntPtr button, bool value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_button_get_focus_on_click(IntPtr button);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_focus_on_click(IntPtr button, bool value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_button_set_relief(IntPtr button, Constants.GtkReliefStyle value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkReliefStyle gtk_button_get_relief(IntPtr button);
	}
}

