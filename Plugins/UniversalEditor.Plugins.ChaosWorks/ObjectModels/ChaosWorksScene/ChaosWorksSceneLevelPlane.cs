//
//  ChaosWorksSceneLevelPlane.cs
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
	public class ChaosWorksSceneLevelPlane
	{
		public class ChaosWorksSceneLevelPlaneCollection
			: System.Collections.ObjectModel.Collection<ChaosWorksSceneLevelPlane>
		{
		}

		public PositionVector2 Position { get; set; } = PositionVector2.Empty;
		public int u1 { get; set; } = 0;
		public int u2 { get; set; } = 0;
		public int u3 { get; set; } = 0;

		public ChaosWorksSceneObject.ChaosWorksSceneObjectCollection Objects { get; } = new ChaosWorksSceneObject.ChaosWorksSceneObjectCollection();
	}
}
