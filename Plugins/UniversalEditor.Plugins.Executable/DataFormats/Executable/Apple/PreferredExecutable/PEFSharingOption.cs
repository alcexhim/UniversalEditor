//
//  PEFSharingOption.cs - specifies how a section should be shared in a Preferred Executable file
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
	/// Specifies how a section should be shared in a Preferred Executable file.
	/// </summary>
	public enum PEFSharingOption : byte
	{
		/// <summary>
		/// Indicates that the section is shared within a process, but a fresh copy is created for different processes.
		/// </summary>
		Process = 1,
		/// <summary>
		/// Indicates that the section is shared between all processes in the system.
		/// </summary>
		Global = 4,
		/// <summary>
		/// Indicates that the section is shared between all processes, but is protected. Protected sections are read/write in privileged mode and read-only in user mode. This option is not
		/// available in System 7.
		/// </summary>
		Protected = 5
	}
}
