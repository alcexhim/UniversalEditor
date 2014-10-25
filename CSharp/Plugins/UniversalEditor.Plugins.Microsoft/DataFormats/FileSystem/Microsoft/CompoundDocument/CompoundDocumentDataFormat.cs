using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument
{
	public class CompoundDocumentDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionNumber("SectorSize", "&Sector size (in bytes):", 512, 128));
				_dfr.ExportOptions.Add(new CustomOptionNumber("ShortSectorSize", "S&hort sector size (in bytes):", 64));
				_dfr.ExportOptions.Add(new CustomOptionNumber("MinimumStandardStreamSize", "&Minimum standard stream size (in bytes):", 4096, 4096));
				_dfr.Filters.Add("Microsoft Compound Document file system", new byte?[][] { new byte?[] { 0xd0, 0xcf, 0x11, 0xe0, 0xa1, 0xb1, 0x1a, 0xe1 } }, new string[] { "*.cbf" });
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			byte[] validSignature = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };
			byte[] signature = reader.ReadBytes(8);
			if (!signature.Match(validSignature))
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

			ushort SectorSize = reader.ReadUInt16();
			mvarSectorSize = (uint)(Math.Pow(2, SectorSize));

			ushort ShortSectorSize = reader.ReadUInt16();
			mvarShortSectorSize = (uint)(Math.Pow(2, ShortSectorSize));

			if (ShortSectorSize > SectorSize) throw new InvalidDataFormatException("Short sector size (" + ShortSectorSize.ToString() + ") exceeds sector size (" + SectorSize.ToString() + ")");

			byte[] unused1 = reader.ReadBytes(10);

			mvarSectorAllocationTableSize = reader.ReadUInt32();
			mvarDirectoryStreamFirstSectorID = reader.ReadUInt32();
			uint unused2 = reader.ReadUInt32();
			mvarMinimumStandardStreamSize = reader.ReadUInt32();

			mvarShortSectorAllocationTableFirstSectorID = reader.ReadInt32();
			mvarShortSectorAllocationTableSize = reader.ReadInt32();
			mvarMasterSectorAllocationTableFirstSectorID = reader.ReadInt32();
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
				Array.Resize(ref masterSectorAllocationTable, masterSectorAllocationTable.Length + countForMSAT);
				int[] masterSectorAllocationTablePart = reader.ReadInt32Array(countForMSAT);
				Array.Copy(masterSectorAllocationTablePart, 0, masterSectorAllocationTable, nextPositionForMSAT, masterSectorAllocationTablePart.Length);

				nextSectorForMSAT = masterSectorAllocationTablePart[nextSectorForMSAT];
			}
			#endregion
			#region Read Sector Allocation Table
			int pos = GetSectorPositionFromSectorID(masterSectorAllocationTable[0]);
			reader.Accessor.Seek(pos, SeekOrigin.Begin);
			int[] sectorAllocationTable = reader.ReadInt32Array((int)(mvarSectorSize / 4));

			List<int> directorySectors = new List<int>();
			int currentSector = (int)mvarDirectoryStreamFirstSectorID;
			while (currentSector != -2)
			{
				directorySectors.Add(currentSector);
				currentSector = sectorAllocationTable[currentSector];
			}

			byte[] data = new byte[mvarSectorSize * directorySectors.Count];
			for (int i = 0; i < directorySectors.Count; i++)
			{
				pos = GetSectorPositionFromSectorID(directorySectors[i]);
				reader.Accessor.Seek(pos, SeekOrigin.Begin);
				byte[] sectorData = reader.ReadBytes(mvarSectorSize);
				Array.Copy(sectorData, 0, data, (i * mvarSectorSize), mvarSectorSize);
			}
			#endregion
			#region Read Short Sector Allocation Table
			List<int> shortSectorAllocationTable = new List<int>();
			List<int> shortSectorAllocationTableSectors = new List<int>();
			if (mvarShortSectorAllocationTableFirstSectorID >= 0)
			{
				int sector = mvarShortSectorAllocationTableFirstSectorID;
				while (sector >= 0)
				{
					shortSectorAllocationTableSectors.Add(sector);
					sector = sectorAllocationTable[sector];
				}
			}

			byte[] shortSectorAllocationTableData = new byte[mvarSectorSize * shortSectorAllocationTableSectors.Count];
			for (int i = 0; i < shortSectorAllocationTableSectors.Count; i++)
			{
				pos = GetSectorPositionFromSectorID(shortSectorAllocationTableSectors[i]);
				reader.Accessor.Seek(pos, SeekOrigin.Begin);
				byte[] sectorData = reader.ReadBytes(mvarSectorSize);
				Array.Copy(sectorData, 0, shortSectorAllocationTableData, (i * mvarSectorSize), mvarSectorSize);
			}

			Accessors.MemoryAccessor ma1 = new Accessors.MemoryAccessor(shortSectorAllocationTableData);
			Reader shortSectorAllocationTableReader = new Reader(ma1);
			while (!shortSectorAllocationTableReader.EndOfStream)
			{
				int sectorID = shortSectorAllocationTableReader.ReadInt32();
				shortSectorAllocationTable.Add(sectorID);
			}
			#endregion
			#region Read Sector Directory Entries
			Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(data);
			
			Reader sectorReader = new Reader(ma);
			while (!reader.EndOfStream)
			{
				// The first directory entry always represents the root storage entry
				string storageName = sectorReader.ReadFixedLengthString(64, IO.Encoding.UTF16LittleEndian).TrimNull();
				if (String.IsNullOrEmpty(storageName)) break;

				ushort storageNameLength = sectorReader.ReadUInt16();
				storageNameLength /= 2;
				if (storageNameLength > 0) storageNameLength -= 1;
				if (storageName.Length != storageNameLength) throw new InvalidDataFormatException("Sanity check: storage name length is not actual length of storage name");

				byte storageType = sectorReader.ReadByte();
				byte storageNodeColor = sectorReader.ReadByte();

				int leftChildNodeDirectoryID = sectorReader.ReadInt32();
				int rightChildNodeDirectoryID = sectorReader.ReadInt32();
				// directory ID of the root node entry of the red-black tree of all members of the root storage
				int rootNodeEntryDirectoryID = sectorReader.ReadInt32();

				Guid uniqueIdentifier = sectorReader.ReadGuid();
				uint flags = sectorReader.ReadUInt32();
				long creationTimestamp = sectorReader.ReadInt64();
				long lastModificationTimestamp = sectorReader.ReadInt64();

				int firstSectorOfStream = sectorReader.ReadInt32();
				if (storageType == 0x05)
				{
					// this is the root storage entry
					mvarShortSectorFirstSectorID = firstSectorOfStream;

					#region Read Short Stream Container Stream
					List<int> shortStreamContainerStreamSectors = new List<int>();
					{
						int shortSectorDataSector = mvarShortSectorFirstSectorID;
						while (shortSectorDataSector >= 0)
						{
							shortStreamContainerStreamSectors.Add(shortSectorDataSector);
							shortSectorDataSector = sectorAllocationTable[shortSectorDataSector];
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

				int streamLength = sectorReader.ReadInt32();
				int unused3 = sectorReader.ReadInt32();

				List<int> sectors = new List<int>();
				if (streamLength < mvarMinimumStandardStreamSize)
				{
					// use the short-sector allocation table
					int sector = firstSectorOfStream;
					while (sector >= 0)
					{
						sectors.Add(sector);
						sector = shortSectorAllocationTable[sector];
					}
				}
				else
				{
					// use the standard sector allocation table
					int sector = firstSectorOfStream;
					while (sector >= 0)
					{
						sectors.Add(sector);
						sector = sectorAllocationTable[sector];
					}
				}

				File file = new File();
				file.Name = storageName;
				file.Properties.Add("reader", reader);
				file.Properties.Add("sectors", sectors);
				file.Properties.Add("length", streamLength);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
			#endregion
		}

		private Reader shortSectorReader = null;

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			if (shortSectorReader == null) shortSectorReader = new Reader(new Accessors.MemoryAccessor(mvarShortSectorContainerStreamData));
			List<int> sectors = (List<int>)file.Properties["sectors"];
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
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
		}
	}
}
