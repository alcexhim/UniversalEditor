//
//  ICEDataFormat.cs - provides a DataFormat for manipulating archives in REEVEsoft Freeze ICE format
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

namespace UniversalEditor.DataFormats.FileSystem.REEVEsoft.Freeze
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in REEVEsoft Freeze ICE format.
	/// </summary>
	public class ICEDataFormat : DataFormat
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

		public bool RetainOriginalDate { get; set; } = true;
		public bool CompressSubdirectories { get; set; } = true;
		public bool IncludeVolumeName { get; set; } = false;

		/*
        private bool mvarExpandToSubdirectory = false;
        public bool ExpandToSubdirectory { get { return mvarExpandToSubdirectory; } set { mvarExpandToSubdirectory = value; } }
        */

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(2);
			if (signature != "FR") throw new InvalidDataFormatException("File does not begin with \"FR\"");

			while (!br.EndOfStream)
			{
				short s0 = br.ReadInt16();
				short s1 = br.ReadInt16();
				short s2 = br.ReadInt16();
				int decompressedSize = br.ReadInt32();
				int compressedSize = br.ReadInt32();
				short s5 = br.ReadInt16();
				short FileNameLength = br.ReadInt16();
				string FileName = br.ReadFixedLengthString(FileNameLength);

				File file = new File();
				file.Properties.Add("offset", br.Accessor.Position);
				file.Properties.Add("length", compressedSize);
				file.Properties.Add("reader", br);
				file.Name = FileName;
				file.Size = decompressedSize;
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);

				br.Accessor.Seek(compressedSize, SeekOrigin.Current);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			int compressedSize = (int)file.Properties["length"];

			br.Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = br.ReadBytes(compressedSize);

			// this has been identified by aluigi's comtype_scan2 as LZH compression
			byte[] decompressedData = UniversalEditor.Compression.LZH.LZHStream.Decompress(compressedData);
			e.Data = decompressedData;
		}

		/*
        private struct FileEntry
        {
            public string name;
            public int decompressedSize;
            public int compressedSize;
            public byte[] decompressedData;
        }
        */

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("FR");

			foreach (File file in fsom.Files)
			{
				short s0 = 0;
				bw.WriteInt16(s0);

				short s1 = 0;
				bw.WriteInt16(s1);

				short s2 = 0;
				bw.WriteInt16(s2);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;

				bw.WriteInt32((int)decompressedData.Length);
				bw.WriteInt32((int)compressedData.Length);

				short s5 = 0;
				bw.WriteInt16(s5);

				bw.WriteInt16((short)file.Name.Length);
				bw.WriteFixedLengthString(file.Name);

				bw.WriteBytes(compressedData);
			}
		}
	}
}
