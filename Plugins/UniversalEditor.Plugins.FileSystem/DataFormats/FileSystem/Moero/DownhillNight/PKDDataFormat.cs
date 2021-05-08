//
//  PKDDataFormat.cs - provides a DataFormat for manipulating archives in Moero Downhill Night PKD format
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

using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Moero.DownhillNight
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Moero Downhill Night PKD format.
	/// </summary>
	public class PKDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionNumber(nameof(EncryptionKey), "Encryption _key", 0xC5, 0, 255));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(EncryptionKey), "Encryption _key", 0xC5, 0, 255));
			}
			return _dfr;
		}

		/// <summary>
		/// The single-byte encryption key XORed with the file data.
		/// </summary>
		public byte EncryptionKey { get; set; } = 0xC5;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			if (signature != "PACK") throw new InvalidDataFormatException();

			byte[] encryptedData = br.ReadToEnd();
			for (int i = 0; i < encryptedData.Length; i++)
			{
				encryptedData[i] = (byte)(encryptedData[i] ^ EncryptionKey);
			}

			br = new IO.Reader(new MemoryAccessor(encryptedData));

			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(128).TrimNull();
				uint length = br.ReadUInt32();
				uint offset = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("reader", br);
				file.Properties.Add("length", length);
				file.Properties.Add("offset", offset);
				file.DataRequest += file_DataRequest;
				file.Size = length;
			}

			br.Close();
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];

			br.Accessor.Seek(offset, SeekOrigin.Begin);
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			// create a Writer in memory for the unencrypted data
			MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);


			File[] files = fsom.GetAllFiles();

			uint offset = 4;
			foreach (File file in files)
			{
				offset += 136;
			}

			bw.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				bw.WriteFixedLengthString(file.Name, 128);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32((uint)offset);
				offset += (uint)file.Size;
			}
			bw.Flush();
			bw.Close();

			// switch over to dataformat's Writer and encrypt the data
			bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PACK");
			byte[] data = ma.ToArray();
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = (byte)(data[i] ^ EncryptionKey);
			}
			bw.WriteBytes(data);
			bw.Flush();
		}
	}
}
