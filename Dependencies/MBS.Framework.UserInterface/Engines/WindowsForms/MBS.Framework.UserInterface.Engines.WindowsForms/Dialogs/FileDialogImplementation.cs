using System;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Dialogs
{
	[ControlImplementation(typeof(FileDialog))]
	public class FileDialogImplementation : WindowsFormsDialogImplementation
	{
		public FileDialogImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		public override DialogResult Run(System.Windows.Forms.IWin32Window parentHandle)
		{
			FileDialog dlg = (Control as FileDialog);
			if (dlg.Mode == FileDialogMode.SelectFolder || dlg.Mode == FileDialogMode.CreateFolder)
			{
				Internal.FolderBrowserDialog.V2.FolderSelectDialog fbd = new Internal.FolderBrowserDialog.V2.FolderSelectDialog();
				if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					dlg.SelectedFileNames.Clear();
					dlg.SelectedFileNames.Add(fbd.FileName);
					return DialogResult.OK;
				}
				return DialogResult.Cancel;
			}
			return base.Run(parentHandle);
		}

		protected override bool AcceptInternal()
		{
			FileDialog dlg = (Control as FileDialog);
			System.Windows.Forms.FileDialog fd = (Handle as WindowsFormsNativeDialog).Handle as System.Windows.Forms.FileDialog;

			for (int i = 0; i < fd.FileNames.Length; i++)
			{
				dlg.SelectedFileNames.Add(fd.FileNames[i]);
			}
			return true;
		}

		protected override WindowsFormsNativeDialog CreateDialogInternal(Dialog dialog, List<Button> buttons)
		{
			FileDialog dlg = (dialog as FileDialog);

			System.Windows.Forms.CommonDialog fd = null;
			switch (dlg.Mode)
			{
				case FileDialogMode.Open:
				{
					System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
					fd = ofd;
					break;
				}
				case FileDialogMode.Save:
				{
					System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
					fd = sfd;
					break;
				}
				case FileDialogMode.SelectFolder:
				{
					Internal.FolderBrowserDialog.V2.FolderSelectDialog fbd = new Internal.FolderBrowserDialog.V2.FolderSelectDialog();
					fbd.Title = dlg.Text;
					fd = fbd.OpenFileDialog;
					break;
				}
			}

			if (fd is System.Windows.Forms.FileDialog)
			{
				System.Windows.Forms.FileDialog _fd = (fd as System.Windows.Forms.FileDialog);
				if (_fd is System.Windows.Forms.OpenFileDialog)
				{
					(_fd as System.Windows.Forms.OpenFileDialog).Multiselect = dlg.MultiSelect;
				}
				_fd.Title = dlg.Text;
				if (dlg.SelectedFileNames.Count > 0)
					_fd.FileName = dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1];
			}

			if (fd != null)
			{
				return new WindowsFormsNativeDialog(fd);
			}
			return null;
		}
	}
}
