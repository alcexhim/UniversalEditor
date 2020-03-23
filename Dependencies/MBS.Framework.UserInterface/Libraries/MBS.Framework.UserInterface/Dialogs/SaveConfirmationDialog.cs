//
//  SaveConfirmationDialog.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.Dialogs
{
	public class SaveConfirmationDialog : CustomDialog
	{
		public class SaveConfirmationDialogFileName
		{
			public class SaveConfirmationDialogFileNameCollection
				: System.Collections.ObjectModel.Collection<SaveConfirmationDialogFileName>
			{
				public SaveConfirmationDialogFileName Add(string filename)
				{
					SaveConfirmationDialogFileName item = new SaveConfirmationDialogFileName();
					item.FileName = filename;
					Add(item);
					return item;
				}
			}

			public string FileName { get; set; }
			public bool Selected { get; set; } = true;
		}

		private Label lblTitle = null;
		private Label lblNoSaveWarning = null;
		private ListView lv = null;
		private DefaultTreeModel tm = new DefaultTreeModel(new Type[] { typeof(bool), typeof(string) });

		public SaveConfirmationDialog()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			Container ct1 = new Container();
			ct1.Layout = new BoxLayout(Orientation.Horizontal);

			PictureFrame picIcon = new PictureFrame();
			picIcon.VerticalAlignment = VerticalAlignment.Top;
			picIcon.Image = Drawing.Image.FromStock(StockType.DialogWarning, 64);
			ct1.Controls.Add(picIcon, new BoxLayout.Constraints(false, false, 24));

			Container ct = new Container();
			ct.Layout = new BoxLayout(Orientation.Vertical);

			this.lblTitle = new Label();
			lblTitle.HorizontalAlignment = HorizontalAlignment.Left;
			ct.Controls.Add(lblTitle, new BoxLayout.Constraints(false, false, 8));

			this.lv = new ListView();
			lv.Model = tm;
			lv.Columns.Add(new ListViewColumnCheckBox(tm.Columns[0], "Save"));
			lv.Columns.Add(new ListViewColumnText(tm.Columns[1], "File name"));
			lv.HeaderStyle = ColumnHeaderStyle.None;

			ct.Controls.Add(lv, new BoxLayout.Constraints(true, true, 16));

			this.lblNoSaveWarning = new Label();
			lblNoSaveWarning.HorizontalAlignment = HorizontalAlignment.Left;
			ct.Controls.Add(lblNoSaveWarning, new BoxLayout.Constraints(false, false, 8));


			ct1.Controls.Add(ct, new BoxLayout.Constraints(true, true, 8));

			this.Controls.Add(ct1, new BoxLayout.Constraints(true, true, 8));

			Container ctButton = new Container();
			ctButton.Layout = new BoxLayout(Orientation.Horizontal);
			ctButton.Padding = new Framework.Drawing.Padding(16);

			ctButton.Controls.Add(new Label(), new BoxLayout.Constraints(false, false, 4, BoxLayout.PackType.End));
			ctButton.Controls.Add(new Button("Close _without Saving", DialogResult.No), new BoxLayout.Constraints(false, false, 4, BoxLayout.PackType.End));
			ctButton.Controls.Add(new Button(StockType.Cancel, DialogResult.Cancel), new BoxLayout.Constraints(false, false, 4, BoxLayout.PackType.End));
			// this.Buttons.Add(new Button("_Don't save"));
			ctButton.Controls.Add(new Button(StockType.Save, DialogResult.Yes), new BoxLayout.Constraints(false, false, 4, BoxLayout.PackType.End));

			this.Controls.Add(ctButton, new BoxLayout.Constraints(false, false, 16));
		}

		public string SaveChangesMultiplePrompt { get; set; }
		public static string DefaultSaveChangesMultiplePrompt { get; set; } = "There are {0} documents with unsaved changes. Save changes before closing?";

		public string SaveChangesSinglePrompt { get; set; }
		public static string DefaultSaveChangesSinglePrompt { get; set; } = "Save the changes to document \"{0}\" before closing?";

		public string WarningMessage { get; set; } = null;
		public static string DefaultWarningMessage { get; set; } = "If you don't save, all your changes will be permanently lost.";

		public string WarningMessageTimed { get; set; } = null;
		public static string DefaultWarningMessageTimed { get; set; } = "If you don't save, changes from the last {0} seconds will be permanently lost.";

		public int? SecondsSinceLastSave { get; set; } = null;
		public SaveConfirmationDialogFileName.SaveConfirmationDialogFileNameCollection FileNames { get; } = new SaveConfirmationDialogFileName.SaveConfirmationDialogFileNameCollection();

		protected internal override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			if (FileNames.Count > 1)
			{
				// lv.HeaderStyle = ColumnHeaderStyle.None;

				for (int i = 0; i < FileNames.Count; i++)
				{
					tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
					{
						new TreeModelRowColumn(tm.Columns[0], FileNames[i].Selected),
						new TreeModelRowColumn(tm.Columns[1], FileNames[i].FileName)
					}));
				}

				lv.Visible = true;

				this.lblTitle.Text = String.Format((SaveChangesMultiplePrompt == null ? DefaultSaveChangesMultiplePrompt : SaveChangesMultiplePrompt), FileNames.Count);
			}
			else if (FileNames.Count == 1)
			{
				this.lblTitle.Text = String.Format((SaveChangesSinglePrompt == null ? DefaultSaveChangesSinglePrompt : SaveChangesSinglePrompt), FileNames[0].FileName);
				lv.Visible = false;
			}

			if (SecondsSinceLastSave != null)
			{
				this.lblNoSaveWarning.Text = String.Format(WarningMessageTimed == null ? DefaultWarningMessageTimed : WarningMessageTimed, SecondsSinceLastSave.GetValueOrDefault());
			}
			else
			{
				this.lblNoSaveWarning.Text = String.Format(WarningMessage == null ? DefaultWarningMessage : WarningMessage, SecondsSinceLastSave.GetValueOrDefault());
			}
		}
	}
}
