//
//  GtkSelection.cs
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
	internal class GtkSelection
	{
		/// <summary>
		/// </summary>
		/// <param name="selection_data">a pointer to a GtkSelectionData</param>
		/// <param name="type">the type of selection data</param>
		/// <param name="format">format (number of bits in a unit)</param>
		/// <param name="data">pointer to the data (will be copied)</param>
		/// <param name="length">length of the data</param>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_selection_data_set(IntPtr /*GtkSelectionData*/ selection_data, IntPtr /*GdkAtom*/ type, int format, byte[] data, int length);
		/// <summary>
		/// Sets the contents of the selection from a UTF-8 encoded string. The string is converted to the form determined by selection_data->target.
		/// </summary>
		/// <param name="selection_data">a GtkSelectionData</param>
		/// <param name="str">a UTF-8 string</param>
		/// <param name="len">the length of str , or -1 if str is nul-terminated.</param>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern void gtk_selection_data_set_text(IntPtr /*GtkSelectionData*/ selection_data, string str, int len);
		/// <summary>
		/// Gets the contents of the selection data as a UTF-8 string.
		/// </summary>
		/// <returns>if the selection data contained a recognized text type and it could be converted to UTF-8, a newly allocated string containing the converted text, otherwise NULL. If the result is non-NULL it must be freed with g_free().</returns>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern string gtk_selection_data_get_text (IntPtr /*GtkSelectionData*/ selection_data);

		/// <summary>
		/// Gtks the selection data set uris.
		/// </summary>
		/// <returns><c>true</c>, if selection data set uris was gtked, <c>false</c> otherwise.</returns>
		/// <param name="selection_data">Selection data.</param>
		/// <param name="uris">Uris.</param>
		[DllImport(Gtk.LIBRARY_FILENAME)]
		public static extern bool gtk_selection_data_set_uris(IntPtr /*GtkSelectionData*/ selection_data, string[] uris);
	}
}

