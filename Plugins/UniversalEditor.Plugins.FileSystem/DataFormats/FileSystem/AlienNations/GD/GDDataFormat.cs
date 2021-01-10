//
//  GDDataFormat.cs - provides a DataFormat for manipulating archives in GD format
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

namespace UniversalEditor.DataFormats.FileSystem.AlienNations.GD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in GD format.
	/// </summary>
	public class GDDataFormat : DataFormat
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

			IO.Reader reader = base.Accessor.Reader;
			uint archiveHeaderLength = reader.ReadUInt32();
			if (archiveHeaderLength != 24) throw new InvalidDataFormatException("Archive header length is not equal to 24!");

			uint fileNameDirectoryOffset = reader.ReadUInt32();
			uint fileNameOffsetsDirectoryOffset = reader.ReadUInt32();
			uint fileEntriesDirectoryOffset = reader.ReadUInt32();
			uint fileDataOffset = reader.ReadUInt32();
			uint fileCount = reader.ReadUInt32();

			string[] fileNames = new string[fileCount];
			uint[] fileNameRelativeOffsets = new uint[fileCount];
			ulong[] fileDataOffsets = new ulong[fileCount];

			#region FileNameDirectory
			{
				reader.Seek(fileNameDirectoryOffset, IO.SeekOrigin.Begin);
				for (uint i = 0; i < fileCount; i++)
				{
					fileNames[i] = reader.ReadNullTerminatedString();
				}
			}
			#endregion
			#region FileNameOffsetsDirectory
			{
				for (uint i = 0; i < fileCount; i++)
				{
					fileNameRelativeOffsets[i] = reader.ReadUInt32();
				}
			}
			#endregion
			#region FileEntriesDirectory
			{
				for (uint i = 0; i < fileCount; i++)
				{
					ulong unknown1 = reader.ReadUInt64(); // 12
					uint fileID = reader.ReadUInt32();
					uint unknown2 = reader.ReadUInt32(); // 52
					uint unknown3 = reader.ReadUInt32();
					uint unknown4 = reader.ReadUInt32();
					uint unknown5 = reader.ReadUInt32();
					uint unknown6 = reader.ReadUInt32();
					uint unknown7 = reader.ReadUInt32();
					uint unknown8 = reader.ReadUInt32();
					uint unknown9 = reader.ReadUInt32();
					uint unknown10 = reader.ReadUInt32();
					uint unknown11 = reader.ReadUInt32();
					ushort unknown12 = reader.ReadUInt16();
					ushort unknown13 = reader.ReadUInt16();
					fileDataOffsets[i] = reader.ReadUInt64();
				}
			}
			#endregion

			throw new NotImplementedException();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
