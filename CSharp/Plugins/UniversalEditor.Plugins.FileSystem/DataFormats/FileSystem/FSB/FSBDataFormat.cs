using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FSB
{
    public class FSBDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("FMOD Sample Bank", new byte?[][] { new byte?[] { (byte)'F', (byte)'S', (byte)'B', (byte)'3' } }, new string[] { "*.fsb" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string header = br.ReadFixedLengthString(4);
            if (header != "FSB3") throw new InvalidDataFormatException("File does not begin with FSB3");

            uint fileCount = br.ReadUInt32();
            uint directorySize = br.ReadUInt32();
            uint dataSize = br.ReadUInt32();
            uint unknown1 = br.ReadUInt32();
            uint unknown2 = br.ReadUInt32();

            for (uint i = 0; i < fileCount; i++)
            {
                ushort entrySize = br.ReadUInt16();
                string fileName = br.ReadFixedLengthString(30);
                uint decompressedSize = br.ReadUInt32();
                uint offset = br.ReadUInt32();
                uint compressedSize = br.ReadUInt32();
                byte[] otherSoundInformation = br.ReadBytes(16);

                File file = new File();
                file.Name = fileName;
                file.Properties.Add("length", compressedSize);
                file.Properties.Add("offset", offset);
                file.Properties.Add("reader", br);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            uint length = (uint)file.Properties["length"];
            uint offset = (uint)file.Properties["offset"];
            IO.Reader br = (IO.Reader)file.Properties["reader"];

            br.Accessor.Position = offset;
            e.Data = br.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("FSB3");

            bw.WriteUInt32((uint)fsom.Files.Count);

            uint directorySize = 0;
            bw.WriteUInt32(directorySize);

            uint dataSize = 0;
            bw.WriteUInt32(dataSize);

            uint unknown1 = 0;
            bw.WriteUInt32(unknown1);

            uint unknown2 = 0;
            bw.WriteUInt32(unknown2);

            uint offset = (uint)(24 + (60 * fsom.Files.Count));

            foreach (File file in fsom.Files)
            {
                ushort entrySize = 60;
                bw.WriteUInt16(entrySize);

                bw.WriteFixedLengthString(file.Name, 30);
                bw.WriteUInt32((uint)file.Size);
                bw.WriteUInt32(offset);
                uint compressedSize = (uint)file.Size;

                byte[] otherSoundInformation = new byte[16];
                bw.WriteBytes(otherSoundInformation);

                offset += compressedSize;
            }
        }
    }
}
