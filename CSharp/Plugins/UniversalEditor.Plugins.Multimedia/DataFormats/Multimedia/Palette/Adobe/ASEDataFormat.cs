using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.Adobe
{
	public class ASEDataFormat : DataFormat
	{
		// http://www.selapa.net/swatches/colors/fileformats.php#adobe_acb

		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Adobe Swatch Exchange color palette", new byte?[][] { new byte?[] { (byte)'A', (byte)'S', (byte)'E', (byte)'F' } }, new string[] { "*.ase" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);

			IO.Reader br = base.Accessor.Reader;
			br.Endianness = IO.Endianness.BigEndian;
			br.Accessor.DefaultEncoding = Encoding.UTF8;

			string ASEF = br.ReadFixedLengthString(4);
			if (ASEF != "ASEF") throw new InvalidDataFormatException("File does not begin with \"ASEF\"");

			short versionMajor = br.ReadInt16();
			short versionMinor = br.ReadInt16();

			int blockCount = br.ReadInt32();
			for (int i = 0; i < blockCount; i++)
			{
				ushort blockType = br.ReadUInt16();
				int blockLength = br.ReadInt32();
				ushort blockNameLength = br.ReadUInt16();

				string blockName = br.ReadFixedLengthString(blockNameLength).TrimNull();
				
				switch (blockType)
				{
					case 0xC001:
					{
						// Group start
						break;
					}
					case 0xC002:
					{
						// Group end
						break;
					}
					case 0x0001:
					{
						// Color entry
						string colorModel = br.ReadFixedLengthString(4);
						switch (colorModel)
						{
							case "CMYK":
							{
								break;
							}
							case "LAB ":
							{
								float r = br.ReadSingle();
								float g = br.ReadSingle();
								float b = br.ReadSingle();
								break;
							}
							case "RGB ":
							{
								float r = br.ReadSingle();
								float g = br.ReadSingle();
								float b = br.ReadSingle();

								byte _r = (byte)(Math.Round(r * 255));
								byte _g = (byte)(Math.Round(g * 255));
								byte _b = (byte)(Math.Round(b * 255));

								Color color = Color.FromRGBA(_r, _g, _b);
								palette.Entries.Add(color);
								break;
							}
							case "Gray":
							{
								break;
							}
						}

						ushort colorType = br.ReadUInt16();
						// 0 = Global, 1 = Spot, 2 = Normal
						break;
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
