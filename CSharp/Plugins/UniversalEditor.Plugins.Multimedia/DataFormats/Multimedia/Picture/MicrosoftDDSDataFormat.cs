using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture
{
	public class MicrosoftDDSDataFormat : DataFormat
	{
		private const uint DDS_MAGIC = 0x20534444;

		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("Microsoft DirectDraw Surface", new byte?[][] { new byte?[] { 0x44, 0x44, 0x53, 0x20 } }, new string[] { "*.dds" });
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			
			uint dwMagic = br.ReadUInt32(); // 0x20534444
			uint dwSize = br.ReadUInt32();
			uint dwFlags = br.ReadUInt32();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			bw.WriteUInt32(DDS_MAGIC);

			bw.Flush();
		}
	}
}
