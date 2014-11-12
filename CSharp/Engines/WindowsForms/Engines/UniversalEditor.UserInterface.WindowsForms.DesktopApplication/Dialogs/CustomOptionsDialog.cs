using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class CustomOptionDialog : AwesomeControls.Dialog
	{
		public CustomOptionDialog()
		{
			InitializeComponent();
		}

		private Dictionary<string, Control> CustomOptionControls = new Dictionary<string, Control>();

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

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			tbl.Controls.Clear();
			CustomOptionControls = new Dictionary<string, Control>();

			foreach (CustomOption eo in mvarCustomOptions)
			{
				if (!(eo is CustomOptionBoolean))
				{
					Label lbl = new Label();
					lbl.FlatStyle = FlatStyle.System;
					lbl.AutoSize = true;
					lbl.Dock = DockStyle.None;
					lbl.Anchor = AnchorStyles.Left;
					lbl.Text = eo.Title;
					tbl.Controls.Add(lbl);
				}

				if (eo is CustomOptionChoice)
				{
					CustomOptionChoice option = (eo as CustomOptionChoice);

					ComboBox cbo = new ComboBox();
					if (option.RequireChoice) cbo.DropDownStyle = ComboBoxStyle.DropDownList;
					foreach (CustomOptionFieldChoice choice in option.Choices)
					{
						cbo.Items.Add(choice);
					}
					cbo.Dock = DockStyle.Fill;

					tbl.Controls.Add(cbo);

					CustomOptionControls.Add(eo.PropertyName, cbo);
				}
				else if (eo is CustomOptionNumber)
				{
					CustomOptionNumber option = (eo as CustomOptionNumber);

					NumericUpDown txt = new NumericUpDown();
					if (option.MaximumValue.HasValue)
					{
						txt.Maximum = option.MaximumValue.Value;
					}
					else
					{
						txt.Maximum = Decimal.MaxValue;
					}
					if (option.MinimumValue.HasValue)
					{
						txt.Minimum = option.MinimumValue.Value;
					}
					else
					{
						txt.Minimum = Decimal.MinValue;
					}
					txt.Value = option.DefaultValue;
					txt.Dock = DockStyle.Fill;

					tbl.Controls.Add(txt);

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionText)
				{
					CustomOptionText option = (eo as CustomOptionText);

					TextBox txt = new TextBox();
					txt.Text = option.DefaultValue;
					txt.Dock = DockStyle.Fill;
					if (option.MaximumLength.HasValue) txt.MaxLength = option.MaximumLength.Value;

					tbl.Controls.Add(txt);

					CustomOptionControls.Add(eo.PropertyName, txt);
				}
				else if (eo is CustomOptionBoolean)
				{
					CustomOptionBoolean option = (eo as CustomOptionBoolean);

					CheckBox chk = new CheckBox();
					chk.AutoSize = true;
					chk.Anchor = AnchorStyles.Left | AnchorStyles.Right;
					chk.Text = option.Title;

					tbl.Controls.Add(chk);
					tbl.SetColumnSpan(chk, 2);

					CustomOptionControls.Add(eo.PropertyName, chk);
				}
				else if (eo is CustomOptionFile)
				{
					CustomOptionFile option = (eo as CustomOptionFile);

					AwesomeControls.FileTextBox.FileTextBoxControl cmd = new AwesomeControls.FileTextBox.FileTextBoxControl();
					cmd.Click += cmdFileBrowse_Click;
					cmd.Dock = DockStyle.Fill;
					cmd.Tag = eo;
                    switch (option.DialogMode)
                    {
                        case CustomOptionFileDialogMode.Open:
                        {
                            cmd.Mode = AwesomeControls.FileTextBox.FileTextBoxMode.Open;
                            break;
                        }
                        case CustomOptionFileDialogMode.Save:
                        {
                            cmd.Mode = AwesomeControls.FileTextBox.FileTextBoxMode.Save;
                            break;
                        }
                    }

					tbl.Controls.Add(cmd);

					CustomOptionControls.Add(eo.PropertyName, cmd);
				}
			}

			foreach (RowStyle rs in tbl.RowStyles)
			{
				rs.SizeType = SizeType.AutoSize;
			}

			Label lblSpacer = new Label();
			lblSpacer.Dock = DockStyle.Fill;
			tbl.Controls.Add(lblSpacer);
			tbl.SetColumnSpan(lblSpacer, 2);

			Font = SystemFonts.MenuFont;
		}

		private void cmdFileBrowse_Click(object sender, EventArgs e)
		{
			Button cmd = (sender as Button);
			CustomOptionFile eo = (cmd.Tag as CustomOptionFile);

			if (eo.DialogMode == CustomOptionFileDialogMode.Open)
			{
				OpenFileDialog ofd = new OpenFileDialog();
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					cmd.Text = ofd.FileName;
				}
			}
			else if (eo.DialogMode == CustomOptionFileDialogMode.Save)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					cmd.Text = sfd.FileName;
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (CustomOption eo in mvarCustomOptions)
				{
					Control ctl = CustomOptionControls[eo.PropertyName];
					if (ctl is NumericUpDown)
					{
						NumericUpDown itm = (ctl as NumericUpDown);
						(eo as CustomOptionNumber).Value = itm.Value;
					}
					if (ctl is CheckBox)
					{
						CheckBox itm = (ctl as CheckBox);
						(eo as CustomOptionBoolean).Value = itm.Checked;
					}
					else if (ctl is ComboBox)
					{
						CustomOptionFieldChoice choice = ((ctl as ComboBox).SelectedItem as CustomOptionFieldChoice);
						(eo as CustomOptionChoice).Value = choice;
					}
					else if (ctl is TextBox)
					{
						TextBox itm = (ctl as TextBox);
						(eo as CustomOptionText).Value = itm.Text;
					}
					else if (ctl is AwesomeControls.FileTextBox.FileTextBoxControl)
					{
						AwesomeControls.FileTextBox.FileTextBoxControl itm = (ctl as AwesomeControls.FileTextBox.FileTextBoxControl);
						(eo as CustomOptionFile).Value = itm.SelectedFileName;
					}
				}
			}
			catch (OverflowException ex)
			{
				MessageBox.Show("One or more of the parameters you specified is invalid.  Please ensure you have provided the correct parameters, and then try again.\r\n\r\n" + ex.Message, "Invalid Parameters Specified", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		public static DialogResult ShowDialog(ref CustomOption.CustomOptionCollection coll, string title, EventHandler aboutButtonClicked)
		{
			CustomOptionDialog dlg = new CustomOptionDialog();
			if (aboutButtonClicked != null) dlg.AboutButtonClicked += aboutButtonClicked;
			dlg.CustomOptions = coll;
			dlg.Text = title;
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
			{
				return DialogResult.Cancel;
			}
			return DialogResult.OK;
		}
	}
}
