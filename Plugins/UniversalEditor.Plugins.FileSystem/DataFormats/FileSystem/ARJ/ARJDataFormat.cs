//
//  ARJDataFormat.cs - provides a DataFormat for manipulating archives in ARJ format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in ARJ format.
	/// </summary>
	public class ARJDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://www.fileformat.info/format/arj/corion.htm");
			}
			return _dfr;
		}

		private Checksum.Modules.CRC32.CRC32ChecksumModule crc = new Checksum.Modules.CRC32.CRC32ChecksumModule();

		#region Loading
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;

			ushort signature = br.ReadUInt16();
			if (signature != 0xEA60)
				throw new InvalidDataFormatException("File does not begin with 0xEA60");

			short basicHeaderSize = br.ReadInt16(); // Basic header size (0 if end of archive) 44 in test
			Internal.ARJBasicHeader hdr = ReadBasicHeader(br);

			byte nul = br.ReadByte(); // always dual-null-terminated for some reason

			while (!br.EndOfStream)
			{
				Internal.ARJFileHeader fileheader = ReadFileHeader(br);

				// TODO: Fix this ugly hack.
				if (fileheader.FileName == String.Empty) break;

				File f = fsom.AddFile(fileheader.FileName);
				f.Size = fileheader.OriginalSize;
				f.Properties["reader"] = br;
				f.Properties["offset"] = br.Accessor.Position;
				f.Properties["CompressionMethod"] = fileheader.CompressionMethod;
				f.Properties["CompressedLength"] = fileheader.CompressedSize;
				f.Properties["DecompressedLength"] = fileheader.OriginalSize;
				f.DataRequest += F_DataRequest;

				// skip over file data
				br.Seek(fileheader.CompressedSize, IO.SeekOrigin.Current);
			}
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (File)sender;

			Reader br = (Reader)f.Properties["reader"];
			long offset = (long)f.Properties["offset"];
			ARJCompressionMethod compressionMethod = (ARJCompressionMethod)f.Properties["CompressionMethod"];
			uint compressedLength = (uint)f.Properties["CompressedLength"];
			uint decompressedLength = (uint)f.Properties["DecompressedLength"];

			br.Seek(offset, SeekOrigin.Begin);

			byte[] unk = br.ReadBytes(6);

			byte[] compressedData = br.ReadBytes(compressedLength);
			byte[] decompressedData = compressedData;
			e.Data = decompressedData;
		}


		private Internal.ARJFileHeader ReadFileHeader(IO.Reader br)
		{
			Internal.ARJFileHeader fileheader = new Internal.ARJFileHeader();

			byte[] junk = br.ReadBytes(6); // idek

			ushort sig = br.ReadUInt16();
			if (sig != 0xEA60)
				throw new InvalidDataFormatException("file header does not begin with 0xEA60");

			ushort headerSz = br.ReadUInt16();

			fileheader.HeaderSize = br.ReadByte();                                                      // Size of header including extra data
			fileheader.VersionNumber = br.ReadByte();                                               // Archiver version number
			fileheader.MinimumRequiredVersion = br.ReadByte();                                  // Minimum version needed to extract
			fileheader.HostOperatingSystem = (ARJHostOperatingSystem)br.ReadByte();     // Host OS (see table 0002)
			fileheader.InternalFlags = (ARJInternalFlags)br.ReadByte();
			fileheader.CompressionMethod = (ARJCompressionMethod)br.ReadByte();
			fileheader.FileType = (ARJFileType)br.ReadByte();
			fileheader.Reserved = br.ReadByte();
			fileheader.Timestamp = br.ReadInt32();
			fileheader.CompressedSize = br.ReadUInt32();
			fileheader.OriginalSize = br.ReadUInt32();
			fileheader.OriginalCRC32 = br.ReadInt32();
			fileheader.FileSpecPosition = br.ReadInt16(); // Filespec position in filename
			fileheader.FileAttributes = br.ReadInt16();
			fileheader.HostData = br.ReadInt16();

			if ((fileheader.InternalFlags & ARJInternalFlags.StartPositionFieldAvailable) == ARJInternalFlags.StartPositionFieldAvailable)
			{
				uint extFieStartPos = br.ReadUInt32();
			}

			byte[] unknown = br.ReadBytes(16);

			fileheader.FileName = br.ReadNullTerminatedString();
			byte nul = br.ReadByte();
			return fileheader;
		}
		private Internal.ARJBasicHeader ReadBasicHeader(IO.Reader br)
		{
			Internal.ARJBasicHeader header = new Internal.ARJBasicHeader();
			int basicHeaderSizeWithoutFileName = 36;

			header.HeaderSize = br.ReadByte(); // Size of header including extra data, not including original filename

			header.VersionNumber = br.ReadByte();                                               // Archiver version number
			header.MinimumRequiredVersion = br.ReadByte();                                  // Minimum version needed to extract
			header.HostOperatingSystem = (ARJHostOperatingSystem)br.ReadByte();     // Host OS (see table 0002)

			header.InternalFlags = (ARJInternalFlags)br.ReadByte();
			header.CompressionMethod = (ARJCompressionMethod)br.ReadByte();
			header.FileType = (ARJFileType)br.ReadByte();
			header.Reserved = br.ReadByte();
			header.Timestamp = br.ReadInt32();                                                          // Date/Time of original file in MS-DOS format
			header.CompressedSize = br.ReadInt32();
			header.OriginalSize = br.ReadInt32();
			header.OriginalCRC32 = br.ReadInt32();
			header.FileSpecPosition = br.ReadInt16(); // Filespec position in filename
			header.FileAttributes = br.ReadInt16();
			header.HostData = br.ReadInt16();

			if ((header.InternalFlags & ARJInternalFlags.StartPositionFieldAvailable) == ARJInternalFlags.StartPositionFieldAvailable)
			{
				int extendedFileStartingPosition = br.ReadInt32();

				long pos = br.Accessor.Position;

				br.Accessor.Position = extendedFileStartingPosition;
				string fileName = br.ReadNullTerminatedString();
				string comment = br.ReadNullTerminatedString();
				br.Accessor.Position = pos;
			}

			header.BasicHeaderCRC32 = br.ReadInt32();
			header.OriginalFileName = br.ReadNullTerminatedString();
			return header;
		}
		#endregion
		#region Saving
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt16(0xEA60);

			string filename = System.IO.Path.GetFileName(Accessor.GetFileName());
			ushort basicHeaderSize = (ushort)(34 + filename.Length + 2);
			bw.WriteUInt16(basicHeaderSize);                                                                // Basic header size (0 if end of archive)

			Internal.ARJBasicHeader basicHeader = new Internal.ARJBasicHeader();
			basicHeader.HeaderSize = 34;
			basicHeader.VersionNumber = 11;
			basicHeader.MinimumRequiredVersion = 1;
			basicHeader.HostOperatingSystem = GetHostOperatingSystem();
			basicHeader.InternalFlags = ARJInternalFlags.PathTranslation;
			basicHeader.CompressionMethod = ARJCompressionMethod.Store;
			basicHeader.FileType = ARJFileType.CommentHeader;
			basicHeader.Reserved = 0x49;
			basicHeader.Timestamp = 0x5E885D0A;
			basicHeader.CompressedSize = 0x5E885D0A;
			basicHeader.OriginalFileName = filename;

			WriteBasicHeader(bw, basicHeader);
			bw.WriteBytes(new byte[6]);

			foreach (File file in fsom.Files)
			{
				Internal.ARJFileHeader fileheader = new Internal.ARJFileHeader();
				fileheader.Timestamp = 0;

				byte[] data = file.GetData();
				fileheader.HeaderSize = 46;
				fileheader.VersionNumber = 11;
				fileheader.MinimumRequiredVersion = 1;
				fileheader.HostOperatingSystem = GetHostOperatingSystem();
				fileheader.InternalFlags = ARJInternalFlags.PathTranslation;
				fileheader.CompressionMethod = ARJCompressionMethod.Store;
				fileheader.FileType = ARJFileType.Binary;
				fileheader.Reserved = 0x49;
				fileheader.Timestamp = 0x5E885D0A;
				fileheader.CompressedSize = (uint)data.Length;
				fileheader.OriginalSize = (uint)data.Length;
				fileheader.OriginalCRC32 = (int)crc.Calculate(data);
				fileheader.FileSpecPosition = 0;
				fileheader.FileAttributes = 0;
				fileheader.HostData = 0;
				fileheader.FileName = file.Name;

				WriteFileHeader(bw, fileheader);

				bw.WriteBytes(new byte[6]);
				bw.WriteBytes(data);
			}

			bw.WriteUInt32(0x0000EA60); // terminator (60 EA 00 00)
		}

		private ARJHostOperatingSystem GetHostOperatingSystem()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				{
					return ARJHostOperatingSystem.MacOS;
				}
				case PlatformID.Unix:
				{
					return ARJHostOperatingSystem.Unix;
				}
				case PlatformID.Win32Windows:
				case PlatformID.Win32NT:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					return ARJHostOperatingSystem.Windows;
				}
				case PlatformID.Win32S:
				{
					return ARJHostOperatingSystem.DOS;
				}
			}
			return ARJHostOperatingSystem.DOS;
		}

		private void WriteFileHeader(IO.Writer bw, Internal.ARJFileHeader fileheader)
		{
			bw.WriteUInt16(0xEA60);
			bw.WriteUInt16((ushort)(fileheader.HeaderSize + fileheader.FileName.Length + 2));

			bw.WriteByte(fileheader.HeaderSize);
			bw.WriteByte(fileheader.VersionNumber);
			bw.WriteByte(fileheader.MinimumRequiredVersion);
			bw.WriteByte((byte)fileheader.HostOperatingSystem);
			bw.WriteByte((byte)fileheader.InternalFlags);
			bw.WriteByte((byte)fileheader.CompressionMethod);
			bw.WriteByte((byte)fileheader.FileType);
			bw.WriteByte((byte)fileheader.Reserved);
			bw.WriteInt32(fileheader.Timestamp);
			bw.WriteUInt32(fileheader.CompressedSize);
			bw.WriteUInt32(fileheader.OriginalSize);
			bw.WriteInt32(fileheader.OriginalCRC32);
			bw.WriteInt16(fileheader.FileSpecPosition);
			bw.WriteInt16(fileheader.FileAttributes);
			bw.WriteInt16(fileheader.HostData);

			bw.WriteUInt32(0);
			bw.WriteUInt32(0);
			bw.WriteUInt32(0);
			bw.WriteUInt32(0);

			bw.WriteNullTerminatedString(fileheader.FileName);
			bw.WriteByte((byte)0);
		}
		private void WriteBasicHeader(IO.Writer bw, Internal.ARJBasicHeader header)
		{
			bw.WriteByte(header.HeaderSize);
			bw.WriteByte(header.VersionNumber);
			bw.WriteByte(header.MinimumRequiredVersion);
			bw.WriteByte((byte)header.HostOperatingSystem);
			bw.WriteByte((byte)header.InternalFlags);
			bw.WriteByte((byte)header.CompressionMethod);
			bw.WriteByte((byte)header.FileType);
			bw.WriteByte(header.Reserved);
			bw.WriteInt32(header.Timestamp);
			bw.WriteInt32(header.CompressedSize);
			bw.WriteInt32(header.OriginalSize);
			bw.WriteInt32(header.OriginalCRC32);
			bw.WriteInt16(header.FileSpecPosition);
			bw.WriteInt16(header.FileAttributes);
			bw.WriteInt16(header.HostData);

			if ((header.InternalFlags & ARJInternalFlags.StartPositionFieldAvailable) == ARJInternalFlags.StartPositionFieldAvailable)
			{
				int extendedFileStartingPosition = 0;
				bw.WriteInt32(extendedFileStartingPosition);

				// long pos = br.Accessor.Position;

				// br.Accessor.Position = extendedFileStartingPosition;
				// string fileName = br.ReadNullTerminatedString();
				// string comment = br.ReadNullTerminatedString();
				// br.Accessor.Position = pos;
			}

			bw.WriteInt32(header.BasicHeaderCRC32);
			bw.WriteNullTerminatedString(header.OriginalFileName);
			bw.WriteByte(0);
		}
		#endregion
	}
}
