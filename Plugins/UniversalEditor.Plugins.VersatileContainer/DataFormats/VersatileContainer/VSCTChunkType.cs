//
//  VSCTChunkType.cs
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
namespace UniversalEditor.DataFormats.VersatileContainer
{
	public enum VSCTChunkType
	{
		Header = 0xC0,
		Schema = 0xC5,
		End = 0xCF,

		FieldSignedByte = 0x51,
		FieldUInt16 = 0x52,
		FieldUInt32 = 0x54,
		FieldUInt64 = 0x58,

		FieldData = 0xF0,
		FieldByte = 0xF1,
		FieldShort = 0xF2,
		FieldInt32 = 0xF4,
		FieldInt64 = 0xF8,
		FieldString = 0xF5,
		FieldDouble = 0xFD,
		FieldFloat = 0xFF
	}
}
