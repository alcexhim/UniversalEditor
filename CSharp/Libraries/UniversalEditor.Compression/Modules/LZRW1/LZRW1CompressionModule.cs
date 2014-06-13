using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;

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
            StreamAccessor sao = new StreamAccessor(outputStream);
            StreamAccessor sai = new StreamAccessor(inputStream);
            Reader br = new Reader(sai);
            Writer bw = new Writer(sao);

            /*
            byte *p_src = p_src_first + 4,
            *p_dst = p_dst_first,
                *p_dst_end = p_dst_first + dst_len;
            byte  *p_src_post = p_src_first + src_len;
            byte  *p_src_max16 = p_src_first + src_len - (16 * 2);
            */

            uint control = 1;

            uint flag = br.ReadUInt32();
            if (flag == FLAG_COPY)
            {
                // entire stream is uncompressed, so read it all
                byte[] data = br.ReadToEnd();
                bw.WriteBytes(data);
            }
            while (!br.EndOfStream)
            {
                uint unroll;
                if (control == 1)
                {
                    byte byte1 = br.ReadByte();
                    byte byte2 = br.ReadByte();

                    uint primaryControlValue = (uint)(0x10000 | byte1);
                    uint secondaryControlValue = (uint)(byte2 << 8);

                    control = primaryControlValue;
                    control |= secondaryControlValue;
                }
                unroll = (uint)((br.Accessor.Position < br.Accessor.Length - 32) ? 16 : 1);
                while (unroll-- != 0)
                {
                    if ((control & 1) != 0)
                    {
                        byte offsetCalcByte1 = br.ReadByte();
                        byte offsetCalcByte2 = br.ReadByte();

                        ushort offset = (ushort)(((offsetCalcByte1 & 0xF0) << 4) | offsetCalcByte2);
                        ushort len = (ushort)((offsetCalcByte1 & 0xF) | 4);

                        long oldpos = sao.Position;
                        sao.Position = sao.Length - offset;
                        
                        IO.Reader bro = new Reader(sao);
                        byte value = bro.ReadByte();

                        sao.Position = oldpos;

                        // if((p_dst + offset) > p_dst_end) return(-1);

                        
                        for (int i = 0; i < len; i++)
                        {
                            bw.WriteByte(value);
                        }

                        System.IO.File.WriteAllBytes(@"C:\Temp\Test.dat", outputStream.ToByteArray());
                    }
                    else
                    {
                        if (br.EndOfStream) return;
                        bw.WriteByte(br.ReadByte());
                        System.IO.File.WriteAllBytes(@"C:\Temp\Test.dat", outputStream.ToByteArray());
                    }
                    control >>= 1;
                }
            }
        }
    }
}
