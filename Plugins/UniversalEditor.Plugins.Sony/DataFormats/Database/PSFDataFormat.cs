//
//  PSFDataFormat.cs - provides a DataFormat for manipulating Sony PlayStation PSF game information databases
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;

namespace UniversalEditor.Plugins.Sony.DataFormats.Database
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Sony PlayStation PSF game information databases.
	/// </summary>
	public class PSFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(DatabaseObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			DatabaseObjectModel db = (objectModel as DatabaseObjectModel);
			if (db == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string signature = reader.ReadFixedLengthString(4);
			if (signature != "\0PSF")
				throw new InvalidDataFormatException("file does not begin with '\\0PSF'");

			uint unknown1 = reader.ReadUInt32();
			uint stringTableOffset = reader.ReadUInt32();
			uint dataTableOffset = reader.ReadUInt32(); // to "01.00"
			uint columnCount = reader.ReadUInt32();

			PSFColumnInfo[] columnInfos = new PSFColumnInfo[columnCount];
			for (uint i = 0; i < columnCount; i++)
			{
				// each entry is 16 bytes (four x UInt32)
				ushort columnNameOffset = reader.ReadUInt16(); // offset into string table
				PSFColumnDataType columnDataType = (PSFColumnDataType) reader.ReadUInt16();
				uint dataLength = reader.ReadUInt32();
				uint dataMaxLength = reader.ReadUInt32();
				uint valueOffset = reader.ReadUInt32();
				columnInfos[i] = new PSFColumnInfo(columnNameOffset, columnDataType, dataLength, dataMaxLength, valueOffset);
			}

			// now we should be at the string table
			uint stringTableDataLength = dataTableOffset - stringTableOffset;
			byte[] stringTableData = reader.ReadBytes(stringTableDataLength);

			MemoryAccessor maStringTable = new MemoryAccessor(stringTableData);

			DatabaseTable dt = new DatabaseTable();
			DatabaseRecord rec = new DatabaseRecord();

			for (uint i = 0; i < columnCount; i++)
			{
				maStringTable.Seek(columnInfos[i].columnNameOffset, SeekOrigin.Begin);
				string columnName = maStringTable.Reader.ReadNullTerminatedString();
				object value = null;

				reader.Seek(dataTableOffset + columnInfos[i].valueOffset, SeekOrigin.Begin);
				switch (columnInfos[i].columnDataType)
				{
					case PSFColumnDataType.UInt32:
					{
						value = reader.ReadUInt32();
						break;
					}
					case PSFColumnDataType.UTF8S:
					{
						value = reader.ReadFixedLengthString(columnInfos[i].dataLength, Encoding.UTF8);
						break;
					}
					case PSFColumnDataType.UTF8Z:
					{
						value = reader.ReadNullTerminatedString(Encoding.UTF8);
						break;
					}
				}

				dt.Fields.Add(columnName, value);
				rec.Fields.Add(columnName, value);
			}

			dt.Records.Add(rec);
			db.Tables.Add(dt);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			DatabaseObjectModel db = (objectModel as DatabaseObjectModel);
			if (db == null)
				throw new ObjectModelNotSupportedException();
			if (db.Tables.Count != 1)
				throw new ObjectModelNotSupportedException("must have exactly one table to write PSF");

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("\0PSF");

			DatabaseTable dt = db.Tables[0];

			writer.WriteBytes(new byte[] { 01, 01, 00, 00 }); // version

			uint stringTableOffset = (uint)(20 + (16 * dt.Fields.Count));
			stringTableOffset = stringTableOffset.RoundUp(4);
			writer.WriteUInt32(stringTableOffset);

			uint dataTableOffset = stringTableOffset;
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				dataTableOffset += (uint)dt.Fields[i].Name.Length + 1;
			}
			dataTableOffset = dataTableOffset.RoundUp(4);
			writer.WriteUInt32(dataTableOffset);

			writer.WriteUInt32((uint)dt.Fields.Count);

			ushort columnNameOffset = 0;
			uint valueOffset = 0;
			PSFColumnDataType[] columnDataTypes = new PSFColumnDataType[dt.Fields.Count];
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				// each entry is 16 bytes (four x UInt32)
				writer.WriteUInt16(columnNameOffset); // offset into string table

				if (dt.Fields[i].Value is string)
				{
					columnDataTypes[i] = PSFColumnDataType.UTF8Z;
				}
				else if (dt.Fields[i].Value is uint)
				{
					columnDataTypes[i] = PSFColumnDataType.UInt32;
				}
				writer.WriteUInt16((ushort)columnDataTypes[i]);

				uint dataLength = 4;
				int dataMaxLength = GetMaxLengthForField(dt.Fields[i].Name);
				if (dt.Fields[i].Value is string)
				{
					dataLength = (uint)(System.Text.Encoding.UTF8.GetByteCount((string)dt.Fields[i].Value) + 1);
					// dataLength = (uint)(((string)dt.Fields[i].Value).Length + 1);
				}
				else if (dt.Fields[i].Value is uint)
				{
					dataMaxLength = 4;
				}
				writer.WriteUInt32(dataLength);
				writer.WriteInt32(dataMaxLength);
				writer.WriteUInt32(valueOffset);
				if (dt.Fields[i].Value is string)
				{
					valueOffset += (uint) dataMaxLength;
				}
				else if (dt.Fields[i].Value is uint)
				{
					valueOffset += 4;
				}

				columnNameOffset += (ushort)(dt.Fields[i].Name.Length + 1);
			}

			// now we should be at the string table
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				writer.WriteNullTerminatedString(dt.Fields[i].Name);
			}

			// now we should be at the data table
			writer.Align(4);

			for (int i = 0; i < dt.Fields.Count; i++)
			{
				switch (columnDataTypes[i])
				{
					case PSFColumnDataType.UInt32:
					{
						writer.WriteUInt32((uint)(dt.Fields[i].Value));
						break;
					}
					case PSFColumnDataType.UTF8S:
					{
						writer.WriteFixedLengthString((string)dt.Fields[i].Value, Encoding.UTF8, GetMaxLengthForField(dt.Fields[i].Name));
						break;
					}
					case PSFColumnDataType.UTF8Z:
					{
						writer.WriteFixedLengthString((string)dt.Fields[i].Value, Encoding.UTF8, GetMaxLengthForField(dt.Fields[i].Name));
						break;
					}
				}
				writer.Align(4);
			}
		}

		static PSFDataFormat()
		{
			maxLengthsForField.Add("APP_VER", 8);
			maxLengthsForField.Add("ATTRIBUTE", 4);
			maxLengthsForField.Add("BOOTABLE", 4);
			maxLengthsForField.Add("CATEGORY", 4);
			maxLengthsForField.Add("LICENSE", 512);
			maxLengthsForField.Add("PARENTAL_LEVEL", 4);
			maxLengthsForField.Add("PS3_SYSTEM_VER", 8);
			maxLengthsForField.Add("RESOLUTION", 4);
			maxLengthsForField.Add("SOUND_FORMAT", 4);
			maxLengthsForField.Add("TITLE", 128);
			maxLengthsForField.Add("TITLE_ID", 16);
			maxLengthsForField.Add("VERSION", 8);
		}

		private static Dictionary<string, int> maxLengthsForField = new Dictionary<string, int>();
		private static int GetMaxLengthForField(string name)
		{
			if (maxLengthsForField.ContainsKey(name))
				return maxLengthsForField[name];
			return 16;
		}
	}
}
