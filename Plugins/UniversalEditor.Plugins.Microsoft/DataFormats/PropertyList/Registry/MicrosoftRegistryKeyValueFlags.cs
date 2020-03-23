//
//  MicrosoftRegistryKeyValueFlags.cs
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
	[Flags()]
	public enum MicrosoftRegistryKeyValueFlags : short
	{
		/// <summary>
		/// Value name is an ASCII string, possibly an extended ASCII string (otherwise it is a UTF-16LE string)
		/// </summary>
		CompatibleName = 0x0001,
		/// <summary>
		/// Is a tombstone value (the flag is used starting from Insider Preview builds of Windows 10 "Redstone 1"), a tombstone value also has the Data type field set to REG_NONE, the Data size field set to 0, and the Data offset field set to 0xFFFFFFFF
		/// </summary>
		Tombstone = 0x0002
	}
}
