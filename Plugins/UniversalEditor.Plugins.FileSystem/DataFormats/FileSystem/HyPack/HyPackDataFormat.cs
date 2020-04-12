//
//  HyPackDataFormat.cs - provides a DataFormat for manipulating archives in HyPack format
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

namespace UniversalEditor.DataFormats.FileSystem.HyPack
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in HyPack format.
	/// </summary>
	public class HyPackDataFormat : DataFormat
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
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			string HyPack = br.ReadFixedLengthString(6);
			if (HyPack != "HyPack") throw new InvalidDataFormatException("File does not begin with \"HyPack\"");

			byte unknown0 = br.ReadByte();
			int DirectoryCount = br.ReadInt32();

			byte unknown = br.ReadByte();

			int FileCount = br.ReadInt32();
			int FileDataOffset = 16268; // 16 + (48 * FileCount);

			for (int i = 0; i < FileCount; i++)
			{
				string FileName = br.ReadFixedLengthString(21);
				if (FileName.Contains("\0")) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

				short unknown1 = br.ReadInt16();
				short unknown2 = br.ReadInt16();
				short FileOffset = br.ReadInt16();
				short unknown4 = br.ReadInt16();
				short FileLength = br.ReadInt16();
				short unknown6 = br.ReadInt16();
				short unknown7 = br.ReadInt16();
				short unknown8 = br.ReadInt16();
				short unknown9 = br.ReadInt16();
				short unknown10 = br.ReadInt16();
				short unknown11 = br.ReadInt16();
				short unknown12 = br.ReadInt16();
				short unknown13 = br.ReadInt16();

				byte FileType = br.ReadByte();

				long position = br.Accessor.Position;
				br.Accessor.Position = FileDataOffset + FileOffset;
				byte[] FileData = br.ReadBytes(FileLength);
				br.Accessor.Position = position;

				fsom.Files.Add(FileName, FileData);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{

		}
	}
}
