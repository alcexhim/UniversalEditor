//
//  MachOFlags.cs - indicates special attributes for an Apple Mach-O executable file
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
	/// Indicates special attributes for an Apple Mach-O executable file.
	/// </summary>
	[Flags()]
	public enum MachOFlags
	{
		None = 0x000000,
		/// <summary>
		/// The object file contained no undefined references when it was built.
		/// </summary>
		NoUndefinedReferences = 0x000001,
		/// <summary>
		/// The object file is the output of an incremental link against a base file and cannot be
		/// linked again.
		/// </summary>
		IncrementalLink = 0x000002,
		/// <summary>
		/// The file is input for the dynamic linker and cannot be statically linked again.
		/// </summary>
		DynamicLink = 0x000004,
		/// <summary>
		/// The dynamic linker should bind the undefined references when the file is loaded.
		/// </summary>
		BindAtLoad = 0x000008,
		/// <summary>
		/// The file’s undefined references are prebound.
		/// </summary>
		Prebound = 0x000010,
		/// <summary>
		/// The file has its read-only and read-write segments split.
		/// </summary>
		SplitSegments = 0x000020,
		LazyInitialization = 0x000040,
		/// <summary>
		/// The image is using two-level namespace bindings.
		/// </summary>
		TwoLevelNamespace = 0x000080,
		/// <summary>
		/// The executable is forcing all images to use flat namespace bindings.
		/// </summary>
		ForceFlatNamespace = 0x000100,
		/// <summary>
		/// This umbrella guarantees there are no multiple definitions of symbols in its subimages.
		/// As a result, the two-level namespace hints can always be used.
		/// </summary>
		NoMultipleDefinitions = 0x000200,
		/// <summary>
		/// The dynamic linker doesn’t notify the prebinding agent about this executable.
		/// </summary>
		NoFixPrebinding = 0x000400,
		/// <summary>
		/// This file is not prebound but can have its prebinding redone. Used only when
		/// Prebound is not set.
		/// </summary>
		Prebindable = 0x000800,
		/// <summary>
		/// Indicates that this binary binds to all two-level namespace modules of its dependent
		/// libraries. Used only when Prebindable and TwoLevelNamespace are set.
		/// </summary>
		AllModulesBound = 0x001000,
		/// <summary>
		/// This file has been canonicalized by unprebinding—clearing prebinding information from
		/// the file. See the redo_prebinding man page for details.
		/// </summary>
		Canonicalized = 0x004000,
		WeakDefines = 0x008000,
		BindsToWeak = 0x010000,
		RootSafe = 0x040000,
		SetUIDSafe = 0x080000,
		NoReExportedDynamicLibraries = 0x100000,
		PIE = 0x200000,
		/// <summary>
		/// The sections of the object file can be divided into individual blocks. These blocks are
		/// dead-stripped if they are not used by other code. See Linking for details.
		/// </summary>
		SubsectionsViaSymbols = 0x0000
	}
}
