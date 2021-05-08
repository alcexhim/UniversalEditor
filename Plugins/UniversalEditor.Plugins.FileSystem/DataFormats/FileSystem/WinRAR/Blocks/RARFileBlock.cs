//
//  RARFileHeaderV5.cs
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
namespace UniversalEditor.DataFormats.FileSystem.WinRAR.Blocks
{
	public class RARFileBlock : RARBlock
	{
		public V5.RARFileBlockFlags fileFlags;
		public long unpackedSize;
		public V5.RARFileAttributes attributes;
		public uint mtime;
		public uint dataCrc;
		public long compressionFlags;
		public RARHostOperatingSystem hostOperatingSystem;
		public long fileNameLength;
		public string fileName;

		public long dataOffset;

		public override object Clone()
		{
			RARFileBlock clone = new RARFileBlock();
			clone.CRC = CRC;
			clone.Size = Size;
			clone.HeaderType = HeaderType;
			clone.HeaderFlags = HeaderFlags;
			clone.ExtraAreaSize = ExtraAreaSize;
			clone.DataSize = DataSize;

			clone.fileFlags = fileFlags;
			clone.unpackedSize = unpackedSize;
			clone.attributes = attributes;
			clone.mtime = mtime;
			clone.dataCrc = dataCrc;
			clone.compressionFlags = compressionFlags;
			clone.hostOperatingSystem = hostOperatingSystem;
			clone.fileNameLength = fileNameLength;
			clone.fileName = fileName;

			clone.dataOffset = dataOffset;

			return clone;
		}
	}
}
