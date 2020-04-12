//
//  MicrosoftRegistryKeyVirtualizationFlags.cs - indicates how to virtualize a registry key in a Microsoft registry file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
	/// Indicates how to virtualize a registry key in a Microsoft registry file.
	/// </summary>
	[Flags()]
	public enum MicrosoftRegistryKeyVirtualizationFlags
	{
		/// <summary>
		/// Disable registry write virtualization for this key
		/// </summary>
		DontVirtualize = 0x2,
		/// <summary>
		/// Disable registry open virtualization for this key
		/// </summary>
		DontSilentFail = 0x4,
		/// <summary>
		/// Propagate virtualization flags to new child keys (subkeys)
		/// </summary>
		RecurseFlag = 0x8
	}
}
