//
//  GtkTable.cs
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
	[Obsolete("GtkTable has been deprecated. Use GtkGrid instead. It provides the same capabilities as GtkTable for arranging widgets in a rectangular grid, but does support height-for-width geometry management.")]
	internal class GtkTable
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTable*/ gtk_table_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_table_attach(IntPtr /*GtkTable*/ table, IntPtr /*GtkWidget*/ child, uint left_attach, uint right_attach, uint top_attach, uint bottom_attach, Constants.GtkAttachOptions xoptions, Constants.GtkAttachOptions yoptions, uint xpadding, uint ypadding);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_table_set_row_spacings(IntPtr /*GtkTable*/ table, uint spacing);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_table_set_col_spacings(IntPtr /*GtkTable*/ table, uint spacing);
	}
}

