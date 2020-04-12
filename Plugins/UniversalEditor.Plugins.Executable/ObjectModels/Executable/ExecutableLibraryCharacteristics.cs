//
//  ExecutableLibraryCharacteristics.cs - indicates attributes of a library
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

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Indicates attributes of a library.
	/// </summary>
	[Flags()]
	public enum ExecutableLibraryCharacteristics : ushort
	{
		/// <summary>
		/// No library characteristics have been specified.
		/// </summary>
		None = 0x0000,
		/// <summary>
		/// Call when DLL is first loaded into a process's address space.
		/// </summary>
		CallUponLoad = 0x0001,
		/// <summary>
		/// Call when a thread terminates.
		/// </summary>
		CallUponThreadTerminate = 0x0002,
		/// <summary>
		/// Call when a thread starts up.
		/// </summary>
		CallUponThreadStart = 0x0004,
		/// <summary>
		/// Call when DLL exits.
		/// </summary>
		CallUponLibraryExit = 0x0008,
		/// <summary>
		/// The DLL can be relocated at load time.
		/// </summary>
		DynamicBase = 0x0040,
		/// <summary>
		/// Code integrity checks are forced. If you set this flag and a section contains only
		/// uninitialized data, set the PointerToRawData member of IMAGE_SECTION_HEADER for that
		/// section to zero; otherwise, the image will fail to load because the digital signature
		/// cannot be verified.
		/// </summary>
		ForceCodeIntegrityChecks = 0x0080,
		/// <summary>
		/// The image is compatible with data execution prevention (DEP), the NX bit.
		/// </summary>
		DataExecutionPreventionCompatible = 0x0100,
		/// <summary>
		/// The image is isolation aware, but should not be isolated.
		/// </summary>
		NoIsolation = 0x0200,
		/// <summary>
		/// The image does not use structured exception handling (SEH). No handlers can be called in
		/// this image.
		/// </summary>
		NoStructuredExceptionHandling = 0x0400,
		/// <summary>
		/// Do not bind the image.
		/// </summary>
		NoBinding = 0x0800,
		/// <summary>
		/// Reserved.
		/// </summary>
		Reserved4096 = 0x1000,
		/// <summary>
		/// A WDM driver.
		/// </summary>
		WDMDriver = 0x2000,
		/// <summary>
		/// Reserved.
		/// </summary>
		Reserved16384 = 0x4000,
		/// <summary>
		/// The image is terminal server aware.
		/// </summary>
		TerminalServerAware = 0x8000
	}
}
