using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.BPlus
{
	/// <summary>
	/// A DataFormat to parse Microsoft WinHelp (HLP) documentation files.
	/// </summary>
	/// <remarks>Based on documentation contributed by M. Winterhoff, Pete Davis, Holger Haase, and Bent Lynggaard.</remarks>
	public class BPlusFileSystemDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		#region Load
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;

			int magic = br.ReadInt32();
			if (magic != 0x00035F3F) throw new InvalidDataFormatException("File does not begin with 0x00035F3F");
			
			int directoryStart = br.ReadInt32(); // offset of FILEHEADER of internal directory
			int firstFreeBlock = br.ReadInt32(); // offset of FREEHEADER or -1L if no free list
			int entireFileSize = br.ReadInt32(); // size of entire help file in bytes

			// At offset DirectoryStart the FILEHEADER of the internal directory is located
			br.Accessor.Position = directoryStart;
			Internal.FILEHEADER internalFileDirectoryHeader = ReadFileHeader(br);

			#region internal file directory
			// The content of the internal directory is used to associate FileNames and
			// FileOffsets. The directory is structured as a B+ tree.

			// A B+ tree is made from leaf-pages and index-pages of fixed size, one of which
			// is the root-page. All entries are contained in leaf-pages. If more entries
			// are required than fit into a single leaf-page, index-pages are used to locate
			// the leaf-page which contains the required entry.

			// A B+ tree starts with a BTREEHEADER telling you the size of the B+ tree pages,
			// the root-page, the number of levels, and the number of all entries in this
			// B+ tree. You must follow (NLevels-1) index-pages before you reach a leaf-page.

			IO.Reader brFile = new IO.Reader(new MemoryAccessor(internalFileDirectoryHeader.FileContent));
			ushort magic1 = brFile.ReadUInt16();
			if (magic1 != 0x293B) throw new InvalidDataFormatException("Could not read internal directory file");

			ushort flags = brFile.ReadUInt16();                         // bit 0x0002 always 1, bit 0x0400 1 if directory
			bool bit0x0002 = ((flags & 0x0002) == 0x0002);
			bool bit0x0400 = ((flags & 0x0400) == 0x0400);

			ushort PageSize = brFile.ReadUInt16();                      // 0x0400=1k if directory, 0x0800=2k else, or 4k
			string Structure = brFile.ReadFixedLengthString(16);        // string describing format of data

			short MustBeZero = brFile.ReadInt16();
			short PageSplits = brFile.ReadInt16();                      // number of page splits B+ tree has suffered
			short RootPage = brFile.ReadInt16();                        // page number of B+ tree root page
			short MustBeNegOne = brFile.ReadInt16();                    // 0xFFFF
			short TotalPages = brFile.ReadInt16();                      // number of B+ tree pages
			short NLevels = brFile.ReadInt16();                         // number of levels of B+ tree
			int TotalBPlusTreeEntries = brFile.ReadInt32();             // number of entries in B+ tree

			for (int i = 0; i < NLevels - 1; i++)
			{
				#region Index page
				// If NLevel is greater than 1, RootPage is the page number of an index-page.
				// Index-pages start with a BTREEINDEXHEADER and are followed by an array of
				// BTREEINDEX structures, in case of the internal directory containing pairs
				// of FileNames and PageNumbers.

				// (STRINGZ is a NUL-terminated string, sizeof(STRINGZ) is strlen(string)+1).
				// PageNumber gets you to the next page containing entries lexically starting
				// at FileName, but less than the next FileName. PreviousPage gets you to the
				// next page if the desired FileName is lexically before the first FileName.
				Internal.BTREEINDEXHEADER indexHeader = ReadBTreeIndexHeader(brFile);
				for (short j = 0; j < indexHeader.PageEntriesCount; j++)
				{
					// this is the structure of directory index-pages
					Internal.DIRECTORYINDEXENTRY entry = ReadDirectoryIndexEntry(brFile);
				}
				#endregion
			}

			// After NLevels-1 of index-pages you will reach a leaf-page starting with a
			// BTREENODEHEADER followed by an array of BTREELEAF structures, in case of the
			// internal directory containing pairs of FileNames and FileOffsets.
			// You may follow the PreviousPage entry in all NLevels-1 index-pages to reach
			// the first leaf-page, then iterate thru all entries and use NextPage to
			// follow the double linked list of leaf-pages until NextPage is -1 to retrieve
			// a sorted list of all TotalBtreeEntries entries contained in the B+ tree.
			Internal.BTREENODEHEADER nodeHeader = ReadBTreeNodeHeader(brFile);
			List<Internal.DIRECTORYLEAFENTRY> entries = new List<Internal.DIRECTORYLEAFENTRY>();
			for (short i = 0; i < nodeHeader.PageEntriesCount; i++)
			{
				// this is the structure of directory index-pages
				Internal.DIRECTORYLEAFENTRY entry = ReadDirectoryLeafEntry(brFile);
				entries.Add(entry);
			}
			#endregion

			
			foreach (Internal.DIRECTORYLEAFENTRY entry in entries)
			{
				br.Accessor.Position = entry.FileOffset;
				
				Internal.FILEHEADER fileHeader = ReadFileHeader(br);

				File file = new File();
				file.Name = entry.FileName;
				file.SetData(fileHeader.FileContent);
				fsom.Files.Add(file);
			}
		}
		#region Internal Methods
		private Internal.FILEHEADER ReadFileHeader(IO.Reader br)
		{
			Internal.FILEHEADER fileHeader = new Internal.FILEHEADER();
			fileHeader.ReservedSpace = br.ReadInt32();
			int UsedSpace = br.ReadInt32();
			fileHeader.FileFlags = br.ReadByte();
			fileHeader.FileContent = br.ReadBytes(UsedSpace);
			fileHeader.FreeSpace = new byte[fileHeader.ReservedSpace - UsedSpace - 9];
			return fileHeader;
		}
		private Internal.DIRECTORYLEAFENTRY ReadDirectoryLeafEntry(IO.Reader br)
		{
			Internal.DIRECTORYLEAFENTRY entry = new Internal.DIRECTORYLEAFENTRY();
			entry.FileName = br.ReadNullTerminatedString();
			entry.FileOffset = br.ReadInt32();
			return entry;
		}
		private Internal.BTREENODEHEADER ReadBTreeNodeHeader(IO.Reader br)
		{
			Internal.BTREENODEHEADER nodeHeader = new Internal.BTREENODEHEADER();
			nodeHeader.UnusedByteCount = br.ReadUInt16();
			nodeHeader.PageEntriesCount = br.ReadInt16();
			nodeHeader.PreviousPageIndex = br.ReadInt16();
			nodeHeader.NextPageIndex = br.ReadInt16();
			return nodeHeader;
		}
		private Internal.DIRECTORYINDEXENTRY ReadDirectoryIndexEntry(IO.Reader br)
		{
			Internal.DIRECTORYINDEXENTRY entry = new Internal.DIRECTORYINDEXENTRY();
			entry.FileName = br.ReadNullTerminatedString();
			entry.PageNumber = br.ReadInt16();
			return entry;
		}
		private Internal.BTREEINDEXHEADER ReadBTreeIndexHeader(IO.Reader br)
		{
			Internal.BTREEINDEXHEADER bTreeIndexHeader = new Internal.BTREEINDEXHEADER();
			bTreeIndexHeader.UnusedByteCount = br.ReadUInt16();
			bTreeIndexHeader.PageEntriesCount = br.ReadInt16();
			bTreeIndexHeader.PreviousPageIndex = br.ReadInt16();
			return bTreeIndexHeader;
		}
		#endregion
		#endregion
		#region Save
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer _bw = base.Accessor.Writer;
			#region Header
			_bw.WriteInt32((int)0x00035F3F);

			int directoryStart = 0;
			_bw.WriteInt32(directoryStart); // offset of FILEHEADER of internal directory
			int firstFreeBlock = -1; // offset of FREEHEADER or -1L if no free list
			_bw.WriteInt32(firstFreeBlock);
			#endregion
			#region Content
			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);

			Internal.FILEHEADER internalFileHeader = new Internal.FILEHEADER();
			internalFileHeader.FileFlags = 4;

			MemoryAccessor maFile = new MemoryAccessor();
			IO.Writer bwFile = new IO.Writer(maFile);
			bwFile.WriteUInt16((ushort)0x293B);

			ushort flags = 0;                         // bit 0x0002 always 1, bit 0x0400 1 if directory
			bw.WriteUInt16(flags);

			ushort PageSize = 0x0400;                      // 0x0400=1k if directory, 0x0800=2k else, or 4k
			bw.WriteUInt16(PageSize);

			string Structure = "z4\0\0\0\0\0\0\0\0\0\0\0\0\0\0";        // string describing format of data
			bw.WriteFixedLengthString(Structure);

			short MustBeZero = 0;
			bw.WriteInt16(MustBeZero);

			short PageSplits = 0;                      // number of page splits B+ tree has suffered
			bw.WriteInt16(PageSplits);

			short RootPage = 0;                        // page number of B+ tree root page
			bw.WriteInt16(RootPage);

			short MustBeNegOne = -1;                    // 0xFFFF
			bw.WriteInt16(MustBeNegOne);

			short TotalPages = 1;                      // number of B+ tree pages
			bw.WriteInt16(TotalPages);
			short NLevels = 1;                         // number of levels of B+ tree
			bw.WriteInt16(NLevels);

			int TotalBPlusTreeEntries = 0;             // number of entries in B+ tree

			/*
			for (int i = 0; i < NLevels - 1; i++)
			{
				#region Index page
				// If NLevel is greater than 1, RootPage is the page number of an index-page.
				// Index-pages start with a BTREEINDEXHEADER and are followed by an array of
				// BTREEINDEX structures, in case of the internal directory containing pairs
				// of FileNames and PageNumbers.

				// (STRINGZ is a NUL-terminated string, sizeof(STRINGZ) is strlen(string)+1).
				// PageNumber gets you to the next page containing entries lexically starting
				// at FileName, but less than the next FileName. PreviousPage gets you to the
				// next page if the desired FileName is lexically before the first FileName.
				Internal.BTREEINDEXHEADER indexHeader = ReadBTreeIndexHeader(brFile);
				for (short j = 0; j < indexHeader.PageEntriesCount; j++)
				{
					// this is the structure of directory index-pages
					Internal.DIRECTORYINDEXENTRY entry = ReadDirectoryIndexEntry(brFile);
				}
				#endregion
			}

			// After NLevels-1 of index-pages you will reach a leaf-page starting with a
			// BTREENODEHEADER followed by an array of BTREELEAF structures, in case of the
			// internal directory containing pairs of FileNames and FileOffsets.
			// You may follow the PreviousPage entry in all NLevels-1 index-pages to reach
			// the first leaf-page, then iterate thru all entries and use NextPage to
			// follow the double linked list of leaf-pages until NextPage is -1 to retrieve
			// a sorted list of all TotalBtreeEntries entries contained in the B+ tree.
			Internal.BTREENODEHEADER nodeHeader = ReadBTreeNodeHeader(brFile);
			List<Internal.DIRECTORYLEAFENTRY> entries = new List<Internal.DIRECTORYLEAFENTRY>();
			for (short i = 0; i < nodeHeader.PageEntriesCount; i++)
			{
				// this is the structure of directory index-pages
				Internal.DIRECTORYLEAFENTRY entry = ReadDirectoryLeafEntry(brFile);
				entries.Add(entry);
			}
			*/

			WriteFileHeader(bw, internalFileHeader);



			#endregion
			#region Footer
			byte[] data = ma.ToArray();
			_bw.WriteInt32(data.Length);
			_bw.WriteBytes(data);

			_bw.Flush();
			#endregion
		}
		#region Internal Methods
		private void WriteFileHeader(IO.Writer bw, Internal.FILEHEADER value)
		{
			bw.WriteInt32(value.ReservedSpace);
			if (value.FileContent == null)
			{
				bw.WriteInt32((int)0);
			}
			else
			{
				bw.WriteInt32(value.FileContent.Length);
			}
			bw.WriteByte(value.FileFlags);
			if (value.FileContent != null)
			{
				bw.WriteBytes(value.FileContent);
			}
		}
		private void WriteDirectoryLeafEntry(IO.Writer bw, Internal.DIRECTORYLEAFENTRY value)
		{
			bw.WriteNullTerminatedString(value.FileName);
			bw.WriteInt32(value.FileOffset);
		}
		private void WriteBTreeNodeHeader(IO.Writer bw, Internal.BTREENODEHEADER value)
		{
			bw.WriteUInt16(value.UnusedByteCount);
			bw.WriteInt16(value.PageEntriesCount);
			bw.WriteInt16(value.PreviousPageIndex);
			bw.WriteInt16(value.NextPageIndex);
		}
		private void WriteDirectoryIndexEntry(IO.Writer bw, Internal.DIRECTORYINDEXENTRY value)
		{
			bw.WriteNullTerminatedString(value.FileName);
			bw.WriteInt16(value.PageNumber);
		}
		private void WriteBTreeIndexHeader(IO.Writer bw, Internal.BTREEINDEXHEADER value)
		{
			bw.WriteUInt16(value.UnusedByteCount);
			bw.WriteInt16(value.PageEntriesCount);
			bw.WriteInt16(value.PreviousPageIndex);
		}
		#endregion
		#endregion
	}
}
