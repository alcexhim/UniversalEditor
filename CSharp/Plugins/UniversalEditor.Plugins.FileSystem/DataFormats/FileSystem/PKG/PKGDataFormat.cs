using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.PKG
{
    public class PKGDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null) _dfr = base.MakeReferenceInternal();
            _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
            _dfr.ExportOptions.Add(new CustomOptionText(nameof(GameName), "Game &name:"));
            return _dfr;
        }

        private string mvarGameName = String.Empty;
        public string GameName { get { return mvarGameName; } set { mvarGameName = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader br = base.Accessor.Reader;
            br.Endianness = IO.Endianness.BigEndian;
            byte magic1 = br.ReadByte();
            string magic2 = br.ReadFixedLengthString(3); // PKG
            if (magic1 != 0x7F || magic2 != "PKG") throw new InvalidDataFormatException();

            int magic3 = br.ReadInt32();            // unknown, Possible constant of 0x80000001
            int headerSize = br.ReadInt32();        // Size of header (0x00 - 0xBF)
            int unknown1 = br.ReadInt32();          // unknown
            int endBlockSize = br.ReadInt32();      // size of Unknown block at end of data starting @ 0x100
            int unknown2 = br.ReadInt32();          // unknown
            long fileSize = br.ReadInt64();         // Size of file
            long unknown3 = br.ReadInt64();
            long dataSize = br.ReadInt64();         // Size of data @ 0x100 minus 0x80 byte Unknown block. 
            
            string gameID = br.ReadFixedLengthString(48);
            mvarGameName = gameID.TrimNull();


        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
