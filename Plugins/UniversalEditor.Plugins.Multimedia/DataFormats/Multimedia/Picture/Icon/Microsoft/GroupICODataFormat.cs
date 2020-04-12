//
//  GroupICODataFormat.cs - provides a DataFormat for manipulating images in Windows icon (ICO) format
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

using UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Bitmap;

using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Icon.Microsoft
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in Windows icon (ICO) format.
	/// </summary>
	public class GroupICODataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
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
			IO.Reader br = base.Accessor.Reader;

			// An Icon file, which usually has the ICO extension, contains one icon resource. Given that
			// an icon resource can contain multiple images, it is no surprise that the file begins with
			// an icon directory:
			byte u = br.ReadByte();

			short idReserved = br.ReadInt16();   // Reserved (must be 0)
			short idType = br.ReadInt16();       // Resource Type (1 for icons)
			short idCount = br.ReadInt16();      // How many images?

			for (short i = 0; i < idCount; i++)
			{
				// The idCount member indicates how many images are present in the icon resource. The size
				// of the idEntries array is determined by idCount. There exists one ICONDIRENTRY for each
				// icon image in the file, providing details about its location in the file, size and color
				// depth. The ICONDIRENTRY structure is defined as:

				byte bWidth = br.ReadByte();            // Width, in pixels, of the image
				byte bHeight = br.ReadByte();           // Height, in pixels, of the image
				byte bColorCount = br.ReadByte();       // Number of colors in image (0 if >=8bpp)
				byte bReserved = br.ReadByte();         // Reserved ( must be 0)
				short wPlanes = br.ReadInt16();         // Color Planes
				short wBitCount = br.ReadInt16();       // Bits per pixel
				int dwBytesInRes = br.ReadInt32();      // How many bytes in this resource?
				int dwImageOffset = br.ReadInt32();     // Where in the file is this image?

				// The dwBytesInRes member indicates the size of the image data. This image data can be found
				// dwImageOffset bytes from the beginning of the file, and is stored in the following format:
				BitmapInfoHeader icHeader = BitmapInfoHeader.Load(br);      // DIB header

				// RGBQUAD         icColors[1];   // Color table
				// BYTE            icXOR[1];      // DIB bits for XOR mask
				// BYTE            icAND[1];      // DIB bits for AND mask
			}

		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
