//
//  IcarusCommandType.cs - indicates the type of ICARUS command in a compiled ICARUS binary
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

namespace UniversalEditor.ObjectModels.Icarus
{
	/// <summary>
	/// Indicates the type of ICARUS command in a compiled ICARUS binary.
	/// </summary>
    public enum IcarusCommandType
    {
        None = 0,
        Affect = 19,
        Sound = 20,
        Rotate = 22,
        Wait = 23,
        End = 25,
        Set = 26,
        Loop = 27,
        Print = 29,
        Use = 30,
        Flush = 31,
        Run = 32,
        Kill = 33,
        Camera = 35,
        Task = 41,
        Do = 42,
        Declare = 43,
        Signal = 46,
        WaitSignal = 47
    }
}
