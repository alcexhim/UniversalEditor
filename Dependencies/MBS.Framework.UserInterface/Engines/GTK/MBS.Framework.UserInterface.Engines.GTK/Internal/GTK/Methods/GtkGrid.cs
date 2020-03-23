//
//  GtkGrid.cs
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
	internal class GtkGrid
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_grid_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern uint gtk_grid_get_row_spacing(IntPtr /*GtkGrid*/ grid);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_grid_set_row_spacing(IntPtr /*GtkGrid*/ grid, uint spacing);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern uint gtk_grid_get_column_spacing(IntPtr /*GtkGrid*/ grid);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_grid_set_column_spacing(IntPtr /*GtkGrid*/ grid, uint spacing);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_grid_attach(IntPtr /*GtkGrid*/ grid, IntPtr /*GtkWidget*/ widget, int left, int top, int width, int height);
	}
}

