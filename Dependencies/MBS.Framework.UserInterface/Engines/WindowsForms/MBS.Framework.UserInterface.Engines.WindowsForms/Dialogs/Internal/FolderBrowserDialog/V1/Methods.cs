using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs.Internal
{
	internal class Methods
	{
		[DllImport("shell32.dll")]
		public static extern IntPtr SHBrowseForFolder(ref Structures.BROWSEINFO lpbi);

		// Note that the BROWSEINFO object's pszDisplayName only gives you the name of the folder.
		// To get the actual path, you need to parse the returned PIDL
		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
	}
}
