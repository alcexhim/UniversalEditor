//
//  COSDataFormat.cs - provides a DataFormat for manipulating Moosta OMP character costume files
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

namespace UniversalEditor.DataFormats.FileSystem.Moosta.Skin
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Moosta OMP character costume files.
	/// </summary>
	public class COSDataFormat : DataFormat
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

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(7);
			if (signature != "OmpSkin") throw new InvalidDataFormatException("File does not begin with \"OmpSkin\"");

			while (!br.EndOfStream)
			{
				string fileName = br.ReadLengthPrefixedString();
				int fileSize = br.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Source = new EmbeddedFileSource(br, br.Accessor.Position, fileSize);
				file.Size = fileSize;

				br.Accessor.Seek(fileSize, SeekOrigin.Current);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("OmpSkin");

			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				bw.Write(file.Name);
				bw.WriteInt32((int)file.Size);
				file.WriteTo(bw);
			}
			bw.Flush();
		}
	}
}
