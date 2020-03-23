//
//  GTKDialogImplementation.cs
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

namespace MBS.Framework.UserInterface.Engines.GTK
{
	public abstract class GTKDialogImplementation : GTKNativeImplementation
	{
		public GTKDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
			// ewww
			gc_delete_event_handler = new Func<IntPtr, IntPtr, bool>(gc_delete_event);
		}

		protected virtual bool UseCustomButtonImplementation { get; } = false;

		protected abstract bool AcceptInternal();
		public bool Accept()
		{
			return AcceptInternal();
		}
		
		private IntPtr Dialog_AddButton(Dialog dialog, IntPtr handle, Button button)
		{
			/*
			if (button.ResponseValue == (int)DialogResult.Cancel)
				return IntPtr.Zero;
			*/

			IntPtr buttonHandle = IntPtr.Zero;
			if (!(Engine as GTKEngine).IsControlCreated(button))
				(Engine as GTKEngine).CreateControl(button);

			buttonHandle = ((Engine as GTKEngine).GetHandleForControl(button) as GTKNativeControl).Handle;

			if (IsSuggestedButton(dialog, button))
			{
				if (Internal.GTK.Methods.Gtk.LIBRARY_FILENAME == Internal.GTK.Methods.Gtk.LIBRARY_FILENAME_V2)
				{
				}
				else
				{
					IntPtr hStyleCtx = Internal.GTK.Methods.GtkWidget.gtk_widget_get_style_context(buttonHandle);
					Internal.GTK.Methods.GtkStyleContext.gtk_style_context_add_class(hStyleCtx, "suggested-action");
				}
			}

			int nativeResponseValue = button.ResponseValue;
			switch (button.ResponseValue)
			{
				case (int)DialogResult.Cancel:
				{
					nativeResponseValue = (int)Internal.GTK.Constants.GtkResponseType.Cancel;
					break;
				}
				case (int)DialogResult.No:
				{
					nativeResponseValue = (int)Internal.GTK.Constants.GtkResponseType.No;
					break;
				}
				case (int)DialogResult.OK:
				{
					nativeResponseValue = (int)Internal.GTK.Constants.GtkResponseType.OK;
					break;
				}
				case (int)DialogResult.Yes:
				{
					nativeResponseValue = (int)Internal.GTK.Constants.GtkResponseType.Yes;
					break;
				}
			}

			Internal.GTK.Methods.GtkDialog.gtk_dialog_add_action_widget(handle, buttonHandle, nativeResponseValue);
			Internal.GTK.Methods.GtkWidget.gtk_widget_set_can_default(buttonHandle, true);

			// UpdateControlProperties(button, buttonHandle);
			// above updatecontrolprops call must be called after buttonHandle is realized

			Internal.GTK.Methods.GtkWidget.gtk_widget_show_all(buttonHandle);
			return buttonHandle;
		}

		private bool IsSuggestedButton(Dialog dialog, Button button)
		{
			return dialog.DefaultButton == button || IsSuggestedResponse(button.ResponseValue);
		}

		private bool IsSuggestedResponse(int responseValue)
		{
			return (responseValue == (int)DialogResult.OK || responseValue == (int)DialogResult.Yes || responseValue == (int)DialogResult.Continue || responseValue == (int)DialogResult.Retry);
		}

