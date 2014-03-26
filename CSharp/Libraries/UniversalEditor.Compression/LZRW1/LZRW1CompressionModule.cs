using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.LZRW1
{
    public class LZRW1CompressionModule
    {
        /// <summary>
        /// Number of bytes used by copy flag
        /// </summary>
        private const byte FLAG_BYTES = 0x04;

        private const byte FLAG_COMPRESS = 0x00;
        private const byte FLAG_COPY = 0x01;

        public unsafe static byte[] Decompress(byte[] input)
        {
            byte[] output = null;

            if (input[0] == FLAG_COPY)
            {
                output = new byte[input.Length - FLAG_BYTES];
                Array.Copy(input, FLAG_BYTES, output, 0, input.Length - FLAG_BYTES);
            }
            else
            {
                ushort controlbits = 0, control = 0;
                int p_src = FLAG_BYTES;

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.BinaryWriter bw = new IO.BinaryWriter(ms);

                while (p_src != input.Length)
                {
                    if (controlbits == 0)
                    {
                        control = input[p_src++];
                        control |= (ushort)(input[p_src] << 8);
                        controlbits = 16;
                    }
                    if ((control & 1) != 0)
                    {
                        ushort offset, len;
                        int p;
                        offset = (ushort)((input[p_src] & 0xF0) << 4);
                        len = (ushort)(1 + (input[p_src++] & 0xF));
                        offset += (ushort)(input[p_src++] & 0xFF);

                        bw.Flush();
                        byte[] out1 = ms.ToArray();

                        p = out1.Length - offset;
                        while (len-- != 0)
                        {
                            bw.Write(out1[p++]);
                        }
                    }
                    else
                    {
                        bw.Write(input[p_src++]);
                    }
                    control >>= 1;
                    controlbits--;
                }
                bw.Flush();
                bw.Close();
                output = ms.ToArray();
            }
            return output;
        }
    }
}
