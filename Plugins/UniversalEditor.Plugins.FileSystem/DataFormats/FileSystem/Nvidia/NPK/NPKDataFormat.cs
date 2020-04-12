//
//  NPKDataFormat.cs - provides a DataFormat for manipulating archives in Nvidia NPK format
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

using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Nvidia.NPK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Nvidia NPK format.
	/// </summary>
	public class NPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionText(nameof(EncryptionKey), "Encryption _key: ", "bogomojo"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(Encrypted), "_Encrypt the data with the specified key"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(EncryptionKey), "Encryption _key: ", "bogomojo"));
			}
			return _dfr;
		}

		/// <summary>
		/// Determines whether the archive is to be encrypted with the key specified in the <see cref="EncryptionKey" /> property.
		/// </summary>
		public bool Encrypted { get; set; } = false;
		/// <summary>
		/// The key with which to encrypt the files in the archive when <see cref="Encrypted" /> is true.
		/// </summary>
		public string EncryptionKey { get; set; } = "bogomojo";

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;
			byte[] signature = br.ReadBytes(4);
			if (!signature.Match(new byte[] { 0xBE, 0xEF, 0xCA, 0xFE }))
			{
				throw new InvalidDataFormatException("File does not begin with 0xBE, 0xEF, 0xCA, 0xFE");
			}

			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				uint fileNameLength = br.ReadUInt32();
				string fileName = br.ReadFixedLengthString(fileNameLength);
				long dateTime = br.ReadInt64();
				long fileOffset = br.ReadInt64();
				long fileCompressedSize = br.ReadInt64();
				long fileDecompressedSize = br.ReadInt64();

				File file = fsom.AddFile(fileName);
				file.Size = fileDecompressedSize;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", fileOffset);
				file.Properties.Add("CompressedSize", fileCompressedSize);
				file.Properties.Add("DecompressedSize", fileDecompressedSize);
				file.DataRequest += file_DataRequest;
			}

		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			long compressedSize = (long)file.Properties["CompressedSize"];
			long decompressedSize = (long)file.Properties["DecompressedSize"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Position = offset;
			byte[] key = System.Text.Encoding.ASCII.GetBytes(EncryptionKey);
			byte[] compressedData = br.ReadBytes((ulong)compressedSize);
			byte[] decompressedData = null;
			try
			{
				decompressedData = CompressionModules.Zlib.Decompress(compressedData);
			}
			catch
			{
				// data invalid; file may be encrypted

				// simple XOR "decryption" method
				int j = 0;
				for (int i = 0; i < compressedData.Length; i++)
				{
					compressedData[i] ^= key[j];
					j++;
					if (j >= key.Length) j = 0;
				}

				try
				{
					decompressedData = CompressionModules.Zlib.Decompress(compressedData);
				}
				catch
				{
					decompressedData = compressedData;
				}
			}

			if (decompressedData.Length != decompressedSize)
			{
				Array.Resize(ref decompressedData, (int)decompressedSize);
			}
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			IO.Writer writer = base.Accessor.Writer;
			writer.WriteBytes(new byte[] { 0xBE, 0xEF, 0xCA, 0xFE });

			writer.WriteUInt32((uint)files.Length);

			byte[][] compressedDatas = new byte[files.Length][];

			long offset = 8;
			for (int i = 0; i < files.Length; i++)
			{
				offset += 4;
				offset += files[i].Name.Length;
				offset += 32;
			}

			for (int i = 0; i < files.Length; i++)
			{
				File file = files[i];
				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteFixedLengthString(file.Name);

				long dateTime = 0;
				writer.WriteInt64(dateTime);
				writer.WriteInt64(offset);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = CompressionModules.Zlib.Compress(decompressedData);

				if (Encrypted)
				{
					byte[] key = System.Text.Encoding.ASCII.GetBytes(EncryptionKey);
					// simple XOR "decryption" method
					int k = 0;
					for (int j = 0; j < compressedData.Length; j++)
					{
						compressedData[j] ^= key[k];
						k++;
						if (k >= key.Length) k = 0;
					}
				}

				compressedDatas[i] = compressedData;

				writer.WriteInt64(compressedData.Length);
				writer.WriteInt64(decompressedData.Length);

				offset += compressedData.Length;
			}

			for (int i = 0; i < files.Length; i++)
			{
				writer.WriteBytes(compressedDatas[i]);
			}
			writer.Flush();
		}
	}
}
