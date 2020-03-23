//
//  GtkNotebook.cs
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
	internal class GtkNotebook
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_notebook_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_notebook_append_page(IntPtr hNotebook, IntPtr hChild, IntPtr hTabLabel);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_notebook_insert_page(IntPtr hNotebook, IntPtr hChild, IntPtr hTabLabel, int position);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_notebook_get_n_pages(IntPtr hNotebook);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_notebook_get_nth_page(IntPtr hNotebook, int n);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_notebook_page_num(IntPtr hNotebook, IntPtr hChild);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_remove_page(IntPtr hNotebook, int page_num);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_tab_label_text(IntPtr /*GtkNotebook*/ hNotebook, IntPtr /*GtkWidget*/ hPage, string text);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_tab_pos(IntPtr handle, Constants.GtkPositionType value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_tab_reorderable(IntPtr hNotebook, IntPtr hChild, bool reorderable);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_tab_detachable(IntPtr hNotebook, IntPtr hChild, bool detachable);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_notebook_get_tab_label_text(IntPtr hNotebook, IntPtr hChild);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_current_page(IntPtr /*GtkNotebook*/ notebook, int page_num);

		[DllImport(Gtk.LIBRARY_FILENAME, EntryPoint = "gtk_notebook_get_group_name")]
		private static extern IntPtr gtk_notebook_get_group_name_internal(IntPtr hNotebook);

		public static string gtk_notebook_get_group_name(IntPtr hNotebook)
		{
			IntPtr h = gtk_notebook_get_group_name_internal(hNotebook);
			string value = Marshal.PtrToStringAuto(h);
			return value;
		}
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_notebook_set_group_name(IntPtr hNotebook, string value);
	}
}

