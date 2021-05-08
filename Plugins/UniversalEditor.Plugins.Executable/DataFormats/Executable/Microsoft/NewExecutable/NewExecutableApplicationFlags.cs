//
//  NewExecutableApplicationFlags.cs - describes attributes for a New Executable file
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

namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	/// <summary>
	/// Describes attributes for a New Executable file.
	/// </summary>
	[Flags()]
	public enum NewExecutableApplicationFlags : byte
	{
		/// <summary>
		/// Not aware of Windows/Presentation Manager API
		/// </summary>
		FullScreen = 0x01,
		/// <summary>
		/// Compatible with Windows/Presentation Manager API
		/// </summary>
		PMCompatible = 0x02,
		/// <summary>
		/// Uses Windows/Presentation Manager API
		/// </summary>
		PMRequired = 0x03,

		/// <summary>
		/// Dual-mode (OS/2 and DOS) Family Application Program Interface (FAPI)
		/// </summary>
		FamilyApplication = 0x04,
		/// <summary>
		/// Executable image contains errors.
		/// </summary>
		ImageErrors = 0x10,
		/// <summary>
		/// non-conforming program (valid stack is not maintained)
		/// </summary>
		NonConforming = 0x20,
		/// <summary>
		/// DLL or driver rather than application
		/// (SS:SP info invalid, CS:IP points at FAR init routine called with AX = module handle which returns AX = 0000h on failure,
		/// AX nonzero on successful initialization)
		/// </summary>
		Library = 0x40
	}
}
