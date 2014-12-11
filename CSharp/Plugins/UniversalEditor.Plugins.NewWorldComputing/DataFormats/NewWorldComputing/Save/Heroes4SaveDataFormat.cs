using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.NewWorldComputing.Save;

namespace UniversalEditor.DataFormats.NewWorldComputing.Save
{
    public class Heroes4SaveDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null) _dfr = base.MakeReferenceInternal();
            _dfr.Capabilities.Add(typeof(SaveObjectModel), DataFormatCapabilities.All);
            _dfr.Filters.Add("Heroes of Might and Magic IV Save Data", new string[] { "*.h4s" });
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader br = base.Accessor.Reader;
            byte[] dataCompressed = br.ReadToEnd();
            byte[] dataUncompressed = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(dataCompressed);

            br = new IO.Reader(new MemoryAccessor(dataUncompressed));

            string H4_SAVE_GAME = br.ReadFixedLengthString(12);
            if (H4_SAVE_GAME != "H4_SAVE_GAME") throw new InvalidDataFormatException();

            short unknown1 = br.ReadInt16();
            short unknown2 = br.ReadInt16();
            short unknown3 = br.ReadInt16();
            short unknown4 = br.ReadInt16();
            short unknown5 = br.ReadInt16();
            short unknown6 = br.ReadInt16();
            short unknown7 = br.ReadInt16();

            string originalFileName = br.ReadInt16String();

            byte unknown8 = br.ReadByte();
            byte unknown9 = br.ReadByte();
            byte unknown10 = br.ReadByte();

            string title = br.ReadInt16String();

            short unknown11 = br.ReadInt16();
            short unknown12 = br.ReadInt16();
            short unknown13 = br.ReadInt16();
            short unknown14 = br.ReadInt16();

            string description = br.ReadInt16String();

            byte[] unknown = br.ReadBytes(77);

            string loseCondition = br.ReadInt16String();
            string winCondition = br.ReadInt16String();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
        }
    }
}
