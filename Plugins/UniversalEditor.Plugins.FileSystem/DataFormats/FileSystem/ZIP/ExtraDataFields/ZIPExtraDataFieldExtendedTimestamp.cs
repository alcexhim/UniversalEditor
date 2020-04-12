//
//  ZIPExtraDataFieldExtendedTimestamp.cs - describes an extended timestamp extra data field in a ZIP archive
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

using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.FileSystem.ZIP.ExtraDataFields
{
	/// <summary>
	/// Describes an extended timestamp extra data field in a ZIP archive.
	/// </summary>
	public class ZIPExtraDataFieldExtendedTimestamp : ZIPExtraDataField
	{
		public ZIPExtraDataFieldExtendedTimestamp(DateTime modificationTimestamp = default(DateTime), DateTime accessTimestamp = default(DateTime), DateTime creationTimestamp = default(DateTime))
		{
			base.Type = ZIPExtraDataFieldType.ExtendedTimestamp;

			ModificationTimestamp = modificationTimestamp;
			AccessTimestamp = accessTimestamp;
			CreationTimestamp = creationTimestamp;
		}

		public DateTime ModificationTimestamp { get; set; } = DateTime.Now;
		public DateTime AccessTimestamp { get; set; } = DateTime.Now;
		public DateTime CreationTimestamp { get; set; } = DateTime.Now;

		protected override byte[] GetLocalDataInternal()
		{
			/*
			 * Local-header version:

			Value         Size        Description
			-----         ----        -----------
			(time)  0x5455        Short       tag for this extra block type ("UT")
			TSize         Short       total data size for this block
			Flags         Byte        info bits
			(ModTime)     Long        time of last modification (UTC/GMT)
			(AcTime)      Long        time of last access (UTC/GMT)
			(CrTime)      Long        time of original creation (UTC/GMT)
			*/
			MemoryAccessor ma = new MemoryAccessor();
			ma.Writer.WriteByte(7);
			ma.Writer.WriteDOSFileTime(ModificationTimestamp);
			ma.Writer.WriteDOSFileTime(AccessTimestamp);
			ma.Writer.WriteDOSFileTime(CreationTimestamp);
			return ma.ToArray();
		}
		protected override byte[] GetCentralDataInternal()
		{
			/*
			 * Central-header version:

			Value         Size        Description
			-----         ----        -----------
			(time)  0x5455        Short       tag for this extra block type ("UT")
			TSize         Short       total data size for this block
			Flags         Byte        info bits (refers to local header!)
			(ModTime)     Long        time of last modification (UTC/GMT)
			*/

			MemoryAccessor ma = new MemoryAccessor();
			ma.Writer.WriteByte(7);
			ma.Writer.WriteDOSFileTime(ModificationTimestamp);
			return ma.ToArray();
		}
	}
}
