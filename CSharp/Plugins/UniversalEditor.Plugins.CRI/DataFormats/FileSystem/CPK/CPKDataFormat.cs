// Universal Editor file format module for SEGA UMD CPK archive files
// Copyright (C) 2011  Mike Becker
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

using System;

using UniversalEditor.ObjectModels.Database;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK
{
	public class CPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
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
            public uint tableNameStringIndex;
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
            info.tableNameStringIndex = br.ReadUInt32();
            info.tableColumns = br.ReadInt16();
            info.rowWidth = br.ReadInt16();
            info.tableRows = br.ReadInt32();
            info.stringTableSize = info.dataOffset - info.stringTableOffset;
            return info;
        }

        private DatabaseTable ReadUTFTable(IO.Reader br)
        {
            DatabaseTable dt = new DatabaseTable();
            dt.Name = "@UTF";

            string utfSignal = br.ReadFixedLengthString(4);

            if (utfSignal != "@UTF")
            {
                return null;
            }

            UTFTABLEINFO info = ReadUTFTableInfo(br);

            int[] columnNameIndices = new int[info.tableColumns];
            long[] constantOffsets = new long[info.tableColumns];
            CPKColumnStorageType[] storageTypes = new CPKColumnStorageType[info.tableColumns];
            CPKColumnDataType[] dataTypes = new CPKColumnDataType[info.tableColumns];

            for (int i = 0; i < info.tableColumns; i++)
            {
                byte schema = br.ReadByte();
                columnNameIndices[i] = br.ReadInt32();

                storageTypes[i] = (CPKColumnStorageType)(schema & (byte)CPKColumnStorageType.Mask);
                dataTypes[i] = (CPKColumnDataType)(schema & (byte)CPKColumnDataType.Mask);

                if (storageTypes[i] == CPKColumnStorageType.Constant)
                {
                    constantOffsets[i] = Accessor.Position;
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

            // Read string table

            Accessor.Seek(info.stringTableOffset + 8 + 0x10, IO.SeekOrigin.Begin);

            string[] stringTable = br.ReadNullTerminatedStringArray(info.stringTableSize);

            string tableName = stringTable[info.tableNameStringIndex];


            // Seek to string table offset
            Accessor.Seek(info.stringTableOffset + 4 + info.utfOffset, IO.SeekOrigin.Begin);
            for (short i = 0; i < info.tableColumns; i++)
            {
                string columnName = br.ReadNullTerminatedString();
                dt.Fields[i].Name = columnName;
            }

            for (int i = 0; i < info.tableRows; i++)
            {
                uint rowOffset = (uint)(info.utfOffset + 8 + info.rowsOffset + (i * info.rowWidth));
                uint rowStartOffset = rowOffset;

                DatabaseRecord record = new DatabaseRecord();

                for (int j = 0; j < info.tableColumns; j++)
                {
                    CPKColumnStorageType storageType = storageTypes[j];
                    CPKColumnDataType dataType = dataTypes[j];
                    long constantOffset = constantOffsets[j];

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

                    Accessor.Seek(dataOffset1, IO.SeekOrigin.Begin);
                    switch (dataType)
                    {
                        case CPKColumnDataType.String:
                        {
                            uint stringOffset = br.ReadUInt32();
                            string value = null;
                            if (stringOffset < stringTable.Length)
                            {
                                value = stringTable[stringOffset];
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

            return dt;
        }

        private string[] ReadUTFStringTable(IO.Reader br)
        {
            Accessor.Seek(12, IO.SeekOrigin.Current);

            UTFTABLEINFO info = ReadUTFTableInfo(br);
            string[] value = br.ReadNullTerminatedStringArray(info.stringTableSize);

            return value;
        }

        protected override void LoadInternal (ref ObjectModel objectModel)
		{
			ObjectModels.FileSystem.FileSystemObjectModel fsom = (objectModel as ObjectModels.FileSystem.FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Reader br = Accessor.Reader;
        	br.Endianness = IO.Endianness.BigEndian;

            // Rebuilt based on cpk_unpack
            Accessor.Seek(0, IO.SeekOrigin.Begin);

			string CPK = br.ReadFixedLengthString (4);
            Accessor.Seek(0x10, IO.SeekOrigin.Begin);

            DatabaseTable dtUTF = ReadUTFTable(br);

            // For now, just hardcode the TOC offset
            long tocOffset = 2048; // (long)dtUTF.Records[0].Fields["TocOffset"].Value;
            Accessor.Seek(tocOffset, IO.SeekOrigin.Begin);

            string tocSignature = br.ReadFixedLengthString(4);
            long tocEntries = (long)dtUTF.Records.Count;

            string[] tocStringTable = ReadUTFStringTable(br);

            Accessor.Seek(2064, IO.SeekOrigin.Begin);
            DatabaseTable dtUTF2 = ReadUTFTable(br);

            if (objectModel is DatabaseObjectModel)
            {
        		(objectModel as DatabaseObjectModel).Tables.Add (dtUTF);
        	}
			else if (objectModel is FileSystemObjectModel)
			{
        		for (int i = 0; i < dtUTF.Fields.Count; i++)
				{
        			fsom.Files.Add (dtUTF.Fields[i].Name);
				}
			}
		}
		protected override void SaveInternal (ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Writer bw = Accessor.Writer;
			
			bw.Flush ();
			bw.Close ();
		}
	}
}

