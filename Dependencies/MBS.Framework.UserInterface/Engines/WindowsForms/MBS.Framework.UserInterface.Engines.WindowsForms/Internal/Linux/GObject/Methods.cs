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

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Linux.GObject
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "gobject-2.0";

		[DllImport(LIBRARY_FILENAME)]
		public static extern bool g_type_check_instance_is_a(IntPtr instance, IntPtr instance_type);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void g_type_init();

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_init(ref GLib.Structures.Value val, IntPtr gtype);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_boolean(ref GLib.Structures.Value val, bool data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uchar(ref GLib.Structures.Value val, byte data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_char(ref GLib.Structures.Value val, sbyte data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_boxed(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_double(ref GLib.Structures.Value val, double data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_float(ref GLib.Structures.Value val, float data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_int(ref GLib.Structures.Value val, int data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_int64(ref GLib.Structures.Value val, long data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uint64(ref GLib.Structures.Value val, ulong data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_object(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_param(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_pointer(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_string(ref GLib.Structures.Value val, IntPtr data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_uint(ref GLib.Structures.Value val, uint data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_enum(ref GLib.Structures.Value val, int data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_set_flags(ref GLib.Structures.Value val, uint data);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool g_value_get_boolean(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte g_value_get_uchar(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern sbyte g_value_get_char(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_boxed(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern double g_value_get_double(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern float g_value_get_float(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int g_value_get_int(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern long g_value_get_int64(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_value_unset(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong g_value_get_uint64(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_object(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_param(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_pointer(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_value_get_string(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint g_value_get_uint(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int g_value_get_enum(ref GLib.Structures.Value val);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint g_value_get_flags(ref GLib.Structures.Value val);


		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern string g_type_name(IntPtr raw);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_type_from_name(string name);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool g_type_is_a(IntPtr type, IntPtr is_a_type);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_strv_get_type();

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_unref(IntPtr obj);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_set_property(IntPtr /*GObject*/ obj, string property_name, ref GLib.Structures.Value value);
		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void g_object_get_property(IntPtr /*GObject*/ obj, string property_name, ref GLib.Structures.Value value);

		[DllImport(LIBRARY_FILENAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr g_object_new(IntPtr type, string prop1, IntPtr val1, string prop2, int val2, IntPtr zero);
	}
}
