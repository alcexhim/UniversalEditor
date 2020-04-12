//
//  CLTDataFormat.cs - provides a DataFormat for manipulating archives in Abomination CLT format
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Abomination
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Abomination CLT format.
	/// </summary>
	public class CLTDataFormat : DataFormat
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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string AWAD = reader.ReadFixedLengthString(4);
			if (AWAD != "AWAD") throw new InvalidDataFormatException("File does not begin with 'AWAD'");

			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(260);
				uint length = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Source = new EmbeddedFileSource(reader, offset, length);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("AWAD");

			File[] files = fsom.GetAllFiles();
			writer.WriteUInt32((uint)files.Length);
			uint offset = (uint)(8 + ((260 + 4 + 4) * files.Length));

			foreach (File file in files)
			{
				writer.WriteFixedLengthString(file.Name, 260);
				writer.WriteUInt32((uint)file.Size);
				writer.WriteUInt32(offset);
			}
			foreach (File file in files)
			{
				file.WriteTo(writer);
			}
		}
	}
}
