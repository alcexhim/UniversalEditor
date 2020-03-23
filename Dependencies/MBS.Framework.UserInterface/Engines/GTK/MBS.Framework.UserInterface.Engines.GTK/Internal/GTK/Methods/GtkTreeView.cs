//
//  GtkTreeView.cs
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
	internal class GtkTreeView
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_tree_view_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_tree_view_new_with_model(IntPtr /*GtkTreeModel*/ model);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_set_headers_visible(IntPtr /*GtkTreeView*/ tree_view, bool headers_visible);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_set_headers_clickable(IntPtr /*GtkTreeView*/ tree_view, bool setting);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeSelection*/ gtk_tree_view_get_selection(IntPtr /*GtkTreeView*/ tree_view);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_tree_view_insert_column(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreeViewColumn*/ column, int position);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_insert_column_with_attributes(IntPtr handle, int position, string title, IntPtr /*GtkCellRenderer*/ cell, string attributeName, int columnIndexForAttributeValue, IntPtr setThisToZero);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeModel*/ gtk_tree_view_get_model(IntPtr /*GtkTreeView*/ tree_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_set_model(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreeModel*/ model);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_get_path_at_pos(IntPtr /*GtkTreeView*/ tree_view, int x, int y, ref IntPtr /*GtkTreePath**/ path, ref IntPtr /*GtkTreeViewColumn**/ column, ref int cell_x, ref int cell_y);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_enable_model_drag_source(IntPtr /*GtkWidget*/ widget, GDK.Constants.GdkModifierType start_button_mask, Structures.GtkTargetEntry[] targets, int n_targets, GDK.Constants.GdkDragAction actions);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_view_enable_model_drag_dest(IntPtr /*GtkWidget*/ widget, Structures.GtkTargetEntry[] targets, int n_targets, GDK.Constants.GdkDragAction actions);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_expand_row(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreePath*/ path, bool open_all);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_collapse_row(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreePath*/ path);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_view_row_expanded(IntPtr /*GtkTreeView*/ tree_view, IntPtr /*GtkTreePath*/ path);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_tree_view_get_column(IntPtr /*GtkTreeView*/ tree_view, int n);
	}
}

