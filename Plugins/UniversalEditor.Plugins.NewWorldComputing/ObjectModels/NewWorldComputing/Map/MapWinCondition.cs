//
//  MapWinCondition.cs - indicates the condition(s) required to win a game using this map
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

namespace UniversalEditor.ObjectModels.NewWorldComputing.Map
{
	/// <summary>
	/// Indicates the condition(s) required to win a game using this map.
	/// </summary>
	public enum MapWinCondition
	{
		None = 0x0000,
		All = 0x0001,
		Town = 0x0002,
		Hero = 0x0004,
		Artifact = 0x0008,
		Side = 0x0010,
		Gold = 0x0020,
		Any = All | Town | Hero | Artifact | Side | Gold
	}
}
