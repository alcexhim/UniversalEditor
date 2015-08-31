using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using UniversalEditor.Compression.Modules.LZX.Internal;

/* This file was derived from libmspack
 * (C) 2003-2004 Stuart Caie.
 * (C) 2011 Ali Scissons.
 *
 * The LZX method was created by Jonathan Forbes and Tomi Poutanen, adapted
 * by Microsoft Corporation.
 *
 * This source file is Dual licensed; meaning the end-user of this source file
 * may redistribute/modify it under the LGPL 2.1 or MS-PL licenses.
 * 
 * Adapted into a Universal Editor Compression Module by Michael Becker.
 * 
 * Original source code:
 * https://code.google.com/p/monoxna/source/browse/trunk/src/Microsoft.Xna.Framework/HelperCode/LzxDecoder.cs
*/

namespace UniversalEditor.Compression.Modules.LZX
{
    public class LZXCompressionModule : CompressionModule
    {
        public override string Name
        {
            get { return "LZX"; }
        }

        public static uint[] position_base = null;
        public static byte[] extra_bits = null;

        private LzxState m_state;

		public LZXCompressionModule()
		{

		}
		public LZXCompressionModule(int window)
		{
			mvarWindowSize = window;
		}

		private int mvarWindowSize = 18;
		public int WindowSize
		{
			get { return mvarWindowSize; }
			set
			{
				// setup proper exception
				if (value < 15 || value > 21) throw new UnsupportedWindowSizeRangeException();

				mvarWindowSize = value;
			}
		}

		protected override void InitializeInternal()
		{
			// setup proper exception
			if (mvarWindowSize < 15 || mvarWindowSize > 21) throw new UnsupportedWindowSizeRangeException();

			uint wndsize = (uint)(1 << mvarWindowSize);
			int posn_slots;

			// let's initialise our state
			m_state = new LzxState();
			m_state.actual_size = 0;
			m_state.window = new byte[wndsize];
			for (int i = 0; i < wndsize; i++) m_state.window[i] = 0xDC;
			m_state.actual_size = wndsize;
			m_state.window_size = wndsize;
			m_state.window_posn = 0;

			/* initialize static tables */
			if (extra_bits == null)
			{
				extra_bits = new byte[52];
				for (int i = 0, j = 0; i <= 50; i += 2)
				{
					extra_bits[i] = extra_bits[i + 1] = (byte)j;
					if ((i != 0) && (j < 17)) j++;
				}
			}
			if (position_base == null)
			{
				position_base = new uint[51];
				for (int i = 0, j = 0; i <= 50; i++)
				{
					position_base[i] = (uint)j;
					j += 1 << extra_bits[i];
				}
			}

			// calculate required position slots
			if (mvarWindowSize == 20) posn_slots = 42;
			else if (mvarWindowSize == 21) posn_slots = 50;
			else posn_slots = mvarWindowSize << 1;

			m_state.R0 = m_state.R1 = m_state.R2 = 1;
			m_state.main_elements = (ushort)(Constants.NUM_CHARS + (posn_slots << 3));
			m_state.header_read = 0;
			m_state.frames_read = 0;
			m_state.block_remaining = 0;
			m_state.block_type = Constants.BLOCKTYPE.INVALID;
			m_state.intel_curpos = 0;
			m_state.intel_started = 0;

			// yo dawg i herd u liek arrays so we put arrays in ur arrays so u can array while u array
			m_state.PRETREE_table = new ushort[(1 << Constants.PRETREE_TABLEBITS) + (Constants.PRETREE_MAXSYMBOLS << 1)];
			m_state.PRETREE_len = new byte[Constants.PRETREE_MAXSYMBOLS + Constants.LENTABLE_SAFETY];
			m_state.MAINTREE_table = new ushort[(1 << Constants.MAINTREE_TABLEBITS) + (Constants.MAINTREE_MAXSYMBOLS << 1)];
			m_state.MAINTREE_len = new byte[Constants.MAINTREE_MAXSYMBOLS + Constants.LENTABLE_SAFETY];
			m_state.LENGTH_table = new ushort[(1 << Constants.LENGTH_TABLEBITS) + (Constants.LENGTH_MAXSYMBOLS << 1)];
			m_state.LENGTH_len = new byte[Constants.LENGTH_MAXSYMBOLS + Constants.LENTABLE_SAFETY];
			m_state.ALIGNED_table = new ushort[(1 << Constants.ALIGNED_TABLEBITS) + (Constants.ALIGNED_MAXSYMBOLS << 1)];
			m_state.ALIGNED_len = new byte[Constants.ALIGNED_MAXSYMBOLS + Constants.LENTABLE_SAFETY];
			/* initialise tables to 0 (because deltas will be applied to them) */
			for (int i = 0; i < Constants.MAINTREE_MAXSYMBOLS; i++) m_state.MAINTREE_len[i] = 0;
			for (int i = 0; i < Constants.LENGTH_MAXSYMBOLS; i++) m_state.LENGTH_len[i] = 0;
		}

