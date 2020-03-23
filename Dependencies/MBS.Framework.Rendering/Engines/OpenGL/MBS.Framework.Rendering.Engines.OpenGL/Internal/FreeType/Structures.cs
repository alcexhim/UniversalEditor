//
//  Structures..cs
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

using FT_Long = System.Int64;		// signed long
using FT_UShort = System.UInt16;
using FT_Short = System.Int16;
using FT_Pos = System.Int32;
using FT_Fixed = System.Int32;
using FT_Int = System.Int32;
using FT_UInt = System.UInt32;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeType
{
	internal static class Structures
	{
		public struct FT_CharMap
		{
			public IntPtr face;
			public Constants.FT_Encoding encoding;
			public FT_UShort platform_id;
			public FT_UShort encoding_id;
		}
		public struct FT_BBox
		{
			public FT_Pos xMin, yMin;
			public FT_Pos xMax, yMax;
		}
		public struct FT_Glyph_Metrics
		{
			FT_Pos width;
			FT_Pos height;

			FT_Pos horiBearingX;
			FT_Pos horiBearingY;
			FT_Pos horiAdvance;

			FT_Pos vertBearingX;
			FT_Pos vertBearingY;
			FT_Pos vertAdvance;
		}
		public struct FT_Vector
		{
			public FT_Pos x;
			public FT_Pos y;
		}
		public struct FT_Outline
		{
			short n_contours;      /* number of contours in glyph        */
			short n_points;        /* number of points in the glyph      */

			FT_Vector[] points;          /* the outline's points               */
			byte[] tags;            /* the points flags                   */
			short[] contours;        /* the contour end points             */

			int flags;           /* outline masks */
		}
		public struct FT_Bitmap
		{
			public uint rows;
			public uint width;
			int pitch;
			public IntPtr buffer;
			public ushort num_grays;
			public byte pixel_mode;
			public byte palette_mode;
			public IntPtr palette;
		}
		public struct FT_GlyphSlot
		{
			public IntPtr library;
			public IntPtr face;
			public IntPtr next;
			public uint reserved;       /* retained for binary compatibility */
			public IntPtr generic;

			public FT_Glyph_Metrics metrics;
			public FT_Fixed linearHoriAdvance;
			public FT_Fixed linearVertAdvance;
			public FT_Vector advance;

			public Constants.FT_Glyph_Format format;

			public FT_Bitmap bitmap;
			public FT_Int bitmap_left;
			public FT_Int bitmap_top;

			public IntPtr outline;

			FT_UInt num_subglyphs;
			IntPtr /*FT_SubGlyph*/ subglyphs;

			IntPtr control_data;
			long control_len;

			FT_Pos lsb_delta;
			FT_Pos rsb_delta;

			IntPtr other;

			private IntPtr _internal;
		}
		public struct FT_Size_Metrics
		{
			FT_UShort x_ppem;      /* horizontal pixels per EM               */
			FT_UShort y_ppem;      /* vertical pixels per EM                 */

			FT_Fixed x_scale;     /* scaling values used to convert font    */
			FT_Fixed y_scale;     /* units to 26.6 fractional pixels        */

			FT_Pos ascender;    /* ascender in 26.6 frac. pixels          */
			FT_Pos descender;   /* descender in 26.6 frac. pixels         */
			FT_Pos height;      /* text height in 26.6 frac. pixels       */
			FT_Pos max_advance; /* max horizontal advance, in 26.6 pixels */
		}
		public struct FT_Size
		{
			public IntPtr face;      /* parent face object              */
			IntPtr generic;   /* generic pointer for client uses */
			FT_Size_Metrics metrics;   /* size metrics                    */
			private IntPtr _internal;
		}
		public struct FT_Face
		{
			public FT_Long num_faces;
			public FT_Long face_index;

			public FT_Long face_flags;
			public FT_Long style_flags;

			public FT_Long num_glyphs;

			public string family_name;
			public string style_name;

			public int num_fixed_sizes;
			public IntPtr /*FT_Bitmap_Size[]*/ available_sizes;

			public FT_Int num_charmaps;
			public IntPtr /*FT_CharMap[]*/ charmaps;

			public IntPtr generic;

			/*# The following member variables (down to `underline_thickness') */
			/*# are only relevant to scalable outlines; cf. @FT_Bitmap_Size    */
			/*# for bitmap fonts.                                              */
			public FT_BBox bbox;

			public FT_UShort units_per_EM;
			public FT_Short ascender;
			public FT_Short descender;
			public FT_Short height;

			public FT_Short max_advance_width;
			public FT_Short max_advance_height;

			public FT_Short underline_position;
			public FT_Short underline_thickness;

			public FT_GlyphSlot glyph;
			public FT_Size size;
			public FT_CharMap charmap;

			/*@private begin */

			private IntPtr /* FT_Driver */ driver;
			private IntPtr /* FT_Memory */ memory;
			private IntPtr /* FT_Stream */ stream;

			private IntPtr /* FT_ListRec */ sizes_list;

			private IntPtr /* FT_Generic */ autohint;   /* face-specific auto-hinter data */
			private IntPtr /* void* */ extensions; /* unused                         */

			private IntPtr /* FT_Face_Internal */ _internal;

    		/*@private end */
		}

		public struct FT_Bitmap_Size
		{
			FT_Short height;
			FT_Short width;

			FT_Pos size;

			FT_Pos x_ppem;
			FT_Pos y_ppem;
		}
	}
}
