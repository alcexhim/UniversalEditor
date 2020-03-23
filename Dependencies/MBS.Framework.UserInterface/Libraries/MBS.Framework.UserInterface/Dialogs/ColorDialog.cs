using System;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Dialogs
{
	public class ColorDialog : CommonDialog
	{
		private bool mvarAutoUpgradeEnabled = true;
		/// <summary>
		/// Determines whether to use the latest version of the common dialog in the current toolkit. Certain toolkits
		/// (such as GTK) change the common dialog functionality in new releases. Set to false to use the common dialog
		/// from the previous version.
		/// </summary>
		/// <value><c>true</c> if auto upgrade enabled; otherwise, <c>false</c>.</value>
		public bool AutoUpgradeEnabled { get { return mvarAutoUpgradeEnabled; } set { mvarAutoUpgradeEnabled = value; } }

		private Color mvarSelectedColor = Color.Empty;
		public Color SelectedColor { get { return mvarSelectedColor; } set { mvarSelectedColor = value; } }
	}
}

