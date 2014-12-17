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
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FontObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FontObjectModel font = (objectModel as FontObjectModel);

            IO.Reader br = base.Accessor.Reader;
            font.GlyphHeight = br.ReadUInt16();
            font.GlyphWidth = br.ReadUInt16();

            string icnfilename = br.ReadFixedLengthString(13);
            icnfilename = icnfilename.TrimNull();
            font.GlyphCollectionFileName = icnfilename;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FontObjectModel font = (objectModel as FontObjectModel);

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteUInt16(font.GlyphHeight);
            bw.WriteUInt16(font.GlyphWidth);
            bw.WriteFixedLengthString(font.GlyphCollectionFileName, 15);
            bw.Flush();
        }
    }
}