		protected override void CompressInternal(Stream inputStream, Stream outputStream)
        {
            throw new NotImplementedException();
        }
		protected override void DecompressInternal(Stream inputStream, Stream outputStream, int inputLength, int outputLength)
        {
            BitBuffer bitbuf = new BitBuffer(inputStream);
            
            long startpos = inputStream.Position;
            long endpos = inputStream.Position + inputLength;

            byte[] window = m_state.window;

            uint window_posn = m_state.window_posn;
            uint window_size = m_state.window_size;
            uint R0 = m_state.R0;
            uint R1 = m_state.R1;
            uint R2 = m_state.R2;
            uint i, j;

            int togo = outputLength, this_run, main_element, match_length, match_offset, length_footer, extra, verbatim_bits;
            int rundest, runsrc, copy_length, aligned_bits;

            bitbuf.InitBitStream();

            // read header if necessary
            if (m_state.header_read == 0)
            {
                uint intel = bitbuf.ReadBits(1);
                if (intel != 0)
                {
                    // read the filesize
                    i = bitbuf.ReadBits(16); j = bitbuf.ReadBits(16);
                    m_state.intel_filesize = (int)((i << 16) | j);
                }
                m_state.header_read = 1;
            }

            // main decoding loop
            while (togo > 0)
            {
                // last block finished, new block expected
                if (m_state.block_remaining == 0)
                {
                    // TODO may screw something up here
                    if (m_state.block_type == Constants.BLOCKTYPE.UNCOMPRESSED)
                    {
                        if ((m_state.block_length & 1) == 1) inputStream.ReadByte(); /* realign bitstream to word */
                        bitbuf.InitBitStream();
                    }

                    m_state.block_type = (Constants.BLOCKTYPE)bitbuf.ReadBits(3); ;
                    i = bitbuf.ReadBits(16);
                    j = bitbuf.ReadBits(8);
                    m_state.block_remaining = m_state.block_length = (uint)((i << 8) | j);

                    switch (m_state.block_type)
                    {
                        case Constants.BLOCKTYPE.ALIGNED:
                        {
                            for (i = 0, j = 0; i < 8; i++) { j = bitbuf.ReadBits(3); m_state.ALIGNED_len[i] = (byte)j; }
                            MakeDecodeTable(Constants.ALIGNED_MAXSYMBOLS, Constants.ALIGNED_TABLEBITS,
                                            m_state.ALIGNED_len, m_state.ALIGNED_table);
                            /* rest of aligned header is same as verbatim */
                            goto case Constants.BLOCKTYPE.VERBATIM;
                        }
                        case Constants.BLOCKTYPE.VERBATIM:
                        {
                            ReadLengths(m_state.MAINTREE_len, 0, 256, bitbuf);
                            ReadLengths(m_state.MAINTREE_len, 256, m_state.main_elements, bitbuf);
                            MakeDecodeTable(Constants.MAINTREE_MAXSYMBOLS, Constants.MAINTREE_TABLEBITS,
                                            m_state.MAINTREE_len, m_state.MAINTREE_table);
                            if (m_state.MAINTREE_len[0xE8] != 0) m_state.intel_started = 1;

                            ReadLengths(m_state.LENGTH_len, 0, Constants.NUM_SECONDARY_LENGTHS, bitbuf);
                            MakeDecodeTable(Constants.LENGTH_MAXSYMBOLS, Constants.LENGTH_TABLEBITS,
                                            m_state.LENGTH_len, m_state.LENGTH_table);
                            break;
                        }
                        case Constants.BLOCKTYPE.UNCOMPRESSED:
                        {
                            m_state.intel_started = 1; /* because we can't assume otherwise */
                            bitbuf.EnsureBits(16); /* get up to 16 pad bits into the buffer */
                            if (bitbuf.GetBitsLeft() > 16) inputStream.Seek(-2, SeekOrigin.Current); /* and align the bitstream! */
                            byte hi, mh, ml, lo;
                            lo = (byte)inputStream.ReadByte(); ml = (byte)inputStream.ReadByte(); mh = (byte)inputStream.ReadByte(); hi = (byte)inputStream.ReadByte();
                            R0 = (uint)(lo | ml << 8 | mh << 16 | hi << 24);
                            lo = (byte)inputStream.ReadByte(); ml = (byte)inputStream.ReadByte(); mh = (byte)inputStream.ReadByte(); hi = (byte)inputStream.ReadByte();
                            R1 = (uint)(lo | ml << 8 | mh << 16 | hi << 24);
                            lo = (byte)inputStream.ReadByte(); ml = (byte)inputStream.ReadByte(); mh = (byte)inputStream.ReadByte(); hi = (byte)inputStream.ReadByte();
                            R2 = (uint)(lo | ml << 8 | mh << 16 | hi << 24);
                            break;
                        }
                        default:
                        {
                            throw new InvalidOperationException("Unknown block type " + m_state.block_type);
                        }
                    }
                }

                // buffer exhaustion check
                if (inputStream.Position > (startpos + inputLength))
                {
                    // it's possible to have a file where the next run is less than
                    // 16 bits in size. In this case, the READ_HUFFSYM() macro used
                    // in building the tables will exhaust the buffer, so we should
                    // allow for this, but not allow those accidentally read bits to
                    // be used (so we check that there are at least 16 bits
                    // remaining - in this boundary case they aren't really part of
                    // the compressed data)

                    if (inputStream.Position > (startpos + inputLength + 2) || bitbuf.GetBitsLeft() < 16)
                    {
                        throw new InvalidOperationException("WTF");
                    }
                }

                while ((this_run = (int)m_state.block_remaining) > 0 && togo > 0)
                {
                    if (this_run > togo) this_run = togo;
                    togo -= this_run;
                    m_state.block_remaining -= (uint)this_run;

                    /* apply 2^x-1 mask */
                    window_posn &= window_size - 1;
                    /* runs can't straddle the window wraparound */
                    if ((window_posn + this_run) > window_size)
                    {
                        throw new InvalidOperationException("window_posn + this_run is greater than window_size");
                    }

                    switch (m_state.block_type)
                    {
                        case Constants.BLOCKTYPE.VERBATIM:
                        {
                            while (this_run > 0)
                            {
                                main_element = (int)ReadHuffSym(m_state.MAINTREE_table, m_state.MAINTREE_len,
                                                            Constants.MAINTREE_MAXSYMBOLS, Constants.MAINTREE_TABLEBITS,
                                                            bitbuf);
                                if (main_element < Constants.NUM_CHARS)
                                {
                                    /* literal: 0 to NUM_CHARS-1 */
                                    window[window_posn++] = (byte)main_element;
                                    this_run--;
                                }
                                else
                                {
                                    /* match: NUM_CHARS + ((slot<<3) | length_header (3 bits)) */
                                    main_element -= Constants.NUM_CHARS;

                                    match_length = main_element & Constants.NUM_PRIMARY_LENGTHS;
                                    if (match_length == Constants.NUM_PRIMARY_LENGTHS)
                                    {
                                        length_footer = (int)ReadHuffSym(m_state.LENGTH_table, m_state.LENGTH_len,
                                                                    Constants.LENGTH_MAXSYMBOLS, Constants.LENGTH_TABLEBITS,
                                                                    bitbuf);
                                        match_length += length_footer;
                                    }
                                    match_length += Constants.MIN_MATCH;

                                    match_offset = main_element >> 3;

                                    if (match_offset > 2)
                                    {
                                        /* not repeated offset */
                                        if (match_offset != 3)
                                        {
                                            extra = extra_bits[match_offset];
                                            verbatim_bits = (int)bitbuf.ReadBits((byte)extra);
                                            match_offset = (int)position_base[match_offset] - 2 + verbatim_bits;
                                        }
                                        else
                                        {
                                            match_offset = 1;
                                        }

                                        /* update repeated offset LRU queue */
                                        R2 = R1; R1 = R0; R0 = (uint)match_offset;
                                    }
                                    else if (match_offset == 0)
                                    {
                                        match_offset = (int)R0;
                                    }
                                    else if (match_offset == 1)
                                    {
                                        match_offset = (int)R1;
                                        R1 = R0; R0 = (uint)match_offset;
                                    }
                                    else /* match_offset == 2 */
                                    {
                                        match_offset = (int)R2;
                                        R2 = R0; R0 = (uint)match_offset;
                                    }

                                    rundest = (int)window_posn;
                                    this_run -= match_length;

                                    /* copy any wrapped around source data */
                                    if (window_posn >= match_offset)
                                    {
                                        /* no wrap */
                                        runsrc = rundest - match_offset;
                                    }
                                    else
                                    {
                                        runsrc = rundest + ((int)window_size - match_offset);
                                        copy_length = match_offset - (int)window_posn;
                                        if (copy_length < match_length)
                                        {
                                            match_length -= copy_length;
                                            window_posn += (uint)copy_length;
                                            while (copy_length-- > 0) window[rundest++] = window[runsrc++];
                                            runsrc = 0;
                                        }
                                    }
                                    window_posn += (uint)match_length;

                                    /* copy match data - no worries about destination wraps */
                                    while (match_length-- > 0) window[rundest++] = window[runsrc++];
                                }
                            }
                            break;
                        }
                        case Constants.BLOCKTYPE.ALIGNED:
                        {
                            while (this_run > 0)
                            {
                                main_element = (int)ReadHuffSym(m_state.MAINTREE_table, m_state.MAINTREE_len,
                                                                          Constants.MAINTREE_MAXSYMBOLS, Constants.MAINTREE_TABLEBITS,
                                                                          bitbuf);

                                if (main_element < Constants.NUM_CHARS)
                                {
                                    /* literal 0 to NUM_CHARS-1 */
                                    window[window_posn++] = (byte)main_element;
                                    this_run--;
                                }
                                else
                                {
                                    /* match: NUM_CHARS + ((slot<<3) | length_header (3 bits)) */
                                    main_element -= Constants.NUM_CHARS;

                                    match_length = main_element & Constants.NUM_PRIMARY_LENGTHS;
                                    if (match_length == Constants.NUM_PRIMARY_LENGTHS)
                                    {
                                        length_footer = (int)ReadHuffSym(m_state.LENGTH_table, m_state.LENGTH_len,
                                                                         Constants.LENGTH_MAXSYMBOLS, Constants.LENGTH_TABLEBITS,
                                                                         bitbuf);
                                        match_length += length_footer;
                                    }
                                    match_length += Constants.MIN_MATCH;

                                    match_offset = main_element >> 3;

                                    if (match_offset > 2)
                                    {
                                        /* not repeated offset */
                                        extra = extra_bits[match_offset];
                                        match_offset = (int)position_base[match_offset] - 2;
                                        if (extra > 3)
                                        {
                                            /* verbatim and aligned bits */
                                            extra -= 3;
                                            verbatim_bits = (int)bitbuf.ReadBits((byte)extra);
                                            match_offset += (verbatim_bits << 3);
                                            aligned_bits = (int)ReadHuffSym(m_state.ALIGNED_table, m_state.ALIGNED_len,
                                                                       Constants.ALIGNED_MAXSYMBOLS, Constants.ALIGNED_TABLEBITS,
                                                                       bitbuf);
                                            match_offset += aligned_bits;
                                        }
                                        else if (extra == 3)
                                        {
                                            /* aligned bits only */
                                            aligned_bits = (int)ReadHuffSym(m_state.ALIGNED_table, m_state.ALIGNED_len,
                                                                       Constants.ALIGNED_MAXSYMBOLS, Constants.ALIGNED_TABLEBITS,
                                                                       bitbuf);
                                            match_offset += aligned_bits;
                                        }
                                        else if (extra > 0) /* extra==1, extra==2 */
                                        {
                                            /* verbatim bits only */
                                            verbatim_bits = (int)bitbuf.ReadBits((byte)extra);
                                            match_offset += verbatim_bits;
                                        }
                                        else /* extra == 0 */
                                        {
                                            /* ??? */
                                            match_offset = 1;
                                        }

                                        /* update repeated offset LRU queue */
                                        R2 = R1; R1 = R0; R0 = (uint)match_offset;
                                    }
                                    else if (match_offset == 0)
                                    {
                                        match_offset = (int)R0;
                                    }
                                    else if (match_offset == 1)
                                    {
                                        match_offset = (int)R1;
                                        R1 = R0; R0 = (uint)match_offset;
                                    }
                                    else /* match_offset == 2 */
                                    {
                                        match_offset = (int)R2;
                                        R2 = R0; R0 = (uint)match_offset;
                                    }

                                    rundest = (int)window_posn;
                                    this_run -= match_length;

                                    /* copy any wrapped around source data */
                                    if (window_posn >= match_offset)
                                    {
                                        /* no wrap */
                                        runsrc = rundest - match_offset;
                                    }
                                    else
                                    {
                                        runsrc = rundest + ((int)window_size - match_offset);
                                        copy_length = match_offset - (int)window_posn;
                                        if (copy_length < match_length)
                                        {
                                            match_length -= copy_length;
                                            window_posn += (uint)copy_length;
                                            while (copy_length-- > 0) window[rundest++] = window[runsrc++];
                                            runsrc = 0;
                                        }
                                    }
                                    window_posn += (uint)match_length;

                                    /* copy match data - no worries about destination wraps */
                                    while (match_length-- > 0) window[rundest++] = window[runsrc++];
                                }
                            }
                            break;
                        }
                        case Constants.BLOCKTYPE.UNCOMPRESSED:
                        {
                            if ((inputStream.Position + this_run) > endpos) throw new EndOfStreamException();

                            byte[] temp_buffer = new byte[this_run];
                            inputStream.Read(temp_buffer, 0, this_run);
                            temp_buffer.CopyTo(window, window_posn);
                            window_posn += (uint)this_run;
                            break;
                        }
                        default:
                        {
                            throw new InvalidOperationException("Unknown block type " + m_state.block_type);
                        }
                    }
                }
            }

            if (togo != 0) throw new InvalidOperationException("togo is not equal to 0");
            int start_window_pos = (int)window_posn;
            if (start_window_pos == 0) start_window_pos = (int)window_size;
            start_window_pos -= outputLength;
            outputStream.Write(window, start_window_pos, outputLength);

            m_state.window_posn = window_posn;
            m_state.R0 = R0;
            m_state.R1 = R1;
            m_state.R2 = R2;

            // TODO finish intel E8 decoding
            #region intel E8 decoding
            if ((m_state.frames_read++ < 32768) && m_state.intel_filesize != 0)
            {
                if (outputLength <= 6 || m_state.intel_started == 0)
                {
                    m_state.intel_curpos += outputLength;
                }
                else
                {
                    int dataend = outputLength - 10;
                    uint curpos = (uint)m_state.intel_curpos;
                    uint filesize = (uint)m_state.intel_filesize;
                    uint abs_off, rel_off;

                    m_state.intel_curpos = (int)curpos + outputLength;

                    while (outputStream.Position < dataend)
                    {
                        if (outputStream.ReadByte() != 0xE8) { curpos++; continue; }
                        //abs_off = 
                    }
                }
                throw new NotImplementedException("intel e8 decoding not finished");
            }
            #endregion
        }

