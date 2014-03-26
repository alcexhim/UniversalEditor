using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.LZSS
{
	public class LZSSStream : CompressionStream
	{
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}
		public override void Flush()
		{
			throw new NotImplementedException();
		}
		public static byte[] Decompress(byte[] inputData, uint outputLength)
		{
			MT19937ar myMT19937ar = new MT19937ar();
			byte[] outputData = new byte[(int)((UIntPtr)outputLength)];
			uint act_outputLength = 0u;
			uint act_byte = 0u;
			uint nActWindowByte = 4080u;
			byte[] win = new byte[4096];
			ushort flag = 0;
			uint[] init_key = new uint[4];
			byte[] rnd = new byte[1024];
			init_key[0] = (outputLength | 128u) >> 5;
			init_key[1] = (outputLength << 9 | 6u);
			init_key[2] = (outputLength << 6 | 4u);
			init_key[3] = (outputLength | 72u) >> 3;
			myMT19937ar.init_by_array(init_key, 4);
			for (uint i = 0u; i < 1024u; i += 1u)
			{
				rnd[(int)((UIntPtr)i)] = (byte)(myMT19937ar.genrand_int32() >> (int)(i % 7u));
			}
			uint seed = outputLength / 1000u % 10u + outputLength / 100u % 10u + outputLength / 10u % 10u + outputLength % 10u & 794u;
			win.Initialize();
			byte[] result;
			while (act_outputLength < outputLength)
			{
				flag = (ushort)(flag >> 1);
				if (act_outputLength < 32860u)
				{
					goto IL_EE;
				}
			IL_EE:
				if ((flag & 256) == 0)
				{
					seed = (seed + 1u & 1023u);
					flag = (ushort)((inputData[(int)((UIntPtr)(act_byte++))] ^ rnd[(int)((UIntPtr)seed)]) | 65280);
				}
				seed = (seed + 1u & 1023u);
				byte data;
				if ((flag & 1) == 0)
				{
					uint win_offset = (uint)(inputData[(int)((UIntPtr)(act_byte++))] ^ rnd[(int)((UIntPtr)seed)]);
					seed = (seed + 1u & 1023u);
					uint copy_bytes = (uint)(inputData[(int)((UIntPtr)(act_byte++))] ^ rnd[(int)((UIntPtr)seed)]);
					win_offset |= (copy_bytes & 240u) << 4;
					copy_bytes &= 15u;
					copy_bytes += 3u;
					for (uint i = 0u; i < copy_bytes; i += 1u)
					{
						data = win[(int)((UIntPtr)(win_offset + i & 4095u))];
						outputData[(int)((UIntPtr)(act_outputLength++))] = data;
						win[(int)((UIntPtr)(nActWindowByte++))] = data;
						nActWindowByte &= 4095u;
						if (act_outputLength >= outputLength)
						{
							result = inputData;
							return result;
						}
					}
					continue;
				}
				data = (byte)(inputData[(int)((UIntPtr)(act_byte++))] ^ rnd[(int)((UIntPtr)seed)]);
				outputData[(int)((UIntPtr)(act_outputLength++))] = data;
				win[(int)((UIntPtr)(nActWindowByte++))] = data;
				nActWindowByte &= 4095u;
			}
			result = outputData;
			return result;
		}
		public static byte[] Compress(byte[] inputData)
		{
			throw new NotImplementedException();
		}
	}
}
