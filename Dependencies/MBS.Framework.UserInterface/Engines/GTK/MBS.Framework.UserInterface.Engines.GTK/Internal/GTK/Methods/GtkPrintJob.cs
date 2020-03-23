//
//  GtkPrintJob.cs
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
	internal static class GtkPrintJob
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkPrintJob*/ gtk_print_job_new(string title, IntPtr /*GtkPrinter*/ printer, IntPtr /*GtkPrintSettings*/ settings, IntPtr /*GtkPageSetup*/ page_setup);

		[DllImport(Gtk.LIBRARY_FILENAME)]
 		public static extern void gtk_print_job_send(IntPtr /*GtkPrintJob*/ job, Delegates.GtkPrintJobCompleteFunc callback, IntPtr user_data, GObject.Delegates.GDestroyNotify dnotify);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkPrintStatus gtk_print_job_get_status(IntPtr /*GtkPrintJob*/ job);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_surface_t*/ gtk_print_job_get_surface(IntPtr /*GtkPrintJob*/ job, ref IntPtr error);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*cairo_surface_t*/ gtk_print_job_get_surface(IntPtr /*GtkPrintJob*/ job, ref GLib.Structures.GError error);
	}
}
