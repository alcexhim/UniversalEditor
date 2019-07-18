//
//  MicrosoftRegistryKeyDebugFlags.cs
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
	/// <summary>
	/// When the CmpRegDebugBreakEnabled kernel variable is set to 1, a checked Windows
	/// kernel will execute the int 3 instruction on various events according to the bit
	/// mask in the Debug field. A retail Windows kernel has this feature disabled.
	/// </summary>
	public enum MicrosoftRegistryKeyDebugFlags
	{
		/// <summary>
		/// This key is opened
		/// </summary>
		BreakOnOpen = 0x01,
		/// <summary>
		/// This key is deleted
		/// </summary>
		BreakOnDelete = 0x02,
		/// <summary>
		/// A key security item is changed for this key
		/// </summary>
		BreakOnSecurityChange = 0x04,
		/// <summary>
		/// A subkey of this key is created
		/// </summary>
		BreakOnCreateSubkey = 0x08,
		/// <summary>
		/// A subkey of this key is deleted
		/// </summary>
		BreakOnDeleteSubkey = 0x10,
		/// <summary>
		/// A value is set to this key
		/// </summary>
		BreakOnSetValue = 0x20,
		/// <summary>
		/// A value is deleted from this key
		/// </summary>
		BreakOnDeleteValue = 0x40,
		/// <summary>
		/// This key is virtualized
		/// </summary>
		BreakOnKeyVirtualize = 0x80
	}
}
