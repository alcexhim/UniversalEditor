using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.MSCompressed
{
    public enum MSCompressedKWAJCompressionMethod
    {
        /// <summary>
        /// No compression
        /// </summary>
        None = 0,
        /// <summary>
        /// No compression, data is XORed with byte 0xFF
        /// </summary>
        XOR = 1,
        /// <summary>
        /// The same compression method as regular SZDD
        /// </summary>
        SZDD = 2,
        /// <summary>
        /// LZ + Huffman "Jeff Johnson" compression
        /// </summary>
        JeffJohnson = 3,
        /// <summary>
        /// MS-ZIP
        /// </summary>
        MSZIP = 4
    }
}
