using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	public class CHDHunkFileSource : FileSource
	{
		private Reader mvarReader = null;
		public Reader Reader { get { return mvarReader; } set { mvarReader = value; } }

		private uint mvarHunkId = 0;
		public uint HunkId { get { return mvarHunkId; } set { mvarHunkId = value; } }

		private long mvarRawMapOffset = 0;
		public long RawMapOffset { get { return mvarRawMapOffset; } set { mvarRawMapOffset = value; } }

		private uint mvarHunkSize = 0;
		public uint HunkSize { get { return mvarHunkSize; } set { mvarHunkSize = value; } }

		public CHDHunkFileSource(Reader reader, uint hunkID, long rawMapOffset, uint hunkSize)
		{
			mvarReader = reader;
			mvarHunkId = hunkID;
			mvarRawMapOffset = rawMapOffset;
			mvarHunkSize = hunkSize;
		}

		public override byte[] GetData(long offset, long length)
		{
			return ReadHunk(mvarReader, mvarHunkId, offset, length);
		}


		Compression.CompressionModule[] compressionModules = new Compression.CompressionModule[]
		{
			new Compression.Modules.Deflate.DeflateCompressionModule()
		};
		
		private byte[] ReadHunk(IO.Reader br, ulong hunkid, long offset, long length)
		{
			uint hunkRawMapOffset = (uint)((ulong)mvarRawMapOffset + (16 * hunkid));

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
					br.Read(buffer, (int)blockOffset, (int)mvarHunkSize);
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
					bw.WriteBytes(new byte[mvarHunkSize - 8]);
					bw.Flush();
					bw.Close();
					byte[] buffer = ma.ToArray();

					for (uint bytes = 8; bytes < mvarHunkSize; bytes++)
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