		private IntPtr[] Dialog_AddButtons(Dialog dialog, IntPtr handle, List<Button> buttons, bool autoAlign)
		{
			List<IntPtr> list = new List<IntPtr>();
			if (!autoAlign)
			{
				for (int i = 0; i < buttons.Count; i++)
				{
					IntPtr hButton = Dialog_AddButton(dialog, handle, buttons[i]);
					list.Add(hButton);
				}
				return list.ToArray();
			}
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					for (int i = buttons.Count - 1; i > -1; i--)
					{
						IntPtr hButton = Dialog_AddButton(dialog, handle, buttons[i]);
						if (hButton == IntPtr.Zero)
							continue;
						list.Add(hButton);
					}
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					for (int i = 0; i < buttons.Count; i++)
					{
						IntPtr hButton = Dialog_AddButton(dialog, handle, buttons[i]);
						list.Add(hButton);
					}
					break;
				}
			}
			return list.ToArray();
		}

		private Func<IntPtr, IntPtr, bool> gc_delete_event_handler = null;
		private bool gc_delete_event(IntPtr /*GtkWidget*/ widget, IntPtr /*GdkEventKey*/ evt)
		{
			Window window = ((Application.Engine as GTKEngine).GetControlByHandle(widget) as Window);
			if (window != null)
			{
				System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
				InvokeMethod(window, "OnClosing", e);
				if (e.Cancel)
					return true;
			}

			// blatently stolen from GTKNativeImplementation
			// we need to build more GTKNativeImplementation-based dialog impls to avoid code bloat

			// destroy all handles associated with widget
			Control ctl = (Engine as GTKEngine).GetControlByHandle(widget);
			(Engine as GTKEngine).UnregisterControlHandle(ctl);
			return false;
		}

		protected abstract GTKNativeControl CreateDialogInternal(Dialog dialog, List<Button> buttons);
		protected override NativeControl CreateControlInternal(Control control)
		{
			List<Button> buttons = new List<Button>();

			Dialog dialog = (control as Dialog);
			GTKNativeControl nc = CreateDialogInternal(dialog, buttons) as GTKNativeControl;
			IntPtr handle = (nc as GTKNativeControl).Handle;

			// Add any additional buttons to the end of the buttons list
			foreach (Button button in dialog.Buttons)
			{
				buttons.Add(button);
			}

			IntPtr[] hButtons = Dialog_AddButtons(dialog, handle, buttons, dialog.AutoAlignButtons);
			if (dialog.DefaultButton != null)
			{
				IntPtr hButtonDefault = ((Engine as GTKEngine).GetHandleForControl(dialog.DefaultButton) as GTKNativeControl).Handle;
				Internal.GTK.Methods.GtkWidget.gtk_widget_grab_default(hButtonDefault);
			}



			Internal.GTK.Methods.GtkWindow.gtk_window_set_decorated(handle, dialog.Decorated);
			Internal.GTK.Methods.GtkWindow.gtk_window_set_default_size(handle, (int)dialog.Size.Width, (int)dialog.Size.Height);
			Internal.GTK.Methods.GtkWidget.gtk_widget_set_size_request(handle, (int)dialog.MinimumSize.Width, (int)dialog.MinimumSize.Height);

			Internal.GObject.Methods.g_signal_connect(handle, "delete_event", gc_delete_event_handler);
			// Internal.GObject.Methods.g_signal_connect_after(handle, "destroy", gc_Window_Closed, new IntPtr(0xDEADBEEF));

			// if (dialog.AutoUpgradeEnabled) {
			Internal.GLib.Structures.Value val = new MBS.Framework.UserInterface.Engines.GTK.Internal.GLib.Structures.Value(1);
			// }

			InvokeMethod(dialog, "OnCreated", EventArgs.Empty);
			return nc;
		}

		public DialogResult Run(IntPtr parentHandle)
		{
			GTKNativeControl nc = (Handle as GTKNativeControl);
			if (nc == null) return DialogResult.None;

			IntPtr handle = nc.Handle;
			DialogResult result = DialogResult.None;
			if (handle != IntPtr.Zero)
			{
				Internal.GTK.Methods.GtkWindow.gtk_window_set_transient_for(handle, parentHandle);
				Internal.GTK.Methods.GtkDialog.gtk_dialog_set_default_response(handle, (int)Internal.GTK.Constants.GtkResponseType.OK);
				int nativeResult = Internal.GTK.Methods.GtkDialog.gtk_dialog_run(handle);

				switch (nativeResult)
				{
					case (int)Internal.GTK.Constants.GtkResponseType.OK:
					case (int)Internal.GTK.Constants.GtkResponseType.Accept:
					{
						if (Accept())
						{
							result = DialogResult.OK;
						}
						else
						{
							result = DialogResult.Cancel;
						}
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Apply:
					{
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Cancel:
					{
						result = DialogResult.Cancel;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Close:
					{
						result = DialogResult.Cancel;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.DeleteEvent:
					{
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Help:
					{
						result = DialogResult.Help;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.No:
					{
						result = DialogResult.No;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.None:
					{
						result = DialogResult.None;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Reject:
					{
						result = DialogResult.Cancel;
						break;
					}
					case (int)Internal.GTK.Constants.GtkResponseType.Yes:
					{
						result = DialogResult.Yes;
						break;
					}



					case (int)DialogResult.Abort:
					{
						result = DialogResult.Abort;
						break;
					}
					case (int)DialogResult.Cancel:
					{
						result = DialogResult.Cancel;
						break;
					}
					case (int)DialogResult.Continue:
					{
						result = DialogResult.Continue;
						break;
					}
					case (int)DialogResult.Help:
					{
						result = DialogResult.Help;
						break;
					}
					case (int)DialogResult.Ignore:
					{
						result = DialogResult.Ignore;
						break;
					}
					case (int)DialogResult.No:
					{
						result = DialogResult.No;
						break;
					}
					case (int)DialogResult.OK:
					{
						result = DialogResult.OK;
						break;
					}
					case (int)DialogResult.Retry:
					{
						result = DialogResult.Retry;
						break;
					}
					case (int)DialogResult.TryAgain:
					{
						result = DialogResult.TryAgain;
						break;
					}
					case (int)DialogResult.Yes:
					{
						result = DialogResult.Yes;
						break;
					}
				}

				// FIXME: this results in corruption; better to cancel dialog close event?
				if ((nativeResult != 0 && nativeResult != -1) || result == DialogResult.None)
				{
					if (result == DialogResult.None)
						result = (Control as Dialog).DialogResult;
				}

				/*
				for (int i = 0; i < hButtons.Length; i++)
				{
					Internal.GTK.Methods.GtkWidget.gtk_widget_destroy(hButtons[i]);
				}
				*/
				Internal.GTK.Methods.GtkWidget.gtk_widget_destroy(handle);
			}
			return result;
		}
	}
}
