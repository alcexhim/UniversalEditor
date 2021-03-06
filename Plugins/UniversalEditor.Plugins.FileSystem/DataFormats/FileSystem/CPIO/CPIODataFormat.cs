//
//  CPIODataFormat.cs - provides a DataFormat for manipulating archives in CPIO format
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
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.CPIO
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in CPIO format.
	/// </summary>
	public class CPIODataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://people.freebsd.org/~kientzle/libarchive/man/cpio.5.txt");
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(Encoding), "_Encoding: ", CPIOEncoding.BinaryLittleEndian, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("BinaryLittleEndian", "Binary (little-endian)", CPIOEncoding.BinaryLittleEndian),
					new ChoiceSetting.ChoiceSettingValue("BinaryBigEndian", "Binary (big-endian)", CPIOEncoding.BinaryBigEndian),
					new ChoiceSetting.ChoiceSettingValue("ASCII", "ASCII", CPIOEncoding.ASCII)
				}));
			}
			return _dfr;
		}

		public CPIOEncoding Encoding { get; set; } = CPIOEncoding.BinaryLittleEndian;

		private static DateTime UNIX_EPOCH { get; } = new DateTime(1970, 01, 01, 00, 00, 00).ToUniversalTime();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			bool firstone = true;
			while (!reader.EndOfStream)
			{
				byte[] c_magic = reader.ReadBytes(2);
				if (c_magic[0] == 0xC7 && c_magic[1] == 0x71)
				{
					Encoding = CPIOEncoding.BinaryLittleEndian;
				}
				else if (c_magic[0] == 0x71 && c_magic[1] == 0xC7)
				{
					Encoding = CPIOEncoding.BinaryBigEndian;
					reader.Endianness = Endianness.BigEndian;
				}
				else
				{
					base.Accessor.Seek(-2, SeekOrigin.Current);
					string c_magic_str = reader.ReadFixedLengthString(5);
					if (!(c_magic_str == "070701" || c_magic_str == "070702"))
					{
						if (firstone)
							throw new InvalidDataFormatException("File does not begin with one of either { 0xC7, 0x71 }, { 0x71, 0xC7 }, { '070701' }, or { '070702' }");
					}

					Encoding = CPIOEncoding.ASCII;
				}

				bool fin = false;

				switch (Encoding)
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
						if ((c_namesize % 2) != 0)
							reader.ReadByte();

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
						file.ModificationTimestamp = UNIX_EPOCH.AddSeconds(c_mtime).ToLocalTime();
						file.Size = c_filesize;
						file.Source = new EmbeddedFileSource(reader, offset, c_filesize);

						base.Accessor.Seek(c_filesize, SeekOrigin.Current);

						// old version didn't do this (and failed epically); apparently we need to be aligned after skipping file data too
						reader.Align(2);
						break;
					}
				}
				firstone = false;

				if (fin) break;
			}
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
