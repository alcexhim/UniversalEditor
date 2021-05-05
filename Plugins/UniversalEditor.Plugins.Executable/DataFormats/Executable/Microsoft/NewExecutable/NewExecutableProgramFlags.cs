//
//  NewExecutableProgramFlags.cs - describes program flags for a New Executable file
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
	/// Describes program flags for a New Executable file.
	/// </summary>
	[Flags()]
	public enum NewExecutableProgramFlags : byte
	{
		None = 0x00,
		/// <summary>
		/// DGROUP type: single shared.  An executable file with this format
		/// contains one data segment. This bit is set if the file is a
		/// dynamic-link library (DLL).
		/// </summary>
		/// <remarks>
		/// If neither <see cref="SingleSharedDGROUP" /> nor
		/// <see cref="MultipleUnsharedDGROUP" /> is set, the
		/// executable-file format is NOAUTODATA. An
		/// executable file with this format does not contain
		/// an automatic data segment.
		/// </remarks>
		SingleSharedDGROUP = 0x01,
		/// <summary>
		/// DGROUP type: multiple (unshared). An executable file with
		/// this format contains multiple data segments. This
		/// bit is set if the file is a Windows application.
		/// </summary>
		/// <remarks>
		/// If neither <see cref="SingleSharedDGROUP" /> nor
		/// <see cref="MultipleUnsharedDGROUP" /> is set, the
		/// executable-file format is NOAUTODATA. An
		/// executable file with this format does not contain
		/// an automatic data segment.
		/// </remarks>
		MultipleUnsharedDGROUP = 0x02,
		/// <summary>
		/// Global initialization.
		/// </summary>
		GlobalInitialization = 0x04,
		/// <summary>
		/// Protected mode only.
		/// </summary>
		ProtectedModeOnly = 0x08,
		/// <summary>
		/// 8086 instructions
		/// </summary>
		Instructions8086 = 0x10,
		/// <summary>
		/// 80286 instructions
		/// </summary>
		Instructions80286 = 0x20,
		/// <summary>
		/// 80386 instructions
		/// </summary>
		Instructions80386 = 0x40,
		/// <summary>
		/// 80x87 instructions
		/// </summary>
		Instructions80x87 = 0x80
	}
}
