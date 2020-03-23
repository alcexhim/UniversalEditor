using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs.Internal
{
	internal class Constants
	{
		public const int MAXPATH = 256;

		[Flags]
		public enum BrowseInfoFlags : uint
		{
			None = 0x00000000,
			/// <summary>
			/// Only return file system directories.
			///
			/// If the user selects folders that are not part of the file system,
			/// the OK button is grayed.
			/// </summary>
			OnlyFileSystemDirectories = 0x00000001,
			/// <summary>
			/// Do not include network folders below the domain level in the dialog box's tree view control.
			/// </summary>
			ExcludeNetworkFoldersBelowDomain = 0x00000002,
			/// <summary>
			/// Include a status area in the dialog box.
			/// The callback function can set the status text by sending messages to the dialog box.
			/// This flag is not supported when <bold>BIF_NEWDIALOGSTYLE</bold> is specified
			/// </summary>
			StatusText = 0x00000004,
			/// <summary>
			/// Only return file system ancestors.
			/// An ancestor is a subfolder that is beneath the root folder in the namespace hierarchy.
			/// If the user selects an ancestor of the root folder that is not part of the file system, the OK button is grayed
			/// </summary>
			OnlyFileSystemAncestors = 0x00000008,
			/// <summary>
			/// Include an edit control in the browse dialog box that allows the user to type the name of an item.
			/// </summary>
			EditBox = 0x00000010,
			/// <summary>
			/// If the user types an invalid name into the edit box, the browse dialog box calls the application's BrowseCallbackProc with the BFFM_VALIDATEFAILED message.
			/// This flag is ignored if <bold>BIF_EDITBOX</bold> is not specified.
			/// </summary>
			Validate = 0x00000020,
			/// <summary>
			/// Use the new user interface.
			/// Setting this flag provides the user with a larger dialog box that can be resized.
			/// The dialog box has several new capabilities, including: drag-and-drop capability within the
			/// dialog box, reordering, shortcut menus, new folders, delete, and other shortcut menu commands.
			/// </summary>
			NewDialogStyle = 0x00000040,
			/// <summary>
			/// The browse dialog box can display URLs. The <bold>BIF_USENEWUI</bold> and <bold>BIF_BROWSEINCLUDEFILES</bold> flags must also be set.
			/// If any of these three flags are not set, the browser dialog box rejects URLs.
			/// </summary>
			IncludeURLs = 0x00000080,
			/// <summary>
			/// Use the new user interface, including an edit box. This flag is equivalent to <bold>BIF_EDITBOX | BIF_NEWDIALOGSTYLE</bold>
			/// </summary>
			UseNewUI = (EditBox | NewDialogStyle),
			/// <summary>
			/// When combined with <see cref="NewDialogStyle"/>, adds a
			/// usage hint to the dialog box, in place of the edit box.
			/// <bold>BIF_EDITBOX</bold> overrides this flag.
			/// </summary>
			UsageHint = 0x00000100,
			/// <summary>
			/// Do not include the New Folder button in the browse dialog box.
			/// </summary>
			NoNewFolderButton = 0x00000200,
			/// <summary>
			/// When the selected item is a shortcut, return the PIDL of
			/// the shortcut itself rather than its target.
			/// </summary>
			NoTranslateShortcutTargets = 0x00000400,
			/// <summary>
			/// Only return computers. If the user selects anything other than a computer, the OK button is grayed.
			/// </summary>
			IncludeComputers = 0x00001000,
			/// <summary>
			/// Only allow the selection of printers. If the user selects anything other than a printer, the OK button is grayed
			/// </summary>
			IncludePrinters = 0x00002000,
			/// <summary>
			/// The browse dialog box displays files as well as folders.
			/// </summary>
			IncludeFiles = 0x00004000,
			/// <summary>
			/// The browse dialog box can display shareable resources on remote systems.
			/// </summary>
			IncludeSharableResources = 0x00008000,
			/// <summary>
			/// Allow folder junctions such as a library or a compressed file with a .zip file name extension to be browsed.
			/// </summary>
			BrowseJunctions = 0x00010000
		}
	}
}
