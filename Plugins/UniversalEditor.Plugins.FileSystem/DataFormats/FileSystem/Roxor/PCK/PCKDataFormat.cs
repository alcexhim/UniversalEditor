//
//  PCKDataFormat.cs - provides a DataFormat for manipulating archives in Roxor In the Groove PCK format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Roxor.PCK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Roxor In the Groove PCK format.
	/// </summary>
	public class PCKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Comment), "_Comment: ", String.Empty, 128));
			}
			return _dfr;
		}

		public string Comment { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "PCKF") throw new InvalidDataFormatException("File does not begin with 'PCKF'");

			Comment = reader.ReadFixedLengthString(128, Encoding.Windows1252);
			Comment = Comment.TrimNull();

			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				uint compressedlength = reader.ReadUInt32();
				uint decompressedLength = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();
				uint fileNameLength = reader.ReadUInt32();
				bool compressed = (reader.ReadUInt32() != 0);

				string fileName = reader.ReadFixedLengthString(fileNameLength, Encoding.Windows1252);

				File file = fsom.AddFile(fileName);
				file.Size = decompressedLength;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("compressed", compressed);
				file.Properties.Add("CompressedLength", compressedlength);
				file.Properties.Add("DecompressedLength", decompressedLength);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint compressedLength = (uint)file.Properties["CompressedLength"];
			uint decompressedLength = (uint)file.Properties["DecompressedLength"];
			bool compressed = (bool)file.Properties["compressed"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedLength);
			byte[] decompressedData = compressedData;
			if (compressed)
			{

			}

			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("PCKF");
			writer.WriteFixedLengthString(Comment, Encoding.Windows1252, 128);

			File[] files = fsom.GetAllFiles();
			writer.WriteUInt32((uint)files.Length);

			uint offset = 136;
			foreach (File file in files)
			{
				offset += (uint)(20 + file.Name.Length);
			}

			byte[][] compressedDatas = new byte[files.Length][];
			for (int i = 0; i < files.Length; i++)
			{
				File file = files[i];
				bool compressed = false;
				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;
				compressedDatas[i] = compressedData;

				writer.WriteUInt32((uint)compressedData.Length);
				writer.WriteUInt32((uint)decompressedData.Length);
				writer.WriteUInt32(offset);
				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteUInt32((uint)(compressed ? 1 : 0));
				writer.WriteFixedLengthString(file.Name, Encoding.Windows1252, (uint)file.Name.Length);

				offset += (uint)file.Size;
			}
			for (int i = 0; i < files.Length; i++)
			{
				writer.WriteBytes(compressedDatas[i]);
			}
			writer.Flush();
		}
	}
}
