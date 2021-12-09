//
//  WMFDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework;
using MBS.Framework.Collections.Generic;
using MBS.Framework.Drawing;
using UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile.Internal;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public class WMFDataFormat : DataFormat
	{
		private readonly byte[] PLACEABLE_SIGNATURE = new byte[] { 0xD7, 0xCD, 0xC6, 0x9A };

		private Vector2D _outpos = Vector2D.Empty;

		/// <summary>
		/// The number of logical units per inch used to represent the image. This value can be
		/// used to scale an image.
		/// </summary>
		/// <remarks>
		/// By convention, an image is considered to be recorded at 1440 logical units (twips) per inch. Thus,
		/// a value of 720 specifies that the image SHOULD be rendered at twice its normal size, and a value
		/// of 2880 specifies that the image SHOULD be rendered at half its normal size.
		/// </remarks>
		/// <value>The twips per inch.</value>
		public ushort TwipsPerInch { get; set; } = 1440;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VectorImageObjectModel vec = (objectModel as VectorImageObjectModel);
			if (vec == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			reader.Endianness = Endianness.LittleEndian;

			if (reader.Accessor.Length <= 4)
				throw new InvalidDataFormatException("must be longer than 4 bytes");

			byte[] placeableSignature = reader.ReadBytes(4);
			if (placeableSignature.Match(PLACEABLE_SIGNATURE))
			{
				ushort hWmf = reader.ReadUInt16(); // must be 0x0000; memory placeholder for in-memory WMF

				// bounding box - 8 bytes (RECT X,Y,Right,Bottom)
				Rectangle boundingBox = ReadRECT(reader);

				TwipsPerInch = reader.ReadUInt16();

				uint reserved1 = reader.ReadUInt32();
				ushort checksum = reader.ReadUInt16();
			}
			else
			{
				reader.Seek(-4, SeekOrigin.Current);
			}

			WMFMetafileType fileType = (WMFMetafileType)reader.ReadInt16();
			ushort headersize = reader.ReadUInt16(); // number of uint16
			WMFMetafileVersion version = (WMFMetafileVersion) reader.ReadUInt16();
			ushort sizeLow = reader.ReadUInt16();
			ushort sizeHigh = reader.ReadUInt16();

			ushort objectCount = reader.ReadUInt16();
			uint maxRecord = reader.ReadUInt32();
			ushort memberCount = reader.ReadUInt16();
			if (memberCount != 0x0000)
			{
				Console.Error.WriteLine("ue: wmf: warning: someone put something other than 0x00000 in wmf.Header.MemberCount!");
			}

			while (!reader.EndOfStream)
			{
				uint recordSize = reader.ReadUInt32(); // 32-bit unsigned integer that defines the number of WORD structures in the WMF record.
				WMFRecordType recordType = (WMFRecordType)reader.ReadUInt16();

				switch (recordType)
				{
					// ============ Drawing Record Types ============
					case WMFRecordType.Arc:
					{
						ushort yEnd = reader.ReadUInt16();
						ushort xEnd = reader.ReadUInt16();
						ushort yStart = reader.ReadUInt16();
						ushort xStart = reader.ReadUInt16();

						ushort rectBottom = reader.ReadUInt16();
						ushort rectRight = reader.ReadUInt16();
						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.Chord:
					{
						ushort yRadial2 = reader.ReadUInt16();
						ushort xRadial2 = reader.ReadUInt16();
						ushort yRadial1 = reader.ReadUInt16();
						ushort xRadial1 = reader.ReadUInt16();

						ushort rectBottom = reader.ReadUInt16();
						ushort rectRight = reader.ReadUInt16();
						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.Ellipse:
					{
						ushort rectBottom = reader.ReadUInt16();
						ushort rectRight = reader.ReadUInt16();
						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.ExtFloodFill:
					{
						WMFFloodFillMode mode = (WMFFloodFillMode) reader.ReadUInt16();

						Color color = ReadColorRef(reader);

						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.ExtTextOut:
					{
						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();

						ushort length = reader.ReadUInt16();
						WMFExtTextOutOptions fwOpts = (WMFExtTextOutOptions) reader.ReadUInt16();
						if ((fwOpts & WMFExtTextOutOptions.Clipped) == WMFExtTextOutOptions.Clipped || (fwOpts & WMFExtTextOutOptions.Opaque) == WMFExtTextOutOptions.Opaque)
						{
							// I think the struct is present regardless of the value of the flags
						}
						Rectangle rect = ReadRECT(reader);

						string value = reader.ReadFixedLengthString(length);
						if (length % 2 != 0)
						{
							reader.ReadByte(); // padding
						}

						if (recordSize > (16 + value.Length + 1))
						{
							// optional Dx is present?
							for (int i = 0; i < value.Length; i++)
							{
								// distance between origins of adjacent character cells
								short Dx = reader.ReadInt16();
							}
						}
						break;
					}
					case WMFRecordType.FillRegion:
					{
						ushort regionIndex = reader.ReadUInt16(); // index into WMF Object Table
						ushort brushIndex = reader.ReadUInt16(); // index into WMF Object Table
						break;
					}
					case WMFRecordType.FloodFill:
					{
						Color color = ReadColorRef(reader);
						ushort ystart = reader.ReadUInt16();
						ushort xstart = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.FrameRegion:
					{
						ushort regionIndex = reader.ReadUInt16();
						ushort brushIndex = reader.ReadUInt16();
						ushort height = reader.ReadUInt16();
						ushort width = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.InvertRegion:
					{
						ushort regionIndex = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.LineTo:
					{
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.PaintRegion:
					{
						ushort regionIndex = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.PatBLT:
					{
						WMFTernaryRasterOperation rasterOperation = (WMFTernaryRasterOperation)reader.ReadUInt32();
						ushort height = reader.ReadUInt16();
						ushort width = reader.ReadUInt16();
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.Pie:
					{
						ushort yRadial2 = reader.ReadUInt16();
						ushort xRadial2 = reader.ReadUInt16();
						ushort yRadial1 = reader.ReadUInt16();
						ushort xRadial1 = reader.ReadUInt16();

						ushort rectBottom = reader.ReadUInt16();
						ushort rectRight = reader.ReadUInt16();
						ushort rectTop = reader.ReadUInt16();
						ushort rectLeft = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.PolyLine:
					{
						ushort pointCount = reader.ReadUInt16();
						for (ushort i = 0; i < pointCount; i++)
						{
							Vector2D point = ReadPointU16(reader);
						}
						break;
					}
					case WMFRecordType.Polygon:
					{
						ushort pointCount = reader.ReadUInt16();
						for (ushort i = 0; i < pointCount; i++)
						{
							Vector2D point = ReadPointU16(reader);
						}
						break;
					}
					case WMFRecordType.PolyPolygon:
					{
						// PolyPolygon object
						/* v = */
						ReadPolyPolygon(reader);
						break;
					}
					case WMFRecordType.Rectangle:
					{
						ushort bottom = reader.ReadUInt16();
						ushort right = reader.ReadUInt16();
						ushort top = reader.ReadUInt16();
						ushort left = reader.ReadUInt16();

						Rectangle rect = Rectangle.FromLTRB(left, top, right, bottom);
						break;
					}
					case WMFRecordType.RoundRect:
					{
						// weird, I thought they just used radius
						ushort ellipseHeight = reader.ReadUInt16();
						ushort ellipseWidth = reader.ReadUInt16();

						ushort bottom = reader.ReadUInt16();
						ushort right = reader.ReadUInt16();
						ushort top = reader.ReadUInt16();
						ushort left = reader.ReadUInt16();

						Rectangle rect = Rectangle.FromLTRB(left, top, right, bottom);
						break;
					}
					case WMFRecordType.SetPixel:
					{
						Color color = ReadColorRef(reader);
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.TextOut:
					{
						ushort length = reader.ReadUInt16();
						string value = reader.ReadFixedLengthString(length);
						if (length % 2 != 0)
						{
							reader.ReadByte(); // padding
						}

						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					// ============ Object Record Types ============
					case WMFRecordType.CreateBrushIndirect:
					{
						WMFBrush brush = ReadBrush(reader);
						ushort idx = _objs.Add(brush);
						break;
					}
					case WMFRecordType.CreateFontIndirect:
					{
						WMFFont font = ReadFont(reader);
						ushort idx = _objs.Add(font);
						break;
					}
					case WMFRecordType.CreatePalette:
					{
						WMFPalette palette = ReadPalette(reader);
						ushort idx = _objs.Add(palette);
						break;
					}
					case WMFRecordType.CreatePatternBrush:
					{
						WMFBitmap16 bitmap16 = ReadBitmap16Header(reader);
						byte[] reserved = reader.ReadBytes(18);

						int patternSize = bitmap16.DataLength;
						byte[] patternData = reader.ReadBytes(patternSize);
						break;
					}
					case WMFRecordType.CreatePenIndirect:
					{
						WMFPenStyle penStyle = (WMFPenStyle)reader.ReadUInt16();
						Vector2D width = ReadPointU16(reader);
						ushort actualWidth = (ushort)width.X;
						Color color = ReadColorRef(reader);
						break;
					}
					case WMFRecordType.CreateRegion:
					{
						ushort nextInChain = reader.ReadUInt16(); // must be ignored
						ushort objectType = reader.ReadUInt16(); // must be 0x0006
						uint regionObjectCount = reader.ReadUInt32(); // must be ignored
						ushort regionSize = reader.ReadUInt16(); // defines the size of the region in bytes plus the size of aScans in bytes
						ushort scanCount = reader.ReadUInt16(); // number of scanlines composing the region
						ushort maxScan = reader.ReadUInt16(); // maximum number of points in any one scan in this region
						Rectangle boundingRect = ReadRECT(reader);

						// aScans
						for (ushort i = 0; i < scanCount; i++)
						{
							// A 16-bit unsigned integer that specifies the number of horizontal (x-axis)
							// coordinates in the ScanLines array.This value MUST be a multiple of 2, since left and right
							// endpoints are required to specify each scanline.
							ushort scanlineCount = reader.ReadUInt16();

							ushort scanlineTop = reader.ReadUInt16();
							ushort scanlineBottom = reader.ReadUInt16();

							for (ushort j = 0; j < scanlineCount; j++)
							{
								ushort scanlineLeft = reader.ReadUInt16();
								ushort scanlineRight = reader.ReadUInt16();
							}

							ushort scanlineCount2 = reader.ReadUInt16(); // must be == scanlineCount
							if (scanlineCount != scanlineCount2)
								throw new InvalidDataFormatException("ue: wmf: sanity check failed - scanlineCount != scanlineCount2");
						}
						break;
					}
					case WMFRecordType.DeleteObject:
					{
						ushort objectIndex = reader.ReadUInt16();
						_objs.Remove(objectIndex);
						break;
					}
					case WMFRecordType.DIBCreatePatternBrush:
					{
						WMFBrushStyle brushStyle = (WMFBrushStyle)reader.ReadUInt16();
						if (brushStyle != WMFBrushStyle.Pattern)
						{
							Console.Error.WriteLine("ue: wmf: DIBCreatePatternBrush - brushStyle not Pattern; assuming DIBPatternPT");
							brushStyle = WMFBrushStyle.DIBPatternPT;
						}

						WMFColorUsage colorUsage = (WMFColorUsage)reader.ReadUInt16();
						if (brushStyle == WMFBrushStyle.Pattern)
						{
							// If the Style field specifies BS_PATTERN, a ColorUsage value of DIB_RGB_COLORS MUST be used
							// regardless of the contents of this field.
							colorUsage = WMFColorUsage.RGBColors;
						}

						// FIXME: continue reading DIB here
						throw new NotImplementedException();
						break;
					}
					case WMFRecordType.SelectClipRegion:
					{
						ushort regionIndex = reader.ReadUInt16();

						break;
					}
					case WMFRecordType.SelectObject:
					{
						ushort objectIndex = reader.ReadUInt16();

						break;
					}
					case WMFRecordType.SelectPalette:
					{
						ushort objectIndex = reader.ReadUInt16();

						break;
					}
					// ============ State Record Types ============
					case WMFRecordType.AnimatePalette:
					{
						// redefines entries in the logical palette that is defined in the
						// playback device context with the specified Palette Object

						// The logical palette that is specified by the Palette Object in this record is the source of the palette
						// changes, and the logical palette that is currently selected into the playback device context is the
						// destination. Entries in the destination palette with the PC_RESERVED PaletteEntryFlag
						// Enumeration(section 2.1.1.22) set SHOULD be modified by this record, and entries with that flag
						// clear SHOULD NOT be modified. If none of the entries in the destination palette have the
						// PC_RESERVED flag set, then this record SHOULD have no effect.
						WMFPalette palette = ReadPalette(reader);
						break;
					}
					case WMFRecordType.ExcludeClipRect:
					{
						ushort bottom = reader.ReadUInt16();
						ushort right = reader.ReadUInt16();
						ushort top = reader.ReadUInt16();
						ushort left = reader.ReadUInt16();

						Rectangle rect = Rectangle.FromLTRB(left, top, right, bottom);
						break;
					}
					case WMFRecordType.IntersectClipRect:
					{
						ushort bottom = reader.ReadUInt16();
						ushort right = reader.ReadUInt16();
						ushort top = reader.ReadUInt16();
						ushort left = reader.ReadUInt16();

						Rectangle rect = Rectangle.FromLTRB(left, top, right, bottom);
						break;
					}
					case WMFRecordType.MoveTo:
					{
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						_outpos = new Vector2D(x, y);
						break;
					}
					case WMFRecordType.OffsetClipRegion:
					{
						// moves the clipping region in the playback device context
						// by the specified offsets
						ushort yOffset = reader.ReadUInt16();
						ushort xOffset = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.OffsetViewportOrigin:
					{
						// moves the viewport origin in the playback device context
						// by the specified offsets
						ushort yOffset = reader.ReadUInt16();
						ushort xOffset = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.OffsetWindowOrigin:
					{
						// moves the output window origin in the playback device context
						// by the specified offsets
						ushort yOffset = reader.ReadUInt16();
						ushort xOffset = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.RealizePalette:
					{
						// maps entries from the logical palette that is defined in the
						// playback device context to the system palette
						break;
					}
					case WMFRecordType.ResizePalette:
					{
						// redefines the size of the logical palette that is defined in the
						// playback device context
						ushort paletteEntryCount = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.RestoreDC:
					{
						// A 16-bit signed integer that defines the saved state to be restored. If this
						// member is positive, nSavedDC represents a specific instance of the state to be restored.If this
						// member is negative, nSavedDC represents an instance relative to the current state.
						short nSavedDC = reader.ReadInt16();
						break;
					}
					case WMFRecordType.SaveDC:
					{
						// saves the playback device context for later retrieval
						break;
					}
					case WMFRecordType.ScaleViewportExtents:
					{
						// scales the horizontal and vertical extents of the viewport
						// that is defined in the playback device context by using the ratios formed by the specified
						// multiplicands and divisors
						short yDenominator = reader.ReadInt16();
						short yNumerator = reader.ReadInt16();
						short xDenominator = reader.ReadInt16();
						short xNumerator = reader.ReadInt16();
						break;
					}
					case WMFRecordType.ScaleWindowExtents:
					{
						// scales the horizontal and vertical extents of the output window
						// that is defined in the playback device context by using the ratios formed by the specified
						// multiplicands and divisors
						short yDenominator = reader.ReadInt16();
						short yNumerator = reader.ReadInt16();
						short xDenominator = reader.ReadInt16();
						short xNumerator = reader.ReadInt16();
						break;
					}
					case WMFRecordType.SetBackgroundColor:
					{
						// sets the background color in the playback device context to a
						// specified color, or to the nearest physical color if the device cannot represent the specified color
						Color color = ReadColorRef(reader);
						break;
					}
					case WMFRecordType.SetBackgroundMode:
					{
						// defines the background raster operation mix mode in the
						// playback device context.The background mix mode is the mode for combining pens, text, hatched
						// brushes, and interiors of filled objects with background colors on the output surface.
						WMFMixMode mixMode = (WMFMixMode)reader.ReadUInt16();
						ushort reserved = reader.ReadUInt16(); // must be ignored
						break;
					}
					case WMFRecordType.SetLayout:
					{
						WMFLayout layout = (WMFLayout)reader.ReadUInt16();
						ushort reserved = reader.ReadUInt16(); // must be ignored
						break;
					}
					case WMFRecordType.SetMapMode:
					{
						WMFMapMode mapMode = (WMFMapMode)reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetMapperFlags:
					{
						// defines the algorithm that the font mapper uses when it
						// maps logical fonts to physical fonts
						WMFFontMapperFlags mapperValues = (WMFFontMapperFlags) reader.ReadUInt32();
						break;
					}
					case WMFRecordType.SetPaletteEntries:
					{
						// defines RGB color values in a range of entries in the logical
						// palette that is defined in the playback device context
						WMFPalette palette = ReadPalette(reader);

						// The META_SETPALENTRIES modifies the logical palette that is currently selected in the playback
						// device context. A META_SELECTPALETTE Record(section 2.3.4.11) MUST have been used to
						// specify that logical palette in the form of a Palette Object prior to the occurrence of the
						// META_SETPALENTRIES in the metafile. A Palette object is one of the graphics objects that is
						// maintained in the playback device context during playback of the metafile.See Graphics Objects
						// (section 1.3.2) for more information.
						break;
					}
					case WMFRecordType.SetPolyfillMode:
					{
						WMFPolyfillMode mode = (WMFPolyfillMode)reader.ReadUInt16();
						ushort reserved = reader.ReadUInt16(); // must be ignored
						break;
					}
					case WMFRecordType.SetRELabs:
					{
						// reserved and not supported
						break;
					}
					case WMFRecordType.SetForegroundRasterOperation:
					{
						// defines the foreground raster operation mix mode in the playback
						// device context. The foreground mix mode is the mode for combining pens and interiors of filled
						// objects with foreground colors on the output surface.
						WMFBinaryRasterOperation rasterOperation = (WMFBinaryRasterOperation)reader.ReadUInt16();
						ushort reserved = reader.ReadUInt16(); // must be ignored
						break;
					}
					case WMFRecordType.SetStretchBLTMode:
					{
						// defines the bitmap stretching mode in the playback
						// device context
						WMFStretchMode stretchMode = (WMFStretchMode)reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetTextAlign:
					{
						ushort textAlignmentMode = reader.ReadUInt16();
						// MUST be a combination of one or more TextAlignmentMode Flags (section 2.1.2.3) for text with
						// a horizontal baseline, and VerticalTextAlignmentMode Flags(section 2.1.2.4) for text with a
						// vertical baseline.
						ushort reserved = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetTextCharExtra:
					{
						// defines inter-character spacing for text justification in the
						// playback device context.Spacing is added to the white space between each character, including
						// break characters, when a line of justified text is output
						ushort charExtra = reader.ReadUInt16();
						//A 16-bit unsigned integer that defines the amount of extra space, in logical
						// units, to be added to each character. If the current mapping mode is not MM_TEXT, this value is
						// transformed and rounded to the nearest pixel.For details about setting the mapping mode, see
						// META_SETMAPMODE Record(section 2.3.5.17).
						break;
					}
					case WMFRecordType.SetTextColor:
					{
						// defines the text foreground color in the playback device
						// context
						Color color = ReadColorRef(reader);
						break;
					}
					case WMFRecordType.SetTextJustification:
					{
						// defines the amount of space to add to break
						// characters in a string of justified text

						// specifies the number of space characters in
						// the line
						ushort breakCount = reader.ReadUInt16();
						// specifies the total extra space, in logical units,
						// to be added to the line of text. If the current mapping mode is not MM_TEXT, the value identified
						// by the BreakExtra member is transformed and rounded to the nearest pixel. For details about
						// setting the mapping mode, see META_SETMAPMODE Record(section 2.3.5.17).
						ushort breakExtra = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetViewportExtents:
					{
						// sets the horizontal and vertical extents of the viewport in the
						// playback device context
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetViewportOrigin:
					{
						// defines the viewport origin in the playback device
						// context
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetWindowExtents:
					{
						// sets the horizontal and vertical extents of the output window in the
						// playback device context
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					case WMFRecordType.SetWindowOrigin:
					{
						// defines the output window origin in the playback device
						// context
						ushort y = reader.ReadUInt16();
						ushort x = reader.ReadUInt16();
						break;
					}
					// ============ Escape Record Types ============
					case WMFRecordType.Escape:
					{
						WMFEscapeFunction escapeFunction = (WMFEscapeFunction)reader.ReadUInt16();
						ushort dataLength = reader.ReadUInt16();

						switch (escapeFunction)
						{
							case WMFEscapeFunction.StartDocument:
							{
								// informs the printer driver that a new print job is starting
								ushort docNameLength = reader.ReadUInt16();
								string docName = reader.ReadFixedLengthString(docNameLength);
								break;
							}
							default:
							{
								byte[] data = reader.ReadBytes(dataLength);
								ProcessWMFEscapeFunction(objectModel, escapeFunction, data);
								break;
							}
						}
						break;
					}
					default:
					{
						byte[] data = reader.ReadBytes(recordSize * 2);
						ProcessWMFRecord(objectModel, recordType, data);
						break;
					}
				}
			}
		}

		protected virtual void ProcessWMFRecord(ObjectModel objectModel, WMFRecordType recordType, byte[] data)
		{
			// stub for inherited classes
		}
		protected virtual void ProcessWMFEscapeFunction(ObjectModel objectModel, WMFEscapeFunction escapeFunction, byte[] data)
		{
			// stub for inherited classes
		}

		private WMFBitmap16 ReadBitmap16Header(Reader reader)
		{
			WMFBitmap16 bitmap16 = new WMFBitmap16();
			bitmap16.Type = (WMFBitmap16.WMFBitmap16Type)reader.ReadUInt16();
			bitmap16.Width = reader.ReadUInt16();
			bitmap16.Height = reader.ReadUInt16();
			bitmap16.WidthBytes = reader.ReadUInt16();
			bitmap16.Planes = reader.ReadByte();
			bitmap16.BitsPerPixel = reader.ReadByte();

			int bitmapDataSize = 4; // (((bitmap16.Width * bitmap16.BitsPerPixel + 15) >> 4) << 1) * bitmap16.Height;
			byte[] bitmapData = reader.ReadBytes(bitmapDataSize);
			return bitmap16;
		}
		private WMFBitmap16 ReadBitmap16(Reader reader)
		{
			WMFBitmap16 bitmap16 = new WMFBitmap16();
			bitmap16.Type = (WMFBitmap16.WMFBitmap16Type)reader.ReadUInt16();
			bitmap16.Width = reader.ReadUInt16();
			bitmap16.Height = reader.ReadUInt16();
			bitmap16.WidthBytes = reader.ReadUInt16();
			bitmap16.Planes = reader.ReadByte();
			bitmap16.BitsPerPixel = reader.ReadByte();

			int bitmapDataSize = bitmap16.DataLength;
			byte[] bitmapData = reader.ReadBytes(bitmapDataSize);
			return bitmap16;
		}

		private WMFPalette ReadPalette(Reader reader)
		{
			WMFPalette palette = new WMFPalette();
			palette.Start = reader.ReadUInt16();
			ushort entryCount = reader.ReadUInt16();
			palette.Entries = new WMFPalette.WMFPaletteEntry[entryCount];
			for (ushort i = 0; i < entryCount; i++)
			{
				palette.Entries[i] = ReadPaletteEntry(reader);
			}
			return palette;
		}

		private WMFPalette.WMFPaletteEntry ReadPaletteEntry(Reader reader)
		{
			WMFPalette.WMFPaletteEntry entry = new WMFPalette.WMFPaletteEntry();
			byte red = reader.ReadByte();
			byte green = reader.ReadByte();
			byte blue = reader.ReadByte();
			entry.Color = Color.FromRGBAByte(red, green, blue);
			entry.Flags = (WMFPaletteEntryFlags)reader.ReadByte();
			return entry;
		}

		private WMFBrush ReadBrush(Reader reader)
		{
			WMFBrushStyle brushStyle = (WMFBrushStyle)reader.ReadUInt16();
			Color color = ReadColorRef(reader);
			WMFBrushHatchType brushHatchType = (WMFBrushHatchType)reader.ReadUInt16();
			return new WMFBrush(brushStyle, color, brushHatchType);
		}

		private HandleDictionary<WMFObject, ushort> _objs = new HandleDictionary<WMFObject, ushort>();

		private WMFFont ReadFont(Reader reader)
		{
			ushort height = reader.ReadUInt16();
			ushort width = reader.ReadUInt16();
			ushort escapement = reader.ReadUInt16();
			ushort orientation = reader.ReadUInt16();
			ushort weight = reader.ReadUInt16();
			bool italic = reader.ReadBoolean();
			bool underline = reader.ReadBoolean();
			bool strikeout = reader.ReadBoolean();
			WMFCharset charset = (WMFCharset)reader.ReadByte();
			WMFOutputPrecision precision = (WMFOutputPrecision)reader.ReadByte();
			WMFClipPrecision clipPrecision = (WMFClipPrecision)reader.ReadByte();
			WMFFontQuality fontQuality = (WMFFontQuality)reader.ReadByte();

			byte pitchAndFamily = reader.ReadByte();
			WMFFontFamily family = (WMFFontFamily)pitchAndFamily.GetBits(0, 4);
			WMFFontPitch pitch = (WMFFontPitch)pitchAndFamily.GetBits(6, 2);

			string faceName = reader.ReadFixedLengthString(32).TrimNull();
			return new Internal.WMFFont(height, width, escapement, orientation, weight, italic, underline, strikeout, charset, precision, clipPrecision, fontQuality, family, pitch, faceName);
		}

		private void ReadPolyPolygon(Reader reader)
		{
			ushort polygonCount = reader.ReadUInt16();
			ushort[] aPointsPerPolygon = new ushort[polygonCount];
			for (ushort i = 0; i < polygonCount; i++)
			{
				aPointsPerPolygon[i] = reader.ReadUInt16();
			}

			Vector2D[] points = new Vector2D[aPointsPerPolygon.Sum()];
			for (int i = 0; i < points.Length; i++)
			{
				points[i] = ReadPointU16(reader);
			}
		}

		private Vector2D ReadPointU16(Reader reader)
		{
			ushort x = reader.ReadUInt16();
			ushort y = reader.ReadUInt16();
			return new Vector2D(x, y);
		}

		private Rectangle ReadRECT(Reader reader)
		{
			ushort left = reader.ReadUInt16();
			ushort top = reader.ReadUInt16();
			ushort right = reader.ReadUInt16();
			ushort bottom = reader.ReadUInt16();

			return Rectangle.FromLTRB(left, top, right, bottom);
		}

		private static Color ReadColorRef(Reader reader)
		{
			byte colorRefR = reader.ReadByte();
			byte colorRefG = reader.ReadByte();
			byte colorRefB = reader.ReadByte();
			byte colorRefA = reader.ReadByte(); // actually reserved

			return Color.FromRGBAByte(colorRefR, colorRefG, colorRefB);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
