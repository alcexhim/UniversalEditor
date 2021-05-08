//
//  PresentationSlideControl.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.Plugins.Designer.UserInterface.Editors.Designer.Controls;

namespace UniversalEditor.Plugins.Office.UserInterface.Editors.Presentation.Controls
{
	public class PresentationSlideControl : DesignerControl
	{
		private int SlideOffsetX = 32, SlideOffsetY = 0;
		private int SlideWidth = 640, SlideHeight = 480;

		private double _ZoomFactor = 1.0;
		public double ZoomFactor { get { return _ZoomFactor;  } set { _ZoomFactor = value;  Refresh(); } }

		private int ShadowOffset = 4;

		public PresentationSlideControl()
		{
			// TODO: add support for multiple designer areas with their own sets of components
			// DesignerAreas.Clear();

			// DesignerAreas.Add(new DesignerArea());
		}

		protected override void OnBeforePaint(PaintEventArgs e)
		{
			base.OnBeforePaint(e);

			double zp = ((double)SlideHeight / SlideWidth);

			int maxw = (int)(Size.Width - SlideOffsetX - SlideOffsetX);
			int x = SlideOffsetX, w = (int)(ZoomFactor * maxw), h = (int)(ZoomFactor * (zp * maxw));
			int y = (int)((Size.Height - h) / 2) + SlideOffsetY;

			e.Graphics.FillRectangle(new SolidBrush(Colors.Black), new Rectangle(x + ShadowOffset, y + ShadowOffset, w, h));
			e.Graphics.FillRectangle(new SolidBrush(Colors.White), new Rectangle(x, y, w, h));
		}
	}
}
