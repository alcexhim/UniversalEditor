//
//  PCKDataFormat.cs - provides a DataFormat for manipulating archives in Lost Heroes PCK format
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

namespace UniversalEditor.DataFormats.FileSystem.PCK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Lost Heroes PCK format.
	/// </summary>
	public class PCKDataFormat : DataFormat
	{
		/*
        # Game: Lost Heroes [PSP]
        # by Fatduck     Sept 2012
        # http://aluigi.org/quickbms.htm

        idstring "2NBF"
        get NUMRES long
        get OFSBASE long
        get BLKSIZE long

        for i = 0 < NUMRES
           getdstring RESNAME 0x80
           get OFSRES long
           get NULL long
           get SIZERES long
           math OFSRES += OFSBASE
           log RESNAME OFSRES SIZERES
        next i
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
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			if (signature != "2NBF") throw new InvalidDataFormatException("File does not begin with 2NBF");

			int fileCount = br.ReadInt32();
			int baseOffset = br.ReadInt32();
			int blockSize = br.ReadInt32();

			for (int i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(128).TrimNull();
				int offset = br.ReadInt32();
				int blank = br.ReadInt32();
				int length = br.ReadInt32();
				offset += baseOffset;

				File file = new File();
				file.Name = fileName;
				file.Size = length;
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			int offset = (int)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("2NBF");

			bw.WriteInt32((int)fsom.Files.Count);
			int baseOffset = 12 + (140 * fsom.Files.Count);
			bw.WriteInt32(baseOffset);

			int blockSize = 0;
			bw.WriteInt32(blockSize);

			int offset = 0;
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 128);
				bw.WriteInt32(offset);
				bw.WriteInt32((int)0);
				bw.WriteInt32((int)file.Size);
				offset += (int)file.Size;
			}

			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}

			bw.Flush();
		}
	}
}
