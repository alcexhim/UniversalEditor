//
//  CompoundDocumentDataFormat.cs - provides a DataFormat for manipulating Microsoft Compound Document Format files
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
using System.Collections.Generic;
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Compound Document Format files. This class may be inherited to implement
	/// <see cref="DataFormat" />s that manipulate files based on the Microsoft Compound Document Format specification, such as those from older
	/// versions of Microsoft Office.
	/// </summary>
	public class CompoundDocumentBaseDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(SectorSize), "_Sector size (in bytes)", 512, 128));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(ShortSectorSize), "S_hort sector size (in bytes)", 64));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(MinimumStandardStreamSize), "_Minimum standard stream size (in bytes)", 4096, 4096));
				_dfr.Sources.Add("http://www.openoffice.org/sc/compdocfileformat.pdf");
			}
			return _dfr;
		}

		public Guid UniqueIdentifier { get; set; } = Guid.Empty;
		public Version FormatVersion { get; set; } = new Version(3, 62);
		public Endianness Endianness { get; set; } = Endianness.LittleEndian;
		public uint SectorSize { get; set; } = 512;
		public uint ShortSectorSize { get; set; } = 64;
		public uint MinimumStandardStreamSize { get; set; } = 4096;

		private int mvarShortSectorFirstSectorID = 0;

		private int GetSectorPositionFromSectorID(int sectorID)
		{
			if (sectorID < 0) return 0;
			return (int)(512 + (sectorID * SectorSize));
		}
		private int GetShortSectorPositionFromSectorID(int sectorID)
		{
			if (sectorID < 0) return 0;
			return (int)(sectorID * ShortSectorSize);
		}

		private byte[] mvarShortSectorContainerStreamData = null;

		private static readonly byte[] VALID_SIGNATURE = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

		public string LogPath { get; set; } = null;

		private int[] shortSectorAllocationTable = new int[0];
		private List<int> shortSectorAllocationTableSectors = new List<int>();
		private int[] sectorAllocationTable = new int[0];

		private Folder rootEntry = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			CompoundDocumentHeader header = ReadCompoundDocumentHeader(reader);
			if (!header.Signature.Match(VALID_SIGNATURE))
			{
				throw new InvalidDataFormatException("File does not begin with { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }");
			}
			UniqueIdentifier = header.UniqueIdentifier;

			FormatVersion = new Version(header.MajorVersion, header.MinorVersion);
			if (FormatVersion.Major == 0x0003 || FormatVersion.Major == 0x0004)
			{
				if (FormatVersion.Minor != 0x003E)
				{
					// sanity check
				}
			}

			if (header.ByteOrderIdentifier[0] == 0xFE && header.ByteOrderIdentifier[1] == 0xFF)
			{
				Endianness = Endianness.LittleEndian;
			}
			else if (header.ByteOrderIdentifier[0] == 0xFF && header.ByteOrderIdentifier[1] == 0xFE)
			{
				Endianness = Endianness.BigEndian;
			}
			else
			{
				throw new InvalidDataFormatException("Invalid value for byte order (" + header.ByteOrderIdentifier[0].ToString("X").PadLeft(2, '0') + ", " + header.ByteOrderIdentifier[1].ToString("X").PadLeft(2, '0') + ")");
			}

			SectorSize = (uint)(Math.Pow(2, header.SectorSize));

			// If Major Version is 3, the Sector Shift MUST be 0x0009, specifying a sector size of 512 bytes.
			// If Major Version is 4, the Sector Shift MUST be 0x000C, specifying a sector size of 4096 bytes.

			ShortSectorSize = (uint)(Math.Pow(2, header.ShortSectorSize));

			// This field MUST be set to 0x0006. This field specifies the sector size of
			// the Mini Stream as a power of 2.The sector size of the Mini Stream MUST be 64 bytes.

			if (ShortSectorSize > SectorSize) throw new InvalidDataFormatException("Short sector size (" + ShortSectorSize.ToString() + ") exceeds sector size (" + SectorSize.ToString() + ")");

			if (FormatVersion.Major == 3)
			{
				if (header.DirectorySectorCount != 0)
				{
					// If Major Version is 3, the Number of Directory Sectors MUST be zero. This field is not
					// supported for version 3 compound files.
				}
			}

			MinimumStandardStreamSize = header.MinimumStandardStreamSize;

			#region Read Master Sector Allocation Table
			// First part of the master sector allocation table, containing 109 SecIDs

			// TODO: test this! when MSAT contains more than 109 SecIDs
			int countForMSAT = (int)((double)SectorSize / 4);
			int nextSectorForMSAT = header.MasterSectorAllocationTableFirstSectorID;
			int nextPositionForMSAT = header.MasterSectorAllocationTable.Length;

			int[] masterSectorAllocationTable = header.MasterSectorAllocationTable;

			while (nextSectorForMSAT != (int)CompoundDocumentKnownSectorID.EndOfChain)
			{
				int[] masterSectorAllocationTablePart = reader.ReadInt32Array(countForMSAT);
				Array.Resize(ref masterSectorAllocationTable, masterSectorAllocationTable.Length + countForMSAT);
				Array.Copy(masterSectorAllocationTablePart, 0, masterSectorAllocationTable, nextPositionForMSAT, masterSectorAllocationTablePart.Length);

				nextSectorForMSAT = masterSectorAllocationTablePart[masterSectorAllocationTablePart.Length - 1];
			}
			#endregion
			#region Read Sector Allocation Table
			sectorAllocationTable = new int[(int)(SectorSize / 4)];
			for (int i = 0; i < masterSectorAllocationTable.Length; i++)
			{
				if (masterSectorAllocationTable[i] == -1)
					break;

				// The last SecID in each sector of the MSAT refers to the next sector used by the MSAT. If no more sectors follow, the
				// last SecID is the special End Of Chain SecID with the value –2( ➜ 3.1).

				SeekToSector(masterSectorAllocationTable[i]);
				int[] sectorAllocationTablePart = reader.ReadInt32Array((int)(SectorSize / 4));

				Array.Resize<int>(ref sectorAllocationTable, sectorAllocationTable.Length + sectorAllocationTablePart.Length);
				Array.Copy(sectorAllocationTablePart, 0, sectorAllocationTable, (int)(i * (SectorSize / 4)), sectorAllocationTablePart.Length);
			}
			#endregion

			// read directory entries - each entry is 128 bytes
			byte[] directoryData = ReadDirectoryData(reader, header);

			#region Read Short Sector Allocation Table
			shortSectorAllocationTableSectors = new List<int>();
			if (header.ShortSectorAllocationTableFirstSectorID >= 0)
			{
				int sector = header.ShortSectorAllocationTableFirstSectorID;
				while (sector != -2)
				{
					shortSectorAllocationTableSectors.Add(sector);
					if (sector < sectorAllocationTable.Length - 1)
					{
						sector = sectorAllocationTable[sector];
					}
					else
					{
						throw new IndexOutOfRangeException(String.Format("short sector {0} is out of bounds for sector allocation table (length: {1})", sector, sectorAllocationTable.Length));
					}
				}
			}

			shortSectorAllocationTable = new int[(SectorSize / 4) * shortSectorAllocationTableSectors.Count];
			byte[] shortSectorAllocationTableData = new byte[SectorSize * shortSectorAllocationTableSectors.Count];
			for (int i = 0; i < shortSectorAllocationTableSectors.Count; i++)
			{
				SeekToSector(shortSectorAllocationTableSectors[i]);

				int[] tablePart = reader.ReadInt32Array((int)(SectorSize / 4));
				Array.Copy(tablePart, 0, shortSectorAllocationTable, (i * tablePart.Length), tablePart.Length);
			}
			#endregion
			#region Read Sector Directory Entries
			Accessors.MemoryAccessor maDirectory = new Accessors.MemoryAccessor(directoryData);
			Reader directoryReader = new Reader(maDirectory);

			List<CompoundDocumentStorageHeader> listHeaders = new List<CompoundDocumentStorageHeader>();
			while (!directoryReader.EndOfStream)
			{
				CompoundDocumentStorageHeader sh = ReadStorageHeader(directoryReader);

				if (sh.StorageType == CompoundDocumentStorageType.RootStorage)
				{
					// this is the root storage entry
					mvarShortSectorFirstSectorID = sh.FirstSectorIndex;

					#region Read Short Stream Container Stream
					List<int> shortStreamContainerStreamSectors = new List<int>();
					{
						int shortSectorDataSector = mvarShortSectorFirstSectorID;
						while (shortSectorDataSector != -2)
						{
							shortStreamContainerStreamSectors.Add(shortSectorDataSector);
							if (shortSectorDataSector < sectorAllocationTable.Length)
							{
								shortSectorDataSector = sectorAllocationTable[shortSectorDataSector];
							}
							else
							{
								throw new IndexOutOfRangeException(String.Format("short sector data sector {0} is out of bounds for sector allocation table {1}", shortSectorDataSector, sectorAllocationTable.Length));
							}
						}
					}
					byte[] shortStreamContainerStreamData = new byte[shortStreamContainerStreamSectors.Count * SectorSize];
					int i = 0;
					foreach (int sector in shortStreamContainerStreamSectors)
					{
						int wpos = GetSectorPositionFromSectorID(sector);
						reader.Seek(wpos, SeekOrigin.Begin);
						byte[] sectorData = reader.ReadBytes(SectorSize);
						Array.Copy(sectorData, 0, shortStreamContainerStreamData, i, sectorData.Length);
						i += sectorData.Length;
					}
					mvarShortSectorContainerStreamData = shortStreamContainerStreamData;
					#endregion

					rootEntry = new Folder();
					rootEntry.Name = sh.Name; // MUST be "Root Entry", according to spec, but we want to be versatile
					fsom.Folders.Add(rootEntry);
				}
				else if (sh.StorageType == CompoundDocumentStorageType.UserStorage)
				{
				}
				else if (sh.StorageType == CompoundDocumentStorageType.UserStream)
				{
					File file = new File();
					file.Name = sh.Name;
					file.Size = sh.Length;
					file.Properties.Add("reader", reader);
					file.Properties.Add("firstSector", sh.FirstSectorIndex);
					file.Properties.Add("length", sh.Length);
					file.DataRequest += file_DataRequest;
					rootEntry.Files.Add(file);

					Console.WriteLine("{3}    {0}        {1}    {2}", file.Name, sh.FirstSectorIndex, sh.Length, sh.StorageType.ToString());
				}
				else if (sh.StorageType == CompoundDocumentStorageType.Empty)
				{
					Console.WriteLine(String.Format("{0} is empty; should we included it in file list?", sh.Name));
				}
				else
				{
					throw new NotImplementedException(String.Format("storage type {0} not implemented", sh.StorageType));
				}
			}
			#endregion

			WriteLog(fsom);
		}

		private CompoundDocumentHeader ReadCompoundDocumentHeader(Reader reader)
		{
			CompoundDocumentHeader header = new CompoundDocumentHeader();

			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			header.Signature = reader.ReadBytes(8);
			header.UniqueIdentifier = reader.ReadGuid();
			header.MinorVersion = reader.ReadUInt16();
			header.MajorVersion = reader.ReadUInt16();
			header.ByteOrderIdentifier = reader.ReadBytes(2);
			header.SectorSize = reader.ReadUInt16();
			header.ShortSectorSize = reader.ReadUInt16();
			header.Unused1 = reader.ReadBytes(6);
			header.DirectorySectorCount = reader.ReadUInt32();

			header.SectorAllocationTableSize = reader.ReadUInt32();
			header.DirectoryStreamFirstSectorID = reader.ReadUInt32();
			header.TransactionSignatureNumber = reader.ReadUInt32();
			header.MinimumStandardStreamSize = reader.ReadUInt32();

			header.ShortSectorAllocationTableFirstSectorID = reader.ReadInt32();
			header.ShortSectorAllocationTableSize = reader.ReadInt32();

			// SecID of first sector of the master sector allocation table, or –2 (End Of Chain) if no additional sectors used
			header.MasterSectorAllocationTableFirstSectorID = reader.ReadInt32();
			// Total number of sectors used for the master sector allocation table
			header.MasterSectorAllocationTableSize = reader.ReadInt32();

			header.MasterSectorAllocationTable = reader.ReadInt32Array(109);

			return header;
		}

		private CompoundDocumentStorageHeader ReadStorageHeader(Reader directoryReader)
		{
			CompoundDocumentStorageHeader sh = new CompoundDocumentStorageHeader();

			// The first directory entry always represents the root storage entry
			byte[] storageNameBytes = directoryReader.ReadBytes(64);
			ushort storageNameLength = directoryReader.ReadUInt16();

			byte[] storageNameValidBytes = new byte[storageNameLength];
			Array.Copy(storageNameBytes, 0, storageNameValidBytes, 0, storageNameValidBytes.Length);

			string storageName = System.Text.Encoding.Unicode.GetString(storageNameValidBytes);
			storageName = storageName.TrimNull();
			// if (storageName.Length != storageNameLength) throw new InvalidDataFormatException("Sanity check: storage name length is not actual length of storage name");
			sh.Name = storageName;

			sh.StorageType = (CompoundDocumentStorageType)directoryReader.ReadByte();
			sh.NodeColor = (CompoundDocumentStorageColor) directoryReader.ReadByte();

			sh.LeftChildNodeDirectoryID = directoryReader.ReadInt32();
			sh.RightChildNodeDirectoryID = directoryReader.ReadInt32();
			// directory ID of the root node entry of the red-black tree of all members of the root storage
			sh.RootNodeEntryDirectoryID = directoryReader.ReadInt32();

			sh.UniqueIdentifier = directoryReader.ReadGuid();
			sh.Flags = (CompoundDocumentStorageFlags) directoryReader.ReadUInt32();
			sh.CreationTimestamp = ReadDateTime(directoryReader);
			sh.ModificationTimestamp = ReadDateTime(directoryReader);

			// SecID of first sector or short-sector, if this entry refers to a stream
			// SecID of first sector of the short-stream container stream, if this is the root storage entry
			// 0 otherwise
			sh.FirstSectorIndex = directoryReader.ReadInt32();

			// Total stream size in bytes, if this entry refers to a stream,
			// total size of the short-stream container stream, if this is the root storage entry
			// 0 otherwise
			sh.Length = directoryReader.ReadInt32();

			sh.Unused3 = directoryReader.ReadInt32();
			return sh;
		}
		private void WriteStorageHeader(Writer directoryWriter, CompoundDocumentStorageHeader sh)
		{
			// The first directory entry always represents the root storage entry
			byte[] storageNameValidBytes = System.Text.Encoding.Unicode.GetBytes(sh.Name);
			byte[] storageNameBytes = new byte[64];
			Array.Copy(storageNameValidBytes, 0, storageNameBytes, 0, Math.Min(storageNameValidBytes.Length, storageNameBytes.Length));

			directoryWriter.WriteBytes(storageNameBytes);
			directoryWriter.WriteUInt16((ushort)sh.Name.Length);

			directoryWriter.WriteByte((byte)sh.StorageType);
			directoryWriter.WriteByte((byte)sh.NodeColor);

			directoryWriter.WriteInt32(sh.LeftChildNodeDirectoryID);
			directoryWriter.WriteInt32(sh.RightChildNodeDirectoryID);
			// directory ID of the root node entry of the red-black tree of all members of the root storage
			directoryWriter.WriteInt32(sh.RootNodeEntryDirectoryID);

			directoryWriter.WriteGuid(sh.UniqueIdentifier);
			directoryWriter.WriteUInt32((uint)sh.Flags);
			WriteDateTime(directoryWriter, sh.CreationTimestamp);
			WriteDateTime(directoryWriter, sh.ModificationTimestamp);

			// SecID of first sector or short-sector, if this entry refers to a stream
			// SecID of first sector of the short-stream container stream, if this is the root storage entry
			// 0 otherwise
			directoryWriter.WriteInt32(sh.FirstSectorIndex);

			// Total stream size in bytes, if this entry refers to a stream,
			// total size of the short-stream container stream, if this is the root storage entry
			// 0 otherwise
			directoryWriter.WriteInt32(sh.Length);

			directoryWriter.WriteInt32(sh.Unused3);
		}

		private void WriteDateTime(Writer directoryWriter, DateTime creationTimestamp)
		{
			long value = 0;
			directoryWriter.WriteInt64(value);
		}

		private DateTime ReadDateTime(Reader reader)
		{
			long value = reader.ReadInt64();
			return DateTime.Now;
		}

		private void WriteLog(FileSystemObjectModel fsom)
		{
			if (LogPath == null) return;
			if (!System.IO.Directory.Exists(LogPath))
			{
				System.IO.Directory.CreateDirectory(LogPath);
			}
			foreach (File file in fsom.Files)
			{
				System.IO.File.WriteAllBytes(LogPath + System.IO.Path.DirectorySeparatorChar.ToString() + file.Name, file.GetData());
			}
		}

		private byte[] ReadDirectoryData(Reader reader, CompoundDocumentHeader header)
		{
			List<int> directorySectors = new List<int>();
			int currentSector = (int)header.DirectoryStreamFirstSectorID;
			while (currentSector != -2)
			{
				directorySectors.Add(currentSector);
				if (currentSector < sectorAllocationTable.Length - 1)
				{
					currentSector = sectorAllocationTable[currentSector];
				}
				else
				{
					throw new IndexOutOfRangeException(String.Format("sector {0} is out of bounds for sector allocation table (length {1})", currentSector, sectorAllocationTable.Length)); ;
				}
			}

			byte[] directoryData = new byte[SectorSize * directorySectors.Count];
			for (int i = 0; i < directorySectors.Count; i++)
			{
				SeekToSector(directorySectors[i]);
				byte[] sectorData = reader.ReadBytes(SectorSize);
				Array.Copy(sectorData, 0, directoryData, (i * SectorSize), SectorSize);
			}
			return directoryData;
		}

		private void SeekToSector(int v)
		{
			int pos = GetSectorPositionFromSectorID(v);
			Accessor.Seek(pos, SeekOrigin.Begin);
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];

			int firstSectorOfStream = (int)file.Properties["firstSector"];
			int streamLength = (int)file.Properties["length"];

			List<int> sectors = new List<int>();
			byte[] sectorData = null;
			if (streamLength < MinimumStandardStreamSize)
			{
				// use the short-sector allocation table
				int sector = firstSectorOfStream;
				while (sector >= 0)
				{
					sectors.Add(sector);
					if (sector < shortSectorAllocationTable.Length)
					{
						sector = shortSectorAllocationTable[sector];
					}
					else
					{
						throw new IndexOutOfRangeException(String.Format("sector {0} is out of bounds for short sector allocation table (length: {1})", sector, shortSectorAllocationTable.Length));
					}
				}

				sectorData = new byte[sectors.Count * ShortSectorSize];
				for (int i = 0; i < sectors.Count; i++)
				{
					Array.Copy(mvarShortSectorContainerStreamData, (sectors[i] * ShortSectorSize), sectorData, i * ShortSectorSize, ShortSectorSize);
				}

				Array.Resize(ref sectorData, streamLength);
			}
			else
			{
				// use the standard sector allocation table
				int sector = firstSectorOfStream;
				while (sector >= 0)
				{
					sectors.Add(sector);
					if (sector < sectorAllocationTable.Length)
					{
						sector = sectorAllocationTable[sector];
					}
					else
					{
						throw new IndexOutOfRangeException(String.Format("sector {0} is out of bounds for sector allocation table (length: {1})", sector, sectorAllocationTable.Length));
					}
				}

				sectorData = new byte[sectors.Count * SectorSize];
				for (int i = 0; i < sectors.Count; i++)
				{
					SeekToSector(sectors[i]);
					byte[] data = reader.ReadBytes(SectorSize);
					Array.Copy(data, 0, sectorData, (i * SectorSize), data.Length);
				}

				Array.Resize(ref sectorData, streamLength);
			}

			e.Data = sectorData;
			// TODO: actually make this work!

			/*


			List<int> sectors = new List<int>();
			if (storageType != 0x05) // hack: why do we need this?
			{

			}

			*/
			/*
			if (shortSectorReader == null) shortSectorReader = new Reader(new Accessors.MemoryAccessor(mvarShortSectorContainerStreamData));
			List<int> sectors = (List<int>)file.Properties["sectors"];
			if (sectors.Count == 0)
			{
				e.Data = new byte[0];
				return;
			}

			int length = (int)file.Properties["length"];

			byte[] realdata = new byte[length];
			if (length < mvarMinimumStandardStreamSize)
			{
				// use the short-sector allocation table and short stream container stream
				byte[] data = new byte[sectors.Count * mvarShortSectorSize];
				int start = 0;
				foreach (int sector in sectors)
				{
					int pos = GetShortSectorPositionFromSectorID(sector);
					shortSectorReader.Accessor.Seek(pos, SeekOrigin.Begin);
					byte[] sectorData = shortSectorReader.ReadBytes(mvarShortSectorSize);
					Array.Copy(sectorData, 0, data, start, sectorData.Length);
					start += (int)mvarShortSectorSize;
				}
				Array.Copy(data, 0, realdata, 0, realdata.Length);
			}
			else
			{
				byte[] data = new byte[sectors.Count * mvarSectorSize];
				int start = 0;
				foreach (int sector in sectors)
				{
					int pos = GetSectorPositionFromSectorID(sector);
					reader.Accessor.Seek(pos, SeekOrigin.Begin);
					byte[] sectorData = reader.ReadBytes(mvarSectorSize);
					Array.Copy(sectorData, 0, data, start, sectorData.Length);
					start += (int)mvarSectorSize;
				}
				Array.Copy(data, 0, realdata, 0, realdata.Length);
			}
			e.Data = realdata;
			*/
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			CompoundDocumentHeader header = new CompoundDocumentHeader();
			header.Signature = VALID_SIGNATURE;
			header.UniqueIdentifier = UniqueIdentifier;
			header.MinorVersion = (ushort)FormatVersion.Minor;
			header.MajorVersion = (ushort)FormatVersion.Major;
			switch (Endianness)
			{
				case Endianness.LittleEndian:
				{
					header.ByteOrderIdentifier = new byte[] { 0xFE, 0xFF };
					break;
				}
				case Endianness.BigEndian:
				{
					header.ByteOrderIdentifier = new byte[] { 0xFF, 0xFE };
					break;
				}
			}

			WriteCompoundDocumentHeader(writer, header);


			if (fsom.Folders.Count != 1)
			{
				throw new ObjectModelNotSupportedException("underlying File System must contain exactly ONE root folder, which should be named 'Root Entry'");
			}
			if (fsom.Folders[0].Name != "Root Entry")
			{
				// we should probably warn the user that this is not kosher, but we will happily write it
			}

			// FIXME: this is NOT correct
			SeekToSector(16);

			CompoundDocumentStorageHeader shRootEntry = new CompoundDocumentStorageHeader();
			shRootEntry.Name = fsom.Folders[0].Name;
			WriteStorageHeader(writer, shRootEntry);

			foreach (File f in fsom.Folders[0].Files)
			{
				CompoundDocumentStorageHeader sh = new CompoundDocumentStorageHeader();
				sh.Name = f.Name;
				sh.StorageType = CompoundDocumentStorageType.UserStream;
				sh.NodeColor = CompoundDocumentStorageColor.Red;
				sh.LeftChildNodeDirectoryID = 0;
				sh.RightChildNodeDirectoryID = 0;
				sh.RootNodeEntryDirectoryID = 0;
				sh.UniqueIdentifier = Guid.Empty;
				sh.Flags = CompoundDocumentStorageFlags.None;
				sh.CreationTimestamp = DateTime.Now;
				sh.ModificationTimestamp = f.ModificationTimestamp;
				sh.FirstSectorIndex = 0;
				sh.Length = (int)f.Size;
				sh.Unused3 = 0;

				WriteStorageHeader(writer, sh);
			}
		}

		private void WriteCompoundDocumentHeader(Writer writer, CompoundDocumentHeader header)
		{
			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			writer.WriteBytes(header.Signature);
			writer.WriteGuid(header.UniqueIdentifier);

			writer.WriteUInt16(header.MinorVersion);
			writer.WriteUInt16(header.MajorVersion);
			writer.WriteBytes(header.ByteOrderIdentifier);

			if (ShortSectorSize > SectorSize) throw new InvalidDataFormatException("Short sector size (" + ShortSectorSize.ToString() + ") exceeds sector size (" + SectorSize.ToString() + ")");

			// get the 2-root of SectorSize
			header.SectorSize = (ushort)(Math.Log10(SectorSize) / Math.Log10(2));
			writer.WriteUInt16(header.SectorSize);

			// get the 2-root of SectorSize
			header.ShortSectorSize = (ushort)(Math.Log10(ShortSectorSize) / Math.Log10(2));
			writer.WriteUInt16(header.ShortSectorSize);

			header.Unused1 = new byte[6];
			writer.WriteBytes(header.Unused1); // unused?

			writer.WriteUInt32(header.DirectorySectorCount);
			writer.WriteUInt32(header.SectorAllocationTableSize);
			writer.WriteUInt32(header.DirectoryStreamFirstSectorID);
			writer.WriteUInt32(header.TransactionSignatureNumber);
			writer.WriteUInt32(header.MinimumStandardStreamSize);

			writer.WriteInt32(header.ShortSectorAllocationTableFirstSectorID);
			writer.WriteInt32(header.ShortSectorAllocationTableSize);
			writer.WriteInt32(header.MasterSectorAllocationTableFirstSectorID);
			writer.WriteInt32(header.MasterSectorAllocationTableSize);

			#region Read Master Sector Allocation Table
			// First part of the master sector allocation table, containing 109 SecIDs
			int[] masterSectorAllocationTable = new int[109];
			writer.WriteInt32Array(masterSectorAllocationTable);

			// TODO: test this! when MSAT contains more than 109 SecIDs
			int countForMSAT = (int)((double)SectorSize / 4);
			int nextSectorForMSAT = header.MasterSectorAllocationTableFirstSectorID;
			int nextPositionForMSAT = masterSectorAllocationTable.Length;
			#endregion

			// ok, i have absolutely no clue what we're doing here
		}
	}
}
