//
//  ScenePhysicalOperationMode.cs - indicates when physics should be calculated for the 3D scene
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	/// <summary>
	/// Indicates when physics should be calculated for the 3D scene.
	/// </summary>
	public enum ScenePhysicalOperationMode : byte
	{
		/// <summary>
		/// Physics should be disabled entirely.
		/// </summary>
		None = 0,
		/// <summary>
		/// Physics should be enabled during design-time as well as render-time.
		/// </summary>
		Always = 1,
		/// <summary>
		/// Physics should be enabled only during render-time.
		/// </summary>
		RenderOnly = 2
	}
}
