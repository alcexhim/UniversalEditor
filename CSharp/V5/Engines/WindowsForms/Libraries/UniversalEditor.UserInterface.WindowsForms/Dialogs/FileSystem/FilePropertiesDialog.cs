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

			dlg.txtGeneralInformationLocation.Text = mvarParentFileName;
			dlg.SelectedObjects = mvarSelectedObjects;
			return dlg.ShowDialog();
		}

		private string mvarParentFileName = String.Empty;
		/// <summary>
		/// The directory or file name of the parent location that contains the file whose properties are to be displayed.
		/// </summary>
		public string ParentFileName { get { return mvarParentFileName; } set { mvarParentFileName = value; } }
	}
}
