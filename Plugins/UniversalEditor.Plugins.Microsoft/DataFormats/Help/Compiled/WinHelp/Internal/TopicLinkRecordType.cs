//
//  TopicLinkRecordType.cs - indicates the type of topic link record present in a WinHelp file
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

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Indicates the type of topic link record present in a WinHelp file.
	/// </summary>
	internal enum TopicLinkRecordType : byte
	{
		/// <summary>
		/// Version 3.0 displayable information
		/// </summary>
		Display30 = 0x01,
		/// <summary>
		/// Topic header information
		/// </summary>
		TopicHeader = 0x02,
		/// <summary>
		/// Version 3.1 displayable information
		/// </summary>
		Display31 = 0x20,
		/// <summary>
		/// Version 3.1 table
		/// </summary>
		Table31 = 0x23
	}
}
