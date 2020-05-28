//
//  MapHero.cs - represents a hero on a map
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
	public class MapHero : MapItem
	{
		public bool HasCustomTroops { get; set; } = false;

		public bool HasCustomName { get; set; } = false;
		public string Name { get; set; } = null;

		public MapArmyMonster.MapArmyMonsterCollection Monsters { get; } = new MapArmyMonster.MapArmyMonsterCollection();
		public MapArtifact.MapArtifactCollection Artifacts { get; } = new MapArtifact.MapArtifactCollection();

		public bool HasCustomSkills { get; set; } = false;
		public HeroSkill.HeroSkillCollection Skills { get; } = new HeroSkill.HeroSkillCollection();

		public bool HasCustomPortrait { get; set; }
		public byte CustomPortraitIndex { get; set; }

		public uint Experience { get; set; }

		public bool Patrol { get; set; } = false;
		public byte PatrolSquareCount { get; set; } = 0;
	}
}
