//
//  GtkTreeViewColumn.cs
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
	internal static class GtkTreeViewColumn
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeViewColumn*/ gtk_tree_view_column_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_set_title(IntPtr /*GtkTreeViewColumn*/ column, string title);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_clear(IntPtr /*GtkTreeViewColumn*/ column);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_add_attribute(IntPtr /*GtkTreeViewColumn*/ column, IntPtr /*GtkCellRenderer*/ renderer, string name, int value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_pack_start(IntPtr /*GtkTreeViewColumn*/ column, IntPtr /*GtkCellRenderer*/ renderer, bool expand);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_set_sort_column_id(IntPtr /*GtkTreeViewColumn*/ tree_column, int sort_column_id);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_set_resizable(IntPtr /*GtkTreeViewColumn*/ tree_column, bool resizable);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_column_get_resizable(IntPtr /*GtkTreeViewColumn*/ tree_column);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_column_set_reorderable(IntPtr /*GtkTreeViewColumn*/ tree_column, bool reorderable);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_column_get_reorderable(IntPtr /*GtkTreeViewColumn*/ tree_column);
	}
}
