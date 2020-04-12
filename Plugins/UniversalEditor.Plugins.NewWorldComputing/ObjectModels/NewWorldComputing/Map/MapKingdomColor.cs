//
//  MapKingdomColor.cs - indicates the banner color for a kingdom
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
	/// <summary>
	/// Indicates the banner color for a kingdom.
	/// </summary>
	[Flags()]
	public enum MapKingdomColor
	{
		None = 0x00,
		Blue = 0x01,
		Green = 0x02,
		Red = 0x04,
		Yellow = 0x08,
		Orange = 0x10,
		Purple = 0x20,
		Unused = 0x80,
		All = Blue | Green | Red | Yellow | Orange | Purple
	}
}
