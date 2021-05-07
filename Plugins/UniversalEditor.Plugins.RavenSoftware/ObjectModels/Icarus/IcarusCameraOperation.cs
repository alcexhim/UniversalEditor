//
//  IcarusCameraOperation.cs - indicates the operation for the "camera" command from the CAMERA_COMMANDS enumeration
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

namespace UniversalEditor.ObjectModels.Icarus
{
	/// <summary>
	/// Indicates the operation for the "camera" command from the CAMERA_COMMANDS enumeration.
	/// </summary>
	public enum IcarusCameraOperation
	{
		None = 0,
		/// <summary>
		/// Pan to absolute angle from current angle in dir (no dir will use shortest) over number of milliseconds
		/// </summary>
		Pan = 57,
		/// <summary>
		/// Normal is 80, 10 is zoomed in, max is 120. Second value is time in ms to take to zoom to the new FOV
		/// </summary>
		Zoom = 58,
		/// <summary>
		/// Move to a absolute vector origin or TAG("targetname", ORIGIN) over time over number of milliseconds
		/// </summary>
		Move = 59,
		/// <summary>
		/// Fade from [start Red Green Blue], [Opacity] to [end Red Green Blue], [Opacity] [all fields valid ranges are 0 to 1] over [number of milliseconds]
		/// </summary>
		Fade = 60,
		/// <summary>
		/// Path to ROF file
		/// </summary>
		Path = 61,
		/// <summary>
		/// Puts game in camera mode
		/// </summary>
		Enable = 62,
		/// <summary>
		/// Takes game out of camera mode
		/// </summary>
		Disable = 63,
		/// <summary>
		/// Intensity (0-16) and duration, in milliseconds
		/// </summary>
		Shake = 64,
		/// <summary>
		/// Roll to relative angle offsets of current angle over number of milliseconds
		/// </summary>
		Roll = 65,
		/// <summary>
		/// Get on track and move at speed, last number is whether or not to lerp to the start pos
		/// </summary>
		Track = 66,
		/// <summary>
		/// Keep this distance from cameraGroup (if any), last number is whether or not to lerp to the start angle
		/// </summary>
		Distance = 67,
		/// <summary>
		/// Follow ends with matching cameraGroup at angleSpeed, last number is whether or not to lerp to the start angle
		/// </summary>
		Follow = 68
	}
}
