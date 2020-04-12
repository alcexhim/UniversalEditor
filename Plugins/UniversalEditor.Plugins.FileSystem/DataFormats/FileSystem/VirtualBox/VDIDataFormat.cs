//
//  VDIDataFormat.cs - provides a DataFormat for manipulating disk images in VirtualBox VDI format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.VirtualBox
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating disk images in VirtualBox VDI format.
	/// </summary>
	public class VDIDataFormat : DataFormat
	{
		/*
		private CustomDataFormatReference _cdfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_cdfr == null)
			{
				_cdfr = new CustomDataFormatReference();
				_cdfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_cdfr.Title = "VirtualBox disk image";

				_cdfr.Items.Add(new CustomDataFormatItemField("signature", "FixedString", 40));

				_cdfr.Items.Add(new CustomDataFormatItemField("unknown1", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown2", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown3", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown4", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown5", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown6", "Int32"));

				_cdfr.Items.Add(new CustomDataFormatItemField("comment", "FixedString", 256));
				_cdfr.Items.Add(new CustomDataFormatItemField("unknown7", "Int32"));
				_cdfr.Items.Add(new CustomDataFormatItemField("dataOffset", "Int32"));
			}
			return _cdfr;
		}
		*/

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
			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(40);

			int unknown1 = reader.ReadInt32();
			int unknown2 = reader.ReadInt32();
			int unknown3 = reader.ReadInt32();
			int unknown4 = reader.ReadInt32();
			int unknown5 = reader.ReadInt32();
			int unknown6 = reader.ReadInt32();
			int unknown7 = reader.ReadInt32();
			int unknown8 = reader.ReadInt32();
			int unknown9 = reader.ReadInt32();
			int unknown10 = reader.ReadInt32();
			int unknown11 = reader.ReadInt32();

			string comment = reader.ReadFixedLengthString(256);
			int unknown12 = reader.ReadInt32();
			int dataOffset = reader.ReadInt32();

			long dataLength = reader.Accessor.Length - dataOffset;

			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			File f = new File();
			f.Name = System.IO.Path.ChangeExtension(System.IO.Path.GetFileName(Accessor.GetFileName()), "img");
			f.Source = new EmbeddedFileSource(reader, dataOffset, dataLength);
			fsom.Files.Add(f);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
