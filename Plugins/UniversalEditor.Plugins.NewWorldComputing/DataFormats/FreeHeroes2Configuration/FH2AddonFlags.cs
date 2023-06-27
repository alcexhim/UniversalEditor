//
//  FH2AddonFlags.cs
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
	public enum FH2AddonFlags
	{
		CASTLE_MAGEGUILD_POINTS_TURN = 0x30000001,
		WORLD_ARTSPRING_SEPARATELY_VISIT = 0x30000002,
		CASTLE_ALLOW_RECRUITS_SPECIAL = 0x30000004,
		WORLD_STARTHERO_LOSSCOND4HUMANS = 0x30000008,
		WORLD_1HERO_HIRED_EVERY_WEEK = 0x30000010,
		WORLD_DWELLING_ACCUMULATE_UNITS = 0x30000020,
		WORLD_GUARDIAN_TWO_DEFENSE = 0x30000040,
		HEROES_ARENA_ANY_SKILLS = 0x30000080,
		WORLD_USE_UNIQUE_ARTIFACTS_ML = 0x30000100,
		WORLD_USE_UNIQUE_ARTIFACTS_RS = 0x30000200,
		WORLD_USE_UNIQUE_ARTIFACTS_PS = 0x30000400,
		WORLD_USE_UNIQUE_ARTIFACTS_SS = 0x30000800,
		WORLD_DISABLE_BARROW_MOUNDS = 0x30001000,
		WORLD_EXT_OBJECTS_CAPTURED = 0x30004000,
		CASTLE_1HERO_HIRED_EVERY_WEEK = 0x30008000
	}
}
