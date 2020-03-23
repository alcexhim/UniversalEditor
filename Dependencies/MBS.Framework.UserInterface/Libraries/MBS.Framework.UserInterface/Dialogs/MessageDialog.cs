using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Dialogs
{
	public enum MessageDialogModality
	{
		/// <summary>
		/// The user must respond to the message box before continuing work in the window identified by the hWnd parameter. However, the user can move to the windows of other
		/// threads and work in those windows.
		/// 
		/// Depending on the hierarchy of windows in the application, the user may be able to move to other windows within the thread. All child windows of the parent of the
		/// message box are automatically disabled, but pop-up windows are not.
		/// 
		/// MB_APPLMODAL is the default if neither MB_SYSTEMMODAL nor MB_TASKMODAL is specified.
		/// </summary>
		ApplicationModal = 0x00000000,
		/// <summary>
		/// Same as MB_APPLMODAL except that the message box has the WS_EX_TOPMOST style. Use system-modal message boxes to notify the user of serious, potentially damaging errors
		/// that require immediate attention (for example, running out of memory). This flag has no effect on the user's ability to interact with windows other than those
		/// associated with hWnd.
		/// </summary>
		SystemModal = 0x00001000,
		/// <summary>
		/// Same as MB_APPLMODAL except that all the top-level windows belonging to the current thread are disabled if the hWnd parameter is NULL. Use this flag when the calling
		/// application or library does not have a window handle available but still needs to prevent input to other windows in the calling thread without suspending other
		/// threads.
		/// </summary>
		TaskModal = 0x00002000
	}
	public enum MessageDialogButtons
	{
		/// <summary>
		/// The message box contains one push button: OK. This is the default.
		/// </summary>
		OK = 0x00000000,
		/// <summary>
		/// The message box contains two push buttons: OK and Cancel.
		/// </summary>
		OKCancel = 0x00000001,
		/// <summary>
		/// The message box contains three push buttons: Abort, Retry, and Ignore.
		/// </summary>
		AbortRetryIgnore = 0x00000002,
		/// <summary>
		/// The message box contains three push buttons: Yes, No, and Cancel.
		/// </summary>
		YesNoCancel = 0x0000003,
		/// <summary>
		/// The message box contains two push buttons: Yes and No.
		/// </summary>
		YesNo = 0x0000004,
		/// <summary>
		/// The message box contains two push buttons: Retry and Cancel.
		/// </summary>
		RetryCancel = 0x00000005,
		/// <summary>
		/// The message box contains three push buttons: Cancel, Try Again, Continue. Use this message box type instead of <see cref="AbortRetryIgnore" />.
		/// </summary>
		CancelTryContinue = 0x00000006
	}
	public enum MessageDialogIcon
	{
		None = 0x00000000,
		/// <summary>
		/// A stop-sign icon appears in the message box.
		/// </summary>
		Error = 0x00000010,
		/// <summary>
		/// A question-mark icon appears in the message box. The question-mark message icon is no longer recommended because it does not clearly represent a specific type of
		/// message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the message symbol question mark with
		/// Help information. Therefore, do not use this question mark message symbol in your message boxes. The system continues to support its inclusion only for backward
		/// compatibility.
		/// </summary>
		[Obsolete("The question-mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type.")]
		Question = 0x00000020,
		/// <summary>
		/// An exclamation-point icon appears in the message box.
		/// </summary>
		Warning = 0x00000030,
		/// <summary>
		/// An icon consisting of a lowercase letter i in a circle appears in the message box.
		/// </summary>
		Information = 0x00000040
	}
	public class MessageDialog : CommonDialog
	{
		private string mvarContent = String.Empty;
		public string Content { get { return mvarContent; } set { mvarContent = value; } }

		private MessageDialogButtons mvarButtons = MessageDialogButtons.OK;
		public MessageDialogButtons Buttons { get { return mvarButtons; } set { mvarButtons = value; } }

		private MessageDialogIcon mvarIcon = MessageDialogIcon.None;
		public MessageDialogIcon Icon { get { return mvarIcon; } set { mvarIcon = value; } }

		private MessageDialogModality mvarModality = MessageDialogModality.ApplicationModal;
		public MessageDialogModality Modality { get { return mvarModality; } set { mvarModality = value; } }

		private bool mvarShowHelp = false;
		public bool ShowHelp { get { return mvarShowHelp; } set { mvarShowHelp = value; } }

		public static DialogResult ShowDialog(string prompt, string title = null, MessageDialogButtons buttons = MessageDialogButtons.OK, MessageDialogIcon icon = MessageDialogIcon.None, MessageDialogModality modality = MessageDialogModality.ApplicationModal, bool showHelp = false, Control parent = null)
		{
			MessageDialog dialog = new MessageDialog();
			dialog.Buttons = buttons;
			dialog.Content = prompt;
			dialog.Modality = modality;
			dialog.Icon = icon;
			dialog.Text = title;
			dialog.ShowHelp = showHelp;
			dialog.Parent = parent;
			return dialog.ShowDialog();
		}

		public static MessageDialog Create(string prompt, string title = null, MessageDialogButtons buttons = MessageDialogButtons.OK, MessageDialogIcon icon = MessageDialogIcon.None, MessageDialogModality modality = MessageDialogModality.ApplicationModal, bool showHelp = false, Control parent = null)
		{
			MessageDialog dialog = new MessageDialog();
			dialog.Buttons = buttons;
			dialog.Content = prompt;
			dialog.Modality = modality;
			dialog.Icon = icon;
			dialog.Text = title;
			dialog.ShowHelp = showHelp;
			dialog.Parent = parent;
			return dialog;
		}

		private DialogResult mvarDefaultButton = DialogResult.OK;
		public DialogResult DefaultButton { get { return mvarDefaultButton; } set { mvarDefaultButton = value; } }

		private bool mvarDefaultDesktopOnly = false;
		public bool DefaultDesktopOnly { get { return mvarDefaultDesktopOnly; } set { mvarDefaultDesktopOnly = value; } }

		private bool mvarServiceNotification = false;
		public bool ServiceNotification { get { return mvarServiceNotification; } set { mvarServiceNotification = value; } }
	}
}
