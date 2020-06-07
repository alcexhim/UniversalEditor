//
//  UniversalPropertyListDataFormat.cs - provides a DataFormat for manipulating property lists in Universal Editor's Universal Property List format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using System.Linq;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.UniversalPropertyList
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating property lists in Universal Editor's Universal Property List format.
	/// </summary>
	public class UniversalPropertyListDataFormat : DataFormat
	{
		private const int HEADER_SIZE = 20;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(FormatVersion), "Format version:", true, new CustomOptionFieldChoice("1.0", 1.0f)));
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the format version for this <see cref="UniversalPropertyListDataFormat" />.
		/// </summary>
		/// <value>The format version for this <see cref="UniversalPropertyListDataFormat" />.</value>
		public float FormatVersion { get; set; } = 1.0f;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string UPLF = br.ReadFixedLengthString(4);
			if (UPLF != "UPLF") throw new InvalidDataFormatException();

			FormatVersion = br.ReadSingle();

			int filesize = br.ReadInt32();
			plom.Title = br.ReadNullTerminatedString();

			int propertyCount = br.ReadInt32();
			int groupCount = br.ReadInt32();

			for (int i = 0; i < propertyCount; i++)
			{
				Property property = ReadProperty(br);
				plom.Items.Add(property);
			}
			for (int i = 0; i < groupCount; i++)
			{
				Group group = ReadGroup(br);
				plom.Items.Add(group);
			}
		}

		private Property ReadProperty(Reader br)
		{
			Property p = new Property();

			VariantType propertyType = (VariantType)br.ReadByte();
			p.Name = br.ReadNullTerminatedString();
			p.Value = ReadVariant(br, propertyType);
			return p;
		}

		private object ReadVariant(Reader br, VariantType variantType)
		{
			switch (variantType)
			{
				case VariantType.Array: return ReadVariantArray(br);
				case VariantType.Boolean: return br.ReadBoolean();
				case VariantType.Byte: return br.ReadByte();
				case VariantType.Char: return br.ReadChar();
				case VariantType.DateTime: return br.ReadDateTime();
				case VariantType.Decimal: return br.ReadDecimal();
				case VariantType.Double: return br.ReadDouble();
				case VariantType.Guid: return br.ReadGuid();
				case VariantType.Int16: return br.ReadInt16();
				case VariantType.Int32: return br.ReadInt32();
				case VariantType.Int64: return br.ReadInt64();
				case VariantType.Object: throw new NotImplementedException();
				case VariantType.SByte: return br.ReadSByte();
				case VariantType.Single: return br.ReadSingle();
				case VariantType.String: return br.ReadNullTerminatedString();
				case VariantType.UInt16: return br.ReadUInt16();
				case VariantType.UInt32: return br.ReadUInt32();
				case VariantType.UInt64: return br.ReadUInt64();
			}
			return null;
		}
		private object ReadVariantArray(Reader br)
		{
			VariantType arrayItemType = (VariantType)br.ReadByte();
			int arrayItemCount = br.ReadInt32();

			switch (arrayItemType)
			{
				case VariantType.Array:
				{
					object[] items = new object[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = ReadVariantArray(br);
					return items;
				}
				case VariantType.Boolean:
				{
					bool[] items = new bool[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadBoolean();
					return items;
				}
				case VariantType.Byte:
				{
					byte[] items = new byte[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadByte();
					return items;
				}
				case VariantType.Char:
				{
					char[] items = new char[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadChar();
					return items;
				}
				case VariantType.DateTime:
				{
					DateTime[] items = new DateTime[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadDateTime();
					return items;
				}
				case VariantType.Decimal:
				{
					decimal[] items = new decimal[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadDecimal();
					return items;
				}
				case VariantType.Double:
				{
					double[] items = new double[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadDouble();
					return items;
				}
				case VariantType.Guid:
				{
					Guid[] items = new Guid[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadGuid();
					return items;
				}
				case VariantType.Int16:
				{
					short[] items = new short[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadInt16();
					return items;
				}
				case VariantType.Int32:
				{
					int[] items = new int[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadInt32();
					return items;
				}
				case VariantType.Int64:
				{
					long[] items = new long[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadInt64();
					return items;
				}
				case VariantType.Object:
				{
					throw new NotImplementedException();
				}
				case VariantType.SByte:
				{
					sbyte[] items = new sbyte[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadSByte();
					return items;
				}
				case VariantType.Single:
				{
					float[] items = new float[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadSingle();
					return items;
				}
				case VariantType.UInt16:
				{
					ushort[] items = new ushort[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadUInt16();
					return items;
				}
				case VariantType.UInt32:
				{
					uint[] items = new uint[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadUInt32();
					return items;
				}
				case VariantType.UInt64:
				{
					ulong[] items = new ulong[arrayItemCount];
					for (int i = 0; i < arrayItemCount; i++) items[i] = br.ReadUInt64();
					return items;
				}
			}
			return null;
		}

		private Group ReadGroup(Reader br)
		{
			Group group = new Group();
			group.Name = br.ReadNullTerminatedString();

			int propertyCount = br.ReadInt32();
			int groupCount = br.ReadInt32();

			for (int i = 0; i < propertyCount; i++)
			{
				Property p = ReadProperty(br);
				group.Items.Add(p);
			}
			for (int i = 0; i < groupCount; i++)
			{
				Group g = ReadGroup(br);
				group.Items.Add(g);
			}
			return group;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null) throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("UPLF");
			bw.WriteSingle(FormatVersion);

			int filesize = 0;
			filesize += HEADER_SIZE;
			filesize += (plom.Title.Length + 1);
			foreach (PropertyListItem p in plom.Items)
			{
				if (p is Property)
				{
					filesize += GetPropertySize(p as Property);
				}
				else if (p is Group)
				{
					filesize += GetGroupSize(p as Group);
				}
			}

			bw.WriteInt32(filesize);
			bw.WriteNullTerminatedString(plom.Title);

			IEnumerable<Property> properties = plom.Items.OfType<Property>();
			IEnumerable<Group> groups = plom.Items.OfType<Group>();
			bw.WriteInt32(properties.Count());
			bw.WriteInt32(groups.Count());

			foreach (Property p in properties)
			{
				WriteProperty(bw, p);
			}
			foreach (Group g in groups)
			{
				WriteGroup(bw, g);
			}
		}

		private int GetPropertySize(Property p)
		{
			int filesize = 0;
			filesize += (1 + p.Name.Length + 1);
			filesize += GetVariantSize(p.Value);
			return filesize;
		}
		private int GetGroupSize(Group g)
		{
			int groupsize = 0;
			groupsize += (g.Name.Length + 1) + 8;
			foreach (PropertyListItem item in g.Items)
			{
				if (item is Property) groupsize += GetPropertySize(item as Property);
				if (item is Group) groupsize += GetGroupSize(item as Group);
			}
			return groupsize;
		}

		private int GetVariantSize(object value)
		{
			int filesize = 0;
			switch (GetVariantType(value))
			{
				case VariantType.Array:
				{
					Array array = (value as Array);
					filesize += 5;
					for (int i = 0; i < array.Length; i++)
					{
						filesize += GetVariantSize(array.GetValue(i));
					}
					break;
				}
				case VariantType.Boolean:
				case VariantType.Byte:
				case VariantType.Char:
				case VariantType.SByte:
				{
					filesize += 1;
					break;
				}
				case VariantType.Int16:
				case VariantType.UInt16:
				{
					filesize += 2;
					break;
				}
				case VariantType.DateTime:
				case VariantType.Int32:
				case VariantType.Single:
				case VariantType.UInt32:
				{
					filesize += 4;
					break;
				}
				case VariantType.Int64:
				case VariantType.UInt64:
				case VariantType.Double:
				{
					filesize += 8;
					break;
				}
				case VariantType.String:
				{
					filesize += (value as string).Length + 1;
					break;
				}
			}
			return filesize;
		}

		private void WriteProperty(Writer bw, Property p)
		{
			VariantType propertyType = GetVariantType(p.Value);
			bw.WriteByte((byte)propertyType);
			bw.WriteNullTerminatedString(p.Name);
			WriteVariant(bw, propertyType, p.Value);
		}

		private void WriteGroup(Writer bw, Group g)
		{
			bw.WriteNullTerminatedString(g.Name);

			IEnumerable<Property> properties = g.Items.OfType<Property>();
			bw.WriteInt32(properties.Count());

			IEnumerable<Group> groups = g.Items.OfType<Group>();
			bw.WriteInt32(groups.Count());

			foreach (Property p1 in properties)
			{
				WriteProperty(bw, p1);
			}
			foreach (Group g1 in groups)
			{
				WriteGroup(bw, g1);
			}
		}

		private void WriteVariant(Writer bw, VariantType variantType, object p)
		{
			switch (variantType)
			{
				case VariantType.Array: WriteVariantArray(bw, GetVariantType(p), p); break;
				case VariantType.Boolean: bw.WriteBoolean((bool)p); break;
				case VariantType.Byte: bw.WriteByte((byte)p); break;
				case VariantType.Char: bw.WriteChar((char)p); break;
				case VariantType.DateTime: bw.WriteDateTime((DateTime)p); break;
				case VariantType.Decimal: bw.WriteDecimal((decimal)p); break;
				case VariantType.Double: bw.WriteDouble((double)p); break;
				case VariantType.Guid: bw.WriteGuid((Guid)p); break;
				case VariantType.Int16: bw.WriteInt16((short)p); break;
				case VariantType.Int32: bw.WriteInt32((int)p); break;
				case VariantType.Int64: bw.WriteInt64((long)p); break;
				case VariantType.Object: throw new NotImplementedException();
				case VariantType.SByte: bw.WriteSByte((sbyte)p); break;
				case VariantType.Single: bw.WriteSingle((float)p); break;
				case VariantType.String: bw.WriteNullTerminatedString((string)p); break;
				case VariantType.UInt16: bw.WriteUInt16((ushort)p); break;
				case VariantType.UInt32: bw.WriteUInt32((uint)p); break;
				case VariantType.UInt64: bw.WriteUInt64((ulong)p); break;
			}
		}

		private void WriteVariantArray(Writer bw, VariantType arrayItemType, object p)
		{
			Array pa = (p as Array);
			bw.WriteByte((byte)arrayItemType);
			bw.WriteInt32(pa.Length);
			for (int i = 0; i < pa.Length; i++)
			{
				WriteVariant(bw, arrayItemType, pa.GetValue(i));
			}
		}

		private VariantType GetVariantType(object p)
		{
			if (p == null) return VariantType.Null;
			if (p is Array) return VariantType.Array;

			if (p is Boolean) return VariantType.Boolean;
			if (p is Byte) return VariantType.Byte;
			if (p is Char) return VariantType.Char;
			if (p is DateTime) return VariantType.DateTime;
			if (p is Decimal) return VariantType.Decimal;
			if (p is Double) return VariantType.Double;
			if (p is Guid) return VariantType.Guid;
			if (p is Int16) return VariantType.Int16;
			if (p is Int32) return VariantType.Int32;
			if (p is Int64) return VariantType.Int64;
			if (p is SByte) return VariantType.SByte;
			if (p is Single) return VariantType.Single;
			if (p is String) return VariantType.String;
			if (p is UInt16) return VariantType.UInt16;
			if (p is UInt32) return VariantType.UInt32;
			if (p is UInt64) return VariantType.UInt64;

			return VariantType.Object;
		}
	}
}