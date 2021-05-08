//
//  KZPDataFormat.cs - provides a DataFormat for manipulating archives in Kens Labyrinth KZP format
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

namespace UniversalEditor.DataFormats.FileSystem.KensLabyrinth.KZP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Kens Labyrinth KZP format.
	/// </summary>
	public class KZPDataFormat : DataFormat
	{
		private DataFormatReference _dfr = null;
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

			Reader reader = base.Accessor.Reader;
			ushort fileCount = reader.ReadUInt16();
			for (ushort i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(8).TrimNull();
				uint offset = reader.ReadUInt32();
				uint length = 0;
				if (i == fileCount - 1)
				{
					length = (uint)(base.Accessor.Length - offset);
				}
				else
				{
					string nextFileName = reader.ReadFixedLengthString(8);
					uint nextFileOffset = reader.ReadUInt32();
					reader.Seek(-12, SeekOrigin.Current);
					length = (uint)(nextFileOffset - offset);
				}

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.DataRequest += file_DataRequest;
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

			Writer writer = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();
			writer.WriteUInt16((ushort)files.Length);

			uint offset = (uint)(2 + (12 * (ushort)files.Length));
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				writer.WriteFixedLengthString(files[i].Name, 8);
				writer.WriteUInt32(offset);
				offset += (uint)files[i].Size;
			}
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				writer.WriteBytes(files[i].GetData());
			}
			writer.Flush();
		}
	}
}
