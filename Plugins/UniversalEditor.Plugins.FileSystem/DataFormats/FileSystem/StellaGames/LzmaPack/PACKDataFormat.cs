//
//  PACKDataFormat.cs - provides a DataFormat for manipulating archives in Stella Games LzmaPack format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.StellaGames.LzmaPack
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Stella Games LzmaPack format.
	/// </summary>
	public class PACKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=Stella_Games_LzmaPack");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			byte lzmaDecoderPropertiesSize = reader.ReadByte();
			byte[] lzmaDecoderProperties = reader.ReadBytes(lzmaDecoderPropertiesSize);
			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadLengthPrefixedString();
				long decompressedSize = reader.ReadInt64();
				long compressedSize = reader.ReadInt64();
				long offset = reader.ReadInt64();
				PACKFileAttributes fileAttributes = (PACKFileAttributes)reader.ReadUInt16();

				File file = fsom.AddFile(fileName);
				file.Size = decompressedSize;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressedSize", compressedSize);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			long CompressedSize = (long)file.Properties["CompressedSize"];
			long DecompressedSize = (long)file.Properties["DecompressedSize"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(CompressedSize);
			byte[] decompressedData = compressedData;
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();

			byte lzmaDecoderPropertiesSize = 0;
			byte[] lzmaDecoderProperties = new byte[lzmaDecoderPropertiesSize];
			writer.WriteByte(lzmaDecoderPropertiesSize);
			writer.WriteBytes(lzmaDecoderProperties);

			long offset = 1 + lzmaDecoderPropertiesSize + 4;
			foreach (File file in files)
			{
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
				bw.Write(file.Name);
				bw.Close();
				offset += ms.ToArray().Length;
				offset += 26;
			}

			writer.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				writer.WriteLengthPrefixedString(file.Name);
				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;

				writer.WriteInt64((long)decompressedData.Length);
				writer.WriteInt64((long)compressedData.Length);
				writer.WriteInt64(offset);

				PACKFileAttributes fileAttributes = PACKFileAttributes.None;
				writer.WriteUInt16((ushort)fileAttributes);

				offset += compressedData.Length;
			}
		}
	}
}
