using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.MSCompressed
{
    [Flags()]
    public enum MSCompressedKWAJHeaderFlags
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// 4 bytes: decompressed length of file
        /// </summary>
        HasDecompressedLength = 0x00000001,
        /// <summary>
        /// 2 bytes: unknown purpose
        /// </summary>
        UnknownBit1 = 0x00000002,
        /// <summary>
        /// 2 bytes: length of data, followed by that many bytes of (unknown purpose) data
        /// </summary>
        HasExtraData = 0x00000004,
        /// <summary>
        /// 1-9 bytes: null-terminated string with max length 8: file name
        /// </summary>
        HasFileName = 0x00000008,
        /// <summary>
        /// 1-4 bytes: null-terminated string with max length 3: file extension
        /// </summary>
        HasFileExtension = 0x00000010,
        /// <summary>
        /// 2 bytes: length of data, followed by that many bytes of (arbitrary text) data 
        /// </summary>
        HasExtraText = 0x00000011
    }
}
