//
//  PCXHeader.cs - internal structure representing the header of a PC Paintbrush (PCX) image file
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.ZSoft
{
	/// <summary>
	/// Internal structure representing the header of a PC Paintbrush (PCX) image file.
	/// </summary>
	public struct PCXHeader
	{
		public byte Manufacturer;
		public byte Version;
		public byte Encoding;
		public byte BitsPerPixel;

		public short XStart;
		public short YStart;
		public short XEnd;
		public short YEnd;

		public short HDpi;
		public short VDpi;

		public byte[] Palette;
		public byte Reserved1;
		public byte NumBitPlanes;

		public short BytesPerLine;
		public short PaletteInfo;
		public short HorizontalScreenSize;
		public short VerticalScreenSize;

		public int Width { get { return 1 + XEnd - XStart; } }
		public int Height { get { return 1 + YEnd - YStart; } }
	}
}
