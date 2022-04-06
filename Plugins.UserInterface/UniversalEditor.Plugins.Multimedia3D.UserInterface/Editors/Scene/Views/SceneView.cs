//
//  SceneView.cs
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.Editors.Scene.Views
{
	public class SceneView : View
	{
		private OpenGLCanvas canvas;
		public SceneView()
		{
			Layout = new BoxLayout(Orientation.Vertical);

			canvas = new OpenGLCanvas();
			Controls.Add(canvas, new BoxLayout.Constraints(true, true));
		}
	}
}
