//
//  Structures.cs
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

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Linux.GLib
{
	internal static class Structures
	{
		public struct GError
		{
			uint /*GQuark*/       domain;
			int         code;
			IntPtr /*gchar       */ message;
		}
		public struct Value : IDisposable
		{
			public void Dispose()
			{
				if (type != IntPtr.Zero)
				{
					GObject.Methods.g_value_unset(ref this);
				}
			}

			private IntPtr type;

			private long pad_1;

			private long pad_2;

			public static Value Empty;
			public static explicit operator bool(Value val)
			{
				return GObject.Methods.g_value_get_boolean(ref val);
			}


			public static explicit operator Enum(Value val)
			{
				if (glibsharp_value_holds_flags(ref val))
				{
					return (Enum)Enum.ToObject(Constants.GType.LookupType(val.type), GObject.Methods.g_value_get_flags(ref val));
				}
				return (Enum)Enum.ToObject(Constants.GType.LookupType(val.type), GObject.Methods.g_value_get_enum(ref val));
			}


			public object Val
			{
				get
				{
					if (type == Constants.GType.Boolean.Val)
					{
						return (bool)this;
					}
					if (type == Constants.GType.UChar.Val)
					{
						return (byte)this;
					}
					if (type == Constants.GType.Char.Val)
					{
						return (sbyte)this;
					}
					if (type == Constants.GType.Int.Val)
					{
						return (int)this;
					}
					if (type == Constants.GType.UInt.Val)
					{
						return (uint)this;
					}
					if (type == Constants.GType.Int64.Val)
					{
						return (long)this;
					}
					if (type == Constants.GType.UInt64.Val)
					{
						return (ulong)this;
					}
					if (GObject.Methods.g_type_is_a(type, Constants.GType.Enum.Val) || GObject.Methods.g_type_is_a(type, Constants.GType.Flags.Val))
					{
						return (Enum)this;
					}
					if (type == Constants.GType.Float.Val)
					{
						return (float)this;
					}
					if (type == Constants.GType.Double.Val)
					{
						return (double)this;
					}
					if (type == Constants.GType.String.Val)
					{
						return (string)this;
					}
					if (type == Constants.GType.Pointer.Val)
					{
						return (IntPtr)this;
					}
					if (type == Constants.GType.Param.Val)
					{
						return GObject.Methods.g_value_get_param(ref this);
					}
					if (GObject.Methods.g_type_is_a(type, Constants.GType.Object.Val))
					{
						return (Object)this;
					}
					if (GObject.Methods.g_type_is_a(type, Constants.GType.Boxed.Val))
					{
						return ToBoxed();
					}
					if (type == IntPtr.Zero)
					{
						return null;
					}
					throw new Exception("Unknown type " + new Constants.GType(type).ToString());
				}
				set
				{
					if (type == Constants.GType.Boolean.Val)
					{
						GObject.Methods.g_value_set_boolean(ref this, (bool)value);
					}
					else if (type == Constants.GType.UChar.Val)
					{
						GObject.Methods.g_value_set_uchar(ref this, (byte)value);
					}
					else if (type == Constants.GType.Char.Val)
					{
						GObject.Methods.g_value_set_char(ref this, (sbyte)value);
					}
					else if (type == Constants.GType.Int.Val)
					{
						GObject.Methods.g_value_set_int(ref this, (int)value);
					}
					else if (type == Constants.GType.UInt.Val)
					{
						GObject.Methods.g_value_set_uint(ref this, (uint)value);
					}
					else if (type == Constants.GType.Int64.Val)
					{
						GObject.Methods.g_value_set_int64(ref this, (long)value);
					}
					else if (type == Constants.GType.UInt64.Val)
					{
						GObject.Methods.g_value_set_uint64(ref this, (ulong)value);
					}
					else if (GObject.Methods.g_type_is_a(type, Constants.GType.Enum.Val))
					{
						GObject.Methods.g_value_set_enum(ref this, (int)value);
					}
					else if (GObject.Methods.g_type_is_a(type, Constants.GType.Flags.Val))
					{
						GObject.Methods.g_value_set_flags(ref this, (uint)(int)value);
					}
					else if (type == Constants.GType.Float.Val)
					{
						GObject.Methods.g_value_set_float(ref this, (float)value);
					}
					else if (type == Constants.GType.Double.Val)
					{
						GObject.Methods.g_value_set_double(ref this, (double)value);
					}
					else if (type == Constants.GType.String.Val)
					{
						IntPtr intPtr = Marshaller.StringToPtrGStrdup((string)value);
						GObject.Methods.g_value_set_string(ref this, intPtr);
						Marshaller.Free(intPtr);
					}
					else if (type == Constants.GType.Pointer.Val)
					{
						if (value is IntPtr)
						{
							GObject.Methods.g_value_set_pointer(ref this, (IntPtr)value);
						}
						/*
						else if (value is IWrapper)
						{
							GObject.Methods.g_value_set_pointer(ref this, ((IWrapper)value).Handle);
						}
						else
						{
							IntPtr intPtr2 = ManagedValue.WrapObject(value);
							if (type != IntPtr.Zero)
							{
								g_value_unset(ref this);
							}
							g_value_init(ref this, ManagedValue.GType.Val);
							GObject.Methods.g_value_set_boxed(ref this, intPtr2);
							ManagedValue.ReleaseWrapper(intPtr2);
						}
						*/
					}
					else if (type == Constants.GType.Param.Val)
					{
						GObject.Methods.g_value_set_param(ref this, (IntPtr)value);
					}
					/*
					else if (type == ManagedValue.GType.Val)
					{
						IntPtr intPtr3 = ManagedValue.WrapObject(value);
						GObject.Methods.g_value_set_boxed(ref this, intPtr3);
						ManagedValue.ReleaseWrapper(intPtr3);
					}
					else if (g_type_is_a(type, GType.Object.Val))
					{
						if (value is Object)
						{
							GObject.Methods.g_value_set_object(ref this, (value as Object).Handle);
						}
						else
						{
							GObject.Methods.g_value_set_object(ref this, (value as GInterfaceAdapter).Handle);
						}
					}
					else
					{
						if (!g_type_is_a(type, GType.Boxed.Val))
						{
							throw new Exception("Unknown type " + new GType(type).ToString());
						}
						if (value is IWrapper)
						{
							GObject.Methods.g_value_set_boxed(ref this, ((IWrapper)value).Handle);
						}
						else
						{
							IntPtr intPtr4 = Marshaller.StructureToPtrAlloc(value);
							GObject.Methods.g_value_set_boxed(ref this, intPtr4);
							Marshal.FreeHGlobal(intPtr4);
						}
					}
					*/
				}
			}

			/// <summary>
			/// Creates a new <see cref="Value" /> by casting <see cref="value" /> to the appropriate <see cref="Type"/>.
			/// </summary>
			/// <returns>The newly-created <see cref="Value" />.</returns>
			/// <param name="value">Value to cast.</param>
			public static Value FromObject(object value)
			{
				Value val;
				if (value == null)
				{
					val = new Value();
				}
				else if (value is string)
				{
					val = new Value((string)value);
				}
				else if (value is int)
				{
					val = new Value((int)value);
				}
				else if (value is long)
				{
					val = new Value((long)value);
				}
				else
				{
					val = new Value(value.ToString());
				}
				return val;
			}

			public Value(Constants.GType gtype)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				GObject.Methods.g_value_init(ref this, gtype.Val);
			}

			public Value(object obj)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				GObject.Methods.g_value_init(ref this, ((Constants.GType)obj.GetType()).Val);
				Val = obj;
			}

			public Value(bool val)
			{
				this = new Value(Constants.GType.Boolean);
				GObject.Methods.g_value_set_boolean(ref this, val);
			}

			public Value(byte val)
			{
				this = new Value(Constants.GType.UChar);
				GObject.Methods.g_value_set_uchar(ref this, val);
			}

			public Value(sbyte val)
			{
				this = new Value(Constants.GType.Char);
				GObject.Methods.g_value_set_char(ref this, val);
			}

			public Value(int val)
			{
				this = new Value(Constants.GType.Int);
				GObject.Methods.g_value_set_int(ref this, val);
			}

			public Value(uint val)
			{
				this = new Value(Constants.GType.UInt);
				GObject.Methods.g_value_set_uint(ref this, val);
			}

			public Value(ushort val)
			{
				this = new Value(Constants.GType.UInt);
				GObject.Methods.g_value_set_uint(ref this, val);
			}

			public Value(long val)
			{
				this = new Value(Constants.GType.Int64);
				GObject.Methods.g_value_set_int64(ref this, val);
			}

			public Value(ulong val)
			{
				this = new Value(Constants.GType.UInt64);
				GObject.Methods.g_value_set_uint64(ref this, val);
			}

			/*
			[Obsolete("Replaced by Value(object) constructor")]
			public Value(EnumWrapper wrap, string type_name)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(type_name);
				gtksharp_value_create_from_type_name(ref this, intPtr);
				Marshaller.Free(intPtr);
				if (wrap.flags)
				{
					GObject.Methods.g_value_set_flags(ref this, (uint)(int)wrap);
				}
				else
				{
					GObject.Methods.g_value_set_enum(ref this, (int)wrap);
				}
			}
			*/

			public Value(float val)
			{
				this = new Value(Constants.GType.Float);
				GObject.Methods.g_value_set_float(ref this, val);
			}

			public Value(double val)
			{
				this = new Value(Constants.GType.Double);
				GObject.Methods.g_value_set_double(ref this, val);
			}

			public Value(string val)
			{
				this = new Value(Constants.GType.String);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(val);
				GObject.Methods.g_value_set_string(ref this, intPtr);
				Marshaller.Free(intPtr);
			}

			public Value(IntPtr val)
			{
				this = new Value(Constants.GType.Pointer);
				GObject.Methods.g_value_set_pointer(ref this, val);
			}
			/*
			public Value(Opaque val, string type_name)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(type_name);
				gtksharp_value_create_from_type_name(ref this, intPtr);
				Marshaller.Free(intPtr);
				GObject.Methods.g_value_set_boxed(ref this, val.Handle);
			}
			public Value(Object val)
			{
				this = new Value(val?.NativeType ?? GType.Object);
				GObject.Methods.g_value_set_object(ref this, val?.Handle ?? IntPtr.Zero);
			}

			public Value(GInterfaceAdapter val)
			{
				this = new Value(val?.GType ?? GType.Object);
				GObject.Methods.g_value_set_object(ref this, val?.Handle ?? IntPtr.Zero);
			}
			public Value(Object obj, string prop_name)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(prop_name);
				gtksharp_value_create_from_property(ref this, obj.Handle, intPtr);
				Marshaller.Free(intPtr);
			}

			internal Value(Object obj, IntPtr prop)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				gtksharp_value_create_from_property(ref this, obj.Handle, prop);
			}

			[Obsolete]
			public Value(Object obj, string prop_name, EnumWrapper wrap)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(prop_name);
				gtksharp_value_create_from_type_and_property(ref this, obj.NativeType.Val, intPtr);
				Marshaller.Free(intPtr);
				if (wrap.flags)
				{
					GObject.Methods.g_value_set_flags(ref this, (uint)(int)wrap);
				}
				else
				{
					GObject.Methods.g_value_set_enum(ref this, (int)wrap);
				}
			}

			[Obsolete]
			public Value(IntPtr obj, string prop_name, Opaque val)
			{
				type = IntPtr.Zero;
				pad_1 = (pad_2 = 0L);
				IntPtr intPtr = Marshaller.StringToPtrGStrdup(prop_name);
				gtksharp_value_create_from_property(ref this, obj, intPtr);
				Marshaller.Free(intPtr);
				GObject.Methods.g_value_set_boxed(ref this, val.Handle);
			}

			public Value(string[] val)
			{
				this = new Value(new GType(g_strv_get_type()));
				if (val == null)
				{
					GObject.Methods.g_value_set_boxed(ref this, IntPtr.Zero);
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal((val.Length + 1) * IntPtr.Size);
					for (int i = 0; i < val.Length; i++)
					{
						Marshal.WriteIntPtr(intPtr, i * IntPtr.Size, Marshaller.StringToPtrGStrdup(val[i]));
					}
					Marshal.WriteIntPtr(intPtr, val.Length * IntPtr.Size, IntPtr.Zero);
					GObject.Methods.g_value_set_boxed(ref this, intPtr);
					for (int j = 0; j < val.Length; j++)
					{
						Marshaller.Free(Marshal.ReadIntPtr(intPtr, j * IntPtr.Size));
					}
					Marshal.FreeHGlobal(intPtr);
				}
			}

			public void Dispose()
			{
				if (type != IntPtr.Zero)
				{
					g_value_unset(ref this);
				}
			}

			public void Init(GType gtype)
			{
				g_value_init(ref this, gtype.Val);
			}

			public static explicit operator bool(Value val)
			{
				return g_value_get_boolean(ref val);
			}

			public static explicit operator byte(Value val)
			{
				return g_value_get_uchar(ref val);
			}

			public static explicit operator sbyte(Value val)
			{
				return g_value_get_char(ref val);
			}

			public static explicit operator int(Value val)
			{
				return g_value_get_int(ref val);
			}

			public static explicit operator uint(Value val)
			{
				return g_value_get_uint(ref val);
			}

			public static explicit operator ushort(Value val)
			{
				return (ushort)g_value_get_uint(ref val);
			}

			public static explicit operator long(Value val)
			{
				return g_value_get_int64(ref val);
			}

			public static explicit operator ulong(Value val)
			{
				return g_value_get_uint64(ref val);
			}

			/*
			public static explicit operator EnumWrapper(Value val)
			{
				if (glibsharp_value_holds_flags(ref val))
				{
					return new EnumWrapper((int)g_value_get_flags(ref val), true);
				}
				return new EnumWrapper(g_value_get_enum(ref val), false);
			}

			public static explicit operator Enum(Value val)
			{
				if (glibsharp_value_holds_flags(ref val))
				{
					return (Enum)Enum.ToObject(GType.LookupType(val.type), g_value_get_flags(ref val));
				}
				return (Enum)Enum.ToObject(GType.LookupType(val.type), g_value_get_enum(ref val));
			}
			*/

			public static explicit operator float(Value val)
			{
				return GObject.Methods.g_value_get_float(ref val);
			}

			public static explicit operator double(Value val)
			{
				return GObject.Methods.g_value_get_double(ref val);
			}

			public static explicit operator string(Value val)
			{
				IntPtr intPtr = GObject.Methods.g_value_get_string(ref val);
				return (!(intPtr == IntPtr.Zero)) ? Marshaller.Utf8PtrToString(intPtr) : null;
			}

			public static explicit operator IntPtr(Value val)
			{
				return GObject.Methods.g_value_get_pointer(ref val);
			}

			/*
			public static explicit operator Opaque(Value val)
			{
				return Opaque.GetOpaque(g_value_get_boxed(ref val), (Type)new GType(val.type), false);
			}

			public static explicit operator Boxed(Value val)
			{
				return new Boxed(g_value_get_boxed(ref val));
			}

			public static explicit operator Object(Value val)
			{
				return Object.GetObject(g_value_get_object(ref val), false);
			}

			public static explicit operator UnwrappedObject(Value val)
			{
				return new UnwrappedObject(g_value_get_object(ref val));
			}
			*/


			public static explicit operator string[] (Value val)
			{
				IntPtr intPtr = GObject.Methods.g_value_get_boxed(ref val);
				if (intPtr == IntPtr.Zero)
				{
					return null;
				}
				int i;
				for (i = 0; Marshal.ReadIntPtr(intPtr, i * IntPtr.Size) != IntPtr.Zero; i++)
				{
				}
				string[] array = new string[i];
				for (int j = 0; j < i; j++)
				{
					array[j] = Marshaller.Utf8PtrToString(Marshal.ReadIntPtr(intPtr, j * IntPtr.Size));
				}
				return array;
			}

			private object ToBoxed()
			{
				IntPtr o = GObject.Methods.g_value_get_boxed(ref this);
				Type type = Constants.GType.LookupType(this.type);
				if (type == (Type)null)
				{
					throw new Exception("Unknown type " + new Constants.GType(this.type).ToString());
				}
				/*
				if (type.IsSubclassOf(typeof(Opaque)))
				{
					return (Opaque)this;
				}
				*/
				return null; // FastActivator.CreateBoxed(o, type);
			}

			internal void Update(object val)
			{
				if (GObject.Methods.g_type_is_a(type, Constants.GType.Boxed.Val) /* && !(val is IWrapper)*/)
				{
					Marshal.StructureToPtr(val, GObject.Methods.g_value_get_boxed(ref this), false);
				}
			}

			[DllImport("glibsharpglue-2", CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr gtksharp_value_create_from_property(ref Value val, IntPtr obj, IntPtr name);

			[DllImport("glibsharpglue-2", CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr gtksharp_value_create_from_type_and_property(ref Value val, IntPtr gtype, IntPtr name);

			[DllImport("glibsharpglue-2", CallingConvention = CallingConvention.Cdecl)]
			private static extern IntPtr gtksharp_value_create_from_type_name(ref Value val, IntPtr type_name);


			[DllImport("glibsharpglue-2", CallingConvention = CallingConvention.Cdecl)]
			private static extern bool glibsharp_value_holds_flags(ref Value val);
		}
	}
}
