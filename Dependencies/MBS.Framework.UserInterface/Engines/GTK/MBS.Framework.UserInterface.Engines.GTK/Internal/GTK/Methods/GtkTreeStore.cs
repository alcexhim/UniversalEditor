//
//  GtkTreeStore.cs
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
	internal class GtkTreeStore
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeStore*/ gtk_tree_store_newv(int ncolumns, IntPtr[] columnTypes);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_clear(IntPtr /*GtkTreeStore*/ tree_store);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_insert(IntPtr hTreeStore, out Structures.GtkTreeIter hIter, IntPtr hIterParent, int position);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_insert(IntPtr hTreeStore, out Structures.GtkTreeIter hIter, ref Structures.GtkTreeIter iterParent, int position);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_append(IntPtr hTreeStore, out Structures.GtkTreeIter hIter, IntPtr hIterParent);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_append(IntPtr hTreeStore, out Structures.GtkTreeIter hIter, ref Structures.GtkTreeIter iterParent);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_remove(IntPtr /*GtkTreeStore*/ tree_store, ref Structures.GtkTreeIter iter);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_tree_store_set_value(IntPtr hTreeStore, ref Structures.GtkTreeIter hIter, int columnIndex, ref GLib.Structures.Value val);
	}
}