        // READ_LENGTHS(table, first, last)
        // if(lzx_read_lens(LENTABLE(table), first, last, bitsleft))
        //   return ERROR (ILLEGAL_DATA)
        // 

        // TODO make returns throw exceptions
        private int MakeDecodeTable(uint nsyms, uint nbits, byte[] length, ushort[] table)
        {
            ushort sym;
            uint leaf;
            byte bit_num = 1;
            uint fill;
            uint pos = 0; // the current position in the decode table
            uint table_mask = (uint)(1 << (int)nbits);
            uint bit_mask = table_mask >> 1; // don't do 0 length codes
            uint next_symbol = bit_mask;	// base of allocation for long codes

            // fill entries for codes short enough for a direct mapping
            while (bit_num <= nbits)
            {
                for (sym = 0; sym < nsyms; sym++)
                {
                    if (length[sym] == bit_num)
                    {
                        leaf = pos;

                        if ((pos += bit_mask) > table_mask) return 1; /* table overrun */

                        /* fill all possible lookups of this symbol with the symbol itself */
                        fill = bit_mask;
                        while (fill-- > 0) table[leaf++] = sym;
                    }
                }
                bit_mask >>= 1;
                bit_num++;
            }

            // if there are any codes longer than nbits
            if (pos != table_mask)
            {
                // clear the remainder of the table
                for (sym = (ushort)pos; sym < table_mask; sym++) table[sym] = 0;

                // give ourselves room for codes to grow by up to 16 more bits
                pos <<= 16;
                table_mask <<= 16;
                bit_mask = 1 << 15;

                while (bit_num <= 16)
                {
                    for (sym = 0; sym < nsyms; sym++)
                    {
                        if (length[sym] == bit_num)
                        {
                            leaf = pos >> 16;
                            for (fill = 0; fill < bit_num - nbits; fill++)
                            {
                                /* if this path hasn't been taken yet, 'allocate' two entries */
                                if (table[leaf] == 0)
                                {
                                    table[(next_symbol << 1)] = 0;
                                    table[(next_symbol << 1) + 1] = 0;
                                    table[leaf] = (ushort)(next_symbol++);
                                }
                                /* follow the path and select either left or right for next bit */
                                leaf = (uint)(table[leaf] << 1);
                                if (((pos >> (int)(15 - fill)) & 1) == 1) leaf++;
                            }
                            table[leaf] = sym;

                            if ((pos += bit_mask) > table_mask) return 1;
                        }
                    }
                    bit_mask >>= 1;
                    bit_num++;
                }
            }

            // full table?
            if (pos == table_mask) return 0;

            // either erroneous table, or all elements are 0 - let's find out.
            for (sym = 0; sym < nsyms; sym++) if (length[sym] != 0) return 1;
            return 0;
        }

