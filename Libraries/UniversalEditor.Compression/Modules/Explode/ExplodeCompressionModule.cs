using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression.Modules.Explode.Internal;

namespace UniversalEditor.Compression.Modules.Explode
{
	public class ExplodeCompressionModule : CompressionModule
	{
		public override string Name
		{
			get { return "Explode"; }
		}

		protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			throw new NotImplementedException();
		}

		protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			Ptr<byte> _ptrOut = new Ptr<byte>();
			_ptrOut.AutoResize = true;

			this.blast(inputStream, outputStream, _ptrOut, 0);
		}

		private uint _explodeInput(object how, ref Ptr<byte> buf)
		{
			return 0;
		}
		private int _explodeOutput(object how, Ptr<byte> buf, uint len)
		{
			return 0;
		}


		// translated to C# for Universal Editor by Michael Becker

		// modified by Luigi Auriemma because if MAXWIN is the standard 4096 then not all the data is decompressed

		/* blast.c
		* Copyright (C) 2003 Mark Adler
		* version 1.1, 16 Feb 2003
		*
		* This software is provided 'as-is', without any express or implied
		* warranty.  In no event will the author be held liable for any damages
		* arising from the use of this software.
		*
		* Permission is granted to anyone to use this software for any purpose,
		* including commercial applications, and to alter it and redistribute it
		* freely, subject to the following restrictions:
		*
		* 1. The origin of this software must not be misrepresented; you must not
		*    claim that you wrote the original software. If you use this software
		*    in a product, an acknowledgment in the product documentation would be
		*    appreciated but is not required.
		* 2. Altered source versions must be plainly marked as such, and must not be
		*    misrepresented as being the original software.
		* 3. This notice may not be removed or altered from any source distribution.
		*
		* Mark Adler    madler@alumni.caltech.edu
		*
		* blast.c decompresses data compressed by the PKWare Compression Library.
		* This function provides functionality similar to the explode() function of
		* the PKWare library, hence the name "blast".
		*
		* This decompressor is based on the excellent format description provided by
		* Ben Rudiak-Gould in comp.compression on August 13, 2001.  Interestingly, the
		* example Ben provided in the post is incorrect.  The distance 110001 should
		* instead be 111000.  When corrected, the example byte stream becomes:
		*
		*    00 04 82 24 25 8f 80 7f
		*
		* which decompresses to "AIAIAIAIAIAIA" (without the quotes).
		*/

		/*
		 * Change history:
		 *
		 * 1.0  12 Feb 2003     - First version
		 * 1.1  16 Feb 2003     - Fixed distance check for > 4 GB uncompressed data
		 */

		/// <summary>
		/// Maximum code length
		/// </summary>
		private const int MAXBITS = 13;


		/*
		 * Return need bits from the input stream.  This always leaves less than
		 * eight bits in the buffer.  bits() works properly for need == 0.
		 *
		 * Format notes:
		 *
		 * - Bits are stored in bytes from the least significant bit to the most
		 *   significant bit.  Therefore bits are dropped from the bottom of the bit
		 *   buffer, using shift right, and new bytes are appended to the top of the
		 *   bit buffer, using shift left.
		 */
		private static int bits(ExplodeState s, int need)
		{
			// bit accumulator
			int val;

			// load at least need bits into val
			val = s.bitbuf;

			while (s.bitcnt < need)
			{
				if (s.left == 0)
				{
					readInput(ref s);
				}
				val |= (int)(s.indata.GetValueThenIncrement() << s.bitcnt); // load eight bits
				s.left--;
				s.bitcnt += 8;
			}

			// drop need bits and update buffer, always zero to seven bits left
			s.bitbuf = val >> need;
			s.bitcnt -= need;

			// return need bits, zeroing the bits above that
			return val & ((1 << need) - 1);
		}

		private static void readInput(ref ExplodeState s)
		{
			byte[] tmpBuffer = new byte[4096];
			s.left = (uint)s.inputStream.Read(tmpBuffer, 0, tmpBuffer.Length);

			byte[] buffer = s.indata.ToArray();
			if (buffer.Length < s.left)
			{

			}

			if (s.left == 0) throw new OutOfInputException();
		}

		/*
		 * Format notes:
		 *
		 * - The codes as stored in the compressed data are bit-reversed relative to
		 *   a simple integer ordering of codes of the same lengths.  Hence below the
		 *   bits are pulled from the compressed data one at a time and used to
		 *   build the code value reversed from what is in the stream in order to
		 *   permit simple integer comparisons for decoding.
		 *
		 * - The first code for the shortest length is all ones.  Subsequent codes of
		 *   the same length are simply integer decrements of the previous code.  When
		 *   moving up a length, a one bit is appended to the code.  For a complete
		 *   code, the last code of the longest length will be all zeros.  To support
		 *   this ordering, the bits pulled during decoding are inverted to apply the
		 *   more "natural" ordering starting with all zeros and incrementing.
		 */
		/// <summary>
		/// Decode a code from the stream s using huffman table h.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="h"></param>
		/// <returns>
		/// The decoded symbol, or a negative value if there is an error. If all of the lengths are
		/// zero (i.e. an empty code), or if the code is incomplete and an invalid code is received,
		/// then -9 is returned after reading MAXBITS bits.
		/// </returns>
		private static int decode(ExplodeState s, ref ExplodeHuffman h)
		{
			int len;            /* current number of bits in code */
			int code;           /* len bits being decoded */
			int first;          /* first code of length len */
			int count;          /* number of codes of length len */
			int count_ptr = 0;
			int index;          /* index of first code of length len in symbol table */
			int bitbuf;         /* bits from stream */
			int left;           /* bits left in next or left to process */
			Ptr<short> next; // next number of codes

			bitbuf = s.bitbuf;
			left = s.bitcnt;
			code = first = index = 0;
			len = 1;
			next = h.count + 1;
			while (true)
			{
				while (left-- != 0)
				{
					code |= (bitbuf & 1) ^ 1;   /* invert code */
					bitbuf >>= 1;
					count = next.GetValueThenIncrement();
					if (code < first + count)
					{
						// if length len, return symbol
						s.bitbuf = bitbuf;
						s.bitcnt = (s.bitcnt - len) & 7;
						return h.symbol[index + (code - first)];
					}
					index += count;             /* else update for next length */
					first += count;
					first <<= 1;
					code <<= 1;
					len++;
				}
				left = (MAXBITS+1) - len;
				if (left == 0) break;
				if (s.left == 0)
				{
					readInput(ref s);
					if (s.left == 0) throw new OutOfInputException();
				}
				bitbuf = s.indata.GetValue() + 1; // *(s.in)++;
				s.left--;
				if (left > 8) left = 8;
			}
			return -9;                          /* ran out of codes */
		}

		/// <summary>
		/// Given a list of repeated code lengths rep[0..n-1], where each byte is a count (high four
		/// bits + 1) and a code length (low four bits), generate the list of code lengths.  This
		/// compaction reduces the size of the object code. Then given the list of code lengths
		/// length[0..n-1] representing a canonical Huffman code for n symbols, construct the
		/// tables required to decode those codes.  Those tables are the number of codes of each
		/// length, and the symbols sorted by length, retaining their original order within each
		/// length.
		/// </summary>
		/// <param name="h"></param>
		/// <param name="rep"></param>
		/// <returns>
		/// Zero for a complete code set, negative for an over-subscribed code set, and positive for an incomplete code set.
		/// </returns>
		/// <remarks>
		/// The tables can be used if the return value is zero or positive, but they cannot be used
		/// if the return value is negative.  If the return value is zero, it is not possible for
		/// decode() using that table to return an error--any stream of enough bits will resolve to
		/// a symbol.  If the return value is positive, then it is possible for decode() using that
		/// table to return an error for received codes past the end of the incomplete lengths.
		/// </remarks>
		private static int construct(ref ExplodeHuffman h, byte[] rep)
		{
			int n = rep.Length;
			int symbol;									// current symbol when stepping through length[]
			int len;									// current length when stepping through h.count[]
			int left;									// number of possible codes left of current length
			short[] offs = new short[MAXBITS + 1];		// offsets in symbol table for each length
			short[] length = new short[256];			// code lengths

			Ptr<byte> repP = new Ptr<byte>(rep);

			// convert compact repeat counts into symbol bit length list
			symbol = 0;
			do
			{
				len = repP.GetValueThenIncrement();		// *rep++
				left = (len >> 4) + 1;
				len &= 15;
				do
				{
					length[symbol++] = (short)len;
				}
				while (--left != 0);
			}
			while (--n != 0);
			n = symbol;

			// count number of codes of each length
			for (len = 0; len <= MAXBITS; len++)
				h.count[len] = 0;
			for (symbol = 0; symbol < n; symbol++)
			{
				(h.count[length[symbol]])++;			// assumes lengths are within bounds
			}
			if (h.count[0] == n)
			{
				// no codes!
				return 0; // complete, but decode() will fail
			}

			// check for an over-subscribed or incomplete set of lengths
			left = 1;								// one possible code of zero length
			for (len = 1; len <= MAXBITS; len++)
			{
				left <<= 1;							// one more bit, double codes left
				left -= h.count[len];				// deduct count from possible codes
				if (left < 0) return left;			// over-subscribed--return negative
			}										// left > 0 means incomplete

			// generate offsets into symbol table for each length for sorting
			offs[1] = 0;
			for (len = 1; len < MAXBITS; len++)
			{
				offs[len + 1] = (short)(offs[len] + h.count[len]); // h.count[len]
			}

			// put symbols in table sorted by length, by symbol order within each length
			for (symbol = 0; symbol < n; symbol++)
			{
				if (length[symbol] != 0)
				{
					h.symbol[offs[length[symbol]]++] = (short)symbol;
				}
			}

			// return zero for complete set, positive for incomplete set
			return left;
		}

		private static bool		virgin = true;							// build tables once
		private static short[]	litcnt = new short[MAXBITS + 1],
								litsym = new short[256];				// litcode memory
		private static short[]	lencnt = new short[MAXBITS + 1],
								lensym = new short[16];					// lencode memory
		private static short[]	distcnt = new short[MAXBITS + 1],
								distsym = new short[64];				// distcode memory

		/// <summary>
		/// Literal code
		/// </summary>
		private static ExplodeHuffman litcode = new ExplodeHuffman()
		{
			count = new Ptr<short>(litcnt),
			symbol = new Ptr<short>(litsym)
		};
		/// <summary>
		/// Length code
		/// </summary>
		private static ExplodeHuffman lencode = new ExplodeHuffman()
		{
			count = new Ptr<short>(lencnt),
			symbol = new Ptr<short>(lensym)
		};

		/// <summary>
		/// Distance code
		/// </summary>
		private static ExplodeHuffman distcode = new ExplodeHuffman()
		{
			count = new Ptr<short>(distcnt),
			symbol = new Ptr<short>(distsym)
		};

		// bit lengths of literal codes
		private static readonly byte[] litlen = new byte[]
		{
			11, 124, 8, 7, 28, 7, 188, 13, 76, 4, 10, 8, 12, 10, 12, 10, 8, 23, 8,
			9, 7, 6, 7, 8, 7, 6, 55, 8, 23, 24, 12, 11, 7, 9, 11, 12, 6, 7, 22, 5,
			7, 24, 6, 11, 9, 6, 7, 22, 7, 11, 38, 7, 9, 8, 25, 11, 8, 11, 9, 12,
			8, 12, 5, 38, 5, 38, 5, 11, 7, 5, 6, 21, 6, 10, 53, 8, 7, 24, 10, 27,
			44, 253, 253, 253, 252, 252, 252, 13, 12, 45, 12, 45, 12, 61, 12, 45,
			44, 173
		};
		/// <summary>
		/// Bit lengths of length codes 0..15
		/// </summary>
		private static readonly byte[] lenlen = new byte[] { 2, 35, 36, 53, 38, 23 };
		/// <summary>
		/// Bit lengths of distance codes 0..63
		/// </summary>
		private static readonly byte[] distlen = new byte[] { 2, 20, 53, 230, 247, 151, 248 };
		/// <summary>
		/// Base for length codes
		/// </summary>
		private static readonly short[] _base = new short[] { 3, 2, 4, 5, 6, 7, 8, 9, 10, 12, 16, 24, 40, 72, 136, 264 };
		/// <summary>
		/// Extra bits for length codes
		/// </summary>
		private static readonly byte[] extra = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8 };

		/*
		 * Format notes:
		 *
		 * - First byte is 0 if literals are uncoded or 1 if they are coded.  Second
		 *   byte is 4, 5, or 6 for the number of extra bits in the distance code.
		 *   This is the base-2 logarithm of the dictionary size minus six.
		 *
		 * - Compressed data is a combination of literals and length/distance pairs
		 *   terminated by an end code.  Literals are either Huffman coded or
		 *   uncoded bytes.  A length/distance pair is a coded length followed by a
		 *   coded distance to represent a string that occurs earlier in the
		 *   uncompressed data that occurs again at the current location.
		 *
		 * - A bit preceding a literal or length/distance pair indicates which comes
		 *   next, 0 for literals, 1 for length/distance.
		 *
		 * - If literals are uncoded, then the next eight bits are the literal, in the
		 *   normal bit order in th stream, i.e. no bit-reversal is needed. Similarly,
		 *   no bit reversal is needed for either the length extra bits or the distance
		 *   extra bits.
		 *
		 * - Literal bytes are simply written to the output.  A length/distance pair is
		 *   an instruction to copy previously uncompressed bytes to the output.  The
		 *   copy is from distance bytes back in the output stream, copying for length
		 *   bytes.
		 *
		 * - Distances pointing before the beginning of the output data are not
		 *   permitted.
		 *
		 * - Overlapped copies, where the length is greater than the distance, are
		 *   allowed and common.  For example, a distance of one and a length of 518
		 *   simply copies the last byte 518 times.  A distance of four and a length of
		 *   twelve copies the last four bytes three times.  A simple forward copy
		 *   ignoring whether the length is greater than the distance or not implements
		 *   this correctly.
		 */

		private static object virginity = new object();

		/// <summary>
		/// Decode PKWare Compression Library stream.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private static int decomp(ref ExplodeState s)
		{
			int lit;            /* true if literals are coded */
			int dict;           /* log2(dictionary size) - 6 */
			int symbol;         /* decoded symbol, extra bits for distance */
			int len;            /* length for copy */
			int dist;           /* distance for copy */
			int copy;           /* copy counter */
			Ptr<byte> from, to;   /* copy pointers */

			// set up decoding tables (once--might not be thread-safe)
			lock (virginity)
			{
				if (virgin)
				{
					construct(ref litcode, litlen);
					construct(ref lencode, lenlen);
					construct(ref distcode, distlen);
					virgin = false;
				}
			}

			// read header
			lit = bits(s, 8);
			if (lit > 1) return -1;
			dict = bits(s, 8);
			if (dict < 4 || dict > 6) return -2;

			// decode literals and length/distance pairs
			do
			{
				if (bits(s, 1) != 0)
				{
					// get length
					symbol = decode(s, ref lencode);
					len = _base[symbol] + bits(s, extra[symbol]);
					if (len == 519) break; // end code

					// get distance
					symbol = len == 2 ? 2 : dict;
					dist = decode(s, ref distcode) << symbol;
					dist += bits(s, symbol);
					dist++;
					if (s.first != 0 && dist > s.next)
					{
						throw new ArgumentOutOfRangeException("distance too far back");
					}

					// copy length bytes from distance bytes back
					do
					{
						to = s.outdata + s.next;
						from = to - dist;
						copy = -1;
						if (s.next < dist)
						{
							from += copy;
							copy = dist;
						}
						copy -= s.next;
						if (copy > len) copy = len;
						len -= copy;
						s.next += copy;
						do
						{
							to.SetValueThenIncrement(from.GetValueThenIncrement());
						}
						while (--copy != 0);
						if (s.next == -1)
						{
							s.outputStream.Write(s.outdata.ToArray());
							s.next = 0;
							s.first = 0;
						}
					} while (len != 0);
				}
				else
				{
					// get literal and write it
					symbol = (lit != 0) ? decode(s, ref litcode) : bits(s, 8);
					s.outdata.SetValueThenIncrement((byte)symbol);
					if (s.next == -1)
					{
						s.outputStream.Write(s.outdata.ToArray());
						s.next = 0;
						s.first = 0;
					}
				}
			}
			while (true);
			return 0;
		}

		void blast(System.IO.Stream inputStream, System.IO.Stream outputStream, Ptr<byte> outdata, int outsz)
		{
			ExplodeState s = new ExplodeState(); // input/output state

			// initialize input state
			s.inputStream = inputStream;
			s.left = 0;
			s.bitbuf = 0;
			s.bitcnt = 0;
			s.indata = new Ptr<byte>(new byte[0]);

			// initialize output state
			s.outputStream = outputStream;
			s.next = 0;
			s.first = 1;
			s.outdata = new Ptr<byte>(new byte[0]);

			// decompress
			decomp(ref s);

			// write any leftover output
			if (s.outdata.Size > 0) s.outputStream.Write(s.outdata.ToArray());
		}

	}
}
