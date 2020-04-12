//
//  CHDHunkFileSource.cs - provides a FileSource for accessing data within a CHD archive
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

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	/// <summary>
	/// Provides a <see cref="FileSource" /> for accessing data within a CHD archive.
	/// </summary>
	public class CHDHunkFileSource : FileSource
	{
		public Reader Reader { get; set; } = null;
		public uint HunkId { get; set; } = 0;
		public long RawMapOffset { get; set; } = 0;
		public uint HunkSize { get; set; } = 0;

		public CHDHunkFileSource(Reader reader, uint hunkID, long rawMapOffset, uint hunkSize)
		{
			Reader = reader;
			HunkId = hunkID;
			RawMapOffset = rawMapOffset;
			HunkSize = hunkSize;
		}

		public override byte[] GetData(long offset, long length)
		{
			return ReadHunk(Reader, HunkId, offset, length);
		}


		Compression.CompressionModule[] compressionModules = new Compression.CompressionModule[]
		{
			new Compression.Modules.Deflate.DeflateCompressionModule()
		};

		private byte[] ReadHunk(IO.Reader br, ulong hunkid, long offset, long length)
		{
			uint hunkRawMapOffset = (uint)((ulong)RawMapOffset + (16 * hunkid));

			br.Accessor.Position = hunkRawMapOffset;
			ulong blockOffset = br.ReadUInt64();
			uint blockCRC = br.ReadUInt32();
			ushort blockLengthPart1 = br.ReadUInt16();
			byte blockLengthPart2 = br.ReadByte();

			uint blockLength = (uint)(blockLengthPart1 + (blockLengthPart2 << 16));

			byte flags = br.ReadByte();
			CHDEntryType entryType = (CHDEntryType)(flags & CHDDataFormat.V34_MAP_ENTRY_FLAG_TYPE_MASK);
			bool noCRC = ((flags & CHDDataFormat.V34_MAP_ENTRY_FLAG_NO_CRC) == CHDDataFormat.V34_MAP_ENTRY_FLAG_NO_CRC);

			switch (entryType)
			{
				case CHDEntryType.Compressed:
				{
					byte[] compressedData = new byte[0];

					br.Accessor.Position = (long)blockOffset;
					compressedData = br.ReadBytes(blockLength);

					byte[] decompressedData = compressedData; // UniversalEditor.Compression.Zlib.ZlibStream.Decompress(compressedData);

					// no, this is not Zlib, this is plain old Deflate...?
					decompressedData = compressionModules[0].Decompress(compressedData);

					// m_decompressor[0]->decompress(m_compressed, blocklen, dest, m_hunkbytes);
					// if (!(rawmap[15] & V34_MAP_ENTRY_FLAG_NO_CRC) && dest != NULL && crc32_creator::simple(dest, m_hunkbytes) != blockcrc)
					// throw new System.IO.IOException("Decompression failed");
					return decompressedData;
				}
				case CHDEntryType.Uncompressed:
				{
					byte[] buffer = new byte[0];
					br.Read(buffer, (int)blockOffset, (int)HunkSize);
					// if (!noCRC && crc32_creator::simple(dest, m_hunkbytes) != blockcrc)
					// throw CHDERR_DECOMPRESSION_ERROR;
					return buffer;
				}
				case CHDEntryType.Miniature:
				{
					MemoryAccessor ma = new MemoryAccessor();
					IO.Writer bw = new IO.Writer(ma);
					bw.Endianness = IO.Endianness.BigEndian;
					bw.WriteUInt64((ulong)blockOffset);
					bw.WriteBytes(new byte[HunkSize - 8]);
					bw.Flush();
					bw.Close();
					byte[] buffer = ma.ToArray();

					for (uint bytes = 8; bytes < HunkSize; bytes++)
					{
						buffer[bytes] = buffer[bytes - 8];
					}
					return buffer;
				}
				case CHDEntryType.SelfHunk:
				{
					return ReadHunk(br, blockOffset, offset, length);
				}
				case CHDEntryType.ParentHunk:
				{
					// if (mvarParent != null)
					// {
					// }
					throw new InvalidOperationException("Parent required");
				}
			}
			throw new InvalidOperationException("No hunk exists or hunk type not implemented");
		}

		public override long GetLength()
		{
			throw new NotImplementedException();
		}
	}
}
