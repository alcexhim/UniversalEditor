//
//  Marshaller.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Linux
{
	public class Marshaller
	{
		public static void Free(IntPtr ptr)
		{
			GLib.Methods.g_free(ptr);
		}

		public unsafe static IntPtr StringToPtrGStrdup(string str)
		{
			if (str != null)
			{
				fixed (char* native_str = str)
				{
					IntPtr error;
					IntPtr result = GLib.Methods.g_utf16_to_utf8(native_str, new IntPtr(str.Length), IntPtr.Zero, IntPtr.Zero, out error);
					if (error != IntPtr.Zero)
					{
						throw new Exception(error.ToString());
					}
					return result;
				}
			}
			return IntPtr.Zero;
		}

		public unsafe static string Utf8PtrToString(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr items_written = IntPtr.Zero;
			IntPtr error;
			char* value = GLib.Methods.g_utf8_to_utf16(ptr, new IntPtr(-1), IntPtr.Zero, ref items_written, out error);
			if (error != IntPtr.Zero)
			{
				throw new Exception(error.ToString());
			}
			string result = new string(value, 0, (int)items_written);
			GLib.Methods.g_free((IntPtr)(void*)value);
			return result;
		}
	}
}
