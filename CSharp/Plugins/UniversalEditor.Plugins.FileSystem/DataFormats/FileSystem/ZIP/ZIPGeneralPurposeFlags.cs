using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
    public enum ZIPGeneralPurposeFlags : short
    {
        /// <summary>
        /// The CRC-32 and file sizes are not known when the header is written. The fields in the local header are filled with zero,
        /// and the CRC-32 and size are appended in a 12-byte structure (optionally preceded by a 4-byte signature) immediately after
        /// the compressed data.
        /// </summary>
        UnknownCRCAndFileSize = 0x08
    }
}
