//
//  GPWDataFormat.cs - provides a DataFormat for manipulating archives in GPW format
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

namespace UniversalEditor.DataFormats.FileSystem.GPW
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in GPW format. A data format for archives based on the Game Programmers' Wiki.
	/// </summary>
	public class GPWDataFormat : DataFormat
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

			byte[] signature = new byte[] { (byte)0x89, (byte)0x88, (byte)'G', (byte)'p', (byte)'W', (byte)0x0D, (byte)0x0A, (byte)0x1A };
			byte[] realsignature = br.ReadBytes(8);

			if (!signature.Match(realsignature)) throw new InvalidDataFormatException();


		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;

			byte[] signature = new byte[] { (byte)0x89, (byte)0x88, (byte)'G', (byte)'p', (byte)'W', (byte)0x0D, (byte)0x0A, (byte)0x1A };
			bw.WriteBytes(signature);

			// The header contains information describing the contents of the resource, and
			// indicating where the individual files stored within the resource can be located.

			// An int value, indicating how many files are stored within the resource.
			bw.WriteInt32(fsom.Files.Count);

			int offset = 12;
			foreach (File file in fsom.Files)
			{
				// Next 4n bytes
				// Where n is the number of files stored within the resource. Each 4 byte segment
				// houses an int which points to the storage location of a file within the body of
				// the resource. For example, a value of 1234 would indicate that a file is stored
				// beginning at the resource's 1234th byte.
				bw.WriteInt32(offset);
				offset += (int)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				// The body contains filename strings for each of the files stored within the resource,
				// and the actual file data. Each body entry is pointed to by a header entry, as mentioned
				// above. What follows is a description of a single body entry.

				// An int value, indicating how many bytes of data the stored file contains.
				bw.WriteInt32((int)file.Size);

				// An int value, indicating how many characters comprise the filename string.
				bw.WriteInt32(file.Name.Length);

				// Each byte contains a single filename character, where n is the number of characters in the filename string.
				bw.WriteFixedLengthString(file.Name);

				// The stored file's data, where n is the file size.
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
