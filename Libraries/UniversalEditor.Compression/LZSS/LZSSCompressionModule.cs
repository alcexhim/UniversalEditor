//
//  LZSSCompressionModule.cs - provides a CompressionModule for handling LZSS compression
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
using System.IO;

using UniversalEditor.Compression.Common;

namespace UniversalEditor.Compression.Modules.LZSS
{
	/// <summary>
	/// Provides a <see cref="CompressionModule" /> for handling LZSS compression.
	/// </summary>
	public class LZSSCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "lzss"; }
		}
		protected override void DecompressInternal(Stream inputStream, Stream outputStream, int inputLength, int outputLength)
		{
			MT19937ar myMT19937ar = new MT19937ar();
			uint act_outputLength = 0u;
			uint nActWindowByte = 4080u;
			byte[] win = new byte[4096];
			ushort flag = 0;
			uint[] init_key = new uint[4];
			byte[] rnd = new byte[1024];
			init_key[0] = ((uint)outputLength | 128u) >> 5;
			init_key[1] = ((uint)outputLength << 9 | 6u);
			init_key[2] = ((uint)outputLength << 6 | 4u);
			init_key[3] = ((uint)outputLength | 72u) >> 3;
			myMT19937ar.init_by_array(init_key, 4);
			for (uint i = 0u; i < 1024u; i += 1u)
			{
				rnd[(int)(i)] = (byte)(myMT19937ar.genrand_int32() >> (int)(i % 7u));
			}
			uint seed = (uint)outputLength / 1000u % 10u + (uint)outputLength / 100u % 10u + (uint)outputLength / 10u % 10u + (uint)outputLength % 10u & 794u;
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
					flag = (ushort)((inputStream.ReadByte() ^ rnd[(int)(seed)]) | 65280);
				}
				seed = (seed + 1u & 1023u);
				byte data;
				if ((flag & 1) == 0)
				{
					uint win_offset = (uint)(inputStream.ReadByte() ^ rnd[(int)(seed)]);
					seed = (seed + 1u & 1023u);
					uint copy_bytes = (uint)(inputStream.ReadByte() ^ rnd[(int)(seed)]);
					win_offset |= (copy_bytes & 240u) << 4;
					copy_bytes &= 15u;
					copy_bytes += 3u;
					for (uint i = 0u; i < copy_bytes; i += 1u)
					{
						data = win[(int)((win_offset + i & 4095u))];
						outputStream.WriteByte(data);
						win[(int)((nActWindowByte++))] = data;
						nActWindowByte &= 4095u;
						if (act_outputLength >= outputLength) return;
					}
					continue;
				}
				data = (byte)(inputStream.ReadByte() ^ rnd[(int)(seed)]);
				outputStream.WriteByte(data);
				win[(int)((nActWindowByte++))] = data;
				nActWindowByte &= 4095u;
			}
		}
		protected override void CompressInternal(Stream inputStream, Stream outputStream)
		{
			throw new NotImplementedException();
		}
	}
}
