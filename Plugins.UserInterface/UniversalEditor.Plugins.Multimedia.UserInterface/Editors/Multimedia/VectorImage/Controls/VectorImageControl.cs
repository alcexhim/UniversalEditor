//
//  VectorImageControl.cs
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
using System.Collections.Generic;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems;

namespace UniversalEditor.Editors.Multimedia.VectorImage.Controls
{
	public class VectorImageControl : CustomControl
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			VectorImageEditor editor = (Parent as VectorImageEditor);
			if (editor == null) return;

			VectorImageObjectModel vec = (editor.ObjectModel as VectorImageObjectModel);
			if (vec == null) return;

			for (int i = 0; i < vec.Items.Count; i++)
			{
				DrawVectorItem(e.Graphics, vec.Items[i]);
			}
		}

		private void DrawVectorItem(Graphics graphics, VectorItem vectorItem)
		{
			if (vectorItem is PolygonVectorItem)
			{
				PolygonVectorItem poly = (vectorItem as PolygonVectorItem);

				List<Vector2D> pts = new List<Vector2D>();
				for (int i = 0; i < poly.Points.Count; i++)
				{
					pts.Add(new Vector2D(poly.Points[i].X, poly.Points[i].Y));
				}
				Vector2D[] points = pts.ToArray();

				if (poly.Style.FillColor != Colors.Transparent)
				{
					graphics.FillPolygon(new SolidBrush(poly.Style.FillColor), points);
				}
				if (poly.Style.BorderColor != Colors.Transparent)
				{
					graphics.DrawPolygon(new Pen(poly.Style.BorderColor), points);
				}
			}
		}
	}
}
