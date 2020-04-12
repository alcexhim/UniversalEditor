//
//  XCOFFDocumentFlags.cs - describes attributes for an Extended Common Object File Format executable
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

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	/// <summary>
	/// Describes attributes for an Extended Common Object File Format executable.
	/// </summary>
	[Flags()]
	public enum XCOFFDocumentFlags
	{
		F_NONE = 0x0000,
		/// <summary>
		/// Indicates that the relocation information for binding has been removed from the file. This flag must not be set by compilers, even if relocation information was not required.
		/// </summary>
		F_RELFLG = 0x0001,
		/// <summary>
		/// Indicates that the file is executable. No unresolved external references exist.
		/// </summary>
    	F_EXEC = 0x0002,
		/// <summary>
		/// Indicates that line numbers have been stripped from the file by a utility program. This flag is not set by compilers, even if no line-number information has been generated.
		/// </summary>
	    F_LNNO = 0x0004,
		/// <summary>
		/// Reserved.
		/// </summary>
		F_RESERVED_0008 = 0x0008,
		/// <summary>
		/// Indicates that the file was profiled with the fdpr command.
		/// </summary>
		F_FDPR_PROF = 0x0010,
		/// <summary>
		/// Indicates that the file was reordered with the fdpr command.
		/// </summary>
    	F_FDPR_OPTI = 0x0020,
		/// <summary>
		/// Indicates that the file uses Very Large Program Support.
		/// </summary>
    	F_DSA = 0x0040,
		/// <summary>
		/// Reserved.
		/// </summary>
		F_RESERVED_0080 = 0x0080,
    	/// <summary>
    	/// Reserved.
    	/// </summary>
		F_RESERVED_0100 = 0x0100,
		/// <summary>
		/// Reserved.
		/// </summary>
    	F_RESERVED_0200 = 0x0200,
		/// <summary>
		/// Reserved.
		/// </summary>
		F_RESERVED_0400 = 0x0400,
		/// <summary>
		/// Reserved.
		/// </summary>
    	F_RESERVED_0800 = 0x0800,
		/// <summary>
		/// Indicates the file is dynamically loadable and executable. External references are resolved by way of imports, and the file might contain exports and loader relocation.
		/// </summary>
    	F_DYNLOAD = 0x1000,
		/// <summary>
		/// Indicates the file is a shared object (shared library). The file is separately loadable. That is, it is not normally bound with other objects, and its loader exports symbols are used as automatic import symbols for other object files.
		/// </summary>
    	F_SHROBJ = 0x2000,
		/// <summary>
		/// If the object file is a member of an archive, it can be loaded by the system loader, but the member is ignored by the binder. If the object file is not in an archive, this flag has no effect.
		/// </summary>
    	F_LOADONLY = 0x4000,
		/// <summary>
		/// Reserved.
		/// </summary>
		F_RESERVED_8000 = 0x8000
    
	}
}
