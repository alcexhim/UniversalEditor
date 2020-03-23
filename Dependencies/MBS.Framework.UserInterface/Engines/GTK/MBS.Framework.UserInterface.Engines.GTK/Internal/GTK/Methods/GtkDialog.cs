//
//  GtkDialog.cs
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
	internal class GtkDialog
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_dialog_get_type();
		[DllImport(Gtk.LIBRARY_FILENAME), Obsolete("Use gtk_dialog_new_with_buttons to properly set parent window")]
		public static extern IntPtr gtk_dialog_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_dialog_new_with_buttons(string title, IntPtr hParent, Constants.GtkDialogFlags flags, string first_button_text);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_dialog_run(IntPtr handle);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_response(IntPtr /*GtkDialog*/ dialog, Constants.GtkResponseType response_id);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_response(IntPtr /*GtkDialog*/ dialog, int response_id);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_dialog_get_content_area(IntPtr /*GtkDialog*/ dialog);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_dialog_add_button(IntPtr /*GtkDialog*/ dialog, string button_text, int response_id);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_dialog_add_button(IntPtr /*GtkDialog*/ dialog, string button_text, Constants.GtkResponseType response_id);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_add_action_widget(IntPtr /*GtkDialog*/ dialog, IntPtr /*GtkWidget*/ child, int response_id);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_add_action_widget(IntPtr /*GtkDialog*/ dialog, IntPtr /*GtkWidget*/ child, Constants.GtkResponseType response_id);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_dialog_get_default_response(IntPtr /*GtkDialog*/ dialog);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_set_default_response(IntPtr /*GtkDialog*/ dialog, int value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_dialog_set_default_response(IntPtr /*GtkDialog*/ dialog, Constants.GtkResponseType value);
	}
}

