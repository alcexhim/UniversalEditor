//
//  MNGDataFormat.cs - provides a DataFormat for manipulating archives in Hostile Waters MNG format
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

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Hostile Waters MNG format.
	/// </summary>
	public class MNGDataFormat : DataFormat
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
			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadNullTerminatedString();
				uint compressedSize = br.ReadUInt32();
				uint decompressedSize = br.ReadUInt32();
				uint offset = br.ReadUInt32();

				File file = new File();
				file.Name = fileName;
				file.Size = decompressedSize;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", offset);

				file.Properties.Add("CompressedSize", compressedSize);
				file.Properties.Add("DecompressedSize", decompressedSize);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["CompressedSize"];
			uint offset = (uint)file.Properties["offset"];
			uint decompressedLength = (uint)file.Properties["DecompressedSize"];

			br.Accessor.Position = offset;
			byte[] compressedData = br.ReadBytes(length);
			byte[] decompressedData = UniversalEditor.Compression.CompressionModules.Zlib.Decompress(compressedData);
			if (decompressedData.Length != decompressedLength) throw new InvalidOperationException("Decompressed file size mismatch");

			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt32((uint)fsom.Files.Count);

			uint offset = 0;
			foreach (File file in fsom.Files)
			{
				offset += (uint)(file.Name.Length + 13);
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteNullTerminatedString(file.Name);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = UniversalEditor.Compression.CompressionModules.Zlib.Compress(decompressedData);
				bw.WriteUInt32((uint)compressedData.Length);
				bw.WriteUInt32((uint)decompressedData.Length);
				bw.WriteUInt32(offset);
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}
			bw.Flush();
		}
	}
}
