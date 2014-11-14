using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs.FileSystem
{
	public class FilePropertiesDialog : Dialog
	{
		private Internal.FilePropertiesDialogImpl dlg = null;

		private IFileSystemObjectCollection mvarSelectedObjects = new IFileSystemObjectCollection();
		public IFileSystemObjectCollection SelectedObjects { get { return mvarSelectedObjects; } }

		public override System.Windows.Forms.DialogResult ShowDialog()
		{
			if (dlg == null) dlg = new Internal.FilePropertiesDialogImpl();
			if (dlg.IsDisposed) dlg = new Internal.FilePropertiesDialogImpl();

			dlg.SelectedObjects = mvarSelectedObjects;
			return dlg.ShowDialog();
		}
	}
}
