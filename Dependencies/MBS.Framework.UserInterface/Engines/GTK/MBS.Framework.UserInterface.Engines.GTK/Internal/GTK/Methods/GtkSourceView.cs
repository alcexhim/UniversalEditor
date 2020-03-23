//
//  GtkSourceView.cs
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
	internal class GtkSourceView
	{
		public const string LIBRARY_FILENAME = "libgtksourceview-3.0";

		/// <summary>
		/// Creates a new GtkSourceView.
		/// By default, an empty GtkSourceBuffer will be lazily created and can be retrieved with <see cref="GtkTextView.gtk_text_view_get_buffer" /> ().
		/// If you want to specify your own buffer, either override the GtkTextViewClass create_buffer factory method, or use <see cref="gtk_source_view_new_with_buffer"/>  ().
		/// </summary>
		/// <returns>The source view new.</returns>
		[DllImport(GtkSourceView.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_source_view_new();
		/// <summary>
		/// Creates a new GtkSourceView widget displaying the buffer buffer . One buffer can be shared among many widgets.
		/// </summary>
		/// <returns>a new GtkSourceView.</returns>
		/// <param name="buffer">a GtkSourceBuffer.</param>
		[DllImport(GtkSourceView.LIBRARY_FILENAME)]
		public static extern IntPtr /*GtkWidget*/ gtk_source_view_new_with_buffer(IntPtr /*GtkSourceBuffer*/ buffer);
	}
}

