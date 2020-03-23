
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public enum XCOFFSectionType32 : ushort
	{
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0000 = 0x0000,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0001 = 0x0001,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0002 = 0x0002,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0004 = 0x0004,
		/// <summary>
		/// Specifies a pad section. A section of this type is used to provide alignment padding between sections within an XCOFF executable object file. This section header type is obsolete since padding is allowed in an XCOFF file without a corresponding pad section header.
		/// </summary>
		STYP_PAD = 0x0008,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0010 = 0x0010,
		/// <summary>
		/// Specifies an executable text (code) section. A section of this type contains the executable instructions of a program.
		/// </summary>
		STYP_TEXT = 0x0020,
		/// <summary>
		/// Specifies an initialized data section. A section of this type contains the initialized data and the TOC of a program.
		/// </summary>
		STYP_DATA = 0x0040,
		/// <summary>
		/// Specifies an uninitialized data section. A section header of this type defines the uninitialized data of a program.
		/// </summary>
		STYP_BSS = 0x0080,
		/// <summary>
		/// Specifies an exception section. A section of this type provides information to identify the reason that a trap or exception occurred within an executable object program.
		/// </summary>
		STYP_EXCEPT = 0x0100,
		/// <summary>
		/// Specifies a comment section. A section of this type provides comments or data to special processing utility programs.
		/// </summary>
		STYP_INFO = 0x0200,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0400 = 0x0400,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0800 = 0x0800,
		/// <summary>
		/// Specifies a loader section. A section of this type contains object file information for the system loader to load an XCOFF executable. The information includes imported symbols, exported symbols, relocation data, type-check information, and shared object names.
		/// </summary>
		STYP_LOADER = 0x1000,
		/// <summary>
		/// Specifies a debug section. A section of this type contains stabstring information used by the symbolic debugger.
		/// </summary>
		STYP_DEBUG = 0x2000,
		/// <summary>
		/// Specifies a type-check section. A section of this type contains parameter/argument type-check strings used by the binder.
		/// </summary>
		STYP_TYPCHK = 0x4000,
		/// <summary>
		/// Specifies a relocation or line-number field overflow section. A section header of this type contains the count of relocation entries and line number entries for some other section. This section header is required when either of the counts exceeds 65,534. See the s_nreloc and s_nlnno fields in "Sections and Section Headers" for more information on overflow headers.
		/// </summary>
		/// <remarks>An XCOFF64 file may not contain an overflow section header.</remarks>
		STYP_OVRFLO = 0x8000
	}
	public enum XCOFFSectionType64 : uint
	{
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0000 = 0x0000,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0001 = 0x0001,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0002 = 0x0002,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0004 = 0x0004,
		/// <summary>
		/// Specifies a pad section. A section of this type is used to provide alignment padding between sections within an XCOFF executable object file. This section header type is obsolete since padding is allowed in an XCOFF file without a corresponding pad section header.
		/// </summary>
		STYP_PAD = 0x0008,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0010 = 0x0010,
		/// <summary>
		/// Specifies an executable text (code) section. A section of this type contains the executable instructions of a program.
		/// </summary>
		STYP_TEXT = 0x0020,
		/// <summary>
		/// Specifies an initialized data section. A section of this type contains the initialized data and the TOC of a program.
		/// </summary>
		STYP_DATA = 0x0040,
		/// <summary>
		/// Specifies an uninitialized data section. A section header of this type defines the uninitialized data of a program.
		/// </summary>
		STYP_BSS = 0x0080,
		/// <summary>
		/// Specifies an exception section. A section of this type provides information to identify the reason that a trap or exception occurred within an executable object program.
		/// </summary>
		STYP_EXCEPT = 0x0100,
		/// <summary>
		/// Specifies a comment section. A section of this type provides comments or data to special processing utility programs.
		/// </summary>
		STYP_INFO = 0x0200,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0400 = 0x0400,
		/// <summary>
		/// Reserved.
		/// </summary>
		RESERVED_0800 = 0x0800,
		/// <summary>
		/// Specifies a loader section. A section of this type contains object file information for the system loader to load an XCOFF executable. The information includes imported symbols, exported symbols, relocation data, type-check information, and shared object names.
		/// </summary>
		STYP_LOADER = 0x1000,
		/// <summary>
		/// Specifies a debug section. A section of this type contains stabstring information used by the symbolic debugger.
		/// </summary>
		STYP_DEBUG = 0x2000,
		/// <summary>
		/// Specifies a type-check section. A section of this type contains parameter/argument type-check strings used by the binder.
		/// </summary>
		STYP_TYPCHK = 0x4000
	}

}
