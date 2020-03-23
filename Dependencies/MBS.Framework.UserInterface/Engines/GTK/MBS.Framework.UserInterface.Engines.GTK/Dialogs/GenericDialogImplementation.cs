//
//  GenericDialogImplementation.cs
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(CustomDialog))]
	public class GenericDialogImplementation : GTKDialogImplementation
	{
		public GenericDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			return true;
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			IntPtr handle = Internal.GObject.Methods.g_object_new(Internal.GTK.Methods.GtkDialog.gtk_dialog_get_type(), "use-header-bar", 1, IntPtr.Zero);
			// IntPtr handle = Internal.GTK.Methods.GtkDialog.gtk_dialog_new_with_buttons(dlg.Text, hParent, Internal.GTK.Constants.GtkDialogFlags.Modal, null);

			IntPtr hText = Marshal.StringToHGlobalAuto(dialog.Text);
			Internal.GTK.Methods.GtkWindow.gtk_window_set_title(handle, hText);

			IntPtr hDialogContent = Internal.GTK.Methods.GtkDialog.gtk_dialog_get_content_area(handle);

			NativeControl hContainer = (new Controls.ContainerImplementation(Engine, dialog)).CreateControl(dialog);
			// NativeControl hContainer = CreateContainer (dlg);

			Internal.GTK.Methods.GtkBox.gtk_box_pack_start(hDialogContent, (hContainer as GTKNativeControl).Handle, true, true, 0);
			Internal.GTK.Methods.GtkWidget.gtk_widget_show_all(hDialogContent);

			GTKNativeControl nc = new GTKNativeControl(handle);
			(Engine as GTKEngine).RegisterControlHandle(dialog, nc);
			(hContainer as GTKNativeControl).SetNamedHandle("dialog", handle);
			return nc;
		}
	}
}
