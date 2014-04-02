using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.REEVEsoft.Freeze
{
    public class ICEDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("REEVEsoft Freeze archive", new byte?[][] { new byte?[] { (byte)'F', (byte)'R' } }, new string[] { "*.ice" });
            }
            return _dfr;
        }

        private bool mvarRetainOriginalDate = true;
        public bool RetainOriginalDate { get { return mvarRetainOriginalDate; } set { mvarRetainOriginalDate = value; } }

        private bool mvarCompressSubdirectories = true;
        public bool CompressSubdirectories { get { return mvarCompressSubdirectories; } set { mvarCompressSubdirectories = value; } }

        private bool mvarIncludeVolumeName = false;
        public bool IncludeVolumeName { get { return mvarIncludeVolumeName; } set { mvarIncludeVolumeName = value; } }

        /*
        private bool mvarExpandToSubdirectory = false;
        public bool ExpandToSubdirectory { get { return mvarExpandToSubdirectory; } set { mvarExpandToSubdirectory = value; } }
        */

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string signature = br.ReadFixedLengthString(2);
            if (signature != "FR") throw new InvalidDataFormatException("File does not begin with \"FR\"");

            while (!br.EndOfStream)
            {
                short s0 = br.ReadInt16();
                short s1 = br.ReadInt16();
                short s2 = br.ReadInt16();
                int decompressedSize = br.ReadInt32();
                int compressedSize = br.ReadInt32();
                short s5 = br.ReadInt16();
                short FileNameLength = br.ReadInt16();
                string FileName = br.ReadFixedLengthString(FileNameLength);

                File file = new File();
                file.Properties.Add("offset", br.Accessor.Position);
                file.Properties.Add("length", compressedSize);
                file.Properties.Add("reader", br);
                file.Name = FileName;
                file.Size = decompressedSize;
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);

                br.Accessor.Seek(compressedSize, SeekOrigin.Current);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader br = (IO.Reader)file.Properties["reader"];
            long offset = (long)file.Properties["offset"];
            int compressedSize = (int)file.Properties["length"];

            br.Accessor.Seek(offset, SeekOrigin.Begin);
            byte[] compressedData = br.ReadBytes(compressedSize);

            // this has been identified by aluigi's comtype_scan2 as LZH compression
            byte[] decompressedData = UniversalEditor.Compression.LZH.LZHStream.Decompress(compressedData);
            e.Data = decompressedData;
        }

        /*
        private struct FileEntry
        {
            public string name;
            public int decompressedSize;
            public int compressedSize;
            public byte[] decompressedData;
        }
        */

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("FR");

            foreach (File file in fsom.Files)
            {
                short s0 = 0;
                bw.WriteInt16(s0);

                short s1 = 0;
                bw.WriteInt16(s1);

                short s2 = 0;
                bw.WriteInt16(s2);

                byte[] decompressedData = file.GetDataAsByteArray();
                byte[] compressedData = decompressedData;

                bw.WriteInt32((int)decompressedData.Length);
                bw.WriteInt32((int)compressedData.Length);

                short s5 = 0;
                bw.WriteInt16(s5);

                bw.WriteInt16((short)file.Name.Length);
                bw.WriteFixedLengthString(file.Name);

                bw.WriteBytes(compressedData);
            }
        }
    }
}
