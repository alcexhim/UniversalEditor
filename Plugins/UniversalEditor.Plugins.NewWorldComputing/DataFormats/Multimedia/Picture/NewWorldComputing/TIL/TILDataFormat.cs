//
//  TILDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.TIL
{
	public class TILDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);
			if (coll == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			ushort nTiles = reader.ReadUInt16();
			ushort nWidth = reader.ReadUInt16();
			ushort nHeight = reader.ReadUInt16();

			for (ushort i = 0; i < nTiles; i++)
			{
				PictureObjectModel pic = new PictureObjectModel();
				pic.Width = nWidth;
				pic.Height = nHeight;
				for (ushort x = 0; x < nWidth; x++)
				{
					for (ushort y = 0; y < nHeight; y++)
					{
						byte colorIndex = reader.ReadByte();
						Color color = HoMM2Palette.ColorTable[colorIndex];
						pic.SetPixel(color, x, y);
					}
				}
				coll.Pictures.Add(pic);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);
			if (coll == null)
				throw new ObjectModelNotSupportedException();

			ushort w = 0, h = 0;
			for (int i = 0; i < coll.Pictures.Count; i++)
			{
				if (coll.Pictures[i].Width > ushort.MaxValue || coll.Pictures[i].Height > ushort.MaxValue)
				{
					throw new ObjectModelNotSupportedException(String.Format("all PictureObjectModels must have dimensions less than UInt16.MaxValue ({0})", ushort.MaxValue));
				}
				if (i == 0)
				{
					w = (ushort)coll.Pictures[i].Width;
					h = (ushort)coll.Pictures[i].Height;
				}
				else
				{
					if (coll.Pictures[i].Width != w || coll.Pictures[i].Height != h)
						throw new ObjectModelNotSupportedException("all PictureObjectModels in PictureCollectionObjectModel MUST have the same Width and Height");
				}
			}

			Writer writer = Accessor.Writer;
			writer.WriteUInt16((ushort)coll.Pictures.Count);
			writer.WriteUInt16(w);
			writer.WriteUInt16(h);

			for (int i = 0; i < coll.Pictures.Count; i++)
			{
				for (ushort x = 0; x < w; x++)
				{
					for (ushort y = 0; y < h; y++)
					{
						Color color = coll.Pictures[i].GetPixel(x, y);
					}
				}
			}
		}
	}
}
