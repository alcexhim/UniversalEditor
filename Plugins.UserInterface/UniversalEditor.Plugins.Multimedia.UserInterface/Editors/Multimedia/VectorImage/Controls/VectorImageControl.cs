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
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems;

namespace UniversalEditor.Editors.Multimedia.VectorImage.Controls
{
	public class VectorImageControl : CustomControl
	{
		DragManager dragManager = new DragManager();
		public VectorImageControl()
		{
			dragManager.Register(this);
		}

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

		public VectorItem HitTest(Vector2D point)
		{
			VectorImageEditor editor = (Parent as VectorImageEditor);
			if (editor == null) return null;

			VectorImageObjectModel vec = (editor.ObjectModel as VectorImageObjectModel);
			if (vec == null) return null;

			for (int i = 0; i < vec.Items.Count; i++)
			{
				if (vec.Items[i].Contains(point))
					return vec.Items[i];
			}
			return null;
		}

		private VectorItem m_HoverItem = null;
		private double dx = 0, dy = 0;
		private double cx = 0, cy = 0;
		private double gx = 0, gy = 0;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Buttons == MouseButtons.Primary)
			{
				m_HoverItem = HitTest(e.Location);
				if (m_HoverItem is LineVectorItem)
				{
					dx = (m_HoverItem as LineVectorItem).X1.GetValue(MeasurementUnit.Pixel);
					dy = (m_HoverItem as LineVectorItem).Y1.GetValue(MeasurementUnit.Pixel);
					gx = ((m_HoverItem as LineVectorItem).X2.GetValue(MeasurementUnit.Pixel) - (m_HoverItem as LineVectorItem).X1.GetValue(MeasurementUnit.Pixel));
					gy = ((m_HoverItem as LineVectorItem).Y2.GetValue(MeasurementUnit.Pixel) - (m_HoverItem as LineVectorItem).Y1.GetValue(MeasurementUnit.Pixel));
				}
				cx = e.Location.X;
				cy = e.Location.Y;
				Refresh();
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Buttons == MouseButtons.None)
			{
				VectorItem _item = HitTest(e.Location);
				if (_item != m_HoverItem)
				{
					m_HoverItem = _item;
					Refresh();
				}

				if (_item != null)
				{
					Cursor = Cursors.Move;
				}
				else
				{
					Cursor = Cursors.Default;
				}
			}
			else if (e.Buttons == MouseButtons.Primary)
			{
				if (m_HoverItem != null)
				{
					if (m_HoverItem is LineVectorItem)
					{
						Measurement mx = new Measurement(dx + (e.X - cx), MeasurementUnit.Pixel), my = new Measurement(dy + (e.Y - cy), MeasurementUnit.Pixel);
						Measurement mgx = new Measurement(gx + (e.X - cx), MeasurementUnit.Pixel), mgy = new Measurement(gy + (e.Y - cy), MeasurementUnit.Pixel);
						((LineVectorItem)m_HoverItem).X1 = mx;
						((LineVectorItem)m_HoverItem).X2 = mgx;
						((LineVectorItem)m_HoverItem).Y1 = my;
						((LineVectorItem)m_HoverItem).Y2 = mgy;
					}
				}
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

				if (m_HoverItem == poly)
				{
					graphics.DrawPolygon(new Pen(SystemColors.HighlightBackground), points);
				}
			}
			else if (vectorItem is LineVectorItem)
			{
				LineVectorItem line = (vectorItem as LineVectorItem);

				graphics.DrawLine(new Pen(line.Style.BorderColor), line.X1.GetValue(), line.Y1.GetValue(), line.X2.GetValue(), line.Y2.GetValue());
				if (m_HoverItem == line)
				{
					graphics.DrawLine(new Pen(SystemColors.HighlightBackground), line.X1.GetValue(), line.Y1.GetValue(), line.X2.GetValue(), line.Y2.GetValue());
				}
			}
		}
	}
}
