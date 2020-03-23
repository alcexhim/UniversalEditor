//
//  GType.cs
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
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.GTK
{
	public struct GType : IEquatable<GType>
	{
		private IntPtr val;

		public static readonly GType Invalid;

		public static readonly GType None;

		public static readonly GType Interface;

		public static readonly GType Char;

		public static readonly GType UChar;

		public static readonly GType Boolean;

		public static readonly GType Int;

		public static readonly GType UInt;

		public static readonly GType Long;

		public static readonly GType ULong;

		public static readonly GType Int64;

		public static readonly GType UInt64;

		public static readonly GType Enum;

		public static readonly GType Flags;

		public static readonly GType Float;

		public static readonly GType Double;

		public static readonly GType String;

		public static readonly GType Pointer;

		public static readonly GType Boxed;

		public static readonly GType Param;

		public static readonly GType Object;

		private static Dictionary<IntPtr, Type> types;

		private static Dictionary<Type, GType> gtypes;

		public IntPtr Val => val;

		public GType(IntPtr val)
		{
			this.val = val;
		}

		static GType()
		{
			Invalid = new GType((IntPtr)0);
			None = new GType((IntPtr)4);
			Interface = new GType((IntPtr)8);
			Char = new GType((IntPtr)12);
			UChar = new GType((IntPtr)16);
			Boolean = new GType((IntPtr)20);
			Int = new GType((IntPtr)24);
			UInt = new GType((IntPtr)28);
			Long = new GType((IntPtr)32);
			ULong = new GType((IntPtr)36);
			Int64 = new GType((IntPtr)40);
			UInt64 = new GType((IntPtr)44);
			Enum = new GType((IntPtr)48);
			Flags = new GType((IntPtr)52);
			Float = new GType((IntPtr)56);
			Double = new GType((IntPtr)60);
			String = new GType((IntPtr)64);
			Pointer = new GType((IntPtr)68);
			Boxed = new GType((IntPtr)72);
			Param = new GType((IntPtr)76);
			Object = new GType((IntPtr)80);
			types = new Dictionary<IntPtr, Type>(IntPtrEqualityComparer.Instance);
			gtypes = new Dictionary<Type, GType>();


			Engines.GTK.Internal.GObject.Methods.g_type_init();
			Register(Char, typeof(sbyte));
			Register(UChar, typeof(byte));
			Register(Boolean, typeof(bool));
			Register(Int, typeof(int));
			Register(UInt, typeof(uint));
			Register(Int64, typeof(long));
			Register(UInt64, typeof(ulong));
			Register(Float, typeof(float));
			Register(Double, typeof(double));
			Register(String, typeof(string));
			Register(Pointer, typeof(IntPtr));
			Register(Object, typeof(Object));
			gtypes[typeof(char)] = UInt;
		}

		public static GType FromName(string native_name)
		{
			return new GType(Engines.GTK.Internal.GObject.Methods.g_type_from_name(native_name));
		}

		public static void Register(GType native_type, Type type)
		{
			// if (native_type != Pointer && native_type != Boxed && native_type != ManagedValue.GType)
			{
				types[native_type.Val] = type;
			}
			if (type != (Type)null)
			{
				gtypes[type] = native_type;
			}
		}

		public static explicit operator GType(Type type)
		{
			GType value;
			if (gtypes.TryGetValue(type, out value))
			{
				return value;
			}
			/*
			if (type.IsSubclassOf(typeof(Object)))
			{
				value = GLib.Object.LookupGType(type);
				Register(value, type);
				return value;
			}
			GTypeTypeAttribute gTypeTypeAttribute2;
			GTypeAttribute gTypeAttribute;
			if (!type.IsEnum)
			{
				GTypeTypeAttribute gTypeTypeAttribute;
				PropertyInfo property;
				value = (((gTypeTypeAttribute = (GTypeTypeAttribute)Attribute.GetCustomAttribute(type, typeof(GTypeTypeAttribute), false)) != null) ? gTypeTypeAttribute.Type : (((property = type.GetProperty("GType", BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)) != (PropertyInfo)null) ? ((GType)property.GetValue(null, null)) : ((!type.IsSubclassOf(typeof(Opaque))) ? ManagedValue.GType : Pointer)));
			}
			else if ((gTypeTypeAttribute2 = (GTypeTypeAttribute)Attribute.GetCustomAttribute(type, typeof(GTypeTypeAttribute), false)) != null)
			{
				value = gTypeTypeAttribute2.Type;
			}
			else if ((gTypeAttribute = (GTypeAttribute)Attribute.GetCustomAttribute(type, typeof(GTypeAttribute), false)) != null)
			{
				PropertyInfo property2 = gTypeAttribute.WrapperType.GetProperty("GType", BindingFlags.Public | BindingFlags.Static);
				value = (GType)property2.GetValue(null, null);
			}
			else
			{
				value = ManagedValue.GType;
			}
			Register(value, type);
			*/		
			return value;
		}

		private static string GetQualifiedName(string cname)
		{
			for (int i = 1; i < cname.Length; i++)
			{
				if (char.IsUpper(cname[i]))
				{
					if (i == 1 && cname[0] == 'G')
					{
						return "GLib." + cname.Substring(1);
					}
					return cname.Substring(0, i) + "." + cname.Substring(i);
				}
			}
			throw new ArgumentException("cname is not in NamespaceType format. GType.Register should be called directly for " + cname);
		}

		public static explicit operator Type(GType gtype)
		{
			return LookupType(gtype.Val);
		}

		public static void Init()
		{
		}

		public static Type LookupType(IntPtr typeid)
		{
			Type value;
			if (types.TryGetValue(typeid, out value))
			{
				return value;
			}
			string cname = Engines.GTK.Internal.GObject.Methods.g_type_name(typeid);
			string qualifiedName = GetQualifiedName(cname);
			Assembly[] array = (Assembly[])AppDomain.CurrentDomain.GetAssemblies().Clone();
			Assembly[] array2 = array;
			foreach (Assembly assembly in array2)
			{
				value = assembly.GetType(qualifiedName);
				if (value != (Type)null)
				{
					break;
				}
			}
			if (value == (Type)null)
			{
				string text = qualifiedName.Substring(0, qualifiedName.LastIndexOf('.'));
				string b = text.ToLower().Replace('.', '-') + "-sharp";
				Assembly[] array3 = array;
				foreach (Assembly assembly2 in array3)
				{
					AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
					foreach (AssemblyName assemblyName in referencedAssemblies)
					{
						if (!(assemblyName.Name != b))
						{
							try
							{
								string directoryName = System.IO.Path.GetDirectoryName(assembly2.Location);
								Assembly assembly3 = (!System.IO.File.Exists(System.IO.Path.Combine(directoryName, assemblyName.Name + ".dll"))) ? Assembly.Load(assemblyName) : Assembly.LoadFrom(System.IO.Path.Combine(directoryName, assemblyName.Name + ".dll"));
								value = assembly3.GetType(qualifiedName);
								if (value != (Type)null)
								{
									break;
								}
							}
							catch (Exception)
							{
							}
						}
					}
					if (value != (Type)null)
					{
						break;
					}
				}
			}
			Register(new GType(typeid), value);
			return value;
		}

		public override bool Equals(object o)
		{
			if (!(o is GType))
			{
				return false;
			}
			return (GType)o == this;
		}

		public bool Equals(GType other)
		{
			return this == other;
		}

		public static bool operator ==(GType a, GType b)
		{
			return a.Val == b.Val;
		}

		public static bool operator !=(GType a, GType b)
		{
			return a.Val != b.Val;
		}

		public override int GetHashCode()
		{
			return val.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString();

			StringBuilder sb = new StringBuilder();
			sb.Append(Internal.GObject.Methods.g_type_name(val));
			return sb.ToString();
		}

	}
}
