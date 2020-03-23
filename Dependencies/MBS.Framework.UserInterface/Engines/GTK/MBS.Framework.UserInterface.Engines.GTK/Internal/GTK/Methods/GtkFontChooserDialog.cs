//
//  GtkFontChooserDialog.cs
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
	internal class GtkFontChooserDialog
	{
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_font_chooser_dialog_new(string title, IntPtr parentHandle);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_font_selection_dialog_new(string title);

		public static IntPtr gtk_font_dialog_new(string title, IntPtr parentHandle, bool useLegacyFunctionality = false)
		{
			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3 && !useLegacyFunctionality)
			{
				return gtk_font_chooser_dialog_new(title, parentHandle);
			}
			else
			{
				return gtk_font_selection_dialog_new(title);
			}
		}

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern string gtk_font_chooser_get_font(IntPtr /*GtkFontSelectionDialog*/ fsd);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_font_selection_dialog_get_font_selection(IntPtr /*GtkFontSelectionDialog*/ fsd);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_font_chooser_get_font_family(IntPtr fsd);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern IntPtr gtk_font_chooser_get_font_face(IntPtr fsd);

		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern int gtk_font_selection_get_size(IntPtr fsd);
		[DllImport(Gtk.LIBRARY_FILENAME)]
		private static extern int gtk_font_chooser_get_font_size(IntPtr fsd);

		public static MBS.Framework.UserInterface.Drawing.Font gtk_font_dialog_get_font(IntPtr handle, bool useLegacyFunctionality = false)
		{
			IntPtr hFontFamily = IntPtr.Zero;
			IntPtr hFontFace = IntPtr.Zero;
			int fontSize = 0;

			if (Gtk.LIBRARY_FILENAME == Gtk.LIBRARY_FILENAME_V3 && !useLegacyFunctionality)
			{
				hFontFamily = gtk_font_chooser_get_font_family(handle);
				hFontFace = gtk_font_chooser_get_font_face(handle);
				fontSize = gtk_font_chooser_get_font_size(handle);
			}
			else
			{
				IntPtr hsel = gtk_font_selection_dialog_get_font_selection(handle);
				hFontFamily = GtkFontSelection.gtk_font_selection_get_family(hsel);
				hFontFace = GtkFontSelection.gtk_font_selection_get_face(hsel);
				fontSize = gtk_font_selection_get_size(hsel);
			}

			string strFontFamily = Internal.Pango.Methods.pango_font_family_get_name(hFontFamily);
			string strFontFace = Internal.Pango.Methods.pango_font_face_get_face_name(hFontFace);

			bool isBold = false, isItalic = false;
			string[] strFontFaceParts = strFontFace.Split(new char[] { ' ' });
			foreach (string strFontFacePart in strFontFaceParts)
			{
				switch (strFontFacePart.ToLower())
				{
				case "bold":
					{
						isBold = true;
						break;
					}
				case "italic":
					{
						isItalic = true;
						break;
					}
				}
			}

			MBS.Framework.UserInterface.Drawing.Font font = new MBS.Framework.UserInterface.Drawing.Font();
			if (isBold)
			{
				font.Weight = MBS.Framework.UserInterface.Drawing.FontWeights.Bold;
			}
			font.Italic = isItalic;
			font.FamilyName = strFontFamily;
			font.FaceName = strFontFace;
			font.Size = ((double)fontSize / 1024);
			return font;
		}
	}
}

