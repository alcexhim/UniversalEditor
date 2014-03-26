using System;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.JPEG
{
    /// <summary>
    /// Joint Photographic Experts Group image
    /// </summary>
	public class JPEGDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Joint Photographic Experts Group image", new string[] { "*.jpg", "*.jpe", "*.jpeg" } );
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
            IO.Reader br = base.Accessor.Reader;
            br.Endianness = IO.Endianness.BigEndian;

            while (!br.EndOfStream)
            {
				JPEGMarker marker = (JPEGMarker)br.ReadUInt16();
                switch (marker)
                {
                    case JPEGMarker.StartOfImage:
                    {
                        // SOI - Start of Image
                        break;
                    }
					case JPEGMarker.EndOfImage:
					{
						// EOI - End of Image
						
						break;
					}
					case JPEGMarker.StartOfScan:
					{
						ushort length = br.ReadUInt16(); // = 6 + (2 * numComponentsInScan)
						byte numComponentsInScan = br.ReadByte();
						for (byte i = 0; i < numComponentsInScan; i++)
						{
							// for each component, read two bytes
							byte componentID = br.ReadByte(); // 1 = Y, 2 = Cb, 3 = Cr, 4 = I, 5 = Q
							byte huffmanTable = br.ReadByte(); // bit 0-3: AC table, bit 4-7: DC table
						}
						break;
					}
                    case JPEGMarker.StartOfFrameBaseline:
                    {
                        // SOF0 - Start of Frame (Baseline DCT (discrete cosine transform))
						ushort length = br.ReadUInt16();
						byte dataPrecision = br.ReadByte();
						ushort imageHeight = br.ReadUInt16();
						ushort imageWidth = br.ReadUInt16();

						// Number of components. Usually 1 = grayscale, 3 = YcBCr or YIQ, 4 = CMYK
						byte componentCount = br.ReadByte();
						for (byte i = 0; i < componentCount; i++)
						{
							// Component type. 1 = Y, 2 = Cb, 3 = Cr, 4 = I, 5 = Q
							byte componentType = br.ReadByte();
							// bit 0-3 vertical, 4-7 horizontal
							byte samplingFactors = br.ReadByte();
							byte quantizationTableNumber = br.ReadByte();
						}
                        break;
                    }
					case JPEGMarker.DefineQuantizationTables:
					{
						// DQT - Define Quantization Tables
						ushort length = br.ReadUInt16();
						byte precisionAndTableID = br.ReadByte();

						byte[] quantizationTable = new byte[64];
						for (int i = 0; i < 64; i++)
						{
							quantizationTable[i] = br.ReadByte();
						}
						break;
					}
					case JPEGMarker.DefineHuffmanTables:
					{
						ushort length = br.ReadUInt16();
						
						// bit 0-3: number of huffman tables
						// bit 4: type of huffman table (0 = DC, 1 = AC)
						// bit 5-7: not used, must be 0
						byte htInfo = br.ReadByte();

						byte nSymbols = 0;
						byte[] nSymbols1 = br.ReadBytes(16);
						for (int i = 0; i < nSymbols1.Length; i++)
						{
							nSymbols += (byte)(nSymbols1[i]);
						}

						byte[] symbols = br.ReadBytes(nSymbols);
						break;
					}
					case JPEGMarker.Application0:
                    case JPEGMarker.Application1:
                    case JPEGMarker.Application2:
                    case JPEGMarker.Application3:
                    case JPEGMarker.Application4:
                    case JPEGMarker.Application5:
                    case JPEGMarker.Application6:
                    case JPEGMarker.Application7:
                    case JPEGMarker.Application8:
                    case JPEGMarker.Application9:
                    case JPEGMarker.ApplicationA:
                    case JPEGMarker.ApplicationB:
                    case JPEGMarker.ApplicationC:
                    case JPEGMarker.ApplicationD:
                    case JPEGMarker.ApplicationE:
                    case JPEGMarker.ApplicationF:
					{
						ushort length = br.ReadUInt16();
                        length -= 2;
						string chunkID = br.ReadNullTerminatedString();

                        length -= (ushort)(chunkID.Length + 1);
                        byte[] chunkData = br.ReadBytes(length);
                        IO.Reader appbr = new IO.Reader(new MemoryAccessor(chunkData));
                        // System.IO.File.WriteAllBytes(@"C:\Applications\MikuMikuDance\UserFile\Model\kio\Luka110320\pony01s_exifchunk.dat", chunkData);
                        switch (marker)
                        {
                            case JPEGMarker.Application0:
                            {
                                byte versionMajor = appbr.ReadByte();
                                byte versionMinor = appbr.ReadByte();

                                byte densityUnits = appbr.ReadByte();
                                // 0 - No units, aspect ratio only specified
                                // 1 - Pixels per inch
                                // 2 - Pixels per centimetre

                                ushort horizontalPixelDensity = appbr.ReadUInt16();
                                ushort verticalPixelDensity = appbr.ReadUInt16();

                                byte thumbnailWidth = appbr.ReadByte();
                                byte thumbnailHeight = appbr.ReadByte();

                                int thumbnailDataSize = (thumbnailWidth * thumbnailHeight);
                                byte[] thumbnailData = appbr.ReadBytes(thumbnailDataSize);
                                break;
                            }
                            case JPEGMarker.Application1:
                            {
                                switch (chunkID)
                                {
                                    case "Exif":
                                    {

                                        break;
                                    }
                                }
                                break;
                            }
                            case JPEGMarker.ApplicationD:
                            {
                                switch (chunkID)
                                {
                                    case "Photoshop 3.0":
                                    {

                                        break;
                                    }
                                }
                                break;
                            }
                        }
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
