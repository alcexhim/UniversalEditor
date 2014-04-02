using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HighwayPursuit
{
    public class HighwayPursuitDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Highway Pursuit archive", new byte?[][] { new byte?[] { (byte)'H', (byte)'P', (byte)'D', (byte)'T' } }, new string[] { "*.hfd", "*.hgd", "*.hmd", "*.hod", "*.hsd", "*.hvd" });
            }
            return _dfr;
        }

        private struct FILEINFO
        {
            public uint filesize;
            public uint paddingsize;

            public FILEINFO(uint fileSize, uint paddingSize)
            {
                this.filesize = fileSize;
                this.paddingsize = paddingSize;
            }
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            IO.Reader br = base.Accessor.Reader;
            string HPDT = br.ReadFixedLengthString(4);
            if (HPDT != "HPDT") throw new InvalidDataFormatException("File does not begin with HPDT");

            uint fileCount = br.ReadUInt32();

            FILEINFO[] fis = new FILEINFO[fileCount];
            for (uint i = 0; i < fileCount; i++)
            {
                uint fileSize = br.ReadUInt32();
                uint paddingSize = br.ReadUInt32();
                fis[i] = new FILEINFO(fileSize, paddingSize);
            }
            for (uint i = 0; i < fileCount; i++)
            {
                byte[] filedata = br.ReadBytes(fis[i].filesize);
                byte[] paddingdata = br.ReadBytes(fis[i].paddingsize);

                File file = new File();
                file.Name = i.ToString().PadLeft(8, '0');
                file.SetDataAsByteArray(filedata);
                file.Properties.Add("padding", paddingdata);
                fsom.Files.Add(file);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Writer bw = base.Accessor.Writer;

            bw.WriteFixedLengthString("HPDT");
            bw.WriteUInt32((uint)fsom.Files.Count);

            foreach (File file in fsom.Files)
            {
                byte[] filedata = file.GetDataAsByteArray();
                byte[] padding = new byte[0];
                if (file.Properties.ContainsKey("padding"))
                {
                    padding = (byte[])file.Properties["padding"];
                }
                bw.WriteUInt32((uint)filedata.Length);
                bw.WriteUInt32((uint)padding.Length);
            }
            foreach (File file in fsom.Files)
            {
                byte[] filedata = file.GetDataAsByteArray();
                byte[] padding = new byte[0];
                if (file.Properties.ContainsKey("padding"))
                {
                    padding = (byte[])file.Properties["padding"];
                }
                bw.WriteBytes(filedata);
                bw.WriteBytes(padding);
            }
        }
    }
}
