using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Abomination
{
    public class AWFDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Abomination: The Nemesis Project AWF archive", new string[] { "*.awf" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            Reader reader = base.Accessor.Reader;
            uint fileCount = reader.ReadUInt32();
            
            for (uint i = 0; i < fileCount; i++)
            {
                uint offset = reader.ReadUInt32();
                string fileName = reader.ReadFixedLengthString(260);

                uint length = reader.PeekUInt32();
                length -= offset;

                File file = fsom.AddFile(fileName);
                file.Size = length;
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                file.Properties.Add("reader", reader);
                file.DataRequest += file_DataRequest;
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            Reader reader = (Reader)file.Properties["reader"];

            reader.Seek(offset, SeekOrigin.Current);
            byte[] data = reader.ReadBytes(length);
            e.Data = data;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            File[] files = fsom.GetAllFiles();
            Writer writer = base.Accessor.Writer;
            writer.WriteUInt32((uint)files.Length);

            uint offset = (uint)(4 + ((4 + 260) * files.Length));
            foreach (File file in files)
            {
                writer.WriteUInt32(offset);
                writer.WriteFixedLengthString(file.Name, 260);
                offset += (uint)file.Size;
            }
            foreach (File file in files)
            {
                writer.WriteBytes(file.GetDataAsByteArray());
            }
            writer.Flush();
        }
    }
}
