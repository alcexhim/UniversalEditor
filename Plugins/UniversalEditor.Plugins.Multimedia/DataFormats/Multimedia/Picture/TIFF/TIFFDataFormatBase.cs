//
//  TIFFDataFormatBase.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public class TIFFDataFormatBase : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(TIFFObjectModelBase), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice("Endianness", "_Endianness", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("Little-endian", Endianness.LittleEndian),
					new CustomOptionFieldChoice("Big-endian", Endianness.BigEndian)
				}));
			}
			return _dfr;
		}

		public Endianness Endianness { get; set; } = Endianness.LittleEndian;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			TIFFObjectModelBase tiff = (objectModel as TIFFObjectModelBase);
			if (tiff == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			string signature1 = reader.ReadFixedLengthString(2);
			if (signature1 == "II")
			{
				reader.Endianness = Endianness.LittleEndian;
			}
			else if (signature1 == "MM")
			{
				reader.Endianness = Endianness.BigEndian;
			}
			else
			{
				throw new InvalidDataFormatException("file does not begin with either 'II' or 'MM'");
			}

			Endianness = reader.Endianness;

			ushort signature2 = reader.ReadUInt16();
			if (signature2 != 42)
			{
				throw new InvalidDataFormatException("incorrect answer to the ultimate question of life, the universe, and everything (expected 42)");
			}

			while (!reader.EndOfStream)
			{
				// The offset (in bytes) of the first IFD. The directory may be at any location in the
				// file after the header but must begin on a word boundary.In particular, an Image
				// File Directory may follow the image data it describes. Readers must follow the
				// pointers wherever they may lead.
				uint IFDoffset = reader.ReadUInt32();
				if (IFDoffset == 0)
					break;

				reader.Seek(IFDoffset, SeekOrigin.Begin);

				TIFFImageFileDirectory ifd = new TIFFImageFileDirectory();

				ushort directoryEntryCount = reader.ReadUInt16();
				ReadIFDEntries(reader, ifd, directoryEntryCount);

				tiff.ImageFileDirectories.Add(ifd);
			}
		}

		private void ReadIFDEntries(Reader reader, TIFFImageFileDirectory ifd, ushort count)
		{
			// There must be at least 1 IFD in a TIFF file and each IFD must have at least one entry.
			for (ushort i = 0; i < count; i++)
			{
				ifd.Entries.Add(ReadIFDEntry(reader));
			}
		}
		private TIFFImageFileDirectoryEntry ReadIFDEntry(Reader reader)
		{
			// An Image File Directory (IFD) consists of a 2-byte count of the number of directory
			// entries(i.e., the number of fields), followed by a sequence of 12 - byte field
			// entries, followed by a 4 - byte offset of the next IFD(or 0 if none). (Do not forget to
			// write the 4 bytes of 0 after the last IFD.)

			TIFFImageFileDirectoryEntry entry = new TIFFImageFileDirectoryEntry();

			entry.Tag = (TIFFTag)reader.ReadUInt16();
			entry.Type = (TIFFDataType)reader.ReadUInt16();
			entry.Count = reader.ReadUInt32();
			entry.OffsetOrValue = reader.ReadUInt32();

			byte[] value = BitConverter.GetBytes(entry.OffsetOrValue);

			long ofs = reader.Accessor.Position;

			switch (entry.Type)
			{
				case TIFFDataType.Ascii:
				{
					if (entry.Count <= 4)
					{
						entry.Value = System.Text.Encoding.Default.GetString(value);
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadFixedLengthString((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.Undefined:
				{
					if (entry.Count <= 4)
					{
						entry.Value = value;
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadBytes((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.Byte:
				case TIFFDataType.SignedByte:
				{
					if (entry.Count == 1)
					{
						entry.Value = value[0];
						break;
					}
					else if (entry.Count <= 4)
					{
						byte[] val = new byte[entry.Count];
						for (int i = 0; i < entry.Count; i++)
						{
							val[i] = value[i];
						}
						entry.Value = val;
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadBytes((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.Short:
				{
					if (entry.Count == 1)
					{
						entry.Value = BitConverter.ToUInt16(value, 0);
					}
					else if (entry.Count == 2)
					{
						entry.Value = new ushort[] { BitConverter.ToUInt16(value, 0), BitConverter.ToUInt16(value, 2) };
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadUInt16Array((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.SignedShort:
				{
					if (entry.Count == 1)
					{
						entry.Value = BitConverter.ToInt16(value, 0);
					}
					else if (entry.Count == 2)
					{
						entry.Value = new short[] { BitConverter.ToInt16(value, 0), BitConverter.ToInt16(value, 2) };
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadInt16Array((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.Long:
				{
					if (entry.Count == 1)
					{
						entry.Value = BitConverter.ToUInt32(value, 0);
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadUInt32Array((int)entry.Count);
					}
					break;
				}
				case TIFFDataType.SignedLong:
				{
					if (entry.Count == 1)
					{
						entry.Value = BitConverter.ToInt32(value, 0);
					}
					else
					{
						reader.Accessor.Position = entry.OffsetOrValue;
						entry.Value = reader.ReadInt32Array((int)entry.Count);
					}
					break;
				}
			}

			reader.Accessor.Position = ofs;
			return entry;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			TIFFObjectModelBase tiff = (objectModel as TIFFObjectModelBase);
			if (tiff == null) throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.Endianness = Endianness;

			switch (Endianness)
			{
				case Endianness.LittleEndian:
				{
					writer.WriteFixedLengthString("II");
					break;
				}
				case Endianness.BigEndian:
				{
					writer.WriteFixedLengthString("MM");
					break;
				}
				default:
				{
					throw new NotSupportedException();
				}
			}

			writer.WriteUInt16(42); // signature2

			uint IFDoffset = 8;

			for (int i = 0; i < tiff.ImageFileDirectories.Count; i++)
			{
				writer.WriteUInt32(IFDoffset);

				TIFFImageFileDirectory ifd = tiff.ImageFileDirectories[i];
				WriteIFDEntries(writer, ifd);

				IFDoffset = (uint)writer.Accessor.Position + 4;
			}
		}

		private void WriteIFDEntries(Writer writer, TIFFImageFileDirectory ifd)
		{
			// There must be at least 1 IFD in a TIFF file and each IFD must have at least one entry.
			writer.WriteUInt16((ushort)ifd.Entries.Count);

			List<TIFFImageFileDirectoryEntry> ifdentries = new List<TIFFImageFileDirectoryEntry>(ifd.Entries);
			ifdentries.Sort();

			MemoryAccessor maOverflow = new MemoryAccessor();
			Writer overflowWriter = maOverflow.Writer;

			long overflowWriterOffset = Accessor.Position + ((ushort)ifdentries.Count * 12);
			for (ushort i = 0; i < (ushort)ifdentries.Count; i++)
			{
				WriteIFDEntry(writer, ifdentries[i], overflowWriter, overflowWriterOffset);
			}

			writer.WriteBytes(maOverflow.ToArray());
		}

		private void WriteIFDEntry(Writer writer, TIFFImageFileDirectoryEntry entry, Writer overflowWriter, long overflowWriterOffset)
		{
			// An Image File Directory (IFD) consists of a 2-byte count of the number of directory
			// entries(i.e., the number of fields), followed by a sequence of 12 - byte field
			// entries, followed by a 4 - byte offset of the next IFD(or 0 if none). (Do not forget to
			// write the 4 bytes of 0 after the last IFD.)
			long overflowOffset = overflowWriter.Accessor.Position;

			if (entry.Value is string)
			{
				entry.Type = TIFFDataType.Ascii;
				string value = (string)entry.Value;
				entry.Count = (uint)value.Length;
				if (value.Length <= 4)
				{
					entry.OffsetOrValue = BitConverter.ToUInt32(System.Text.Encoding.Default.GetBytes(value), 0);
				}
				else
				{
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteFixedLengthString((string)entry.Value);
				}
			}
			else if (entry.Value is byte[] && entry.Type == TIFFDataType.Undefined)
			{
				byte[] value = (byte[])entry.Value;
				entry.Count = (uint)value.Length;
				if (value.Length <= 4)
				{
					entry.OffsetOrValue = BitConverter.ToUInt32(value, 0);
				}
				else
				{
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteBytes(value);
				}
			}
			else if ((entry.Value is byte || entry.Value is sbyte || entry.Value is byte[] || entry.Value is sbyte[]) && (entry.Type == TIFFDataType.Byte || entry.Type == TIFFDataType.SignedByte))
			{
				if (entry.Value is byte)
				{
					entry.Type = TIFFDataType.Byte;
					entry.Count = 1;
					entry.OffsetOrValue = (byte)entry.Value;
				}
				else if (entry.Value is byte[] && ((byte[])entry.Value).Length <= 4)
				{
					entry.Type = TIFFDataType.Byte;
					byte[] value = (byte[])entry.Value;
					entry.Count = (uint)value.Length;
					entry.OffsetOrValue = BitConverter.ToUInt32(value, 0);
				}
				else if (entry.Value is byte[])
				{
					entry.Type = TIFFDataType.Byte;
					entry.Count = (uint)((byte[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteBytes((byte[])entry.Value);
				}
				else if (entry.Value is sbyte)
				{
					entry.Type = TIFFDataType.SignedByte;
					entry.Count = 1;
					entry.OffsetOrValue = (uint)(sbyte)entry.Value;
				}
				else if (entry.Value is sbyte[] && ((sbyte[])entry.Value).Length <= 4)
				{
					entry.Type = TIFFDataType.SignedByte;
					sbyte[] value = (sbyte[])entry.Value;
					entry.Count = (uint)value.Length;
					entry.OffsetOrValue = BitConverter.ToUInt32((byte[])(Array)value, 0);
				}
				else if (entry.Value is sbyte[])
				{
					entry.Type = TIFFDataType.SignedByte;
					entry.Count = (uint)((sbyte[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteSBytes((sbyte[])entry.Value);
				}
			}
			else if (entry.Value is ushort || entry.Value is ushort[])
			{
				entry.Type = TIFFDataType.Short;
				if (entry.Value is ushort)
				{
					entry.Count = 1;
					entry.OffsetOrValue = (ushort)entry.Value;
				}
				else if (entry.Value is ushort[] && ((ushort[])entry.Value).Length == 2)
				{
					entry.Count = 2;
					byte[] data = new byte[4];
					ushort[] items = (ushort[])entry.Value;

					if (items.Length != 2)
						throw new InvalidOperationException();

					byte[] u1 = BitConverter.GetBytes(items[0]);
					byte[] u2 = BitConverter.GetBytes(items[1]);
					Array.Copy(u1, 0, data, 0, u1.Length);
					Array.Copy(u2, 0, data, 2, u2.Length);

					entry.OffsetOrValue = BitConverter.ToUInt32(data, 0);
				}
				else
				{
					entry.Count = (uint)((ushort[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteUInt16Array((ushort[])entry.Value);
				}
			}
			else if (entry.Value is short || entry.Value is short[])
			{
				entry.Type = TIFFDataType.SignedShort;
				if (entry.Value is short)
				{
					entry.Count = 1;
					entry.OffsetOrValue = (uint)(short)entry.Value;
				}
				else if (entry.Value is short[] && ((short[])entry.Value).Length == 2)
				{
					entry.Count = 2;

					byte[] data = new byte[4];
					short[] items = (short[])entry.Value;

					if (items.Length != 2)
						throw new InvalidOperationException();

					byte[] u1 = BitConverter.GetBytes(items[0]);
					byte[] u2 = BitConverter.GetBytes(items[1]);
					Array.Copy(u1, 0, data, 0, u1.Length);
					Array.Copy(u2, 0, data, 2, u2.Length);

					entry.OffsetOrValue = BitConverter.ToUInt32(data, 0);
				}
				else
				{
					entry.Count = (uint)((short[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteInt16Array((short[])entry.Value);
				}
			}
			else if (entry.Value is uint || entry.Value is uint[])
			{
				entry.Type = TIFFDataType.Long;
				if (entry.Value is uint)
				{
					entry.Count = 1;
					entry.OffsetOrValue = (uint)entry.Value;
				}
				else if (entry.Value is uint[])
				{
					entry.Count = (uint)((uint[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteUInt32Array((uint[])entry.Value);
				}
			}
			else if (entry.Value is int || entry.Value is int[])
			{
				entry.Type = TIFFDataType.SignedLong;
				if (entry.Value is int)
				{
					entry.Count = 1;
					entry.OffsetOrValue = (uint)((int)entry.Value);
				}
				else if (entry.Value is int[])
				{
					entry.Count = (uint)((int[])entry.Value).Length;
					entry.OffsetOrValue = (uint)(overflowWriterOffset + overflowWriter.Accessor.Position);
					overflowWriter.WriteInt32Array((int[])entry.Value);
				}
			}

			writer.WriteUInt16((ushort)entry.Tag);
			writer.WriteUInt16((ushort)entry.Type);
			writer.WriteUInt32(entry.Count);
			writer.WriteUInt32(entry.OffsetOrValue);
		}
	}
}
