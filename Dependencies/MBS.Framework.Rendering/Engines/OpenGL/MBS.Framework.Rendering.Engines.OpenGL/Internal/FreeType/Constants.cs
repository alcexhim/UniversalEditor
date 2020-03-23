//
//  Constants.cs
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
namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeType
{
	internal static class Constants
	{
		public enum FT_Error
		{
			None = 0x00,
			Cannot_Open_Resource = 0x01,
			Unknown_File_Format = 0x02,
			Invalid_File_Format = 0x03,
			Invalid_Version = 0x04,
			Lower_Module_Version = 0x05,
			Invalid_Argument = 0x06,
			Unimplemented_Feature = 0x07,
			Invalid_Table = 0x08,
			Invalid_Offset = 0x09,
			Array_Too_Large = 0x0A,
			Missing_Module = 0x0B,
			Missing_Property = 0x0C,

			/* glyph/character errors */

			Invalid_Glyph_Index = 0x10,
			Invalid_Character_Code = 0x11,
			Invalid_Glyph_Format = 0x12,
			Cannot_Render_Glyph = 0x13,
			Invalid_Outline = 0x14,
			Invalid_Composite = 0x15,
			Too_Many_Hints = 0x16,
			Invalid_Pixel_Size = 0x17,

			/* handle errors */

			Invalid_Handle = 0x20,
			Invalid_Library_Handle = 0x21,
			Invalid_Driver_Handle = 0x22,
			Invalid_Face_Handle = 0x23,
			Invalid_Size_Handle = 0x24,
			Invalid_Slot_Handle = 0x25,
			Invalid_CharMap_Handle = 0x26,
			Invalid_Cache_Handle = 0x27,
			Invalid_Stream_Handle = 0x28,

			/* driver errors */

			Too_Many_Drivers = 0x30,
			Too_Many_Extensions = 0x31,

			/* memory errors */

			Out_Of_Memory = 0x40,
			Unlisted_Object = 0x41,

			/* stream errors */

			Cannot_Open_Stream = 0x51,
			Invalid_Stream_Seek = 0x52,
			Invalid_Stream_Skip = 0x53,
			Invalid_Stream_Read = 0x54,
			Invalid_Stream_Operation = 0x55,
			Invalid_Frame_Operation = 0x56,
			Nested_Frame_Access = 0x57,
			Invalid_Frame_Read = 0x58,

			/* raster errors */

			Raster_Uninitialized = 0x60,
			Raster_Corrupted = 0x61,
			Raster_Overflow = 0x62,
			Raster_Negative_Height = 0x63,

			/* cache errors */

			Too_Many_Caches = 0x70,

			/* TrueType and SFNT errors */

			Invalid_Opcode = 0x80,
			Too_Few_Arguments = 0x81,
			Stack_Overflow = 0x82,
			Code_Overflow = 0x83,
			Bad_Argument = 0x84,
			Divide_By_Zero = 0x85,
			Invalid_Reference = 0x86,
			Debug_OpCode = 0x87,
			ENDF_In_Exec_Stream = 0x88,
			Nested_DEFS = 0x89,
			Invalid_CodeRange = 0x8A,
			Execution_Too_Long = 0x8B,
			Too_Many_Function_Defs = 0x8C,
			Too_Many_Instruction_Defs = 0x8D,
			Table_Missing = 0x8E,
			Horiz_Header_Missing = 0x8F,
			Locations_Missing = 0x90,
			Name_Table_Missing = 0x91,
			CMap_Table_Missing = 0x92,
			Hmtx_Table_Missing = 0x93,
			Post_Table_Missing = 0x94,
			Invalid_Horiz_Metrics = 0x95,
			Invalid_CharMap_Format = 0x96,
			Invalid_PPem = 0x97,
			Invalid_Vert_Metrics = 0x98,
			Could_Not_Find_Context = 0x99,
			Invalid_Post_Table_Format = 0x9A,
			Invalid_Post_Table = 0x9B,
			DEF_In_Glyf_Bytecode = 0x9C,
			Missing_Bitmap = 0x9D,

			/* CFF, CID, and Type 1 errors */

			Syntax_Error = 0xA0,
			Stack_Underflow = 0xA1,
			Ignore = 0xA2,
			No_Unicode_Glyph_Name = 0xA3,
			Glyph_Too_Big = 0xA4,

			/* BDF errors */

			Missing_Startfont_Field = 0xB0,
			Missing_Font_Field = 0xB1,
			Missing_Size_Field = 0xB2,
			Missing_Fontboundingbox_Field = 0xB3,
			Missing_Chars_Field = 0xB4,
			Missing_Startchar_Field = 0xB5,
			Missing_Encoding_Field = 0xB6,
			Missing_Bbx_Field = 0xB7,
			Bbx_Too_Big = 0xB8,
			Corrupted_Font_Header = 0xB9,
			Corrupted_Font_Glyphs = 0xBA,
		}
		public enum FT_Load_Flags : int
		{
			Default = 0x0,
			NoScale = (1 << 0),
			NoHinting = (1 << 1),
			Render = (1 << 2),
			NoBitmap = (1 << 3),
			VerticalLayout = (1 << 4),
			ForceAutohint = (1 << 5),
			CropBitmap = (1 << 6),
			Pedantic = (1 << 7),
			IgnoreGlobalAdvanceWidth = (1 << 9),
			NoRecurse = (1 << 10),
			IgnoreTransform = (1 << 11),
			Monochrome = (1 << 12),
			LinearDesign = (1 << 13),
			NoAutohint = (1 << 15),
			/* Bits 16-19 are used by `FT_LOAD_TARGET_' */
			LoadColor = (1 << 20),
			LoadComputeMetrics = (1 << 21),
			LoadBitmapMetricsOnly = (1 << 22),

			/* */

			/* used internally only by certain font drivers */
			LoadAdvanceOnly = (1 << 8),
			LoadSBitsOnly = (1 << 14)
		}
		public enum FT_Encoding : int
		{
			None = 0x00000000,
			MSSymbol = 0x626D7973, // symb
			Unicode = 0x63696E75, // unic
			ShiftJIS = 0x73696A73, // sjis
			PRC = 0x20206267, // 'gb  '
			Big5 = 0x35676962, // big5
			WanSung = 0x736E6177, // wans
			Johab = 0x61686F6A, // joha

			GB2312 = PRC,

			AdobeStandard = 0x424F4441, // ADOB
			AdobeExpert = 0x45424441, // ADBE
			AdobeCustom = 0x43424441, // ADBC
			AdobeLatin1 = 0x3174616C, // lat1
			OldLatin2 = 0x3274616C, // lat2
			AppleRoman = 0x6E6D7261 // armn
		}
		public enum FT_Glyph_Format
		{
			None = 0x00000000,
			Composite = 0x706D6F63,
			Bitmap = 0x73746962,
			Outline = 0x6C74756F,
			Plotter = 0x746F6C70
		}
	}
}
