using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin.Mfc
{
	public class MipMap
	{
		public Size ImageDimensionsMinusOne { get; set; }
		public Size ImageDimensions { get; set; }
		public uint Level { get; set; }
		public byte[] ImageData { get; set; }

		public List<List<PixelSpan>> PixelSpans { get; set; }
	}
	public struct PixelSpan
	{
		public ushort StartIndex;
		public ushort EndIndex;

		public PixelSpan(ushort startIndex, ushort endIndex)
		{
			StartIndex = startIndex;
			EndIndex = endIndex;
		}
	};

	[MfcSerialisable("CMerlinTexture")]
	internal class CMerlinTexture : CMerlinObject
	{
		public bool HasTransparency { get; private set; }
		public List<MipMap> Mipmaps { get; private set; }

		public override void LoadObject(IO.Reader reader)
		{
			base.LoadObject(reader);

			var flags = reader.ReadUInt16();
			this.HasTransparency = (flags & 1) != 0;
			if ((flags & ~1) != 0)
			{
				throw new NotImplementedException("Unexpected flag set in texture header");
			}

			ushort mipmapCount = reader.ReadUInt16();
			this.Mipmaps = new List<MipMap>(mipmapCount);
			for (int i = 0; i < mipmapCount; i++)
			{
				MipMap mipmap = new MipMap();
				var nextLargestHeight = reader.ReadUInt16();
				var imageHeight = reader.ReadUInt16();
				var nextLargestWidth = reader.ReadUInt16();
				var imageWidth = reader.ReadUInt16();

				mipmap.ImageDimensionsMinusOne = new Size(imageWidth, imageHeight);
				mipmap.ImageDimensions = new Size(nextLargestWidth, nextLargestHeight);
				mipmap.Level = reader.ReadUInt16();

				var imageDataLength = reader.ReadUInt32();
				mipmap.ImageData = reader.ReadBytes((int)imageDataLength);

				int totalSpanCount = (int)reader.ReadUInt32();
				mipmap.PixelSpans = new List<List<PixelSpan>>(totalSpanCount);
				for (int y = 0; y < nextLargestHeight; y++)
				{
					ushort rowSpanCount = reader.ReadUInt16();
					var rowSpans = new List<PixelSpan>(rowSpanCount);
					for (int k = 0; k < rowSpanCount; k++)
					{
						ushort startIndex = reader.ReadUInt16();
						ushort endIndex = reader.ReadUInt16();
						rowSpans.Add(new PixelSpan(startIndex, endIndex));
					}
					mipmap.PixelSpans.Add(rowSpans);
				}

				reader.ReadMfcObjectWithoutHeader<TrailingBytes>();

				this.Mipmaps.Add(mipmap);
			}
		}

		public override void SaveObject(IO.Writer reader)
		{
			throw new NotImplementedException();
		}
	}
}
