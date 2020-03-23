//
//  RibbonControlImplementation.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Ribbon;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Layouts;
using MBS.Framework.Drawing;

using MBS.Framework.UserInterface.Controls.Ribbon.Native;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(RibbonControl))]
	public class RibbonControlImplementation : CustomImplementation, IRibbonControlImplementation
	{
		public RibbonControlImplementation (Engine engine, Control control) : base (engine, control)
		{
		}

		protected override void DestroyInternal()
		{
			tbs.Destroy();
		}

		protected override void InvalidateInternal(int x, int y, int width, int height)
		{
			tbs.Invalidate(x, y, width, height);
		}

		protected override Dimension2D GetControlSizeInternal()
		{
			return tbs.Size;
		}

		protected override void RegisterDragSourceInternal (Control control, DragDropTarget[] targets, DragDropEffect actions, MBS.Framework.UserInterface.Input.Mouse.MouseButtons buttons, MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey modifierKeys)
		{
			throw new NotImplementedException ();
		}
		protected override void RegisterDropTargetInternal (Control control, DragDropTarget[] targets, DragDropEffect actions, MBS.Framework.UserInterface.Input.Mouse.MouseButtons buttons, MBS.Framework.UserInterface.Input.Keyboard.KeyboardModifierKey modifierKeys)
		{
			throw new NotImplementedException ();
		}

		protected override bool IsControlVisibleInternal()
		{
			return (tbs?.Visible).GetValueOrDefault(false);
		}
		protected override void SetControlVisibilityInternal (bool visible)
		{
			if (tbs == null)
				return;
			tbs.Visible = visible;
		}

		private TabContainer tbs = null;
		protected override NativeControl CreateControlInternal (Control control)
		{
			RibbonControl ribbon = (control as RibbonControl);
			tbs = new TabContainer ();
			tbs.Style.Classes.Add ("primary-toolbar");
			tbs.Style.Classes.Add ("Ribbon");

			foreach (RibbonTab rtab in ribbon.Tabs) {
				TabPage tab = new TabPage ();
				tab.Layout = new BoxLayout (Orientation.Horizontal);
				tab.Text = rtab.Title;

				foreach (RibbonTabGroup grp in rtab.Groups) {
					Container ct = new Container ();
					ct.Layout = new BoxLayout (Orientation.Vertical);
					Label lbl = new Label (grp.Title);
					lbl.Attributes.Add ("scale", 0.8);
					lbl.HorizontalAlignment = HorizontalAlignment.Center;

					Container ctImportants = new Container ();
					ctImportants.Layout = new BoxLayout (Orientation.Horizontal);

					Container ctCutCopyDelete = new Container ();
					ctCutCopyDelete.Layout = new BoxLayout (Orientation.Vertical);

					foreach (RibbonCommandItem item in grp.Items) {
						if (item is RibbonCommandItemButton) {
							RibbonCommandItemButton tsb = (item as RibbonCommandItemButton);

							Command cmd = Application.Commands [tsb.CommandID];

							if (cmd == null)
								continue;
							
							Button btn = null;
							if (cmd.StockType == StockType.None) {
								btn = new Button (cmd.Title);
							} else {
								btn = new Button ((StockType)cmd.StockType);
							}
							btn.FocusOnClick = false;
							btn.AlwaysShowImage = true; // .DisplayStyle = ButtonDisplayStyle.ImageAndText;
							btn.BorderStyle = ButtonBorderStyle.None;
							btn.Click += btn_Click;
							btn.TooltipText = cmd.Title;
							btn.SetExtraData<RibbonCommandItemButton> ("rcib", tsb);

							if (tsb.IsImportant) {
								btn.ImagePosition = RelativePosition.Top;
								btn.ImageSize = new Dimension2D (32, 32);
								ctImportants.Controls.Add (btn);
							} else {
								btn.HorizontalAlignment = HorizontalAlignment.Left;
								btn.ImagePosition = RelativePosition.Left;
								ctCutCopyDelete.Controls.Add (btn);
							}
						}
					}

					ctImportants.Controls.Add (ctCutCopyDelete);

					ct.Controls.Add (ctImportants, new BoxLayout.Constraints(true, true));

					Container ctLabelAndDialog = new Container ();
					ctLabelAndDialog.Layout = new BoxLayout (Orientation.Horizontal);

					ctLabelAndDialog.Controls.Add (lbl, new BoxLayout.Constraints(true, true, 8));

					Button btnDialog = new Button ();
					btnDialog.Size = new Dimension2D (8, 8);
					btnDialog.BorderStyle = ButtonBorderStyle.None;
					ctLabelAndDialog.Controls.Add (btnDialog, new BoxLayout.Constraints(false, false, 8));

					ct.Controls.Add (ctLabelAndDialog, new BoxLayout.Constraints (false, false, 8));
					tab.Controls.Add (ct);
				}


				tbs.TabPages.Add (tab);
			}

			return new CustomNativeControl(tbs);
		}

		void btn_Click (object sender, EventArgs e)
		{
			Button btn = (sender as Button);
			RibbonCommandItemButton rcib = btn.GetExtraData<RibbonCommandItemButton> ("rcib");
			if (rcib == null) return;

			Application.ExecuteCommand (rcib.CommandID);
		}


		private bool mvarExpanded = true;
		public bool GetExpanded()
		{
			return mvarExpanded;
		}
		public void SetExpanded(bool value)
		{
			mvarExpanded = value;

			if (tbs == null)
				return;
			if (!mvarExpanded) {
				foreach (TabPage page in tbs.TabPages) {
					foreach (Control ctl in page.Controls) {
						ctl.Visible = false;
					}
				}
			} else {
				foreach (TabPage page in tbs.TabPages) {
					foreach (Control ctl in page.Controls) {
						ctl.Visible = true;
					}
				}
			}
		}

		protected override void SetFocusInternal ()
		{
			tbs.Focus ();
		}

		protected override string GetTooltipTextInternal()
		{
			return tbs.TooltipText;
		}
		protected override void SetTooltipTextInternal(string value)
		{
			tbs.TooltipText = value;
		}

		protected override Cursor GetCursorInternal()
		{
			return tbs.Cursor;
		}
		protected override void SetCursorInternal(Cursor value)
		{
			tbs.Cursor = value;
		}

		protected override bool HasFocusInternal()
		{
			return (tbs?.Focused).GetValueOrDefault();
		}
	}
}

