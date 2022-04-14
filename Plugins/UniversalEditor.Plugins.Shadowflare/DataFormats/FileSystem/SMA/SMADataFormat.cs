//
//  SMADataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.Plugins.Shadowflare.DataFormats.FileSystem.SMA
{
	public class SMADataFormat : DataFormat
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
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			string signature = Accessor.Reader.ReadFixedLengthString(8);
			if (signature != "SMAFile\0")
				throw new InvalidDataFormatException("file does not begin with 'SMAFile', 0x00");

			uint maybeVersion = Accessor.Reader.ReadUInt32();
			if (maybeVersion != 1)
			{
				// sanity check?
			}

			uint maybeChecksum = Accessor.Reader.ReadUInt32();

			uint fileCount = Accessor.Reader.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				uint filenameLength = Accessor.Reader.ReadUInt32();
				string filename = Accessor.Reader.ReadFixedLengthString(filenameLength);

				uint dataLength = Accessor.Reader.ReadUInt32();

				File file = fsom.AddFile(filename);
				file.Size = dataLength;
				file.Source = new EmbeddedFileSource(Accessor.Reader, Accessor.Position, dataLength);

				Accessor.Seek(dataLength, IO.SeekOrigin.Current);
			}
		}

		Checksum.ChecksumModule crc = new Checksum.Modules.StrangeCRC.StrangeCRCChecksumModule();

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			MemoryAccessor ma = new MemoryAccessor();

			Accessor.Writer.WriteFixedLengthString("SMAFile\0");
			Accessor.Writer.WriteUInt32(1);
			Accessor.Writer.WriteUInt32(0);
			ma.Writer.WriteUInt32((uint)files.Length);

			for (uint i = 0; i < (uint)files.Length; i++)
			{
				ma.Writer.WriteUInt32((uint)files[i].Name.Length);
				ma.Writer.WriteFixedLengthString(files[i].Name);

				byte[] data = files[i].GetData();
				ma.Writer.WriteUInt32((uint)data.Length);
				ma.Writer.WriteBytes(data);
			}

			byte[] payload = ma.ToArray();
			uint checksum = (uint)crc.Calculate(payload);

			Accessor.Writer.WriteBytes(ma.ToArray());

			Accessor.Seek(12, IO.SeekOrigin.Begin);
			Accessor.Writer.WriteUInt32(checksum);
		}
	}
}
