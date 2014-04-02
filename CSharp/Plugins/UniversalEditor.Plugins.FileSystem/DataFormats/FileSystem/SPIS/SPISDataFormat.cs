using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SPIS
{
    public class SPISDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("SPIS archive", new byte?[][] { new byte?[] { (byte)'S', (byte)'P', (byte)'I', (byte)'S' } }, new string[] { "*.dsk" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            while (!br.EndOfStream)
            {
                string header1 = br.ReadFixedLengthString(4);
                if (header1 != "SPIS") throw new InvalidDataFormatException("File does not begin with SPIS");

                byte header2 = br.ReadByte();
                if (header2 != 26) throw new InvalidDataFormatException("File does not begin with SPIS, 26");

                string header3 = br.ReadFixedLengthString(3);
                if (header3 != "LZH") throw new InvalidDataFormatException("File does not begin with SPIS, 26, LZH");

                uint decompressedSize = br.ReadUInt32();
                uint unknown1 = br.ReadUInt32();
                byte[] unknown2 = br.ReadBytes(5);
                ushort fileNameLength = br.ReadUInt16();
                ushort unknown3 = br.ReadUInt16();
                uint unknown4 = br.ReadUInt32();
                uint decompressedSize1 = br.ReadUInt32();
                uint compressedSize = br.ReadUInt32();
                byte unknown5 = br.ReadByte(); // 2 
                uint unknown6 = br.ReadUInt32();
                uint unknown7 = br.ReadUInt32(); // 0
                string filename = br.ReadNullTerminatedString();
                byte[] compressedData = br.ReadBytes(compressedSize);

                byte[] decompressedData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.LZH).Decompress(compressedData);

                fsom.Files.Add(filename, decompressedData);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString("SPIS");
                bw.WriteByte((byte)26);
                bw.WriteFixedLengthString("LZH");

                uint decompressedSize = (uint)file.Size;
                bw.WriteUInt32(decompressedSize);

                uint unknown1 = 0;
                bw.WriteUInt32(unknown1);

                byte[] unknown2 = new byte[5];
                bw.WriteBytes(unknown2);

                ushort fileNameLength = (ushort)file.Name.Length;
                bw.WriteUInt16(fileNameLength);

                ushort unknown3 = 0;
                bw.WriteUInt16(unknown3);

                uint unknown4 = 0;
                bw.WriteUInt32(unknown4);

                bw.WriteUInt32(decompressedSize);

                byte[] decompressedData = file.GetDataAsByteArray();
                byte[] compressedData = decompressedData;

                bw.WriteUInt32((uint)compressedData.Length);

                byte unknown5 = 2; // 2 
                bw.WriteByte(unknown5);

                uint unknown6 = 0;
                bw.WriteUInt32(unknown6);

                uint unknown7 = 0;
                bw.WriteUInt32(unknown7);

                bw.WriteNullTerminatedString(file.Name);
                bw.WriteBytes(compressedData);
            }
        }
    }
}
