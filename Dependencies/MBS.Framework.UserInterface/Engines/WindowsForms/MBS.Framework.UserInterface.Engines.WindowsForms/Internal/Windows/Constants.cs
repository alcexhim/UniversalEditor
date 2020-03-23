//
//  Constants.cs
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
namespace MBS.Framework.UserInterface.Engines.WindowsForms.Internal.Windows
{
	internal static class Constants
	{
		public enum ShowWindowCommand
		{
			/// <summary>
			/// Minimizes a window, even if the thread that owns the window is not responding. This flag should only be
			/// used when minimizing windows from a different thread.
			/// </summary>
			ForceMinimize = 11,
			/// <summary>
			/// Hides the window and activates another window.
			/// </summary>
			Hide = 0,
			/// <summary>
			/// Maximizes the specified window.
			/// </summary>
			Maximize = 3,
			/// <summary>
			/// Minimizes the specified window and activates the next top-level window in the Z order.
			/// </summary>
			Minimize = 6,
			/// <summary>
			/// Activates and displays the window. If the window is minimized or maximized, the system restores it to its
			/// original size and position. An application should specify this flag when restoring a minimized window.
			/// </summary>
			Restore = 9,
			/// <summary>
			/// Activates the window and displays it in its current size and position.
			/// </summary>
			Show = 5,
			/// <summary>
			/// Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the
			/// CreateProcess function by the program that started the application.
			/// </summary>
			ShowDefault = 10,
			/// <summary>
			/// Activates the window and displays it as a maximized window.
			/// </summary>
			ShowMaximized = 3,
			/// <summary>
			/// Activates the window and displays it as a minimized window.
			/// </summary>
			ShowMinimized = 2,
			/// <summary>
			/// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window
			/// is not activated.
			/// </summary>
			ShowMinimizedNoActive = 7,
			/// <summary>
			/// Displays the window in its current size and position. This value is similar to SW_SHOW, except that the
			/// window is not activated.
			/// </summary>
			ShowNoActivate = 8,
			/// <summary>
			/// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except
			/// that the window is not activated.
			/// </summary>
			ShowNormalNoActivate = 4,
			/// <summary>
			/// Activates and displays a window. If the window is minimized or maximized, the system restores it to its
			/// original size and position. An application should specify this flag when displaying the window for the
			/// first time. 
			/// </summary>
			ShowNormal = 1
		}
		[Flags()]
		public enum TaskDialogFlags : int
		{
			None = 0,
			/// <summary>
			/// 	<para>
			/// 		Enables hyperlink processing for the strings specified in the pszContent, pszExpandedInformation and pszFooter members. When enabled, these
			/// 		members may point to strings that contain hyperlinks in the following form: <code>&lt;A HREF="str"&gt;Hyperlink&lt;/A&gt;</code>
			/// 	</para>
			/// 	<para>
			/// 		Warning: Enabling hyperlinks when using content from an unsafe source may cause security vulnerabilities.
			/// 	</para>
			/// 	<para>
			/// 		Note: Task Dialogs will not actually execute any hyperlinks. Hyperlink execution must be handled in the callback function specified by
			/// 		pfCallback. For more details, see TaskDialogCallbackProc.
			/// 	</para>
			/// </summary>
			EnableHyperlinks = 0x1,
			/// <summary>
			/// Indicates that the dialog should use the icon referenced by the handle in the hMainIcon member as the primary icon in the task dialog. If this flag is
			/// specified, the pszMainIcon member is ignored. 
			/// </summary>
			UseHIconMain = 0x2,
			/// <summary>
			/// Indicates that the dialog should use the icon referenced by the handle in the hFooterIcon member as the footer icon in the task dialog. If this flag is
			/// specified, the pszFooterIcon member is ignored. 
			/// </summary>
			UseHIconFooter = 0x4,
			/// <summary>
			/// Indicates that the dialog should be able to be closed using Alt-F4, Escape, and the title bar's close button even if no cancel button is specified in either
			/// the dwCommonButtons or pButtons members. 
			/// </summary>
			AllowDialogCancellation = 0x8,
			/// <summary>
			/// Indicates that the buttons specified in the pButtons member are to be displayed as command links (using a standard task dialog glyph) instead of push
			/// buttons. When using command links, all characters up to the first new line character in the pszButtonText member will be treated as the command link's
			/// main text, and the remainder will be treated as the command link's note. This flag is ignored if the cButtons member is zero. 
			/// </summary>
			UseCommandLinks = 0x10,
			/// <summary>
			/// Indicates that the buttons specified in the pButtons member are to be displayed as command links (without a glyph) instead of push buttons. When using
			/// command links, all characters up to the first new line character in the pszButtonText member will be treated as the command link's main text, and the
			/// remainder will be treated as the command link's note. This flag is ignored if the cButtons member is zero.
			/// </summary>
			UseCommandLinksNoIcon = 0x20,
			/// <summary>
			/// Indicates that the string specified by the pszExpandedInformation member is displayed at the bottom of the dialog's footer area instead of immediately
			/// after the dialog's content. This flag is ignored if the pszExpandedInformation member is NULL. 
			/// </summary>
			ExpandFooterArea = 0x40,
			/// <summary>
			/// Indicates that the string specified by the pszExpandedInformation member is displayed when the dialog is initially displayed. This flag is ignored if the
			/// pszExpandedInformation member is NULL. 
			/// </summary>
			ExpandedByDefault = 0x80,
			/// <summary>
			/// Indicates that the verification checkbox in the dialog is checked when the dialog is initially displayed. This flag is ignored if the pszVerificationText
			/// parameter is NULL. 
			/// </summary>
			VerificationFlagChecked = 0x100,
			/// <summary>
			/// Indicates that a Progress Bar is to be displayed.
			/// </summary>
			ShowProgressBar = 0x200,
			/// <summary>
			/// Indicates that an Marquee Progress Bar is to be displayed. 
			/// </summary>
			ShowMarqueeProgressBar = 0x400,
			/// <summary>
			/// Indicates that the task dialog's callback is to be called approximately every 200 milliseconds.
			/// </summary>
			CallbackTimer = 0x800,
			/// <summary>
			/// Indicates that the task dialog is positioned (centered) relative to the window specified by hwndParent. If the flag is not supplied (or no hwndParent
			/// member is specified), the task dialog is positioned (centered) relative to the monitor.
			/// </summary>
			PositionRelativeToWindow = 0x1000,
			/// <summary>
			/// Indicates that text is displayed reading right to left.
			/// </summary>
			RTLLayout = 0x2000,
			/// <summary>
			/// Indicates that no default item will be selected.
			/// </summary>
			NoDefaultRadioButton = 0x4000,
			/// <summary>
			/// Indicates that the task dialog can be minimized.
			/// </summary>
			CanBeMinimized = 0x8000,
			/// <summary>
			/// Indicates that the width of the task dialog is determined by the width of its content area. This flag is ignored if cxWidth is not set to 0.
			/// </summary>
			SizeToContent = 0x01000000
		}

		[Flags()]
		public enum TaskDialogCommonButtonFlags : int
		{
			/// <summary>
			/// The task dialog contains the push button: OK.
			/// </summary>
			OK = 0x1,
			/// <summary>
			/// The task dialog contains the push button: Yes.
			/// </summary>
			Yes = 0x2,
			/// <summary>
			/// The task dialog contains the push button: No.
			/// </summary>
			No = 0x4,
			/// <summary>
			/// The task dialog contains the push button: Cancel. If this button is specified, the task dialog will respond to typical cancel actions (Alt-F4 and Escape).
			/// </summary>
			Cancel = 0x8,
			/// <summary>
			/// The task dialog contains the push button: Retry.
			/// </summary>
			Retry = 0x10,
			/// <summary>
			/// The task dialog contains the push button: Close.
			/// </summary>
			Close = 0x20
		}

        public enum TaskDialogResult
        {
            OK = 1,
            Cancel = 2,
            Retry = 4,
            Yes = 6,
            No = 7 
        }

        public enum WindowLong
		{
			HInstance = -6,
			ID = -12,
			Style = -16,
			ExtendedStyle = -20
		}
	}
}
