using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript
{
	public class InstallationScriptObjectModel : ObjectModel
	{
		private string mvarProductName = String.Empty;
		public string ProductName { get { return mvarProductName; } set { mvarProductName = value; } }

		private Version mvarProductVersion = new Version(1, 0);
		public Version ProductVersion { get { return mvarProductVersion; } set { mvarProductVersion = value; } }

		private string mvarBackgroundImageFileName = String.Empty;
		public string BackgroundImageFileName { get { return mvarBackgroundImageFileName; } set { mvarBackgroundImageFileName = value; } }

		private Dialog.DialogCollection mvarDialogs = new Dialog.DialogCollection();
		public Dialog.DialogCollection Dialogs { get { return mvarDialogs; } }

		private string mvarStartMenuDirectoryName = String.Empty;
		public string StartMenuDirectoryName { get { return mvarStartMenuDirectoryName; } set { mvarStartMenuDirectoryName = value; } }

		public override void Clear()
		{
			mvarProductName = String.Empty;
			mvarProductVersion = new Version(1, 0);
			mvarBackgroundImageFileName = String.Empty;
			mvarDialogs.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			InstallationScriptObjectModel clone = (where as InstallationScriptObjectModel);
			if (clone == null) throw new InvalidOperationException();

			clone.ProductName = (mvarProductName.Clone() as string);
			clone.ProductVersion = (mvarProductVersion.Clone() as Version);
			clone.BackgroundImageFileName = (mvarBackgroundImageFileName.Clone() as string);
			foreach (Dialog dialog in mvarDialogs)
			{
				clone.Dialogs.Add(dialog.Clone() as Dialog);
			}
		}
	}
}
