//
//  ARJHostOperatingSystem.cs - indicates the host operating system that created the ARJ file
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

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	/// <summary>
	/// Indicates the host operating system that created the ARJ file.
	/// </summary>
	public enum ARJHostOperatingSystem : byte
	{
		DOS = 0,
		PrimOS = 1,
		Unix = 2,
		Amiga = 3,
		MacOS = 4,
		OS2 = 5,
		AppleGS = 6,
		AtariST = 7,
		NeXT = 8,
		VaxVMS = 9,
		Windows = 11
	}
}