        // TODO throw exceptions instead of returns
        private void ReadLengths(byte[] lens, uint first, uint last, BitBuffer bitbuf)
        {
            uint x, y;
            int z;

            // hufftbl pointer here?

            for (x = 0; x < 20; x++)
            {
                y = bitbuf.ReadBits(4);
                m_state.PRETREE_len[x] = (byte)y;
            }
            MakeDecodeTable(Constants.PRETREE_MAXSYMBOLS, Constants.PRETREE_TABLEBITS,
                            m_state.PRETREE_len, m_state.PRETREE_table);

            for (x = first; x < last;)
            {
                z = (int)ReadHuffSym(m_state.PRETREE_table, m_state.PRETREE_len,
                                Constants.PRETREE_MAXSYMBOLS, Constants.PRETREE_TABLEBITS, bitbuf);
                if (z == 17)
                {
                    y = bitbuf.ReadBits(4); y += 4;
                    while (y-- != 0) lens[x++] = 0;
                }
                else if (z == 18)
                {
                    y = bitbuf.ReadBits(5); y += 20;
                    while (y-- != 0) lens[x++] = 0;
                }
                else if (z == 19)
                {
                    y = bitbuf.ReadBits(1); y += 4;
                    z = (int)ReadHuffSym(m_state.PRETREE_table, m_state.PRETREE_len,
                                Constants.PRETREE_MAXSYMBOLS, Constants.PRETREE_TABLEBITS, bitbuf);
                    z = lens[x] - z; if (z < 0) z += 17;
                    while (y-- != 0) lens[x++] = (byte)z;
                }
                else
                {
                    z = lens[x] - z; if (z < 0) z += 17;
                    lens[x++] = (byte)z;
                }
            }
        }

