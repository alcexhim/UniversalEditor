//
//  GtkCellArea.cs
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
using static MBS.Framework.UserInterface.Engines.GTK.Internal.GLib.Structures;

namespace MBS.Framework.UserInterface.Engines.GTK.Internal.GTK.Methods
{
	internal static class GtkCellArea
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_cell_area_cell_set(IntPtr /*GtkCellArea*/ area, IntPtr /*GtkCellRenderer*/ renderer, string first_prop_name, int first_prop_value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_cell_area_cell_set_property(IntPtr /*GtkCellArea*/ area, IntPtr /*GtkCellRenderer*/ renderer, string first_prop_name, Value first_prop_value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_cell_area_attribute_connect(IntPtr /*GtkCellArea*/ area, IntPtr /*GtkCellRenderer*/ renderer, string attribute, int column);
	}
}
