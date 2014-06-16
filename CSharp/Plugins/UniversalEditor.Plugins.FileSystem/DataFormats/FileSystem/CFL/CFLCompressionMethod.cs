using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
    public enum CFLCompressionMethod
    {
        /// <summary>
        /// No compression
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// Zlib, normal compression level
        /// </summary>
        ZlibNormal = 0x00000001,
        /// <summary>
        /// Zlib, maximum compression level
        /// </summary>
        ZlibMaximum = 0x00000901,
        /// <summary>
        /// LZSS
        /// </summary>
        LZSS = 0x00000002,
        /// <summary>
        /// Bzip2
        /// </summary>
        BZip2 = 0x00000003,
        /// <summary>
        /// Tries out all registered compressors and uses the best result.
        /// </summary>
        Best = 0x0000FFFF
    }
}
