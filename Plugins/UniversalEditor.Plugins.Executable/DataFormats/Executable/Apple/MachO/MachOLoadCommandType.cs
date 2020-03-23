using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	public enum MachOLoadCommandType : uint
	{
		/// <summary>
		/// File segment to be mapped
		/// </summary>
		Segment = 0x00000001,
		/// <summary>
		/// Link-edit stab symbol table info (obsolete)
		/// </summary>
		SymbolTable = 0x00000002,
		/// <summary>
		/// Link-edit gdb symbol table info
		/// </summary>
		SymbolSegment = 0x00000003,
		/// <summary>
		/// Thread
		/// </summary>
		Thread = 0x00000004,
		/// <summary>
		/// UNIX thread (includes a stack)
		/// </summary>
		UnixThread = 0x00000005,
		/// <summary>
		/// Load a fixed VM shared library
		/// </summary>
		LoadFixedVMSharedLibrary = 0x00000006,
		/// <summary>
		/// Fixed VM shared library id
		/// </summary>
		FixedVMSharedLibraryID = 0x00000007,
		/// <summary>
		/// Object identification information (obsolete)
		/// </summary>
		ObjectIdentification = 0x00000008,
		/// <summary>
		/// Fixed VM file inclusion
		/// </summary>
		FixedVMFileInclusion = 0x00000009

		/*

  BFD_MACH_O_LC_PREPAGE = 0xa,		// Prepage command (internal use).
  BFD_MACH_O_LC_DYSYMTAB = 0xb,		// Dynamic link-edit symbol table info.
  BFD_MACH_O_LC_LOAD_DYLIB = 0xc,	// Load a dynamically linked shared library.
  BFD_MACH_O_LC_ID_DYLIB = 0xd,		// Dynamically linked shared lib identification.
  BFD_MACH_O_LC_LOAD_DYLINKER = 0xe,	// Load a dynamic linker.
  BFD_MACH_O_LC_ID_DYLINKER = 0xf,	// Dynamic linker identification.
  BFD_MACH_O_LC_PREBOUND_DYLIB = 0x10,	// Modules prebound for a dynamically.
  BFD_MACH_O_LC_ROUTINES = 0x11,	// Image routines.
  BFD_MACH_O_LC_SUB_FRAMEWORK = 0x12,	// Sub framework.
  BFD_MACH_O_LC_SUB_UMBRELLA = 0x13,	// Sub umbrella.
  BFD_MACH_O_LC_SUB_CLIENT = 0x14,	// Sub client.
  BFD_MACH_O_LC_SUB_LIBRARY = 0x15,   	// Sub library.
  BFD_MACH_O_LC_TWOLEVEL_HINTS = 0x16,	// Two-level namespace lookup hints.
  BFD_MACH_O_LC_PREBIND_CKSUM = 0x17, 	// Prebind checksum.
  // Load a dynamically linked shared library that is allowed to be missing (weak).
  BFD_MACH_O_LC_LOAD_WEAK_DYLIB = 0x18 | BFD_MACH_O_LC_REQ_DYLD,
		 */


	}
}
