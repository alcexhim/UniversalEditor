//
//  ColorDialog.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(ColorDialog))]
	public class ColorDialogImplementation : GTKDialogImplementation
	{
		public ColorDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			Internal.GDK.Structures.GdkRGBA color = new Internal.GDK.Structures.GdkRGBA();
			Internal.GTK.Methods.GtkColorDialog.gtk_color_chooser_get_rgba((Handle as GTKNativeControl).Handle, out color);
			(Control as ColorDialog).SelectedColor = Color.FromRGBADouble(color.red, color.green, color.blue, color.alpha);
			return true;
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			ColorDialog dlg = (dialog as ColorDialog);
			string title = dlg.Text;
			if (title == null)
				title = "Select Color";

			IntPtr handle = Internal.GTK.Methods.GtkColorDialog.gtk_color_dialog_new(title, (Engine as GTKEngine).CommonDialog_GetParentHandle(dlg), !dlg.AutoUpgradeEnabled);
			Internal.GTK.Methods.GtkColorDialog.gtk_color_chooser_set_use_alpha(handle, true);

			Internal.GDK.Structures.GdkRGBA rgba = new Internal.GDK.Structures.GdkRGBA();
			rgba.alpha = dlg.SelectedColor.A;
			rgba.blue = dlg.SelectedColor.B;
			rgba.green = dlg.SelectedColor.G;
			rgba.red = dlg.SelectedColor.R;
			Internal.GTK.Methods.GtkColorDialog.gtk_color_chooser_set_rgba(handle, ref rgba);
			return new GTKNativeControl(handle);
		}
	}
}
