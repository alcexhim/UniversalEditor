using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
    internal struct CFFOLDER
    {
        /// <summary>
        /// offset of the first CFDATA block in this folder
        /// </summary>
        public uint firstDataBlockOffset;
        /// <summary>
        /// number of CFDATA blocks in this folder
        /// </summary>
        public ushort dataBlockCount;
        /// <summary>
        /// compression type indicator
        /// </summary>
        public CABCompressionMethod compressionMethod;
        /// <summary>
        /// (optional) per-folder reserved area
        /// </summary>
        public byte[] reservedArea;
    }
}
