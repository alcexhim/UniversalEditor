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
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;

namespace UniversalEditor.Plugins.CRI.DataFormats.Database.UTF
{
	public class UTFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(DatabaseObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

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
			DatabaseObjectModel utf = (objectModel as DatabaseObjectModel);
			if (utf == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			string utf_signature = br.ReadFixedLengthString(4);
			if (utf_signature != "@UTF")
				throw new InvalidDataFormatException(); // we are assuming passed in decrypted UTF from the CPK

			DatabaseTable dt = new DatabaseTable();

			br.Endianness = IO.Endianness.BigEndian;

			UTFTABLEINFO info = ReadUTFTableInfo(br);

			int[] columnNameOffsets = new int[info.tableColumns];
			long[] constantOffsets = new long[info.tableColumns];
			UTFColumnStorageType[] storageTypes = new UTFColumnStorageType[info.tableColumns];
			UTFColumnDataType[] dataTypes = new UTFColumnDataType[info.tableColumns];

			// Read string table - remember, this is relative to UTF data WITH the "@UTF" signature
			br.Accessor.SavePosition();
			br.Seek(info.stringTableOffset + 8, IO.SeekOrigin.Begin);

			byte[] stringTableData = br.ReadBytes(info.stringTableSize);
			MemoryAccessor maStringTable = new MemoryAccessor(stringTableData);

			maStringTable.Reader.Seek(info.tableNameStringOffset, IO.SeekOrigin.Begin);
			br.Accessor.LoadPosition();

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

				storageTypes[i] = (UTFColumnStorageType)(schema & (byte)UTFColumnStorageType.Mask);
				dataTypes[i] = (UTFColumnDataType)(schema & (byte)UTFColumnDataType.Mask);

				object constantValue = null;
				if (storageTypes[i] == UTFColumnStorageType.Constant)
				{
					constantOffsets[i] = br.Accessor.Position;
					switch (dataTypes[i])
					{
						case UTFColumnDataType.Long:
						case UTFColumnDataType.Long2:
						case UTFColumnDataType.Data:
						{
							constantValue = br.ReadInt64();
							break;
						}
						case UTFColumnDataType.Float:
						{
							constantValue = br.ReadSingle();
							break;
						}
						case UTFColumnDataType.String:
						{
							int valueOffset = br.ReadInt32();
							maStringTable.Reader.Seek(valueOffset, IO.SeekOrigin.Begin);
							constantValue = maStringTable.Reader.ReadNullTerminatedString();
							break;
						}
						case UTFColumnDataType.Int:
						case UTFColumnDataType.Int2:
						{
							constantValue = br.ReadInt32();
							break;
						}
						case UTFColumnDataType.Short:
						case UTFColumnDataType.Short2:
						{
							constantValue = br.ReadInt16();
							break;
						}
						case UTFColumnDataType.Byte:
						case UTFColumnDataType.Byte2:
						{
							constantValue = br.ReadByte();
							break;
						}
						default:
						{
							Console.WriteLine("cpk: ReadUTFTable: unknown data type for column " + i.ToString());
							break;
						}
					}
				}

				dt.Fields.Add("Field" + i.ToString(), constantValue, DataTypeForDataType(dataTypes[i]));
			}

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
					UTFColumnStorageType storageType = storageTypes[j];
					UTFColumnDataType dataType = dataTypes[j];
					long constantOffset = constantOffsets[j] - 11;

					switch (storageType)
					{
						case UTFColumnStorageType.PerRow:
						{
							switch (dataType)
							{
								case UTFColumnDataType.String:
								{
									string value = null;
									if (storageType == UTFColumnStorageType.Constant)
									{
										value = (dt.Fields[j].Value as string);
									}
									else
									{
										uint stringOffset = br.ReadUInt32();
										if (stringOffset < stringTableData.Length)
										{
											maStringTable.Reader.Seek(stringOffset, IO.SeekOrigin.Begin);
											value = maStringTable.Reader.ReadNullTerminatedString();
										}
									}
									record.Fields.Add(dt.Fields[j].Name, value);
									break;
								}
								case UTFColumnDataType.Data:
								{
									uint varDataOffset = br.ReadUInt32();
									uint varDataSize = br.ReadUInt32();

									byte[] value = new byte[0];
									record.Fields.Add(dt.Fields[j].Name, value);

									// Is the data in another table??
									// ReadUTFTable(br);
									break;
								}
								case UTFColumnDataType.Long:
								case UTFColumnDataType.Long2:
								{
									ulong value = br.ReadUInt64();
									record.Fields.Add(dt.Fields[j].Name, value);

									break;
								}
								case UTFColumnDataType.Int:
								case UTFColumnDataType.Int2:
								{
									uint value = br.ReadUInt32();
									record.Fields.Add(dt.Fields[j].Name, value);

									break;
								}
								case UTFColumnDataType.Short:
								case UTFColumnDataType.Short2:
								{
									ushort value = br.ReadUInt16();
									record.Fields.Add(dt.Fields[j].Name, value);
									break;
								}
								case UTFColumnDataType.Float:
								{
									float value = br.ReadSingle();
									record.Fields.Add(dt.Fields[j].Name, value);
									break;
								}
								case UTFColumnDataType.Byte:
								case UTFColumnDataType.Byte2:
								{
									byte value = br.ReadByte();
									record.Fields.Add(dt.Fields[j].Name, value);
									break;
								}
							}
							break;
						}
						case UTFColumnStorageType.Constant:
						{
							record.Fields.Add(dt.Fields[j].Name, dt.Fields[j].Value);
							continue;
						}
						case UTFColumnStorageType.Zero:
						{
							record.Fields.Add(dt.Fields[j].Name, null);
							continue;
						}
					}
				}

