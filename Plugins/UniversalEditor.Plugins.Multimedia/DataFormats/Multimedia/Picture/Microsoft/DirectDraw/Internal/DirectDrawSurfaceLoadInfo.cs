//
//  DirectDrawSurfaceLoadInfo.cs - internal structure representing DirectDraw Surface load information
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

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
	public enum DirectDrawSurfaceLoadInfoType : uint
	{
		None = 0x0000,
		UnsignedByte = 0x1401,
		UnsignedShort565 = 0x8363,
		UnsignedShort1555REV = 0x8366
	}
	public enum DirectDrawSurfaceLoadInfoFormat
	{
		RGB = 0x1907,
		CompressedRGBAS3TCDXT1 = 0x83F1,
		CompressedRGBAS3TCDXT3 = 0x83F2,
		CompressedRGBAS3TCDXT5 = 0x83F3,
		RGB5 = 0x8050,
		RGB8 = 0x8051,
		RGB5A1 = 0x8057,
		RGBA8 = 0x8058,
		BGR = 0x80E0,
		BGRA = 0x80E1
	}
	/// <summary>
	/// Internal structure representing DirectDraw Surface load information.
	/// </summary>
	internal struct DirectDrawSurfaceLoadInfo
	{
		public bool Compressed { get; }
		public bool Swap { get; }
		public bool Palette { get; }
		public uint DivSize { get; }
		public uint BlockBytes { get; }
		public DirectDrawSurfaceFormat Format { get; }

		public DirectDrawSurfaceLoadInfo(bool compressed, bool swap, bool palette, uint divSize, uint blockBytes, DirectDrawSurfaceFormat format)
		{
			Compressed = compressed;
			Swap = swap;
			Palette = palette;
			DivSize = divSize;
			BlockBytes = blockBytes;
			Format = format;
		}
	}
}
