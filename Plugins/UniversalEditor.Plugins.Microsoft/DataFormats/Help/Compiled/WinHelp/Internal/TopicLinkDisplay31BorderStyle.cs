//
//  TopicLinkDisplay31BorderStyle.cs - internal enum indicating the border style for a topic link on WinHelp 3.1 systems
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
	/// Internal enum indicating the border style for a topic link on WinHelp 3.1 systems.
	/// </summary>
	internal enum TopicLinkDisplay31BorderStyle : byte
	{
		None = 0x00,
		Box = 0x01,
		Top = 0x02,
		Left = 0x04,
		Bottom = 0x08,
		Right = 0x10,
		Thick = 0x20,
		Double = 0x40,
		Unknown = 0x80
	}
}
