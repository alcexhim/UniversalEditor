using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
    public class PNGDataFormat : DataFormat
    {
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("Portable Network Graphics", new byte?[][] { new byte?[] { (byte)0x89, (byte)'P', (byte)'N', (byte)'G', (byte)0x0D, (byte)0x0A, (byte)0x1A, (byte)0x0A } }, new string[] { "*.png" });
            return dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader br = base.Accessor.Reader;
            PictureObjectModel pic = (objectModel as PictureObjectModel);

            byte[] signature = br.ReadBytes(8);
            br.Endianness = IO.Endianness.BigEndian;

            PNGChunk.PNGChunkCollection chunks = new PNGChunk.PNGChunkCollection();

            while (!br.EndOfStream)
            {
                int chunkLength = br.ReadInt32();
                string chunkType = br.ReadFixedLengthString(4);
                byte[] chunkData = br.ReadBytes(chunkLength);
                int chunkCRC = br.ReadInt32();
                chunks.Add(chunkType, chunkData);
                
                if (chunkType == "IEND") break;
            }

            IO.Reader brIHDR = new IO.Reader(new MemoryAccessor(chunks["IHDR"].Data));
            brIHDR.Endianness = IO.Endianness.BigEndian;
            pic.Width = brIHDR.ReadInt32();
            pic.Height = brIHDR.ReadInt32();
            byte bitDepth = brIHDR.ReadByte();
            PNGColorType colorType = (PNGColorType)brIHDR.ReadByte();
            PNGCompressionMethod compressionMethod = (PNGCompressionMethod)brIHDR.ReadByte();
            byte filterMethod = brIHDR.ReadByte();
            byte interlaceMethod = brIHDR.ReadByte();

            byte[] imageData = chunks["IDAT"].Data;
            
            // first do a Zlib decompress
            byte[] uncompressedFilteredImageData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib).Decompress(imageData);

            // now do a PNG decompress
            PNGCompressionModule compressionModule = new PNGCompressionModule();
            compressionModule.ImageWidth = pic.Width;
            compressionModule.ImageHeight = pic.Height;
            compressionModule.Method = compressionMethod;
            compressionModule.BytesPerPixel = (int)(((double)bitDepth / 8) * 3);
            
            byte[] uncompressed = compressionModule.Decompress(uncompressedFilteredImageData);

            for (int y = 0; y < pic.Height; y++)
            {
                for (int x = 0; x < pic.Width; x++)
                {
                    int index = ((y * pic.Width) + x) * 3;

                    byte r = uncompressed[index];
                    byte g = uncompressed[index + 1];
                    byte b = uncompressed[index + 2];

                    Color color = Color.FromRGBA(r, g, b);
                    pic.SetPixel(color, x, y);
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            IO.Writer bw = base.Accessor.Writer;
            PictureObjectModel pic = (objectModel as PictureObjectModel);

            byte[] signature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            bw.WriteBytes(signature);

            bw.Flush();
        }
    }
}
