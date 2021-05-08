//
//  LZRW1CompressionModule.cs - port of Ross Williams' (public-domain) lzrw1 from quickbms to C#
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace UniversalEditor.Compression.Modules.LZRW1
{
	public class LZRW1CompressionModule : CompressionModule
	{
		/// <summary>
		/// Number of bytes used by copy flag
		/// </summary>
		private const byte FLAG_BYTES = 0x04;

		private const byte FLAG_COMPRESS = 0x00;
		private const byte FLAG_COPY = 0x01;


		public override string Name
		{
			get { return "LZRW1"; }
		}

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			throw new NotImplementedException();
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			StreamAccessor sao = new StreamAccessor(outputStream);
			StreamAccessor sai = new StreamAccessor(inputStream);
			Reader br = new Reader(sai);
			Writer bw = new Writer(sao);

			/*
				byte *p_src = p_src_first + 4,
				*p_dst = p_dst_first,
				*p_dst_end = p_dst_first + dst_len;
				byte  *p_src_post = p_src_first + src_len;
				byte  *p_src_max16 = p_src_first + src_len - (16 * 2);
			*/

			uint control = 1;

			uint flag = br.ReadUInt32();
			if (flag == FLAG_COPY)
			{
				// entire stream is uncompressed, so read it all
				byte[] data = br.ReadToEnd();
				bw.WriteBytes(data);
			}
			while (!br.EndOfStream)
			{
				uint unroll;
				if (control == 1)
				{
					control = (uint)(0x10000 | br.ReadByte());
					control |= (uint)(br.ReadByte() << 8);
				}
				unroll = (uint)((br.Accessor.Position <= (br.Accessor.Length - 32)) ? 16 : 1);
				while (unroll-- > 0)
				{
					if ((control & 1) == 1)
					{
						ushort lenmt = br.ReadByte();
						byte offsetCalcByte2 = br.ReadByte();

						ushort offset = (ushort)(((lenmt & 0xF0) << 4) | offsetCalcByte2);

						long oldpos = sao.Position;
						long newpos = sao.Length - offset;

						IO.Reader bro = new Reader(sao);

						// if((p_dst + offset) > p_dst_end) return(-1);

						// look I know this is ugly but HOLY FUCK IT WORKS FINALLY OMG!!!
						// i'll clean it up eventually... I promise. one of these days...
						sao.Position = newpos;
						byte value = bro.ReadByte();
						sao.Position = oldpos;
						bw.WriteByte(value);
						newpos++;
						oldpos++;

						sao.Position = newpos;
						value = bro.ReadByte();
						sao.Position = oldpos;
						bw.WriteByte(value);
						newpos++;
						oldpos++;

						sao.Position = newpos;
						value = bro.ReadByte();
						sao.Position = oldpos;
						bw.WriteByte(value);
						newpos++;
						oldpos++;

						lenmt &= 0xF;
						while (lenmt-- > 0)
						{
							sao.Position = newpos;
							value = bro.ReadByte();
							sao.Position = oldpos;
							bw.WriteByte(value);
							newpos++;
							oldpos++;
						}
					}
					else
					{
						if (br.EndOfStream) return;
						bw.WriteByte(br.ReadByte());
					}
					control >>= 1;
				}
			}
		}
	}
}
