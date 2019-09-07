//
//  PCXDataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.Drawing;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.ZSoft
{
	public class PCXDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private PCXHeader LoadPCXHeader(Reader reader)
		{
			PCXHeader header = new PCXHeader();
			header.Manufacturer = reader.ReadByte(); //      Constant Flag, 10 = ZSoft.pcx

			header.Version = reader.ReadByte(); //      Version information
												// 0 = Version 2.5 of PC Paintbrush
												// 2 = Version 2.8 w / palette information
												// 3 = Version 2.8 w / o palette information
												// 4 = PC Paintbrush for Windows(Plus for Windows uses Ver 5)
												// 5 = Version 3.0 and > of PC Paintbrush and PC Paintbrush +, includes Publisher's Paintbrush . Includes 24 - bit.PCX files
			header.Encoding = reader.ReadByte(); // 1 = .PCX run length encoding
			header.BitsPerPixel = reader.ReadByte(); //  1     Number of bits to represent a pixel (per Plane) - 1, 2, 4, or 8
			header.XStart = reader.ReadInt16(); //     Image Dimensions: Xmin,Ymin,Xmax,Ymax (2-bytes each)
			header.YStart = reader.ReadInt16();
			header.XEnd = reader.ReadInt16();
			header.YEnd = reader.ReadInt16();

			// *HDpi and VDpi represent the Horizontal and Vertical resolutions which the
			// image was created(either printer or scanner); i.e.an image which was
			// scanned might have 300 and 300 in each of these fields.
			header.HDpi = reader.ReadInt16(); //     Horizontal Resolution of image in DPI *
			header.VDpi = reader.ReadInt16(); // Vertical Resolution of image in DPI *


			header.Palette = reader.ReadBytes(48); //   Color palette setting, see text
			header.Reserved1 = reader.ReadByte();//    Should be set to 0.
			header.NumBitPlanes = reader.ReadByte();//     Number of color planes

			header.BytesPerLine = reader.ReadInt16(); //     Number of bytes to allocate for a scanline plane. MUST be an EVEN number. Do NOT calculate from Xmax-Xmin.
			header.PaletteInfo = reader.ReadInt16(); // How to interpret palette - 1 = Color / BW, 2 = Grayscale(ignored in PB IV / IV +)
			header.HorizontalScreenSize = reader.ReadInt16();     // Horizontal screen size in pixels.New field found only in PB IV / IV Plus
			header.VerticalScreenSize = reader.ReadInt16(); // Vertical screen size in pixels.New field found only in PB IV / IV Plus

			byte[] Filler = reader.ReadBytes(54); // Blank to fill out 128 byte header.Set all bytes to 0

			if (header.NumBitPlanes == 3)
			{
				header.BitsPerPixel = 24;
			}
			else
			{
				if (header.NumBitPlanes != 1 && header.NumBitPlanes != 4)
				{
					int nbp = (int)header.NumBitPlanes & 0xFF;
					throw new InvalidDataFormatException(String.Format("Bad number of bit planes [{0}] for PCX file", nbp));
				}
				switch (header.BitsPerPixel)
				{
					case 1:
					{
						switch (header.NumBitPlanes)
						{
							case 1:
							{
								header.BitsPerPixel = 1;
								break;
							}
							case 2:
							{
								header.BitsPerPixel = 2;
								break;
							}
							case 3:
							case 4:
							{
								header.BitsPerPixel = 4;
								break;
							}
							default:
							{
								int ibpp = (int)header.BitsPerPixel & 0xFF;
								int inum = (int)header.NumBitPlanes & 0xFF;
								throw new InvalidDataFormatException(String.Format("Bad BPP [{0}/{1}] for PCX file", ibpp, inum));
							}
						};
						break;
					}
					case 2:
					{
						header.BitsPerPixel = 2;
						break;
					}
					case 4:
					{
						header.BitsPerPixel = 4;
						break;
					}
					case 8:
					{
						header.BitsPerPixel = 8;
						break;
					}
					default:
					{
						int ibpp = (int)header.BitsPerPixel & 0xFF;
						throw new InvalidDataFormatException(String.Format("Bad BPP [{0}] for PCX file", ibpp));
					}
				}
			}
			return header;
		}

		private enum PaletteType
		{
			NoPalette,
			BuiltInPalette,
			FooterPalette
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = base.Accessor.Reader;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			PCXHeader header = LoadPCXHeader(reader);

			byte[] row = null;
			int bytesPerLine = header.NumBitPlanes * header.BytesPerLine;
			int width = 1 + header.XEnd - header.XStart;
			int height = 1 + header.YEnd - header.YStart;
			PaletteType palType = PaletteType.NoPalette;
			switch (header.BitsPerPixel)
			{
				case 1:
				case 2:
				case 4:
				{
					palType = PaletteType.BuiltInPalette;
					header.BitsPerPixel = 8;
					row = new byte[header.Width];
					break;
				}
				case 8:
				{
					palType = PaletteType.FooterPalette;
					header.BitsPerPixel = 8;
					break;
				}
				default:
				{
					palType = PaletteType.NoPalette;
					header.BitsPerPixel = 24;
					break;
				}
			}

			PaletteObjectModel palette = new PaletteObjectModel();
			if (palType == PaletteType.BuiltInPalette)
			{
				reader.Seek(16, SeekOrigin.Begin); // (stream_start + (bsw::file_size_t)16);
				LoadPCXPalette(reader, palette, true);
			}
			else
			{
				if (palType == PaletteType.FooterPalette)
				{
					if (reader.Accessor.Length < 769 + 128)
					{
						throw new InvalidDataFormatException(String.Format("Bad  size [{0}] of the PCX file", reader.Accessor.Length));
					}
					reader.Seek(-769, SeekOrigin.End);
					byte pcx_pal_magic = reader.ReadByte();
					if (pcx_pal_magic != 12)
					{
						throw new InvalidDataFormatException("Unexpected start palette code in the PCX file");
					}
					LoadPCXPalette(reader, palette, false);
				}
			}

			reader.Seek(128, SeekOrigin.Begin);

			pic.Width = header.Width;
			pic.Height = header.Height;

			if (palette != null)
			{
			   // pic.Palette = palette;
			}
			byte[] scan_line = new byte[bytesPerLine];
			for (int y = 0; y < header.Height; y++)
			{
				LoadPCXScanLine(reader, scan_line);
				if (header.BitsPerPixel == 24)
				{
				    put_pixels_24 (pic, scan_line, y, header);               
				}
			   else
			   {
			       if (header.BitsPerPixel == 8)
			       {
			           put_pixels_8 (pic, scan_line, y, header, palette); 
			       }
			       else
			       {
			           put_pixels_by_bits (pic, palette, scan_line, y, header, row);
			       }

			   }
			}
		}
		private void LoadPCXScanLine(Reader reader, byte[] line)
		{
			int x = 0;
			while (x < line.Length)
			{
				byte run_len = 1;
				byte code = reader.ReadByte();
				byte val;
				if (0xC0 == (0xC0 & code))
				{
					run_len = (byte)(0x3F & code);
					val = reader.ReadByte();
				}
				else
				{
					val = code;
				}

				for (int i = 0; i < run_len; i++, x++)
				{
					if (x < line.Length)
					{
						line[x] = val;
					}
					else
					{
						// end of line
						break;
					}
				}
			}
		}

		private void put_pixels_24(PictureObjectModel img, byte[] scan_line, int y, PCXHeader pcx_info)
		{
			for (int x = 0; x < pcx_info.Width; x++)
			{
				img.SetPixel(Color.FromRGBAByte(scan_line[0 * pcx_info.Width + x], scan_line[1 * pcx_info.Width + x], scan_line[2 * pcx_info.Width + x]), x, y);
			}
		}
		private void put_pixels_8(PictureObjectModel img, byte[] scan_line, int y, PCXHeader pcx_info, PaletteObjectModel palette)
		{
			for (int x = 0; x < scan_line.Length /* pcx_info.Width */; x++)
			{
				img.SetPixel(palette.Entries[scan_line[x] % (palette.Entries.Count)].Color, x, y);
			}
		}
		private void put_pixels_by_bits(PictureObjectModel img, PaletteObjectModel palette, byte[] scan_line, int y, PCXHeader pcx_info, byte[] row)
		{
			int bytes_in_line = pcx_info.BytesPerLine / pcx_info.NumBitPlanes;
			uint k = 0;
			for (uint plane = 0; plane < pcx_info.NumBitPlanes; plane++)
			{
				uint x = 0;
				for (int i = 0; i < bytes_in_line; i++)
				{
					byte _byte = scan_line[k++];
					for(int j = 7; j >= 0; j--) 
					{
						byte bit = (byte)((_byte >> j) & 1);
						/* skip padding bits */
						if (i* 8 + j >= pcx_info.Width)
						{
							continue;
						}
						int z = ((int)bit << (int)plane);
						row[x++] |= (byte)z;
					}
				}
			}
			for (int x = 0; x < pcx_info.Width; x++)
			{
				img.SetPixel(palette.Entries[row[x]].Color, x, y);
				row[x] = 0;
			}
		}

		private void LoadPCXPalette(Reader reader, PaletteObjectModel palette, bool is16colors)
		{
			uint to_read = (uint)(is16colors ? 16 : 256);
			for (uint i = 0; i < to_read; i++)
			{
				byte r = reader.ReadByte();
				byte g = reader.ReadByte();
				byte b = reader.ReadByte();

				palette.Entries.Add(Color.FromRGBAInt32((int)r & 0xFF, (int)g & 0xFF, (int)b & 0xFF));
			}
		}




		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
