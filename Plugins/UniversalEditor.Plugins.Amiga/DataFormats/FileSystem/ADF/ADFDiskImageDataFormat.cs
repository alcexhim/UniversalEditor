//
//  ADFDiskImageDataFormat.cs
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
using MBS.Framework;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.Plugins.Amiga.DataFormats.FileSystem.ADF.Internal;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Amiga.DataFormats.FileSystem.ADF
{
	public class ADFDiskImageDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public int BytesPerSector { get; set; } = 512;
		[CustomOptionText("Volume _name")]
		public string VolumeName { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			// boot block
			string diskType = reader.ReadFixedLengthString(3);
			if (diskType == "DOS")
			{

			}
			else if (diskType == "PFS")
			{
				throw new NotImplementedException("Professional File System not implemented ... yet!");
			}
			else
			{
				throw new InvalidDataFormatException();
			}

			ADFDiskImageFlags flags = (ADFDiskImageFlags)reader.ReadByte();
			uint checksum = reader.ReadUInt32();
			uint rootblock = reader.ReadUInt32(); // value is 880 for DD and HD
			byte[] bootblock = reader.ReadBytes(1012); // The size for a floppy disk is 1012; for a harddisk it is (DosEnvVec->Bootblocks * BSIZE) - 12

			uint checksumVerify = CalculateChecksum(diskType, flags, rootblock, bootblock);
			if (checksumVerify != checksum)
			{
				(Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Warning, "boot block checksum mismatch", Accessor.GetFileName());
			}

			reader.Seek(CalculateSectorOffset(rootblock), SeekOrigin.Begin);

			uint[] hashTableEntries = null;
			#region Rootblock
			{
				Internal.ADFBlock block = ReadBlock(reader);
				VolumeName = block.filename;
				hashTableEntries = block.hashTableEntries;
				/*
		ulong	1	next_hash	unused (value = 0)
		ulong	1	parent_dir	unused (value = 0)
BSIZE-  8/-0x08	ulong	1	extension	FFS: first directory cache block,
						0 otherwise
BSIZE-  4/-0x04	ulong	1	sec_type	block secondary type = ST_ROOT 
						(value 1)
						*/
				LoadDirectory(reader, block, fsom);
			}
			#endregion
		}

		private void LoadDirectory(Reader reader, ADFBlock block, IFileSystemContainer fsom)
		{
			for (int i = 0; i < block.hashTableEntries.Length; i++)
			{
				if (block.hashTableEntries[i] == 0) continue;

				reader.Seek(CalculateSectorOffset(block.hashTableEntries[i]), SeekOrigin.Begin);

				Internal.ADFBlock block2 = ReadBlock(reader);
				if (block2.sec_type == ADFDiskImageBlockSecondaryType.File)
				{
					File f = fsom.AddFile(block2.filename);
					f.Properties["block"] = block2;
					f.Properties["reader"] = reader;
					f.DataRequest += F_DataRequest;
				}
				else if (block2.sec_type == ADFDiskImageBlockSecondaryType.Directory)
				{
					Folder folder = fsom.Folders.Add(block2.filename);
					LoadDirectory(reader, block2, folder);
				}
			}
		}

		private void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (File)sender;
			Internal.ADFBlock block = f.GetProperty<Internal.ADFBlock>("block");
			Reader reader = f.GetProperty<Reader>("reader");

			byte[] data = new byte[0];
			int len = 0;
			for (int i = 0; i < block.hashTableEntries.Length; i++)
			{
				if (block.hashTableEntries[i] == 0) continue;
				reader.Seek(CalculateSectorOffset(block.hashTableEntries[i]), SeekOrigin.Begin);
				byte[] blockData = reader.ReadBytes(BytesPerSector);
				Array.Resize<byte>(ref data, data.Length + blockData.Length);
				Array.Copy(blockData, 0, data, len, BytesPerSector);
				len += BytesPerSector;
			}
			e.Data = data;
		}


		private Internal.ADFBlock ReadBlock(Reader reader)
		{
			Internal.ADFBlock block = new Internal.ADFBlock();
			block.blockPrimaryType = reader.ReadUInt32(); // 2 = T_HEADER
			block.headerKey = reader.ReadUInt32();
			block.highSeq = reader.ReadUInt32(); // file: number of data block ptr stored here
			block.hashTableSize = reader.ReadUInt32();
			block.firstData = reader.ReadUInt32(); // unused (value 0)
			block.blockChecksum = reader.ReadUInt32();

			int hashTableEntryCount = (BytesPerSector / 4) - 56;
			block.hashTableEntries = reader.ReadUInt32Array(hashTableEntryCount); // 72 for floppy disk, (BSIZE/4) - 56 for hard disks

			block.bm_flag = reader.ReadUInt32(); // -1 means VALID
			block.bm_pages = reader.ReadUInt32Array(25); // bitmap blocks pointers (first one at bm_pages[0])
			block.bm_ext = reader.ReadUInt32(); // first bitmap extension block (hard disks only)

			// last root alteration date
			block.r_days = reader.ReadUInt32(); // days since 1 jan 78
			block.r_mins = reader.ReadUInt32(); // minutes past midnight
			block.r_ticks = reader.ReadUInt32(); // ticks (1/50 sec) past last minute
			byte name_len = reader.ReadByte(); // volume name length
			block.filename = reader.ReadFixedLengthString(30).TrimNull(); // volume name
			block.unused1 = reader.ReadByte(); // set to 0
			block.unused2 = reader.ReadUInt32(); // set to 0
			block.unused3 = reader.ReadUInt32(); // set to 0

			// last disk alteration date
			block.v_days = reader.ReadUInt32(); // days since 1 jan 78
			block.v_mins = reader.ReadUInt32();
			block.v_ticks = reader.ReadUInt32();

			// filesystem creation date
			block.c_days = reader.ReadUInt32();
			block.c_mins = reader.ReadUInt32();
			block.c_ticks = reader.ReadUInt32();

			block.next_hash = reader.ReadUInt32();
			block.parent_dir = reader.ReadUInt32();

			block.extension = reader.ReadUInt32();
			block.sec_type = (ADFDiskImageBlockSecondaryType)reader.ReadUInt32();
			return block;
		}

		private long CalculateSectorOffset(long sector)
		{
			return BytesPerSector * sector;
		}

		private uint CalculateChecksum(string diskType, ADFDiskImageFlags flags, uint rootblock, byte[] data)
		{
			MemoryAccessor ma = new MemoryAccessor();
			ma.Writer.Endianness = Endianness.BigEndian;
			ma.Writer.WriteFixedLengthString(diskType, 3);
			ma.Writer.WriteByte((byte)flags);
			ma.Writer.WriteUInt32(0);
			ma.Writer.WriteUInt32(rootblock);
			ma.Writer.WriteBytes(data);
			ma.Flush();
			ma.Close();
			return CalculateChecksum(ma.ToArray());
		}
		private uint CalculateChecksum(byte[] data)
		{
			return CalculateChecksum(new MemoryAccessor(data));
		}
		private uint CalculateChecksum(MemoryAccessor ma)
		{
			uint newsum = 0;
			ma.Reader.Endianness = Endianness.BigEndian;
			for (int i = 0; i < ma.Length / 4; i++)
			{
				uint d = ma.Reader.ReadUInt32();
				if ((UInt32.MaxValue - newsum) < d)   /* overflow */
					newsum++;
				newsum += d;
			}
			newsum = ~newsum;       /* not */
			return newsum;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();


		}
	}
}
