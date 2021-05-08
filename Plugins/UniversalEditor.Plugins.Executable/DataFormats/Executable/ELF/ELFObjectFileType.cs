//
//  ELFObjectFileType.cs - identifies the object file type for an Executable and Linkable Format (ELF) executable
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

namespace UniversalEditor.DataFormats.Executable.ELF
{
	/// <summary>
	/// Identifies the object file type for an Executable and Linkable Format (ELF) executable.
	/// </summary>
	public enum ELFObjectFileType : ushort
	{
		/// <summary>
		/// No file type
		/// </summary>
		None = 0,
		/// <summary>
		/// Relocatable file
		/// </summary>
		Relocatable = 1,
		/// <summary>
		/// Executable file
		/// </summary>
		Executable = 2,
		/// <summary>
		/// Shared object file
		/// </summary>
		SharedObject = 3,
		/// <summary>
		/// Core file
		/// </summary>
		Core = 4,
		/// <summary>
		/// Processor-specific
		/// </summary>
		ProcessorSpecificFF00 = 0xFF00,
		/// <summary>
		/// Processor-specific
		/// </summary>
		ProcessorSpecificFFFF = 0xFFFF
	}
}
