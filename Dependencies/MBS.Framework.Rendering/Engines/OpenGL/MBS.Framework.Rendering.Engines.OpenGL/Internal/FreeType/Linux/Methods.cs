//
//  Methods.cs
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
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeType.Linux
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "libfreetype";

		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_Init_FreeType(ref IntPtr /*FT_Library*/ alibrary);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_Done_Face(IntPtr /*FT_Face*/ hFace);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_Done_FreeType(IntPtr /*FT_Library*/ library);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_New_Face(IntPtr hFreeType, string filename, int faceIndex, ref IntPtr hFace);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_Set_Pixel_Sizes(IntPtr hFace, uint pixel_width, uint pixel_height);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.FT_Error FT_Load_Char(IntPtr face, ulong char_code, Constants.FT_Load_Flags load_flags);
	}
}
