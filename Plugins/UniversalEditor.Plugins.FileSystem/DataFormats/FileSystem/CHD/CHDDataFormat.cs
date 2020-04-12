//
//  CHDDataFormat.cs - provides a DataFormat for manipulating archives in Compressed Hunks of Data (CHD) format
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

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Compressed Hunks of Data (CHD) format.
	/// </summary>
	public class CHDDataFormat : DataFormat
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

		public uint FormatVersion { get; set; } = 1;
		public CHDFlags Flags { get; set; } = CHDFlags.None;
		public CHDCompressionType CompressionType { get; set; } = CHDCompressionType.None;
		/// <summary>
		/// Number of 512-byte sectors per hunk.
		/// </summary>
		public uint HunkSize { get; set; } = 0;

		private long m_RawMapOffset = 0;

		/// <summary>
		/// The type of hunk.
		/// </summary>
		internal const byte V34_MAP_ENTRY_FLAG_TYPE_MASK = 0x0f;
		/// <summary>
		/// No CRC is present.
		/// </summary>
		internal const byte V34_MAP_ENTRY_FLAG_NO_CRC = 0x10;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;
			br.Endianness = IO.Endianness.BigEndian;
			string signature = br.ReadFixedLengthString(8);
			if (signature != "MComprHD") throw new InvalidDataFormatException("File does not begin with \"MComprHD\"");

			uint headerLength = br.ReadUInt32();                        // length of header (including tag and length fields)
			FormatVersion = br.ReadUInt32();                        // drive format version
			Flags = (CHDFlags)br.ReadUInt32();                      // flags (see below)
			CompressionType = (CHDCompressionType)br.ReadUInt32();  // compression type

			uint totalHunks = 0;

			if (FormatVersion == 1 || FormatVersion == 2)
			{
				HunkSize = br.ReadUInt32();
			}
			if (FormatVersion < 5)
			{
				totalHunks = br.ReadUInt32();
			}
			if (FormatVersion <= 2)
			{
				uint cylinders = br.ReadUInt32();       // number of cylinders on hard disk
				uint heads = br.ReadUInt32();           // number of heads on hard disk
				uint sectors = br.ReadUInt32();         // number of sectors on hard disk
			}
			else
			{
				ulong logicalBytes = br.ReadUInt64();
				ulong metaOffset = br.ReadUInt64();
			}

			if (FormatVersion <= 3)
			{
				byte[] md5 = br.ReadBytes(16);          // MD5 checksum of raw data
				byte[] parentmd5 = br.ReadBytes(16);    // MD5 checksum of parent file
			}
			if (FormatVersion >= 3)
			{
				HunkSize = br.ReadUInt32();
			}
			if (FormatVersion >= 5)
			{
				uint unitbytes = br.ReadUInt32();           // number of bytes per unit within each hunk
				byte[] rawsha1 = br.ReadBytes(20);          // raw data SHA1
				byte[] sha1 = br.ReadBytes(20);             // combined raw+meta SHA1
				byte[] parentsha1 = br.ReadBytes(20);       // combined raw+meta SHA1 of parent
			}
			else if (FormatVersion >= 3)
			{
				byte[] sha1 = br.ReadBytes(20);             // combined raw+meta SHA1
				byte[] parentsha1 = br.ReadBytes(20);       // combined raw+meta SHA1 of parent
				if (FormatVersion == 4)
				{
					byte[] rawsha1 = br.ReadBytes(20);          // raw data SHA1
				}
			}

			if (m_RawMapOffset == 0) m_RawMapOffset = br.Accessor.Position;

			for (uint i = 0; i < totalHunks; i++)
			{
				File file = new File();
				file.Name = "HUNK" + (i + 1).ToString().PadLeft(4, '0');
				file.Size = HunkSize;
				file.Source = new CHDHunkFileSource(br, i, m_RawMapOffset, HunkSize);
				fsom.Files.Add(file);
			}

		}

		Compression.CompressionModule[] compressionModules = new Compression.CompressionModule[]
		{
			new Compression.Modules.Deflate.DeflateCompressionModule()
		};

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.Endianness = IO.Endianness.BigEndian;

			bw.WriteFixedLengthString("MComprHD");

			uint headerLength = 16;

			// length of header (including tag and length fields)
			bw.WriteUInt32(headerLength);
			// drive format version
			bw.WriteUInt32(FormatVersion);
			bw.WriteUInt32((uint)Flags);
			bw.WriteUInt32((uint)CompressionType);

			uint totalHunks = 0;

			if (FormatVersion == 1 || FormatVersion == 2)
			{
				bw.WriteUInt32(HunkSize);
			}
			if (FormatVersion < 5)
			{
				bw.WriteUInt32(totalHunks);
			}
			if (FormatVersion <= 2)
			{
				// number of cylinders on hard disk
				uint cylinders = 0;
				bw.WriteUInt32(cylinders);
				// number of heads on hard disk
				uint heads = 0;
				bw.WriteUInt32(heads);
				// number of sectors on hard disk
				uint sectors = 0;
				bw.WriteUInt32(sectors);
			}
			else
			{
				ulong logicalBytes = 0;
				bw.WriteUInt64(logicalBytes);
				ulong metaOffset = 0;
				bw.WriteUInt64(metaOffset);
			}

			if (FormatVersion <= 3)
			{
				// MD5 checksum of raw data
				byte[] md5 = new byte[16];
				bw.WriteBytes(md5);

				// MD5 checksum of parent file
				byte[] parentmd5 = new byte[16];
				bw.WriteBytes(parentmd5);
			}
			if (FormatVersion >= 3)
			{
				bw.WriteUInt32(HunkSize);
			}
			if (FormatVersion >= 5)
			{
				uint unitbytes = 0;         // number of bytes per unit within each hunk
				bw.WriteUInt32(unitbytes);
				byte[] rawsha1 = new byte[20];          // raw data SHA1
				bw.WriteBytes(rawsha1);

				byte[] sha1 = new byte[20];             // combined raw+meta SHA1
				bw.WriteBytes(sha1);

				byte[] parentsha1 = new byte[20];       // combined raw+meta SHA1 of parent
				bw.WriteBytes(parentsha1);
			}
			else if (FormatVersion >= 3)
			{
				byte[] sha1 = new byte[20];             // combined raw+meta SHA1
				bw.WriteBytes(sha1);

				byte[] parentsha1 = new byte[20];       // combined raw+meta SHA1 of parent
				bw.WriteBytes(parentsha1);

				if (FormatVersion == 4)
				{
					byte[] rawsha1 = new byte[20];          // raw data SHA1
					bw.WriteBytes(rawsha1);
				}
			}
		}
	}
}
