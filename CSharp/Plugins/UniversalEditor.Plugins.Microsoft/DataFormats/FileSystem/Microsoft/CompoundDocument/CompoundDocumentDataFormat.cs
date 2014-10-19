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

		private void ReadHeader(Reader reader)
		{
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

			// First part of the master sector allocation table, containing 109 SecIDs
			int[] masterSectorAllocationTable = reader.ReadInt32Array(109);
			if (mvarMasterSectorAllocationTableFirstSectorID != (int)CompoundDocumentKnownSectorID.EndOfChain)
			{
				int nextSectorForMSAT = masterSectorAllocationTable[masterSectorAllocationTable.Length - 1];
			}

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

			Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(data);
			reader = new Reader(ma);

			List<File> files = new List<File>();
			while (!reader.EndOfStream)
			{
				// The first directory entry always represents the root storage entry
				string storageName = reader.ReadFixedLengthString(64, IO.Encoding.UTF16LittleEndian).TrimNull();
				ushort storageNameLength = reader.ReadUInt16();
				storageNameLength /= 2;
				if (storageNameLength > 0) storageNameLength -= 1;
				if (storageName.Length != storageNameLength) throw new InvalidDataFormatException("Sanity check: storage name length is not actual length of storage name");

				byte storageType = reader.ReadByte();
				byte storageNodeColor = reader.ReadByte();

				int leftChildNodeDirectoryID = reader.ReadInt32();
				int rightChildNodeDirectoryID = reader.ReadInt32();
				// directory ID of the root node entry of the red-black tree of all members of the root storage
				int rootNodeEntryDirectoryID = reader.ReadInt32();

				Guid uniqueIdentifier = reader.ReadGuid();
				uint flags = reader.ReadUInt32();
				long creationTimestamp = reader.ReadInt64();
				long lastModificationTimestamp = reader.ReadInt64();

				int firstSectorOfStream = reader.ReadInt32();
				int streamOffset = GetSectorPositionFromSectorID(firstSectorOfStream);
				int streamLength = reader.ReadInt32();
				int unused3 = reader.ReadInt32();

				if (streamLength < mvarMinimumStandardStreamSize)
				{
					// stored as a short-sector container stream
					streamOffset = (int)(512 + (firstSectorOfStream * mvarShortSectorSize));
				}

				File file = new File();
				file.Name = storageName;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", streamOffset);
				file.Properties.Add("length", streamLength);
				file.DataRequest += file_DataRequest;
				files.Add(file);
			}
		}

		private int GetSectorPositionFromSectorID(int sectorID)
		{
			if (sectorID < 0) return 0;
			return (int)(512 + (sectorID * mvarSectorSize));
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			// The header is always located at the beginning of the file, and its size is
			// exactly 512 bytes. This implies that the first sector (0) always starts at
			// file offset 512.
			ReadHeader(reader);

			// TODO: read extra sectors if necessary

			int directoryEntryLength = 128;
			int directoryEntryCount = (int)((mvarSectorAllocationTableSize * mvarSectorSize) / directoryEntryLength);
			for (int i = 0; i < directoryEntryCount; i++)
			{
				// The first directory entry always represents the root storage entry
				string storageName = reader.ReadFixedLengthString(64, IO.Encoding.UTF16LittleEndian).TrimNull();
				ushort storageNameLength = reader.ReadUInt16();
				storageNameLength /= 2;
				storageNameLength -= 1;
				if (storageName.Length != storageNameLength) throw new InvalidDataFormatException("Sanity check: storage name length is not actual length of storage name");

				byte storageType = reader.ReadByte();
				byte storageNodeColor = reader.ReadByte();

				int leftChildNodeDirectoryID = reader.ReadInt32();
				int rightChildNodeDirectoryID = reader.ReadInt32();
				// directory ID of the root node entry of the red-black tree of all members of the root storage
				int rootNodeEntryDirectoryID = reader.ReadInt32();

				Guid uniqueIdentifier = reader.ReadGuid();
				uint flags = reader.ReadUInt32();
				long creationTimestamp = reader.ReadInt64();
				long lastModificationTimestamp = reader.ReadInt64();

				int firstSectorOfStream = reader.ReadInt32();
				int streamOffset = GetSectorPositionFromSectorID(firstSectorOfStream);
				int streamLength = reader.ReadInt32();
				int unused3 = reader.ReadInt32();

				/*
				The directory entry of a stream contains the SecID of the first sector or
				short-sector containing the stream data. All streams that are shorter than a
				specific size given in the header are stored as a short-stream, thus inserted
				into the short-stream container stream. In this case the SecID specifies the
				first short-sector inside the short-stream container stream, and the
				short-sector allocation table is used to build up the SecID chain of the
				stream.
				*/

				if (i == 0)
				{
					// in the case of the Root Entry, the firstSectorOfStream is the SecID of
					// the first sector and the streamLength is the size of the short-stream
					// container stream
					continue;
				}
				else
				{

				}

				if (streamLength < mvarMinimumStandardStreamSize)
				{
					// stored as a short-sector container stream
					streamOffset = (int)(512 + (firstSectorOfStream * mvarShortSectorSize));
				}

				File file = new File();
				file.Name = storageName;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", streamOffset);
				file.Properties.Add("length", streamLength);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			int offset = (int)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			reader.Accessor.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
		}
	}
}
