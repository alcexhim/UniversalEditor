//
//  MapDwellingType.cs - indicates the type of dwelling placed on the map
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
	/// Indicates the type of dwelling placed on the map.
	/// </summary>
	[Flags]
	public enum MapDwellingType
	{
		None = 0,
		Dwelling1 = 8,
		Dwelling2 = 16,
		Dwelling3 = 32,
		Dwelling4 = 64,
		Dwelling5 = 128,
		Dwelling6 = 256,
		UpgradedDwelling2 = 512,
		UpgradedDwelling3 = 1024,
		UpgradedDwelling4 = 2048,
		UpgradedDwelling5 = 4096,
		UpgradedDwelling6 = 8192
	}
}
