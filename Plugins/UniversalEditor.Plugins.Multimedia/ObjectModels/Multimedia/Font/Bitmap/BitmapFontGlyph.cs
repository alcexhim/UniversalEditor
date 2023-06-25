//
//  BitmapFontGlyph.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.ObjectModels.Multimedia.Font.Bitmap
{
	public class BitmapFontGlyph
	{
		public class BitmapFontGlyphCollection
			: System.Collections.ObjectModel.Collection<BitmapFontGlyph>
		{

		}

		public char Character { get; set; }

		public double X { get; set; }
		public double Y { get; set; }
		public BitmapFontPixmap Pixmap { get; set; } = null;
		public PictureObjectModel Picture { get; set; }
	}
}
