//
//  GtkAboutDialog.cs
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
	internal class GtkAboutDialog
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_about_dialog_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_program_name(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_program_name(IntPtr handle, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_version(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_version(IntPtr handle, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_copyright(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_copyright(IntPtr handle, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_comments(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_comments(IntPtr handle, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_license(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_license(IntPtr handle, string value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_license_type(IntPtr handle, Internal.GTK.Constants.GtkLicense value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_about_dialog_get_website(IntPtr handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_about_dialog_set_website(IntPtr handle, string value);
	}
}

