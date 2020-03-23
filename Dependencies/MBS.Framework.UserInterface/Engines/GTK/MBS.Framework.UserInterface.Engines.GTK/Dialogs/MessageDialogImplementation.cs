//
//  MessageDialogImplementation.cs
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
	[ControlImplementation(typeof(MessageDialog))]
	public class MessageDialogImplementation : GTKDialogImplementation
	{
		public MessageDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override bool AcceptInternal()
		{
			// nothing to do here
			return true;
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog control, List<Button> buttons)
		{
			MessageDialog dlg = (control as MessageDialog);

			Internal.GTK.Constants.GtkMessageType messageType = Internal.GTK.Constants.GtkMessageType.Other;
			switch (dlg.Icon)
			{
				case MessageDialogIcon.Error:
				{
					messageType = Internal.GTK.Constants.GtkMessageType.Error;
					break;
				}
				case MessageDialogIcon.Information:
				{
					messageType = Internal.GTK.Constants.GtkMessageType.Info;
					break;
				}
				case MessageDialogIcon.Warning:
				{
					messageType = Internal.GTK.Constants.GtkMessageType.Warning;
					break;
				}
				case MessageDialogIcon.Question:
				{
					messageType = Internal.GTK.Constants.GtkMessageType.Question;
					break;
				}
			}

			Internal.GTK.Constants.GtkButtonsType buttonsType = Internal.GTK.Constants.GtkButtonsType.None;
			switch (dlg.Buttons)
			{
				case MessageDialogButtons.AbortRetryIgnore:
				case MessageDialogButtons.CancelTryContinue:
				case MessageDialogButtons.RetryCancel:
				case MessageDialogButtons.YesNoCancel:
				{
					buttonsType = Internal.GTK.Constants.GtkButtonsType.None;
					break;
				}
				case MessageDialogButtons.OK:
				{
					buttonsType = Internal.GTK.Constants.GtkButtonsType.OK;
					break;
				}
				case MessageDialogButtons.OKCancel:
				{
					buttonsType = Internal.GTK.Constants.GtkButtonsType.OKCancel;
					break;
				}
				case MessageDialogButtons.YesNo:
				{
					buttonsType = Internal.GTK.Constants.GtkButtonsType.YesNo;
					break;
				}
			}

			IntPtr parentHandle = IntPtr.Zero;
			if (dlg.Parent != null)
			{
				parentHandle = ((Engine as GTKEngine).GetHandleForControl(dlg.Parent) as GTKNativeControl).Handle;
			}

			
			switch (dlg.Buttons)
			{
				case MessageDialogButtons.AbortRetryIgnore:
				{
					buttons.Add(new Button("_Abort", DialogResult.Abort));
					buttons.Add(new Button("_Retry", DialogResult.Retry));
					buttons.Add(new Button("_Ignore", DialogResult.Ignore));
					break;
				}
				case MessageDialogButtons.CancelTryContinue:
				{
					buttons.Add(new Button(StockType.Cancel, DialogResult.Abort));
					buttons.Add(new Button("T_ry Again", DialogResult.Retry));
					buttons.Add(new Button("C_ontinue", DialogResult.Continue));
					break;
				}
				case MessageDialogButtons.RetryCancel:
				{
					buttons.Add(new Button("_Retry", DialogResult.Retry));
					buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));
					break;
				}
				case MessageDialogButtons.YesNoCancel:
				{
					buttons.Add(new Button(StockType.Yes, DialogResult.Yes));
					buttons.Add(new Button(StockType.No, DialogResult.No));
					buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));
					break;
				}
			}

			IntPtr handle = Internal.GTK.Methods.GtkMessageDialog.gtk_message_dialog_new(parentHandle, Internal.GTK.Constants.GtkDialogFlags.Modal, messageType, buttonsType, dlg.Content);
			return new GTKNativeControl(handle);
		}
	}
}
