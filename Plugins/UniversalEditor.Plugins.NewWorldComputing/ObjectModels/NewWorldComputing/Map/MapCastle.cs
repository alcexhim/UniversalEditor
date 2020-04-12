//
//  MapCastle.cs - defines characteristics related to a castle placed on a map
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
	/// Defines characteristics related to a castle placed on a map.
	/// </summary>
	public class MapCastle
	{
		private MapCastleColor mvarColor = MapCastleColor.Unknown;
		public MapCastleColor Color { get { return mvarColor; } set { mvarColor = value; } }

		private bool mvarHasCustomBuilding = false;
		public bool HasCustomBuilding { get { return mvarHasCustomBuilding; } set { mvarHasCustomBuilding = value; } }

		private MapBuildingType mvarBuildings = MapBuildingType.None;
		public MapBuildingType Buildings { get { return mvarBuildings; } set { mvarBuildings = value; } }

		private MapDwellingType mvarDwellings = MapDwellingType.None;
		public MapDwellingType Dwellings { get { return mvarDwellings; } set { mvarDwellings = value; } }

		private MapMageGuildLevel mvarMageGuildLevel = MapMageGuildLevel.None;
		public MapMageGuildLevel MageGuildLevel { get { return mvarMageGuildLevel; } set { mvarMageGuildLevel = value; } }

		private bool mvarHasCustomTroops = false;
		public bool HasCustomTroops { get { return mvarHasCustomTroops; } set { mvarHasCustomTroops = value; } }

		private MapArmyMonster[] mvarMonsters = new MapArmyMonster[5];
		public MapArmyMonster[] Monsters { get { return mvarMonsters; } }

		private bool mvarHasCaptain = false;
		public bool HasCaptain { get { return mvarHasCaptain; } set { mvarHasCaptain = value; } }

		private bool mvarHasCustomName = false;
		public bool HasCustomName { get { return mvarHasCustomName; } set { mvarHasCustomName = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private MapCastleType mvarType = MapCastleType.Unknown;
		public MapCastleType Type { get { return mvarType; } set { mvarType = value; } }

		private bool mvarIsCastle = false;
		public bool IsCastle { get { return mvarIsCastle; } set { mvarIsCastle = value; } }

		private bool mvarIsUpgradable = false;
		public bool IsUpgradable { get { return mvarIsUpgradable; } set { mvarIsUpgradable = value; } }
	}
}
