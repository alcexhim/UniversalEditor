//
//  GtkBox.cs
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
	internal class GtkBox
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_hbox_new(bool homogenous, int spacing);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_vbox_new(bool homogenous, int spacing);

		[DllImport(Gtk.LIBRARY_FILENAME_V3, EntryPoint = "gtk_box_new")]
		private static extern IntPtr gtk_box_new_v3(Constants.GtkOrientation orientation, int spacing);

		public static IntPtr gtk_box_new(Constants.GtkOrientation orientation, bool homogenous = true, int spacing = 0, bool useGtk2API = false)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V2 || useGtk2API)
			{
				switch (orientation)
				{
				case Constants.GtkOrientation.Horizontal:
					{
						return gtk_hbox_new(homogenous, spacing);
					}
				case Constants.GtkOrientation.Vertical:
					{
						return gtk_vbox_new(homogenous, spacing);
					}
				}
			}
			else if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3)
			{
				return gtk_box_new_v3(orientation, spacing);
			}
			return IntPtr.Zero;
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_box_get_homogeneous(IntPtr box);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_box_set_homogeneous(IntPtr box, bool value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_box_get_spacing(IntPtr box);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_box_set_spacing(IntPtr box, int value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_box_pack_start(IntPtr box, IntPtr child, bool expand, bool fill, int padding);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_box_pack_end(IntPtr box, IntPtr child, bool expand, bool fill, int padding);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_box_set_child_packing(IntPtr /*GtkBox*/ box, IntPtr /*GtkWidget*/ child, bool expand, bool fill, int padding, Constants.GtkPackType pack_type);
	}
}

