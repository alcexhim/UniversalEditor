//
//  ChaosWorksSceneLevel.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.Plugins.ChaosWorks.ObjectModels.ChaosWorksScene
{
	public class ChaosWorksSceneLevel
	{
		public class ChaosWorksSceneLevelCollection
			: System.Collections.ObjectModel.Collection<ChaosWorksSceneLevel>
		{

		}

		/// <summary>
		/// Gets or sets the index of the active plane (usually 5).
		/// </summary>
		/// <value>The index of the active plane.</value>
		public int ActivePlaneIndex { get; set; } = 0;

		/// <summary>
		/// Gets or sets the center position.
		/// </summary>
		/// <value>The center position.</value>
		public PositionVector2 CenterPosition { get; set; } = PositionVector2.Empty;

		public ChaosWorksSceneLevelPlane.ChaosWorksSceneLevelPlaneCollection Planes { get; } = new ChaosWorksSceneLevelPlane.ChaosWorksSceneLevelPlaneCollection();
	}
}
