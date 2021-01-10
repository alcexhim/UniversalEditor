//
//  GOBDataFormat.cs - provides a DataFormat for manipulating archives in GOB format
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

namespace UniversalEditor.DataFormats.FileSystem.IndianaJones.GOB
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in GOB format.
	/// </summary>
	public class GOBDataFormat : DataFormat
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
			string signature = br.ReadFixedLengthString(4);
			if (signature != "GOB ") throw new InvalidDataFormatException("File does not begin with \"GOB \"");

			uint version = br.ReadUInt32();
			if (version != 20) throw new InvalidDataFormatException("Unrecognized GOB version \"" + version.ToString() + "\"");

			uint unknown1 = br.ReadUInt32();
			uint fileCount = br.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				uint offset = br.ReadUInt32();
				uint length = br.ReadUInt32();
				string filename = br.ReadFixedLengthString(128).TrimNull();

				File file = fsom.AddFile(filename);
				file.Size = length;
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Seek(offset, SeekOrigin.Begin);
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("GOB ");

			bw.WriteUInt32((uint)20);

			bw.WriteUInt32((uint)0);

			File[] files = fsom.GetAllFiles();
			bw.WriteUInt32((uint)files.Length);

			uint offset = 16;
			foreach (File file in files)
			{
				offset += 136;
			}
			foreach (File file in files)
			{
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteFixedLengthString(file.Name, 128);

				offset += (uint)file.Size;
			}
			foreach (File file in files)
			{
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
