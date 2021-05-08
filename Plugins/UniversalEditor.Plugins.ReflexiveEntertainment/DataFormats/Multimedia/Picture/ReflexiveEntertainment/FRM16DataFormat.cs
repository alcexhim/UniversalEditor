//
//  FRM16DataFormat.cs - provides a DataFormat for manipulating images in Reflexive Arcade's FRM16 format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.ReflexiveEntertainment
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in Reflexive Arcade's FRM16 format.
	/// </summary>
	public class FRM16DataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://lionheart.eowyn.cz/doku.php?id=formats:frm16");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pict = (objectModel as PictureObjectModel);
			if (pict == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			byte[] signature = reader.ReadBytes(2);
			if (!(signature[0] == 0x32 && signature[1] == 0x10)) throw new InvalidDataFormatException("File does not begin with { 0x32, 0x10 }");

			short anchorX = reader.ReadInt16();
			short anchorY = reader.ReadInt16();

			short width = reader.ReadInt16();
			short height = reader.ReadInt16();
			pict.Width = width;
			pict.Height = height;

			FRM16Flags flags = (FRM16Flags)reader.ReadInt16();
			FRM16Type type = (FRM16Type)reader.ReadInt16();
			short unknown1 = reader.ReadInt16();

			int bitmapSizeLayer1 = reader.ReadInt32(); // Bitmap size for the 1st, opaque layer
			int bitmapSizeLayer2 = reader.ReadInt32(); // Bitmap size for the 2nd layer, unused
			int bitmapSizeLayer3 = reader.ReadInt32(); // Bitmap size for the 3rd layer, unused
			int bitmapSizeLayer4 = reader.ReadInt32(); // Bitmap size for the 4th layer, alpha
			int bitmapSizeLayer5 = reader.ReadInt32(); // Bitmap size for the 5th layer, RGB

			// Each layer consists of a bitmap and a scanline lookup table (LUT).

			// Pixel color is encoded as a 16bit word, with a 5-6-5 RGB encoding.
			// FIXME: How are fully transparent pixels encoded?

			// Type 64 files do not use any compression. Each bitmap consists of width x height pixels of
			// 2 bytes each.

			// Type 68 FRM16 files use Run Length Encoding, RLE. Each bitmap line consists of variable number of
			// pixel runs. The RLE scheme uses two highest bits in the first byte of each run. If bit 7 is set,
			// bits 0-6 contain number of pixels to skip in the resulting image. If bit 7 is clear and bit 6 is
			// set, bits 0-5 contain number of 16bit pixels which follow. If both bits 7 and 6 are cleared, bits
			// 0-5 contain a number of repetitions of the following 16 bit pixel.

			for (short x = 0; x < width; x++)
			{
				for (short y = 0; y < height; y++)
				{
					short pixel = reader.ReadInt16();
					int r = pixel.GetBits(0, 5);
					int g = pixel.GetBits(5, 6);
					int b = pixel.GetBits(11, 5);
					pict.SetPixel(Color.FromRGBAInt32(r, g, b), x, y);
				}
			}

			for (short i = 0; i < (height * 4) - 4; i++)
			{
				int scanlineOffset = reader.ReadInt32();

			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
