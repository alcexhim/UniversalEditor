//
//  AGGDataFormat.cs - provides a DataFormat for manipulating Heroes of Might and Magic II AGG archives
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing.AGG
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Heroes of Might and Magic II AGG archives.
	/// </summary>
	public class AGGDataFormat : DataFormat
	{
		private struct AGGFileEntry
		{
			public uint hash;
			public uint offset;
			public uint size;
			public string name;
		}

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

			IO.Reader br = base.Accessor.Reader;
			ushort fileCount = br.ReadUInt16();

			AGGFileEntry[] files = new AGGFileEntry[fileCount];
			for (ushort i = 0; i < fileCount; i++)
			{
				files[i].hash = br.ReadUInt32();
				files[i].offset = br.ReadUInt32();
				files[i].size = br.ReadUInt32();
			}
			br.Accessor.Seek(-(fileCount * 15), IO.SeekOrigin.End);
			for (ushort i = 0; i < fileCount; i++)
			{
				files[i].name = br.ReadFixedLengthString(15);
				files[i].name = files[i].name.TrimNull();

				File file = new File();
				file.Name = files[i].name;
				file.Size = files[i].size;
				file.Source = new EmbeddedFileSource(br, files[i].offset, files[i].size);
				fsom.Files.Add(file);
			}


			// 43341516
		}

		/*
		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			AGGFileEntry entry = (AGGFileEntry)file.Properties["InternalData"];
			IO.Reader br = (IO.Reader)file.Properties["BinaryReader"];

			br.Accessor.Seek(entry.offset, IO.SeekOrigin.Begin);
			e.Data = br.ReadBytes(entry.size);

			// UniversalEditor.Common.Hashing.CRC32.Initialize(UniversalEditor.Common.Hashing.CRC32.Keys.ReversedReciprocal);
			UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule cksm = new UniversalEditor.Checksum.Modules.CRC32.CRC32ChecksumModule();
			uint hash = (uint)cksm.Calculate(e.Data);

		}
		*/

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			ushort fileCount = (ushort)fsom.Files.Count;
			bw.WriteUInt16(fileCount);

			uint offset = (uint)(bw.Accessor.Position + (12 * fsom.Files.Count));
			foreach (File file in fsom.Files)
			{
				uint hash = 0;
				uint size = (uint)file.Size;

				bw.WriteUInt32(hash);
				bw.WriteUInt32(offset);
				bw.WriteUInt32(size);

				offset += size;
			}

			for (ushort i = 0; i < fileCount; i++)
			{
				bw.WriteBytes(fsom.Files[i].GetData());
				// fsom.Files[i].WriteTo(bw);
			}

			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 15);
			}
		}
	}
}
