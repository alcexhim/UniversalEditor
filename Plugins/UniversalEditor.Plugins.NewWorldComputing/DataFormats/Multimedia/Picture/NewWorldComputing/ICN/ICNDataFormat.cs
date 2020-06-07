//
//  ICNDataFormat.cs - provides a DataFormat for manipulating sprites (animatable collections of images) in Heroes of Might and Magic II ICN format
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
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.NewWorldComputing.ICN
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating sprites (animatable collections of images) in Heroes of Might and Magic II ICN format.
	/// </summary>
	public class ICNDataFormat : DataFormat
	{
		private struct SpriteHeader
		{
			public short offsetX;
			public short offsetY;
			public ushort width;
			public ushort height;
			public byte type;
			public uint dataOffset;
		}

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}


		private Color shadowColor = Color.FromRGBAByte(0, 0, 0, 0x40);
		private Color opaqueColor = Color.FromRGBAByte(0, 0, 0, 0xFF); // non-transparent mask

		private Color GetColorIndex(byte index)
		{
			int depth = 9;
			if (depth == 8) return Color.Empty;

			// index *= 3;
			return HoMM2Palette.ColorTable[index];
		}
		
		private void SpriteDrawICN(ref PictureObjectModel pic, SpriteHeader head, ref byte[] data, bool debug)
		{
			if (data == null || data.Length == 0) return;
		
			byte c = 0;
			ushort x = 0, y = 0;

			int cur = 0;
			while (cur < data.Length)
			{
				if (data[cur] == 0)
				{
					// 0x00 - end line
					++y;
					x = 0;
					++cur;
				}
				else if (data[cur] < 0x80)
				{
					// 0x7F - count data
					c = data[cur];
					++cur;
					while((c-- != 0) && (cur < data.Length))
					{
						pic.SetPixel(GetColorIndex(data[cur]), x, y);
						++x;
						++cur;
					}
				}
				else if (data[cur] == 0x80)
				{
					// 0x80 - end data
					break;
				}
				else if (data[cur] < 0xC0)
				{
					// 0xBF - skip data
					x += (ushort)(data[cur] - 0x80);
					++cur;
				}
				else if (data[cur] == 0xC0)
				{
					// 0xC0 - shadow
					++cur;
					c = (byte)((data[cur] % 4 != 0) ? (data[cur] % 4) : data[++cur]);
					while((c-- != 0))
					{
						pic.SetPixel(shadowColor, x, y);
						++x;
					}
					++cur;
				}
				else if (data[cur] == 0xC1)
				{
					// 0xC1
					++cur;
					c = data[cur];
					++cur;
					while (c-- != 0)
					{
						pic.SetPixel(GetColorIndex(data[cur]), x, y);
						++x;
					}
					++cur;
				}
				else
				{
					c = (byte)(data[cur] - 0xC0);
					++cur;
					while (c-- != 0)
					{
						pic.SetPixel(GetColorIndex(data[cur]), x, y);
						++x;
					}
					++cur;
				}

				if (cur > data.Length)
				{
					throw new ArgumentOutOfRangeException("cur", cur, "must be less than max " + data.Length.ToString());
				}
			}
		}


		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);
			PictureObjectModel picret = (objectModel as PictureObjectModel);
			if (coll == null && picret == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			#region header
			ushort spriteCount = br.ReadUInt16();
			uint totalSize = br.ReadUInt32();
			#endregion

			long position = br.Accessor.Position;

			List<SpriteHeader> shs = new List<SpriteHeader>();
			for (ushort i = 0; i < spriteCount; i++)
			{
				SpriteHeader sh = new SpriteHeader();
				sh.offsetX = br.ReadInt16();
				sh.offsetY = br.ReadInt16();
				sh.width = br.ReadUInt16();
				sh.height = br.ReadUInt16();
				sh.type = br.ReadByte();
				sh.dataOffset = br.ReadUInt32();
				shs.Add(sh);
			}
			for (ushort i = 0; i < spriteCount; i++)
			{
				SpriteHeader sh = shs[i];

				PictureObjectModel pic = new PictureObjectModel();
				/*
				pic.Left = sh.offsetX;
				pic.Top = sh.offsetY;
				*/
				pic.Width = sh.width;
				pic.Height = sh.height;

				uint datasize = ((i + 1 != spriteCount) ? (shs[i + 1].dataOffset - shs[i].dataOffset) : (totalSize - shs[i].dataOffset));

				br.Accessor.Seek(position + shs[i].dataOffset, IO.SeekOrigin.Begin);
				byte[] data = br.ReadBytes(datasize);

				try
				{
					SpriteDrawICN(ref pic, sh, ref data, false);
				}
				catch (InvalidOperationException)
				{
					continue;
				}

				// pic.ToBitmap().Save(@"C:\Temp\test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

				if (picret != null)
				{
					pic.CopyTo(picret);
					return;
				}
				coll.Pictures.Add(pic);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
