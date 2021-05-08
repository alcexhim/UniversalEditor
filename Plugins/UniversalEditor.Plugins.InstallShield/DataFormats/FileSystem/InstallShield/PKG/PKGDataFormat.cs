//
//  PKGDataFormat.cs - provides a DataFormat for manipulating InstallShield PKG archive files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.InstallShield.PKG
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating InstallShield PKG archive files.
	/// </summary>
	public class PKGDataFormat : DataFormat
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

			Reader br = Accessor.Reader;
			ushort signature = br.ReadUInt16();
			if (signature != 0xA34A) throw new InvalidDataFormatException("File does not begin with 0xA34A");

			ushort unknown1a = br.ReadUInt16();
			ushort unknown1b = br.ReadUInt16();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			/*
			uint unknown4 = br.ReadUInt32();
			ushort unknown5 = br.ReadUInt16();
			ushort unknown6 = br.ReadUInt16();
			ushort unknown7 = br.ReadUInt16();
			*/
			ushort fileCount = br.ReadUInt16();
			for (ushort i = 0; i < fileCount; i++)
			{
				ushort FileNameLength = br.ReadUInt16();
				string FileName = br.ReadFixedLengthString(FileNameLength);
				/*
				byte unknown8 = br.ReadByte();
				uint unknown9 = br.ReadUInt32();
				ushort unknown10 = br.ReadUInt16();
				*/
				ushort unknown8 = br.ReadUInt16();
				ushort maybeLength = br.ReadUInt16();
				ushort unknown9 = br.ReadUInt16();

				File file = fsom.AddFile(FileName);
				file.Size = maybeLength;
				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer bw = Accessor.Writer;
			bw.WriteUInt16(0xA34A);
			bw.WriteUInt32(10);
			bw.WriteUInt32(2);
			bw.WriteUInt32(65536);

			File[] files = fsom.GetAllFiles();
			bw.WriteUInt16((ushort)files.Length);
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				bw.WriteUInt16((ushort)files[i].Name.Length);
				bw.WriteFixedLengthString(files[i].Name, (ushort)files[i].Name.Length);

				bw.WriteUInt16(2);
				bw.WriteUInt16((ushort)files[i].Size);
				bw.WriteUInt16(8);
			}
		}
	}
}
