//
//  MFDDataFormat.cs - provides a DataFormat for manipulating archives in 187 Ride or Die MFD format
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

namespace UniversalEditor.DataFormats.FileSystem.MFD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in 187 Ride or Die MFD format.
	/// </summary>
	public class MFDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=GRAF:187_Ride_Or_Die_MFD");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				uint fileID = reader.ReadUInt32();
				uint offset = reader.ReadUInt32();
				uint length = reader.ReadUInt32();

				File file = new File();
				file.Name = i.ToString().PadLeft(8, '0');
				file.Size = length;
				file.DataRequest += file_DataRequest;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];

			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			Writer writer = base.Accessor.Writer;
			writer.WriteUInt32((uint)files.Length);

			uint offset = (uint)(4 + (12 * files.Length));
			foreach (File file in files)
			{
				uint fileID = (uint)Array.IndexOf(files, file);
				writer.WriteUInt32(fileID);

				uint length = (uint)file.Size;
				writer.WriteUInt32(offset);
				writer.WriteUInt32(length);

				offset += length + (length % 4);
			}
			foreach (File file in files)
			{
				writer.WriteBytes(file.GetData());
				writer.Align(4);
			}
			writer.Flush();
		}
	}
}
