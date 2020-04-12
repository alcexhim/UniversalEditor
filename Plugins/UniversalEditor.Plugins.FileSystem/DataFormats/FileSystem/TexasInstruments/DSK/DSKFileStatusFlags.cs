//
//  DSKFileStatusFlags.cs - indicates status attributes for a file on a Texas Instruments DSK file system
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
namespace UniversalEditor.DataFormats.FileSystem.TexasInstruments.DSK
{
	/// <summary>
	/// Indicates status attributes for a file on a Texas Instruments DSK file system.
	/// </summary>
	[Flags()]
	public enum DSKFileStatusFlags
	{
		/*
		 * Bit No. Description
0   ... File type indicator.
    ........ 0 = Data file
    ........ 1 = Program file
1   ... Data type indicator
    ........ 0 = ASCII data (DISPLAY file)
    ........ 1 = Binary data (INTERNAL or PROGRAM file)
2   ... This bit was reserved for expansion of the data
    ... type indicator.
3   ... PROTECT FLAG
    ........ 0 = NOT protected
    ........ 1 = Protected
4,5 and 6 These bits were reserved for expansion of ????
7   .... Fixed/variable flag
     ........ 0 = Fixed record lengths
     ........ 1 = Variable record lengths
    	*/

		None = 0x00,
		Program = 0x01,
		Binary = 0x02,
		Protected = 0x08,
		/*
		Reserved1 = 0x10,
		Reserved2 = 0x20,
		Reserved3 = 0x40,
		*/
		Variable = 0x80
	}
}
