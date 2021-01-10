//
//  GIMDataFormat.cs - provides a DataFormat for manipulating images in PlayStation Portable GIM format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com> 2013-05-19
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.IO;
using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GIM
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in PlayStation Portable GIM format.
	/// </summary>
	public class GIMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("http://pspdum.my.land.to/psp/gim.html");

				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(Endianness), "_Endianness", true,
					new CustomOptionFieldChoice("Big-Endian", IO.Endianness.BigEndian),
					new CustomOptionFieldChoice("Little-Endian", IO.Endianness.LittleEndian, true)
				));

				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(ImageFormat), "_Image format", true,
					new CustomOptionFieldChoice("Indexed (4-bit)", GIMImageFormat.Index4),
					new CustomOptionFieldChoice("Indexed (8-bit)", GIMImageFormat.Index8),
					new CustomOptionFieldChoice("Indexed (16-bit)", GIMImageFormat.Index16),
					new CustomOptionFieldChoice("Indexed (32-bit)", GIMImageFormat.Index32),
					new CustomOptionFieldChoice("Bitmap (R4-G4-B4-A4)", GIMImageFormat.RGBA4444),
					new CustomOptionFieldChoice("Bitmap (R5-G5-B5-A1)", GIMImageFormat.RGBA5551),
					new CustomOptionFieldChoice("Bitmap (R5-G6-B5-A0)", GIMImageFormat.RGBA5650),
					new CustomOptionFieldChoice("Bitmap (R8-G8-B8-A8)", GIMImageFormat.RGBA8888, true)
				));
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(PaletteFormat), "_Palette format", true,
					new CustomOptionFieldChoice("Bitmap (R4-G4-B4-A4)", GIMPaletteFormat.RGBA4444),
					new CustomOptionFieldChoice("Bitmap (R5-G5-B5-A1)", GIMPaletteFormat.RGBA5551),
					new CustomOptionFieldChoice("Bitmap (R5-G6-B5-A0)", GIMPaletteFormat.RGBA5650),
					new CustomOptionFieldChoice("Bitmap (R8-G8-B8-A8)", GIMPaletteFormat.RGBA8888, true)
				));
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(PixelOrder), "Pixel _order", true,
					new CustomOptionFieldChoice("Normal", GIMPixelOrder.Normal, true),
					new CustomOptionFieldChoice("Faster", GIMPixelOrder.Faster)
				));

				_dfr.ExportOptions.Add(new CustomOptionText(nameof(OriginalFileName), "Original _filename"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(CreationUserName), "Creation _user"));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(CreationApplication), "_Application name", "Universal Editor"));
			}
			return _dfr;
		}

		public Endianness Endianness { get; set; } = Endianness.LittleEndian;
		public GIMImageFormat ImageFormat { get; set; } = GIMImageFormat.RGBA8888;
		public GIMPaletteFormat PaletteFormat { get; set; } = GIMPaletteFormat.RGBA8888;
		public GIMPixelOrder PixelOrder { get; set; } = GIMPixelOrder.Normal;
		public string OriginalFileName { get; set; } = String.Empty;
		public string CreationUserName { get; set; } = String.Empty;
		public string CreationApplication { get; set; } = String.Empty;

		public PaletteObjectModel Palette { get; } = new PaletteObjectModel();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;

			string signature = br.ReadFixedLengthString(4);
			if (signature == ".GIM")
			{
				br.Endianness = Endianness.BigEndian;
			}
			else if (signature == "MIG.")
			{

			}
			else
			{
				throw new InvalidDataFormatException("File does not begin with \"MIG.\" or \".GIM\"");
			}

			string version = br.ReadFixedLengthString(4);
			string format = br.ReadFixedLengthString(4);
			uint nul = br.ReadUInt32();

			if (!(format.EndsWith("\0") && nul == 0))
			{
				// back up?
				br.Accessor.Position += 42; // (45) -  4 bytes for UInt32 and 1 byte for the end of Format
			}

			List<int> pixelColorIndices = new List<int>();

			while (!br.EndOfStream)
			{
				GIMPartType partType = (GIMPartType)br.ReadInt16();
				short partUnknown1 = br.ReadInt16();
				uint address = br.ReadUInt32();
				uint partSize = br.ReadUInt32();
				uint partUnknown2 = br.ReadUInt32(); // 16?

				uint realPartSize = partSize - 16;
				long pos = br.Accessor.Position;

				switch (partType)
				{
					case GIMPartType.EndOfFileAddress:
					{
						// address field contains the end-of-file address
						break;
					}
					case GIMPartType.FileInfoAddress:
					{
						// address field contains the file info address
						break;
					}
					case GIMPartType.ImageData:
					{
						ushort dataOffset = br.ReadUInt16();
						ushort unknown1 = br.ReadUInt16();

						ImageFormat = (GIMImageFormat)br.ReadUInt16();
						PixelOrder = (GIMPixelOrder)br.ReadUInt16();

						ushort visibleWidth = br.ReadUInt16();
						ushort visibleHeight = br.ReadUInt16();
						pic.Width = visibleWidth;
						pic.Height = visibleHeight;

						ushort colorDepth = br.ReadUInt16();

						ushort unknown2 = br.ReadUInt16();
						ushort unknown3 = br.ReadUInt16();
						ushort unknown4 = br.ReadUInt16();
						uint unknown5 = br.ReadUInt32();
						uint unknown6 = br.ReadUInt32();
						uint unknown7 = br.ReadUInt32();
						uint partSizeMinus16 = br.ReadUInt32();
						uint unknown8 = br.ReadUInt32();
						ushort unknown9 = br.ReadUInt16(); // 1
						ushort unknown10 = br.ReadUInt16(); // 1
						ushort unknown11 = br.ReadUInt16(); // 3
						ushort unknown12 = br.ReadUInt16(); // 1
						uint unknown13 = br.ReadUInt16(); // 0x40
						byte[] unknown14 = br.ReadBytes(12);

						switch (PixelOrder)
						{
							case GIMPixelOrder.Normal:
							{
								byte nextByte = 0;
								bool useNextByte = false;

								for (ushort x = 0; x < visibleWidth; x++)
								{
									for (ushort y = 0; y < visibleHeight; y++)
									{
										switch (ImageFormat)
										{
											case GIMImageFormat.RGBA5650:
											{
												Color color = br.ReadColorRGBA5650();
												pic.SetPixel(color, x, y);
												break;
											}
											case GIMImageFormat.RGBA5551:
											{
												Color color = br.ReadColorRGBA5551();
												pic.SetPixel(color, x, y);
												break;
											}
											case GIMImageFormat.RGBA4444:
											{
												Color color = br.ReadColorRGBA4444();
												pic.SetPixel(color, x, y);
												break;
											}
											case GIMImageFormat.RGBA8888:
											{
												Color color = br.ReadColorRGBA8888();
												pic.SetPixel(color, x, y);
												break;
											}
											case GIMImageFormat.Index4:
											{
												int paletteIndex = 0;
												if (useNextByte)
												{
													paletteIndex = nextByte.GetBits(4, 4);
													useNextByte = false;
												}
												else
												{
													nextByte = br.ReadByte();
													useNextByte = true;
													paletteIndex = nextByte.GetBits(0, 4);
												}

												Color color = Palette.Entries[paletteIndex].Color;
												pic.SetPixel(color, x, y);
												break;
											}
											case GIMImageFormat.Index8:
											{
												int paletteIndex = (int)br.ReadByte();
												pixelColorIndices.Add(paletteIndex);
												break;
											}
											case GIMImageFormat.Index16:
											{
												int paletteIndex = (int)br.ReadInt16();
												pixelColorIndices.Add(paletteIndex);
												break;
											}
											case GIMImageFormat.Index32:
											{
												int paletteIndex = br.ReadInt32();
												pixelColorIndices.Add(paletteIndex);
												break;
											}
										}
									}
								}
								break;
							}
						}
						break;
					}
					case GIMPartType.PaletteData:
					{
						ushort dataOffset = br.ReadUInt16();
						ushort nul1 = br.ReadUInt16();
						PaletteFormat = (GIMPaletteFormat)br.ReadUInt16();
						ushort nul2 = br.ReadUInt16();
						ushort paletteColorCount = br.ReadUInt16();

						byte[] unknown3 = br.ReadBytes(16);
						uint unknown6 = br.ReadUInt32();
						uint unknown7 = br.ReadUInt32();

						uint partSizeMinus16 = br.ReadUInt32();
						uint unknown8 = br.ReadUInt32();
						ushort unknown9 = br.ReadUInt16();
						ushort unknown10 = br.ReadUInt16();
						ushort unknown11 = br.ReadUInt16();
						ushort unknown12 = br.ReadUInt16();

						uint unknown13 = br.ReadUInt32();

						// documentation says read 12 bytes here, but that extra 2 bytes is what was messing
						// us up, so we really should only read 10 bytes here!!!
						byte[] unknown14 = br.ReadBytes(10);

						// palette data(16色なら16x4=64B、256色なら256x4=1024B)
						// GIM用のパレットのデータはR,G,B,Aの4バイト×色数と並んでいる。
						// BMP用のパレットのデータはB,G,R,Aの順なのでR,Bの値の入れ替えが必要。

						// palette data (if 256 color 256x4 = 1024B, if 16 color 16x4 = 64B)
						// Data Palette for GIM is aligned with 4 bytes × number of colors R, G, B, and A.
						// Require replacement of the values ​​of R, and B data of the palette of BMP for so order B, G, R, of A.
						for (ushort i = 0; i < paletteColorCount; i++)
						{
							switch (PaletteFormat)
							{
								case GIMPaletteFormat.RGBA5650:
								{
									Color color = br.ReadColorRGBA5650();
									Palette.Entries.Add(color);
									break;
								}
								case GIMPaletteFormat.RGBA5551:
								{
									// 5 bits R, 5 bits G, 5 bits B, 1 bits alpha
									Color color = br.ReadColorRGBA5551();
									Palette.Entries.Add(color);
									break;
								}
								case GIMPaletteFormat.RGBA4444:
								{
									Color color = br.ReadColorRGBA4444();
									Palette.Entries.Add(color);
									break;
								}
								case GIMPaletteFormat.RGBA8888:
								{
									Color color = br.ReadColorRGBA8888();
									Palette.Entries.Add(color);
									break;
								}
							}
						}
						break;
					}
					case GIMPartType.FileInfoData:
					{
						OriginalFileName = br.ReadNullTerminatedString();
						CreationUserName = br.ReadNullTerminatedString();
						string timestamp = br.ReadNullTerminatedString();
						CreationApplication = br.ReadNullTerminatedString();
						break;
					}
				}

				long bytesReadSoFar = br.Accessor.Position - pos;
				long bytesRemaining = realPartSize - bytesReadSoFar;
				br.Accessor.Seek(bytesRemaining, SeekOrigin.Current);
			}

			foreach (int index in pixelColorIndices)
			{
				Color color = Palette.Entries[index].Color;
				pic.SetPixel(color);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer bw = base.Accessor.Writer;
			bw.Endianness = Endianness;
			switch (Endianness)
			{
				case Endianness.BigEndian:
				{
					bw.WriteFixedLengthString(".GIM1.00\0PSP");
					break;
				}
				case Endianness.LittleEndian:
				{
					bw.WriteFixedLengthString("MIG.00.1PSP\0");
					break;
				}
			}
		}
	}
}
