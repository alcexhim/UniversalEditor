//
//  AppChooserDialogImplementation.cs
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
	[ControlImplementation(typeof(AppChooserDialog))]
	public class AppChooserDialogImplementation : GTKDialogImplementation
	{
		public AppChooserDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			throw new NotImplementedException();
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			AppChooserDialog dlg = (dialog as AppChooserDialog);
			IntPtr handle = Internal.GTK.Methods.GtkAppChooserDialog.gtk_app_chooser_dialog_new_for_content_type((Engine as GTKEngine).CommonDialog_GetParentHandle(dlg), Internal.GTK.Constants.GtkDialogFlags.Modal, dlg.ContentType);
			return new GTKNativeControl(handle);
		}
	}
}
