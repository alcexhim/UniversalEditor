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

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(PopupWindow))]
	public class PopupWindowImplementation : WindowImplementation
	{
		public PopupWindowImplementation (Engine engine, PopupWindow control) : base(engine, control)
		{
		}

		private Internal.GObject.Delegates.GCallbackV1I popover_closed_handler = null;
		private void popover_closed(IntPtr handle)
		{
			PopupWindow ctl = ((Engine as GTKEngine).GetControlByHandle (handle) as PopupWindow);
			if (ctl == null)
				return;

			// InvokeMethod (ctl.ControlImplementation, "OnClosed", EventArgs.Empty);
		}

		protected override NativeControl CreateControlInternal (Control control)
		{
			PopupWindow ctl = (Control as PopupWindow);

			IntPtr hCtrlParent = IntPtr.Zero;
			if (ctl.Owner != null) {
				if (ctl.Owner.ControlImplementation.Handle is GTKNativeControl) {
					hCtrlParent = (ctl.Owner.ControlImplementation.Handle as GTKNativeControl).Handle;
				} else {
				}
			}
			IntPtr handle = Internal.GTK.Methods.GtkPopover.gtk_popover_new (hCtrlParent);

			popover_closed_handler = new Internal.GObject.Delegates.GCallbackV1I (popover_closed);
			Internal.GObject.Methods.g_signal_connect (handle, "closed", popover_closed_handler);

			foreach (Control ctl1 in ctl.Controls) {
				if (!ctl1.IsCreated) Engine.CreateControl (ctl1);
				if (!ctl1.IsCreated) continue;

				IntPtr hCtrl1 = (Engine.GetHandleForControl(ctl1) as GTKNativeControl).Handle;
				Internal.GTK.Methods.GtkWidget.gtk_widget_show_all (hCtrl1);
				Internal.GTK.Methods.GtkContainer.gtk_container_add (handle, hCtrl1);
			}

			Internal.GTK.Methods.GtkPopover.gtk_popover_set_modal (handle, ctl.Modal);
			return new GTKNativeControl (handle);
		}

		protected override void SetControlVisibilityInternal (bool visible)
		{
			if (Handle == null) return;

			PopupWindow ctl = (Control as PopupWindow);
			IntPtr handle = (Handle as GTKNativeControl).Handle;

			if (ctl.Owner != null) {
				IntPtr hCtrlParent = (Engine.GetHandleForControl(ctl.Owner) as GTKNativeControl).Handle;
				Internal.GTK.Methods.GtkPopover.gtk_popover_set_relative_to (handle, hCtrlParent);
			}

			if (visible) {
				Internal.GTK.Methods.GtkPopover.gtk_popover_popup (handle);
			} else {
				Internal.GTK.Methods.GtkPopover.gtk_popover_popdown (handle);
			}
		}
	}
}

