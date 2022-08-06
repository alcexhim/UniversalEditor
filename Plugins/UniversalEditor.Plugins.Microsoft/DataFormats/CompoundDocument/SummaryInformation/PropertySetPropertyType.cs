//
//  PropertySetPropertyType.cs
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
namespace UniversalEditor.DataFormats.CompoundDocument.SummaryInformation
{
	public enum PropertySetPropertyType : ushort
	{
		/// <summary>
		/// Type is undefined, and the minimum property set version is 0.
		/// </summary>
		Empty = 0x0000,
		/// <summary>
		/// Type is null, and the minimum property set version is 0.
		/// </summary>
		Null = 0x0001,
		/// <summary>
		/// Type is 16-bit signed integer, and the minimum property set version is 0.
		/// </summary>
		I2 = 0x0002,
		/// <summary>
		/// Type is 32-bit signed integer, and the minimum property set version is 0.
		/// </summary>
		I4 = 0x0003,
		/// <summary>
		/// Type is 4-byte (single-precision) IEEE floating-point number, and the
		/// minimum property set version is 0.
		/// </summary>
		R4 = 0x0004,
		/// <summary>
		/// Type is 8-byte (double-precision) IEEE floating-point number, and the
		/// minimum property set version is 0.
		/// </summary>
		R8 = 0x0005,
		/// <summary>
		/// Type is CURRENCY, and the minimum property set version is 0.
		/// </summary>
		Currency = 0x0006,
		/// <summary>
		/// Type is DATE (OLE Automation), and the minimum property set version is 0.
		/// </summary>
		Date = 0x0007,
		/// <summary>
		/// Type is CodePageString, and the minimum property set version is 0.
		/// </summary>
		BStr = 0x0008,
		/// <summary>
		/// Type is HRESULT, and the minimum property set version is 0.
		/// </summary>
		Error = 0x000A,
		/// <summary>
		/// Type is VARIANT_BOOL, and the minimum property set version is 0.
		/// </summary>
		Bool = 0x000B,
		/// <summary>
		/// Type is DECIMAL, and the minimum property set version is 0.
		/// </summary>
		Decimal = 0x000E,
		/// <summary>
		/// Type is 1-byte signed integer, and the minimum property set version is 1.
		/// </summary>
		I1 = 0x0010,
		/// <summary>
		/// Type is 1-byte unsigned integer, and the minimum property set version is 0.
		/// </summary>
		UI1 = 0x0011,
		/// <summary>
		/// Type is 2-byte unsigned integer, and the minimum property set version is 0.
		/// </summary>
		UI2 = 0x0012,
		/// <summary>
		/// Type is 4-byte unsigned integer, and the minimum property set version is 0.
		/// </summary>
		UI4 = 0x0013,
		/// <summary>
		/// Type is 8-byte signed integer, and the minimum property set version is 0.
		/// </summary>
		I8 = 0x0014,
		/// <summary>
		/// Type is 8-byte unsigned integer, and the minimum property set version is 0.
		/// </summary>
		UI8 = 0x0015,
		/// <summary>
		/// Type is 4-byte signed integer, and the minimum property set version is 1.
		/// </summary>
		Int = 0x0016,
		/// <summary>
		/// Type is 4-byte unsigned integer, and the minimum property set version is 1.
		/// </summary>
		UInt = 0x0017,
		/// <summary>
		/// Type is CodePageString, and the minimum property set version is 0.
		/// </summary>
		LPStr = 0x001E,
		/// <summary>
		/// Type is UnicodeString, and the minimum property set version is 0.
		/// </summary>
		LPWStr = 0x001F,
		/// <summary>
		/// Type is FILETIME, and the minimum property set version is 0.
		/// </summary>
		FileTime = 0x0040,
		/// <summary>
		/// Type is binary large object (BLOB), and the minimum property set version is 0.
		/// </summary>
		Blob = 0x0041,
		/// <summary>
		/// Type is Stream, and the minimum property set version is 0. VT_STREAM is not
		/// allowed in a simple property set.
		/// </summary>
		Stream = 0x0042,
		/// <summary>
		/// Type is Storage, and the minimum property set version is 0. VT_STORAGE is not
		/// allowed in a simple property set.
		/// </summary>
		Storage = 0x0043,
		/// <summary>
		/// Type is Stream representing an Object in an application-specific manner, and the
		/// minimum property set version is 0. VT_STREAMED_Object is not allowed in a simple
		/// property set.
		/// </summary>
		StreamedObject = 0x0044,
		/// <summary>
		/// Type is Storage representing an Object in an application-specific manner, and the
		/// minimum property set version is 0. VT_STORED_Object is not allowed in a simple
		/// property set.
		/// </summary>
		StoredObject = 0x0045,
		/// <summary>
		/// Type is BLOB representing an object in an application-specific manner. The minimum
		/// property set version is 0.
		/// </summary>
		BlobObject = 0x0046,
		/// <summary>
		/// Type is PropertyIdentifier, and the minimum property set version is 0.
		/// </summary>
		PropertyIdentifier = 0x0047,
		/// <summary>
		/// Type is CLSID, and the minimum property set version is 0.
		/// </summary>
		CLSID = 0x0048,
		/// <summary>
		/// Type is Stream with application-specific version GUID (VersionedStream). The
		/// minimum property set version is 0. VT_VERSIONED_STREAM is not allowed in a
		/// simple property set.
		/// </summary>
		VersionedStream = 0x0049,

		Vector = 0x1000,
		Array = 0x2000
	}
}
