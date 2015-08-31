using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture
{
	public class MicrosoftDDSDataFormat : DataFormat
	{
		private const uint DDS_MAGIC = 0x20534444;

		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("Microsoft DirectDraw Surface", new byte?[][] { new byte?[] { 0x44, 0x44, 0x53, 0x20 } }, new string[] { "*.dds" });
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.BinaryReader br = base.Stream.BinaryReader;
			
			uint dwMagic = br.ReadUInt32(); // 0x20534444
			uint dwSize = br.ReadUInt32();
			uint dwFlags = br.ReadUInt32();
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.BinaryWriter bw = base.Stream.BinaryWriter;
			bw.Write(DDS_MAGIC);

			bw.Flush();
		}
	}
}
