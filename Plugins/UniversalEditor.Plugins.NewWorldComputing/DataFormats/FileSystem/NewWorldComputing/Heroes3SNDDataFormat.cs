//
//  Heroes3SNDDataFormat.cs - provides a DataFormat for manipulating Heroes of Might and Magic III SND archives
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
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Heroes of Might and Magic III SND archives.
	/// </summary>
	public class Heroes3SNDDataFormat : DataFormat
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
			uint fileCount = reader.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				File file = new File();
				file.Name = String.Join(".", reader.ReadFixedLengthString(12).Split(new char[] { '\0' }));

				byte[] unknown28 = reader.ReadBytes(28);
				uint offset = reader.ReadUInt32();
				uint length = reader.ReadUInt32();
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.Properties.Add("reader", reader);

				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
				file.Size = length;
				fsom.Files.Add(file);
			}
		}

		#region Data Request
		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			string FileName = String.Empty;
			if (Accessor is FileAccessor)
			{
				FileName = (Accessor as FileAccessor).FileName;
			}

			File file = (sender as File);
			IO.Reader reader = (IO.Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			reader.Seek(offset, IO.SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}
		#endregion

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

		}
	}
}
