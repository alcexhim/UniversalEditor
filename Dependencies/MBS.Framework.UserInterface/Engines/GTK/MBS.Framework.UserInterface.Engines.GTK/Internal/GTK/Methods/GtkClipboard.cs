//
//  GtkClipboard.cs
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
	internal static class GtkClipboard
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkClipboard*/ gtk_clipboard_get_default(IntPtr /*GdkDisplay*/ display);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_clipboard_clear(IntPtr /*GtkClipboard*/ clipboard);

		[DllImport(Gtk.LIBRARY_FILENAME)]	
		public static extern void gtk_clipboard_request_text(IntPtr handle, Action<IntPtr /*GtkClipboard*/, string, IntPtr> text_received_func, IntPtr user_data);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_clipboard_set_text(IntPtr /*GtkClipboard*/ clipboard, string text, int len);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_clipboard_wait_is_text_available(IntPtr /*GtkClipboard*/ handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_clipboard_wait_is_image_available(IntPtr /*GtkClipboard*/ handle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_clipboard_wait_is_uris_available(IntPtr /*GtkClipboard*/ handle);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_clipboard_wait_for_text(IntPtr /*GtkClipboard*/ handle);
		/// <summary>
		/// Returns a list of targets that are present on the clipboard, or NULL if there aren’t any targets available. The returned list must be freed with
		/// g_free(). This function waits for the data to be received using the main loop, so events, timeouts, etc, may be dispatched during the wait.
		/// </summary>
		/// <returns><c>true</c> if any targets are present on the clipboard; otherwise, <c>false</c>.</returns>
		/// <param name="clipboard">a GtkClipboard</param>
		/// <param name="targets">location to store an array of targets. The result stored here must be freed with g_free(). </param>
		/// <param name="n_targets">location to store number of items in targets . </param>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_clipboard_wait_for_targets(IntPtr /*GtkClipboard*/ clipboard, ref IntPtr /*GdkAtom*/ targets, ref int n_targets);
	}
}
