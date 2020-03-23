//
//  PrintDialogImplementation.cs
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
using MBS.Framework.UserInterface.Dialogs.Native;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Engines.GTK.Printing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Printing;

namespace MBS.Framework.UserInterface.Engines.GTK.Dialogs
{
	[ControlImplementation(typeof(PrintDialog))]
	public class PrintDialogImplementation : GTKDialogImplementation, IPrintDialogImplementation
	{
		public PrintDialogImplementation(Engine engine, Control control)
			: base(engine, control)
		{
			// begin_print_handler = new Internal.GObject.Delegates.GCallbackV3I(begin_print);
		}

		public Printer GetSelectedPrinter()
		{
			IntPtr h = (Handle as GTKNativeControl).Handle;
			IntPtr /*GtkPrinter*/ hPrinter = Internal.GTK.Methods.GtkPrintUnixDialog.gtk_print_unix_dialog_get_selected_printer(h);
			return new GTKPrinter(hPrinter);
		}

		public void SetSelectedPrinter(Printer value)
		{
			// huh?
		}


		public PrintSettings GetSettings()
		{
			return null;
		}
		public void SetSettings(PrintSettings value)
		{
		}

		protected override void RegisterDragSourceInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
		}

		protected override void RegisterDropTargetInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
		}

		protected override void SetControlVisibilityInternal(bool visible)
		{
		}

		protected override void SetFocusInternal()
		{
		}

		protected override bool AcceptInternal()
		{
			throw new NotImplementedException();
		}

		protected override GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			GTKNativeControl nc = (Engine.GetHandleForControl(dialog.ParentWindow) as GTKNativeControl);
			IntPtr hParentWindow = (nc == null ? IntPtr.Zero : nc.Handle);

			IntPtr handle = Internal.GTK.Methods.GtkPrintUnixDialog.gtk_print_unix_dialog_new(dialog.Text, hParentWindow);

			// Internal.GObject.Methods.g_signal_connect(handle, "begin_print", begin_print_handler);
			return new GTKNativeControl(handle);
		}
	}
}
