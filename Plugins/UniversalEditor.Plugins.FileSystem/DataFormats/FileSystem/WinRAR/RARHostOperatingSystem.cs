//
//  RARHostOperatingSystem.cs - indicates the operating system on which the RAR file was created
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

namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	/// <summary>
	/// Indicates the operating system on which the RAR file was created.
	/// </summary>
	public enum RARHostOperatingSystem : byte
	{
		MSDOS = 0,
		OS2 = 1,
		Windows = 2,
		Unix = 3,
		MacOS = 4,
		BeOS = 5
	}
}
