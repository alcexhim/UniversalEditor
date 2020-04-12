//
//  GCFDataFormat.cs - implements the Valve GCF archive format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Valve.GCF
{
	/// <summary>
	/// Implements the Valve GCF archive format.
	/// </summary>
	public class GCFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionVersion(nameof(FormatVersion), "Format &version: "));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(CacheID), "&Cache ID: "));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(LastVersionPlayed), "&Last version played: "));
			}
			return _dfr;
		}

		private Version mvarFormatVersion = new Version(1, 0);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private uint mvarCacheID = 0;
		public uint CacheID { get { return mvarCacheID; } set { mvarCacheID = value; } }

		private uint mvarLastVersionPlayed = 0;
		public uint LastVersionPlayed { get { return mvarLastVersionPlayed; } set { mvarLastVersionPlayed = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			#region GCF Header
			uint uiDummyA0 = reader.ReadUInt32();
			if (uiDummyA0 != 0x00000001) throw new InvalidDataFormatException("File does not begin with 0x00000001");

			uint uiMajorVersion = reader.ReadUInt32();
			uint uiMinorVersion = reader.ReadUInt32();
			mvarFormatVersion = new Version((int)uiMajorVersion, (int)uiMinorVersion);

			mvarCacheID = reader.ReadUInt32();
			if (uiMinorVersion < 5)
			{
				// In version 3 the GCFDataBlockHeader is missing the
				// uiLastVersionPlayed field.
				mvarLastVersionPlayed = reader.ReadUInt32();
			}

			uint uiDummyA1 = reader.ReadUInt32();
			uint uiDummyA2 = reader.ReadUInt32();

			// Total size of GCF file in bytes.
			uint uiFileSize = reader.ReadUInt32();

			// Size of each data block in bytes.
			uint uiBlockSize = reader.ReadUInt32();

			// Number of total data blocks.
			uint uiTotalBlockCount = reader.ReadUInt32();

			uint uiDummyA3 = reader.ReadUInt32();
			#endregion
			#region GCF Block Entries
			{
				#region GCF Block Entry Header
				// Number of data blocks.
				uint uiBlockCount = reader.ReadUInt32();
				// Number of data blocks that point to data.
				uint uiBlocksUsed = reader.ReadUInt32();

				uint uiDummyB0 = reader.ReadUInt32();
				uint uiDummyB1 = reader.ReadUInt32();
				uint uiDummyB2 = reader.ReadUInt32();
				uint uiDummyB3 = reader.ReadUInt32();
				uint uiDummyB4 = reader.ReadUInt32();

				// Header checksum.
				uint uiChecksum = reader.ReadUInt32();
				#endregion

				for (uint i = 0; i < uiBlockCount; i++)
				{
					// Flags for the block entry.  0x200F0000 == Not used.
					GCFBlockEntryFlags uiEntryFlags = (GCFBlockEntryFlags)reader.ReadUInt32();

					// The offset for the data contained in this block entry in the
					// file.
					uint uiFileDataOffset = reader.ReadUInt32();

					// The length of the data in this block entry.
					uint uiFileDataSize = reader.ReadUInt32();
					// The index to the first data block of this block entry's data.
					uint uiFirstDataBlockIndex = reader.ReadUInt32();
					// The next block entry in the series.  (N/A if == BlockCount.)
					uint uiNextBlockEntryIndex = reader.ReadUInt32();
					// The previous block entry in the series.
					// (N/A if == BlockCount.)
					uint uiPreviousBlockEntryIndex = reader.ReadUInt32();
					// The index of the block entry in the directory.
					uint uiDirectoryIndex = reader.ReadUInt32();
				}
			}
			#endregion
			#region GCF Fragmentation Map
			{
				#region Header
				// Number of data blocks.
				uint uiBlockCount = reader.ReadUInt32();
				// The index of the first unused fragmentation map entry.
				uint uiFirstUnusedEntry = reader.ReadUInt32();
				// The block entry terminator; 0 = 0x0000ffff or 1 = 0xffffffff.
				uint uiTerminator = reader.ReadUInt32();
				// Header checksum.
				uint uiChecksum = reader.ReadUInt32();
				#endregion
				for (uint i = 0; i < uiBlockCount; i++)
				{
					// The index of the next data block.
					uint uiNextDataBlockIndex = reader.ReadUInt32();
				}
			}
			#endregion

			if (uiMinorVersion == 5)
			{
				// The below section is part of version 5 but not version 6.
				#region Block Entry Map
				{
					#region Header
					// Number of data blocks.
					uint uiBlockCount = reader.ReadUInt32();
					// Index of the first block entry.
					uint uiFirstBlockEntryIndex = reader.ReadUInt32();
					// Index of the last block entry.
					uint uiLastBlockEntryIndex = reader.ReadUInt32();
					uint uiDummy0 = reader.ReadUInt32();
					// Header checksum.
					uint uiChecksum = reader.ReadUInt32();
					#endregion
					for (uint i = 0; i < uiBlockCount; i++)
					{
						// The previous block entry.  (N/A if == BlockCount.)
						uint uiPreviousBlockEntryIndex = reader.ReadUInt32();
						// The next block entry.  (N/A if == BlockCount.)
						uint uiNextBlockEntryIndex = reader.ReadUInt32();
					}
				}
				#endregion
			}

			#region Directory Header
			{
				// Always 0x00000004
				uint uiDummy0 = reader.ReadUInt32();
				// Cache ID.
				uint uiCacheID = reader.ReadUInt32();
				// GCF file version.
				uint uiLastVersionPlayed = reader.ReadUInt32();
				// Number of items in the directory.
				uint uiItemCount = reader.ReadUInt32();
				// Number of files in the directory.
				uint uiFileCount = reader.ReadUInt32();
				// Always 0x00008000.  Data per checksum?
				uint uiDummy1 = reader.ReadUInt32();
				
				// Size of lpGCFDirectoryEntries & lpGCFDirectoryNames &
				// lpGCFDirectoryInfo1Entries & lpGCFDirectoryInfo2Entries &
				// lpGCFDirectoryCopyEntries & lpGCFDirectoryLocalEntries in
				// bytes.
				uint uiDirectorySize = reader.ReadUInt32();
				// Size of the directory names in bytes.
				uint uiNameSize = reader.ReadUInt32();
				// Number of Info1 entries.
				uint uiInfo1Count = reader.ReadUInt32();
				// Number of files to copy.
				uint uiCopyCount = reader.ReadUInt32();
				// Number of files to keep local.
				uint uiLocalCount = reader.ReadUInt32();
				uint uiDummy2 = reader.ReadUInt32();
				uint uiDummy3 = reader.ReadUInt32();
				// Header checksum.
				uint uiChecksum = reader.ReadUInt32();

				for (uint i = 0; i < uiItemCount; i++)
				{
					// Offset to the directory item name from the end of the
					// directory items.
					uint uiNameOffset = reader.ReadUInt32();
					// Size of the item.  (If file, file size.  If folder, num
					// items.)
					uint uiItemSize = reader.ReadUInt32();
					// Checksome index. (0xFFFFFFFF == None).
					uint uiChecksumIndex = reader.ReadUInt32();
					// Flags for the directory item.  (0x00000000 == Folder).
					GCFDirectoryEntryFlags uiDirectoryFlags = (GCFDirectoryEntryFlags)reader.ReadUInt32();
					// Index of the parent directory item.  (0xFFFFFFFF ==
					// None).
					uint uiParentIndex = reader.ReadUInt32();
					// Index of the next directory item.  (0x00000000 == None).
					uint uiNextIndex = reader.ReadUInt32();
					// Index of the first directory item.  (0x00000000 == None).
					uint uiFirstIndex = reader.ReadUInt32();
				}
			}
			#endregion
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
