using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Moosta.ImageCollection
{
    public class IMCDataFormat : DataFormat
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

            List<File> files = new List<File>();
            IO.Reader br = base.Accessor.Reader;
            int fileCount = br.ReadInt32();
            for (int i = 0; i < fileCount; i++)
            {
                string fileName = br.ReadLengthPrefixedString();
                files.Add(fsom.AddFile(fileName));
            }
            for (int i = 0; i < fileCount; i++)
            {
                int length = br.ReadInt32();
                int unknown1 = br.ReadInt32();
                long offset = br.Accessor.Position;

                files[i].Properties.Add("length", length);
                files[i].Properties.Add("offset", offset);
                files[i].Properties.Add("reader", br);
                files[i].Size = length;
                files[i].DataRequest += IMCDataFormat_DataRequest;

                br.Accessor.Seek(length, SeekOrigin.Current);
            }
        }

        private void IMCDataFormat_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader br = (IO.Reader)file.Properties["reader"];
            int length = (int)file.Properties["length"];
            long offset = (long)file.Properties["offset"];
            br.Accessor.Seek(offset, SeekOrigin.Begin);
            e.Data = br.ReadBytes(length);
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Writer bw = base.Accessor.Writer;

            File[] files = fsom.GetAllFiles();
            bw.WriteInt32(files.Length);
            foreach (File file in files)
            {
                bw.Write(file.Name);
            }
            foreach (File file in files)
            {
                bw.WriteInt64(file.Size);
                bw.WriteBytes(file.GetDataAsByteArray());
            }
            bw.Flush();
        }
    }
}
