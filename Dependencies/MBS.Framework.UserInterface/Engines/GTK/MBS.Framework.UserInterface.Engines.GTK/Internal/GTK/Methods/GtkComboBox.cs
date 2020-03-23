//
//  GtkComboBox.cs
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
	internal class GtkComboBox
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkComboBox*/ gtk_combo_box_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkComboBox*/ gtk_combo_box_new_with_area(IntPtr /*GtkCellArea*/ area);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkComboBox*/ gtk_combo_box_new_with_entry();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_combo_box_set_model(IntPtr /*GtkComboBox*/ combo_box, IntPtr /*GtkTreeModel*/ model);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeModel*/ gtk_combo_box_get_model(IntPtr /*GtkComboBox*/ combo_box);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_combo_box_set_entry_text_column(IntPtr /*GtkComboBox*/ combo_box, int value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_combo_box_get_entry_text_column(IntPtr /*GtkComboBox*/ combo_box);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_combo_box_get_active_iter(IntPtr /*GtkComboBox*/ handle, ref Structures.GtkTreeIter iter);
	}
}

