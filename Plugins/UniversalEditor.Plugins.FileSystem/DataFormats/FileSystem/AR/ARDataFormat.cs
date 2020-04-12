//
//  ARDataFormat.cs - provides a DataFormat for manipulating archives in AR format
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
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.AR
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in AR format.
	/// </summary>
	public class ARDataFormat : DataFormat
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
			if (fsom == null) return;

			IO.Reader reader = base.Accessor.Reader;
			string sig_arch = reader.ReadFixedLengthString(7);
			byte sig_mustBe0x0A = reader.ReadByte();

			if (sig_arch != "!<arch>" || sig_mustBe0x0A != 0x0A) throw new InvalidDataFormatException("File does not begin with !<arch>, 0x1A");

			while (!reader.EndOfStream)
			{
				string szFileName = reader.ReadFixedLengthString(0x10).Trim();

				if (szFileName == "\0") break;

				if (szFileName.EndsWith("/"))
				{
					// GNU ar uses a '/' to mark the end of the filename; this allows for the use of spaces without
					// the use of an extended filename.
					szFileName = szFileName.Substring(0, szFileName.Length - 1);
				}

				if (String.IsNullOrEmpty(szFileName))
				{
					// for some reason, this happens in .a archives
					// 7-Zip File Manager gives the blank file the archive name without the .a extension,
					// so that's what we'll do.
					szFileName = System.IO.Path.GetFileNameWithoutExtension((base.Accessor as FileAccessor).FileName); //; (fsom.Files.Count + 1).ToString().PadLeft(8, '0');
				}

				string fileModTimestamp = reader.ReadFixedLengthString(12).Trim();
				string ownerID = reader.ReadFixedLengthString(6).Trim();
				string groupID = reader.ReadFixedLengthString(6).Trim();
				string fileMode = reader.ReadFixedLengthString(8).Trim();

				string szFileSize = reader.ReadFixedLengthString(10).Trim();
				int fileSize = Int32.Parse(szFileSize);

				string fileMagic = reader.ReadFixedLengthString(2);

				long offset = reader.Accessor.Position;
				reader.Seek(fileSize, IO.SeekOrigin.Current);

				File file = fsom.AddFile(szFileName);
				file.Size = fileSize;
				file.Source = new EmbeddedFileSource(reader, offset, fileSize);

				if ((reader.Accessor.Position % 2) != 0)
				{
					// fixed 2013-05-20 for certain .a files
					// The data section is 2 byte aligned. If it would end on an odd offset, a '\n' is used as filler.
					char xA = reader.ReadChar();
				}
			}
		}

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			Reader reader = (Reader)file.Properties["reader"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("!<arch>");
			bw.WriteByte((byte)0x0A);
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name.PadRight(0x10, ' '));

				bw.WriteFixedLengthString(new string(' ', 12));          // file modification timestamp
				bw.WriteFixedLengthString(new string(' ', 6));           // owner ID
				bw.WriteFixedLengthString(new string(' ', 6));           // group ID
				bw.WriteFixedLengthString(new string(' ', 8));           // file mode

				// file size
				bw.WriteFixedLengthString(file.Size.ToString().PadLeft(10, ' '), 10);

				bw.WriteByte((byte)0x60);
				bw.WriteByte((byte)10);

				file.WriteTo(bw);
				if ((bw.Accessor.Position % 2) != 0)
				{
					// fixed 2015-07-23 to match LoadInternal fix 2013-05-20 for certain .a files
					// The data section is 2 byte aligned. If it would end on an odd offset, a '\n' is used as filler.
					bw.WriteByte(0xA);
				}
			}
		}
	}
}
