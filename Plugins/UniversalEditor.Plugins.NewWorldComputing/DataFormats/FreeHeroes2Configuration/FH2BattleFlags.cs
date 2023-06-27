//
//  FH2BattleFlags.cs
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
	public enum FH2BattleFlags
	{
		BATTLE_ARCHMAGE_RESIST_BAD_SPELL = 0x40001000,
		BATTLE_MAGIC_TROOP_RESIST = 0x40002000,
		// UNUSED			= 0x40008000,
		BATTLE_SOFT_WAITING = 0x40010000,
		BATTLE_REVERSE_WAIT_ORDER = 0x40020000,
		BATTLE_MERGE_ARMIES = 0x40100000,
		BATTLE_SKIP_INCREASE_DEFENSE = 0x40200000,
		BATTLE_OBJECTS_ARCHERS_PENALTY = 0x42000000
	}
}
