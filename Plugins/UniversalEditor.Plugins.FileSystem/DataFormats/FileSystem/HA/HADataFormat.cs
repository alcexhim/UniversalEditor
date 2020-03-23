//
//  HADataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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

namespace UniversalEditor.DataFormats.FileSystem.HA
{
	public class HADataFormat : DataFormat
	{
		public Compression.CompressionMethod CompressionMethod { get; set; } = Compression.CompressionMethod.None;

		protected override DataFormatReference MakeReferenceInternal()
		{
			return base.MakeReferenceInternal();
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string HA = br.ReadFixedLengthString(2);
			short cnt = br.ReadInt16();
			for (short c = 0; c < cnt; c++)
			{
				byte versionAndType = br.ReadByte();
				int compressedSize = br.ReadInt32();
				int decompressedSize = br.ReadInt32();
				int crc = br.ReadInt32();
				int fileTime = br.ReadInt32();
				string path = br.ReadNullTerminatedString();
				string name = br.ReadNullTerminatedString();
				string fileName = path + System.IO.Path.DirectorySeparatorChar.ToString() + name;
				byte machineSpecificInformationLength = br.ReadByte();
				for (byte b = 0; b < machineSpecificInformationLength; b = (byte)(b + 1))
				{
					byte machineSpecificInformationType = br.ReadByte();
					byte machineSpecificInformation = br.ReadByte();
				}

				File f = fsom.AddFile(fileName);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("HA");

			File[] files = fsom.GetAllFiles();
			bw.WriteInt16((short)files.Length);
			foreach (File f in files)
			{
				byte versionAndType = 0;
				bw.WriteByte(versionAndType);
				byte[] decompressedData = f.GetData();
				byte[] compressedData = Compression.CompressionModule.FromKnownCompressionMethod(CompressionMethod).Compress(decompressedData);
				bw.WriteInt32(compressedData.Length);
				bw.WriteInt32(decompressedData.Length);
				int crc = 0;
				bw.WriteInt32(crc);
				int filetime = 0;
				bw.WriteInt32(filetime);
				string path = System.IO.Path.GetDirectoryName(f.Name);
				string name = System.IO.Path.GetFileName(f.Name);
				bw.WriteNullTerminatedString(path);
				bw.WriteNullTerminatedString(name);
				byte machineSpecificInformationLength = 0;
				bw.WriteByte(machineSpecificInformationLength);
			}
		}
	}
}
