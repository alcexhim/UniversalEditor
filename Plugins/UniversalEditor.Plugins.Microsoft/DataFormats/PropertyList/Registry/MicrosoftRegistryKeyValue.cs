//
//  MicrosoftRegistryKeyValue.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	public struct MicrosoftRegistryKeyValue
	{
		/// <summary>
		/// In bytes, can be 0 (name isn't set)
		/// </summary>
		public short NameLength;
		/// <summary>
		/// In bytes, can be 0 (value isn't set), the most significant bit has a special meaning (see below)
		/// </summary>
		public int DataSize;
		/// <summary>
		/// In bytes, relative from the start of the hive bins data (or data itself, see below)
		/// </summary>
		public int DataOffset;
		public MicrosoftRegistryKeyValueDataType DataType;
		public MicrosoftRegistryKeyValueFlags Flags;
		public short Spare;
		public string Name;

		public override string ToString()
		{
			return Name + " @ " + DataOffset.ToString();
		}
	}
}
