using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.LZPL2
{
    public class LZPL2CompressionModule
    {
        private int mvarBufferSize = 4096;
        public int BufferSize { get { return mvarBufferSize; } set { mvarBufferSize = value; } }

        private static LZPL2CompressionModule mvarDefaultModule = new LZPL2CompressionModule();
        public static LZPL2CompressionModule DefaultModule { get { return mvarDefaultModule; } }

        public byte[] Decompress(byte[] data, uint decompressedSize)
        {
            byte[] buffer = new byte[mvarBufferSize];
            byte mask = 0, flags = 0, x = 0, y = 0, b = 0;

            uint srcpos = 0, dstpos = 0;

            byte[] decompressedData = new byte[decompressedSize];
            while ((srcpos < data.Length) && (dstpos < decompressedData.Length))
            {
                if (mask == 0)
                {
                    flags = data[srcpos++];
                    if (srcpos >= data.Length) break;
                    mask = 1;
                }

                if ((flags & mask) != 0)
                {
                    // Raw byte
                    buffer[dstpos % mvarBufferSize] = data[srcpos];
                    decompressedData[dstpos++] = data[srcpos++];
                }
                else
                {
                    // Pointer: 0xAB 0xCD (CAB=pointer, D=length)
                    x = data[srcpos++];
                    y = data[srcpos++];

                    int off = (((y & 0xf0) << 4) | x) + 18;
                    int len = (y & 0x0f) + 3;

                    while ((len-- > 0) && (dstpos < decompressedData.Length))
                    {
                        b = buffer[off++ % mvarBufferSize];
                        buffer[dstpos % mvarBufferSize] = b;
                        decompressedData[dstpos++] = b;
                    }
                }
                mask <<= 1;
            }
            return decompressedData;
        }

    }
}
