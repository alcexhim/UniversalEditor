//
//  PAKDataFormat.cs - provides a DataFormat for manipulating archives in Dreamfall PAK format
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

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.CuteZipPAK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Dreamfall PAK format.
	/// </summary>
	[DataFormatImplementationStatus(ImplementationStatus.Complete)]
	public class PAKDataFormat : DataFormat
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
			if (!signature.Equals("PACK"))
			{
				throw new InvalidDataFormatException("file does not begin with 'PACK'");
			}

			uint tocOffset = br.ReadUInt32();
			uint tocLength = br.ReadUInt32();
			// data begins immediately after

			br.Accessor.Seek(tocOffset, IO.SeekOrigin.Begin);

			while (br.Accessor.Position < tocOffset + tocLength)
			{
				string fileName = br.ReadFixedLengthString(56).TrimNull();
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Source = new EmbeddedFileSource(br, fileOffset, fileLength);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = Accessor.Writer;

			bw.WriteFixedLengthString("PACK");

			uint fileOffset = 12;
			File[] files = fsom.GetAllFiles();
			uint dataLength = 12;
			for (int i = 0; i < files.Length; i++)
			{
				dataLength += (uint)files[i].GetData().Length;
			}
			bw.WriteUInt32(dataLength);
			bw.WriteUInt32((uint)files.Length * 64); // total TOC size

			for (int i = 0; i < files.Length; i++)
			{
				bw.WriteBytes(files[i].GetData());
			}

			// place TOC at end of file data
			for (int i = 0; i < files.Length; i++)
			{
				uint fileLength = (uint)files[i].GetData().Length;

				bw.WriteFixedLengthString(files[i].Name, 56);
				bw.WriteUInt32(fileOffset);
				bw.WriteUInt32(fileLength);
				fileOffset += fileLength;
			}
		}
	}
}
