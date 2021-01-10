//
//  KenSilvermanGRPDataFormat.cs - provides a DataFormat for manipulating archives in Ken Silverman GRP format
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

namespace UniversalEditor.DataFormats.FileSystem.KenSilverman
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Ken Silverman GRP format.
	/// </summary>
	public class KenSilvermanGRPDataFormat : DataFormat
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
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string signature = reader.ReadFixedLengthString(12);
			if (signature != "KenSilverman")
				throw new InvalidDataFormatException("file does not begin with 'KenSilverman'");

			uint fileCount = reader.ReadUInt32();
			uint offset = (16 * (fileCount + 1));
			for (uint i = 0; i < fileCount; i++)
			{
				string filename = reader.ReadFixedLengthString(12);
				filename = filename.TrimNull();

				uint size = reader.ReadUInt32();
				File f = fsom.AddFile(filename);
				f.Size = size;
				f.Properties.Add("reader", reader);
				f.Properties.Add("offset", offset);
				f.Properties.Add("length", size);
				f.DataRequest += f_DataRequest;

				offset += size;
			}
		}

		private void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader reader = (Reader)f.Properties["reader"];
			uint offset = (uint)f.Properties["offset"];
			uint length = (uint)f.Properties["length"];

			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteFixedLengthString("KenSilverman");

			uint filecount = (uint)fsom.Files.Count;
			writer.WriteUInt32(filecount);

			for (uint i = 0; i < filecount; i++)
			{
				writer.WriteFixedLengthString(fsom.Files[(int)i].Name, 12);
				writer.WriteUInt32((uint)fsom.Files[(int)i].Size);
			}
			for (uint i = 0; i < filecount; i++)
			{
				writer.WriteBytes(fsom.Files[(int)i].GetData());
			}
		}
	}
}
