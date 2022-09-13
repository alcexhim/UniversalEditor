//
//  ChaosWorksSceneObject.cs
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
	public class ChaosWorksSceneObject
	{
		public class ChaosWorksSceneObjectCollection
			: System.Collections.ObjectModel.Collection<ChaosWorksSceneObject>
		{

		}

		public string SpriteName { get; set; }
		public int Phase { get; set; }
		public int Mirror { get; set; }
		public PositionVector2 Position { get; set; }
		public int Link { get; set; }
		public int LPos { get; set; }
		public int S { get; set; }
		public string TypeName { get; set; }
	}
}
