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

using MBS.Framework.Drawing;

using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in Portable Network Graphics (PNG) format.
	/// </summary>
	public class PNGDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			byte[] signature = br.ReadBytes(8);
			br.Endianness = IO.Endianness.BigEndian;

			PNGChunk.PNGChunkCollection chunks = new PNGChunk.PNGChunkCollection();

			while (!br.EndOfStream)
			{
				int chunkLength = br.ReadInt32();
				string chunkType = br.ReadFixedLengthString(4);
				byte[] chunkData = br.ReadBytes(chunkLength);
				int chunkCRC = br.ReadInt32();
				chunks.Add(chunkType, chunkData);

				if (chunkType == "IEND") break;
			}

			IO.Reader brIHDR = new IO.Reader(new MemoryAccessor(chunks["IHDR"].Data));
			brIHDR.Endianness = IO.Endianness.BigEndian;
			pic.Width = brIHDR.ReadInt32();
			pic.Height = brIHDR.ReadInt32();
			byte bitDepth = brIHDR.ReadByte();
			PNGColorType colorType = (PNGColorType)brIHDR.ReadByte();
			PNGCompressionMethod compressionMethod = (PNGCompressionMethod)brIHDR.ReadByte();
			byte filterMethod = brIHDR.ReadByte();
			byte interlaceMethod = brIHDR.ReadByte();

			byte[] imageData = chunks["IDAT"].Data;

			// first do a Zlib decompress
			byte[] uncompressedFilteredImageData = CompressionModule.FromKnownCompressionMethod(CompressionMethod.Zlib).Decompress(imageData);

			// now do a PNG decompress
			PNGCompressionModule compressionModule = new PNGCompressionModule();
			compressionModule.ImageWidth = pic.Width;
			compressionModule.ImageHeight = pic.Height;
			compressionModule.Method = compressionMethod;
			compressionModule.BytesPerPixel = (int)(((double)bitDepth / 8) * 3);

			byte[] uncompressed = compressionModule.Decompress(uncompressedFilteredImageData);

			for (int y = 0; y < pic.Height; y++)
			{
				for (int x = 0; x < pic.Width; x++)
				{
					int index = ((y * pic.Width) + x) * 3;

					if (uncompressed.Length - index > 2)
					{
						byte r = uncompressed[index];
						byte g = uncompressed[index + 1];
						byte b = uncompressed[index + 2];

						Color color = Color.FromRGBAByte(r, g, b);
						pic.SetPixel(color, x, y);
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			byte[] signature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
			bw.WriteBytes(signature);

			bw.Flush();
		}
	}
}
