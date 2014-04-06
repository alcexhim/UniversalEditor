using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
    public class PNGCompressionModule : CompressionModule
    {
        public override string Name
        {
            get { return "PNG"; }
        }

        protected override void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            throw new NotImplementedException();
        }
        protected override void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
        {
            IO.Reader reader = new IO.Reader(new StreamAccessor(inputStream));

            IO.Writer writer = new IO.Writer(new StreamAccessor(outputStream));
            IO.Reader readerOut = new IO.Reader(new StreamAccessor(outputStream));

            for (int y = 0; y < mvarImageHeight; y++)
            {
                PNGFilterType filterType = (PNGFilterType)reader.ReadByte();

                for (int x = 0; x < mvarImageWidth; x++)
                {
                    switch (filterType)
                    {
                        case PNGFilterType.None:
                        {
                            byte r = reader.ReadByte();
                            byte g = reader.ReadByte();
                            byte b = reader.ReadByte();

                            writer.WriteByte(r);
                            writer.WriteByte(g);
                            writer.WriteByte(b);
                            break;
                        }
                        case PNGFilterType.Sub:
                        {
                            // reverse the Sub filter on each pixel
                            byte g = (byte)(Sub(reader.Accessor.Position, reader) + (Raw(reader.Accessor.Position - mvarBytesPerPixel, reader) % 256));

                            writer.WriteByte(g);
                            break;
                        }
                        case PNGFilterType.Up:
                        {
                            // reverse the Up filter on each pixel
                            byte g = (byte)(Up(reader.Accessor.Position, reader) + Prior(reader.Accessor.Position, reader));

                            writer.WriteByte(g);
                            break;
                        }
                        case PNGFilterType.Paeth:
                        {
                            // reverse the Paeth filter on each pixel
                            byte g = (byte)(Paeth(reader.Accessor.Position, reader) + PaethPredictor(Raw(reader.Accessor.Position - mvarBytesPerPixel, reader), Prior(reader.Accessor.Position, reader), Prior(reader.Accessor.Position - mvarBytesPerPixel, reader)));

                            writer.WriteByte(g);
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                }
            }
        }

        private byte PaethPredictor(byte a, byte b, byte c)
        {
            // a = left, b = above, c = upper left
            
            byte p = (byte)(a + b - c); // initial estimate
            byte pa = (byte)(Math.Abs(p - a) % 256); // distance to a
            byte pb = (byte)(Math.Abs(p - b) % 256); // distance to b
            byte pc = (byte)(Math.Abs(p - c) % 256); // distance to c

            // return nearest of a,b,c,
            // breaking ties in order a,b,c.
            if (pa <= pb && pa <= pc)
            {
                return a;
            }
            else if (pb <= pc)
            {
                return b;
            }
            else
            {
                return c;
            }
        }

        private byte Paeth(long position, IO.Reader reader)
        {
            return (byte)(Raw(position, reader) - PaethPredictor(Raw(position - mvarBytesPerPixel, reader), Prior(position, reader), Prior(position - mvarBytesPerPixel, reader)));
        }

        /// <summary>
        /// Predicts the next pixel based on the pixel immediately above the pixel specified at position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private byte Up(long position, IO.Reader reader)
        {
            return (byte)(Raw(position, reader) - Prior(position, reader));
        }

        /// <summary>
        /// Retrieves the unfiltered bytes of the prior scanline at the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private byte Prior(long position, IO.Reader reader)
        {
            // move up one scanline
            position -= mvarImageWidth;

            // retrieve the actual value
            return (byte)(Raw(position, reader));
        }

        private byte Sub(long position, IO.Reader reader)
        {
            return (byte)(Raw(position, reader) - Raw(position - mvarBytesPerPixel, reader));
        }
        private byte Raw(long position, IO.Reader reader)
        {
            if (position < 0) return 0;
            position++;

            long pos = reader.Accessor.Position;
            reader.Accessor.Position = position;

            byte val = reader.ReadByte();
            reader.Accessor.Position = pos + 1;

            return val;
        }

        private int mvarBytesPerPixel = 0;
        public int BytesPerPixel { get { return mvarBytesPerPixel; } set { mvarBytesPerPixel = value; } }

        private int mvarImageWidth = 0;
        public int ImageWidth { get { return mvarImageWidth; } set { mvarImageWidth = value; } }

        private int mvarImageHeight = 0;
        public int ImageHeight { get { return mvarImageHeight; } set { mvarImageHeight = value; } }

        private PNGCompressionMethod mvarCompressionMethod = PNGCompressionMethod.Zlib;
        public PNGCompressionMethod Method { get { return mvarCompressionMethod; } set { mvarCompressionMethod = value; } }
    }
}
