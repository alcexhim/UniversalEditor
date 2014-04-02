using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ElectronicArts
{
    public class BIGFDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Electronic Arts BIGF archive", new byte?[][] { new byte?[] { (byte)'B', (byte)'I', (byte)'G', (byte)'F' } }, new string[] { "*.abg", "*.ama", "*.big", "*.dua", "*.fra", "*.gea", "*.poa", "*.spa", "*.swa", "*.uka", "*.hog", "*.viv" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string header = br.ReadFixedLengthString(4);
            if (header != "BIGF") throw new InvalidDataFormatException("File does not start with BIGF");

            uint archiveSize = br.ReadUInt32();
            uint fileCount = br.ReadUInt32();
            uint firstFileOffset = br.ReadUInt32();

            // TODO: figure out what firstFileOffset points to... the data or the entry
            // br.Accessor.Seek(firstFileOffset, SeekOrigin.Begin);
            for (uint i = 0; i < fileCount; i++)
            {
                uint offset = br.ReadUInt32();
                uint length = br.ReadUInt32();
                string filename = br.ReadNullTerminatedString();

                File file = new File();
                file.Name = filename;
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                file.Properties.Add("reader", br);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            IO.Reader br = (IO.Reader)file.Properties["reader"];

            br.Accessor.Position = offset;
            e.Data = br.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("BIGF");

            uint archiveSize = 0;
            long archiveSizePos = bw.Accessor.Position;
            bw.WriteUInt32(archiveSize);

            bw.WriteUInt32((uint)fsom.Files.Count);
            
            uint offset = 16;
            foreach (File file in fsom.Files)
            {
                offset += (uint)(8 + file.Name.Length + 1);
            }
            bw.WriteUInt32(offset);

            foreach (File file in fsom.Files)
            {
                bw.WriteUInt32(offset);
                bw.WriteUInt32((uint)file.Size);
                bw.WriteNullTerminatedString(file.Name);
            }
            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());
            }
        }
    }
}
