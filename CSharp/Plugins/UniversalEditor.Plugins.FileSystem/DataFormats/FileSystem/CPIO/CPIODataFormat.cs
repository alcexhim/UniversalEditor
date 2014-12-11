using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.CPIO
{
	public class CPIODataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://people.freebsd.org/~kientzle/libarchive/man/cpio.5.txt");
				_dfr.ExportOptions.Add(new CustomOptionChoice("Encoding", "&Encoding: ", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("Binary (little-endian)", CPIOEncoding.BinaryLittleEndian, true),
					new CustomOptionFieldChoice("Binary (big-endian)", CPIOEncoding.BinaryBigEndian),
					new CustomOptionFieldChoice("ASCII", CPIOEncoding.ASCII)
				}));
				_dfr.Filters.Add("CPIO archive", new byte?[][] { new byte?[] { 0xC7, 0x71 }, new byte?[] { 0x71, 0xC7 }, new byte?[] { (byte)'0', (byte)'7', (byte)'0', (byte)'7', (byte)'0', (byte)'1' } }, new string[] { "*.cpio" });
			}
			return _dfr;
		}

		private CPIOEncoding mvarEncoding = CPIOEncoding.BinaryLittleEndian;
		public CPIOEncoding Encoding { get { return mvarEncoding; } set { mvarEncoding = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			while (!reader.EndOfStream)
			{
				byte[] c_magic = reader.ReadBytes(2);
				if (c_magic[0] == 0xC7 && c_magic[1] == 0x71)
				{
					mvarEncoding = CPIOEncoding.BinaryLittleEndian;
				}
				else if (c_magic[0] == 0x71 && c_magic[1] == 0xC7)
				{
					mvarEncoding = CPIOEncoding.BinaryBigEndian;
					reader.Endianness = Endianness.BigEndian;
				}
				else
				{
					base.Accessor.Seek(-2, SeekOrigin.Current);
					string c_magic_str = reader.ReadFixedLengthString(5);
					if (!(c_magic_str == "070701" || c_magic_str == "070702")) throw new InvalidDataFormatException("File does not begin with one of either { 0xC7, 0x71 }, { 0x71, 0xC7 }, { '070701' }, or { '070702' }");

					mvarEncoding = CPIOEncoding.ASCII;
				}

				bool fin = false;

				switch (mvarEncoding)
				{
					case CPIOEncoding.BinaryBigEndian:
					case CPIOEncoding.BinaryLittleEndian:
					{
						ushort c_dev = reader.ReadUInt16();
						ushort c_ino = reader.ReadUInt16();
						ushort c_mode = reader.ReadUInt16();
						ushort c_uid = reader.ReadUInt16();
						ushort c_gid = reader.ReadUInt16();
						ushort c_nlink = reader.ReadUInt16();
						ushort c_rdev = reader.ReadUInt16();
					
						// Modification time of the file, indicated as the number of seconds since the
						// start of the epoch, 00:00:00 UTC January 1, 1970.  The four-byte integer is
						// stored with the most-significant 16 bits first followed by the
						// least-significant 16 bits.  Each of the two 16 bit values are stored in
						// machine-native byte order.
						uint c_mtime = ReadTransEndianUInt32(reader);

						// The number of bytes in the pathname that follows the header. This count
						// includes the trailing NUL byte.
						ushort c_namesize = reader.ReadUInt16();

						// The size of the file.  Note that this archive format is limited to four
						// gigabyte file sizes.  See mtime above for a description of the storage of
						// four-byte integers.
						uint c_filesize = ReadTransEndianUInt32(reader);

						// The pathname immediately follows the fixed header.  If the namesize is odd,
						// an additional NUL byte is added after the pathname.  The file data is then
						// appended, padded with NUL bytes to an even length.
						string c_filename = reader.ReadFixedLengthString(c_namesize).TrimNull();
						reader.Align(2);

						if (c_filename == "TRAILER!!!")
						{
							fin = true;
							break;
						}

						long offset = base.Accessor.Position;
					
						// Hardlinked files are not given special treatment; the full file contents are
						// included with each copy of the file.
						File file = fsom.AddFile(c_filename);
						file.Size = c_filesize;
						file.DataRequest += file_DataRequest;
						file.Properties.Add("reader", reader);
						file.Properties.Add("offset", offset);
						file.Properties.Add("length", c_filesize);

						base.Accessor.Seek(c_filesize, SeekOrigin.Current);
						break;
					}
				}

				if (fin) break;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}

		private uint ReadTransEndianUInt32(Reader reader)
		{
			// The four-byte integer is stored with the most-significant 16 bits first followed by
			// the least-significant 16 bits (big-endian).  Each of the two 16 bit values are stored
			// in machine-native byte order.

			// TODO: TEST THIS!!!

			ushort part1 = reader.ReadUInt16();
			ushort part2 = reader.ReadUInt16();

			byte[] part1bytes = BitConverter.GetBytes(part1);
			byte[] part2bytes = BitConverter.GetBytes(part2);

			uint result = 0;
			switch (reader.Endianness)
			{
				case Endianness.LittleEndian:
				{
					result = BitConverter.ToUInt32(new byte[]
					{
						part2bytes[0], part2bytes[1],
						part1bytes[0], part1bytes[1]
					}, 0);
					break;
				}
				case Endianness.BigEndian:
				{
					result = BitConverter.ToUInt32(new byte[]
					{
						part1bytes[0], part1bytes[1],
						part2bytes[0], part2bytes[1]
					}, 0);
					break;
				}
			}
			return result;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
