//
//  WIMDataFormat.cs - provides a DataFormat to manipulate Windows Imaging Format (WIM) archives
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Windows Imaging Format (WIM) archives.
	/// </summary>
	public class WIMDataFormat : DataFormat
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			WIMArchiveHeader header = ReadWIMArchiveHeader(reader);

			reader.Seek((long)header.rhOffsetTable.liOffset, SeekOrigin.Begin);
			byte[] tblOffsetTable = reader.ReadBytes(header.rhOffsetTable.ullSize);

			Reader offsetTableReader = new Reader(new Accessors.MemoryAccessor(tblOffsetTable));
			List<WIMOffsetTableEntry> entries = new List<WIMOffsetTableEntry>();
			while (!offsetTableReader.EndOfStream)
			{
				WIMOffsetTableEntry entry = ReadWIMOffsetTableEntry(offsetTableReader);
				entries.Add(entry);
			}

			WIMOffsetTableEntry lastEntry = entries[entries.Count - 1];
			reader.Seek((long)lastEntry.liOffset, SeekOrigin.Begin);

			int padRest = (int)((long)lastEntry.liOffset % 8);

			Dictionary<byte[], WIMDirectoryEntry> direntries = new Dictionary<byte[], WIMDirectoryEntry>();

			#region SECURITYBLOCK_DISK
			{
				uint dwTotalLength = reader.ReadUInt32();
				uint dwNumEntries = reader.ReadUInt32();
				ulong[] liEntryLength = reader.ReadUInt64Array((int)dwNumEntries);
			}
			#endregion
			#region DIRENTRY
			{
				WIMDirectoryEntry entry = default(WIMDirectoryEntry);
				do
				{
					entry = ReadWIMDirectoryEntry(reader);

					if (!entry.Equals(default(WIMDirectoryEntry)))
					{
						direntries.Add(entry.bHash, entry);
						reader.Align(8, padRest);
					}
					else
					{
						reader.Align(8);
					}
				}
				while (!entry.Equals(default(WIMDirectoryEntry)));


				Dictionary<WIMDirectoryEntry, WIMOffsetTableEntry> offsetDictionary = new Dictionary<WIMDirectoryEntry, WIMOffsetTableEntry>();

				foreach (WIMOffsetTableEntry ent in entries)
				{
					foreach (KeyValuePair<byte[], WIMDirectoryEntry> kvp in direntries)
					{
						if (kvp.Key.Match(ent.bHash))
						{
							offsetDictionary.Add(kvp.Value, ent);
						}
					}
				}

				foreach (KeyValuePair<WIMDirectoryEntry, WIMOffsetTableEntry> kvp in offsetDictionary)
				{
					File file = fsom.AddFile(kvp.Key.FileName);
					file.Name = kvp.Key.FileName;
					file.Size = (long)kvp.Value.liOriginalSize;
					file.Properties.Add("offset", (long)kvp.Value.liOffset);
					file.Properties.Add("length", (long)kvp.Value.liOriginalSize);
					file.Properties.Add("reader", reader);
					file.DataRequest += file_DataRequest;
				}
			}
			#endregion

			#region Load XML data - we don't actually use this anywhere... yet
			{
				reader.Seek((long)header.rhXmlData.liOffset, SeekOrigin.Begin);
				string xmlData = reader.ReadFixedLengthString((long)header.rhXmlData.ullSize, Encoding.UTF16LittleEndian);

				UniversalEditor.ObjectModels.Markup.MarkupObjectModel mom = new ObjectModels.Markup.MarkupObjectModel();
				UniversalEditor.DataFormats.Markup.XML.XMLDataFormat xdf = new Markup.XML.XMLDataFormat();
				Document.Load(mom, xdf, new Accessors.StringAccessor(xmlData));

				fsom.SetCustomProperty<UniversalEditor.ObjectModels.Markup.MarkupObjectModel>(MakeReference(), "XMLDescriptor", mom);
			}
			#endregion
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			long length = (long)file.Properties["length"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(length);
			byte[] uncompressedData = compressedData;
			e.Data = uncompressedData;
		}

		private WIMDirectoryEntry ReadWIMDirectoryEntry(Reader lastEntryReader)
		{
			WIMDirectoryEntry item = new WIMDirectoryEntry();
			item.liLength = lastEntryReader.ReadUInt64();
			if (item.liLength == 0) return default(WIMDirectoryEntry);

			item.dwAttributes = lastEntryReader.ReadUInt32();
			item.dwSecurityId = lastEntryReader.ReadInt32();
			item.liSubdirOffset = lastEntryReader.ReadUInt64();
			item.liUnused1 = lastEntryReader.ReadUInt64();
			item.liUnused2 = lastEntryReader.ReadUInt64();
			item.liCreationTime = lastEntryReader.ReadUInt64();
			item.liLastAccessTime = lastEntryReader.ReadUInt64();
			item.liLastWriteTime = lastEntryReader.ReadUInt64();
			item.bHash = lastEntryReader.ReadBytes(20);
			item.dwReparseTag = lastEntryReader.ReadUInt32();
			item.dwReparseReserved = lastEntryReader.ReadUInt32();

			if ((item.dwAttributes & 16) == 16)
			{
				item.liHardLink = lastEntryReader.ReadUInt64();
			}
			else
			{
				item.liHardLink = lastEntryReader.ReadUInt32();
			}
			
			ushort wStreamCount = lastEntryReader.ReadUInt16();
			item.wStreams = new WIMStreamEntry[wStreamCount];

			item.wShortNameLength = lastEntryReader.ReadUInt16();
			ushort wFileNameLength = lastEntryReader.ReadUInt16();
			item.FileName = lastEntryReader.ReadFixedLengthString(wFileNameLength, Encoding.UTF16LittleEndian);
			for (ushort i = 0; i < wStreamCount; i++)
			{
				item.wStreams[i] = ReadWIMStreamEntry(lastEntryReader);
			}

			ulong realLength = (ulong)((8 * 10) + 20 + 4 + wFileNameLength);
			if (item.liLength != realLength)
			{

			}
			return item;
		}
		private WIMStreamEntry ReadWIMStreamEntry(Reader reader)
		{
			WIMStreamEntry item = new WIMStreamEntry();
			item.liLength = reader.ReadUInt64();
			item.Unused1 = reader.ReadUInt64();
			item.bHash = reader.ReadBytes(20);
			item.wStreamNameLength = reader.ReadUInt16();
			item.StreamName = reader.ReadFixedLengthString(item.wStreamNameLength, Encoding.UTF16LittleEndian);
			return item;
		}
		private WIMOffsetTableEntry ReadWIMOffsetTableEntry(Reader reader)
		{
			WIMResourceHeaderDiskShort reshdr = ReadWIMResourceHeaderDiskShort(reader);
			WIMOffsetTableEntry wim = new WIMOffsetTableEntry();
			wim.flags = reshdr.flags;
			wim.liOffset = reshdr.liOffset;
			wim.liOriginalSize = reshdr.liOriginalSize;
			wim.ullSize = reshdr.ullSize;

			wim.usPartNumber = reader.ReadUInt16();
			wim.dwRefCount = reader.ReadUInt32();
			wim.bHash = reader.ReadBytes(20);
			return wim;
		}
		private WIMArchiveHeader ReadWIMArchiveHeader(Reader reader)
		{
			WIMArchiveHeader header = new WIMArchiveHeader();

			header.magic = reader.ReadFixedLengthString(8);
			if (header.magic != "MSWIM\0\0\0") throw new InvalidDataFormatException("File does not begin with 'MSWIM', 0x00, 0x00, 0x00");

			header.cbSize = reader.ReadUInt32();
			header.dwVersion = reader.ReadUInt32();
			header.dwFlags = (WIMArchiveFlags)reader.ReadUInt32();
			header.dwCompressionSize = reader.ReadUInt32();
			header.gWIMGuid = reader.ReadGuid();
			header.usPartNumber = reader.ReadUInt16();
			header.usTotalParts = reader.ReadUInt16();
			header.dwImageCount = reader.ReadUInt32();
			header.rhOffsetTable = ReadWIMResourceHeaderDiskShort(reader);
			header.rhXmlData = ReadWIMResourceHeaderDiskShort(reader);
			header.rhBootMetadata = ReadWIMResourceHeaderDiskShort(reader);
			header.dwBootIndex = reader.ReadUInt32();
			header.rhIntegrity = ReadWIMResourceHeaderDiskShort(reader);
			header.bUnused = reader.ReadBytes(60);

			return header;
		}
		private WIMResourceHeaderDiskShort ReadWIMResourceHeaderDiskShort(Reader reader)
		{
			WIMResourceHeaderDiskShort item = new WIMResourceHeaderDiskShort();
			long sizeAndFlags = reader.ReadInt64();
			ulong size = (ulong)(sizeAndFlags << 8);
			size = (ulong)(size >> 8);
			byte flags = (byte)(sizeAndFlags >> 56);

			item.ullSize = size;
			item.flags = (WIMResourceHeaderDiskFlags)flags;
			item.liOffset = reader.ReadUInt64();
			item.liOriginalSize = reader.ReadUInt64();
			return item;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("MSWIM", 8);

			throw new NotImplementedException();
		}
	}
}
