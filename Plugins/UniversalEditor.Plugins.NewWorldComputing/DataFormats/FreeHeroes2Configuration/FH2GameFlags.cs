//
//  FH2GameFlags.cs
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
	public enum FH2GameFlags : ulong
	{
		GAME_AUTOSAVE_BEGIN_DAY = 0x10000010,
		GAME_REMEMBER_LAST_FOCUS = 0x10000020,
		GAME_SAVE_REWRITE_CONFIRM = 0x10000040,
		GAME_CASTLE_FLASH_BUILDING = 0x10000080,
		GAME_SHOW_SYSTEM_INFO = 0x10000100,
		GAME_AUTOSAVE_ON = 0x10000200,
		GAME_USE_FADE = 0x10000400,
		GAME_SHOW_SDL_LOGO = 0x10000800,
		GAME_EVIL_INTERFACE = 0x10001000,
		GAME_HIDE_INTERFACE = 0x10002000,
		GAME_ALSO_CONFIRM_AUTOSAVE = 0x10004000,
		// UNUSED			= 0x10008000,
		GAME_DYNAMIC_INTERFACE = 0x10010000,
		GAME_BATTLE_SHOW_GRID = 0x10020000,
		GAME_BATTLE_SHOW_MOUSE_SHADOW = 0x10040000,
		GAME_BATTLE_SHOW_MOVE_SHADOW = 0x10080000,
		GAME_BATTLE_SHOW_DAMAGE = 0x10100000,
		GAME_CONTINUE_AFTER_VICTORY = 0x10200000,
		POCKETPC_HIDE_CURSOR = 0x10400000,
		POCKETPC_LOW_MEMORY = 0x10800000,
		POCKETPC_TAP_MODE = 0x11000000,
		POCKETPC_DRAG_DROP_SCROLL = 0x12000000,
		POCKETPC_LOW_RESOLUTION = 0x14000000
	}
}
