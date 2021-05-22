//
//  OLE1DataFormat.cs - provides a DataFormat for manipulating Microsoft Object Linking and Embedding (OLE) version 1 files
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
using System.Collections.Generic;
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.OLE.OLE1
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Microsoft Object Linking and Embedding (OLE) version 1 files.
	/// </summary>
	public class OLE1DataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(ProgramName), "_Program name"));
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(OriginalFileName), "Original file _name"));
			}
			return _dfr;
		}

		public string ProgramName { get; set; }
		public string OriginalFileName { get; set; }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			OLE1ObjectHeader chunk = ReadOLE1ObjectHeader(reader);
			switch (chunk.Type)
			{
				case OLE1ChunkType.EmbeddedObject:
				{
					uint nativeDataSize = reader.ReadUInt32();

					// blah?
					ushort x0200 = reader.ReadUInt16();
					byte nul = reader.ReadByte();
					string w = reader.ReadNullTerminatedString();
					uint unkn2 = reader.ReadUInt32();
					uint originalFileNameLen = reader.ReadUInt32();
					string originalFileName = reader.ReadFixedLengthString(originalFileNameLen);

					uint nativeDataLen = reader.ReadUInt32();
					byte[] nativeData = reader.ReadBytes(nativeDataLen);

					reader.Seek(nativeDataSize + 40, SeekOrigin.Begin);
					fsom.Files.Add("NATIVEDATA", nativeData);
					break;
				}
				case OLE1ChunkType.LinkedObject:
				{
					// networkname
					uint unknown4 = reader.ReadUInt32(); // 0

					// reserved
					uint unknown5 = reader.ReadUInt32(); // 0

					// linkupdateoption
					uint unknown6 = reader.ReadUInt32(); // 0
					break;
				}
			}

			OLE1PresentationObject chunk2 = ReadOLE1PresentationObject(reader);
			fsom.Files.Add(System.IO.Path.GetFileName(chunk.TopicName.Replace('\\', System.IO.Path.DirectorySeparatorChar)), chunk2.Data);
		}

		private OLE1ObjectHeader ReadOLE1ObjectHeader(Reader reader)
		{
			OLE1ObjectHeader header = new OLE1ObjectHeader();
			uint oleversion = reader.ReadUInt32(); // 1281
			header.Type = (OLE1ChunkType)reader.ReadUInt32(); // 01, 02, 05

			uint classNameLength = reader.ReadUInt32(); // length of program name + null terminator
			string className = reader.ReadFixedLengthString(classNameLength);
			header.ClassName = className.TrimNull();

			uint topicNameLength = reader.ReadUInt32(); // incl. null terminator
			string topicName = reader.ReadFixedLengthString(topicNameLength);
			header.TopicName = topicName.TrimNull();

			uint itemNameLength = reader.ReadUInt32(); // 0
			string itemName = reader.ReadFixedLengthString(itemNameLength);
			header.ItemName = itemName.TrimNull();
			return header;
		}
		private OLE1PresentationObject ReadOLE1PresentationObject(Reader reader)
		{
			OLE1PresentationObject header = new OLE1PresentationObject();
			uint oleversion = reader.ReadUInt32(); // 1281
			uint flags = reader.ReadUInt32();
			if (flags == 0x00000005)
			{
				uint classNameLength = reader.ReadUInt32();
				header.ClassName = reader.ReadFixedLengthString(classNameLength).TrimNull();
			}
			else
			{
				header.ClassName = null;
			}

			header.Width = reader.ReadUInt32();
			header.Height = (uint)(reader.ReadInt32() * -1); // the fuck?

			uint datasize = reader.ReadUInt32();
			if (header.ClassName == "METAFILEPICT")
			{
				datasize -= 8;
				ushort reserved1 = reader.ReadUInt16();
				ushort reserved2 = reader.ReadUInt16();
				ushort reserved3 = reader.ReadUInt16();
				ushort reserved4 = reader.ReadUInt16();
			}

			header.Data = reader.ReadBytes(datasize);
			return header;
		}

		private void WriteOLE1ObjectHeader(Writer writer, OLE1ObjectHeader value)
		{
			writer.WriteUInt32(1281); // ole version
			writer.WriteUInt32((uint)value.Type);

			writer.WriteUInt32((uint)(value.ClassName.Length + 1));
			writer.WriteNullTerminatedString(value.ClassName);
			writer.WriteUInt32((uint)(value.TopicName.Length + 1));
			writer.WriteNullTerminatedString(value.TopicName);
			writer.WriteUInt32((uint)(value.ItemName.Length + 1));
			writer.WriteNullTerminatedString(value.ItemName);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			OLE1ObjectHeader header = new OLE1ObjectHeader();
			WriteOLE1ObjectHeader(writer, header);

			switch (header.Type)
			{
				case OLE1ChunkType.LinkedObject:
				{
					// networkname
					writer.WriteUInt32(0);
					// reserved
					writer.WriteUInt32(0);
					// linkupdateoption
					writer.WriteUInt32(0);
					break;
				}
			}
		}
	}
}
