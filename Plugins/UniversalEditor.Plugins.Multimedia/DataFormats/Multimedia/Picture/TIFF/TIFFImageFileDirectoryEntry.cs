//
//  TIFFImageFileDirectory.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public class TIFFImageFileDirectoryEntry : ICloneable, IComparable<TIFFImageFileDirectoryEntry>
	{
		public class TIFFImageFileDirectoryEntryCollection
			: System.Collections.ObjectModel.Collection<TIFFImageFileDirectoryEntry>
		{
			public TIFFImageFileDirectoryEntry GetByTag(TIFFTag tag)
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i].Tag == tag)
						return this[i];
				}
				return null;
			}

			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, int value)
			{
				if (value <= short.MaxValue)
				{
					return Add(tag, (short)value);
				}

				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.SignedLong;
				item.Value = value;
				item.Count = 1;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, short value)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.SignedShort;
				item.Value = value;
				item.Count = 1;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, uint value)
			{
				if (value <= ushort.MaxValue)
				{
					return Add(tag, (ushort)value);
				}

				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.Long;
				item.Value = value;
				item.Count = 1;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, ushort value)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.Short;
				item.Value = value;
				item.Count = 1;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, int[] values)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.SignedLong;
				item.Value = values;
				item.Count = (uint)values.Length;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, short[] values)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.SignedShort;
				item.Value = values;
				item.Count = (uint)values.Length;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, uint[] values)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.Long;
				item.Value = values;
				item.Count = (uint)values.Length;
				Add(item);
				return item;
			}
			public TIFFImageFileDirectoryEntry Add(TIFFTag tag, ushort[] values)
			{
				TIFFImageFileDirectoryEntry item = new TIFFImageFileDirectoryEntry();
				item.Tag = tag;
				item.Type = TIFFDataType.Short;
				item.Value = values;
				item.Count = (uint)values.Length;
				Add(item);
				return item;
			}
		}

		public long GetNumericValue()
		{
			switch (Type)
			{
				case TIFFDataType.Byte: return (byte)Value;
				case TIFFDataType.Short: return (ushort)Value;
				case TIFFDataType.Long: return (uint)Value;
				case TIFFDataType.SignedByte: return (sbyte)Value;
				case TIFFDataType.SignedShort: return (short)Value;
				case TIFFDataType.SignedLong: return (int)Value;
			}
			throw new NotSupportedException();
		}

		/// <summary>
		/// The Tag that identifies the field.
		/// </summary>
		public TIFFTag Tag { get; set; }
		/// <summary>
		/// The field Type.
		/// </summary>
		public TIFFDataType Type { get; set; }
		/// <summary>
		/// The number of values, Count of the indicated Type.
		/// </summary>
		public uint Count { get; set; } = 1;
		/// <summary>
		/// The Value Offset, the file offset (in bytes) of the Value for the field. The Value is expected to begin on a word boundary; the corresponding
		/// Value Offset will thus be an even number. This file offset may point anywhere in the file, even after the image data.
		/// </summary>
		/// <remarks>
		/// To save time and space the Value Offset contains the Value instead of pointing to the Value IF AND ONLY IF the Value fits into 4 bytes. If the Value is shorter
		/// than 4 bytes, it is left-justified within the 4-byte Value Offset, i.e., stored in the lower-numbered bytes. Whether the Value fits within 4 bytes is determined by
		/// the Type and Count of the field.
		/// </remarks>
		public uint OffsetOrValue { get; set; }

		public object Value { get; set; }

		public override string ToString()
		{
			return String.Format("{0} ({1} x {2}) @ {3}", Tag, Type, Count, OffsetOrValue);
		}

		public object Clone()
		{
			TIFFImageFileDirectoryEntry clone = new TIFFImageFileDirectoryEntry();
			clone.Tag = Tag;
			clone.Type = Type;
			clone.Count = Count;
			clone.OffsetOrValue = OffsetOrValue;
			return clone;
		}

		public int CompareTo(TIFFImageFileDirectoryEntry other)
		{
			return Tag.CompareTo(other.Tag);
		}
	}
}
