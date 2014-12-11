using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.MFD
{
    public class MFDDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("187 Ride or Die MFD archive", new string[] { "*.mfd" });
                _dfr.Sources.Add("http://wiki.xentax.com/index.php?title=GRAF:187_Ride_Or_Die_MFD");
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
                uint fileID = reader.ReadUInt32();
                uint offset = reader.ReadUInt32();
                uint length = reader.ReadUInt32();
                
                File file = new File();
                file.Name = i.ToString().PadLeft(8, '0');
                file.Size = length;
                file.DataRequest += file_DataRequest;
                file.Properties.Add("reader", reader);
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            Reader reader = (Reader)file.Properties["reader"];
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];

            reader.Seek(offset, SeekOrigin.Begin);
            e.Data = reader.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();
            
            File[] files = fsom.GetAllFiles();

            Writer writer = base.Accessor.Writer;
            writer.WriteUInt32((uint)files.Length);

            uint offset = (uint)(4 + (12 * files.Length));
            foreach (File file in files)
            {
                uint fileID = (uint)Array.IndexOf(files, file);
                writer.WriteUInt32(fileID);

                uint length = (uint)file.Size;
                writer.WriteUInt32(offset);
                writer.WriteUInt32(length);

                offset += length + (length % 4);
            }
            foreach (File file in files)
            {
                writer.WriteBytes(file.GetDataAsByteArray());
                writer.Align(4);
            }
            writer.Flush();
        }
    }
}
