using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.NewWorldComputing.Font;

namespace UniversalEditor.DataFormats.NewWorldComputing.FNT
{
    public class FNTDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FontObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic font", new string[] { "*.fnt" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FontObjectModel font = (objectModel as FontObjectModel);

            IO.BinaryReader br = base.Stream.BinaryReader;
            font.GlyphHeight = br.ReadUInt16();
            font.GlyphWidth = br.ReadUInt16();

            string icnfilename = br.ReadFixedLengthString(13);
            icnfilename = icnfilename.TrimNull();
            font.GlyphCollectionFileName = icnfilename;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FontObjectModel font = (objectModel as FontObjectModel);

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            bw.Write(font.GlyphHeight);
            bw.Write(font.GlyphWidth);
            bw.WriteFixedLengthString(font.GlyphCollectionFileName, 15);
            bw.Flush();
        }
    }
}
