//
//  RSCDataFormat.cs - implements the Knowledge Adventure RSC archive format
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

namespace UniversalEditor.DataFormats.FileSystem.KnowledgeAdventure.RSC
{
	/// <summary>
	/// Implements the Knowledge Adventure RSC archive format.
	/// </summary>
	public class RSCDataFormat : DataFormat
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

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(6);
			if (signature != "BWRF10") throw new InvalidDataFormatException("File does not begin with 'BWRF10'");

			int fileCount = reader.ReadInt32();
			if (fileCount * 20 > reader.Accessor.Length) throw new InvalidDataFormatException("Number of files (" + fileCount.ToString() + ") does not make sense");

			for (int i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(12).TrimNull();
				int offset = reader.ReadInt32();
				int length = reader.ReadInt32();

				File file = new File();
				file.Name = fileName;
				file.Size = length;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			int offset = (int)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("BWRF10");
			writer.WriteInt32(fsom.Files.Count);

			int offset = 10;
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				offset += 20;
			}

			for (int i = 0; i < fsom.Files.Count; i++)
			{
				writer.WriteFixedLengthString(fsom.Files[i].Name, 12);
				writer.WriteInt32(offset);

				int length = (int)fsom.Files[i].Size;
				writer.WriteInt32(length);
				offset += length;
			}

			for (int i = 0; i < fsom.Files.Count; i++)
			{
				byte[] data = fsom.Files[i].GetData();
				writer.WriteBytes(data);
			}
		}
	}
}
