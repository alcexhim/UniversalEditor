//
//  GtkPrintUnixDialog.cs
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
	/// <summary>
	/// GtkPrintUnixDialog implements a print dialog for platforms which don’t provide a native print dialog, like Unix. It can be used very much like any other GTK+ dialog, at the cost of the portability offered by the high-level printing API
	/// In order to print something with GtkPrintUnixDialog, you need to use gtk_print_unix_dialog_get_selected_printer() to obtain a GtkPrinter object and use it to construct a GtkPrintJob using gtk_print_job_new().
	/// GtkPrintUnixDialog uses the following response values: GTK_RESPONSE_OK: for the “Print” button, GTK_RESPONSE_APPLY: for the “Preview” button, GTK_RESPONSE_CANCEL: for the “Cancel” button
	/// </summary>
	public static class GtkPrintUnixDialog
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_print_unix_dialog_new(string title, IntPtr /*GtkWindow*/ parent);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkPrinter*/ gtk_print_unix_dialog_get_selected_printer(IntPtr /*GtkPrintUnixDialog*/ dialog);
	}
}
