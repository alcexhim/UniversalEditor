//
//  UTFDataFormat.cs
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK.UTF
{
	public class UTFDataFormat : DataFormat
	{

		private struct UTFTABLEINFO
		{
			public long utfOffset;
			public int tableSize;
			public int schemaOffset;
			public int rowsOffset;
			public int stringTableOffset;
			public int dataOffset;
			public uint tableNameStringOffset;
			public short tableColumns;
			public short rowWidth;
			public int tableRows;
			public int stringTableSize;
		}

		private UTFTABLEINFO ReadUTFTableInfo(IO.Reader br)
		{
			UTFTABLEINFO info = new UTFTABLEINFO();
			info.utfOffset = Accessor.Position;
			info.tableSize = br.ReadInt32();
			info.schemaOffset = 0x20;
			info.rowsOffset = br.ReadInt32();
			info.stringTableOffset = br.ReadInt32();
			info.dataOffset = br.ReadInt32();

			// CPK Header & UTF Header are ignored, so add 8 to each offset

			info.tableNameStringOffset = br.ReadUInt32(); // 00000007
			info.tableColumns = br.ReadInt16(); // 0023
			info.rowWidth = br.ReadInt16(); // 007e
			info.tableRows = br.ReadInt32(); // 00000001
			info.stringTableSize = info.dataOffset - info.stringTableOffset;
			return info;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader br = base.Accessor.Reader;
			string utf_signature = br.ReadFixedLengthString(4);
			if (utf_signature != "@UTF")
				throw new InvalidDataFormatException(); // we are assuming passed in decrypted UTF from the CPK

			DatabaseObjectModel utf = (objectModel as DatabaseObjectModel);
			DatabaseTable dt = new DatabaseTable();

			br.Endianness = IO.Endianness.BigEndian;

			UTFTABLEINFO info = ReadUTFTableInfo(br);

			int[] columnNameOffsets = new int[info.tableColumns];
			long[] constantOffsets = new long[info.tableColumns];
			CPKColumnStorageType[] storageTypes = new CPKColumnStorageType[info.tableColumns];
			CPKColumnDataType[] dataTypes = new CPKColumnDataType[info.tableColumns];

			for (int i = 0; i < info.tableColumns; i++)
			{
				byte schema = br.ReadByte();
				/*// wtf is this?
				if (schema == 0)
				{
					br.Accessor.Seek(3, SeekOrigin.Current);
					column.flags = br.ReadByte();
				}
				*/
				columnNameOffsets[i] = br.ReadInt32();

				storageTypes[i] = (CPKColumnStorageType)(schema & (byte)CPKColumnStorageType.Mask);
				dataTypes[i] = (CPKColumnDataType)(schema & (byte)CPKColumnDataType.Mask);

				if (storageTypes[i] == CPKColumnStorageType.Constant)
				{
					constantOffsets[i] = br.Accessor.Position;
					switch (dataTypes[i])
					{
						case CPKColumnDataType.Long:
						case CPKColumnDataType.Long2:
						case CPKColumnDataType.Data:
						{
							long dummy = br.ReadInt64();
							break;
						}
						case CPKColumnDataType.Float:
						{
							float dummy = br.ReadSingle();
							break;
						}
						case CPKColumnDataType.String:
						case CPKColumnDataType.Int:
						case CPKColumnDataType.Int2:
						{
							int dummy = br.ReadInt32();
							break;
						}
						case CPKColumnDataType.Short:
						case CPKColumnDataType.Short2:
						{
							short dummy = br.ReadInt16();
							break;
						}
						case CPKColumnDataType.Byte:
						case CPKColumnDataType.Byte2:
						{
							byte dummy = br.ReadByte();
							break;
						}
						default:
						{
							Console.WriteLine("cpk: ReadUTFTable: unknown data type for column " + i.ToString());
							break;
						}
					}
				}

				dt.Fields.Add("Field" + i.ToString(), null);
			}

			// Read string table - remember, this is relative to UTF data WITH the "@UTF" signature
			br.Seek(info.stringTableOffset + 8, IO.SeekOrigin.Begin);

			byte[] stringTableData = br.ReadBytes(info.stringTableSize);
			MemoryAccessor maStringTable = new MemoryAccessor(stringTableData);

			maStringTable.Reader.Seek(info.tableNameStringOffset, IO.SeekOrigin.Begin);
			dt.Name = maStringTable.Reader.ReadNullTerminatedString();

			for (int i = 0; i < info.tableColumns; i++)
			{
				maStringTable.Reader.Seek(columnNameOffsets[i], IO.SeekOrigin.Begin);
				dt.Fields[i].Name = maStringTable.Reader.ReadNullTerminatedString();
			}

			for (int i = 0; i < info.tableRows; i++)
			{
				uint rowOffset = (uint)(info.utfOffset + 4 + info.rowsOffset + (i * info.rowWidth));
				uint rowStartOffset = rowOffset;
				br.Accessor.Seek(rowOffset, SeekOrigin.Begin);

				DatabaseRecord record = new DatabaseRecord();

				for (int j = 0; j < info.tableColumns; j++)
				{
					CPKColumnStorageType storageType = storageTypes[j];
					CPKColumnDataType dataType = dataTypes[j];
					long constantOffset = constantOffsets[j] - 11;

					switch (storageType)
					{
						case CPKColumnStorageType.PerRow:
						break;
						case CPKColumnStorageType.Constant:
						break;
						case CPKColumnStorageType.Zero:
						record.Fields.Add(dt.Fields[j].Name, null);
						continue;
					}

					long dataOffset1 = 0;
					if (storageType == CPKColumnStorageType.Constant)
					{
						dataOffset1 = constantOffset;
					}
					else
					{
						dataOffset1 = rowOffset;
					}

					// br.Seek(dataOffset1, IO.SeekOrigin.Begin);
					switch (dataType)
					{
						case CPKColumnDataType.String:
						{
							uint stringOffset = 0;
							if (storageType == CPKColumnStorageType.Constant)
							{
								stringOffset = (uint)constantOffset;
							}
							else
							{
								stringOffset = br.ReadUInt32();
							}
							string value = null;
							if (stringOffset < stringTableData.Length)
							{
								maStringTable.Reader.Seek(stringOffset, IO.SeekOrigin.Begin);
								value = maStringTable.Reader.ReadNullTerminatedString();
							}
							record.Fields.Add(dt.Fields[j].Name, value);

							break;
						}
						case CPKColumnDataType.Data:
						{
							uint varDataOffset = br.ReadUInt32();
							uint varDataSize = br.ReadUInt32();

							byte[] value = new byte[0];
							record.Fields.Add(dt.Fields[j].Name, value);

							// Is the data in another table??
							// ReadUTFTable(br);
							break;
						}
						case CPKColumnDataType.Long:
						case CPKColumnDataType.Long2:
						{
							ulong value = br.ReadUInt64();
							record.Fields.Add(dt.Fields[j].Name, value);

							break;
						}
						case CPKColumnDataType.Int:
						case CPKColumnDataType.Int2:
						{
							uint value = br.ReadUInt32();
							record.Fields.Add(dt.Fields[j].Name, value);

							break;
						}
						case CPKColumnDataType.Short:
						case CPKColumnDataType.Short2:
						{
							ushort value = br.ReadUInt16();
							record.Fields.Add(dt.Fields[j].Name, value);
							break;
						}
						case CPKColumnDataType.Float:
						{
							float value = br.ReadSingle();
							record.Fields.Add(dt.Fields[j].Name, value);
							break;
						}
						case CPKColumnDataType.Byte:
						case CPKColumnDataType.Byte2:
						{
							byte value = br.ReadByte();
							record.Fields.Add(dt.Fields[j].Name, value);
							break;
						}
					}
				}

				dt.Records.Add(record);
			}
			utf.Tables.Add(dt);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
