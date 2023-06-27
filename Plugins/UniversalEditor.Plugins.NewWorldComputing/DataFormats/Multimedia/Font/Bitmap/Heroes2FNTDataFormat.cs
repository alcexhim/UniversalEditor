//
//  Heroes2FNTDataFormat.cs
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
using UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN;
using UniversalEditor.ObjectModels.Multimedia.Font.Bitmap;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.DataFormats.Multimedia.Font.Bitmap
{
	public class Heroes2FNTDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(BitmapFontObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			BitmapFontObjectModel font = (objectModel as BitmapFontObjectModel);
			if (font == null)
				throw new ObjectModelNotSupportedException();

			uint size = Accessor.Reader.ReadUInt32();
			font.Size = new MBS.Framework.Drawing.Dimension2D(size, size);

			string fontFileName = Accessor.Reader.ReadFixedLengthString(13).TrimNull();

			Accessor accRelative = Accessor.GetRelative(fontFileName);
			if (accRelative != null)
			{
				ICNDataFormat icn = new ICNDataFormat();
				PictureCollectionObjectModel coll = new PictureCollectionObjectModel();
				Document.Load(coll, icn, accRelative);

				// from FONT.ICN
				string chars = "'!\"'$%&'()*+,`'/0123456789:;._.?.ABCDEFGHIJKLMNOPQRSTUVWXYZ[']'_.abcdefghijklmnopqrstuvwxyz.....";

				for (int i = 0;  i < coll.Pictures.Count;  i++)
				{
					PictureObjectModel pic = coll.Pictures[i];
					BitmapFontPixmap pixmap = new BitmapFontPixmap();
					BitmapFontGlyph glyph = new BitmapFontGlyph();
					glyph.Pixmap = pixmap;
					glyph.Character = chars[i];
					pixmap.Picture = pic;
					glyph.Picture = pic;

					pixmap.Glyphs.Add(glyph);
					font.Pixmaps.Add(pixmap);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
