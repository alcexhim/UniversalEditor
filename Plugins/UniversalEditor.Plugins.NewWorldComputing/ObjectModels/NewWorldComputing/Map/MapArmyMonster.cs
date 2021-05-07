//
//  MapArmyMonster.cs - represents an army monster placed on a Heroes of Might and Magic map
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
	/// Represents an army monster placed on a Heroes of Might and Magic map.
	/// </summary>
	public class MapArmyMonster : MapItem
	{
		public class MapArmyMonsterCollection
			: System.Collections.ObjectModel.Collection<MapArmyMonster>
		{

		}

		public ushort Amount { get; set; } = 0;
		public MapMonsterType MonsterType { get; set; } = MapMonsterType.Unknown;

		public MapArmyMonster(MapMonsterType monsterType, ushort amount)
		{
			MonsterType = monsterType;
			Amount = amount;
		}
	}
}
