//
//  NewExecutableTargetOperatingSystem.cs - enumeration specifying the target operating system for a New Executable file
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

namespace UniversalEditor.DataFormats.Executable.Microsoft.NewExecutable
{
	/// <summary>
	/// An enumeration specifying the target operating system for a New Executable file.
	/// </summary>
	public enum NewExecutableTargetOperatingSystem : byte
	{
		/// <summary>
		/// Unknown
		/// </summary>
		Unknown = 0x00,
		/// <summary>
		/// OS/2
		/// </summary>
		OS2 = 0x01,
		/// <summary>
		/// Windows
		/// </summary>
		Windows = 0x02,
		/// <summary>
		/// European MS-DOS 4.x
		/// </summary>
		EuropeanMSDOS4x = 0x03,
		/// <summary>
		/// Windows/386
		/// </summary>
		Windows386 = 0x04,
		/// <summary>
		/// BOSS (Borland Operating System Services)
		/// </summary>
		Borland = 0x05,
		/// <summary>
		/// PharLap 286|DOS-Extender, OS/2
		/// </summary>
		PharLapOS2 = 0x81,
		/// <summary>
		/// PharLap 286|DOS-Extender, Windows
		/// </summary>
		PharLapWindows = 0x82
	}
}
