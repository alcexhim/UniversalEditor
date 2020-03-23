//
//  GtkSeparator.cs
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
	internal class GtkWindow
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_window_new(Constants.GtkWindowType windowType);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_window_get_title(IntPtr window);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_title(IntPtr window, IntPtr title);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_transient_for(IntPtr /*GtkWindow*/ window, IntPtr /*GtkWindow*/ parent);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_window_get_icon_name(IntPtr window);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_icon_name(IntPtr window, string icon_name);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_focus_on_map(IntPtr window, bool value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_window_get_hide_titlebar_when_maximized(IntPtr window);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_hide_titlebar_when_maximized(IntPtr window, bool value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_add_accel_group(IntPtr window, IntPtr accel_group);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_default_size(IntPtr /*GtkWindow*/ window, int width, int height);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_get_default_size(IntPtr /*GtkWindow*/ window, out int width, out int height);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_resize(IntPtr /*GtkWindow*/ window, int width, int height);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_move(IntPtr /*GtkWindow*/ window, int x, int y);

		/// <summary>
		/// Gtks the window set titlebar.
		/// </summary>
		/// <param name="window">Window.</param>
		/// <param name="titlebar">Titlebar.</param>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_titlebar(IntPtr /*GtkWindow*/ window, IntPtr /*GtkWidget*/ titlebar);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_decorated(IntPtr /*GtkWindow*/ handle, bool decorated);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_gravity(IntPtr /*GtkWindow*/ handle, GDK.Constants.GdkGravity gravity);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_window_set_position(IntPtr /*GtkWindow*/ handle, Constants.GtkWindowPosition position);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_window_has_toplevel_focus(IntPtr /*GtkWindow*/ window);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GList*/ gtk_window_list_toplevels();


	}
}

