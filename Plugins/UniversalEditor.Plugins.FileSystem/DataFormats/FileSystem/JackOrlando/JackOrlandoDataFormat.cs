//
//  JackOrlandoDataFormat.cs - provides a DataFormat for manipulating archives in Jack Orlando format
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

namespace UniversalEditor.DataFormats.FileSystem.JackOrlando
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Jack Orlando format.
	/// </summary>
	public class JackOrlandoDataFormat : DataFormat
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
			string PAK = br.ReadFixedLengthString(4);
			if (PAK != "PAK\0") throw new InvalidDataFormatException("File does not begin with \"PAK\", 0");

			uint fileCount = br.ReadUInt32();
			uint firstFileOffset = br.ReadUInt32();
			uint unknown = br.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();
				uint fileNameLength = br.ReadUInt32();
				string fileName = br.ReadNullTerminatedString();

				// do we require this?
				if (fileName.Length != (fileNameLength - 1)) throw new InvalidDataFormatException("File name length mismatch");

				File file = new File();
				file.Name = fileName;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", fileOffset);
				file.Properties.Add("length", fileLength);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PAK\0");

			bw.WriteUInt32((uint)fsom.Files.Count);

			uint firstFileOffset = 16;
			foreach (File file in fsom.Files)
			{
				firstFileOffset += (uint)(12 + (file.Name.Length + 1));
			}
			bw.WriteUInt32(firstFileOffset);

			uint offset = firstFileOffset;
			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32((uint)file.Name.Length);
				bw.WriteNullTerminatedString(file.Name);
				offset += (uint)file.Size;
			}

			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}
			bw.Flush();
		}
	}
}