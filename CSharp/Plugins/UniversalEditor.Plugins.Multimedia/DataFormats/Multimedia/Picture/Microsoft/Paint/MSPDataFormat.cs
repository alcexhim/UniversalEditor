﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.Paint
{
	public class MSPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Microsoft Paint (Windows 1.0) picture", new byte?[][] { new byte?[] { (byte)'D', (byte)'a', (byte)'n', (byte)'M' }, new byte?[] { (byte)'L', (byte)'i', (byte)'n', (byte)'S' } }, new string[] { "*.msp" });
				_dfr.Sources.Add("http://www.fileformat.info/format/mspaint/egff.htm");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel picture = (objectModel as PictureObjectModel);
			if (picture == null) throw new ObjectModelNotSupportedException();

			IO.BinaryReader br = base.Stream.BinaryReader;
			if (br.BaseStream.Length < 32) throw new InvalidDataFormatException("File must be at least 32 bytes");

			string signature = br.ReadFixedLengthString(4);
			if (!(signature == "DanM" || signature == "LinS")) throw new InvalidDataFormatException("File does not begin with \"DanM\" or \"LinS\"");

			short bitmapWidth = br.ReadInt16();
			short bitmapHeight = br.ReadInt16();
			picture.Width = bitmapWidth;
			picture.Height = bitmapHeight;

			short bitmapAspectRatioX = br.ReadInt16();
			short bitmapAspectRatioY = br.ReadInt16();
			short printerAspectRatioX = br.ReadInt16();
			short printerAspectRatioY = br.ReadInt16();
			short printerWidth = br.ReadInt16();
			short printerHeight = br.ReadInt16();
			short aspectCorrectionX = br.ReadInt16();
			short aspectCorrectionY = br.ReadInt16();
			short checksum = br.ReadInt16();
			short padding0 = br.ReadInt16();
			short padding1 = br.ReadInt16();
			short padding2 = br.ReadInt16();

			if (signature == "DanM")
			{
				for (int y = 0; y < bitmapHeight; y++)
				{
					int x = 0;
					while (x < bitmapWidth)
					{
						byte next = br.ReadByte();
						bool[] exploded = ExplodeBits(next);
						for (int i = 0; i < exploded.Length; i++, x++)
						{
							Color color = Colors.Black;
							if (exploded[i]) color = Colors.White;

							picture.SetPixel(color, x, y);
						}
					}
				}
			}
			else
			{
				throw new NotImplementedException("Format LinS is not yet implemented");
			}
		}

		private bool[] ExplodeBits(byte value)
		{
			bool[] bits = new bool[8];
			bits[0] = ((value & 0x01) == 0x01);
			bits[1] = ((value & 0x02) == 0x02);
			bits[2] = ((value & 0x04) == 0x04);
			bits[3] = ((value & 0x08) == 0x08);
			bits[4] = ((value & 0x10) == 0x10);
			bits[5] = ((value & 0x20) == 0x20);
			bits[6] = ((value & 0x40) == 0x40);
			bits[7] = ((value & 0x80) == 0x80);
			return bits;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