				dt.Records.Add(record);
			}
			utf.Tables.Add(dt);
		}

		private Type DataTypeForDataType(UTFColumnDataType dataType)
		{
			switch (dataType)
			{
				case UTFColumnDataType.Byte:
				case UTFColumnDataType.Byte2:
				{
					return typeof(byte);
				}
				case UTFColumnDataType.Data:
				{
					return typeof(byte[]);
				}
				case UTFColumnDataType.Float:
				{
					return typeof(float);
				}
				case UTFColumnDataType.Int:
				case UTFColumnDataType.Int2:
				{
					return typeof(int);
				}
				case UTFColumnDataType.Long:
				case UTFColumnDataType.Long2:
				{
					return typeof(long);
				}
				case UTFColumnDataType.Short:
				case UTFColumnDataType.Short2:
				{
					return typeof(short);
				}
				case UTFColumnDataType.String:
				{
					return typeof(string);
				}
			}
			return null;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			DatabaseObjectModel utf = (objectModel as DatabaseObjectModel);
			if (utf == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = Accessor.Writer;
			bw.WriteFixedLengthString("@UTF");

			DatabaseTable dt = utf.Tables[0];

			bw.Endianness = IO.Endianness.BigEndian;

			// do the hard work here to determine if a field should be recorded as zero or not
			UTFColumnStorageType[] columnStorageTypes = new UTFColumnStorageType[dt.Fields.Count];
			UTFColumnDataType[] columnDataTypes = new UTFColumnDataType[dt.Fields.Count];
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				columnStorageTypes[i] = UTFColumnStorageType.Zero;
				if (dt.Fields[i].DataType == typeof(bool))
				{
					columnDataTypes[i] = UTFColumnDataType.Byte;
				}
				else if (dt.Fields[i].DataType == typeof(byte[]))
				{
					columnDataTypes[i] = UTFColumnDataType.Data;
				}
				else if (dt.Fields[i].DataType == typeof(float))
				{
					columnDataTypes[i] = UTFColumnDataType.Float;
				}
				else if (dt.Fields[i].DataType == typeof(int))
				{
					columnDataTypes[i] = UTFColumnDataType.Int;
				}
				else if (dt.Fields[i].DataType == typeof(long))
				{
					columnDataTypes[i] = UTFColumnDataType.Long;
				}
				else if (dt.Fields[i].DataType == typeof(short))
				{
					columnDataTypes[i] = UTFColumnDataType.Short;
				}
				else if (dt.Fields[i].DataType == typeof(string))
				{
					columnDataTypes[i] = UTFColumnDataType.String;
				}

				if (dt.Fields[i].Value != null)
				{
					columnStorageTypes[i] = UTFColumnStorageType.Constant;
					continue;
				}
				for (int j = 0; j < dt.Records.Count; j++)
				{
					if (dt.Records[j].Fields[i].Value != null)
					{
						columnStorageTypes[i] = UTFColumnStorageType.PerRow;
						break;
					}
				}
			}

			int tableSize = 32; // size of entire file
			tableSize += (5 * dt.Fields.Count);

			tableSize += (dt.Name.Length + 1);
			tableSize += 7; // "<NULL>\0".Length

			int rowsOffset = 24 + (5 * dt.Fields.Count);
			int stringTableOffset = rowsOffset;
			short rowWidth = 0;
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				tableSize += (dt.Fields[i].Name.Length + 1);
				if (columnStorageTypes[i] == UTFColumnStorageType.Constant)
				{
					tableSize += GetLengthForDataType(columnDataTypes[i]);
					stringTableOffset += GetLengthForDataType(columnDataTypes[i]);
					rowsOffset += GetLengthForDataType(columnDataTypes[i]);

					if (columnDataTypes[i] == UTFColumnDataType.String)
					{
						tableSize += ((string)dt.Fields[i].Value).Length + 1;
					}
				}
				else if (columnStorageTypes[i] == UTFColumnStorageType.PerRow)
				{
					rowWidth += GetLengthForDataType(columnDataTypes[i]);
				}
			}

			for (int i = 0; i < dt.Records.Count; i++)
			{
				for (int j = 0; j < dt.Records[i].Fields.Count; j++)
				{
					if (columnStorageTypes[j] == UTFColumnStorageType.PerRow)
					{
						tableSize += GetLengthForDataType(columnDataTypes[j]);
						stringTableOffset += GetLengthForDataType(columnDataTypes[j]);
						if (columnDataTypes[j] == UTFColumnDataType.String)
						{
							tableSize += ((string)dt.Records[i].Fields[j].Value).Length + 1;
						}
					}
				}
			}

			tableSize += ((8 - (tableSize % 8)) % 8);
			tableSize -= 8;

			bw.WriteInt32(tableSize);
			bw.WriteInt32(rowsOffset);
			bw.WriteInt32(stringTableOffset);
			bw.WriteInt32(tableSize); // data offset - same as table size?
			bw.WriteUInt32(7); // "<NULL>\0".Length
			bw.WriteInt16((short)dt.Fields.Count); // 0023
			bw.WriteInt16(rowWidth); // 007e
			bw.WriteInt32(dt.Records.Count); // 00000001

			int columnNameOffset = (int)8 + (int)dt.Name.Length; // add space for "<NULL>\0" string and dt.Name + 1

			List<string> stringTable = new List<string>();
			stringTable.Add("<NULL>");
			stringTable.Add(dt.Name);
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				byte schema = 0;
				schema |= (byte)((byte)columnStorageTypes[i] | (byte)columnDataTypes[i]);

				bw.WriteByte(schema);
				bw.WriteInt32(columnNameOffset);

				columnNameOffset += dt.Fields[i].Name.Length + 1;
				stringTable.Add(dt.Fields[i].Name);

				if (columnStorageTypes[i] == UTFColumnStorageType.Constant)
				{
					WriteValue(bw, dt.Fields[i].Value, columnDataTypes[i], stringTable);
					if (columnDataTypes[i] == UTFColumnDataType.String)
					{
						columnNameOffset += ((string)dt.Fields[i].Value).Length + 1; 
					}
				}
			}

			for (int i = 0; i < dt.Records.Count; i++)
			{
				for (int j = 0; j < dt.Records[i].Fields.Count; j++)
				{
					if (columnStorageTypes[j] == UTFColumnStorageType.PerRow)
					{
						WriteValue(bw, dt.Records[i].Fields[j].Value, columnDataTypes[j], stringTable);
					}
				}
			}

			for (int i = 0; i < stringTable.Count; i++)
			{
				bw.WriteNullTerminatedString(stringTable[i]);
			}

			bw.Align(8);
		}

		private short GetLengthForDataType(UTFColumnDataType columnDataType)
		{
			switch (columnDataType)
			{
				case UTFColumnDataType.String:
				{
					return 4;
				}
				case UTFColumnDataType.Data:
				case UTFColumnDataType.Long:
				case UTFColumnDataType.Long2:
				{
					return 8;
				}
				case UTFColumnDataType.Byte:
				case UTFColumnDataType.Byte2:
				{
					return 1;
				}
				case UTFColumnDataType.Float:
				case UTFColumnDataType.Int:
				case UTFColumnDataType.Int2:
				{
					return 4;
				}
				case UTFColumnDataType.Short:
				case UTFColumnDataType.Short2:
				{
					return 2;
				}
			}
			throw new NotImplementedException();
		}

		private void WriteValue(Writer bw, object value, UTFColumnDataType columnDataType, List<string> stringTable)
		{
			switch (columnDataType)
			{
				case UTFColumnDataType.String:
				{
					string str = (string)value;
					if (stringTable.Contains(str))
					{
						bw.WriteUInt32((uint)stringTable.GetItemOffset(stringTable.IndexOf(str), 1));
					}
					else
					{
						stringTable.Add(str);
						bw.WriteUInt32((uint)stringTable.GetItemOffset(stringTable.Count - 1, 1));
					}
					break;
				}
				case UTFColumnDataType.Data:
				{
					uint varDataOffset = 0;
					uint varDataSize = 0;
					bw.WriteUInt32(varDataOffset);
					bw.WriteUInt32(varDataSize);
					break;
				}
				case UTFColumnDataType.Long:
				case UTFColumnDataType.Long2:
				{
					bw.WriteUInt64((ulong)value);
					break;
				}
				case UTFColumnDataType.Int:
				case UTFColumnDataType.Int2:
				{
					bw.WriteUInt32((uint)value);
					break;
				}
				case UTFColumnDataType.Short:
				case UTFColumnDataType.Short2:
				{
					bw.WriteUInt16((ushort)value);
					break;
				}
				case UTFColumnDataType.Float:
				{
					bw.WriteSingle((float)value);
					break;
				}
				case UTFColumnDataType.Byte:
				case UTFColumnDataType.Byte2:
				{
					bw.WriteByte((byte)value);
					break;
				}
			}
		}
	}
}
