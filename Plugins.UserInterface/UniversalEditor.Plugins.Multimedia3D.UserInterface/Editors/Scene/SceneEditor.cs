//
//  StageEditor.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Multimedia3D.Scene;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.Editors.Scene
{
	public class SceneEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.ID = new Guid("{6dbe6294-681b-4d1f-8249-d04a1a3ef4a2}");
				_er.SupportedObjectModels.Add(typeof(SceneObjectModel));
				_er.Views.Add("Scene");
				_er.Views.Add("Graph");
			}
			return _er;
		}

		private Views.SceneView viewScene;
		private Views.GraphView viewGraph;

		public SceneEditor()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			viewScene = new Views.SceneView();
			Controls.Add(viewScene, new BoxLayout.Constraints(true, true));
			viewGraph = new Views.GraphView();
			Controls.Add(viewGraph, new BoxLayout.Constraints(true, true));
		}

		protected override void OnViewChanged(EditorViewChangedEventArgs e)
		{
			base.OnViewChanged(e);
			switch (e.NewView.Title)
			{
				case "Scene":
				{
					viewScene.Visible = true;
					viewGraph.Visible = false;

					viewScene.ObjectModel = ObjectModel;
					break;
				}
				case "Graph":
				{
					viewScene.Visible = false;
					viewGraph.Visible = true;

					viewGraph.ObjectModel = ObjectModel;
					break;
				}
			}
		}

		public override void UpdateSelections()
		{
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			return null;
		}
	}
}
