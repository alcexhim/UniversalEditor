using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.LZX.Internal
{
    internal class Constants
    {
        public const ushort MIN_MATCH = 2;
        public const ushort MAX_MATCH = 257;
        public const ushort NUM_CHARS = 256;

        public enum BLOCKTYPE
        {
            INVALID = 0,
            VERBATIM = 1,
            ALIGNED = 2,
            UNCOMPRESSED = 3
        }

        public const ushort PRETREE_NUM_ELEMENTS = 20;
        public const ushort ALIGNED_NUM_ELEMENTS = 8;
        public const ushort NUM_PRIMARY_LENGTHS = 7;
        public const ushort NUM_SECONDARY_LENGTHS = 249;

        public const ushort PRETREE_MAXSYMBOLS = PRETREE_NUM_ELEMENTS;
        public const ushort PRETREE_TABLEBITS = 6;
        public const ushort MAINTREE_MAXSYMBOLS = NUM_CHARS + 50 * 8;
        public const ushort MAINTREE_TABLEBITS = 12;
        public const ushort LENGTH_MAXSYMBOLS = NUM_SECONDARY_LENGTHS + 1;
        public const ushort LENGTH_TABLEBITS = 12;
        public const ushort ALIGNED_MAXSYMBOLS = ALIGNED_NUM_ELEMENTS;
        public const ushort ALIGNED_TABLEBITS = 7;

        public const ushort LENTABLE_SAFETY = 64;
    }
}
