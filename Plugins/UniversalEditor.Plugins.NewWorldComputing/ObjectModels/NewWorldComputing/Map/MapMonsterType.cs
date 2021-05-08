//
//  MapMonsterType.cs - indicates the type of monster to display on the map
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
	/// Indicates the type of monster to display on the map.
	/// </summary>
	public enum MapMonsterType : byte
	{
		Unknown,
		#region Knight
		Peasant,
		Archer,
		Ranger,
		Pikeman,
		VeteranPikeman,
		Swordsman,
		MasterSwordsman,
		Cavalry,
		Champion,
		Paladin,
		Crusader,
		#endregion
		#region Barbarian
		Goblin,
		Orc,
		OrcChief,
		Wolf,
		Ogre,
		OgreLord,
		Troll,
		WarTroll,
		Cyclops,
		#endregion
		#region Sorceress
		Sprite,
		Dwarf,
		BattleDwarf,
		Elf,
		GrandElf,
		Druid,
		GreaterDruid,
		Unicorn,
		Phoenix,
		#endregion
		#region Warlock
		Centaur,
		Gargoyle,
		Griffin,
		Minotaur,
		MinotaurKing,
		Hydra,
		GreenDragon,
		RedDragon,
		BlackDragon,
		#endregion
		#region Wizard
		Halfling,
		Boar,
		IronGolem,
		SteelGolem,
		Roc,
		Mage,
		ArchMage,
		Giant,
		Titan,
		#endregion
		#region Necromancer
		Skeleton,
		Zombie,
		MutantZombie,
		Mummy,
		RoyalMummy,
		Vampire,
		VampireLord,
		Lich,
		PowerLich,
		BoneDragon,
		#endregion
		#region Neutral
		Rogue,
		Nomad,
		Ghost,
		Genie,
		Medusa,
		EarthElemental,
		AirElemental,
		FireElemental,
		WaterElemental,
		#endregion
		#region Random
		Random1,
		Random2,
		Random3,
		Random4,
		Random
		#endregion
	}
}
