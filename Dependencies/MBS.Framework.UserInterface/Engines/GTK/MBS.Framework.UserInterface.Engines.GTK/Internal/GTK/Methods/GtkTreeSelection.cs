//
//  GtkTreeSelection.cs
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
	internal class GtkTreeSelection
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_selection_get_selected(IntPtr /*GtkTreeSelection*/ selection, ref IntPtr /*GtkTreeModel*/ model, Structures.GtkTreeIter[] iter);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ gtk_tree_selection_get_selected_rows(IntPtr /*GtkTreeSelection*/ selection, ref IntPtr /*GtkTreeModel*/ model);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_tree_selection_count_selected_rows(IntPtr /*GtkTreeSelection*/ selection);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_selection_select_all(IntPtr /*GtkTreeSelection*/ selection);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_selection_unselect_all(IntPtr /*GtkTreeSelection*/ selection);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_selection_set_mode(IntPtr /*GtkTreeSelection*/ selection, Constants.GtkSelectionMode type);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkSelectionMode gtk_tree_selection_get_mode(IntPtr /*GtkTreeSelection*/ selection);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_selection_set_select_function(IntPtr /*GtkTreeSelection*/ selection, Delegates.GtkTreeSelectionFunc func, IntPtr data, IntPtr destroy);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_selection_set_select_function(IntPtr /*GtkTreeSelection*/ selection, IntPtr func, IntPtr data, IntPtr destroy);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_selection_select_iter(IntPtr /*GtkTreeSelection*/ selection, ref Structures.GtkTreeIter iter);
	}
}

