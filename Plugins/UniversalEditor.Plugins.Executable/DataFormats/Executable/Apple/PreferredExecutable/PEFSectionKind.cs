//
//  PEFSectionKind.cs - indicates the type of section in a Preferred Executable file
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

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable
{
	/// <summary>
	/// Indicates the type of section in a Preferred Executable file.
	/// </summary>
	public enum PEFSectionKind : byte
	{
		/// <summary>
		/// Contains read-only executable code in an uncompressed binary format. A container can have any number of code sections. Code sections are always shared.
		/// </summary>
		Code = 0,
		/// <summary>
		/// Contains uncompressed, initialized, read/write data followed by zero-initialized read/write data. A container can have any number of data sections, each with a different sharing
		/// option.
		/// </summary>
		UnpackedData = 1,
		/// <summary>
		/// Contains read/write data initialized by a pattern specification contained in the section's contents. The contents essentially contain a small program that tells the Code Fragment
		/// Manager how to initialize the raw data in memory. A container can have any number of pattern-initialized data sections, each with its own sharing option. See "Pattern-Initialized
		/// Data" (page 8-10) for more information about creating pattern specifications.
		/// </summary>
		PatternInitializedData = 2,
		/// <summary>
		/// Contains uncompressed, initialized, read-only data. A container can have any number of constant sections, and they are implicitly shared.
		/// </summary>
		Constant = 3,
		/// <summary>
		/// Contains information about imports, exports, and entry points. See "The Loader Section" (page 8-15) for more details. A container can have only one loader section.
		/// </summary>
		Loader = 4,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Debug = 5,
		/// <summary>
		/// Contains information that is both executable and modifiable. For example, this section can store code that contains embedded data. A container can have any number of executable data
		/// sections, each with a different sharing option.
		/// </summary>
		ExecutableData = 6,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Exception = 7,
		/// <summary>
		/// Reserved for future use.
		/// </summary>
		Traceback = 8
	}
}
