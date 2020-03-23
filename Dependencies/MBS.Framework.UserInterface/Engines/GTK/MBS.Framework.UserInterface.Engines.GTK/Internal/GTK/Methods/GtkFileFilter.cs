//
//  GtkFileFilter.cs
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
	internal class GtkFileFilter
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkFileFilter*/ gtk_file_filter_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_file_filter_set_name(IntPtr /*GtkFileFilter*/ filter, string name);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_file_filter_get_name(IntPtr /*GtkFileFilter*/ filter);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_file_filter_add_mime_type(IntPtr /*GtkFileFilter*/ filter, string mime_type);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_file_filter_add_pattern(IntPtr /*GtkFileFilter*/ filter, string pattern);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_file_filter_add_pixbuf_formats(IntPtr /*GtkFileFilter*/ filter);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_file_filter_add_custom(IntPtr /*GtkFileFilter*/ filter, Constants.GtkFileFilterFlags needed, Delegates.GtkFileFilterFunc func, IntPtr data, GLib.Delegates.GDestroyNotify notify);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkFileFilterFlags gtk_file_filter_get_needed(IntPtr /*GtkFileFilter*/ filter);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_file_filter_filter(IntPtr /*GtkFileFilter*/ filter, ref Structures.GtkFileFilterInfo filter_info);
	}
}

