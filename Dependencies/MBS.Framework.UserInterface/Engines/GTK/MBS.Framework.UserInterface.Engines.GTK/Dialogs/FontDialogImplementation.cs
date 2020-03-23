//
//  FontDialogImplementation.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(FontDialog))]
	public class FontDialogImplementation : GTKDialogImplementation
	{
		public FontDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			IntPtr handle = (Handle as GTKNativeControl).Handle;
			FontDialog dlg = (Control as FontDialog);
			MBS.Framework.UserInterface.Drawing.Font font = Internal.GTK.Methods.GtkFontChooserDialog.gtk_font_dialog_get_font(handle, !dlg.AutoUpgradeEnabled);
			dlg.SelectedFont = font;
			return true;
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			FontDialog dlg = (Control as FontDialog);
			string title = dlg.Text;
			if (title == null)
				title = "Select Font";

			IntPtr handle = Internal.GTK.Methods.GtkFontChooserDialog.gtk_font_dialog_new(title, (Engine as GTKEngine).CommonDialog_GetParentHandle(dlg), !dlg.AutoUpgradeEnabled);
			return new GTKNativeControl(handle);
		}
	}
}
