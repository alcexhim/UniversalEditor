//
//  MachOVMProtection.cs - specifies protection attributes for the VM in an Apple Mach-O executable
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

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Specifies protection attributes for the VM in an Apple Mach-O executable.
	/// </summary>
	[Flags()]
	public enum MachOVMProtection : uint
	{
		None = 0x00,
		/// <summary>
		/// Read permission
		/// </summary>
		Read = 0x01,
		/// <summary>
		/// Write permission
		/// </summary>
		Write = 0x02,
		/// <summary>
		/// Execute permission
		/// </summary>
		Execute = 0x04,

		/// <summary>
		/// The default protection for newly created virtual memory
		/// </summary>
		Default = (Read | Write | Execute),

		/// <summary>
		/// Maximum privileges possible, for parameter checking.
		/// </summary>
		All = (Read | Write | Execute)
	}
}
