//
//  FH2WorldFlags.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.DataFormats.FreeHeroes2Configuration
{
	[Flags()]
	public enum FH2WorldFlags
	{
		/* influence on game balance: save to savefile */
		WORLD_SHOW_VISITED_CONTENT = 0x20000001,
		WORLD_ABANDONED_MINE_RANDOM = 0x20000002,
		WORLD_SAVE_MONSTER_BATTLE = 0x20000004,
		WORLD_ALLOW_SET_GUARDIAN = 0x20000008,
		WORLD_NOREQ_FOR_ARTIFACTS = 0x20000010,
		WORLD_ARTIFACT_CRYSTAL_BALL = 0x20000020,
		WORLD_SCOUTING_EXTENDED = 0x20000040,
		WORLD_ONLY_FIRST_MONSTER_ATTACK = 0x20000080,
		WORLD_EYE_EAGLE_AS_SCHOLAR = 0x20000100,
		HEROES_BUY_BOOK_FROM_SHRINES = 0x20000200,
		WORLD_BAN_WEEKOF = 0x20000400,
		WORLD_BAN_PLAGUES = 0x20000800,
		UNIONS_ALLOW_HERO_MEETINGS = 0x20001000,
		UNIONS_ALLOW_CASTLE_VISITING = 0x20002000,
		// UNUSED			= 0x20004000,
		HEROES_AUTO_MOVE_BATTLE_DST = 0x20008000,
		WORLD_BAN_MONTHOF_MONSTERS = 0x20010000,
		HEROES_TRANSCRIBING_SCROLLS = 0x20020000,
		WORLD_NEW_VERSION_WEEKOF = 0x20040000,
		CASTLE_ALLOW_GUARDIANS = 0x20080000,
		CASTLE_ALLOW_BUY_FROM_WELL = 0x20100000,
		HEROES_LEARN_SPELLS_WITH_DAY = 0x20200000,
		HEROES_ALLOW_BANNED_SECSKILLS = 0x20400000,
		HEROES_COST_DEPENDED_FROM_LEVEL = 0x20800000,
		HEROES_REMEMBER_POINTS_RETREAT = 0x21000000,
		HEROES_SURRENDERING_GIVE_EXP = 0x22000000,
		HEROES_RECALCULATE_MOVEMENT = 0x24000000,
		HEROES_PATROL_ALLOW_PICKUP = 0x28000000
	}
}