        private uint ReadHuffSym(ushort[] table, byte[] lengths, uint nsyms, uint nbits, BitBuffer bitbuf)
        {
            uint i, j;
            bitbuf.EnsureBits(16);
            if ((i = table[bitbuf.PeekBits((byte)nbits)]) >= nsyms)
            {
                j = (uint)(1 << (int)((sizeof(uint) * 8) - nbits));
                do
                {
                    j >>= 1; i <<= 1; i |= (bitbuf.GetBuffer() & j) != 0 ? (uint)1 : 0;
                    if (j == 0) return 0; // TODO throw proper exception
                } while ((i = table[i]) >= nsyms);
            }
            j = lengths[i];
            bitbuf.RemoveBits((byte)j);

            return i;
        }

        #region Our BitBuffer Class
        private class BitBuffer
        {
            uint buffer;
            byte bitsleft;
            Stream byteStream;

            public BitBuffer(Stream stream)
            {
                byteStream = stream;
                InitBitStream();
            }

            public void InitBitStream()
            {
                buffer = 0;
                bitsleft = 0;
            }

            public void EnsureBits(byte bits)
            {
                while (bitsleft < bits)
                {
                    int lo = (byte)byteStream.ReadByte();
                    int hi = (byte)byteStream.ReadByte();
                    int amount2shift = sizeof(uint) * 8 - 16 - bitsleft;
                    buffer |= (uint)(((hi << 8) | lo) << (sizeof(uint) * 8 - 16 - bitsleft));
                    bitsleft += 16;
                }
            }

