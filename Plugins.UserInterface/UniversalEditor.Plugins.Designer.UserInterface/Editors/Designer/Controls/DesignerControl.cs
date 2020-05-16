//
//  DesignerControl.cs
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

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;
using UniversalEditor.ObjectModels.Designer;

namespace UniversalEditor.Plugins.Designer.UserInterface.Editors.Designer.Controls
{
	public class DesignerControl : CustomControl
	{
		private DesignerObjectModel m_ObjectModel = null;
		public DesignerObjectModel ObjectModel { get { return m_ObjectModel; } set { m_ObjectModel = value; Refresh(); } }

		private Design _SelectedDesign = null;
		public Design SelectedDesign { get { return _SelectedDesign; } set { _SelectedDesign = value;  Refresh(); } }

		public ComponentInstance.ComponentInstanceCollection SelectedComponents { get; } = new ComponentInstance.ComponentInstanceCollection();

		private int margin_x = 13, margin_y = 13, margin_r = 13, margin_b = 13;

		public ComponentInstance HitTest(Vector2D location)
		{
			for (int i = SelectedDesign.ComponentInstances.Count - 1; i >= 0; i--)
			{
				ComponentInstance inst = SelectedDesign.ComponentInstances[i];
				if (location.X >= inst.X.Value && location.X <= inst.X.Value + inst.Width.Value && location.Y >= inst.Y.Value && location.Y <= inst.Y.Value + inst.Height.Value)
					return inst;
			}
			return null;
		}

		private ComponentInstance dragging = null;
		private double cx = 0, cy = 0;
		private double dx = 0, dy = 0;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Buttons == MouseButtons.Primary || SelectedComponents.Count == 0)
			{
				ComponentInstance hit = HitTest(e.Location);
				if (hit != null)
				{
					SelectedComponents.Clear();
					SelectedComponents.Add(hit);
					Refresh();

					if (e.Buttons == MouseButtons.Primary)
					{
						dragging = hit;
						cx = e.X;
						cy = e.Y;
						dx = hit.X.Value;
						dy = hit.Y.Value;
					}
				}
				else
				{
					dragging = null;
				}
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Buttons == MouseButtons.Primary && dragging != null)
			{
				dragging.X = new ObjectModels.Designer.Measurement(dx + (e.X - cx), ObjectModels.Designer.MeasurementUnit.Pixel);
				dragging.Y = new ObjectModels.Designer.Measurement(dy + (e.Y - cy), ObjectModels.Designer.MeasurementUnit.Pixel);
				Refresh();
			}
			else
			{
				ComponentInstance hit = HitTest(e.Location);
				if (hit != null)
				{
					Cursor = Cursors.Move;
				}
				else
				{
					Cursor = Cursors.Default;
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (m_ObjectModel == null) return;
			if (_SelectedDesign == null && m_ObjectModel.Designs.Count > 0) _SelectedDesign = m_ObjectModel.Designs[0];
			if (_SelectedDesign == null) return;

			e.Graphics.DrawRectangle(new Pen(Colors.DarkGray), new Rectangle(margin_x, margin_y, _SelectedDesign.Size.Width - margin_x - margin_r, _SelectedDesign.Size.Height - margin_y - margin_b));

			for (int i = 0; i < SelectedDesign.ComponentInstances.Count; i++)
			{
				ComponentInstance inst = SelectedDesign.ComponentInstances[i];
				Rectangle componentRect = new MBS.Framework.Drawing.Rectangle(margin_x + inst.X.Value, margin_y + inst.Y.Value, inst.Width.Value, inst.Height.Value);
				inst.Component.Render(inst, e, componentRect);

				if (SelectedComponents.Contains(inst))
				{
					e.Graphics.DrawRectangle(new Pen(SystemColors.HighlightBackground), componentRect);
				}
				else
				{
					e.Graphics.DrawRectangle(new Pen(Colors.DarkGray), componentRect);
				}
			}
			base.OnPaint(e);
		}
	}
}
