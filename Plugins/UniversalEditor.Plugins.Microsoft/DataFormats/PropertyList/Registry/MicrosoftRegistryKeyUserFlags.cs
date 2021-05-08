//
//  MicrosoftRegistryKeyUserFlags.cs - indicates user attributes on a registry key in a Microsoft registry file
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
	/// Indicates user attributes on a registry key in a Microsoft registry file.
	/// </summary>
	[Flags()]
	public enum MicrosoftRegistryKeyUserFlags
	{
		/// <summary>
		/// Is a 32-bit key: this key was created through the Wow64 subsystem or this key
		/// shall not be used by a 64-bit program (e.g. by a 64-bit driver during the boot)
		/// </summary>
		Wow64Key = 0x1,
		/// <summary>
		/// This key was created by the reflection process (when reflecting a key from another view)
		/// </summary>
		/// <remarks>
		/// In Windows 7, Windows Server 2008 R2, and more recent versions of Windows, the
		/// bit mask 0x1 isn't used to mark 32-bit keys created by userspace programs.
		/// </remarks>
		Reflection = 0x2,
		/// <summary>
		/// Disable registry reflection for this key
		/// </summary>
		DisableReflection = 0x4,
		/// <summary>
		/// In the old location of the User flags field: Execute the int 3 instruction on
		/// an access to this key (both retail and checked Windows kernels); this bit was
		/// superseded by the Debug field (see below).
		///
		/// In the new location of the User flags field: disable registry reflection for
		/// this key if a corresponding key exists in another view and it wasn't created by
		/// a caller (see below)
		/// </summary>
		/// <remarks>
		/// The bit mask 0x8 was supported in the new location of the User flags field only
		/// in pre-release versions of Windows Vista, e.g. beta 2.
		/// </remarks>
		BreakOrDisableReflectionIfExists = 0x8
	}
}
