//
//  WMFRecordType.cs
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public enum WMFRecordType : ushort
	{
		EOF = 0x0000,

		// ====================
		// Drawing Record Types
		// ====================

		/// <summary>
		/// Draws an elliptical arc.
		/// </summary>
		Arc = 0x0817,
		/// <summary>
		/// Draws a chord.
		/// </summary>
		Chord = 0x0830,
		/// <summary>
		/// Draws an ellipse.
		/// </summary>
		Ellipse = 0x0418,
		/// <summary>
		/// Fills an area with the brush that is defined in the playback device
		/// context.
		/// </summary>
		ExtFloodFill = 0x0548,
		/// <summary>
		/// Outputs a character string with optional opaquing and clipping.
		/// </summary>
		ExtTextOut = 0x0a32,
		/// <summary>
		/// Fills a region using a specified brush.
		/// </summary>
		FillRegion = 0x0228,
		/// <summary>
		/// Fills an area of the output surface with the brush that is defined in the
		/// playback device context.
		/// </summary>
		FloodFill = 0x0419,
		/// <summary>
		/// Draws a border around a specified region using a specified brush.
		/// </summary>
		FrameRegion = 0x0429,
		/// <summary>
		/// Draws a region in which the colors are inverted.
		/// </summary>
		InvertRegion = 0x012A,
		/// <summary>
		/// Draws a line from the drawing position that is defined in the playback
		/// device context up to, but not including, a specified position.
		/// </summary>
		LineTo = 0x0213,
		/// <summary>
		/// Paints the specified region using the brush that is defined in the playback
		/// device context.
		/// </summary>
		PaintRegion = 0x012B,
		/// <summary>
		/// Paints a specified rectangle by using the brush that is defined in the
		/// playback device context.
		/// </summary>
		PatBLT = 0x061D,
		/// <summary>
		/// Draws a pie-shaped wedge bounded by the intersection of an ellipse and
		/// two radial lines.
		/// </summary>
		Pie = 0x081A,
		/// <summary>
		/// Draws a series of line segments by connecting the points in the specified
		/// array.
		/// </summary>
		PolyLine = 0x0325,
		/// <summary>
		/// Paints a polygon consisting of two or more vertices connected by straight
		/// lines.
		/// </summary>
		Polygon = 0x0324,
		/// <summary>
		/// Paints a series of closed polygons.
		/// </summary>
		PolyPolygon = 0x0538,
		/// <summary>
		/// Paints a rectangle.
		/// </summary>
		Rectangle = 0x041B,
		/// <summary>
		/// Paints a rectangle with rounded corners.
		/// </summary>
		RoundRect = 0x061C,
		/// <summary>
		/// Sets the pixel at specified coordinates to a specified color.
		/// </summary>
		SetPixel = 0x041F,
		/// <summary>
		/// Outputs a character string.
		/// </summary>
		TextOut = 0x0521,

		RealizePalette = 0x0035,
		SetPaletteEntries = 0x0037,
		SetBackgroundMode = 0x0102,
		SetMapMode = 0x0103,
		SetForegroundRasterOperation = 0x0104,
		SetRELabs = 0x0105,
		SetPolyfillMode = 0x0106,
		SetStretchBLTMode = 0x0107,
		SetTextCharExtra = 0x0108,
		RestoreDC = 0x0127,
		ResizePalette = 0x0139,
		DIBCreatePatternBrush = 0x0142,
		SetLayout = 0x0149,
		SetBackgroundColor = 0x0201,
		SetTextColor = 0x0209,
		OffsetViewportOrigin = 0x0211,
		MoveTo = 0x0214,
		OffsetClipRegion = 0x0220,
		SetMapperFlags = 0x0231,
		SelectPalette = 0x0234,
		SetTextJustification = 0x020A,
		SetWindowOrigin = 0x020B,
		SetWindowExtents = 0x020C,
		SetViewportOrigin = 0x020D,
		SetViewportExtents = 0x020E,
		OffsetWindowOrigin = 0x020F,
		ScaleWindowExtents = 0x0410,
		ScaleViewportExtents = 0x0412,
		ExcludeClipRect = 0x0415,
		IntersectClipRect = 0x0416,
		AnimatePalette = 0x0436,
		SaveDC = 0x001E,
		StretchBLT = 0x0B23,
		Escape = 0x0626,
		SelectClipRegion = 0x012C,
		SelectObject = 0x012D,
		SetTextAlign = 0x012E,
		BitBLT = 0x0922,
		SetDIBToDEV = 0x0d33,
		DIBBitBLT = 0x0940,
		DIBStretchBLT = 0x0b41,
		StretchDIB = 0x0f43,
		DeleteObject = 0x01f0,
		CreatePalette = 0x00f7,
		CreatePatternBrush = 0x01F9,
		CreatePenIndirect = 0x02FA,
		CreateFontIndirect = 0x02FB,
		CreateBrushIndirect = 0x02FC,
		CreateRegion = 0x06FF
	}
}
