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

            int flag = br.ReadInt32();
            if (flag == FLAG_COPY)
            {
                // entire stream is uncompressed, so read it all
                byte[] data = br.ReadToEnd();
                bw.WriteBytes(data);
            }
            else
            {
                // flag is not copy, so we have to decompress
                ushort controlbits = 0, control = 0;
                while (!br.EndOfStream)
                {
                    if (controlbits == 0)
                    {
                        // control bits are 0, so it's time to read a new control ushort
                        // don't use ReadUInt16 here because we need to keep the bytes
                        // for future use
                        control = br.ReadByte();
                        control |= (ushort)(br.ReadByte() << 8);
                        controlbits = 16;
                    }
                    if ((control & 1) != 0)
                    {
                        ushort offset, len;
                        offset = (ushort)((br.PeekByte() & 0xF0) << 4);
                        len = (ushort)(1 + (br.ReadByte() & 0xF));
                        offset += (ushort)(br.ReadByte() & 0xFF);

                        bw.Flush();

                        long oldpos = sao.Position;
                        sao.Position = sao.Length - offset;
                        byte[] nextdata = sao.Reader.ReadBytes(len);
                        sao.Position = oldpos;

                        bw.WriteBytes(nextdata);
                    }
                    else
                    {
                        // control bit is 0, so perform a simple copy of the next byte
                        bw.WriteByte(br.ReadByte());
                    }

                    // pop the latest control bit off the control ushort
                    control >>= 1;
                    controlbits--;
                }
            }
            bw.Flush();
        }
    }
}
