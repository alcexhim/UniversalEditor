//
//  ARCDataFormat.cs - provides a DataFormat for manipulating archives in ARC format
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

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ARC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in ARC format.
	/// </summary>
	public class ARCDataFormat : DataFormat
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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			br.Endianness = IO.Endianness.LittleEndian;
			byte magic = br.ReadByte();
			if (magic != 0x1A) throw new InvalidDataFormatException("File does not begin with 0x1A");

			byte compressionMethod = br.ReadByte();
			string fileName = br.ReadNullTerminatedString(12);
			int compressedFileSize = br.ReadInt32();
			int fileDateMSDOS = br.ReadInt32();
			short crc16 = br.ReadInt16();
			int decompressedFileSize = br.ReadInt32();
			switch (compressionMethod)
			{
				case 20:
				case 0x15:
				case 0x16:
				case 0x17:
				case 0x18:
				case 0x19:
				case 0x1a:
				case 0x1b:
				case 0x1c:
				case 0x1d:
				{
					byte headerLength = br.ReadByte();
					byte headerType = br.ReadByte();
					byte headerSubType = br.ReadByte();
					byte[] data = br.ReadBytes((uint)headerLength);
					switch (headerType)
					{
						case 20:
						{
							break;
						}
						case 0x15:
						{
							break;
						}
					}
					break;
				}
			}
			byte[] compressedData = br.ReadBytes(compressedFileSize);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteByte((byte)0x1a);
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				byte compressionMethod = 1;
				bw.WriteByte(compressionMethod);
				bw.WriteFixedLengthString(fsom.Files[i].Name, 12);
				byte[] decompressedData = fsom.Files[i].GetData();
				byte[] compressedData = null;
				switch (compressionMethod)
				{
					case 1:
					{
						compressedData = decompressedData;
						break;
					}
					case 20:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
					case 0x1b:
					case 0x1c:
					case 0x1d:
					{
						byte headerLength = 0;
						bw.WriteByte(headerLength);
						byte headerType = 0;
						bw.WriteByte(headerType);
						byte headerSubType = 0;
						bw.WriteByte(headerSubType);
						byte[] data = new byte[headerLength];
						bw.WriteBytes(data);
						switch (headerType)
						{
							case 20:
							{
								break;
							}
							case 0x15:
							{
								break;
							}
						}
						break;
					}
				}
				int compressedFileSize = compressedData.Length;
				bw.WriteInt32(compressedFileSize);
				int fileDateMSDOS = 0;
				bw.WriteInt32(fileDateMSDOS);
				short crc16 = 0;
				bw.WriteInt16(crc16);
				int decompressedFileSize = decompressedData.Length;
				bw.WriteInt32(decompressedFileSize);
				bw.WriteBytes(compressedData);
			}
		}
	}
}
