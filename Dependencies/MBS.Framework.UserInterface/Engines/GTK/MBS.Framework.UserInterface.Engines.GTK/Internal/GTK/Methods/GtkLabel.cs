//
//  GtkLabel.cs
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
	internal class GtkLabel
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_label_new(string text);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_label_new_with_mnemonic(string text);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_line_wrap(IntPtr /*GtkLabel*/ label, bool wrap);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_xalign(IntPtr /*GtkLabel*/ label, double value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*PangoAttrList*/ gtk_label_get_attributes(IntPtr /*GtkLabel*/ label);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_attributes(IntPtr /*GtkLabel*/ label, IntPtr /*PangoAttrList*/ attrs);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_label_get_text(IntPtr /*GtkLabel*/ label);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_text(IntPtr /*GtkLabel*/ label, IntPtr value);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_label_get_label(IntPtr /*GtkLabel*/ label);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_label(IntPtr /*GtkLabel*/ label, IntPtr value);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_label_set_justify(IntPtr /*GtkLabel*/ label, Constants.GtkJustification jtype);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern Constants.GtkJustification gtk_label_get_justify(IntPtr /*GtkLabel*/ label);
	}
}

