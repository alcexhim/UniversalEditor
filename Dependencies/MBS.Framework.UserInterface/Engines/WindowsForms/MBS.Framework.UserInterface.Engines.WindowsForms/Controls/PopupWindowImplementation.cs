//
//  PopupWindowImplementation.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(PopupWindow))]
	public class PopupWindowImplementation : WindowImplementation
	{
		public PopupWindowImplementation (Engine engine, PopupWindow control) : base(engine, control)
		{
		}

		protected override NativeControl CreateControlInternal (Control control)
		{
			PopupWindow ctl = (Control as PopupWindow);

			System.Windows.Forms.Control hCtrlParent = null;
			if (ctl.Owner != null) {
				if (ctl.Owner.ControlImplementation.Handle is WindowsFormsNativeControl) {
					hCtrlParent = (ctl.Owner.ControlImplementation.Handle as WindowsFormsNativeControl).Handle;
				} else {
				}
			}
			System.Windows.Forms.Form handle = new System.Windows.Forms.Form();
			handle.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			handle.Text = String.Empty;
			handle.ControlBox = false;

			foreach (Control ctl1 in ctl.Controls) {
				if (!ctl1.IsCreated) Engine.CreateControl (ctl1);
				if (!ctl1.IsCreated) continue;

				System.Windows.Forms.Control hCtrl1 = (Engine.GetHandleForControl(ctl1) as WindowsFormsNativeControl).Handle;
				hCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
				handle.Controls.Add(hCtrl1);
			}

			return new WindowsFormsNativeControl (handle);
		}

		protected override void SetControlVisibilityInternal (bool visible)
		{
			if (Handle == null) return;

			PopupWindow ctl = (Control as PopupWindow);
			System.Windows.Forms.Form handle = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.Form);
			handle.Size = WindowsFormsEngine.Dimension2DToSystemDrawingSize(ctl.Size);
			handle.MinimumSize = WindowsFormsEngine.Dimension2DToSystemDrawingSize(ctl.MinimumSize);
			handle.MaximumSize = WindowsFormsEngine.Dimension2DToSystemDrawingSize(ctl.MaximumSize);

			if (ctl.Owner != null) {
				System.Windows.Forms.Control hCtrlParent = (Engine.GetHandleForControl(ctl.Owner) as WindowsFormsNativeControl).Handle;
				handle.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
				handle.Location = new System.Drawing.Point(hCtrlParent.Location.X, hCtrlParent.Location.Y - handle.Height);
			}

			if (visible) {
				if (ctl.Modal)
				{
					handle.ShowDialog();
				}
				else
				{
					handle.Show();
				}
			} else {
				handle.Hide();
			}
		}
	}
}

