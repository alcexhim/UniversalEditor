//
//  GtkSourceBuffer.cs
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
	internal class GtkSourceBuffer
	{
		[DllImport(GtkSourceView.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkSourceBuffer*/ gtk_source_buffer_new (IntPtr /*GtkTextTagTable*/ table);
		/// <summary>
		/// Creates a new source buffer using the highlighting patterns in language . This is equivalent to creating a new source buffer with a new tag table and then calling gtk_source_buffer_set_language().
		/// </summary>
		/// <returns>The source buffer new with language.</returns>
		/// <param name="language">Language.</param>
		[DllImport(GtkSourceView.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkSourceBuffer*/ gtk_source_buffer_new_with_language (IntPtr /*GtkSourceLanguage*/ language);
		[DllImport(GtkSourceView.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkSourceBuffer*/ gtk_source_buffer_set_language (IntPtr /*GtkSourceBuffer*/ buffer, IntPtr /*GtkSourceLanguage*/ language);
	}
}

