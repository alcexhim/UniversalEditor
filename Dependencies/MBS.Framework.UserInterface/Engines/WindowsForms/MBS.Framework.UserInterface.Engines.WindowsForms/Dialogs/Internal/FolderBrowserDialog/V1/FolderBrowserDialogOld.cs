using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs.Internal.FolderBrowserDialog.V1
{
	internal class FolderBrowserDialogOld
	{
		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private string mvarSelectedPath = String.Empty;
		public string SelectedPath { get { return mvarSelectedPath; } set { mvarSelectedPath = value; } }

		private bool mvarShowNewFolderButton = true;
		public bool ShowNewFolderButton { get { return mvarShowNewFolderButton; } set { mvarShowNewFolderButton = value; } }

		private bool mvarTranslateShortcutTargets = true;
		/// <summary>
		/// When the selected item is a shortcut, return the path of the
		/// shortcut itself rather than its target.
		/// </summary>
		public bool TranslateShortcutTargets { get { return mvarTranslateShortcutTargets; } set { mvarTranslateShortcutTargets = value; } }

		public DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}
		public DialogResult ShowDialog(IWin32Window owner)
		{
			Internal.Structures.BROWSEINFO lpbi = new Internal.Structures.BROWSEINFO();
			lpbi.lpszTitle = mvarDescription;
			Internal.Constants.BrowseInfoFlags flags = Internal.Constants.BrowseInfoFlags.None;
			
			if (mvarAutoUpgradeEnabled) flags |= Internal.Constants.BrowseInfoFlags.UseNewUI;
			if (!mvarShowNewFolderButton) flags |= Internal.Constants.BrowseInfoFlags.NoNewFolderButton;
			if (!mvarTranslateShortcutTargets) flags |= Internal.Constants.BrowseInfoFlags.NoTranslateShortcutTargets;

			flags |= Internal.Constants.BrowseInfoFlags.OnlyFileSystemDirectories;
			lpbi.ulFlags = flags;

			if (owner != null) lpbi.hwndOwner = owner.Handle;

			IntPtr pidl = Internal.Methods.SHBrowseForFolder(ref lpbi);
			if (pidl == IntPtr.Zero) return DialogResult.Cancel;

			IntPtr bufferAddress = Marshal.AllocHGlobal(256);
			StringBuilder sb = new StringBuilder(Internal.Constants.MAXPATH);
			if (true != Internal.Methods.SHGetPathFromIDList(pidl, bufferAddress))
			{
				throw new InvalidOperationException("Invalid PIDL");
			}
			sb.Append(Marshal.PtrToStringAuto(bufferAddress));
			
			mvarSelectedPath = sb.ToString();

			// Caller is responsible for freeing this memory.
			Marshal.FreeCoTaskMem(pidl);

			return DialogResult.OK;
		}

		private bool mvarAutoUpgradeEnabled = true;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="FolderBrowserDialog" /> instance should automatically
		/// upgrade appearance and behavior when running on Windows Vista.
		/// </summary>
		/// <returns>true if this <see cref="FolderBrowserDialog" /> instance should automatically upgrade appearance and behavior when running on Windows Vista; otherwise, false. The default is true.</returns>
		[DefaultValue(true)]
		public bool AutoUpgradeEnabled { get { return mvarAutoUpgradeEnabled; } set { mvarAutoUpgradeEnabled = value; } }
	}
}
