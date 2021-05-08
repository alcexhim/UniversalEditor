//
//  PEFContainerHeader.cs - describes the header of a Preferred Executable container
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.PreferredExecutable.Internal
{
	/// <summary>
	/// Describes the header of a Preferred Executable container.
	/// </summary>
	internal struct PEFContainerHeader
	{
		/// <summary>
		/// Designates that the container uses an Apple-defined format. This field must be set to "Joy!" in ASCII.
		/// </summary>
		public string tag1;
		/// <summary>
		/// Identifies the type of container (currently set to "peff" in ASCII).
		/// </summary>
		public string tag2;
		/// <summary>
		/// Indicates the architecture type that the container was generated for. This field holds the ASCII value "pwpc" for the
		/// PowerPC CFM implementation or "m68k" for CFM-68K.
		/// </summary>
		public string architecture;
		/// <summary>
		/// Indicates the version of PEF used in the container. The current version is 1.
		/// </summary>
		public uint formatVersion;
		/// <summary>
		/// Indicates when the PEF container was created. The stamp follows the Macintosh time-measurement scheme (that is, the number
		/// of seconds measured from January 1, 1904).
		/// </summary>
		public uint dateTimeStamp;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint oldDefVersion;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint oldImpVersion;
		/// <summary>
		/// Contains version information that the Code Fragment Manager uses to check shared library compatibility.
		/// </summary>
		public uint currentVersion;
		/// <summary>
		/// Indicates the total number of sections contained in the container.
		/// </summary>
		public ushort sectionCount;
		/// <summary>
		/// Indicates the number of instantiated sections. Instantiated sections contain code or data that are required for execution.
		/// </summary>
		public ushort instSectionCount;
		/// <summary>
		/// Currently reserved and must be set to 0.
		/// </summary>
		public uint reservedA;
	}
}
