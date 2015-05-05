using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
    internal struct CFDATA
    {
        /// <summary>
        /// checksum of this CFDATA entry
        /// </summary>
        public uint checksum;
        /// <summary>
        /// number of compressed bytes in this block
        /// </summary>
        public ushort compressedLength;
        /// <summary>
        /// number of uncompressed bytes in this block
        /// </summary>
        public ushort decompressedLength;
        /// <summary>
        /// (optional) per-datablock reserved area
        /// </summary>
        public byte[] reservedArea;
        /// <summary>
        /// compressed data bytes 
        /// </summary>
        public byte[] data;
    }
}
