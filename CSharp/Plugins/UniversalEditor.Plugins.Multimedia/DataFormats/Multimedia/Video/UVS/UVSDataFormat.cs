using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
    public class UVSDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Sources.Add("http://wiki.xiph.org/OggUVS");
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader br = base.Accessor.Reader;
            #region Main Header Packet
            string codecIdentifierWord1 = br.ReadFixedLengthString(4);
            string codecIdentifierWord2 = br.ReadFixedLengthString(4);
            if (codecIdentifierWord1 != "UVS " || codecIdentifierWord2 != "    ") throw new InvalidDataFormatException("File does not begin with \"UVS     \"");

            // Version Major (breaks backwards compatability to increment)
            ushort versionMajor = br.ReadUInt16();
            // Version Minor (backwards compatable, ie, more supported format id's)
            ushort versionMinor = br.ReadUInt16();

            ushort displayWidth = br.ReadUInt16();
            ushort displayHeight = br.ReadUInt16();

            ushort pixelAspectRatioNumerator = br.ReadUInt16();
            ushort pixelAspectRatioDenominator = br.ReadUInt16();
            double pixelAspectRatio = ((double)pixelAspectRatioNumerator / (double)pixelAspectRatioDenominator);
            
            ushort fieldRateNumerator = br.ReadUInt16();
            ushort fieldRateDenominator = br.ReadUInt16();
            double fieldRate = ((double)fieldRateNumerator / (double)fieldRateDenominator);

            uint timebase = br.ReadUInt32(); // in hertz
            uint fieldImageSize = br.ReadUInt32(); // in bytes
            uint extraHeaderCount= br.ReadUInt32();
            UVSColorspace colorspace = (UVSColorspace)(br.ReadUInt32());

            UVSFlags flags = (UVSFlags)(br.ReadUInt32());
            UVSLayoutID layoutID = new UVSLayoutID(br.ReadUInt32());
            #endregion
            #region Comment packet
            #endregion
            if (layoutID == UVSLayoutID.Custom)
            {

            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
