using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Moosta.ImageCollection
{
    public class IMCDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Moosta image collection archive", new string[] { "*.imc" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            List<File> files = new List<File>();
            IO.Reader br = base.Stream.Reader;
            int fileCount = br.ReadInt32();
            for (int i = 0; i < fileCount; i++)
            {
                string fileName = br.ReadString();
                files.Add(fsom.AddFile(fileName));
            }
            for (int i = 0; i < fileCount; i++)
            {
                int length = br.ReadInt32();
                int unknown1 = br.ReadInt32();
                long offset = br.BaseStream.Position;

                files[i].Properties.Add("length", length);
                files[i].Properties.Add("offset", offset);
                files[i].Properties.Add("reader", br);
                files[i].Size = length;
                files[i].DataRequest += IMCDataFormat_DataRequest;

                br.BaseStream.Seek(length, System.IO.SeekOrigin.Current);
            }
        }

        private void IMCDataFormat_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader br = (IO.Reader)file.Properties["reader"];
            int length = (int)file.Properties["length"];
            long offset = (long)file.Properties["offset"];
            br.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
            e.Data = br.ReadBytes(length);
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Writer bw = base.Stream.Writer;

            File[] files = fsom.GetAllFiles();
            bw.Write(files.Length);
            foreach (File file in files)
            {
                bw.Write(file.Name);
            }
            foreach (File file in files)
            {
                bw.Write(file.Size);
                bw.Write(file.GetDataAsByteArray());
            }
            bw.Flush();
        }
    }
}
