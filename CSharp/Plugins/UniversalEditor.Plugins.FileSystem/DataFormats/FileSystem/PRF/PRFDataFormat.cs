using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.PRF
{
    public class PRFDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Hoyle Casino 99 PRF archive", new byte?[][] { new byte?[] { null, null, null, null, (byte)'P', (byte)'R', (byte)'F', (byte)0 } }, new string[] { "*.prf" });
            }
            return _dfr;
        }

        private uint mvarVersion = 0;
        public uint Version { get { return mvarVersion; } set { mvarVersion = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            mvarVersion = br.ReadUInt32();
            string header = br.ReadFixedLengthString(4);
            if (header != "PRF\0") throw new InvalidDataFormatException("File does not begin with \"PRF\", 0");

            uint unknown1 = br.ReadUInt32(); // 2?
            uint unknown2 = br.ReadUInt32(); // 64? 
            uint directorySize = br.ReadUInt32();
            uint directoryOffset = br.ReadUInt32(); // +24

            uint fileCount = (uint)(((directorySize - 24) / 16) - 1);

            while (!br.EndOfStream)
            {
                uint fileType = br.ReadUInt32();
                uint fileID = br.ReadUInt32();
                uint fileOffset = br.ReadUInt32();
                uint fileSize = br.ReadUInt32();

                File file = new File();
                file.Name = fileID.ToString().PadLeft(8, '0');
                file.Size = fileSize;
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
