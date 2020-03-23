//
//  GtkApplication.cs
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
	internal class GtkApplication
	{
		[DllImport(Gtk.LIBRARY_FILENAME_V3, EntryPoint = "gtk_application_new")]
		private static extern IntPtr gtk_application_new_v3(string application_id, Internal.GIO.Constants.GApplicationFlags flags);

		public static IntPtr gtk_application_new(string application_id, Internal.GIO.Constants.GApplicationFlags flags)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V2)
			{
				return IntPtr.Zero;
			}
			else
			{
				return gtk_application_new_v3(application_id, flags);
			}
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_application_add_window(IntPtr /*GtkApplication*/ application, IntPtr /*GtkWindow*/ window);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GMenuModel*/ gtk_application_get_menubar(IntPtr /*GtkApplication*/ application);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_application_set_menubar(IntPtr /*GtkApplication*/ application, IntPtr /*GMenuModel*/ menubar);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GMenuModel*/ gtk_application_get_app_menu(IntPtr /*GtkApplication*/ application);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_application_set_app_menu(IntPtr /*GtkApplication*/ application, IntPtr /*GMenuModel*/ menu);
	}
}

