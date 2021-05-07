using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.WirelessBitmap
{
	public class WBMPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Wireless Application Protocol Bitmap image", new string[] { "*.wbmp" });
			}
			return _dfr;
		}

		private uint ReadCompactUInt32(IO.BinaryReader br)
		{
			uint retval = 0;
			byte value = br.ReadByte();
			bool a = ((value & 0x80) == 0x80);
			while (a)
			{
				uint val = (uint)(value & ~0x80);
				retval += val;
				value = br.ReadByte();
				a = ((value & 0x80) == 0x80);
			}

			retval += (uint)(value & ~0x80);
			return retval;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = new PictureObjectModel();
			IO.BinaryReader br = base.Stream.BinaryReader;
			br.BaseStream.Position = 0;

			uint imageType = ReadCompactUInt32(br);
			byte reserved = br.ReadByte();
			uint imageWidth = ReadCompactUInt32(br);
			uint imageHeight = ReadCompactUInt32(br);

			while (!br.EndOfStream)
			{
				byte next = br.ReadByte();

				for (int i = 0; i < 8; i++)
				{
					byte bit = next.GetBits(i, 1);
					if (bit == 0)
					{
						pic.SetPixel(System.Drawing.Color.Black);
					}
					else
					{
						pic.SetPixel(System.Drawing.Color.White);
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