            public uint PeekBits(byte bits)
            {
                return (buffer >> ((sizeof(uint) * 8) - bits));
            }

            public void RemoveBits(byte bits)
            {
                buffer <<= bits;
                bitsleft -= bits;
            }

            public uint ReadBits(byte bits)
            {
                uint ret = 0;

                if (bits > 0)
                {
                    EnsureBits(bits);
                    ret = PeekBits(bits);
                    RemoveBits(bits);
                }

                return ret;
            }

            public uint GetBuffer()
            {
                return buffer;
            }

            public byte GetBitsLeft()
            {
                return bitsleft;
            }
        }
        #endregion

        struct LzxState
        {
            public uint R0, R1, R2;			/* for the LRU offset system				*/
            public ushort main_elements;		/* number of main tree elements				*/
            public int header_read;		/* have we started decoding at all yet? 	*/
            public Constants.BLOCKTYPE block_type;			/* type of this block						*/
            public uint block_length;		/* uncompressed length of this block 		*/
            public uint block_remaining;	/* uncompressed bytes still left to decode	*/
            public uint frames_read;		/* the number of CFDATA blocks processed	*/
            public int intel_filesize;		/* magic header value used for transform	*/
            public int intel_curpos;		/* current offset in transform space		*/
            public int intel_started;		/* have we seen any translateable data yet?	*/

            public ushort[] PRETREE_table;
            public byte[] PRETREE_len;
            public ushort[] MAINTREE_table;
            public byte[] MAINTREE_len;
            public ushort[] LENGTH_table;
            public byte[] LENGTH_len;
            public ushort[] ALIGNED_table;
            public byte[] ALIGNED_len;

            // NEEDED MEMBERS
            // CAB actualsize
            // CAB window
            // CAB window_size
            // CAB window_posn
            public uint actual_size;
            public byte[] window;
            public uint window_size;
            public uint window_posn;
        }
    }
}
