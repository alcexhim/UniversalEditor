//
//  BitmapFontObjectModel.cs
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Font.Bitmap
{
	public class BitmapFontObjectModel : ObjectModel
	{
		public string Title { get; set; } = null;
		public Dimension2D Size { get; set; }
		public BitmapFontPixmap.BitmapFontPixmapCollection Pixmaps { get; } = new BitmapFontPixmap.BitmapFontPixmapCollection();

		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		public BitmapFontGlyph FindGlyph(char c)
		{
			foreach (BitmapFontPixmap pixmap in Pixmaps)
			{
				foreach (BitmapFontGlyph glyph in pixmap.Glyphs)
				{
					if (glyph.Character == c)
						return glyph;
				}
			}
			return null;
		}
	}
}
