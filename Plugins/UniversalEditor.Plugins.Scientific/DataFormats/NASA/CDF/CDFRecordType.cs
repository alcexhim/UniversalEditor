//
//  CDFRecordType.cs
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
namespace UniversalEditor.Plugins.Scientific.DataFormats.NASA.CDF
{
	public enum CDFRecordType : uint
	{
		/// <summary>
		/// CDF Descriptor Record. General information about the CDF.
		/// </summary>
		CDR = 0x00000001,
		/// <summary>
		/// Global Descriptor Record. Additional general information about the CDF.
		/// </summary>
		GDR = 0x00000002,
		/// <summary>
		/// rVariable Descriptor Record. Information about an rVariable.
		/// </summary>
		rVDR = 0x00000003,
		/// <summary>
		/// Attribute Descriptor Record. Information about an attribute.
		/// </summary>
		ADR = 0x00000004,
		/// <summary>
		/// Attribute g/rEntry Descriptor Record. Information about a gEntry or rEntry of an attribute.
		/// </summary>
		AgrEDR = 0x00000005,
		/// <summary>
		/// Variable Index Record. Indexing information for a variable.
		/// </summary>
		VXR = 0x00000006,
		/// <summary>
		/// Variable Values Record. One or more variable records.
		/// </summary>
		VVR = 0x00000007,
		/// <summary>
		/// zVariable Descriptor Record. Information about a zVariable.
		/// </summary>
		zVDR = 0x00000008,
		/// <summary>
		/// Attribute zEntry Descriptor Record. Information about a zEntry of an attribute.
		/// </summary>
		AzEDR = 0x00000009,
		/// <summary>
		/// Compressed CDF Record. Information about a compressed CDF/variable.
		/// </summary>
		CCR = 0x0000000A,
		/// <summary>
		/// Compression Parameters Record. Information about the compression used for a CDF/variable.
		/// </summary>
		CPR = 0x0000000B,
		/// <summary>
		/// Sparseness Parameters Record. Information about the speci ed sparseness array.
		/// </summary>
		SPR = 0x0000000C,
		/// <summary>
		/// Compressed Variable Values Record. Information for the compressed CDF/variable.
		/// </summary>
		CVVR = 0x0000000D,
		/// <summary>
		/// Unused Internal Record. An internal record not currently being used.
		/// </summary>
		UIR = 0xFFFFFFFF
	}
}
