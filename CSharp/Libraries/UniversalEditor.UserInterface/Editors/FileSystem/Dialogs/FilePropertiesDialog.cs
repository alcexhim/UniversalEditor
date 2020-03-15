//
//  FilePropertiesDialog.cs
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
using System.ComponentModel;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Editors.FileSystem.Dialogs
{
	public class FilePropertiesDialog : Dialog
	{
		private TextBox txtFileName = null;
		private TextBox txtFileType = null;
		private TextBox txtFileSize = null;
		private TextBox txtFileDate = null;

		public static DialogResult ShowDialog(IFileSystemObject fso)
		{
			FilePropertiesDialog dlg = new FilePropertiesDialog();
			dlg.Text = String.Format("{0} Properties", fso.Name);
			dlg.txtFileName.Text = fso.Name;

			if (fso is Folder)
			{
				Folder f = fso as Folder;
				dlg.txtFileType.Text = "Folder";
				dlg.txtFileSize.Text = String.Format("{0} files, {1} folders", f.Files.Count, f.Folders.Count);
			}
			else if (fso is File)
			{
				File f = fso as File;
				dlg.txtFileType.Text = System.IO.Path.GetExtension(f.Name) + " File";
				dlg.txtFileSize.Text = UniversalEditor.UserInterface.Common.FileInfo.FormatSize(f.Size);
			}

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				fso.Name = dlg.txtFileName.Text;
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}

		public FilePropertiesDialog()
		{
			Layout = new BoxLayout(Orientation.Vertical);
			Size = new MBS.Framework.Drawing.Dimension2D(400, 600);

			TabContainer tbs = new TabContainer();

			TabPage tabGeneral = new TabPage();
			tabGeneral.Text = "General";
			tabGeneral.Layout = new GridLayout();

			tabGeneral.Controls.Add(new Label("_Name"), new GridLayout.Constraints(0, 0));
			txtFileName = new TextBox();
			tabGeneral.Controls.Add(txtFileName, new GridLayout.Constraints(0, 1, 1, 2, ExpandMode.Horizontal));

			tabGeneral.Controls.Add(new Label("_Size"), new GridLayout.Constraints(1, 0));
			txtFileSize = new TextBox();
			txtFileSize.Editable = false;
			tabGeneral.Controls.Add(txtFileSize, new GridLayout.Constraints(1, 1, 1, 2, ExpandMode.Horizontal));

			tabGeneral.Controls.Add(new Label("_Type"), new GridLayout.Constraints(2, 0));
			txtFileType = new TextBox();
			txtFileType.Editable = false;
			tabGeneral.Controls.Add(txtFileType, new GridLayout.Constraints(2, 1, 1, 2, ExpandMode.Horizontal));

			Button cmdChangeType = new Button("C_hange...");
			tabGeneral.Controls.Add(cmdChangeType, new GridLayout.Constraints(2, 2, 1, 1));

			tabGeneral.Controls.Add(new Label("_Date modified"), new GridLayout.Constraints(3, 0));
			txtFileDate = new TextBox();
			tabGeneral.Controls.Add(txtFileDate, new GridLayout.Constraints(3, 1, 1, 2, ExpandMode.Horizontal));

			tbs.TabPages.Add(tabGeneral);

			Controls.Add(tbs, new BoxLayout.Constraints(true, true));

			Buttons.Add(new Button(StockType.OK, DialogResult.OK));
			Buttons.Add(new Button(StockType.Cancel, DialogResult.Cancel));
		}
	}
}
