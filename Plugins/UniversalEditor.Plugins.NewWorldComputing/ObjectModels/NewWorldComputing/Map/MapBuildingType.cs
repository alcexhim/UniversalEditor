//
//  MapBuildingType.cs - indicates the type of building placed on the map
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
	/// Indicates the type of building placed on the map.
	/// </summary>
	[Flags()]
	public enum MapBuildingType : ushort
	{
		None = 0,
		ThievesGuild = 2,
		Tavern = 4,
		Shipyard = 8,
		Well = 16,
		Statue = 128,
		LeftTurret = 256,
		RightTurret = 512,
		Marketplace = 1024,
		/// <summary>
		/// Creature enhancer. Farm (Knight), Garbage Heap (Barbarian), Crystal Garden (Sorceress), Waterfall (Warlock),
		/// Orchard (Wizard), Skull Pile (Necroman).
		/// </summary>
		CreatureEnhancer = 2048,
		Moat = 4096,
		/// <summary>
		/// Castle enhancer. Fortification (Knight), Coliseum (Barbarian), Rainbow (Sorceress), Dungeon (Warlock),
		/// Library (Wizard), Storm (Necroman).
		/// </summary>
		CastleEnhancer = 8192
	}
}
