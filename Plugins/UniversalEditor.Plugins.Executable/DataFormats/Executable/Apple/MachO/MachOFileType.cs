//
//  MachOFileType.cs - indicates the type of Apple Mach-O executable file in use
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

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Indicates the type of Apple Mach-O executable file in use.
	/// </summary>
	public enum MachOFileType
	{
		/// <summary>
		/// Used for intermediate object files. It is a very compact format containing all its
		/// sections in one segment. The compiler and assembler usually create one Object file
		/// for each source code file. By convention, the file name extension for this format
		/// is .o.
		/// </summary>
		Object = 0x1,
		/// <summary>
		/// Used by standard executable programs.
		/// </summary>
		Executable = 0x2,
		FVMLibrary = 0x3,
		/// <summary>
		/// Used to store core files, which are traditionally created when a program crashes. Core
		/// files store the entire address space of a process at the time it crashed. You can later
		/// run gdb on the core file to figure out why the crash occurred.
		/// </summary>
		Core = 0x4,
		/// <summary>
		/// Used for special-purpose programs that are not loaded by the OS X kernel, such as
		/// programs burned into programmable ROM chips. Do not confuse this file type with the
		/// <see cref="Prebound"/> flag, which is a flag that the static linker sets in the header
		/// structure to mark a prebound image.
		/// </summary>
		Preload = 0x5,
		/// <summary>
		/// For dynamic shared libraries. It contains some additional tables to support multiple
		/// modules. By convention, the file name extension for this format is .dylib, except for
		/// the main shared library of a framework, which does not usually have a file name
		/// extension.
		/// </summary>
		DynamicLibrary = 0x6,
		/// <summary>
		/// The type of a dynamic linker shared library. This is the type of the dyld file.
		/// </summary>
		DynamicLinkerLibrary = 0x7,
		/// <summary>
		/// Typically used by code that you load at runtime (typically called bundles or plug-ins).
		/// By convention, the file name extension for this format is .bundle.
		/// </summary>
		Bundle = 0x8,
		DynamicLibraryStub = 0x9,
		/// <summary>
		/// Designates files that store symbol information for a corresponding binary file.
		/// </summary>
		DebuggingSymbols = 0xA
	}
}
