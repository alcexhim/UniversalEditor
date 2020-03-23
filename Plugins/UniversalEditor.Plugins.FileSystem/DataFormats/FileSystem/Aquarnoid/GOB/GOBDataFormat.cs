using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Aquarnoid.GOB
{
    public class GOBDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }


        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Reader reader = base.Accessor.Reader;
            string GOB_ = reader.ReadFixedLengthString(4);
            if (GOB_ != "GOB\0") throw new InvalidDataFormatException("File does not begin with \"GOB\", 0");

            uint fileCount = reader.ReadUInt32();
            for (uint i = 0; i < fileCount; i++)
            {
                uint offset = reader.ReadUInt32();
                uint length = reader.ReadUInt32();
                string fileName = reader.ReadFixedLengthString(260).TrimNull();

                File file = fsom.AddFile(fileName);
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                file.Properties.Add("reader", reader);
                file.Size = length;
                file.DataRequest += file_DataRequest;
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            IO.Reader reader = (IO.Reader)file.Properties["reader"];

            reader.Seek(offset, IO.SeekOrigin.Begin);
            e.Data = reader.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Writer writer = base.Accessor.Writer;
            writer.WriteFixedLengthString("GOB\0");

            File[] files = fsom.GetAllFiles();
            writer.WriteUInt32((uint)files.Length);
            
            uint offset = (uint)(8 + (268 * files.Length));
            foreach (File file in files)
            {
                writer.WriteUInt32(offset);
                writer.WriteUInt32((uint)(file.Size));
                writer.WriteFixedLengthString(file.Name, 260);
                offset += (uint)file.Size;
            }
            foreach (File file in files)
            {
                writer.WriteBytes(file.GetData());
            }
            writer.Flush();
        }
    }
}
