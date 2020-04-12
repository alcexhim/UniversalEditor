//
//  GIMSwizzleEncoder.cs - provides functions for swizzle encoding/decoding pixel data in a GIM image file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Provides functions for swizzle encoding/decoding pixel data in a GIM image file.
	/// </summary>
	public class GIMSwizzleEncoder
	{
		// Swizzle a GIM image
		public static void Encode(ref byte[] Buf, int Pointer, int Width, int Height)
		{
			// Make a copy of the unswizzled input
			byte[] UnSwizzled = new byte[Buf.Length - Pointer];
			Array.Copy(Buf, Pointer, UnSwizzled, 0, UnSwizzled.Length);

			int rowblocks = (Width / 16);

			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					int blockx = x / 16;
					int blocky = y / 8;

					int block_index = blockx + ((blocky) * rowblocks);
					int block_address = block_index * 16 * 8;

					Buf[Pointer + block_address + (x - blockx * 16) + ((y - blocky * 8) * 16)] = UnSwizzled[x + (y * Width)];
				}
			}
		}

		// Unswizzle a GIM image
		public static void Decode(ref byte[] Buf, int Pointer, int Width, int Height)
		{
			// Make a copy of the unswizzled input
			byte[] Swizzled = new byte[Buf.Length - Pointer];
			Array.Copy(Buf, Pointer, Swizzled, 0, Swizzled.Length);

			int rowblocks = (Width / 16);

			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					int blockx = x / 16;
					int blocky = y / 8;

					int block_index = blockx + (blocky * rowblocks);
					int block_address = block_index * 16 * 8;

					Buf[Pointer + x + (y * Width)] = Swizzled[block_address + (x - blockx * 16) + ((y - blocky * 8) * 16)];
				}
			}
		}
	}
}
