//
//  UVSLayoutID.cs - internal structure representing a UVS layout ID
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

namespace UniversalEditor.DataFormats.Multimedia.Video.UVS
{
	/// <summary>
	/// Internal structure representing a UVS layout ID.
	/// </summary>
	public struct UVSLayoutID : IComparable<UVSLayoutID>, IComparable<uint>
	{
		public uint ID { get; }

		public UVSLayoutID(uint id)
		{
			ID = id;
		}

		/// <summary>
		/// The layout is defined in the Data Layout Packet.
		/// </summary>
		public static readonly UVSLayoutID Custom = new UVSLayoutID(0);
		/// <summary>
		/// 8-bpp Y plane, followed by 8-bpp 2×2 V and U planes.
		/// </summary>
		public static readonly UVSLayoutID YV12 = new UVSLayoutID(0x32315659);
		/// <summary>
		/// 8-bpp Y plane, followed by 8-bpp 2×2 U and V planes.
		/// </summary>
		public static readonly UVSLayoutID IYUV = new UVSLayoutID(0x56555949);
		/// <summary>
		/// UV downsampled 2:1 horizontally, ordered Y0 U0 Y1 V0
		/// </summary>
		public static readonly UVSLayoutID YUY2 = new UVSLayoutID(0x32595559);
		/// <summary>
		/// UV downsampled 2:1 horizontally, ordered U0 Y0 V0 Y1
		/// </summary>
		public static readonly UVSLayoutID UYVY = new UVSLayoutID(0x59565955);
		/// <summary>
		/// UV downsampled 2:1 horizontally, ordered Y0 V0 Y1 U0
		/// </summary>
		public static readonly UVSLayoutID YVYU = new UVSLayoutID(0x55595659);
		/// <summary>
		/// 8 bits per component, stored BGR, rows aligned to a 32 bit boundary, rows stored bottom first.
		/// </summary>
		public static readonly UVSLayoutID RGB24DIB = new UVSLayoutID(0x80808081);
		/// <summary>
		/// 8 bits per component, stored BGRx (x is don't care), rows stored bottom first.
		/// </summary>
		public static readonly UVSLayoutID RGB32DIB = new UVSLayoutID(0x80808082);
		/// <summary>
		/// 8 bits per component, stored BGRA, rows stored bottom first.
		/// </summary>
		public static readonly UVSLayoutID ARGBDIB = new UVSLayoutID(0x80808083);

		public int CompareTo(UVSLayoutID other)
		{
			return ID.CompareTo(other.ID);
		}
		public int CompareTo(uint other)
		{
			return ID.CompareTo(other);
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		public static bool operator ==(UVSLayoutID left, UVSLayoutID right)
		{
			return (left.ID == right.ID);
		}
		public static bool operator !=(UVSLayoutID left, UVSLayoutID right)
		{
			return (left.ID != right.ID);
		}
	}
}
