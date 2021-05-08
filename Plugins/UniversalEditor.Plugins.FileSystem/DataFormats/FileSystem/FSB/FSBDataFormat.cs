//
//  FSBDataFormat.cs - provides a DataFormat for manipulating archives in FSB format
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

namespace UniversalEditor.DataFormats.FileSystem.FSB
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in FSB format.
	/// </summary>
	public class FSBDataFormat : DataFormat
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
			string header = br.ReadFixedLengthString(4);
			if (header != "FSB3") throw new InvalidDataFormatException("File does not begin with FSB3");

			uint fileCount = br.ReadUInt32();
			uint directorySize = br.ReadUInt32();
			uint dataSize = br.ReadUInt32();
			uint unknown1 = br.ReadUInt32();
			uint unknown2 = br.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				ushort entrySize = br.ReadUInt16();
				string fileName = br.ReadFixedLengthString(30);
				uint decompressedSize = br.ReadUInt32();
				uint offset = br.ReadUInt32();
				uint compressedSize = br.ReadUInt32();
				byte[] otherSoundInformation = br.ReadBytes(16);

				File file = new File();
				file.Name = fileName;
				file.Properties.Add("length", compressedSize);
				file.Properties.Add("offset", offset);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
				file.Size = decompressedSize;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("FSB3");

			bw.WriteUInt32((uint)fsom.Files.Count);

			uint directorySize = 0;
			bw.WriteUInt32(directorySize);

			uint dataSize = 0;
			bw.WriteUInt32(dataSize);

			uint unknown1 = 0;
			bw.WriteUInt32(unknown1);

			uint unknown2 = 0;
			bw.WriteUInt32(unknown2);

			uint offset = (uint)(24 + (60 * fsom.Files.Count));

			FileNameShortener ss = new FileNameShortener();
			ss.Count = fsom.Files.Count;
			ss.MaxFileNameLength = 26;
			ss.MaxExtensionLength = 3;

			foreach (File file in fsom.Files)
			{
				ushort entrySize = 60;
				bw.WriteUInt16(entrySize);

				bw.WriteFixedLengthString(ss.Shorten(file.Name), 30);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32(offset);
				uint compressedSize = (uint)file.Size;
				bw.WriteUInt32(compressedSize);

				byte[] otherSoundInformation = new byte[16];
				bw.WriteBytes(otherSoundInformation);

				offset += compressedSize;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
