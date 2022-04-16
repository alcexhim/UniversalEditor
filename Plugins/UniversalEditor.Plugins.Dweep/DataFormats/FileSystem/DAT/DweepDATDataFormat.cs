//
//  DweepDATDataFormat.cs - provides a DataFormat for reading and writing Dweep DAT archives
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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

namespace UniversalEditor.Plugins.Dweep.DataFormats.FileSystem.DAT
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for reading and writing Dweep
	/// DAT archives.
	/// </summary>
	[DataFormatImplementationStatus(ImplementationStatus.Complete)]
	public class DweepDATDataFormat : DataFormat
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
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			uint filecount = Accessor.Reader.ReadUInt32();

			uint thisFileOffset = Accessor.Reader.ReadUInt32();
			for (uint i = 0; i < filecount - 1; i++)
			{
				string filename = Accessor.Reader.ReadFixedLengthString(13).TrimNull();
				uint nextFileOffset = Accessor.Reader.ReadUInt32();

				uint length = nextFileOffset - thisFileOffset;

				File file = fsom.AddFile(filename);
				file.Source = new EmbeddedFileSource(Accessor.Reader, thisFileOffset, length);

				thisFileOffset = nextFileOffset;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();
			Accessor.Writer.WriteUInt32((uint)(files.Length + 1));

			uint offset = (uint)(((files.Length + 1) * 17) + 4);
			Accessor.Writer.WriteUInt32(offset);

			for (int i = 0; i < files.Length; i++)
			{
				Accessor.Writer.WriteFixedLengthString(files[i].Name, 13);
				Accessor.Writer.WriteUInt32((uint)(offset + files[i].Size));
				offset += (uint)files[i].Size;
			}

			Accessor.Writer.WriteFixedLengthString(String.Empty, 13);

			for (int i = 0; i < files.Length; i++)
			{
				Accessor.Writer.WriteBytes(files[i].GetData());
			}
		}
	}
}
