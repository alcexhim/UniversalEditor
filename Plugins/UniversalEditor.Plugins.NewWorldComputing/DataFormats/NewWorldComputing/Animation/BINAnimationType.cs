//
//  BINAnimationType.cs
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
using System;
namespace UniversalEditor.DataFormats.NewWorldComputing.Animation
{
	public enum BINAnimationType
	{
		MOVE_START, // Start of the moving sequence on 1st animation cycle: flyers will fly up
		MOVE_TILE_START, // Supposed to be played at the beginning of 2nd+ move.
		MOVE_MAIN, // Core animation. Most units only have this one.
		MOVE_TILE_END, // Cavalry & wolf. Played at the end of the cycle (2nd tile to 3rd), but not at the last one
		MOVE_STOP, // End of the moving sequence when arrived: landing for example
		MOVE_ONE, // Used when moving 1 tile. LICH and POWER_LICH doesn't have this, use MOVE_MAIN
		TEMPORARY, // This is an empty placeholder for combined animation built from previous parts
		STATIC, // Frame 1
		IDLE1,
		IDLE2, // Idle animations: picked at random with different probablities, rarely all 5 present
		IDLE3,
		IDLE4,
		IDLE5,
		DEATH,
		WINCE_UP,
		WINCE_END,
		ATTACK1, // Attacks, number represents the angle: 1 is TOP, 2 is CENTER, 3 is BOTTOM
		ATTACK1_END,
		DOUBLEHEX1,
		DOUBLEHEX1_END,
		ATTACK2,
		ATTACK2_END,
		DOUBLEHEX2,
		DOUBLEHEX2_END,
		ATTACK3,
		ATTACK3_END,
		DOUBLEHEX3,
		DOUBLEHEX3_END,
		SHOOT1,
		SHOOT1_END,
		SHOOT2,
		SHOOT2_END,
		SHOOT3,
		SHOOT3_END
	}
}
