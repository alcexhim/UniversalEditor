//
//  BIGFDataFormat.cs - provides a DataFormat for manipulating archives in Electronic Arts BIGF format
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.ElectronicArts
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Electronic Arts BIGF format.
	/// </summary>
	public class BIGFDataFormat : DataFormat
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
			if (header != "BIGF") throw new InvalidDataFormatException("File does not start with BIGF");

			br.Endianness = IO.Endianness.BigEndian;

			uint archiveSize = br.ReadUInt32();
			uint fileCount = br.ReadUInt32();
			uint firstFileOffset = br.ReadUInt32();

			// TODO: figure out what firstFileOffset points to... the data or the entry
			// br.Accessor.Seek(firstFileOffset, SeekOrigin.Begin);
			for (uint i = 0; i < fileCount; i++)
			{
				uint offset = br.ReadUInt32();
				uint length = br.ReadUInt32();
				string filename = br.ReadNullTerminatedString();

				File file = fsom.AddFile(filename);
				file.Source = new EmbeddedFileSource(br, offset, length);
				file.Size = length;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("BIGF");

			bw.Endianness = IO.Endianness.BigEndian;

			uint archiveSize = 0;
			long archiveSizePos = bw.Accessor.Position;
			bw.WriteUInt32(archiveSize);

			bw.WriteUInt32((uint)fsom.Files.Count);

			uint offset = 16;
			foreach (File file in fsom.Files)
			{
				offset += (uint)(8 + file.Name.Length + 1);
			}
			bw.WriteUInt32(offset);

			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteNullTerminatedString(file.Name);

				offset += (uint)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData()); // file.WriteTo(bw);
			}
		}
	}
}
