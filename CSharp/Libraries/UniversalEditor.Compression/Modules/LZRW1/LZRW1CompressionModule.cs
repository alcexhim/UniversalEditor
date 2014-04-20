using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.LZRW1
{
    public class LZRW1CompressionModule : CompressionModule
    {
        /// <summary>
        /// Number of bytes used by copy flag
        /// </summary>
        private const byte FLAG_BYTES = 0x04;

        private const byte FLAG_COMPRESS = 0x00;
        private const byte FLAG_COPY = 0x01;

        public static byte[] Decompress(byte[] input)
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

				Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor();
                IO.Writer bw = new IO.Writer(ma);

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
                        byte[] out1 = ma.ToArray();

                        p = out1.Length - offset;
                        while (len-- != 0)
                        {
                            bw.WriteByte(out1[p++]);
                        }
                    }
                    else
                    {
						bw.WriteByte(input[p_src++]);
                    }
                    control >>= 1;
                    controlbits--;
                }
                bw.Flush();
                bw.Close();
                output = ma.ToArray();
            }
            return output;
        }

        public override string Name
        {
            get { return "LZRW1"; }
        }

        protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            throw new NotImplementedException();
        }

        protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {
            throw new NotImplementedException();
        }
    }
}
