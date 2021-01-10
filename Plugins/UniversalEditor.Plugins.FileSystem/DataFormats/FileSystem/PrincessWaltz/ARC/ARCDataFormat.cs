//
//  ARCDataFormat.cs - provides a DataFormat for manipulating archives in Princess Waltz ARC format
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

using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.PrincessWaltz.ARC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Princess Waltz ARC format.
	/// </summary>
	public class ARCDataFormat : DataFormat
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

			Reader reader = base.Accessor.Reader;
			uint fileExtensionCount = reader.ReadUInt32();
			Dictionary<string, uint> countsByExtension = new Dictionary<string, uint>();
			for (uint i = 0; i < fileExtensionCount; i++)
			{
				string extension = reader.ReadFixedLengthString(4).TrimNull();
				uint fileCount = reader.ReadUInt32();
				uint unknown1 = reader.ReadUInt32();
				countsByExtension.Add(extension, fileCount);
			}
			foreach (KeyValuePair<string, uint> kvp in countsByExtension)
			{
				uint count = kvp.Value;
				for (uint j = 0; j < count; j++)
				{
					string fileName = reader.ReadFixedLengthString(8).TrimNull();
					byte padding = reader.ReadByte();
					fileName += "." + kvp.Key;

					uint length = reader.ReadUInt32();
					uint offset = reader.ReadUInt32();

					File file = fsom.AddFile(fileName);
					file.Size = length;
					file.Source = new EmbeddedFileSource(reader, offset, length);
				}
			}
		}
		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			Dictionary<string, List<File>> filesByExtension = new Dictionary<string, List<File>>();
			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				string ext = System.IO.Path.GetExtension(file.Name);
				if (ext.StartsWith(".")) ext = ext.Substring(1);
				if (!filesByExtension.ContainsKey(ext))
				{
					filesByExtension.Add(ext, new List<File>());
				}
				filesByExtension[ext].Add(file);
			}

			uint fileExtensionCount = (uint)filesByExtension.Count;
			writer.WriteUInt32(fileExtensionCount);
			foreach (KeyValuePair<string, List<File>> kvp in filesByExtension)
			{
				writer.WriteFixedLengthString(kvp.Key, 4);
				writer.WriteUInt32((uint)kvp.Value.Count);
				writer.WriteUInt32(0);
			}

			uint offset = (uint)(4 + (12 * filesByExtension.Count) + (17 * files.Length));

			foreach (KeyValuePair<string, List<File>> kvp in filesByExtension)
			{
				foreach (File file in kvp.Value)
				{
					string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name);
					writer.WriteFixedLengthString(fileName, 8);
					writer.WriteByte(0);

					uint length = (uint)file.Size;
					writer.WriteUInt32(length);
					writer.WriteUInt32(offset);
					offset += length;
				}
			}

			foreach (KeyValuePair<string, List<File>> kvp in filesByExtension)
			{
				foreach (File file in kvp.Value)
				{
					file.WriteTo(writer);
				}
			}
			writer.Flush();
		}
	}
}
