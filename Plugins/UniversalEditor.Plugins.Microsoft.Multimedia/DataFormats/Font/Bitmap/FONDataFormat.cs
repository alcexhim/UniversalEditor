//
//  FONDataFormat.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Executable.Microsoft;
using UniversalEditor.ObjectModels.Executable;
using UniversalEditor.ObjectModels.Multimedia.Font.Bitmap;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.Plugins.Microsoft.Multimedia.DataFormats.Font.Bitmap
{
	public class FONDataFormat : MicrosoftExecutableDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(BitmapFontObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ExecutableObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ExecutableObjectModel exe = objectModels.Pop() as ExecutableObjectModel;
			BitmapFontObjectModel fon = objectModels.Pop() as BitmapFontObjectModel;

			foreach (ExecutableResource res in exe.Resources)
			{
				if (res.ResourceType == ExecutableResourceTypes.Font)
				{
					MemoryAccessor ma = new MemoryAccessor(res.Source.GetData());

					ushort dfVersion = ma.Reader.ReadUInt16();
					uint dfSize = ma.Reader.ReadUInt32();
					string dfCopyright = ma.Reader.ReadFixedLengthString(60).TrimNull();
					ushort dfType = ma.Reader.ReadUInt16();
					ushort dfPoints = ma.Reader.ReadUInt16();
					ushort dfVertRes = ma.Reader.ReadUInt16();
					ushort dfHorizRes = ma.Reader.ReadUInt16();
					ushort dfAscent = ma.Reader.ReadUInt16();
					ushort dfInternalLeading = ma.Reader.ReadUInt16();
					ushort dfExternalLeading = ma.Reader.ReadUInt16();
					byte dfItalic = ma.Reader.ReadByte();
					byte dfUnderline = ma.Reader.ReadByte();
					byte dfStrikeout = ma.Reader.ReadByte();
					ushort dfWeight = ma.Reader.ReadUInt16();
					byte dfCharSet = ma.Reader.ReadByte();
					ushort dfPixWidth = ma.Reader.ReadUInt16();
					ushort dfPixHeight = ma.Reader.ReadUInt16();
					byte dfPitchAndFamily = ma.Reader.ReadByte();
					ushort dfAvgWidth = ma.Reader.ReadUInt16();
					ushort dfMaxWidth = ma.Reader.ReadUInt16();
					byte dfFirstChar = ma.Reader.ReadByte();
					byte dfLastChar = ma.Reader.ReadByte();
					byte dfDefaultChar = ma.Reader.ReadByte();
					byte dfBreakChar = ma.Reader.ReadByte();
					ushort dfWidthBytes = ma.Reader.ReadUInt16();
					uint dfDevice = ma.Reader.ReadUInt32();
					uint dfFace = ma.Reader.ReadUInt32();
					uint dfReserved = ma.Reader.ReadUInt32();

					fon.Size = new MBS.Framework.Drawing.Dimension2D(dfMaxWidth, dfPixHeight);

					uint charStart = 0x00000000, charSize = 0;
					if (dfVersion == 0x200)
					{
						charStart = 0x76;
						charSize = 4;
					}
					else
					{
						charStart = 0x94;
						charSize = 6;
					}

					//char szDeviceName[];
					//char szFaceName[];

					if (dfDevice != 0)
					{

					}
					if (dfFace != 0)
					{
						ma.Reader.Accessor.SavePosition();

						ma.Reader.Accessor.Seek(dfFace, IO.SeekOrigin.Begin);
						fon.Title = ma.Reader.ReadNullTerminatedString();

						ma.Reader.Accessor.LoadPosition();
					}

					BitmapFontPixmap pixmap = new BitmapFontPixmap();

					int glyphCount = dfLastChar - dfFirstChar;
					for (int j = 0; j < glyphCount; j++)
					{
						char c = (char)(dfFirstChar + j);
						uint maskOffsetAddress = (uint)(j * dfPixHeight * dfPixWidth);
						uint charPos = (uint)(charStart + charSize * j);

						ma.Reader.Accessor.Seek(charPos, IO.SeekOrigin.Begin);

						ushort width = ma.Reader.ReadUInt16();
						PictureObjectModel pic = new PictureObjectModel(dfMaxWidth, dfPixHeight);

						uint offset = 0;
						if (charSize == 4)
						{
							// yeah i know, it makes no sense, it's bass-ackwards
							offset = ma.Reader.ReadUInt16();
						}
						else
						{
							offset = ma.Reader.ReadUInt32();
						}

						ma.Reader.Accessor.Seek(offset, IO.SeekOrigin.Begin);
						int x = 0, y = 0;

						byte mask = 0x80;
						int pk = (width + 7) / 8;
						byte b = 0, bits = 0;
						b = ma.Reader.ReadByte();

						long pos = ma.Position;
						for (y = 0; y < pic.Height; y++)
						{
							for (x = 0; x < pic.Width; x++)
							{
								if (bits < 8)
								{
									// check if first bit is set
									if ((b & mask) == mask)
									{
										pic.SetPixel(MBS.Framework.Drawing.Colors.Black, x, y);
									}
									else
									{
										pic.SetPixel(MBS.Framework.Drawing.Colors.White, x, y);
									}
									b <<= 1; // goto next bit
									bits++;
								}
								else
								{
									bits = 0;
									b = ma.Reader.ReadByte();
								}
							}

							long dwpos = ma.Position;
							ma.Reader.Align(dfWidthBytes);
						}
						/*
						for (int column = 0; column < pk; column++)
						{
							// read row
							for (int row = 0; row < dfPixHeight; row++)
							{
								// read byte
								byte b = ma.Reader.ReadByte();
								uint rowAddr = (uint)(maskOffsetAddress + row * dfPixWidth);

								for (byte bits = 0; bits < 8; bits++)
								{
									// don't try to put filler bits into mask
									if ((bits + column * 8) >= width)
									{
										x = 0;
										y++;
										break;
									}
									uint addr = (uint)(rowAddr + bits + column * 8);

									x++;
								}
							}

							y++;
							x = 0;
						}
						*/

						BitmapFontGlyph glyph = new BitmapFontGlyph();
						glyph.Character = c;
						glyph.Picture = pic;
						pixmap.Glyphs.Add(glyph);
					}
					fon.Pixmaps.Add(pixmap);
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			BitmapFontObjectModel fon = objectModels.Pop() as BitmapFontObjectModel;
			ExecutableObjectModel exe = new ExecutableObjectModel();
			objectModels.Push(exe);

			base.BeforeSaveInternal(objectModels);
		}
	}
}
