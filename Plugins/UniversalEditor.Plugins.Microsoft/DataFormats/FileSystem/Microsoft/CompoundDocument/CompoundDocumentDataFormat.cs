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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Compound Document Format files. This class may be inherited to implement
	/// <see cref="DataFormat" />s that manipulate files based on the Microsoft Compound Document Format specification, such as those from older
	/// versions of Microsoft Office.
	/// </summary>
	public class CompoundDocumentDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(SectorSize), "_Sector size (in bytes)", 512, 128));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(ShortSectorSize), "S_hort sector size (in bytes)", 64));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(MinimumStandardStreamSize), "_Minimum standard stream size (in bytes)", 4096, 4096));
				_dfr.Sources.Add("http://www.openoffice.org/sc/compdocfileformat.pdf");
			}
			return _dfr;
		}

		private Guid mvarUniqueIdentifier = Guid.Empty;
		public Guid UniqueIdentifier { get { return mvarUniqueIdentifier; } set { mvarUniqueIdentifier = value; } }

		private Version mvarFormatVersion = new Version(3, 62);
		public Version FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private Endianness mvarEndianness = Endianness.LittleEndian;
		public Endianness Endianness { get { return mvarEndianness; } set { mvarEndianness = value; } }

		private uint mvarSectorSize = 512;
		public uint SectorSize { get { return mvarSectorSize; } set { mvarSectorSize = value; } }

		private uint mvarShortSectorSize = 64;
		public uint ShortSectorSize { get { return mvarShortSectorSize; } set { mvarShortSectorSize = value; } }

		/// <summary>
		/// Total number of sectors used for the sector allocation table
		/// </summary>
		private uint mvarSectorAllocationTableSize = 0;
		/// <summary>
		/// Sector ID of the first sector of the directory stream
		/// </summary>
		private uint mvarDirectoryStreamFirstSectorID = 0;

		private uint mvarMinimumStandardStreamSize = 4096;
		/// <summary>
		/// Minimum size of a standard stream (in bytes). Minimum allowed and most-used size is
		/// 4096 bytes. Streams with an actual size smaller than (and not equal to) this value
		/// are stored as short-streams.
		/// </summary>
		public uint MinimumStandardStreamSize { get { return mvarMinimumStandardStreamSize; } set { mvarMinimumStandardStreamSize = value; } }

		/// <summary>
		/// Sector ID of the first sector of the short-sector allocation table (or
		/// <see cref="CompoundDocumentKnownSectorID.EndOfChain" /> if not extant).
		/// </summary>
		private int mvarShortSectorAllocationTableFirstSectorID = 0;
		/// <summary>
		/// Total number of sectors used for the short-sector allocation table.
		/// </summary>
		private int mvarShortSectorAllocationTableSize = 0;
		/// <summary>
		/// Sector ID of the first sector of the master sector allocation table (or
		/// <see cref="CompoundDocumentKnownSectorID.EndOfChain" /> if no additional sectors
		/// used).
		/// </summary>
		private int mvarMasterSectorAllocationTableFirstSectorID = 0;
		/// <summary>
		/// Total number of sectors used for the master sector allocation table.
		/// </summary>
		private int mvarMasterSectorAllocationTableSize = 0;

		private int mvarShortSectorFirstSectorID = 0;

		private int GetSectorPositionFromSectorID(int sectorID)
		{
			if (sectorID < 0) return 0;
			return (int)(512 + (sectorID * mvarSectorSize));
		}
		private int GetShortSectorPositionFromSectorID(int sectorID)
		{
			if (sectorID < 0) return 0;
			return (int)(sectorID * mvarShortSectorSize);
		}

		private byte[] mvarShortSectorContainerStreamData = null;

		private static readonly byte[] VALID_SIGNATURE = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

		public string LogPath { get; set; } = null;

		private int[] shortSectorAllocationTable = new int[0];
		private List<int> shortSectorAllocationTableSectors = new List<int>();
		private int[] sectorAllocationTable = new int[0];

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			byte[] signature = reader.ReadBytes(8);
			if (!signature.Match(VALID_SIGNATURE))
			{
				throw new InvalidDataFormatException("File does not begin with { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }");
			}
			mvarUniqueIdentifier = reader.ReadGuid();

			ushort MinorVersion = reader.ReadUInt16();
			ushort MajorVersion = reader.ReadUInt16();
			mvarFormatVersion = new Version(MajorVersion, MinorVersion);

			byte[] ByteOrderIdentifier = reader.ReadBytes(2);
			if (ByteOrderIdentifier[0] == 0xFE && ByteOrderIdentifier[1] == 0xFF)
			{
				mvarEndianness = Endianness.LittleEndian;
			}
			else if (ByteOrderIdentifier[0] == 0xFF && ByteOrderIdentifier[1] == 0xFE)
			{
				mvarEndianness = Endianness.BigEndian;
			}
			else
			{
				throw new InvalidDataFormatException("Invalid value for byte order (" + ByteOrderIdentifier[0].ToString("X").PadLeft(2, '0') + ", " + ByteOrderIdentifier[1].ToString("X").PadLeft(2, '0') + ")");
			}

			ushort uSectorSize = reader.ReadUInt16();
			mvarSectorSize = (uint)(Math.Pow(2, uSectorSize));

			ushort uShortSectorSize = reader.ReadUInt16();
			mvarShortSectorSize = (uint)(Math.Pow(2, uShortSectorSize));

			if (ShortSectorSize > SectorSize) throw new InvalidDataFormatException("Short sector size (" + ShortSectorSize.ToString() + ") exceeds sector size (" + SectorSize.ToString() + ")");

			byte[] unused1 = reader.ReadBytes(10);

			mvarSectorAllocationTableSize = reader.ReadUInt32();
			mvarDirectoryStreamFirstSectorID = reader.ReadUInt32();
			uint unused2 = reader.ReadUInt32();
			mvarMinimumStandardStreamSize = reader.ReadUInt32();

			mvarShortSectorAllocationTableFirstSectorID = reader.ReadInt32();
			mvarShortSectorAllocationTableSize = reader.ReadInt32();

			// SecID of first sector of the master sector allocation table, or –2 (End Of Chain) if no additional sectors used
			mvarMasterSectorAllocationTableFirstSectorID = reader.ReadInt32();
			// Total number of sectors used for the master sector allocation table
			mvarMasterSectorAllocationTableSize = reader.ReadInt32();

			#region Read Master Sector Allocation Table
			// First part of the master sector allocation table, containing 109 SecIDs
			int[] masterSectorAllocationTable = reader.ReadInt32Array(109);

			// TODO: test this! when MSAT contains more than 109 SecIDs
			int countForMSAT = (int)((double)mvarSectorSize / 4);
			int nextSectorForMSAT = mvarMasterSectorAllocationTableFirstSectorID;
			int nextPositionForMSAT = masterSectorAllocationTable.Length;

			while (nextSectorForMSAT != (int)CompoundDocumentKnownSectorID.EndOfChain)
			{
				int[] masterSectorAllocationTablePart = reader.ReadInt32Array(countForMSAT);
				Array.Resize(ref masterSectorAllocationTable, masterSectorAllocationTable.Length + countForMSAT);
				Array.Copy(masterSectorAllocationTablePart, 0, masterSectorAllocationTable, nextPositionForMSAT, masterSectorAllocationTablePart.Length);

				nextSectorForMSAT = masterSectorAllocationTablePart[masterSectorAllocationTablePart.Length - 1];
			}
			#endregion
			#region Read Sector Allocation Table
			sectorAllocationTable = new int[(int)(mvarSectorSize / 4)];
			for (int i = 0; i < masterSectorAllocationTable.Length; i++)
			{
				if (masterSectorAllocationTable[i] == -1)
					break;

				// The last SecID in each sector of the MSAT refers to the next sector used by the MSAT. If no more sectors follow, the
				// last SecID is the special End Of Chain SecID with the value –2( ➜ 3.1).

				SeekToSector(masterSectorAllocationTable[i]);
				int[] sectorAllocationTablePart = reader.ReadInt32Array((int)(mvarSectorSize / 4));

				Array.Resize<int>(ref sectorAllocationTable, sectorAllocationTable.Length + sectorAllocationTablePart.Length);
				Array.Copy(sectorAllocationTablePart, 0, sectorAllocationTable, (int)(i * (mvarSectorSize / 4)), sectorAllocationTablePart.Length);
			}
			#endregion

			// read directory entries - each entry is 128 bytes
			byte[] directoryData = ReadDirectoryData(reader);

			#region Read Short Sector Allocation Table
			shortSectorAllocationTableSectors = new List<int>();
			if (mvarShortSectorAllocationTableFirstSectorID >= 0)
			{
				int sector = mvarShortSectorAllocationTableFirstSectorID;
				while (sector != -2)
				{
					shortSectorAllocationTableSectors.Add(sector);
					if (sector < sectorAllocationTable.Length - 1) {
						sector = sectorAllocationTable [sector];
					} else {
						throw new IndexOutOfRangeException(String.Format("short sector {0} is out of bounds for sector allocation table (length: {1})", sector, sectorAllocationTable.Length));
					}
				}
			}

			shortSectorAllocationTable = new int[(mvarSectorSize / 4) * shortSectorAllocationTableSectors.Count];
			byte[] shortSectorAllocationTableData = new byte[mvarSectorSize * shortSectorAllocationTableSectors.Count];
			for (int i = 0; i < shortSectorAllocationTableSectors.Count; i++)
			{
				SeekToSector(shortSectorAllocationTableSectors[i]);

				int[] tablePart = reader.ReadInt32Array((int)(mvarSectorSize / 4));
				Array.Copy(tablePart, 0, shortSectorAllocationTable, (i * tablePart.Length), tablePart.Length);
			}
			#endregion
			#region Read Sector Directory Entries
			Accessors.MemoryAccessor maDirectory = new Accessors.MemoryAccessor(directoryData);
			Reader directoryReader = new Reader(maDirectory);
			while (!directoryReader.EndOfStream)
			{
				// The first directory entry always represents the root storage entry
				byte[] storageNameBytes = directoryReader.ReadBytes(64);
				ushort storageNameLength = directoryReader.ReadUInt16();

				byte[] storageNameValidBytes = new byte[storageNameLength];
				Array.Copy(storageNameBytes, 0, storageNameValidBytes, 0, storageNameValidBytes.Length);

				string storageName = System.Text.Encoding.Unicode.GetString(storageNameValidBytes);
				storageName = storageName.TrimNull();
				// if (storageName.Length != storageNameLength) throw new InvalidDataFormatException("Sanity check: storage name length is not actual length of storage name");

				CompoundDocumentStorageType storageType = (CompoundDocumentStorageType) directoryReader.ReadByte();
				byte storageNodeColor = directoryReader.ReadByte();

				int leftChildNodeDirectoryID = directoryReader.ReadInt32();
				int rightChildNodeDirectoryID = directoryReader.ReadInt32();
				// directory ID of the root node entry of the red-black tree of all members of the root storage
				int rootNodeEntryDirectoryID = directoryReader.ReadInt32();

				Guid uniqueIdentifier = directoryReader.ReadGuid();
				uint flags = directoryReader.ReadUInt32();
				long creationTimestamp = directoryReader.ReadInt64();
				long lastModificationTimestamp = directoryReader.ReadInt64();

				// SecID of first sector or short-sector, if this entry refers to a stream
				// SecID of first sector of the short-stream container stream, if this is the root storage entry
				// 0 otherwise
				int firstSectorOfStream = directoryReader.ReadInt32();

				// Total stream size in bytes, if this entry refers to a stream,
				// total size of the short-stream container stream, if this is the root storage entry
				// 0 otherwise
				int streamLength = directoryReader.ReadInt32();

 				int unused3 = directoryReader.ReadInt32();

				if (storageType == CompoundDocumentStorageType.RootStorage)
				{
					// this is the root storage entry
					mvarShortSectorFirstSectorID = firstSectorOfStream;

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
					byte[] shortStreamContainerStreamData = new byte[shortStreamContainerStreamSectors.Count * mvarSectorSize];
					int i = 0;
					foreach (int sector in shortStreamContainerStreamSectors)
					{
						int wpos = GetSectorPositionFromSectorID(sector);
						reader.Seek(wpos, SeekOrigin.Begin);
						byte[] sectorData = reader.ReadBytes(mvarSectorSize);
						Array.Copy(sectorData, 0, shortStreamContainerStreamData, i, sectorData.Length);
						i += sectorData.Length;
					}
					mvarShortSectorContainerStreamData = shortStreamContainerStreamData;
					#endregion
				}
				else if (storageType == CompoundDocumentStorageType.UserStorage)
				{
				}
				else if (storageType == CompoundDocumentStorageType.UserStream)
				{
					File file = new File();
					file.Name = storageName;
					file.Size = streamLength;
					file.Properties.Add("reader", reader);
					file.Properties.Add("firstSector", firstSectorOfStream);
					file.Properties.Add("length", streamLength);
					file.DataRequest += file_DataRequest;
					fsom.Files.Add(file);

					Console.WriteLine("{3}    {0}        {1}    {2}", file.Name, firstSectorOfStream, streamLength, storageType.ToString());
				}
				else if (storageType == CompoundDocumentStorageType.Empty)
				{
					Console.WriteLine(storageName + " is empty; should we included it in file list?");
				}
				else
				{
					throw new NotImplementedException(storageType.ToString() + " not implemented");
				}
			}
			#endregion

			WriteLog(fsom);
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

		private byte[] ReadDirectoryData(Reader reader)
		{
			List<int> directorySectors = new List<int>();
			int currentSector = (int)mvarDirectoryStreamFirstSectorID;
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

			byte[] directoryData = new byte[mvarSectorSize * directorySectors.Count];
			for (int i = 0; i < directorySectors.Count; i++)
			{
				SeekToSector(directorySectors[i]);
				byte[] sectorData = reader.ReadBytes(mvarSectorSize);
				Array.Copy(sectorData, 0, directoryData, (i * mvarSectorSize), mvarSectorSize);
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
			if (streamLength < mvarMinimumStandardStreamSize)
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

			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			writer.WriteBytes(VALID_SIGNATURE);
			writer.WriteGuid(UniqueIdentifier);

			writer.WriteUInt16((ushort)FormatVersion.Minor);
			writer.WriteUInt16((ushort)FormatVersion.Major);

			switch (Endianness)
			{
				case Endianness.LittleEndian:
				{
					writer.WriteBytes(new byte[] { 0xFE, 0xFF });
					break;
				}
				case Endianness.BigEndian:
				{
					writer.WriteBytes(new byte[] { 0xFF, 0xFE });
					break;
				}
			}

			if (ShortSectorSize > SectorSize) throw new InvalidDataFormatException("Short sector size (" + ShortSectorSize.ToString() + ") exceeds sector size (" + SectorSize.ToString() + ")");

			// get the 2-root of SectorSize
			ushort uSectorSize = (ushort)(Math.Log10(SectorSize) / Math.Log10(2));
			writer.WriteUInt16(uSectorSize);

			// get the 2-root of SectorSize
			ushort uShortSectorSize = (ushort)(Math.Log10(ShortSectorSize) / Math.Log10(2));
			writer.WriteUInt16(uShortSectorSize);

			writer.WriteBytes(new byte[10]); // unused?

			writer.WriteUInt32(mvarSectorAllocationTableSize);
			writer.WriteUInt32(mvarDirectoryStreamFirstSectorID);
			writer.WriteUInt32(0);
			writer.WriteUInt32(mvarMinimumStandardStreamSize);

			writer.WriteInt32(mvarShortSectorAllocationTableFirstSectorID);
			writer.WriteInt32(mvarShortSectorAllocationTableSize);
			writer.WriteInt32(mvarMasterSectorAllocationTableFirstSectorID);
			writer.WriteInt32(mvarMasterSectorAllocationTableSize);

			#region Read Master Sector Allocation Table
			// First part of the master sector allocation table, containing 109 SecIDs
			int[] masterSectorAllocationTable = new int[109];
			writer.WriteInt32Array(masterSectorAllocationTable);

			// TODO: test this! when MSAT contains more than 109 SecIDs
			int countForMSAT = (int)((double)mvarSectorSize / 4);
			int nextSectorForMSAT = mvarMasterSectorAllocationTableFirstSectorID;
			int nextPositionForMSAT = masterSectorAllocationTable.Length;
			#endregion

			// ok, i have absolutely no clue what we're doing here
		}
	}
}
