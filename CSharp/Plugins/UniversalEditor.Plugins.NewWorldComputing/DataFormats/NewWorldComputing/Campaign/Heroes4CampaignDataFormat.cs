using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.NewWorldComputing.Campaign;

namespace UniversalEditor.DataFormats.NewWorldComputing.Campaign
{
    public class Heroes4CampaignDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null) _dfr = base.MakeReferenceInternal();
            _dfr.Capabilities.Add(typeof(CampaignObjectModel), DataFormatCapabilities.All);
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
			CampaignObjectModel campaign = (objectModel as CampaignObjectModel);
			if (campaign == null) throw new ObjectModelNotSupportedException();

            Reader br = base.Accessor.Reader;
            
			string H4CAMPAIGN = br.ReadFixedLengthString(10);
			if (H4CAMPAIGN != "H4CAMPAIGN") throw new InvalidDataFormatException("File does not begin with 'H4CAMPAIGN'");

            int unknown1 = br.ReadInt32();
            byte nul = br.ReadByte();

            byte[] restOfDataCompressed = br.ReadToEnd();
            byte[] restOfDataUncompressed = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(restOfDataCompressed);

            br = new Reader(new MemoryAccessor(restOfDataUncompressed));

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

			campaign.Title = br.ReadInt16String();
            byte unknown12 = br.ReadByte();
            campaign.Description = br.ReadInt16String();
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			CampaignObjectModel campaign = (objectModel as CampaignObjectModel);
			if (campaign == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("H4CAMPAIGN");

			throw new NotImplementedException();
        }
    }
}
