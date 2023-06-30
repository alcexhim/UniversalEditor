//
//  MBRDataFormat.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using UniversalEditor.ObjectModels.PartitionedFileSystem;

namespace UniversalEditor.DataFormats.PartitionedFileSystem.MBR
{
	public class MBRDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PartitionedFileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PartitionedFileSystemObjectModel disk = objectModel as PartitionedFileSystemObjectModel;
			if (disk == null)
				throw new ObjectModelNotSupportedException();

			disk.PartitionDataRequest += Disk_PartitionDataRequest;

			Reader r = Accessor.Reader;

			ushort newldrRecordSize = r.ReadUInt16();
			string newldr = r.ReadFixedLengthString(6);
			if (newldr.Equals("NEWLDR"))
			{
				// this is a NEWLDR MBR
			}
			else
			{
				// this is not a NEWLDR MBR
				r.Accessor.Seek(-8, SeekOrigin.Current);
			}

			byte[] bootstrapCodeAreaPart1 = r.ReadBytes(218);
			ushort diskTimestampUnknown = r.ReadUInt16();
			byte originalPhysicalDrive = r.ReadByte();
			byte diskTimestampSeconds = r.ReadByte();
			byte diskTimestampMinutes = r.ReadByte();
			byte diskTimestampHours = r.ReadByte();

			// wiki says also 222,but palimpsest writes 216
			byte[] bootstrapCodeAreaPart2 = r.ReadBytes(216);

			// Disk signature (optional; UEFI, Linux, Windows NT family and other OSes)
			uint diskSignature = r.ReadUInt32();
			ushort copyProtectionFlag = r.ReadUInt16();
			if (copyProtectionFlag == 0x5A5A)
			{
				// lol
			}

			MBRPartitionEntry partentry1 = ReadMBRPartitionEntry(r);
			if (partentry1.PartitionType != MBRPartitionType.None)
			{
				AddPartition(disk, partentry1);
			}
			MBRPartitionEntry partentry2 = ReadMBRPartitionEntry(r);
			if (partentry2.PartitionType != MBRPartitionType.None)
			{
				AddPartition(disk, partentry2);
			}
			MBRPartitionEntry partentry3 = ReadMBRPartitionEntry(r);
			if (partentry3.PartitionType != MBRPartitionType.None)
			{
				AddPartition(disk, partentry3);
			}
			MBRPartitionEntry partentry4 = ReadMBRPartitionEntry(r);
			if (partentry4.PartitionType != MBRPartitionType.None)
			{
				AddPartition(disk, partentry4);
			}

			ushort bootSignature = r.ReadUInt16();
			if (bootSignature != 0xAA55)
			{
				Console.Error.WriteLine("ue: mbr: warning: not found 0xAA55 at offset 0x01FE");
			}

		}

		void Disk_PartitionDataRequest(object sender, PartitionDataRequestEventArgs e)
		{
			long offset = e.Disk.CalculatePartitionOffset(e.Partition);
			long length = e.Disk.CalculatePartitionSize(e.Partition);

			Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] data = Accessor.Reader.ReadBytes(length);
			e.Data = data;
		}

		private void AddPartition(PartitionedFileSystemObjectModel disk, MBRPartitionEntry partentry1)
		{
			Partition part = new Partition();
			part.FirstAbsoluteSectorLBA = partentry1.FirstAbsoluteSectorLBA;
			part.FirstAbsoluteSectorAddress = partentry1.FirstAbsoluteSectorAddress;
			part.LastAbsoluteSectorAddress = partentry1.LastAbsoluteSectorAddress;
			part.SectorCount = partentry1.SectorCount;
			part.PartitionType = MBRPartitionTypeToPartitionType(partentry1.PartitionType);

			if ((partentry1.Status & MBRPartitionEntryStatus.Active) == MBRPartitionEntryStatus.Active)
			{
				part.IsBootable = true;
			}
			disk.Partitions.Add(part);
		}

		private PartitionType MBRPartitionTypeToPartitionType(MBRPartitionType partitionType)
		{
			switch (partitionType)
			{
				case MBRPartitionType.FAT12: return PartitionType.FAT12;
				case MBRPartitionType.FAT32LBA: return PartitionType.FAT32LBA;
				case MBRPartitionType.IFS_HPFS_NTFS_exFAT_QNX:  return PartitionType.IFS_HPFS_NTFS_exFAT_QNX;
				case MBRPartitionType.None: return PartitionType.None;
				case MBRPartitionType.XenixRoot: return PartitionType.XenixRoot;
				case MBRPartitionType.XenixUsr: return PartitionType.XenixUsr;
			}
			throw new ArgumentOutOfRangeException();
		}

		private MBRPartitionEntry ReadMBRPartitionEntry(Reader r)
		{
			MBRPartitionEntry entry = new MBRPartitionEntry();
			entry.Status = (MBRPartitionEntryStatus)r.ReadByte();

			entry.FirstAbsoluteSectorAddress = ReadCHSPartitionAddress(r);
			entry.PartitionType = (MBRPartitionType)r.ReadByte();
			entry.LastAbsoluteSectorAddress = ReadCHSPartitionAddress(r);
			entry.FirstAbsoluteSectorLBA = r.ReadUInt32();
			entry.SectorCount = r.ReadUInt32();
			return entry;
		}

		private CHSPartitionAddress ReadCHSPartitionAddress(Reader r)
		{
			byte head = r.ReadByte();
			byte sectorAndCylinder1 = r.ReadByte();
			byte cylinder2 = r.ReadByte();
			return new CHSPartitionAddress(0, head, 0);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PartitionedFileSystemObjectModel part = objectModel as PartitionedFileSystemObjectModel;
			if (part == null)
				throw new ObjectModelNotSupportedException();


		}
	}
}
