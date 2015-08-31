using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.DataFormats.Kronosaur.ResourceTable;
using UniversalEditor.ObjectModels.Kronosaur.ResourceTable;
using UniversalEditor.ObjectModels.FileSystem.FileSources;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;

namespace UniversalEditor.DataFormats.FileSystem.Kronosaur.TDB
{
	public class ALDFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("https://github.com/kronosaur/Alchemy/blob/7935e4e338a2fc112f95f354dcba6cdea4318553/Kernel/CDataFile.cpp");
			}
			return _dfr;
		}

		private const int HEADERSIZE			=	60;
		private const int ENTRYSTRUCTSIZE		=	28;

		private const int FREE_ENTRY			=	-1;
		private const int INVALID_ENTRY			=	-1;

		private int mvarFormatVersion = 2;
		public int FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		private int mvarBlockSize = 4096;
		public int BlockSize { get { return mvarBlockSize; } set { mvarBlockSize = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "FDLA") throw new InvalidDataFormatException("File does not begin with 'FDLA'");

			mvarFormatVersion = reader.ReadInt32();

			mvarBlockSize = reader.ReadInt32();
			int blockCount = reader.ReadInt32();
			int entryTableCount = reader.ReadInt32();
			int entryTableOffset = reader.ReadInt32();
			int defaultEntryIndex = reader.ReadInt32();
			int[] reserved = reader.ReadInt32Array(8);

			// read the entry table
			reader.Seek(entryTableOffset, SeekOrigin.Begin);

			List<ALDFEntryStruct> entries = new List<ALDFEntryStruct>();
			for (int i = 0; i < entryTableCount; i++)
			{
				ALDFEntryStruct entry = ReadALDFEntryStruct(reader);
				entries.Add(entry);
			}

			ALDFEntryStruct defaultEntry = entries[defaultEntryIndex];
			SeekToBlock(reader, defaultEntry.dwBlock, true);

			ResourceTableObjectModel trobj = new ResourceTableObjectModel();
			Document.Load(trobj, new TRDBDataFormat(), reader.Accessor, false);

			CompressionModule zlib = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib);

			foreach (ResourceTableEntry entry in trobj.Entries)
			{
				ALDFEntryStruct entry1 = entries[entry.EntryID];
				File file = fsom.AddFile(entry.Name);
				file.Source = new EmbeddedFileSource(reader, GetLogicalOffsetFromBlockOffset(entry1.dwBlock, true), entry1.dwSize, new FileSourceTransformation[] { new FileSourceTransformation(FileSourceTransformationType.Input, delegate(System.IO.Stream inputStream, System.IO.Stream outputStream)
				{
					if ((entry.Flags & ResourceTableEntryFlags.CompressZlib) == ResourceTableEntryFlags.CompressZlib)
					{
						Reader br = new Reader(new StreamAccessor(inputStream));
						Writer bw = new Writer(new StreamAccessor(outputStream));

						byte[] compressedData = br.ReadToEnd();
						byte[] decompressedData = zlib.Decompress(compressedData);
						bw.WriteBytes(decompressedData);
						bw.Flush();
					}
				}) });

				file.Size = entry1.dwSize;
			}
		}

		private long GetLogicalOffsetFromBlockOffset(int blockIndex, bool includeHeaderSize = false)
		{
			return (blockIndex * mvarBlockSize) + (includeHeaderSize ? HEADERSIZE : 0);
		}

		private void SeekToBlock(Reader reader, int blockIndex, bool includeHeaderSize = false)
		{
			long offset = GetLogicalOffsetFromBlockOffset(blockIndex, includeHeaderSize);
			reader.Seek(offset, SeekOrigin.Begin);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			File[] files = fsom.GetAllFiles();

			int iInitialEntries = files.Length;
			int iBlockSize = 4096;

			//	Figure out how big the entry table will be
			int iEntryTableSize = (int)((iInitialEntries + 1) * ENTRYSTRUCTSIZE);
			int iBlockCount = ((iEntryTableSize / iBlockSize) + 1);
			int iEntryTableCount = iInitialEntries + 1;
			int iEntryTableOffset = HEADERSIZE;

			// Write the header
			writer.WriteFixedLengthString("ALDF");
			writer.WriteInt32(mvarFormatVersion);
			writer.WriteInt32(iBlockSize);
			writer.WriteInt32(iBlockCount);
			writer.WriteInt32(iEntryTableCount);
			writer.WriteInt32(iEntryTableOffset);
			writer.WriteInt32(0);
			writer.WriteInt32Array(new int[8]);

			// Prepare the single entry that describes the entry table
			ALDFEntryStruct entry = new ALDFEntryStruct();
			entry.dwBlock = 0;
			entry.dwBlockCount = iBlockCount;
			entry.dwSize = iEntryTableSize;
			entry.dwVersion = 1;
			entry.dwPrevEntry = INVALID_ENTRY;
			entry.dwLatestEntry = INVALID_ENTRY;
			entry.dwFlags = ALDFEntryFlags.None;

			// Write the single entry that describes the entry table
			WriteALDFEntryStruct(writer, entry);

			//	Write the rest of the entries
			entry.dwBlock = FREE_ENTRY;
			entry.dwBlockCount = 0;
			entry.dwSize = 0;
			entry.dwVersion = 1;
			entry.dwPrevEntry = INVALID_ENTRY;
			entry.dwLatestEntry = INVALID_ENTRY;
			entry.dwFlags = ALDFEntryFlags.None;

			for (int i = 0; i < files.Length; i++)
			{
				WriteALDFEntryStruct(writer, entry);
			}
		}

		private ALDFEntryStruct ReadALDFEntryStruct(Reader reader)
		{
			ALDFEntryStruct entry = new ALDFEntryStruct();
			entry.dwBlock = reader.ReadInt32();
			entry.dwBlockCount = reader.ReadInt32();
			entry.dwSize = reader.ReadInt32();
			entry.dwVersion = reader.ReadInt32();
			entry.dwPrevEntry = reader.ReadInt32();
			entry.dwLatestEntry = reader.ReadInt32();
			entry.dwFlags = (ALDFEntryFlags)reader.ReadInt32();
			return entry;
		}
		private void WriteALDFEntryStruct(Writer writer, ALDFEntryStruct entry)
		{
			writer.WriteInt32(entry.dwBlock);
			writer.WriteInt32(entry.dwBlockCount);
			writer.WriteInt32(entry.dwSize);
			writer.WriteInt32(entry.dwVersion);
			writer.WriteInt32(entry.dwPrevEntry);
			writer.WriteInt32(entry.dwLatestEntry);
			writer.WriteInt32((int)entry.dwFlags);
		}
	}
}
