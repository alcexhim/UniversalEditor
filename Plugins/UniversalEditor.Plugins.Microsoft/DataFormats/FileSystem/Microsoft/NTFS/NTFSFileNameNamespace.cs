//
//  NTFSFileNameNamespace.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.DataFormats.FileSystem.Microsoft.NTFS
{
	public enum NTFSFileNameNamespace : byte
	{
		/// <summary>
		/// Case sensitive character set that consists of all Unicode
		/// characters except for: \0 (zero character), / (forward slash).
		/// The : (colon) is valid for NTFS but not for Windows.
		/// </summary>
		POSIX = 0,
		/// <summary>
		/// A case insensitive sub set of the <see cref="POSIX" /> character
		/// set that consists of all Unicode characters except for:
		/// " * / : &lt; &gt; ? \ |
		/// Note that names cannot end with a . (dot) or ' ' (space).
		/// </summary>
		Windows = 1,
		/// <summary>
		/// A case insensitive sub set of the <see cref="Windows" />
		/// character set that consists of all upper case ASCII characters
		/// except for:
		/// " * + , / : ; &lt; = &gt; ? \
		/// </summary>
		DOS = 2,
		/// <summary>
		/// Both the <see cref="DOS" /> and <see cref="Windows" /> names are
		/// identical. Which is the same as the DOS character set, with the
		/// exception that lower case is used as well.
		/// </summary>
		DOSWindows = 3
	}
}
