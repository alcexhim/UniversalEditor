//
//  MB6DataFormat.cs - provides a DataFormat for manipulating archives in Hardball MB6 format
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

namespace UniversalEditor.DataFormats.FileSystem.Hardball
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Hardball MB6 format.
	/// </summary>
	public class MB6DataFormat : DataFormat
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
			string MB6_ = br.ReadFixedLengthString(4);
			if (MB6_ != "MB6\0") throw new InvalidDataFormatException("File does not begin with \"MB6\", 0");

			// read the first file entry
			uint fileSize1 = br.ReadUInt32();
			uint fileOffset1 = br.ReadUInt32();
			string fileName1 = br.ReadFixedLengthString(14);     // encrypted?

			File file1 = new File();
			file1.Name = fileName1;
			file1.Size = fileSize1;
			file1.Properties.Add("reader", br);
			file1.Properties.Add("length", fileSize1);
			file1.Properties.Add("offset", fileOffset1);
			file1.DataRequest += file_DataRequest;

			fsom.Files.Add(file1);

			while (br.Accessor.Position <= fileSize1)
			{
				uint fileSize = br.ReadUInt32();
				uint fileOffset = br.ReadUInt32();
				string fileName = br.ReadFixedLengthString(14);     // encrypted?

				File file = new File();
				file.Name = fileName;
				file.Size = fileSize;
				file.Properties.Add("reader", br);
				file.Properties.Add("length", fileSize);
				file.Properties.Add("offset", fileOffset);
				file.DataRequest += file_DataRequest;

				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];
			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("MB6\0");

			uint fileOffset = (uint)(22 * fsom.Files.Count);
			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32(fileOffset);
				bw.WriteFixedLengthString(file.Name, 14);
				fileOffset += (uint)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}
			bw.Flush();
		}
	}
}
