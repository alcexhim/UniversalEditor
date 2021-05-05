//
//  HFSDataFormat.cs - provides a DataFormat for manipulating file systems in HFS/HFS+ format
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

using UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal;
using UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal.CatalogRecords;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating file systems in HFS/HFS+ format.
	/// </summary>
	public class HFSDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(VolumeName), "Volume _name"));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(VolumeBackupSequenceNumber), "_Backup sequence number", 0, Int16.MinValue, Int16.MaxValue));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(VolumeWriteCount), "Volume _write count"));
				_dfr.Sources.Add("https://developer.apple.com/legacy/library/documentation/mac/Files/Files-102.html");
				_dfr.Sources.Add("http://www.cs.fsu.edu/~baker/devices/lxr/http/source/linux/fs/hfs/hfs.h");
				_dfr.Sources.Add("www.fenestrated.net/~macman/mirrors/Apple Technotes (As of 2002)/tn/tn1150.html");
			}
			return _dfr;
		}

		public string VolumeName { get; set; } = String.Empty;
		public short VolumeBackupSequenceNumber { get; set; } = 0;
		public int VolumeWriteCount { get; set; } = 0;

		private const int LOGICAL_BLOCK_SIZE = 512;
		public void SeekToLogicalBlock(long index)
		{
			long offset = (LOGICAL_BLOCK_SIZE * index);
			base.Accessor.Seek(offset, IO.SeekOrigin.Begin);
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			reader.Seek(0, SeekOrigin.Begin); // debug only, remove when finished

			// let's not forget that Mac OS legacy filesystems are big-endian
			reader.Endianness = Endianness.BigEndian;

			// Logical blocks 0 and 1 of the volume are the Boot blocks which contain system
			// startup information. For example, the names of the System and Shell (usually
			// the Finder) files which are loaded at startup.

			HFSMasterDirectoryBlock _mdb = new HFSMasterDirectoryBlock();
			#region Master Directory Block
			{
				// Logical block 2 contains the Master Directory Block (MDB). This defines a wide
				// variety of data about the volume itself, for example date & time stamps for when the
				// volume was created, the location of the other volume structures such as the Volume
				// Bitmap or the size of logical structures such as allocation blocks. There is also a
				// duplicate of the MDB called the Alternate Master Directory Block (aka Alternate MDB)
				// located at the opposite end of the volume in the second to last logical block. This
				// is intended mainly for use by disk utilities and is only updated when either the
				// Catalog File or Extents Overflow File grow in size.
				SeekToLogicalBlock(2);

				byte[] signature = reader.ReadBytes(2);
				if (signature.Match(new byte[] { 0x42, 0x44 }))
				{
					// HFS volume
				}
				else if (signature.Match(new byte[] { 0xD2, 0xD7 }))
				{
					// obsolete flat MFS volume
					throw new InvalidDataFormatException("MFS volume not supported yet");
				}
				else
				{
					throw new InvalidDataFormatException("File does not contain 'BD' at logical block 2");
				}

				_mdb.creationTimestamp = reader.ReadInt32();
				_mdb.modificationTimestamp = reader.ReadInt32();
				_mdb.volumeAttributes = (HFSVolumeAttributes)reader.ReadInt16();
				_mdb.rootDirectoryFileCount = reader.ReadUInt16();
				_mdb.volumeBitmapFirstBlockIndex = reader.ReadUInt16();
				_mdb.nextAllocationSearchStart = reader.ReadInt16();
				_mdb.allocationBlockCount = reader.ReadUInt16();
				_mdb.allocationBlockSize = reader.ReadInt32();

				// allocation block sanity check - must be a multiple of 512 bytes
				if ((_mdb.allocationBlockSize % 512) != 0) throw new InvalidDataFormatException("Allocation block size is not a multiple of 512");

				_mdb.defaultClumpSize = reader.ReadInt32();
				_mdb.firstAllocationBlockIndex = reader.ReadInt16();
				_mdb.nextUnusedCatalogNodeID = reader.ReadInt32();
				_mdb.unusedAllocationBlockCount = reader.ReadUInt16();

				VolumeName = ReadHFSName(reader, 27);

				_mdb.lastBackupTimestamp = reader.ReadInt32();
				VolumeBackupSequenceNumber = reader.ReadInt16();
				VolumeWriteCount = reader.ReadInt32();
				_mdb.clumpSizeForExtentsOverflowFile = reader.ReadInt32();
				_mdb.clumpSizeForCatalogFile = reader.ReadInt32();
				_mdb.rootDirectoryDirectoryCount = reader.ReadUInt16();
				_mdb.totalFileCount = reader.ReadInt32();
				_mdb.totalDirectoryCount = reader.ReadInt32();
				_mdb.finderInfo = reader.ReadInt32Array(8); // information used by the Finder
				_mdb.volumeCacheBlockCount = reader.ReadUInt16();
				_mdb.volumeBitmapCacheBlockCount = reader.ReadUInt16();
				_mdb.commonVolumeCacheBlockCount = reader.ReadUInt16();

				// ExtDataRec: ARRAY[3] of ExtDescriptor
				int extentsOverflowFileSize = reader.ReadInt32();
				_mdb.extentRecordsForExtentsOverflowFile = ReadHFSExtentDescriptorArray(reader, 3);

				int catalogFileSize = reader.ReadInt32();
				_mdb.extentRecordsForCatalogFile = ReadHFSExtentDescriptorArray(reader, 3);

				int irgs = _mdb.extentRecordsForCatalogFile[0].firstAllocationBlockIndex;
				int crgs = _mdb.extentRecordsForCatalogFile[0].allocationBlockCount;

				reader.Seek(13815, SeekOrigin.Begin);
				HFSCatalogRecord hfscr = ReadHFSCatalogRecord(reader);
			}
			#endregion

			#region Volume bitmap
			{
				SeekToLogicalBlock(_mdb.volumeBitmapFirstBlockIndex);

			}
			#endregion
			#region
			#endregion

		}

		private string ReadHFSName(Reader reader, int length = 31)
		{
			byte nameLength = reader.ReadByte();
			string name = reader.ReadFixedLengthString(length);
			if (nameLength < name.Length)
			{
				// clip off the rest of the unused characters for the volume name
				name = name.Substring(0, nameLength);
			}
			return name;
		}

		private HFSCatalogRecord ReadHFSCatalogRecord(Reader reader)
		{
			HFSCatalogRecordType type = (HFSCatalogRecordType)reader.ReadInt16();

			switch (type)
			{
				case HFSCatalogRecordType.Directory:
				case HFSCatalogRecordType.ExtendedDirectory:
				{
					HFSCatalogDirectoryRecord rec = new HFSCatalogDirectoryRecord();
					rec.type = type;

					rec.flags = (HFSDirectoryFlags)reader.ReadInt16();
					if (type == HFSCatalogRecordType.ExtendedDirectory)
					{
						rec.directoryValence = reader.ReadInt32();
					}
					else
					{
						rec.directoryValence = reader.ReadInt16();
					}
					rec.directoryID = reader.ReadInt32();
					rec.creationTimestamp = reader.ReadInt32();
					if (type == HFSCatalogRecordType.ExtendedDirectory)
					{
						rec.modificationTimestamp = reader.ReadInt32();
						rec.attributeModificationTimestamp = reader.ReadInt32();
						rec.lastAccessTimestamp = reader.ReadInt32();
					}
					else
					{
						rec.modificationTimestamp = reader.ReadInt32();
					}
					rec.lastBackupTimestamp = reader.ReadInt32();
					if (type == HFSCatalogRecordType.ExtendedDirectory)
					{
						rec.permissions = ReadHFSPlusPermissions(reader);
					}
					rec.finderUserInformation = ReadHFSDInfo(reader);
					rec.finderAdditionalInformation = ReadHFSDXInfo(reader);
					if (type == HFSCatalogRecordType.ExtendedDirectory)
					{
						rec.textEncoding = (HFSPlusTextEncoding)reader.ReadUInt32();
						uint reserved = reader.ReadUInt32();
					}
					else
					{
						int[] reserved = reader.ReadInt32Array(4);
					}
					return rec;
				}
				case HFSCatalogRecordType.ExtendedFile:
				case HFSCatalogRecordType.File:
				{
					HFSCatalogFileRecord rec = new HFSCatalogFileRecord();
					rec.type = type;
					if (type == HFSCatalogRecordType.ExtendedFile)
					{
						rec.flags = (HFSFileFlags)reader.ReadUInt16();
						uint reserved1 = reader.ReadUInt32();
						rec.fileID = reader.ReadInt32();
						rec.creationTimestamp = reader.ReadInt32();
						rec.modificationTimestamp = reader.ReadInt32();
						rec.attributeModificationTimestamp = reader.ReadInt32();
						rec.lastAccessTimestamp = reader.ReadInt32();
						rec.lastBackupTimestamp = reader.ReadInt32();
						rec.permissions = ReadHFSPlusPermissions(reader);

						rec.finderUserInformation = ReadHFSFInfo(reader);
						rec.finderAdditionalInformation = ReadHFSFXInfo(reader);

						rec.textEncoding = (HFSPlusTextEncoding)reader.ReadUInt32();
						uint reserved2 = reader.ReadUInt32();

						rec.dataFork = ReadHFSPlusForkData(reader);
						rec.resourceFork = ReadHFSPlusForkData(reader);
					}
					else
					{
						rec.flags = (HFSFileFlags)reader.ReadSByte();
						rec.fileType = reader.ReadSByte();
						rec.finderUserInformation = ReadHFSFInfo(reader);
						rec.fileID = reader.ReadInt32();
						rec.dataForkFirstAllocationBlock = reader.ReadInt16();
						rec.dataForkLogicalEOF = reader.ReadInt32();
						rec.dataForkPhysicalEOF = reader.ReadInt32();
						rec.resourceForkFirstAllocationBlock = reader.ReadInt16();
						rec.resourceForkLogicalEOF = reader.ReadInt32();
						rec.resourceForkPhysicalEOF = reader.ReadInt32();
						rec.creationTimestamp = reader.ReadInt32();
						rec.modificationTimestamp = reader.ReadInt32();
						rec.lastBackupTimestamp = reader.ReadInt32();
						rec.finderAdditionalInformation = ReadHFSFXInfo(reader);
						rec.fileClumpSize = reader.ReadInt16();
						rec.firstDataForkExtentRecord = ReadHFSExtentDescriptorArray(reader, 3);
						rec.firstResourceForkExtentRecord = ReadHFSExtentDescriptorArray(reader, 3);
						rec.reserved2 = reader.ReadInt32();
					}
					return rec;
				}
				case HFSCatalogRecordType.DirectoryThread:
				{
					HFSCatalogDirectoryThreadRecord rec = new HFSCatalogDirectoryThreadRecord();
					rec.type = type;
					rec.reserved2 = reader.ReadInt32Array(2);
					rec.parentID = reader.ReadInt32();

					rec.fileName = ReadHFSName(reader);
					return rec;
				}
				case HFSCatalogRecordType.FileThread:
				{
					HFSCatalogFileThreadRecord rec = new HFSCatalogFileThreadRecord();
					rec.type = type;
					rec.reserved2 = reader.ReadInt32Array(2);
					rec.parentID = reader.ReadInt32();

					byte fileNameLength = reader.ReadByte();
					rec.fileName = reader.ReadFixedLengthString(fileNameLength);
					return rec;
				}
			}
			return null;
		}

		private HFSPlusForkData ReadHFSPlusForkData(Reader reader)
		{
			HFSPlusForkData item = new HFSPlusForkData();
			item.logicalSize = reader.ReadUInt64();
			item.clumpSize = reader.ReadUInt32();
			item.totalBlocks = reader.ReadUInt32();
			item.extents = ReadHFSPlusExtentRecord(reader);
			return item;
		}

		private HFSPlusExtentDescriptor[] ReadHFSPlusExtentRecord(Reader reader)
		{
			HFSPlusExtentDescriptor[] item = new HFSPlusExtentDescriptor[8];
			for (int i = 0; i < 8; i++)
			{
				item[i] = ReadHFSPlusExtentDescriptor(reader);
			}
			return item;
		}

		private HFSPlusExtentDescriptor ReadHFSPlusExtentDescriptor(Reader reader)
		{
			HFSPlusExtentDescriptor item = new HFSPlusExtentDescriptor();
			item.startBlock = reader.ReadUInt32();
			item.blockCount = reader.ReadUInt32();
			return item;
		}

		private HFSPlusPermissions ReadHFSPlusPermissions(Reader reader)
		{
			HFSPlusPermissions item = new HFSPlusPermissions();
			item.ownerID = reader.ReadUInt32();
			item.groupID = reader.ReadUInt32();
			item.permissions = reader.ReadUInt32();
			item.specialDevice = reader.ReadUInt32();
			return item;
		}

		private HFSDInfo ReadHFSDInfo(Reader reader)
		{
			HFSDInfo item = new HFSDInfo();
			item.rect = ReadHFSRect(reader);
			item.flags = (HFSDInfoFlags)reader.ReadInt16();
			item.location = ReadHFSPoint(reader);
			item.view = reader.ReadInt16();
			return item;
		}
		private HFSDXInfo ReadHFSDXInfo(Reader reader)
		{
			HFSDXInfo item = new HFSDXInfo();
			item.scroll = ReadHFSPoint(reader);
			item.openChain = reader.ReadInt32();
			item.reserved = reader.ReadInt16();
			item.comment = reader.ReadInt16();
			item.putAway = reader.ReadInt32();
			return item;
		}

		private HFSFInfo ReadHFSFInfo(Reader reader)
		{
			HFSFInfo item = new HFSFInfo();
			item.type = reader.ReadFixedLengthString(4);
			item.creator = reader.ReadFixedLengthString(4);
			item.fdFlags = (HFSFInfoFlags)reader.ReadInt16();
			item.location = ReadHFSPoint(reader);
			item.parentFolderID = reader.ReadInt16();
			return item;
		}
		private HFSFXInfo ReadHFSFXInfo(Reader reader)
		{
			HFSFXInfo item = new HFSFXInfo();
			item.iconID = reader.ReadInt16();
			item.reserved = reader.ReadBytes(8);
			item.comment = reader.ReadInt16();
			item.putAway = reader.ReadInt32();
			return item;
		}

		private HFSRect ReadHFSRect(Reader reader)
		{
			HFSRect item = new HFSRect();
			item.top = reader.ReadInt16();
			item.bottom = reader.ReadInt16();
			item.left = reader.ReadInt16();
			item.right = reader.ReadInt16();
			return item;
		}
		private HFSPoint ReadHFSPoint(Reader reader)
		{
			HFSPoint item = new HFSPoint();
			item.x = reader.ReadInt16();
			item.y = reader.ReadInt16();
			return item;
		}

		private HFSExtentDescriptor[] ReadHFSExtentDescriptorArray(Reader reader, int count)
		{
			HFSExtentDescriptor[] items = new HFSExtentDescriptor[count];
			for (int i = 0; i < count; i++)
			{
				items[i] = ReadHFSExtentDescriptor(reader);
			}
			return items;
		}
		private HFSExtentDescriptor ReadHFSExtentDescriptor(Reader reader)
		{
			HFSExtentDescriptor item = new HFSExtentDescriptor();
			item.firstAllocationBlockIndex = reader.ReadInt16();
			item.allocationBlockCount = reader.ReadInt16();
			return item;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
