﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.NewWorldComputing.Campaign;

namespace UniversalEditor.DataFormats.NewWorldComputing.Campaign
{
    public class Heroes4CampaignDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null) _dfr = base.MakeReference();
            _dfr.Capabilities.Add(typeof(CampaignObjectModel), DataFormatCapabilities.All);
            _dfr.Filters.Add("Heroes of Might and Magic IV campaign", new byte?[][] { new byte?[] { (byte)'H', (byte)'4', (byte)'C', (byte)'A', (byte)'M', (byte)'P', (byte)'A', (byte)'I', (byte)'G', (byte)'N' } }, new string[] { "*.h4c" });
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.BinaryReader br = base.Stream.BinaryReader;
            string H4CAMPAIGN = br.ReadFixedLengthString(10);
            int unknown1 = br.ReadInt32();
            byte nul = br.ReadByte();

            byte[] restOfDataCompressed = br.ReadToEnd();
            byte[] restOfDataUncompressed = UniversalEditor.Compression.Gzip.GzipStream.Decompress(restOfDataCompressed);

            br = new IO.BinaryReader(restOfDataUncompressed);

            short unknown2 = br.ReadInt16();
            short unknown3 = br.ReadInt16();
            short unknown4 = br.ReadInt16();
            short unknown5 = br.ReadInt16();
            short unknown6 = br.ReadInt16();
            short unknown7 = br.ReadInt16();
            short unknown8 = br.ReadInt16();
            short unknown9 = br.ReadInt16();
            short unknown10 = br.ReadInt16();
            short unknown11 = br.ReadInt16();

            string campaignName = br.ReadInt16String();
            byte unknown12 = br.ReadByte();
            string campaignDescription = br.ReadInt16String();

        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
