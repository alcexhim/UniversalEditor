//
//  SPISDataFormat.cs - provides a DataFormat for manipulating archives in SPIS format
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

using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SPIS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in SPIS format.
	/// </summary>
	public class SPISDataFormat : DataFormat
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
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			while (!br.EndOfStream)
			{
				string header1 = br.ReadFixedLengthString(4);
				if (header1 != "SPIS") throw new InvalidDataFormatException("File does not begin with SPIS");

				byte header2 = br.ReadByte();
				if (header2 != 26) throw new InvalidDataFormatException("File does not begin with SPIS, 26");

				string header3 = br.ReadFixedLengthString(3);
				if (header3 != "LZH") throw new InvalidDataFormatException("File does not begin with SPIS, 26, LZH");

				uint decompressedSize = br.ReadUInt32();
				uint unknown1 = br.ReadUInt32();
				byte[] unknown2 = br.ReadBytes(5);
				ushort fileNameLength = br.ReadUInt16();
				ushort unknown3 = br.ReadUInt16();
				uint unknown4 = br.ReadUInt32();
				uint decompressedSize1 = br.ReadUInt32();
				uint compressedSize = br.ReadUInt32();
				byte unknown5 = br.ReadByte(); // 2 
				uint unknown6 = br.ReadUInt32();
				uint unknown7 = br.ReadUInt32(); // 0
				string filename = br.ReadNullTerminatedString();
				byte[] compressedData = br.ReadBytes(compressedSize);

				byte[] decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.LZH).Decompress(compressedData);

				fsom.Files.Add(filename, decompressedData);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString("SPIS");
				bw.WriteByte((byte)26);
				bw.WriteFixedLengthString("LZH");

				uint decompressedSize = (uint)file.Size;
				bw.WriteUInt32(decompressedSize);

				uint unknown1 = 0;
				bw.WriteUInt32(unknown1);

				byte[] unknown2 = new byte[5];
				bw.WriteBytes(unknown2);

				ushort fileNameLength = (ushort)file.Name.Length;
				bw.WriteUInt16(fileNameLength);

				ushort unknown3 = 0;
				bw.WriteUInt16(unknown3);

				uint unknown4 = 0;
				bw.WriteUInt32(unknown4);

				bw.WriteUInt32(decompressedSize);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;

				bw.WriteUInt32((uint)compressedData.Length);

				byte unknown5 = 2; // 2 
				bw.WriteByte(unknown5);

				uint unknown6 = 0;
				bw.WriteUInt32(unknown6);

				uint unknown7 = 0;
				bw.WriteUInt32(unknown7);

				bw.WriteNullTerminatedString(file.Name);
				bw.WriteBytes(compressedData);
			}
		}
	}
}
