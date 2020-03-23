//
//  GtkIconView.cs
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
	internal class GtkIconView
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_icon_view_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkTreeModel*/ gtk_icon_view_get_model(IntPtr /*GtkIconView*/ icon_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_set_model(IntPtr /*GtkIconView*/ icon_view, IntPtr /*GtkTreeModel*/ model);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ gtk_icon_view_get_selected_items(IntPtr /*GtkIconView*/ icon_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_set_text_column(IntPtr /*GtkIconView*/ icon_view, int column);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_icon_view_get_text_column(IntPtr /*GtkIconView*/ icon_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_set_pixbuf_column(IntPtr /*GtkIconView*/ icon_view, int column);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_icon_view_get_pixbuf_column(IntPtr /*GtkIconView*/ icon_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_set_item_width(IntPtr /*GtkIconView*/ icon_view, int column);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_icon_view_get_item_width(IntPtr /*GtkIconView*/ icon_view);[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkSelectionMode gtk_icon_view_get_selection_mode(IntPtr /*GtkIconView*/ icon_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_set_selection_mode(IntPtr /*GtkIconView*/ icon_view, Constants.GtkSelectionMode type);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_enable_model_drag_source(IntPtr /*GtkWidget*/ widget, GDK.Constants.GdkModifierType start_button_mask, Structures.GtkTargetEntry[] targets, int n_targets, GDK.Constants.GdkDragAction actions);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_icon_view_enable_model_drag_dest(IntPtr /*GtkWidget*/ widget, Structures.GtkTargetEntry[] targets, int n_targets, GDK.Constants.GdkDragAction actions);
	}
}

