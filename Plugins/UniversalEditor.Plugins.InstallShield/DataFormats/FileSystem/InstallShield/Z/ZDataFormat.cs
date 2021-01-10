//
//  ZDataFormat.cs - provides a DataFormat for manipulating InstallShield Z archive files
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

namespace UniversalEditor.DataFormats.FileSystem.InstallShield.Z
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating InstallShield Z archive files.
	/// </summary>
	public class ZDataFormat : DataFormat
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			Reader reader = base.Accessor.Reader;
			
			uint signature1 = reader.ReadUInt32();
			uint signature2 = reader.ReadUInt32();
			if (!(signature1 == 0x8C655D13 && signature2 == 0x0002013A))
			{
				throw new InvalidDataFormatException("File does not begin with { 0x13, 0x5D, 0x65, 0x8C, 0x3A, 0x01, 0x02, 0x00 }");
			}
				
			uint unknown3 = reader.ReadUInt32();
			ushort fileCount = reader.ReadUInt16();				//	07 (old)	432 (new)
			uint unknown4 = reader.ReadUInt32();
			
			uint archiveLength = reader.ReadUInt32();

			uint something = reader.ReadUInt32();
			uint unknown5 = reader.ReadUInt32();					//				255
			uint unknown6 = reader.ReadUInt32();
			uint unknown7 = reader.ReadUInt32();
			ushort unknown8 = reader.ReadUInt16();
			byte unknown9 = reader.ReadByte();
			uint headerOffset = reader.ReadUInt32();
			uint formatVersion = reader.ReadUInt32();


			ushort folderNameCount = reader.ReadUInt16();

			List<string> names = new List<string>();

			reader.Accessor.Seek(headerOffset, SeekOrigin.Begin);

			for (ushort i = 0; i < folderNameCount; i++)
			{
				ushort a1 = reader.ReadUInt16();							// 3
				ushort a2 = reader.ReadUInt16();							// 16
				ushort nameLength = reader.ReadUInt16();
				string name = reader.ReadFixedLengthString(nameLength);

				uint unknownB1 = reader.ReadUInt32();						// 0
				byte nul = reader.ReadByte();								// 0
				
				names.Add(name);											// WIN32
			}

			byte unknownB1X = reader.ReadByte();							// 0
			
			for (ushort i = 0; i < fileCount; i++)
			{
				ushort folderNameIndex = reader.ReadUInt16();
				uint decompressedLength = reader.ReadUInt32();
				uint compressedLength = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();
				ushort unknownB4A = reader.ReadUInt16();				// 155197798
				ushort unknownB4B = reader.ReadUInt16();				// 155197798
				uint unknownB5 = reader.ReadUInt32();				// 32
				ushort unknownB6 = reader.ReadUInt16();				// 55
				uint unknownB7 = reader.ReadUInt32();				// 16

				string fileName = reader.ReadLengthPrefixedString();

				uint unknownB8 = reader.ReadUInt32();				// ???
				uint unknownB9 = reader.ReadUInt32();				// ???
				uint unknownB10 = reader.ReadUInt32();				// ???
				ushort unknownB11A = reader.ReadUInt16();

				fileName = names[folderNameIndex] + "/" + fileName;
				if (fileName.StartsWith("/")) fileName = fileName.Substring(1);

				File file = fsom.AddFile(fileName);
				file.Size = decompressedLength;
				file.Properties.Add("reader", reader);
				file.Properties.Add("CompressedLength", compressedLength);
				file.Properties.Add("DecompressedLength", decompressedLength);
				file.Properties.Add("offset", offset);
				file.DataRequest += file_DataRequest;
			}
		}

		private UniversalEditor.Compression.Modules.Explode.ExplodeCompressionModule comp = new UniversalEditor.Compression.Modules.Explode.ExplodeCompressionModule();

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			uint offset = (uint)file.Properties["offset"];
			uint CompressedLength = (uint)file.Properties["CompressedLength"];
			uint DecompressedLength = (uint)file.Properties["DecompressedLength"];

			base.Accessor.Reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = base.Accessor.Reader.ReadBytes(CompressedLength);
			byte[] decompressedData = compressedData;
			if (CompressedLength != DecompressedLength)
			{
				decompressedData = comp.Decompress(compressedData);
			}
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
