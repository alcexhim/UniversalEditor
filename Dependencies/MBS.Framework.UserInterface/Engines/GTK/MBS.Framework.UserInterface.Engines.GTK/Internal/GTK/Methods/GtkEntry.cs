//
//  GtkEntry.cs
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
	internal class GtkEntry
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern GType gtk_entry_get_type();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_entry_new();

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern ushort gtk_entry_get_text_length(IntPtr /*GtkEntry*/ entry);

		[DllImport(Gtk.LIBRARY_FILENAME, EntryPoint = "gtk_entry_get_text")]
		private static extern IntPtr gtk_entry_get_text_ptr(IntPtr /*GtkEntry*/ entry);
		public static string gtk_entry_get_text(IntPtr /*GtkEntry*/ entry)
		{
			IntPtr ptr = gtk_entry_get_text_ptr(entry);
			string val = Marshal.PtrToStringAuto(ptr);
			return val;
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_entry_set_text(IntPtr /*GtkEntry*/ entry, string value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_entry_get_visibility(IntPtr /*GtkEntry*/ entry);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_entry_set_visibility(IntPtr /*GtkEntry*/ entry, bool value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_entry_get_max_length(IntPtr /*GtkEntry*/ entry);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_entry_set_max_length(IntPtr /*GtkEntry*/ entry, int value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern int gtk_entry_get_width_chars(IntPtr /*GtkEntry*/ entry);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_entry_set_width_chars(IntPtr /*GtkEntry*/ entry, int value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_entry_get_activates_default(IntPtr /*GtkEntry*/ entry);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_entry_set_activates_default(IntPtr /*GtkEntry*/ entry, bool value);
	}
}

