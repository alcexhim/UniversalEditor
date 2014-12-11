using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Descent
{
    public class DescentHOGDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Descent HOG archive", new byte?[][] { new byte?[] { (byte)'D', (byte)'H', (byte)'F' } }, new string[] { "*.hog" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Reader br = base.Accessor.Reader;
            string DHF = br.ReadFixedLengthString(3);
            if (DHF != "DHF") throw new InvalidDataFormatException("File does not begin with \"DHF\"");

            while (!br.EndOfStream)
            {
                string FileName = br.ReadFixedLengthString(13);
                FileName = FileName.TrimNull();

                int length = br.ReadInt32();
                long offset = br.Accessor.Position;

                File file = new File();
                file.Name = FileName;
                file.Size = length;
                file.Properties.Add("reader", br);
                file.Properties.Add("offset", offset);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);

                br.Accessor.Seek(length, IO.SeekOrigin.Current);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            long offset = (long)file.Properties["offset"];
            IO.Reader reader = (IO.Reader)file.Properties["reader"];
            reader.Accessor.Seek(offset, IO.SeekOrigin.Begin);
            e.Data = reader.ReadBytes(file.Size);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("DHF");
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 13);
                bw.WriteInt32((int)file.Size);
                bw.WriteBytes(file.GetDataAsByteArray());
            }
            bw.Flush();
        }
    }
}
