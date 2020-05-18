//
//  DirectDrawSurfaceDataFormat.cs - provides a DataFormat for manipulating images in DirectDraw Surface (DDS) format
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

using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.DirectDraw
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in DirectDraw Surface (DDS) format.
	/// </summary>
	public class DirectDrawSurfaceDataFormat : DataFormat
	{
		public const uint DDS_MAGIC = 0x20534444;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null) _dfr = base.MakeReferenceInternal();
			_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			uint magic = br.ReadUInt32();
			if (magic != DDS_MAGIC) throw new InvalidDataFormatException();

			#region Header
			uint dwHeaderSize = br.ReadUInt32();
			if (dwHeaderSize != 124) throw new InvalidDataFormatException();

			Internal.DirectDrawSurfaceHeaderFlags dwHeaderFlags = (Internal.DirectDrawSurfaceHeaderFlags)br.ReadUInt32();
			// if (!((dwHeaderFlags & Internal.Constants.DirectDrawSurfaceFlags.PixelFormat) == Internal.Constants.DirectDrawSurfaceFlags.PixelFormat) || !((dwFlags & Internal.Constants.DirectDrawSurfaceFlags.Caps) == Internal.Constants.DirectDrawSurfaceFlags.Caps)) throw new InvalidDataFormatException();

			uint dwHeight = br.ReadUInt32();
			uint dwWidth = br.ReadUInt32();
			pic.Width = (int)dwWidth;
			pic.Height = (int)dwHeight;

			uint dwPitchOrLinearSize = br.ReadUInt32();
			uint dwDepth = br.ReadUInt32();
			uint dwMipMapCount = br.ReadUInt32();
			uint[] dwReserved1 =
			{
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32(),
				br.ReadUInt32()
			};
			#endregion
			#region PixelFormat
			Internal.DirectDrawSurfacePixelFormat pf = new Internal.DirectDrawSurfacePixelFormat();
			if ((dwHeaderFlags & Internal.DirectDrawSurfaceHeaderFlags.PixelFormat) == Internal.DirectDrawSurfaceHeaderFlags.PixelFormat)
			{
				pf.PixelFormatSize = br.ReadUInt32();
				pf.Flags = (Internal.DirectDrawSurfacePixelFormatFlags)br.ReadUInt32();
				pf.FourCC = br.ReadFixedLengthString(4);
				pf.RGBBitCount = br.ReadUInt32();
				pf.RBitMask = br.ReadUInt32();
				pf.GBitMask = br.ReadUInt32();
				pf.BBitMask = br.ReadUInt32();
				pf.AlphaBitMask = br.ReadUInt32();
			}
			#endregion
			#region Caps2
			if ((dwHeaderFlags & Internal.DirectDrawSurfaceHeaderFlags.Caps) == Internal.DirectDrawSurfaceHeaderFlags.Caps)
			{
				Internal.DirectDrawSurfaceCaps1 dwCaps1 = (Internal.DirectDrawSurfaceCaps1)br.ReadUInt32();
				Internal.DirectDrawSurfaceCaps2 dwCaps2 = (Internal.DirectDrawSurfaceCaps2)br.ReadUInt32();
				uint dwCaps3 = br.ReadUInt32(); // reserved
				uint dwCaps4 = br.ReadUInt32(); // reserved
				uint dwReserved2 = br.ReadUInt32(); // reserved
			}
			#endregion

			#region DDS Load Info
			Internal.DirectDrawSurfaceLoadInfo li;
			#region DXT1
			if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.FourCC) == Internal.DirectDrawSurfacePixelFormatFlags.FourCC) && (pf.FourCC == "1TXD" || pf.FourCC == "DXT1"))
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(true, false, false, 4, 8, Internal.DirectDrawSurfaceFormat.DXT1);
			}
			#endregion
			#region DXT3
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.FourCC) == Internal.DirectDrawSurfacePixelFormatFlags.FourCC) && (pf.FourCC == "3TXD" || pf.FourCC == "DXT3"))
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(true, false, false, 4, 16, Internal.DirectDrawSurfaceFormat.DXT3);
			}
			#endregion
			#region DXT5
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.FourCC) == Internal.DirectDrawSurfacePixelFormatFlags.FourCC) && (pf.FourCC == "5TXD" || pf.FourCC == "DXT5"))
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(true, false, false, 4, 16, Internal.DirectDrawSurfaceFormat.DXT5);
			}
			#endregion
			#region R16F
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.FourCC) == Internal.DirectDrawSurfacePixelFormatFlags.FourCC) && (pf.FourCC == "o\0\0\0" || pf.FourCC == "\0\0\0o"))
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 4, 16, Internal.DirectDrawSurfaceFormat.R16F);
			}
			#endregion
			#region G16R16F
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.FourCC) == Internal.DirectDrawSurfacePixelFormatFlags.FourCC) && (pf.FourCC == "p\0\0\0" || pf.FourCC == "\0\0\0p"))
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 4, 16, Internal.DirectDrawSurfaceFormat.G16R16F);
			}
			#endregion
			#region BGRA8
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) == Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 32 && pf.RBitMask == 0xff0000 && pf.GBitMask == 0xff00 && pf.BBitMask == 0xff && pf.AlphaBitMask == 0xff000000U)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 1, 4, Internal.DirectDrawSurfaceFormat.A8R8G8B8);
			}
			#endregion
			#region BGR8
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) != Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 24 && pf.RBitMask == 0xff0000 && pf.GBitMask == 0xff00 && pf.BBitMask == 0xff)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 1, 3, Internal.DirectDrawSurfaceFormat.R8G8B8);
			}
			#endregion
			#region A8B8G8R8
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) == Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 32 && pf.RBitMask == 0x0000ff && pf.GBitMask == 0xff00 && pf.BBitMask == 0xff0000 && pf.AlphaBitMask == 0xff000000U)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 1, 4, Internal.DirectDrawSurfaceFormat.A8B8G8R8);
			}
			#endregion
			#region BGR5A1
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) == Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 16 && pf.RBitMask == 0x00007c00 && pf.GBitMask == 0x000003e0 && pf.BBitMask == 0x0000001f && pf.AlphaBitMask == 0x00008000)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, true, false, 1, 2, Internal.DirectDrawSurfaceFormat.A1R5G5B5);
			}
			#endregion
			#region BGR565
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) != Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 16 && pf.RBitMask == 0x0000f800 && pf.GBitMask == 0x000007e0 && pf.BBitMask == 0x0000001f)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, true, false, 1, 2, Internal.DirectDrawSurfaceFormat.R5G6B5);
			}
			#endregion
			#region G16R16
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) != Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 32 && pf.RBitMask == 0x0000ffff && pf.GBitMask == 0xffff0000 && pf.BBitMask == 0x00000000)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 1, 1, Internal.DirectDrawSurfaceFormat.G16R16);
			}
			#endregion
			#region R3G3B2
			else if (((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.RGB) == Internal.DirectDrawSurfacePixelFormatFlags.RGB) && ((pf.Flags & Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) != Internal.DirectDrawSurfacePixelFormatFlags.AlphaPixels) && pf.RGBBitCount == 8 && pf.RBitMask == 0x000000e0 && pf.GBitMask == 0x0000001c && pf.BBitMask == 0x00000003)
			{
				li = new Internal.DirectDrawSurfaceLoadInfo(false, false, false, 1, 1, Internal.DirectDrawSurfaceFormat.R3G3B2);
			}
			#endregion
			#region Failure
			else
			{
				throw new InvalidDataFormatException("DirectDraw Surface invalid pixel format type");
			}
			#endregion
			#endregion

			#region Load the images
			uint width = dwWidth, height = dwHeight;
			uint mipMapCount = ((dwHeaderFlags & Internal.DirectDrawSurfaceHeaderFlags.MipMapCount) == Internal.DirectDrawSurfaceHeaderFlags.MipMapCount) ? dwMipMapCount : 1;

			if (li.Compressed)
			{
				uint size = Math.Max(li.DivSize, width) / li.DivSize * Math.Max(li.DivSize, height) / li.DivSize * li.BlockBytes;
				if (size != dwPitchOrLinearSize) throw new InvalidOperationException("size is not equal to (pitch or linear size)");

				if ((dwHeaderFlags & Internal.DirectDrawSurfaceHeaderFlags.LinearSize) != Internal.DirectDrawSurfaceHeaderFlags.LinearSize) throw new InvalidOperationException("flags does not contain LinearSize");

				byte[] data = new byte[size];
				for (uint ix = 0; ix < mipMapCount; ++ix)
				{
					br.Read(data, 0, data.Length);

					if (objectModel is PictureObjectModel)
					{
						// do not read any more mipmaps if we are just looking for one picture
						if (ix != 0)
						{
							width = (width + 1) >> 1;
							height = (height + 1) >> 1;
							size = Math.Max(li.DivSize, width) / li.DivSize * Math.Max(li.DivSize, height) / li.DivSize * li.BlockBytes;
							data = new byte[size];
							continue;
						}
					}

					byte[] rgba = null;
					Internal.DXTDecompression.decompressImage(out rgba, (int)width, (int)height, data, 1 << 1);
					for (int i = 0; i < rgba.Length; i += 4)
					{
						Color color = Color.FromRGBAByte(rgba[i], rgba[i + 1], rgba[i + 2], rgba[i + 3]);
						pic.SetPixel(color);
					}

					/*
					glCompressedTexImage2D(GL_TEXTURE_2D, ix, li->internalFormat, x, y, 0, size, data);
					gl->updateError();
					*/

					width = (width + 1) >> 1;
					height = (height + 1) >> 1;
					size = Math.Max(li.DivSize, width) / li.DivSize * Math.Max(li.DivSize, height) / li.DivSize * li.BlockBytes;
				}
				data = null;
			}
			else if (li.Palette)
			{
				// currently, we unpack palette into BGRA
				// I'm not sure we always get pitch...
				if ((dwHeaderFlags & Internal.DirectDrawSurfaceHeaderFlags.Pitch) != Internal.DirectDrawSurfaceHeaderFlags.Pitch) throw new InvalidDataFormatException("header flags does not include pitch");
				if (pf.RGBBitCount != 8) throw new InvalidDataFormatException("pixel format RGB bit count must be 8");
				uint size = dwPitchOrLinearSize * height;

				//  And I'm even less sure we don't get padding on the smaller MIP levels...
				if (!(size == (width * height * li.BlockBytes))) throw new InvalidDataFormatException("size does not equal (x * y * li.BlockBytes)");

				byte[] data = null;
				uint[] palette = new uint[256];
				uint[] unpacked = new uint[size];

				for (int i = 0; i < 256; i++)
				{
					palette[i] = br.ReadUInt32();
				}
				for (uint ix = 0; ix < mipMapCount; ++ix)
				{
					data = br.ReadBytes(size);
					for (uint zz = 0; zz < size; ++zz)
					{
						unpacked[zz] = palette[data[zz]];
					}
					/*
					glPixelStorei(GL_UNPACK_ROW_LENGTH, y);
					glTexImage2D(GL_TEXTURE_2D, ix, li.InternalFormat, x, y, 0, li.ExternalFormat, li.Type, unpacked);
					gl->updateError();
					*/
					width = (width + 1) >> 1;
					height = (height + 1) >> 1;
					size = width * height * li.BlockBytes;
				}
				data = null;
				unpacked = null;
			}
			else
			{
				if (li.Swap)
				{
					// glPixelStorei( GL_UNPACK_SWAP_BYTES, GL_TRUE );
				}
				//fixme: how are MIP maps stored for 24-bit if pitch != ySize*3 ?
				/*
				for (uint ix = 0; ix < mipMapCount; ++ix )
				{
				*/
				uint y = 0;

				for (uint x = 0; x <= width; x++)
				{
					if (x + 1 > width)
					{
						x = 0;
						y++;
					}
					if (y + 1 >= height)
					{
						break;
					}

					byte r = 0, g = 0, b = 0, a = 255;
					switch (li.Format)
					{
						case Internal.DirectDrawSurfaceFormat.R3G3B2:
						{
							// despite it saying R8G8B8, it is actually stored in the file as BGR!!!
							byte rgb = br.ReadByte();
							b = (byte)rgb.GetBits(0, 2);
							g = (byte)rgb.GetBits(2, 3);
							r = (byte)rgb.GetBits(5, 3);
							break;
						}
						case Internal.DirectDrawSurfaceFormat.A8R8G8B8:
						{
							// this is A8R8G8B8, in Grand Chase Cursor0.dds it is stored as RGBA
							r = br.ReadByte();
							g = br.ReadByte();
							b = br.ReadByte();
							a = br.ReadByte();
							break;
						}
						case Internal.DirectDrawSurfaceFormat.A8B8G8R8:
						{
							// despite it saying R8G8B8, it is actually stored in the file as BGR!!!
							a = br.ReadByte();
							b = br.ReadByte();
							g = br.ReadByte();
							r = br.ReadByte();
							break;
						}
						case Internal.DirectDrawSurfaceFormat.R8G8B8:
						{
							// despite it saying R8G8B8, it is actually stored in the file as BGR!!!
							b = br.ReadByte();
							g = br.ReadByte();
							r = br.ReadByte();
							break;
						}
						case Internal.DirectDrawSurfaceFormat.G16R16F:
						{
							float g0 = br.ReadHalf();
							float r0 = br.ReadHalf();

							throw new NotImplementedException();
							break;
						}
					}

					Color color = Color.FromRGBAByte(a, r, g, b);
					pic.SetPixel(color, (int)x, (int)y);
				}

				width = (width + 1) >> 1;
				height = (height + 1) >> 1;
				/*
				}
				*/
			}
			#endregion
		}

		private Color GetColorFromR5G6B5(ushort rgb1, byte alpha = 255)
		{
			byte r = (byte)rgb1.GetBits(0, 5);
			byte g = (byte)rgb1.GetBits(5, 6);
			byte b = (byte)rgb1.GetBits(11, 5);
			return Color.FromRGBAByte(alpha, r, g, b);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
