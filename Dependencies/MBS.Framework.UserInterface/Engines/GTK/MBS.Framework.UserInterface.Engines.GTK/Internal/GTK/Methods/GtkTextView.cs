//
//  GtkTextView.cs
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
	internal class GtkTextView
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_text_view_new();
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr gtk_text_view_get_buffer(IntPtr /*GtkTextView*/ text_view);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_text_view_set_buffer(IntPtr /*GtkTextView*/ text_view, IntPtr /*GtkTextBuffer*/ buffer);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_text_view_set_wrap_mode(IntPtr /*GtkTextView*/ text_view, Constants.GtkWrapMode mode);
	}
}

