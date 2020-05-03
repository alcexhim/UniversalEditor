//
//  RARBlockDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using UniversalEditor.DataFormats.FileSystem.WinRAR.Blocks;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	public class RARBlockDataFormat : DataFormat
	{
		public RARFormatVersion FormatVersion { get; set; } = RARFormatVersion.Modern;

		private long FindStartOfRar(Reader reader)
		{
			while (!reader.EndOfStream)
			{
				string Rar = reader.ReadFixedLengthString(4);
				if (Rar == "\x52\x45\x7e\x5e")
				{
					FormatVersion = RARFormatVersion.Ancient;
					return reader.Accessor.Position;
				}
				else if (Rar == "Rar!")
				{
					ushort a10 = reader.ReadUInt16();
					if (a10 != 0x071A) throw new InvalidDataFormatException("Invalid block header");

					byte a11 = reader.ReadByte(); // 01?
					if (a11 == 1)
					{
						byte a12 = reader.ReadByte();
						if (a12 != 0) throw new InvalidDataFormatException("Invalid block header");

						FormatVersion = RARFormatVersion.Enhanced;
						return reader.Accessor.Position;
					}
					else if (a11 == 0)
					{
					}
					else
					{
						throw new InvalidDataFormatException("Invalid block header");
					}

					FormatVersion = RARFormatVersion.Modern;
					return reader.Accessor.Position;
				}
				else
				{
					reader.Seek(-3, SeekOrigin.Current);
					continue;
				}
			}
			return -1;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			RARBlockObjectModel bom = (objectModel as RARBlockObjectModel);

			Reader reader = Accessor.Reader;
			long rarStart = FindStartOfRar(reader);
			if (rarStart == -1)
				throw new InvalidDataFormatException("could not find start of RAR data");

			reader.Accessor.Seek(rarStart, SeekOrigin.Begin);

			while (!reader.EndOfStream)
			{
				switch (FormatVersion)
				{
					case RARFormatVersion.Modern:
					{
						//RARv4
						ushort head_crc = reader.ReadUInt16();
						V4.RARBlockTypeV4 headerType = (V4.RARBlockTypeV4)reader.ReadByte();
						V4.RARArchiveBlockFlagsV4 head_flags = (V4.RARArchiveBlockFlagsV4)reader.ReadUInt16();
						ushort head_size = reader.ReadUInt16();

						if (reader.EndOfStream) break;

						switch (headerType)
						{
							case V4.RARBlockTypeV4.Archive:
							{
								RARArchiveBlock block = new RARArchiveBlock();
								block.crc = head_crc;
								block.headerType = RARBlockTypeV4ToRARBlockType(headerType);
								block.size = head_size;

								ushort reserved1 = reader.ReadUInt16();
								uint reserved2 = reader.ReadUInt32();

								bom.Blocks.Add(block);
								break;
							}
							case V4.RARBlockTypeV4.File:
							{
								RARFileBlock block = new RARFileBlock();
								block.crc = head_crc;
								block.headerType = RARBlockTypeV4ToRARBlockType(headerType);
								block.size = head_size;

								block.dataSize = reader.ReadUInt32();
								block.unpackedSize = reader.ReadUInt32();
								block.hostOperatingSystem = (RARHostOperatingSystem)reader.ReadByte();
								block.dataCrc = reader.ReadUInt32();
								block.mtime = reader.ReadUInt32();

								// Version number is encoded as 10 * Major version + minor version.
								byte requiredVersionToUnpack = reader.ReadByte();

								RARCompressionMethod compressionMethod = (RARCompressionMethod)reader.ReadByte();
								block.fileNameLength = reader.ReadUInt16();
								uint fileAttributes = reader.ReadUInt32();

								if (((RARFileHeaderFlags)head_flags & RARFileHeaderFlags.SupportLargeFiles) == RARFileHeaderFlags.SupportLargeFiles)
								{
									// High 4 bytes of 64 bit value of compressed file size.
									uint highPackSize = reader.ReadUInt32();
									// High 4 bytes of 64 bit value of uncompressed file size.
									uint highUnpackSize = reader.ReadUInt32();
								}

								block.fileName = reader.ReadFixedLengthString(block.fileNameLength);
								byte nul = reader.ReadByte();

								if (((RARFileHeaderFlags)head_flags & RARFileHeaderFlags.EncryptionSaltPresent) == RARFileHeaderFlags.EncryptionSaltPresent)
								{
									long salt = reader.ReadInt64();
								}

								if (((RARFileHeaderFlags)head_flags & RARFileHeaderFlags.ExtendedTimeFieldPresent) == RARFileHeaderFlags.ExtendedTimeFieldPresent)
								{
									uint exttime = reader.ReadUInt32();

								}

								long offset = reader.Accessor.Position;
								block.dataOffset = offset;

								reader.Seek(block.dataSize, SeekOrigin.Current);

								if (!(block.unpackedSize == 0 && block.dataSize == 0))
								{
									// if both these fields are zero, we don't care - it's a folder record
									// otherwise, it's a file record and we need to add it
									bom.Blocks.Add(block);
								}
								break;
							}
						}

						break;
					}
					case RARFormatVersion.Enhanced:
					{
						//RARv5
						uint crc = reader.ReadUInt32();
						long size = reader.Read7BitEncodedInt();
						V5.RARBlockTypeV5 headerType = (V5.RARBlockTypeV5)reader.Read7BitEncodedInt();
						RARBlockFlags headerFlags = (RARBlockFlags)reader.Read7BitEncodedInt();
						long extraAreaSize = 0, dataSize = 0;

						if ((headerFlags & RARBlockFlags.ExtraAreaPresent) == RARBlockFlags.ExtraAreaPresent)
						{
							extraAreaSize = reader.Read7BitEncodedInt();
						}
						if ((headerFlags & RARBlockFlags.DataAreaPresent) == RARBlockFlags.DataAreaPresent)
						{
							dataSize = reader.Read7BitEncodedInt();
						}

						switch (headerType)
						{
							case V5.RARBlockTypeV5.Main:
							{
								RARArchiveBlock header = new RARArchiveBlock();
								header.crc = crc;
								header.size = size;
								header.headerType = RARBlockTypeV5ToRARBlockType(headerType);
								header.headerFlags = headerFlags;
								header.extraAreaSize = extraAreaSize;
								header.dataSize = dataSize;

								((RARArchiveBlock)header).archiveFlags = (V5.RARArchiveBlockFlagsV5)reader.Read7BitEncodedInt();
								if ((((RARArchiveBlock)header).archiveFlags & V5.RARArchiveBlockFlagsV5.VolumeNumberFieldPresent) == V5.RARArchiveBlockFlagsV5.VolumeNumberFieldPresent)
								{
									((RARArchiveBlock)header).volumeNumber = reader.Read7BitEncodedInt();
								}

								bom.Blocks.Add(header);
								break;
							}
							case V5.RARBlockTypeV5.File:
							case V5.RARBlockTypeV5.Service:
							{
								RARFileBlock header = new RARFileBlock();
								header.crc = crc;
								header.size = size;
								header.headerType = RARBlockTypeV5ToRARBlockType(headerType);
								header.headerFlags = headerFlags;
								header.extraAreaSize = extraAreaSize;
								header.dataSize = dataSize;

								((RARFileBlock)header).fileFlags = (V5.RARFileBlockFlags)reader.Read7BitEncodedInt();
								((RARFileBlock)header).unpackedSize = reader.Read7BitEncodedInt();
								((RARFileBlock)header).attributes = (V5.RARFileAttributes)reader.Read7BitEncodedInt();
								if ((((RARFileBlock)header).fileFlags & V5.RARFileBlockFlags.TimeFieldPresent) == V5.RARFileBlockFlags.TimeFieldPresent)
								{
									((RARFileBlock)header).mtime = reader.ReadUInt32();
								}
								if ((((RARFileBlock)header).fileFlags & V5.RARFileBlockFlags.CRC32Present) == V5.RARFileBlockFlags.CRC32Present)
								{
									((RARFileBlock)header).dataCrc = reader.ReadUInt32();
								}
								((RARFileBlock)header).compressionFlags = reader.Read7BitEncodedInt();
								((RARFileBlock)header).hostOperatingSystem = (RARHostOperatingSystem)reader.Read7BitEncodedInt();
								((RARFileBlock)header).fileNameLength = reader.Read7BitEncodedInt();
								((RARFileBlock)header).fileName = reader.ReadFixedLengthString(((RARFileBlock)header).fileNameLength);
								((RARFileBlock)header).dataOffset = reader.Accessor.Position + extraAreaSize;

								bom.Blocks.Add(header);
								break;
							}
							case V5.RARBlockTypeV5.End:
							{
								RAREndBlock header = new RAREndBlock();
								header.crc = crc;
								header.size = size;
								header.headerType = RARBlockTypeV5ToRARBlockType(headerType);
								header.headerFlags = headerFlags;
								((RAREndBlock)header).endOfArchiveFlags = (V5.RAREndBlockFlags)reader.Read7BitEncodedInt();
								header.extraAreaSize = extraAreaSize;
								header.dataSize = dataSize;

								bom.Blocks.Add(header);
								break;
							}
						}

						// extra area...
						reader.Seek(extraAreaSize, SeekOrigin.Current);
						// data area...
						reader.Seek(dataSize, SeekOrigin.Current);

						break;
					}
				}
			}
		}

		private RARBlockType RARBlockTypeV5ToRARBlockType(V5.RARBlockTypeV5 headerType)
		{
			switch (headerType)
			{
				case V5.RARBlockTypeV5.Encryption: return RARBlockType.Encryption;
				case V5.RARBlockTypeV5.End: return RARBlockType.End;
				case V5.RARBlockTypeV5.File: return RARBlockType.File;
				case V5.RARBlockTypeV5.Main: return RARBlockType.Main;
				case V5.RARBlockTypeV5.Service: return RARBlockType.Service;
			}
			return RARBlockType.Unknown;
		}

		private RARBlockType RARBlockTypeV4ToRARBlockType(V4.RARBlockTypeV4 headerType)
		{
			switch (headerType)
			{
				case V4.RARBlockTypeV4.Archive: return RARBlockType.Main;
				case V4.RARBlockTypeV4.File: return RARBlockType.File;
				case V4.RARBlockTypeV4.Marker: return RARBlockType.Marker;
				case V4.RARBlockTypeV4.OldAuthenticity: return RARBlockType.OldAuthenticity;
				case V4.RARBlockTypeV4.OldAuthenticity2:return RARBlockType.OldAuthenticity2;
				case V4.RARBlockTypeV4.OldComment: return RARBlockType.OldComment;
				case V4.RARBlockTypeV4.OldRecoveryRecord: return RARBlockType.OldRecoveryRecord;
				case V4.RARBlockTypeV4.OldSubblock: return RARBlockType.OldSubblock;
				case V4.RARBlockTypeV4.Subblock: return RARBlockType.Subblock;
			}
			return RARBlockType.Unknown;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
