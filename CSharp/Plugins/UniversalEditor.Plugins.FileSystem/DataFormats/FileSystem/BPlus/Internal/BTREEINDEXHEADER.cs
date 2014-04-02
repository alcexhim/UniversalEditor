using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.BPlus.Internal
{
    internal struct BTREEINDEXHEADER
    {
        /// <summary>
        /// Number of free bytes at the end of this page.
        /// </summary>
        public ushort UnusedByteCount;
        /// <summary>
        /// Number of entries in this index-page.
        /// </summary>
        public short PageEntriesCount;
        /// <summary>
        /// Page number of previous page.
        /// </summary>
        public short PreviousPageIndex;
    }
}
