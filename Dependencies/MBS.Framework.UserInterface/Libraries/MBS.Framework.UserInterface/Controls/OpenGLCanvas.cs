//
//  OpenGLCanvas.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace MBS.Framework.UserInterface.Controls
{
	public class OpenGLCanvas : SystemControl
	{
		public MBS.Framework.Rendering.Engine Engine { get; private set; }

		public OpenGLCanvas()
		{
			Engine = MBS.Framework.Rendering.Engine.GetDefault();
		}

		public event OpenGLCanvasRenderEventHandler Render;
		public void OnRender(OpenGLCanvasRenderEventArgs e)
		{
			Render?.Invoke(this, e);

			if (Engine != null)
				Engine.Flush();
		}
	}
}
