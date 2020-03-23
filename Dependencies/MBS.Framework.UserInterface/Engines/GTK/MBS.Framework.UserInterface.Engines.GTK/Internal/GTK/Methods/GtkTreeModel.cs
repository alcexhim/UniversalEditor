//
//  GtkTreeModel.cs
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
	internal class GtkTreeModel
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_model_get_iter(IntPtr /*GtkTreeModel*/ tree_model, ref Structures.GtkTreeIter iter, IntPtr /*GtkTreePath*/ path);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_model_get_iter_first(IntPtr /*GtkTreeModel*/ tree_model, ref Structures.GtkTreeIter iter);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_model_iter_next(IntPtr /*GtkTreeModel*/ tree_model, ref Structures.GtkTreeIter iter);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_tree_model_iter_previous(IntPtr /*GtkTreeModel*/ tree_model, ref Structures.GtkTreeIter iter);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_tree_model_get_path (IntPtr /*GtkTreeModel*/ tree_model, ref Structures.GtkTreeIter iter);
	}
}

