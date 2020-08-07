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
	public class CustomOptionsDialog : CustomDialog
	{
		private Dictionary<string, Control> CustomOptionControls = new Dictionary<string, Control>();

		public CustomOptionsDialog()
		{
			this.Layout = new GridLayout();

			Button cmdOK = new Button(StockType.OK);
			cmdOK.Click += cmdOK_Click;
			Buttons.Add(cmdOK);
			Buttons.Add(new Button(StockType.Cancel));

			// Buttons[0].ResponseValue = (int)DialogResult.OK;
			DefaultButton = Buttons[0];
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
					lbl.HorizontalAlignment = HorizontalAlignment.Left;
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
						TreeModelRow row = new TreeModelRow(new TreeModelRowColumn[] { new TreeModelRowColumn(tm.Columns[0], choice) });
						row.SetExtraData<CustomOptionFieldChoice>("choice", choice);
						tm.Rows.Add(row);
					}
					cbo.Model = tm;
					// cbo.Dock = DockStyle.Fill;

					this.Controls.Add(cbo, new GridLayout.Constraints(iRow, 1, 1, 2, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, cbo);
				}
				else if (eo is CustomOptionNumber)
				{
					CustomOptionNumber option = (eo as CustomOptionNumber);

					// NumericUpDown in WinForms used decimal, UWT uses double... what's the difference? is it bad?
					NumericTextBox txt = new NumericTextBox(); // NumericUpDown txt = new NumericUpDown();
					if (option.MaximumValue.HasValue)
					{
						txt.Maximum = (double)option.MaximumValue.Value;
					}
					else
					{
						txt.Maximum = Double.MaxValue;
					}
					if (option.MinimumValue.HasValue)
					{
						txt.Minimum = (double)option.MinimumValue.Value;
					}
					else
					{
						txt.Minimum = Double.MinValue;
					}
					txt.Value = (double)option.DefaultValue;

					this.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 2, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionText)
				{
					CustomOptionText option = (eo as CustomOptionText);

					TextBox txt = new TextBox();
					txt.Text = option.DefaultValue;
					if (option.MaximumLength.HasValue) txt.MaxLength = option.MaximumLength.Value;

					this.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 2, ExpandMode.Horizontal));

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionBoolean)
				{
					CustomOptionBoolean option = (eo as CustomOptionBoolean);

					CheckBox chk = new CheckBox();
					chk.Checked = option.DefaultValue;
					chk.Text = option.Title;

					this.Controls.Add(chk, new GridLayout.Constraints(iRow, 0, 1, 3, ExpandMode.Horizontal));
					CustomOptionControls.Add(eo.PropertyName, chk);
				}
				else if (eo is CustomOptionFile)
				{
					CustomOptionFile option = (eo as CustomOptionFile);

					Button cmd = new Button();
					cmd.Text = "_Browse...";
					cmd.Click += cmdFileBrowse_Click;
					cmd.SetExtraData<CustomOption>("eo", eo);

					TextBox txt = new TextBox();
					cmd.SetExtraData<TextBox>("txt", txt);

					// AwesomeControls.FileTextBox.FileTextBoxControl cmd = new AwesomeControls.FileTextBox.FileTextBoxControl();
					txt.Click += cmdFileBrowse_Click;
					// cmd.Dock = DockStyle.Fill;
					txt.SetExtraData<CustomOption>("eo", eo);
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

					this.Controls.Add(txt, new GridLayout.Constraints(iRow, 1, 1, 1, ExpandMode.Horizontal));
					this.Controls.Add(cmd, new GridLayout.Constraints(iRow, 2, 1, 1, ExpandMode.None));

					CustomOptionControls.Add(eo.PropertyName, txt);
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
					if (cmd is TextBox)
					{
						(cmd as TextBox).Text = fd.SelectedFileNames[0];
					}
					else
					{
						cmd.GetExtraData<TextBox>("txt").Text = fd.SelectedFileNames[0];
					}
				}
			}
			else if (eo.DialogMode == CustomOptionFileDialogMode.Save)
			{
				fd.Mode = FileDialogMode.Save;
				fd.Text = "Select File to Save";
				if (fd.ShowDialog() == DialogResult.OK)
				{
					if (cmd is TextBox)
					{
						(cmd as TextBox).Text = fd.SelectedFileNames[0];
					}
					else
					{
 						cmd.GetExtraData<TextBox>("txt").Text = fd.SelectedFileNames[0];
					}
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

					if (!CustomOptionControls.ContainsKey(eo.PropertyName))
					{
						continue;
					}
					Control ctl = CustomOptionControls[eo.PropertyName];

					if (ctl is NumericTextBox)
					{
						NumericTextBox itm = (ctl as NumericTextBox);
						(eo as CustomOptionNumber).Value = (decimal)itm.Value;
					}
					if (ctl is CheckBox)
					{
						CheckBox itm = (ctl as CheckBox);
						(eo as CustomOptionBoolean).Value = itm.Checked;
					}
					else if (ctl is ComboBox)
					{
						CustomOptionFieldChoice choice = (ctl as ComboBox).SelectedItem.GetExtraData<CustomOptionFieldChoice>("choice");
						(eo as CustomOptionChoice).Value = choice;
					}
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

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public static DialogResult ShowDialog(ref CustomOption.CustomOptionCollection coll, string title, EventHandler aboutButtonClicked)
		{
			CustomOptionsDialog dlg = new CustomOptionsDialog();
			if (aboutButtonClicked != null) dlg.AboutButtonClicked += aboutButtonClicked;
			dlg.CustomOptions = coll;
			dlg.Text = title;

			DialogResult result = dlg.ShowDialog();

			// FIXME: (in UWT) on GTK, closing dialog with 'esc' key results in DialogResult.None, NOT DialogResult.Cancel as on Windows!
			if (result == DialogResult.Cancel || result == DialogResult.None)
			{
				return DialogResult.Cancel;
			}
			return DialogResult.OK;
		}
	}
}
