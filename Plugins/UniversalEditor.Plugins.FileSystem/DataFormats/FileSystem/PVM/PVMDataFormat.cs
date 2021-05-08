//
//  PVMDataFormat.cs - provides a DataFormat for manipulating archives in PVM format
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

using System;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.PVM
{
	/// <summary>
	/// Provides a <see cref="DataFormat"/> for manipulating archives in PVM format.
	/// </summary>
	public class PVMDataFormat : DataFormat
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

			Reader reader = base.Accessor.Reader;

			// go to the end of the file and read the central directory offset
			if (reader.Accessor.Length < 4) throw new InvalidDataFormatException("File must be larger than 4 bytes");

			reader.Seek(-4, SeekOrigin.End);
			int centralDirectoryOffset = reader.ReadInt32();

			if (centralDirectoryOffset > reader.Accessor.Length) throw new InvalidDataFormatException("Central directory offset is larger than file itself!");

			reader.Seek(centralDirectoryOffset, SeekOrigin.Begin);

			byte signatureLength = reader.ReadByte();
			if (signatureLength != 5) throw new InvalidDataFormatException("Signature expected to be 5 bytes in length");
			string signature = reader.ReadFixedLengthString(signatureLength);
			if (signature != "[PVM]") throw new InvalidDataFormatException("File does not contain the signature '[PVM]'");

			int fileCount = reader.ReadInt32();

			for (int i = 0; i < fileCount; i++)
			{
				short unknown1 = reader.ReadInt16();
				string fileName = reader.ReadFixedLengthString(13).TrimNull();

				// FIXME: these aren't offset/length, they're outside the file limits!!
				int offset = reader.ReadInt32();
				short unknown2 = reader.ReadInt16();        // 8448
				short[] unknown3 = reader.ReadInt16Array(17);

				int length = reader.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Source = new UniversalEditor.ObjectModels.FileSystem.FileSources.EmbeddedFileSource(reader, offset, length);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
