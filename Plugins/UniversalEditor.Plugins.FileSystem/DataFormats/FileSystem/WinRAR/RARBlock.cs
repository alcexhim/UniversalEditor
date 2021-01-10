//
//  RARBlockHeaderV5.cs
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
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	public abstract class RARBlock : ICloneable
	{
		public class RARBlockCollection
			: System.Collections.ObjectModel.Collection<RARBlock>
		{

		}

		/// <summary>
		/// CRC32 of header data starting from Header size field and up to and including the optional extra area.
		/// </summary>
		public uint CRC;
		/// <summary>
		/// Size of header data starting from Header type field and up to and including the optional extra area. This field must not be longer than 3 bytes in
		/// current implementation, resulting in 2 MB maximum header size.
		/// </summary>
		public long Size;
		/// <summary>
		/// Type of archive header.
		/// </summary>
		public RARBlockType HeaderType;
		public RARBlockFlags HeaderFlags;
		/// <summary>
		/// Size of extra area. Optional field, present only if 0x0001 header flag is set.
		/// </summary>
		public long ExtraAreaSize;
		/// <summary>
		/// Size of data area.Optional field, present only if 0x0002 header flag is set.
		/// </summary>
		public long DataSize;
		
		public abstract object Clone();
	}
}
