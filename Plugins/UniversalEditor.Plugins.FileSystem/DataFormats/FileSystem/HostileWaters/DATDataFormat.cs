//
//  DATDataFormat.cs - provides a DataFormat for manipulating archives in Hostile Waters DAT format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Hostile Waters DAT format.
	/// </summary>
	public class DATDataFormat : DataFormat
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

		/// <summary>
		/// Gets or sets the full path to the companion MBX file that contains the actual file data.
		/// </summary>
		/// <value>The full path to the companion MBX file that contains the actual file data.</value>
		public string MBXFileName { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			if (!(base.Accessor is FileAccessor) && MBXFileName == null) throw new InvalidOperationException("Requires a file reference or known MBX file path");

			IO.Reader br = base.Accessor.Reader;
			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(12);
				if (String.IsNullOrEmpty(fileName)) fileName = (i + 1).ToString().PadLeft(8, '0');

				uint fileLength = br.ReadUInt32();
				uint fileOffset = br.ReadUInt32();

				if ((fileOffset + fileLength) >= br.Accessor.Length) throw new InvalidDataFormatException("File offset + length is too large");

				File file = new File();
				file.Name = fileName;
				file.Size = fileLength;
				file.Source = new MBXFileSource(MBXFileName, fileOffset, fileLength);

				fsom.Files.Add(file);
			}
		}

		private Writer mbxWriter = null;

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt32((uint)fsom.Files.Count);
			uint offset = 0;
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 12);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32(offset);
				offset += (uint)file.Size;
			}

			if (mbxWriter == null)
			{
				if (!(base.Accessor is FileAccessor) && this.MBXFileName == null) throw new InvalidOperationException("Requires a file reference");

				string MBXFileName = null;
				if (this.MBXFileName != null)
				{
					MBXFileName = this.MBXFileName;
				}
				else
				{
					FileAccessor acc = (base.Accessor as FileAccessor);
					MBXFileName = System.IO.Path.ChangeExtension(acc.FileName, "mbx");
				}

				mbxWriter = new Writer(new FileAccessor(MBXFileName, true, true));
			}
			mbxWriter.Accessor.Seek(0, SeekOrigin.Begin);
			foreach (File file in fsom.Files)
			{
				file.WriteTo(mbxWriter);
			}
			mbxWriter.Flush();
			mbxWriter.Close();
			mbxWriter = null;
		}
	}
}
