//
//  FontAttributes.cs - indicates the attributes for font formatting in a WinHelp file
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

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Indicates the attributes for font formatting in a WinHelp file.
	/// </summary>
	[Flags()]
	internal enum FontAttributes : byte
	{
		None = 0x00,
		Bold = 0x01,
		Italic = 0x02,
		Underline = 0x04,
		Strikeout = 0x08,
		DoubleUnderline = 0x10,
		SmallCaps = 0x20
	}
}
