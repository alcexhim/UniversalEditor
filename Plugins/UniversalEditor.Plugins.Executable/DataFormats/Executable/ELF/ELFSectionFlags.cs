//
//  ELFSectionFlags.cs - describes attributes of a section in an Executable and Linkable Format (ELF) executable
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

namespace UniversalEditor.DataFormats.Executable.ELF
{
	/// <summary>
	/// Describes attributes of a section in an Executable and Linkable Format (ELF) executable.
	/// </summary>
	[Flags()]
	public enum ELFSectionFlags : uint
	{
		/// <summary>
		/// The section contains data that should be writable during process execution.
		/// </summary>
		Writable = 0x1,
		/// <summary>
		/// The section occupies memory during process execution. Some control sections do
		/// not reside in the memory image of an object file; this attribute is off for
		/// those sections.
		/// </summary>
		Allocated = 0x2,
		/// <summary>
		/// The section contains executable machine instructions.
		/// </summary>
		Executable = 0x4,
		/// <summary>
		/// All bits included in this mask are reserved for processor-specific semantics.
		/// </summary>
		ProcessorSpecificMask = 0xF0000000
	}
}
