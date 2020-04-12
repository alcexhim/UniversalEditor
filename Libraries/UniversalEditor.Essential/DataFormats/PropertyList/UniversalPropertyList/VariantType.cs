//
//  VariantType.cs - indicates the type of a variant expression in a Universal Property List file
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

namespace UniversalEditor.DataFormats.PropertyList.UniversalPropertyList
{
	/// <summary>
	/// Indicates the type of a variant expression in a Universal Property List file.
	/// </summary>
	public enum VariantType : byte
	{
		Null = 0,
		Array = 1,
		Boolean = 2,
		Byte = 3,
		Char = 4,
		DateTime = 5,
		Decimal = 6,
		Double = 7,
		Guid = 8,
		Int16 = 9,
		Int32 = 10,
		Int64 = 11,
		Object = 12,
		SByte = 13,
		Single = 14,
		String = 15,
		UInt16 = 16,
		UInt32 = 17,
		UInt64 = 18
	}
}
