//
//  BlockInfo.cs - internal structure representing block information for an ALZip EGG archive
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

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG.Internal
{
	/// <summary>
	/// Internal structure representing block information for an ALZip EGG archive.
	/// </summary>
	public class BlockInfo
	{
		public ALZipCompressionMethod compressionMethod;
		public byte hint;
		public uint decompressedSize;
		public uint compressedSize;
		public uint crc32;
		public long offset;
		public byte[] compressedData;

		public BlockInfo(ALZipCompressionMethod compressionMethod, byte hint, uint decompressedSize, uint compressedSize, uint crc32, long offset, byte[] compressedData = null)
		{
			// TODO: Complete member initialization
			this.compressionMethod = compressionMethod;
			this.hint = hint;
			this.decompressedSize = decompressedSize;
			this.compressedSize = compressedSize;
			this.crc32 = crc32;
			this.offset = offset;
			this.compressedData = compressedData;
		}
	}
}
