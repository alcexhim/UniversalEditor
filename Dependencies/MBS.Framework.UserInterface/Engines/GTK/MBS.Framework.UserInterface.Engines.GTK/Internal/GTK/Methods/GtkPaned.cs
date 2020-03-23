//
//  GtkPaned.cs
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
	internal class GtkPaned
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_hpaned_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_vpaned_new();

		[DllImport(Gtk.LIBRARY_FILENAME_V3, EntryPoint = "gtk_paned_new")]
		private static extern IntPtr gtk_paned_new_v3(Constants.GtkOrientation orientation);

		public static IntPtr gtk_paned_new(Constants.GtkOrientation orientation, bool useGtk2API = false)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V2 || useGtk2API)
			{
				switch (orientation)
				{
				case Constants.GtkOrientation.Horizontal:
					{
						return gtk_hpaned_new();
					}
				case Constants.GtkOrientation.Vertical:
					{
						return gtk_vpaned_new();
					}
				}
			}
			else if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3)
			{
				return gtk_paned_new_v3(orientation);
			}
			return IntPtr.Zero;
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_paned_add1(IntPtr hPaned, IntPtr hChild);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_paned_add2(IntPtr hPaned, IntPtr hChild);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_paned_pack1(IntPtr hPaned, IntPtr hChild, bool resize, bool shrink);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_paned_pack2(IntPtr hPaned, IntPtr hChild, bool resize, bool shrink);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_paned_get_position(IntPtr /*GtkPaned*/ handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_paned_set_position(IntPtr /*GtkPaned*/ handle, int value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_paned_set_wide_handle(IntPtr /*GtkPaned*/ handle, bool value);
	}
}

