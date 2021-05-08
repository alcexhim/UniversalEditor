//
//  BAGDataFormat.cs - provides a DataFormat for manipulating archives in BAG format
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

namespace UniversalEditor.DataFormats.FileSystem.BAG
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in BAG format. BAG archive by Jeff Connelly. The main goal of bag archives is to provide
	/// a simple and efficient way to combine many files into one. For that reason, it is extremely simple.
	/// </summary>
	public class BAGDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Reader br = base.Accessor.Reader;

			// The header is at the beginning of the file
			string BAG = br.ReadFixedLengthString(3);
			string version = br.ReadFixedLengthString(2); // 11?

			Folder curDir = null;

			while (br.PeekByte() != 0x1A)
			{
				// There is one file block for each file
				int fileLength = br.ReadInt32(); // file length in bytes
				byte fileNameLength = br.ReadByte();
				string fileName = br.ReadFixedLengthString(fileNameLength);
				byte[] fileContents = br.ReadBytes(fileLength);

				// A file length of zero in a file block indicates a special file that will be
				// used as a description.  Directorys are also stored in the filename field:
				// Filename='> dirname' means change current directory to 'dirname'.
				// For example,
				// Filename='> JEFF'
				// Filename='> COMPUTER'
				// Filename='> BAG'
				// Would make the directory be JEFF\COMPUTER\BAG.  Files are put in the current
				// directory.
				if (fileName.StartsWith("> "))
				{
					string dirName = fileName.Substring(2);
					if (curDir == null)
					{
						curDir = fsom.Folders.Add(dirName);
					}
					else
					{
						curDir = curDir.Folders.Add(dirName);
					}
					continue;
				}

				// The files contents is not always raw data, it can be compressed. A
				// four-byte signature can specify the compression scheme:
				// ---- Compression ----
				// "LZW "	LZW encoded (note extra space at end)
				// "RLEn"	RLE method N (1 to 4) encoded
				// "HUFF"	Huffman encoded
				// "LZHF"	LZHUF encoded (LZSS + Arithmetic) (Note - the authors of LZHUF do
				//			not allow using it for any commercial purpose)
				// "LZAR"	LZARI encoded (LZSS + Huffman)
				// "WCOD"	Word coded -- text only
				// If no signature is found it is assumed to be raw data.
				//
				// The word coding compression is as follows:
				// * Initalize dictionary (max. size FFFF)
				// * Loop until end-of-file
				// * Read a space-delimited word from the input stream
				// * Search for the word in the dictionary
				// * If it is not there, add it to the first free space and output location
				//   of where in the dictionary the word was added as a 16-bit integer.
				// * If it is there, output the location of where it is as a 16-bit
				//   integer.
				// * Write dictionary at end of file. Each word is null-terminated, whole
				//   dictionary is double-null terminated.
				// This means that each word will be encoded as 2 bytes.

				if (curDir == null)
				{
					fsom.Files.Add(fileName, fileContents);
				}
				else
				{
					curDir.Files.Add(fileName, fileContents);
				}
			}

			// The last character is an end-of-file character to ensure the whole file was
			// received when transferring this archive.
			byte eof = br.ReadByte(); // 0x1A

			if (eof != 0x1A)
			{
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;

			// The header is at the beginning of the file
			bw.WriteFixedLengthString("BAG");
			bw.WriteFixedLengthString("11");

			foreach (File file in fsom.Files)
			{
				// There is one file block for each file
				bw.WriteInt32((int)file.Size);

				byte fileNameLength = (byte)file.Name.Length;
				bw.WriteByte(fileNameLength);

				string fileName = file.Name;
				if (fileName.Length > fileNameLength) fileName = fileName.Substring(0, fileNameLength);
				bw.WriteFixedLengthString(fileName);

				file.WriteTo(bw);
			}

			// The last character is an end-of-file character to ensure the whole file was
			// received when transferring this archive.
			bw.WriteByte((byte)0x1A);
		}
	}
}
