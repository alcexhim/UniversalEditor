using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG.Internal
{
    public class BlockInfo
    {
        public ALZipCompressionMethod compressionMethod;
        public byte hint;
        public uint decompressedSize;
        public uint compressedSize;
        public uint crc32;
        public long offset;
        public byte[] compressedData;

        public BlockInfo(ALZipCompressionMethod compressionMethod, byte hint, uint decompressedSize, uint compressedSize, uint crc32, long offset, byte[] compressedData = null)
        {
            // TODO: Complete member initialization
            this.compressionMethod = compressionMethod;
            this.hint = hint;
            this.decompressedSize = decompressedSize;
            this.compressedSize = compressedSize;
            this.crc32 = crc32;
            this.offset = offset;
            this.compressedData = compressedData;
        }
    }
}
