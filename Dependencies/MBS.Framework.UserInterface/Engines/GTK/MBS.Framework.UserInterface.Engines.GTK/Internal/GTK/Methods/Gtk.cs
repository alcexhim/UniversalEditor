//
//  Gtk.cs
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
	internal static class Gtk
	{
		public const string LIBRARY_FILENAME_V2 = "gtk-x11-2.0";
		public const string LIBRARY_FILENAME_V3 = "gtk-3";

		// using GTK3 seems to sacrifice theming support, whine whine
		public const string LIBRARY_FILENAME = LIBRARY_FILENAME_V3;

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_init_check(ref int argc, ref string[] args);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_main();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_main_quit();

		// used to implement the equivalent of System.Windows.Forms.Application.DoEvents() in GTK
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_events_pending();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_main_iteration();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern uint gtk_get_major_version();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern uint gtk_get_minor_version();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern uint gtk_get_micro_version();

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool gtk_enumerate_printers(Func<IntPtr, IntPtr, bool> func, IntPtr data, Action<IntPtr> destroyNotify, bool wait);
	}
}

