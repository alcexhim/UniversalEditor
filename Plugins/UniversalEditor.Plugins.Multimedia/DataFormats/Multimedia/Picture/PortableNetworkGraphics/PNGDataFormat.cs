//
//  PNGDataFormat.cs - provides a DataFormat for manipulating images in Portable Network Graphics (PNG) format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using MBS.Framework.Drawing;

using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.DataFormats.Chunked.ISOMediaBase;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in Portable Network Graphics (PNG) format.
	/// </summary>
	public class PNGDataFormat : ISOMediaBaseDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = new DataFormatReference(GetType());
			dfr.Title = "Portable Network Graphics (PNG) image";
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override byte[] ExpectedSignature => new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a };
		protected override ISOMediaBaseChunkLengthType ChunkLengthType => ISOMediaBaseChunkLengthType.DataOnly;
		protected override ISOMediaBaseChecksumPosition ChecksumPosition => ISOMediaBaseChecksumPosition.AfterChunkData;

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = objectModels.Pop() as ChunkedObjectModel;
			PictureObjectModel pic = objectModels.Pop() as PictureObjectModel;

			IO.Reader brIHDR = new IO.Reader(new MemoryAccessor(((RIFFDataChunk)chunked.Chunks["IHDR"]).Source.GetData()));
			brIHDR.Endianness = IO.Endianness.BigEndian;
			pic.Width = brIHDR.ReadInt32();
			pic.Height = brIHDR.ReadInt32();
			byte bitDepth = brIHDR.ReadByte();
			PNGColorType colorType = (PNGColorType)brIHDR.ReadByte();
			PNGCompressionMethod compressionMethod = (PNGCompressionMethod)brIHDR.ReadByte();
			byte filterMethod = brIHDR.ReadByte();
			byte interlaceMethod = brIHDR.ReadByte();

			byte[] imageData = ((RIFFDataChunk)chunked.Chunks["IDAT"]).Source.GetData();

			// first do a Zlib decompress
			byte[] uncompressedFilteredImageData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib).Decompress(imageData);

			// now do a PNG decompress
			PNGCompressionModule compressionModule = new PNGCompressionModule();
			compressionModule.ImageWidth = pic.Width;
			compressionModule.ImageHeight = pic.Height;
			compressionModule.Method = compressionMethod;
			compressionModule.BytesPerPixel = (int)(((double)bitDepth / 8) * 3);

			byte[] uncompressed = compressionModule.Decompress(uncompressedFilteredImageData);

			Color[] palette = null;
			if ((colorType & PNGColorType.Palette) == PNGColorType.Palette)
			{
				if (chunked.Chunks["PLTE"] == null)
				{
					throw new InvalidDataFormatException("palette color PNG does not specify 'PLTE' chunk");
				}

				byte[] paletteData = ((RIFFDataChunk)chunked.Chunks["PLTE"]).Source.GetData();
				if (paletteData.Length % 3 != 0)
				{
					throw new InvalidDataFormatException("palette chunk length not divisible by 3");
				}

				palette = ReadColors(paletteData);
			}

			for (int y = 0; y < pic.Height; y++)
			{
				for (int x = 0; x < pic.Width; x++)
				{
					if ((colorType & PNGColorType.Color) == PNGColorType.Color)
					{
						if ((colorType & PNGColorType.Palette) == PNGColorType.Palette)
						{
							// 3 - Color | Palette
							// Each pixel is a palette index; a PLTE chunk must appear.
							int index = ((y * pic.Width) + x);

							// Sample depth is always 8 bits for color type 3
							byte dat = uncompressed[index];

							Color color = palette[dat];
							pic.SetPixel(color, x, y);
						}
						else if ((colorType & PNGColorType.AlphaChannel) == PNGColorType.AlphaChannel)
						{
							// 6 - Color | Alpha Channel
							// Each pixel is a grayscale sample, followed by an alpha sample.
						}
						else
						{
							// 2 - Color
							// Each pixel is an R,G,B triple.
							int index = ((y * pic.Width) + x) * 3;

							if (uncompressed.Length - index > 2)
							{
								byte r = uncompressed[index];
								byte g = uncompressed[index + 1];
								byte b = uncompressed[index + 2];

								Color color = Color.FromRGBAByte(r, g, b);
								pic.SetPixel(color, x, y);
							}
							else
							{

							}
						}
					}
					else if ((colorType & PNGColorType.AlphaChannel) == PNGColorType.AlphaChannel)
					{
						// 4 - Alpha Channel
						// Each pixel is a grayscale sample, followed by an alpha sample.
					}
					else if (colorType == PNGColorType.Grayscale)
					{
						// 0 - Grayscale
						// Each pixel is a grayscale sample.
					}
				}
			}

			objectModels.Push(pic);
		}

		private Color[] ReadColors(byte[] paletteData)
		{
			Color[] colors = new Color[paletteData.Length / 3];
			for (int i = 0; i < paletteData.Length; i += 3)
			{
				colors[i / 3] = Color.FromRGBAByte(paletteData[i + 0], paletteData[i + 1], paletteData[i + 2]);
			}
			return colors;
		}

	}
}
