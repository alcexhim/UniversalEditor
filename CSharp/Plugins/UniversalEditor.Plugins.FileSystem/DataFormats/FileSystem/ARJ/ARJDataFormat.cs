using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	public class ARJDataFormat : DataFormat
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

		#region Loading
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			
			byte[] signature = br.ReadBytes(2);														// should be 0x60, 0xEA
			if (signature[0] != 0x60 || signature[1] != 0xEA) throw new InvalidDataFormatException("File does not begin with { 0x60, 0xEA }");

			short basicHeaderSize = br.ReadInt16(); // Basic header size (0 if end of archive)
			Internal.ARJBasicHeader hdr = ReadBasicHeader(br);

			byte nul = br.ReadByte(); // always dual-null-terminated for some reason

			int unknown2 = br.ReadInt32();
			short unknown3 = br.ReadInt16();
			byte[] unknown4 = br.ReadBytes(114);
			int unknown5 = br.ReadInt32();

			while (!br.EndOfStream)
			{
				Internal.ARJFileHeader fileheader = ReadFileHeader(br);

				// TODO: Fix this ugly hack.
				if (fileheader.FileName == String.Empty) break;

				byte[] OriginalData = fileheader.CompressedData;

				fsom.Files.Add(fileheader.FileName, OriginalData);
			}
		}

		private Internal.ARJFileHeader ReadFileHeader(IO.Reader br)
		{
			Internal.ARJFileHeader fileheader = new Internal.ARJFileHeader();
			fileheader.HeaderSize = br.ReadByte();														// Size of header including extra data
			fileheader.VersionNumber = br.ReadByte();												// Archiver version number
			fileheader.MinimumRequiredVersion = br.ReadByte();									// Minimum version needed to extract
			fileheader.HostOperatingSystem = (ARJHostOperatingSystem)br.ReadByte();		// Host OS (see table 0002)
			fileheader.InternalFlags = (ARJInternalFlags)br.ReadByte();
			fileheader.CompressionMethod = (ARJCompressionMethod)br.ReadByte();
			fileheader.FileType = (ARJFileType)br.ReadByte();
			fileheader.Reserved = br.ReadByte();
			fileheader.Timestamp = br.ReadInt32();
			fileheader.CompressedSize = br.ReadInt32();
			fileheader.OriginalSize = br.ReadInt32();
			fileheader.OriginalCRC32 = br.ReadInt32();
			fileheader.FileSpecPosition = br.ReadInt16(); // Filespec position in filename
			fileheader.FileAttributes = br.ReadInt16();
			fileheader.HostData = br.ReadInt16();

			fileheader.Unknown1 = br.ReadInt32();
			fileheader.Unknown2 = br.ReadInt32();
			fileheader.Unknown3 = br.ReadInt32();
			fileheader.Unknown4 = br.ReadInt32();

			fileheader.FileName = br.ReadNullTerminatedString();

			byte nul1 = br.ReadByte();

			short unknown13 = br.ReadInt16();
			short unknown14 = br.ReadInt16();
			short unknown15 = br.ReadInt16();
			fileheader.CompressedData = br.ReadBytes(fileheader.CompressedSize);
			int unknown16 = br.ReadInt32();

			return fileheader;
		}
		private Internal.ARJBasicHeader ReadBasicHeader(IO.Reader br)
		{
			Internal.ARJBasicHeader header = new Internal.ARJBasicHeader();
			int basicHeaderSizeWithoutFileName = 36;

			header.HeaderSize = br.ReadByte();														// Size of header including extra data
			header.VersionNumber = br.ReadByte();												// Archiver version number
			header.MinimumRequiredVersion = br.ReadByte();									// Minimum version needed to extract
			header.HostOperatingSystem = (ARJHostOperatingSystem)br.ReadByte();		// Host OS (see table 0002)
			header.InternalFlags = (ARJInternalFlags)br.ReadByte();
			header.CompressionMethod = (ARJCompressionMethod)br.ReadByte();
			header.FileType = (ARJFileType)br.ReadByte();
			header.Reserved = br.ReadByte();
			header.Timestamp = br.ReadInt32();															// Date/Time of original file in MS-DOS format
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

			header.Unknown1 = br.ReadInt32();
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
			bw.WriteBytes(new byte[] { 0x60, 0xEA });

			short basicHeaderSize = 45;
			bw.WriteInt16(basicHeaderSize);																// Basic header size (0 if end of archive)

			Internal.ARJBasicHeader basicHeader = new Internal.ARJBasicHeader();
			basicHeader.HeaderSize = 34;
			basicHeader.VersionNumber = 11;
			basicHeader.MinimumRequiredVersion = 1;
			#region Host Operating System
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.MacOS;
					break;
				}
				case PlatformID.Unix:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.Unix;
					break;
				}
				case PlatformID.Win32Windows:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.Windows;
					break;
				}
				case PlatformID.Win32NT:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.Windows;
					break;
				}
				case PlatformID.Win32S:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.DOS;
					break;
				}
				case PlatformID.WinCE:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.Windows;
					break;
				}
				case PlatformID.Xbox:
				{
					basicHeader.HostOperatingSystem = ARJHostOperatingSystem.Windows;
					break;
				}
			}
			#endregion
			basicHeader.InternalFlags = ARJInternalFlags.None;
			basicHeader.CompressionMethod = ARJCompressionMethod.Store;
			basicHeader.FileType = ARJFileType.CommentHeader;
			
			WriteBasicHeader(bw, basicHeader);

			foreach (File file in fsom.Files)
			{
				Internal.ARJFileHeader fileheader = new Internal.ARJFileHeader();
				fileheader.Timestamp = 0;

				byte[] data = file.GetData();
				fileheader.CompressedSize = data.Length;
				fileheader.OriginalSize = data.Length;
				fileheader.OriginalCRC32 = 0;
				fileheader.FileSpecPosition = 0;
				fileheader.FileAttributes = 0;
				fileheader.HostData = 0;
				fileheader.FileName = file.Name;
				fileheader.CompressedData = data;
				WriteFileHeader(bw, fileheader);
			}
		}

		private void WriteFileHeader(IO.Writer bw, Internal.ARJFileHeader fileheader)
		{
			bw.WriteByte(fileheader.HeaderSize);
			bw.WriteByte(fileheader.VersionNumber);
			bw.WriteByte(fileheader.MinimumRequiredVersion);
			bw.WriteByte((byte)fileheader.HostOperatingSystem);
			bw.WriteByte((byte)fileheader.InternalFlags);
			bw.WriteByte((byte)fileheader.CompressionMethod);
			bw.WriteByte((byte)fileheader.FileType);
			bw.WriteByte((byte)fileheader.Reserved);
			bw.WriteInt32(fileheader.Timestamp);
			bw.WriteInt32(fileheader.CompressedSize);
			bw.WriteInt32(fileheader.OriginalSize);
			bw.WriteInt32(fileheader.OriginalCRC32);
			bw.WriteInt16(fileheader.FileSpecPosition);
			bw.WriteInt16(fileheader.FileAttributes);
			bw.WriteInt16(fileheader.HostData);
			bw.WriteInt32(fileheader.Unknown1);
			bw.WriteInt32(fileheader.Unknown2);
			bw.WriteInt32(fileheader.Unknown3);
			bw.WriteInt32(fileheader.Unknown4);
			bw.WriteNullTerminatedString(fileheader.FileName);
			bw.WriteByte((byte)0);
			short unknown13 = 0;
			short unknown14 = 0;
			short unknown15 = 0;
			bw.WriteInt16(unknown13);
			bw.WriteInt16(unknown14);
			bw.WriteInt16(unknown15);
			bw.WriteBytes(fileheader.CompressedData);
			int unknown16 = 0;
			bw.WriteInt32(unknown16);
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

			bw.WriteInt32(header.Unknown1);
			bw.WriteNullTerminatedString(header.OriginalFileName);
		}
		#endregion
	}
}
