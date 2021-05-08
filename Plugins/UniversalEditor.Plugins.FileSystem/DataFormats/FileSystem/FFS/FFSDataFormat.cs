//
//  FFSDataFormat.cs - provides a DataFormat for manipulating archives in FFS format
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

namespace UniversalEditor.DataFormats.FileSystem.FFS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in FFS format.
	/// </summary>
	public class FFSDataFormat : DataFormat
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
			string FFS_ = br.ReadFixedLengthString(4);
			if (FFS_ != "FFS ") throw new InvalidDataFormatException("File does not begin with \"FFS \"");

			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				uint fileNameLength = br.ReadUInt32();
				string fileName = br.ReadFixedLengthString(fileNameLength);
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();

				File file = new File();
				file.Name = fileName;
				file.Size = fileLength;
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

			if (!br.EndOfStream)
			{
				byte end = br.ReadByte();
				if (end != 255)
				{
					// is this bad?
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("FFS ");

			bw.WriteUInt32((uint)fsom.Files.Count);

			uint offset = 8;
			foreach (File file in fsom.Files)
			{
				offset += (uint)(12 + file.Name.Length);
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32((uint)file.Name.Length);
				bw.WriteFixedLengthString(file.Name);
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				offset += (uint)file.Size + 1;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
				bw.WriteByte((byte)255); // not quite sure what this is for, other than a file separator??
			}
		}
	}
}
