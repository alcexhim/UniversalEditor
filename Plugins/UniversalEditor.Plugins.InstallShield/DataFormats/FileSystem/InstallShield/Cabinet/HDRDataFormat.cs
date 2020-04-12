//
//  HDRDataFormat.cs - provides a DataFormat for manipulating InstallShield cabinet header files
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
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.InstallShield.Cabinet
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating InstallShield cabinet header files.
	/// </summary>
	public class HDRDataFormat : DataFormat
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

		private System.IO.FileStream cabfile = null;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.DataFormats.FileSystem.InstallShield.Cabinet.HDRDataFormat"/> should skip
		/// attempting to load the respective cabinet. This should be set to FALSE when using this DataFormat interactively
		/// so that the associated <see cref="FileSystemObjectModel" /> can retrieve data appropriately, but it should be
		/// set to TRUE when using this DataFormat in conjunction with the <see cref="CABDataFormat" />, since that handles
		/// data retrieval itself.
		/// </summary>
		/// <value><c>true</c> if the associated cabinet file should be ignored; otherwise, <c>false</c>.</value>
		public bool IgnoreCabinet { get; set; } = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			// ZLibX compression (see aluigi's code from quickbms)
			if (!IgnoreCabinet)
			{
				string cabfilename = System.IO.Path.ChangeExtension(Accessor.GetFileName(), ".cab");
				if (System.IO.File.Exists(cabfilename))
				{
					cabfile = System.IO.File.Open(cabfilename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
				}
			}

			string signature = reader.ReadFixedLengthString(4);
			if (signature != "ISc(")
				throw new InvalidDataFormatException("invalid CAB or HDR file - does not begin with 'ISc('");

			uint unknown1 = reader.ReadUInt32();
			if (unknown1 != 16798209)
			{
				Console.WriteLine("ue: ISc: warning: magic value does not equal 16798209; this has not been tested");
			}

			uint unknown2 = reader.ReadUInt32();
			uint stringDirectoryOffset = reader.ReadUInt32();
			uint stringDirectoryLength = reader.ReadUInt32();
			if (stringDirectoryLength == 0)
			{
				// proooooobably not a HDR, maybe a CAB?
				throw new InvalidDataFormatException();
			}

			uint totalFileLength = reader.ReadUInt32();

			// rest is padding to offset 512
			// STRINGS DIRECTORY
			reader.Seek(stringDirectoryOffset, SeekOrigin.Begin);
			uint enStrTblOffset = reader.ReadUInt32();
			uint unknownB1 = reader.ReadUInt32();
			uint unknownB2 = reader.ReadUInt32();
			uint strTblLength = reader.ReadUInt32();
			uint unknownB3 = reader.ReadUInt32();
			uint unknownB4 = reader.ReadUInt32();  // 4 - Unknown(337)
			uint unknownB4_2 = reader.ReadUInt32();  // 4 - Unknown(337)
			uint folderCount = reader.ReadUInt32();
			uint unknownB6 = reader.ReadUInt32();
			uint unknownB7 = reader.ReadUInt32();
			uint fileCount = reader.ReadUInt32();
			uint unknownB9 = reader.ReadUInt32();
			uint unknownB10 = reader.ReadUInt32();
			uint unknownB11 = reader.ReadUInt32();

			//	X - Unknown Stuff
			reader.Seek(stringDirectoryOffset + strTblLength, SeekOrigin.Begin);

			string[] folderNames = new string[folderCount];
			for (uint i = 0; i < folderCount; i++)
			{
				uint folderNameOffset = reader.ReadUInt32();
				reader.Accessor.SavePosition();
				reader.Seek(stringDirectoryOffset + strTblLength + folderNameOffset, SeekOrigin.Begin);
				string folderName = ReadName(reader, unknown1);
				reader.Accessor.LoadPosition();
				folderNames[i] = folderName;
			}

			ushort uver = reader.ReadUInt16();
			reader.Seek(-2, SeekOrigin.Current);

			if (uver <= 0x10)
			{
				long CHUNK_SIZE = 0x8000;
				for (uint i = 0; i < fileCount; i++)
				{
					short type = reader.ReadInt16();
					ulong decompressedLength = reader.ReadUInt64();
					ulong compressedLength = reader.ReadUInt64();
					ulong offset = reader.ReadUInt64();

					string hash = reader.ReadFixedLengthString(16);
					ushort dummy1 = reader.ReadUInt16(); // 0 or 1
					ushort dummy2 = reader.ReadUInt16(); // 0 or 0xa
					ushort dummy3 = reader.ReadUInt16(); // 0 or 0xee
					ushort dummy4 = reader.ReadUInt16(); // 0
					ulong zero = reader.ReadUInt64();
					uint nameOffset = reader.ReadUInt32();
					ushort folderIndex = reader.ReadUInt16();
					uint flags = reader.ReadUInt32();
					uint dummy5 = reader.ReadUInt32();
					uint dataID = reader.ReadUInt32(); // duplicate but sometimes it's zero when a cab is not available

					string dummy = reader.ReadFixedLengthString(9);
					ushort dataID2 = reader.ReadUInt16();// yeah it's the same as before

					reader.Accessor.SavePosition();
					reader.Seek(stringDirectoryOffset + strTblLength + nameOffset, SeekOrigin.Begin);
					string fileName = ReadName(reader, unknown1);
					reader.Accessor.LoadPosition();

					File file = fsom.AddFile(fileName);
					file.Properties.Add("offset", offset);
					file.Properties.Add("compressedLength", compressedLength);
					file.Properties.Add("decompressedLength", decompressedLength);
				}
			}
			else
			{
				ulong CHUNK_SIZE = 0x10000;

				for (uint i = 0; i < fileCount; i++)
				{
					uint offset = reader.ReadUInt32();
					reader.Accessor.SavePosition();
					reader.Accessor.Seek(stringDirectoryOffset + strTblLength + offset, SeekOrigin.Begin);
					uint nameOffset = reader.ReadUInt32();
					uint folderIndex = reader.ReadUInt32();
					ushort type = reader.ReadUInt16();
					uint decompressedLength = reader.ReadUInt32();
					uint compressedLength = reader.ReadUInt32();
					uint dummy1 = reader.ReadUInt32();
					uint dummy2 = reader.ReadUInt32();
					uint dummy3 = reader.ReadUInt32();
					ushort dummy4 = reader.ReadUInt16();
					uint dummy5 = reader.ReadUInt32();
					ushort dummy6 = reader.ReadUInt16();
					uint offset2 = reader.ReadUInt32();
					ulong dummt7 = reader.ReadUInt64();
					ulong dummy8 = reader.ReadUInt64();
					uint data_id = 1;

					reader.Seek(stringDirectoryOffset + strTblLength + nameOffset, SeekOrigin.Begin);
					string fileName = ReadName(reader, unknown1);

					reader.Accessor.LoadPosition();

					if (folderIndex > 0)
					{
						fileName = folderNames[folderIndex] + '/' + fileName;
					}

					File file = fsom.AddFile(fileName);
					file.Properties.Add("offset", offset);
					file.Properties.Add("compressedLength", compressedLength);
					file.Properties.Add("decompressedLength", decompressedLength);
					file.Size = decompressedLength;
					file.DataRequest += File_DataRequest; ;
				}
			}
		}

		private void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			if (cabfile == null)
				return;

			File file = (sender as File);
			uint offset = (uint)file.Properties["offset"];
			uint compressedLength = (uint)file.Properties["compressedLength"];
			uint decompressedLength = (uint)file.Properties["decompressedLength"];

			byte[] compressedData = new byte[compressedLength];
			cabfile.Seek(offset, System.IO.SeekOrigin.Begin);
			cabfile.Read(compressedData, 0, compressedData.Length);

			CompressionModule deflatex = CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Deflate);
			byte[] decompressedData = deflatex.Decompress(compressedData, (int)decompressedLength);

			e.Data = decompressedData;
		}


		private string ReadName(Reader reader, uint flags)
		{
			string folderName = null;
			if ((flags & 0x04000000) == 0x04000000)
			{
				folderName = reader.ReadNullTerminatedString(IO.Encoding.UTF16LittleEndian);
			}
			else
			{
				folderName = reader.ReadNullTerminatedString();
			}
			return folderName;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteFixedLengthString("ISc(");
			writer.WriteUInt32(16798209);
		}
	}
}
