//
//  PRFDataFormat.cs - provides a DataFormat for manipulating archives in PRF format
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

namespace UniversalEditor.DataFormats.FileSystem.PRF
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in PRF format.
	/// </summary>
	public class PRFDataFormat : DataFormat
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

		/// <summary>
		/// Gets or sets the format version of the PRF file.
		/// </summary>
		/// <value>The format version of the PRF file.</value>
		public uint Version { get; set; } = 0;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			Version = br.ReadUInt32();
			string header = br.ReadFixedLengthString(4);
			if (header != "PRF\0") throw new InvalidDataFormatException("File does not begin with \"PRF\", 0");

			uint unknown1 = br.ReadUInt32(); // 2?
			uint unknown2 = br.ReadUInt32(); // 64?
			uint directorySize = br.ReadUInt32();
			uint directoryOffset = br.ReadUInt32(); // +24

			uint fileCount = (uint)(((directorySize - 24) / 16) - 1);

			while (!br.EndOfStream)
			{
				uint fileType = br.ReadUInt32();
				uint fileID = br.ReadUInt32();
				uint fileOffset = br.ReadUInt32();
				uint fileSize = br.ReadUInt32();

				File file = new File();
				file.Name = fileID.ToString().PadLeft(8, '0');
				file.Size = fileSize;
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			throw new NotImplementedException();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
