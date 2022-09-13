//
//  ChaosWorkSceneObjectModel.cs
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
	public class ChaosWorksSceneObjectModel : ObjectModel
	{
		public override void Clear()
		{
		}

		public override void CopyTo(ObjectModel where)
		{
		}

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Game Development", "Chaos Works", "Chaos Works Scene" };
			}
			return _omr;
		}

		public ChaosWorksSceneType.ChaosWorksSceneTypeCollection Types { get; } = new ChaosWorksSceneType.ChaosWorksSceneTypeCollection();
		public ChaosWorksSceneSprite.ChaosWorksSceneSpriteCollection Sprites { get; } = new ChaosWorksSceneSprite.ChaosWorksSceneSpriteCollection();
		public ChaosWorksSceneLevel.ChaosWorksSceneLevelCollection Levels { get; } = new ChaosWorksSceneLevel.ChaosWorksSceneLevelCollection();

		public ChaosWorksSceneLevel MainLevel { get; set; } = null;

	}
}
