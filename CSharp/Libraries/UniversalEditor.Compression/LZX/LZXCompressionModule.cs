using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.LZX
{
    class LZXCompressionModule
    {
        /***************************************************************************
         *                        lzx.c - LZX decompression routines               *
         *                           -------------------                           *
         *                                                                         *
         *  maintainer: Jed Wing <jedwin@ugcs.caltech.edu>                         *
         *  source:     modified lzx.c from cabextract v0.5                        *
         *  notes:      This file was taken from cabextract v0.5, which was,       *
         *              itself, a modified version of the lzx decompression code   *
         *              from unlzx.                                                *
         *                                                                         *
         *  platforms:  In its current incarnation, this file has been tested on   *
         *              two different Linux platforms (one, redhat-based, with a   *
         *              2.1.2 glibc and gcc 2.95.x, and the other, Debian, with    *
         *              2.2.4 glibc and both gcc 2.95.4 and gcc 3.0.2).  Both were *
         *              Intel x86 compatible machines.                             *
         ***************************************************************************/

        /***************************************************************************
         *
         *   Copyright(C) Stuart Caie
         *
         * This library is free software; you can redistribute it and/or
         * modify it under the terms of the GNU Lesser General Public
         * License as published by the Free Software Foundation; either
         * version 2.1 of the License, or (at your option) any later version.
         *
         * This library is distributed in the hope that it will be useful,
         * but WITHOUT ANY WARRANTY; without even the implied warranty of
         * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
         * Lesser General Public License for more details.
         *
         * You should have received a copy of the GNU Lesser General Public
         * License along with this library; if not, write to the Free Software
         * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301, USA
         *
         ***************************************************************************/

        public class Lzx
        {
            const int LZX_MIN_MATCH = (2);
            const int LZX_MAX_MATCH = (257);
            const int LZX_NUM_CHARS = (256);
            const int LZX_BLOCKTYPE_INVALID = (0);   /* also blocktypes 4-7 invalid */
            const int LZX_BLOCKTYPE_VERBATIM = (1);
            const int LZX_BLOCKTYPE_ALIGNED = (2);
            const int LZX_BLOCKTYPE_UNCOMPRESSED = (3);
            const int LZX_PRETREE_NUM_ELEMENTS = (20);
            const int LZX_ALIGNED_NUM_ELEMENTS = (8);   /* aligned offset tree #elements */
            const int LZX_NUM_PRIMARY_LENGTHS = (7);   /* this one missing from spec! */
            const int LZX_NUM_SECONDARY_LENGTHS = (249); /* length tree #elements */

            const int LZX_PRETREE_MAXSYMBOLS = (LZX_PRETREE_NUM_ELEMENTS);
            const int LZX_PRETREE_TABLEBITS = (6);
            const int LZX_MAINTREE_MAXSYMBOLS = (LZX_NUM_CHARS + 50 * 8);
            const int LZX_MAINTREE_TABLEBITS = (12);
            const int LZX_LENGTH_MAXSYMBOLS = (LZX_NUM_SECONDARY_LENGTHS + 1);
            const int LZX_LENGTH_TABLEBITS = (12);
            const int LZX_ALIGNED_MAXSYMBOLS = (LZX_ALIGNED_NUM_ELEMENTS);
            const int LZX_ALIGNED_TABLEBITS = (7);

            const int LZX_LENTABLE_SAFETY = (64);

            public struct LZXstate
            {
                byte[] window;          /* the actual decoding window              */
                uint window_size;     /* window size (32Kb through 2Mb)          */
                uint actual_size;     /* window size when it was first allocated */
                uint window_posn;     /* current offset within the window        */
                uint R0, R1, R2;      /* for the LRU offset system               */
                ushort main_elements;   /* number of main tree elements            */
                int header_read;     /* have we started decoding at all yet?    */
                ushort block_type;      /* type of this block                      */
                uint block_length;    /* uncompressed length of this block       */
                uint block_remaining; /* uncompressed bytes still left to decode */
                uint frames_read;     /* the number of CFDATA blocks processed   */
                int intel_filesize;  /* magic header value used for transform   */
                int intel_curpos;    /* current offset in transform space       */
                int intel_started;   /* have we seen any translatable data yet? */

                ushort[] PRETREE_table;
                byte[] PRETREE_len;

                ushort[] MAINTREE_table;
                byte[] MAINTREE_len;

                ushort[] LENGTH_table;
                byte[] LENGTH_len;

                ushort[] ALIGNED_table;
                byte[] ALIGNED_len;

                public LZXstate(int window)
                {
                    PRETREE_table = new ushort[(1 << LZX_PRETREE_TABLEBITS) + (LZX_PRETREE_MAXSYMBOLS << 1)];
                    PRETREE_len = new byte[LZX_PRETREE_MAXSYMBOLS + LZX_LENTABLE_SAFETY];

                    MAINTREE_table = new ushort[(1 << LZX_MAINTREE_TABLEBITS) + (LZX_MAINTREE_MAXSYMBOLS << 1)];
                    MAINTREE_len = new byte[LZX_MAINTREE_MAXSYMBOLS + LZX_LENTABLE_SAFETY];

                    LENGTH_table = new ushort[(1 << LZX_LENGTH_TABLEBITS) + (LZX_LENGTH_MAXSYMBOLS << 1)];
                    LENGTH_len = new byte[LZX_LENGTH_MAXSYMBOLS + LZX_LENTABLE_SAFETY];

                    ALIGNED_table = new ushort[(1 << LZX_ALIGNED_TABLEBITS) + (LZX_ALIGNED_MAXSYMBOLS << 1)];
                    ALIGNED_len = new byte[LZX_ALIGNED_MAXSYMBOLS + LZX_LENTABLE_SAFETY];

                    // LZXinit
                    {
                        uint wndsize = (uint)(1 << (int)window);
                        int i, posn_slots;

                        // LZX supports window sizes of 2^15 (32Kb) through 2^21 (2Mb)
                        // if a previously allocated window is big enough, keep it
                        if (window < 15 || window > 21) throw (new Exception("Invalid window size"));

                        /* allocate state and associated window */
                        this.window = new byte[wndsize];
                        this.actual_size = wndsize;
                        this.window_size = wndsize;

                        /* calculate required position slots */
                        if (window == 20) posn_slots = 42;
                        else if (window == 21) posn_slots = 50;
                        else posn_slots = window << 1;

                        /** alternatively **/
                        /* posn_slots=i=0; while (i < wndsize) i += 1 << extra_bits[posn_slots++]; */

                        /* initialize other state */
                        this.R0 = this.R1 = this.R2 = 1;
                        this.main_elements = (ushort)(LZX_NUM_CHARS + (posn_slots << 3));
                        this.header_read = 0;
                        this.frames_read = 0;
                        this.block_remaining = 0;
                        this.block_type = LZX_BLOCKTYPE_INVALID;
                        this.intel_curpos = 0;
                        this.intel_started = 0;
                        this.window_posn = 0;

                        /* initialise tables to 0 (because deltas will be applied to them) */
                        for (i = 0; i < LZX_MAINTREE_MAXSYMBOLS; i++) this.MAINTREE_len[i] = 0;
                        for (i = 0; i < LZX_LENGTH_MAXSYMBOLS; i++) this.LENGTH_len[i] = 0;

                        ////
                        this.block_length = 0;
                        this.intel_filesize = 0;
                    }
                }

                public void Reset()
                {
                    this.R0 = this.R1 = this.R2 = 1;
                    this.header_read = 0;
                    this.frames_read = 0;
                    this.block_remaining = 0;
                    this.block_type = LZX_BLOCKTYPE_INVALID;
                    this.intel_curpos = 0;
                    this.intel_started = 0;
                    this.window_posn = 0;

                    for (int i = 0; i < LZX_MAINTREE_MAXSYMBOLS + LZX_LENTABLE_SAFETY; i++) this.MAINTREE_len[i] = 0;
                    for (int i = 0; i < LZX_LENGTH_MAXSYMBOLS + LZX_LENTABLE_SAFETY; i++) this.LENGTH_len[i] = 0;
                }
            };

            /* LZX decruncher */

            /* Microsoft's LZX document and their implementation of the
             * com.ms.util.cab Java package do not concur.
             *
             * In the LZX document, there is a table showing the correlation between
             * window size and the number of position slots. It states that the 1MB
             * window = 40 slots and the 2MB window = 42 slots. In the implementation,
             * 1MB = 42 slots, 2MB = 50 slots. The actual calculation is 'find the
             * first slot whose position base is equal to or more than the required
             * window size'. This would explain why other tables in the document refer
             * to 50 slots rather than 42.
             *
             * The constant NUM_PRIMARY_LENGTHS used in the decompression pseudocode
             * is not defined in the specification.
             *
             * The LZX document does not state the uncompressed block has an
             * uncompressed length field. Where does this length field come from, so
             * we can know how large the block is? The implementation has it as the 24
             * bits following after the 3 blocktype bits, before the alignment
             * padding.
             *
             * The LZX document states that aligned offset blocks have their aligned
             * offset huffman tree AFTER the main and length trees. The implementation
             * suggests that the aligned offset tree is BEFORE the main and length
             * trees.
             *
             * The LZX document decoding algorithm states that, in an aligned offset
             * block, if an extra_bits value is 1, 2 or 3, then that number of bits
             * should be read and the result added to the match offset. This is
             * correct for 1 and 2, but not 3, where just a huffman symbol (using the
             * aligned tree) should be read.
             *
             * Regarding the E8 preprocessing, the LZX document states 'No translation
             * may be performed on the last 6 bytes of the input block'. This is
             * correct.  However, the pseudocode provided checks for the *E8 leader*
             * up to the last 6 bytes. If the leader appears between -10 and -7 bytes
             * from the end, this would cause the next four bytes to be modified, at
             * least one of which would be in the last 6 bytes, which is not allowed
             * according to the spec.
             *
             * The specification states that the huffman trees must always contain at
             * least one element. However, many CAB files contain blocks where the
             * length tree is completely empty (because there are no matches), and
             * this is expected to succeed.
             */


            /* LZX uses what it calls 'position slots' to represent match offsets.
             * What this means is that a small 'position slot' number and a small
             * offset from that slot are encoded instead of one large offset for
             * every match.
             * - position_base is an index to the position slot bases
             * - extra_bits states how many bits of offset-from-base data is needed.
             */
            static byte[] extra_bits = new byte[] {
                 0,  0,  0,  0,  1,  1,  2,  2,  3,  3,  4,  4,  5,  5,  6,  6,
                 7,  7,  8,  8,  9,  9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14,
                15, 15, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17,
                17, 17, 17
            };

            static uint[] position_base = new uint[] {
                      0,       1,       2,      3,      4,      6,      8,     12,     16,     24,     32,       48,      64,      96,     128,     192,
                    256,     384,     512,    768,   1024,   1536,   2048,   3072,   4096,   6144,   8192,    12288,   16384,   24576,   32768,   49152,
                  65536,   98304,  131072, 196608, 262144, 393216, 524288, 655360, 786432, 917504, 1048576, 1179648, 1310720, 1441792, 1572864, 1703936,
                1835008, 1966080, 2097152
            };
        }
    }
}
