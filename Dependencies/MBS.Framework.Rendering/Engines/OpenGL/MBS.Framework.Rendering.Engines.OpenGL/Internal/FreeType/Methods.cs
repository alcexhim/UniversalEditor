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
namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeType
{
	internal static class Methods
	{
		public static Constants.FT_Error FT_Init_FreeType(ref IntPtr /*FT_Library*/ alibrary)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_Init_FreeType(ref alibrary);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}

		public static Constants.FT_Error FT_New_Face(IntPtr hFreeType, string filename, int faceIndex, ref IntPtr face)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_New_Face(hFreeType, filename, faceIndex, ref face);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}

		public static Constants.FT_Error FT_Load_Char(IntPtr face, char char_code, Constants.FT_Load_Flags load_flags)
		{
			return FT_Load_Char(face, (ulong)char_code, load_flags);
		}
		public static Constants.FT_Error FT_Load_Char(IntPtr face, ulong char_code, Constants.FT_Load_Flags load_flags)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_Load_Char(face, char_code, load_flags);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}

		public static Constants.FT_Error FT_Set_Pixel_Sizes(IntPtr hFace, uint pixel_width, uint pixel_height)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_Set_Pixel_Sizes(hFace, pixel_width, pixel_height);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}
		public static Constants.FT_Error FT_Done_Face(IntPtr /*FT_Face*/ hFace)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_Done_Face(hFace);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}

		public static Constants.FT_Error FT_Done_FreeType(IntPtr /*FT_Library*/ alibrary)
		{
			Constants.FT_Error err = Constants.FT_Error.None;
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					err = Linux.Methods.FT_Done_FreeType(alibrary);
					break;
				}
				default:
				{
					throw new PlatformNotSupportedException();
				}
			}
			FT_Error_To_Exception(err);
			return err;
		}

		public static void FT_Error_To_Exception(Constants.FT_Error err)
		{
			if (err == Constants.FT_Error.None)
				return;

			switch (err)
			{
				case Constants.FT_Error.Array_Too_Large:
				case Constants.FT_Error.Bad_Argument:
				{
					throw new ArgumentException(err.ToString());
				}
				case Constants.FT_Error.Bbx_Too_Big:
				case Constants.FT_Error.Cannot_Open_Resource:
				case Constants.FT_Error.Cannot_Open_Stream:
				case Constants.FT_Error.Cannot_Render_Glyph:
				case Constants.FT_Error.CMap_Table_Missing:
				default:
				{
					throw new Exception(err.ToString());
				}
			}
		}
	}
}
