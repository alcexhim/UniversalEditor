using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs
{
	public partial class DataFormatOptionsDialog : AwesomeControls.Dialog
	{
		public DataFormatOptionsDialog()
		{
			InitializeComponent();
		}

		private DataFormatOptionsDialogType mvarDialogType = DataFormatOptionsDialogType.Export;
		public DataFormatOptionsDialogType DialogType { get { return mvarDialogType; } set { mvarDialogType = value; } }

		private Dictionary<string, Control> CustomOptionControls = new Dictionary<string, Control>();

		private DataFormat mvarDataFormat = null;
		public DataFormat DataFormat
		{
			get { return mvarDataFormat; }
			set
			{
				mvarDataFormat = value;
				cmdAbout.Enabled = (mvarDataFormat != null);
				if (mvarDataFormat == null) return;

				this.Text = mvarDataFormat.MakeReference().Title + " Options";

				DataFormatReference dfr = mvarDataFormat.MakeReference();
				CustomOptionControls = new Dictionary<string, Control>();

				CustomOption.CustomOptionCollection coll = null;
				if (mvarDialogType == DataFormatOptionsDialogType.Export)
				{
					coll = dfr.ExportOptions;
				}
				else
				{
					coll = dfr.ImportOptions;
				}

				foreach (CustomOption eo in coll)
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
		}

		private void cmdAbout_Click(object sender, EventArgs e)
		{
			DataFormatAboutDialog dlg = new DataFormatAboutDialog();
			dlg.DataFormatReference = mvarDataFormat.MakeReference();
			dlg.ShowDialog();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			DataFormatReference dfr = mvarDataFormat.MakeReference();
			Type type = mvarDataFormat.GetType();

			CustomOption.CustomOptionCollection coll = null;
			if (mvarDialogType == DataFormatOptionsDialogType.Export)
			{
				coll = dfr.ExportOptions;
			}
			else
			{
				coll = dfr.ImportOptions;
			}

			try
			{
				foreach (CustomOption eo in coll)
				{
					System.Reflection.PropertyInfo pi = type.GetProperty(eo.PropertyName);
					if (pi == null) continue;

					Control ctl = CustomOptionControls[eo.PropertyName];
					if (ctl is NumericUpDown)
					{
						NumericUpDown txt = (ctl as NumericUpDown);
						pi.SetValue(mvarDataFormat, Convert.ChangeType(txt.Value, pi.PropertyType), null);
					}
					if (ctl is CheckBox)
					{
						CheckBox txt = (ctl as CheckBox);
						pi.SetValue(mvarDataFormat, Convert.ChangeType(txt.Checked, pi.PropertyType), null);
					}
					else if (ctl is ComboBox)
					{
						CustomOptionFieldChoice choice = ((ctl as ComboBox).SelectedItem as CustomOptionFieldChoice);
						if (choice != null)
						{
							Type[] interfaces = pi.PropertyType.GetInterfaces();
							bool convertible = false;
							foreach (Type t in interfaces)
							{
								if (t == typeof(IConvertible))
								{
									convertible = true;
									break;
								}
							}
							if (convertible)
							{
								pi.SetValue(mvarDataFormat, Convert.ChangeType(choice.Value, pi.PropertyType), null);
							}
							else
							{
								pi.SetValue(mvarDataFormat, choice.Value, null);
							}
						}
					}
					else if (ctl is TextBox)
					{
						TextBox txt = (ctl as TextBox);
						pi.SetValue(mvarDataFormat, Convert.ChangeType(txt.Text, pi.PropertyType), null);
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

		public static DialogResult ShowDialog(ref DataFormat fmt, DataFormatOptionsDialogType dialogType)
		{
			DataFormatReference dfr = fmt.MakeReference();
			if ((dialogType == DataFormatOptionsDialogType.Export && dfr.ExportOptions.Count > 0) || (dialogType == DataFormatOptionsDialogType.Import && dfr.ImportOptions.Count > 0))
			{
				DataFormatOptionsDialog dlg = new DataFormatOptionsDialog();
				dlg.DialogType = dialogType;
				dlg.DataFormat = fmt;

				if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
				{
					return DialogResult.Cancel;
				}
				return DialogResult.OK;
			}
			return DialogResult.None;
		}
	}
}
