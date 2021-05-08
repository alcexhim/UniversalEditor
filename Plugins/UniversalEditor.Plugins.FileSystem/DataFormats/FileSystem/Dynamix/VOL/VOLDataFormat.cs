//
//  VOLDataFormat.cs - provides a DataFormat for manipulating archives in Dynamix VOL format
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Dynamix.VOL
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Dynamix VOL format.
	/// </summary>
	public class VOLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ID = new Guid("{7AB0E953-243D-4DA4-BC2F-766CF0F5168A}");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string PVOL = reader.ReadFixedLengthString(4);
			if (PVOL != "PVOL") throw new InvalidDataFormatException();

			uint offsetToVOLSChunk = reader.ReadUInt32();

			base.Accessor.Seek(offsetToVOLSChunk, SeekOrigin.Begin);

			string VOLS = reader.ReadFixedLengthString(4);
			if (VOLS != "vols") throw new InvalidDataFormatException("Missing or corrupted 'vols' chunk");

			uint volsLength = reader.ReadUInt32();
			byte[] volsData = reader.ReadBytes(volsLength);
			MemoryAccessor volsAccessor = new MemoryAccessor(volsData);
			Reader volsReader = new Reader(volsAccessor);

			string VOLI = reader.ReadFixedLengthString(4);
			if (VOLI != "voli") throw new InvalidDataFormatException("Missing or corrupted 'voli' chunk");

			uint voliLength = reader.ReadUInt32();
			uint fileCount = (uint)(voliLength / 17);

			for (uint i = 0; i < fileCount; i++)
			{
				uint unknown = reader.ReadUInt32();
				uint fileNameOffset = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();
				uint length = reader.ReadUInt32();
				byte nul = reader.ReadByte();

				volsReader.Seek(fileNameOffset, SeekOrigin.Begin);
				string fileName = volsReader.ReadNullTerminatedString();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.Properties.Add("reader", reader);
				file.Size = length;
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			reader.Seek(offset, SeekOrigin.Begin);

			string VBLK = reader.ReadFixedLengthString(4);
			if (VBLK != "VBLK") throw new InvalidDataFormatException("Data chunk does not begin with 'VBLK'");

			byte unknown1 = reader.ReadByte();
			ushort unknown2 = reader.ReadUInt16(); // middle two bytes of file length??
			byte unknown3 = reader.ReadByte(); // attributes?? set to 0x80

			e.Data = reader.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("PVOL");

			File[] files = fsom.GetAllFiles();

			uint offsetToVOLSChunk = 8;
			foreach (File file in files)
			{
				// assuming one VBLK per file
				offsetToVOLSChunk += 8;
				offsetToVOLSChunk += (uint)file.Size;
			}
			writer.WriteUInt32(offsetToVOLSChunk);

			foreach (File file in files)
			{
				writer.WriteFixedLengthString("VBLK");
				writer.WriteUInt16((ushort)file.Size);
				writer.WriteUInt16(32768);
				writer.WriteBytes(file.GetData());
			}

			#region VOLS chunk
			{
				writer.WriteFixedLengthString("vols");

				uint volsChunkSize = 0;
				foreach (File file in files)
				{
					// vols is file names, an array of null-terminated strings
					volsChunkSize += (uint)(file.Name.Length + 1);
				}
				writer.WriteUInt32(volsChunkSize);

				foreach (File file in files)
				{
					writer.WriteNullTerminatedString(file.Name);
				}
			}
			#endregion
			#region VOLI chunk
			{
				writer.WriteFixedLengthString("voli");

				uint voliChunkSize = 0;
				if (files.Length > 0)
				{
					foreach (File file in files)
					{
						voliChunkSize += 17;
					}
				}
				writer.WriteUInt32(voliChunkSize);

				if (files.Length > 0)
				{
					// first VBLK entry ALWAYS starts at offset 8
					// (4 for the PVOL signature + 4 for the offset to vols chunk)
					uint offset = 8;

					uint fileNameOffset = 0;

					foreach (File file in files)
					{
						// unknown
						writer.WriteUInt32(0);

						// offset into string table for the file name
						writer.WriteUInt32(fileNameOffset);

						// offset of first VBLK entry for the file
						writer.WriteUInt32(offset);
						// file size
						writer.WriteUInt32((uint)file.Size);

						offset += ((uint)file.Size + 8);

						// nul byte at end of each entry in VOLI chunk... why??
						writer.WriteByte(0);

						fileNameOffset += (uint)(file.Name.Length + 1);
					}
				}
				else
				{
					writer.WriteUInt32(0);
					writer.WriteUInt32(0);
				}
			}
			#endregion
		}
	}
}
