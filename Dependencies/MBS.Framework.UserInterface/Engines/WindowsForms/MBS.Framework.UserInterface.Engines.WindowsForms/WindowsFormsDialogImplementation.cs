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

namespace MBS.Framework.UserInterface.Engines.WindowsForms
{
	public abstract class WindowsFormsDialogImplementation : WindowsFormsNativeImplementation
	{
		public WindowsFormsDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected abstract bool AcceptInternal();
		public bool Accept()
		{
			return AcceptInternal();
		}

		private bool IsSuggestedResponse(int responseValue)
		{
			return (responseValue == (int)DialogResult.OK || responseValue == (int)DialogResult.Yes || responseValue == (int)DialogResult.Continue || responseValue == (int)DialogResult.Retry);
		}

		private IntPtr[] Dialog_AddButtons(IntPtr handle, List<Button> buttons, bool autoAlign)
		{
			List<IntPtr> list = new List<IntPtr>();
			/*
			if (!autoAlign)
			{
				for (int i = 0; i < buttons.Count; i++)
				{
					IntPtr hButton = Dialog_AddButton(handle, buttons[i]);
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
						IntPtr hButton = Dialog_AddButton(handle, buttons[i]);
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
						IntPtr hButton = Dialog_AddButton(handle, buttons[i]);
						list.Add(hButton);
					}
					break;
				}
			}
			*/
			return list.ToArray();
		}

		protected abstract WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons);
		protected override NativeControl CreateControlInternal(Control control)
		{
			List<Button> buttons = new List<Button>();

			Dialog dialog = (control as Dialog);
			WindowsFormsNativeDialog nc = CreateDialogInternal(dialog, buttons) as WindowsFormsNativeDialog;

			if (nc != null)
			{
				System.Windows.Forms.CommonDialog handle = (nc as WindowsFormsNativeDialog).Handle;

				// Add any additional buttons to the end of the buttons list
				foreach (Button button in dialog.Buttons)
				{
					buttons.Add(button);
				}

				// IntPtr[] hButtons = Dialog_AddButtons(handle, buttons, dialog.AutoAlignButtons);
				if (dialog.DefaultButton != null)
				{
				}

				InvokeMethod(dialog, "OnCreated", EventArgs.Empty);
			}
			return nc;
		}

		public virtual DialogResult Run(System.Windows.Forms.IWin32Window parentHandle)
		{
			WindowsFormsNativeDialog nc = (Handle as WindowsFormsNativeDialog);
			if (nc == null) return DialogResult.None;

			System.Windows.Forms.CommonDialog handle = nc.Handle;
			DialogResult result = DialogResult.None;
			if (handle != null)
			{
				System.Windows.Forms.DialogResult nativeResult = handle.ShowDialog(parentHandle);
				if (nc.Form != null)
				{
					nativeResult = nc.Form.DialogResult;
				}

				switch (nativeResult)
				{
					case System.Windows.Forms.DialogResult.OK:
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
					case System.Windows.Forms.DialogResult.Cancel:
					{
						result = DialogResult.Cancel;
						break;
					}
					case System.Windows.Forms.DialogResult.No:
					{
						result = DialogResult.No;
						break;
					}
					case System.Windows.Forms.DialogResult.None:
					{
						result = DialogResult.None;
						break;
					}
					case System.Windows.Forms.DialogResult.Yes:
					{
						result = DialogResult.Yes;
						break;
					}
					case System.Windows.Forms.DialogResult.Abort:
					{
						result = DialogResult.Abort;
						break;
					}
					case System.Windows.Forms.DialogResult.Ignore:
					{
						result = DialogResult.Ignore;
						break;
					}
					case System.Windows.Forms.DialogResult.Retry:
					{
						result = DialogResult.Retry;
						break;
					}
				}
			}
			return result;
		}
	}
}
