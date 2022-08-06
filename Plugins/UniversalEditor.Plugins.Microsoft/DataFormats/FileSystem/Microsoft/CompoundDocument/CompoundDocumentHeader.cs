//
//  CompoundDocumentHeader.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	public struct CompoundDocumentHeader
	{
		public byte[] Signature { get; set; }
		public Guid UniqueIdentifier { get; set; }
		public ushort MinorVersion { get; set; }
		public ushort MajorVersion { get; set; }
		public byte[] ByteOrderIdentifier { get; set; }
		public ushort SectorSize { get; set; }
		public ushort ShortSectorSize { get; set; }
		public byte[] Unused1 { get; set; }
		public uint DirectorySectorCount { get; set; }
		/// <summary>
		/// Total number of sectors used for the sector allocation table
		/// </summary>
		public uint SectorAllocationTableSize { get; set; }
		/// <summary>
		/// Sector ID of the first sector of the directory stream
		/// </summary>
		public uint DirectoryStreamFirstSectorID { get; set; }
		public uint TransactionSignatureNumber { get; set; }
		/// <summary>
		/// Minimum size of a standard stream (in bytes). Minimum allowed and most-used size is
		/// 4096 bytes. Streams with an actual size smaller than (and not equal to) this value
		/// are stored as short-streams.
		/// </summary>
		public uint MinimumStandardStreamSize { get; set; } // = 4096;
		/// <summary>
		/// Sector ID of the first sector of the short-sector allocation table (or
		/// <see cref="CompoundDocumentKnownSectorID.EndOfChain" /> if not extant).
		/// </summary>
		/// <value>The short sector allocation table first sector identifier.</value>
		public int ShortSectorAllocationTableFirstSectorID { get; set; }
		public int ShortSectorAllocationTableSize { get; set; }
		/// <summary>
		/// Sector ID of the first sector of the master sector allocation table (or
		/// <see cref="CompoundDocumentKnownSectorID.EndOfChain" /> if no additional sectors
		/// used).
		/// </summary>
		public int MasterSectorAllocationTableFirstSectorID { get; set; }
		/// <summary>
		/// Total number of sectors used for the master sector allocation table.
		/// </summary>
		public int MasterSectorAllocationTableSize { get; set; }
		public int[] MasterSectorAllocationTable { get; set; }
	}
}
