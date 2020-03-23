//
//  GtkColorChooserDialog.cs
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
	internal class GtkColorDialog
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_color_selection_dialog_new(string title);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_color_chooser_dialog_new(string title, IntPtr parentHandle);

		public static IntPtr gtk_color_dialog_new(string title, IntPtr parentHandle, bool useLegacyFunctionality = false)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3 && !useLegacyFunctionality)
			{
				return gtk_color_chooser_dialog_new(title, parentHandle);
			}
			else
			{
				return gtk_color_selection_dialog_new(title);
			}
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_color_chooser_get_rgb(IntPtr /*GtkColorChooser*/ chooser, out Internal.GDK.Structures.GdkRGBA color);

		// only in gtk-3
		[DllImport(Gtk.LIBRARY_FILENAME, EntryPoint = "gtk_color_chooser_get_rgba")]
		private static extern IntPtr gtk_color_chooser_get_rgba_internal(IntPtr /*GtkColorChooser*/ chooser, out Internal.GDK.Structures.GdkRGBA color);
		// only in gtk-3
		[DllImport(Gtk.LIBRARY_FILENAME, EntryPoint = "gtk_color_chooser_set_rgba")]
		private static extern void gtk_color_chooser_set_rgba_internal(IntPtr /*GtkColorChooser*/ chooser, ref Internal.GDK.Structures.GdkRGBA color);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_color_selection_dialog_get_color_selection(IntPtr /*GtkColorSelectionDialog*/ chooser);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_color_selection_get_current_color(IntPtr /*GtkColorSelection*/ colorsel, out Internal.GDK.Structures.GdkColor color);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_color_selection_get_previous_color(IntPtr /*GtkColorSelection*/ colorsel, out Internal.GDK.Structures.GdkColor color);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern void gtk_color_selection_set_current_color(IntPtr /*GtkColorSelection*/ colorsel, Internal.GDK.Structures.GdkColor color);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_color_chooser_set_use_alpha(IntPtr /*GtkColorChooser*/ chooser, bool use_alpha); 

		public static IntPtr gtk_color_chooser_get_rgba(IntPtr /*GtkColorChooser*/ chooser, out Internal.GDK.Structures.GdkRGBA color)
		{
			IntPtr retval = IntPtr.Zero;

			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3)
			{
				retval = gtk_color_chooser_get_rgba_internal(chooser, out color);
				return retval;
			}
			else if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V2)
			{
				IntPtr colorsel = gtk_color_selection_dialog_get_color_selection(chooser);
				Internal.GDK.Structures.GdkColor color1 = new Internal.GDK.Structures.GdkColor();
				gtk_color_selection_get_current_color(colorsel, out color1);

				// this seems weird. is this correct? dividing TWICE???
				Internal.GDK.Structures.GdkRGBA color2 = new Internal.GDK.Structures.GdkRGBA();
				color2.red = ((double)color1.red / 255) / 255;
				color2.green = ((double)color1.green / 255) / 255;
				color2.blue = ((double)color1.blue / 255) / 255;
				color2.alpha = 1.0;
				color = color2;
				return IntPtr.Zero;
			}

			Internal.GDK.Structures.GdkRGBA empty = new Internal.GDK.Structures.GdkRGBA();
			color = empty;
			return IntPtr.Zero;
		}
		public static void gtk_color_chooser_set_rgba(IntPtr /*GtkColorChooser*/ chooser, ref Internal.GDK.Structures.GdkRGBA color)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3)
			{
				gtk_color_chooser_set_rgba_internal(chooser, ref color);
			}
			else if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V2)
			{
				IntPtr colorsel = gtk_color_selection_dialog_get_color_selection(chooser);
				Internal.GDK.Structures.GdkColor color1 = new Internal.GDK.Structures.GdkColor();
				color1.blue = (ushort)(color.blue * 255);
				color1.green = (ushort)(color.green * 255);
				color1.red = (ushort)(color.red * 255);
				gtk_color_selection_set_current_color(colorsel, color1);
			}
		}
	}
}

