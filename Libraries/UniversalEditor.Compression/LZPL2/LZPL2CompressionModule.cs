//
//  LZPL2CompressionModule.cs - provides a CompressionModule for handling LZPL2 compression
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

namespace UniversalEditor.Compression.LZPL2
{
	/// <summary>
	/// Provides a <see cref="CompressionModule" /> for handling LZPL2 compression.
	/// </summary>
	public class LZPL2CompressionModule
	{
		private int mvarBufferSize = 4096;
		public int BufferSize { get { return mvarBufferSize; } set { mvarBufferSize = value; } }

		private static LZPL2CompressionModule mvarDefaultModule = new LZPL2CompressionModule();
		public static LZPL2CompressionModule DefaultModule { get { return mvarDefaultModule; } }

		public byte[] Decompress(byte[] data, uint decompressedSize)
		{
			byte[] buffer = new byte[mvarBufferSize];
			byte mask = 0, flags = 0, x = 0, y = 0, b = 0;

			uint srcpos = 0, dstpos = 0;

			byte[] decompressedData = new byte[decompressedSize];
			while ((srcpos < data.Length) && (dstpos < decompressedData.Length))
			{
				if (mask == 0)
				{
					flags = data[srcpos++];
					if (srcpos >= data.Length) break;
					mask = 1;
				}

				if ((flags & mask) != 0)
				{
					// Raw byte
					buffer[dstpos % mvarBufferSize] = data[srcpos];
					decompressedData[dstpos++] = data[srcpos++];
				}
				else
				{
					// Pointer: 0xAB 0xCD (CAB=pointer, D=length)
					x = data[srcpos++];
					y = data[srcpos++];

					int off = (((y & 0xf0) << 4) | x) + 18;
					int len = (y & 0x0f) + 3;

					while ((len-- > 0) && (dstpos < decompressedData.Length))
					{
						b = buffer[off++ % mvarBufferSize];
						buffer[dstpos % mvarBufferSize] = b;
						decompressedData[dstpos++] = b;
					}
				}
				mask <<= 1;
			}
			return decompressedData;
		}

	}
}
