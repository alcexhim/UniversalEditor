using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TLCD
{
    public class TLCDDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("The Learning Company data file", new byte?[][] { new byte?[] { (byte)'T', (byte)'L', (byte)'C', (byte)'D' } }, new string[] { "*.tld" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            Reader br = base.Accessor.Reader;
            string tagTLCD = br.ReadFixedLengthString(4);
            if (tagTLCD != "TLCD") throw new InvalidDataFormatException("File does not begin with \"TLCD\"");

            uint unknown1 = br.ReadUInt32();
            uint count = br.ReadUInt32();
            uint unknown3 = br.ReadUInt32();
            uint unknown4 = br.ReadUInt32();

            List<Internal.TLCSequence> seqs = new List<Internal.TLCSequence>();

            for (uint i = 0; i < count; i++)
            {
                // filetype can be ASEQ, SSND, LIPS, OTHR
                Internal.TLCSequence seq = new Internal.TLCSequence();
                seq.filetype = br.ReadFixedLengthString(4);
                seq.offset = br.ReadUInt32();
                seq.length = br.ReadUInt32();
                seq.id = br.ReadUInt32();
                seq.unknown4 = br.ReadUInt32();
                seqs.Add(seq);
            }

            foreach (Internal.TLCSequence seq in seqs)
            {
                File file = new File();
                file.Properties.Add("fileinfo", seq);
                file.Name = seq.id.ToString() + "." + seq.filetype;
                file.Size = seq.length;
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            Internal.TLCSequence seq = (Internal.TLCSequence)file.Properties["fileinfo"];

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = seq.offset;
            e.Data = br.ReadBytes(seq.length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
