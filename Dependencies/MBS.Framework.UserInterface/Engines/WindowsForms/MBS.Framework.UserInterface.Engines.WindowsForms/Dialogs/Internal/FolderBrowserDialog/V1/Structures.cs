using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs.Internal
{
	internal class Structures
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct BROWSEINFO
		{
			/// <summary>
			/// A handle to the owner window for the dialog box.
			/// </summary>
			public IntPtr hwndOwner;
			/// <summary>
			/// A PIDL that specifies the location of the root folder from
			/// which to start browsing. Only the specified folder and its
			/// subfolders in the namespace hierarchy appear in the dialog
			/// box. This member can be NULL; in that case, the namespace
			/// root (the Desktop folder) is used.
			/// </summary>
			public IntPtr pidlRoot;
			/// <summary>
			/// Pointer to a buffer to receive the display name of the
			/// folder selected by the user. The size of this buffer is
			/// assumed to be MAX_PATH characters.
			/// </summary>
			public IntPtr pszDisplayName;
			/// <summary>
			/// Pointer to a null-terminated string that is displayed above
			/// the tree view control in the dialog box. This string can be
			/// used to specify instructions to the user.
			/// </summary>
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpszTitle;
			/// <summary>
			/// Flags that specify the options for the dialog box.
			/// </summary>
			public Constants.BrowseInfoFlags ulFlags;
			/// <summary>
			/// Pointer to an application-defined function that the dialog
			/// box calls when an event occurs. This member can be null.
			/// </summary>
			public Delegates.BrowseCallbackProc lpfn;
			/// <summary>
			/// An application-defined value that the dialog box passes to
			/// the callback function, if one is specified in lpfn.
			/// </summary>
			public IntPtr lParam;
			/// <summary>
			/// An integer value that receives the index of the image
			/// associated with the selected folder, stored in the system
			/// image list.
			/// </summary>
			public int iImage;
		}
	}
}
