//
//  SummaryInformationDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using MBS.Framework;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.CompoundDocument.SummaryInformation
{
	public class SummaryInformationDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public Endianness Endianness { get; set; } = Endianness.LittleEndian;
		public ushort FormatVersion { get; set; } = 0;
		public uint SystemIdentifier { get; set; } = 0x00020006;
		public Guid ClassIdentifier { get; set; } = Guid.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
			{
				throw new ObjectModelNotSupportedException();
			}

			Reader reader = Accessor.Reader;
			ushort byteOrder = reader.ReadUInt16();
			if (byteOrder == 0xFFFE)
			{
				Endianness = Endianness.LittleEndian;
			}
			else if (byteOrder == 0xFEFF)
			{
				Endianness = Endianness.BigEndian;
			}
			else
			{
				throw new InvalidDataFormatException("file must begin with a valid big-endian or little-endian byte order mark (BOM)");
			}

			FormatVersion = reader.ReadUInt16();
			if (FormatVersion != 0)
			{
				throw new InvalidDataFormatException(String.Format("unsupported format version {0}", FormatVersion));
			}

			SystemIdentifier = reader.ReadUInt32();

			ClassIdentifier = reader.ReadGuid();
			uint propertySetCount = reader.ReadUInt32();

			PropertySetInfo[] propertySetInfos = new PropertySetInfo[propertySetCount];
			Group[] propertySetGroups = new Group[propertySetCount];
			for (uint i = 0; i < propertySetCount; i++)
			{
				PropertySetInfo propertySetInfo = new PropertySetInfo();
				propertySetInfo.Guid = reader.ReadGuid();
				propertySetInfo.Offset = reader.ReadUInt32();
				propertySetInfos[i] = propertySetInfo;

				propertySetGroups[i] = new Group(propertySetInfo.Guid.ToString("B"));
				propertySetGroups[i].SetExtraData<Guid>("guid", propertySetInfo.Guid);
				plom.Items.Add(propertySetGroups[i]);
			}

			for (int i = 0; i < propertySetInfos.Length; i++)
			{
				PropertySetInfo propertySetInfo = propertySetInfos[i];

				// we should be at offset 48, if propertySetCount == 1
				uint propertySetSize = reader.ReadUInt32();
				uint propertySetPropertyCount = reader.ReadUInt32();

				PropertyInfo[] pis = new PropertyInfo[propertySetPropertyCount];
				for (uint j = 0; j < propertySetPropertyCount; j++)
				{
					PropertyInfo pi = new PropertyInfo();
					pi.Identifier = reader.ReadUInt32();
					pi.Offset = reader.ReadUInt32();
					pis[j] = pi;

					Property property = new Property(pi.Identifier.ToString());
					property.SetExtraData<PropertyInfo>("info", pi);
					propertySetGroups[i].Items.Add(property);
				}
			}

			for (int i = 0; i < propertySetInfos.Length; i++)
			{
				IEnumerable<Property> properties = propertySetGroups[i].Items.OfType<Property>();
				foreach (Property property in properties)
				{
					PropertyInfo pi = property.GetExtraData<PropertyInfo>("info");

					Accessor.Seek(propertySetInfos[i].Offset + pi.Offset, SeekOrigin.Begin);

					object value = ReadTypedValue(Accessor.Reader);
					property.Value = value;
				}
			}
		}

		public static object ReadTypedValue(Reader reader)
		{
			PropertySetPropertyType propertyType = (PropertySetPropertyType)reader.ReadUInt16();
			ushort padding = reader.ReadUInt16();
			return ReadTypedValue(reader, propertyType);
		}
		public static object ReadTypedValue(Reader reader, PropertySetPropertyType propertyType)
		{
			object value = null;
			switch (propertyType)
			{
				case PropertySetPropertyType.Empty:
				{
					break;
				}
				case PropertySetPropertyType.Null:
				{
					break;
				}
				case PropertySetPropertyType.I2:
				{
					value = reader.ReadInt16();
					break;
				}
				case PropertySetPropertyType.I4:
				{
					value = reader.ReadInt32();
					break;
				}
				case PropertySetPropertyType.R4:
				{
					value = reader.ReadSingle();
					break;
				}
				case PropertySetPropertyType.R8:
				{
					value = reader.ReadDouble();
					break;
				}
				case PropertySetPropertyType.Currency:
				{
					value = reader.ReadUInt64();
					break;
				}
				case PropertySetPropertyType.Date:
				{
					value = DateTime.FromOADate(reader.ReadDouble());
					break;
				}
				case PropertySetPropertyType.BStr:
				{
					value = ReadCodePageString(reader);
					break;
				}
				case PropertySetPropertyType.Error:
				{
					value = reader.ReadUInt32();
					break;
				}
				case PropertySetPropertyType.Bool:
				{
					value = reader.ReadBoolean();
					break;
				}
				case PropertySetPropertyType.Decimal:
				{
					value = reader.ReadDecimal();
					break;
				}
				case PropertySetPropertyType.I1:
				{
					value = reader.ReadSByte();
					break;
				}
				case PropertySetPropertyType.UI1:
				{
					value = reader.ReadByte();
					break;
				}
				case PropertySetPropertyType.UI2:
				{
					value = reader.ReadUInt16();
					break;
				}
				case PropertySetPropertyType.UI4:
				{
					value = reader.ReadUInt32();
					break;
				}
				case PropertySetPropertyType.I8:
				{
					value = reader.ReadInt64();
					break;
				}
				case PropertySetPropertyType.UI8:
				{
					value = reader.ReadUInt64();
					break;
				}
				case PropertySetPropertyType.Int:
				{
					value = reader.ReadInt32();
					break;
				}
				case PropertySetPropertyType.UInt:
				{
					value = reader.ReadUInt32();
					break;
				}
				case PropertySetPropertyType.LPStr:
				{
					value = ReadCodePageString(reader);
					break;
				}
				case PropertySetPropertyType.LPWStr:
				{
					value = ReadUnicodeString(reader);
					break;
				}
				case PropertySetPropertyType.FileTime:
				{
					uint lowDateTime = reader.ReadUInt32();
					uint highDateTime = reader.ReadUInt32();
					value = null;
					break;
				}
				default:
				{
					break;
				}
			}
			reader.Align(4);
			return value;
		}
		public static void WriteTypedValue(Writer writer, object value)
		{
			if (value is byte)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.UI1);
			}
			else if (value is short)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.I2);
			}
			else if (value is int)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.I4);
			}
			else if (value is long)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.I8);
			}
			else if (value is sbyte)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.I1);
			}
			else if (value is ushort)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.UI2);
			}
			else if (value is uint)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.UI4);
			}
			else if (value is ulong)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.UI8);
			}
			else if (value is string)
			{
				WriteTypedValue(writer, value, PropertySetPropertyType.LPWStr);
			}
			else
			{

			}
		}
		public static void WriteTypedValue(Writer writer, object value, PropertySetPropertyType propertyType)
		{
			writer.WriteUInt32((uint)propertyType);
			switch (propertyType)
			{
				case PropertySetPropertyType.Empty:
				{
					break;
				}
				case PropertySetPropertyType.Null:
				{
					break;
				}
				case PropertySetPropertyType.I2:
				{
					writer.WriteInt16((short)value);
					break;
				}
				case PropertySetPropertyType.I4:
				{
					writer.WriteInt32((int)value);
					break;
				}
				case PropertySetPropertyType.R4:
				{
					writer.WriteSingle((float)value);
					break;
				}
				case PropertySetPropertyType.R8:
				{
					writer.WriteDouble((double)value);
					break;
				}
				case PropertySetPropertyType.Currency:
				{
					writer.WriteUInt64((ulong)value);
					break;
				}
				case PropertySetPropertyType.Date:
				{
					writer.WriteDouble(((DateTime)value).ToOADate());
					break;
				}
				case PropertySetPropertyType.BStr:
				{
					WriteCodePageString(writer, (string)value);
					break;
				}
				case PropertySetPropertyType.Error:
				{
					writer.WriteUInt32((uint)value);
					break;
				}
				case PropertySetPropertyType.Bool:
				{
					writer.WriteBoolean((bool)value);
					break;
				}
				case PropertySetPropertyType.Decimal:
				{
					writer.WriteDecimal((decimal)value);
					break;
				}
				case PropertySetPropertyType.I1:
				{
					writer.WriteSByte((sbyte)value);
					break;
				}
				case PropertySetPropertyType.UI1:
				{
					writer.WriteByte((byte)value);
					break;
				}
				case PropertySetPropertyType.UI2:
				{
					writer.WriteUInt16((ushort)value);
					break;
				}
				case PropertySetPropertyType.UI4:
				{
					writer.WriteUInt32((uint)value);
					break;
				}
				case PropertySetPropertyType.I8:
				{
					writer.WriteInt64((long)value);
					break;
				}
				case PropertySetPropertyType.UI8:
				{
					writer.WriteUInt64((ulong)value);
					break;
				}
				case PropertySetPropertyType.Int:
				{
					writer.WriteInt32((int)value);
					break;
				}
				case PropertySetPropertyType.UInt:
				{
					writer.WriteUInt32((uint)value);
					break;
				}
				case PropertySetPropertyType.LPStr:
				{
					WriteCodePageString(writer, (string)value);
					break;
				}
				case PropertySetPropertyType.LPWStr:
				{
					WriteUnicodeString(writer, (string)value);
					break;
				}
				case PropertySetPropertyType.FileTime:
				{
					uint lowDateTime = 0;
					uint highDateTime = 0;
					writer.WriteUInt32(lowDateTime);
					writer.WriteUInt32(highDateTime);
					break;
				}
				default:
				{
					break;
				}
			}
			writer.Align(4);
		}

		public static string ReadUnicodeString(Reader reader)
		{
			uint length = reader.ReadUInt32();
			return reader.ReadFixedLengthString(length * 2, Encoding.UTF16LittleEndian);
		}
		public static void WriteUnicodeString(Writer writer, string value)
		{
			uint length = (uint)value.Length;
			writer.WriteUInt32(length);
			writer.WriteFixedLengthString(value, Encoding.UTF16LittleEndian);
		}

		public static string ReadCodePageString(Reader reader)
		{
			uint length = reader.ReadUInt32();
			return reader.ReadFixedLengthString(length);
		}
		public static void WriteCodePageString(Writer writer, string value)
		{
			uint length = (uint)value.Length;
			writer.WriteUInt32(length);
			writer.WriteFixedLengthString(value);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PropertyListObjectModel plom = (objectModel as PropertyListObjectModel);
			if (plom == null)
			{
				throw new ObjectModelNotSupportedException();
			}

			switch (Endianness)
			{
				case Endianness.LittleEndian:
				{
					Accessor.Writer.WriteUInt16(0xFFFE);
					break;
				}
				case Endianness.BigEndian:
				{
					Accessor.Writer.WriteUInt16(0xFEFF);
					break;
				}
				default:
				{
					throw new NotSupportedException();
				}
			}

			Accessor.Writer.WriteUInt16(FormatVersion);
			Accessor.Writer.WriteUInt32(SystemIdentifier);

			Accessor.Writer.WriteGuid(ClassIdentifier);

			IEnumerable<Group> groups = plom.Items.OfType<Group>();
			Accessor.Writer.WriteUInt32((uint)groups.Count());
			uint offset = (uint)(28 + (20 * groups.Count()));

			foreach (Group group in groups)
			{
				Accessor.Writer.WriteGuid(group.GetExtraData<Guid>("guid"));
				Accessor.Writer.WriteUInt32(offset);

				IEnumerable<Property> properties = group.Items.OfType<Property>();
				offset += (uint)(8 + (8 * properties.Count()));
			}

			offset -= (uint) Accessor.Position;

			foreach (Group group in groups)
			{
				IEnumerable<Property> properties = group.Items.OfType<Property>();

				uint propertySetSize = (uint)(8 + (8 * properties.Count()));
				foreach (Property property in properties)
				{
					propertySetSize += CalculatePropertySize(property);
				}
				Accessor.Writer.WriteUInt32(propertySetSize);
				Accessor.Writer.WriteUInt32((uint)properties.Count());

				foreach (Property property in properties)
				{
					uint identifier = 0;
					if (!UInt32.TryParse(property.Name, out identifier))
					{
						identifier = 0;
					}
					Accessor.Writer.WriteUInt32(identifier);

					Accessor.Writer.WriteUInt32(offset);
					offset += CalculatePropertySize(property);
				}
			}

			foreach (Group group in groups)
			{
				IEnumerable<Property> properties = group.Items.OfType<Property>();

				foreach (Property property in properties)
				{
					WriteTypedValue(Accessor.Writer, property.Value);
				}
			}
		}

		private uint CalculatePropertySize(Property property)
		{
			uint size = 4;
			if (property.Value != null)
			{
				if (property.Value is string)
				{
					size += (uint)(4 + ((string)property.Value).Length).Align(4);
				}
				size += (uint)property.Value.SizeOf().Align(4);
			}
			return size;
		}
	}
}
