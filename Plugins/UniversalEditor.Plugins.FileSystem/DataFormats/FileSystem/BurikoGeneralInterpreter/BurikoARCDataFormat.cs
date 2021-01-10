//
//  BurikoARCDataFormat.cs - provides a DataFormat for manipulating archives in Buriko General Interpreter ARC format
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

using System.Linq;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.BurikoGeneralInterpreter
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Buriko General Interpreter ARC format.
	/// </summary>
	public class BurikoARCDataFormat : DataFormat
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
			string PackFile____ = br.ReadFixedLengthString(12);
			if (PackFile____ != "PackFile    ") throw new InvalidDataFormatException("File does not begin with \"PackFile    \"");

			int fileCount = br.ReadInt32();
			int FileDataOffset = 16 + (fileCount * 32);

			for (int i = 0; i < fileCount; i++)
			{
				string FileName = br.ReadFixedLengthString(16);
				if (FileName.Contains('\0')) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

				int FileOffset = br.ReadInt32();
				int FileSize = br.ReadInt32();
				int reserved1 = br.ReadInt32();
				int reserved2 = br.ReadInt32();

				File file = new File();
				file.Name = FileName;
				file.Size = FileSize;
				file.Source = new EmbeddedFileSource(br, FileDataOffset + FileOffset, FileSize);
				fsom.Files.Add(file);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PackFile    ");

			bw.WriteInt32(fsom.Files.Count);

			int fileOffset = 0;

			foreach (File file in fsom.Files)
			{
				int i = fsom.Files.IndexOf(file);

				bw.WriteFixedLengthString(file.Name, 16);

				bw.WriteInt32(fileOffset);

				int length = (int)file.Source.GetLength();
				bw.WriteInt32(length);

				int reserved1 = 0;
				bw.WriteInt32(reserved1);

				int reserved2 = 0;
				bw.WriteInt32(reserved2);

				fileOffset += length;
			}
			foreach (File file in fsom.Files)
			{
				file.WriteTo(bw);
			}
		}
	}
}
