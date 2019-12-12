//
//  CustomOptionsDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System.Collections.Generic;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.UserInterface.Dialogs
{
	public class CustomOptionsDialog : Dialog
	{
		private Dictionary<string, Control> CustomOptionControls = new Dictionary<string, Control>();

		public CustomOptionsDialog()
		{
			this.Layout = new GridLayout();

			Button cmdOK = new Button(ButtonStockType.OK);
			cmdOK.Click += cmdOK_Click;
			Buttons.Add(cmdOK);
			Buttons.Add(new Button(ButtonStockType.Cancel));

			Buttons[0].ResponseValue = (int)DialogResult.OK;
			Buttons[1].ResponseValue = (int)DialogResult.Cancel;
		}

		public event EventHandler AboutButtonClicked;

		private void cmdAbout_Click(object sender, EventArgs e)
		{
			if (AboutButtonClicked != null)
			{
				AboutButtonClicked(sender, e);
			}
		}

		private CustomOption.CustomOptionCollection mvarCustomOptions = new CustomOption.CustomOptionCollection();
		public CustomOption.CustomOptionCollection CustomOptions { get { return mvarCustomOptions; } set { mvarCustomOptions = value; } }

		protected override void OnCreating(EventArgs e)
		{
			base.OnCreating(e);

			this.Controls.Clear();

			CustomOptionControls = new Dictionary<string, Control>();

			int iRow = 0;
			foreach (CustomOption eo in mvarCustomOptions)
			{
				// do not render the CustomOption if it's supposed to be invisible
				if (!eo.Visible) continue;

				if (!(eo is CustomOptionBoolean))
				{
					Label lbl = new Label();
					// lbl.FlatStyle = FlatStyle.System;
					// lbl.AutoSize = true;
					// lbl.Dock = DockStyle.None;
					// lbl.Anchor = AnchorStyles.Left;
					lbl.UseMnemonic = true;
					lbl.Text = eo.Title; // .Replace("_", "&"); // only for WinForms
					this.Controls.Add(lbl, new GridLayout.Constraints(iRow, 0, 1, 1, ExpandMode.None));
				}

				if (eo is CustomOptionChoice)
				{
					CustomOptionChoice option = (eo as CustomOptionChoice);

					ComboBox cbo = new ComboBox();
					cbo.ReadOnly = option.RequireChoice;
					DefaultTreeModel tm = new DefaultTreeModel(new Type[] { typeof(string) });
					foreach (CustomOptionFieldChoice choice in option.Choices)
					{
						tm.Rows.Add(new TreeModelRow(new TreeModelRowColumn[] { new TreeModelRowColumn(tm.Columns[0], choice) }));
					}
					cbo.Model = tm;
					// cbo.Dock = DockStyle.Fill;

					this.Controls.Add(cbo, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, cbo);
				}
				else if (eo is CustomOptionNumber)
				{
					CustomOptionNumber option = (eo as CustomOptionNumber);

					TextBox txt = new TextBox(); // NumericUpDown txt = new NumericUpDown();
					if (option.MaximumValue.HasValue)
					{
						// txt.Maximum = option.MaximumValue.Value;
					}
					else
					{
						// txt.Maximum = Decimal.MaxValue;
					}
					if (option.MinimumValue.HasValue)
					{
						// txt.Minimum = option.MinimumValue.Value;
					}
					else
					{
						// txt.Minimum = Decimal.MinValue;
					}
					// txt.Value = option.DefaultValue;

					this.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionText)
				{
					CustomOptionText option = (eo as CustomOptionText);

					TextBox txt = new TextBox();
					txt.Text = option.DefaultValue;
					if (option.MaximumLength.HasValue) txt.MaxLength = option.MaximumLength.Value;

					this.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionBoolean)
				{
					CustomOptionBoolean option = (eo as CustomOptionBoolean);

					CheckBox chk = new CheckBox();
					chk.Text = option.Title;

					this.Controls.Add(chk, new GridLayout.Constraints(iRow, 0, 1, 2, ExpandMode.Horizontal));
					CustomOptionControls.Add(eo.PropertyName, chk);
				}
				else if (eo is CustomOptionFile)
				{
					CustomOptionFile option = (eo as CustomOptionFile);

					TextBox cmd = new TextBox();
					// AwesomeControls.FileTextBox.FileTextBoxControl cmd = new AwesomeControls.FileTextBox.FileTextBoxControl();
					cmd.Click += cmdFileBrowse_Click;
					// cmd.Dock = DockStyle.Fill;
					cmd.SetExtraData<CustomOption>("eo", eo);
					switch (option.DialogMode)
					{
						case CustomOptionFileDialogMode.Open:
						{
							// cmd.Mode = AwesomeControls.FileTextBox.FileTextBoxMode.Open;
							break;
						}
						case CustomOptionFileDialogMode.Save:
						{
							// cmd.Mode = AwesomeControls.FileTextBox.FileTextBoxMode.Save;
							break;
						}
					}

					this.Controls.Add(cmd, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, cmd);
				}

				// tbl.ColumnCount = 2;
				// tbl.RowCount = CustomOptionControls.Count;
				iRow++;
			}

			/*
			foreach (RowStyle rs in tbl.RowStyles)
			{
				rs.SizeType = SizeType.AutoSize;
			}
			*/

			// Font = SystemFonts.MenuFont;
		}

		private void cmdFileBrowse_Click(object sender, EventArgs e)
		{
			Control cmd = (sender as Control);
			CustomOptionFile eo = (cmd.GetExtraData<CustomOption>("eo") as CustomOptionFile);

			FileDialog fd = new FileDialog();

			string[] filters = eo.Filter.Split(new char[] { '|' });
			for (int i = 0; i < filters.Length; i += 2)
			{
				if (i + 1 < filters.Length)
				{
					fd.FileNameFilters.Add(filters[i], filters[i + 1]);
				}
				else
				{
					fd.FileNameFilters.Add(filters[i], filters[i]);
				}
			}

			if (eo.DialogMode == CustomOptionFileDialogMode.Open)
			{
				fd.Mode = FileDialogMode.Open;
				fd.Text = "Select File to Open";
				if (fd.ShowDialog() == DialogResult.OK)
				{
					(cmd as TextBox).Text = fd.SelectedFileNames[0];  // SelectedFileName = ofd.FileName;
				}
			}
			else if (eo.DialogMode == CustomOptionFileDialogMode.Save)
			{
				fd.Mode = FileDialogMode.Save;
				fd.Text = "Select File to Save";
				if (fd.ShowDialog() == DialogResult.OK)
				{
					(cmd as TextBox).Text = fd.SelectedFileNames[0]; // SelectedFileName = sfd.FileName;
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (CustomOption eo in mvarCustomOptions)
				{
					// Do not process invisible CustomOptions; this results in a crash
					if (!eo.Visible) continue;

					Control ctl = CustomOptionControls[eo.PropertyName];
					/*
					if (ctl is NumericUpDown)
					{
						NumericUpDown itm = (ctl as NumericUpDown);
						(eo as CustomOptionNumber).Value = itm.Value;
					}
					*/
					if (ctl is CheckBox)
					{
						CheckBox itm = (ctl as CheckBox);
						(eo as CustomOptionBoolean).Value = itm.Checked;
					}
					/*
					else if (ctl is ComboBox)
					{
						CustomOptionFieldChoice choice = ((ctl as ComboBox).SelectedItem as CustomOptionFieldChoice);
						(eo as CustomOptionChoice).Value = choice;
					}
					*/
					else if (ctl is TextBox)
					{
						TextBox itm = (ctl as TextBox);
						if (eo is CustomOptionFile)
						{
							(eo as CustomOptionFile).Value = itm.Text;
						}
						else
						{
							(eo as CustomOptionText).Value = itm.Text;
						}
					}
					/*
					else if (ctl is AwesomeControls.FileTextBox.FileTextBoxControl)
					{
						AwesomeControls.FileTextBox.FileTextBoxControl itm = (ctl as AwesomeControls.FileTextBox.FileTextBoxControl);
						(eo as CustomOptionFile).Value = itm.SelectedFileName;
					}
					*/
				}
			}
			catch (OverflowException ex)
			{
				MessageDialog.ShowDialog("One or more of the parameters you specified is invalid.  Please ensure you have provided the correct parameters, and then try again.\r\n\r\n" + ex.Message, "Invalid Parameters Specified", MessageDialogButtons.OK, MessageDialogIcon.Error);
				return;
			}

			// this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public static DialogResult ShowDialog(ref CustomOption.CustomOptionCollection coll, string title, EventHandler aboutButtonClicked)
		{
			CustomOptionsDialog dlg = new CustomOptionsDialog();
			if (aboutButtonClicked != null) dlg.AboutButtonClicked += aboutButtonClicked;
			dlg.CustomOptions = coll;
			dlg.Text = title;
			if (dlg.ShowDialog() == DialogResult.Cancel)
			{
				return DialogResult.Cancel;
			}
			return DialogResult.OK;
		}
	}
}
