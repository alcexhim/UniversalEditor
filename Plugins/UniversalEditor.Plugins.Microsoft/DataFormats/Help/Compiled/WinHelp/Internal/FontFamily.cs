//
//  FontFamily.cs - indicates the type of generic font family used for font formatting in a WinHelp file
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
	/// Indicates the type of generic font family used for font formatting in a WinHelp file. WARNING: This is a different order than
	/// FF_ROMAN, FF_SWISS, etc. of Windows!
	/// </summary>
	internal enum FontFamily : byte
	{
		Modern = 0x01,
		Roman = 0x02,
		Swiss = 0x03,
		Tech = 0x03,
		Nil = 0x03,
		Script = 0x04,
		Decor = 0x05
	}
}
