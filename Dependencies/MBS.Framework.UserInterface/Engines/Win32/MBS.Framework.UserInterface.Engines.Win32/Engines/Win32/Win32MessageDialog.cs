using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalWidgetToolkit.Dialogs;

namespace UniversalWidgetToolkit.Engines.Win32
{
	internal static class Win32MessageDialog
	{
		public static Internal.Windows.Constants.User32.MessageDialogStyles GetMessageDialogStyles(MessageDialog dialog)
		{
			Internal.Windows.Constants.User32.MessageDialogStyles styles = Internal.Windows.Constants.User32.MessageDialogStyles.OK;
			switch (dialog.Buttons)
			{
				case MessageDialogButtons.AbortRetryIgnore:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.AbortRetryIgnore;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.Abort:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.Retry:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Ignore:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton4;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.CancelTryContinue:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.CancelTryContinue;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.Cancel:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.TryAgain:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Continue:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton4;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.OK:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.OK;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.OK:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.OKCancel:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.OKCancel;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.OK:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.Cancel:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.RetryCancel:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.RetryCancel;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.Retry:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.Cancel:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.YesNo:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.YesNo;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.Yes:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.No:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
					}
					break;
				}
				case MessageDialogButtons.YesNoCancel:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.YesNoCancel;
					switch (dialog.DefaultButton)
					{
						case CommonDialogResult.Yes:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton1;
							break;
						}
						case CommonDialogResult.No:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton2;
							break;
						}
						case CommonDialogResult.Cancel:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton3;
							break;
						}
						case CommonDialogResult.Help:
						{
							styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultButton4;
							break;
						}
					}
					break;
				}
			}
			switch (dialog.Icon)
			{
				case MessageDialogIcon.Error:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.IconError;
					break;
				}
				case MessageDialogIcon.Information:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.IconInformation;
					break;
				}
				case MessageDialogIcon.Question:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.IconQuestion;
					break;
				}
				case MessageDialogIcon.Warning:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.IconWarning;
					break;
				}
			}

			switch (dialog.Modality)
			{
				case MessageDialogModality.ApplicationModal:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.ApplicationModal;
					break;
				}
				case MessageDialogModality.SystemModal:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.SystemModal;
					break;
				}
				case MessageDialogModality.TaskModal:
				{
					styles |= Internal.Windows.Constants.User32.MessageDialogStyles.TaskModal;
					break;
				}
			}

			if (dialog.ShowHelp) styles |= Internal.Windows.Constants.User32.MessageDialogStyles.Help;

			if (dialog.DefaultDesktopOnly) styles |= Internal.Windows.Constants.User32.MessageDialogStyles.DefaultDesktopOnly;
			if (dialog.ServiceNotification) styles |= Internal.Windows.Constants.User32.MessageDialogStyles.ServiceNotification;

			return styles;
		}
	}
}
