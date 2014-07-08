using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniversalEditor.UserInterface.WindowsForms.Dialogs.PropertyList
{
	public partial class PropertyDetailsDialog : Form
	{
		public PropertyDetailsDialog()
		{
			InitializeComponent();
			cboPropertyType.SelectedIndex = 0;

            Font = SystemFonts.MenuFont;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(txtPropertyName.Text))
			{
				MessageBox.Show("Please enter a name for this property.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int dummy32 = 0;
			long dummy64 = 0;

			switch (cboPropertyType.SelectedIndex)
			{
				case 2:
				{
					break;
				}
				case 3:
				{
					if (!Int32.TryParse(txtPropertyValue.Text, out dummy32))
					{
						MessageBox.Show("The value you entered is not a valid 32-bit integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					break;
				}
				case 8:
				{
					if (!Int64.TryParse(txtPropertyValue.Text, out dummy64))
					{
						MessageBox.Show("The value you entered is not a valid 32-bit integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					break;
				}
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cboPropertyType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cboPropertyType.SelectedIndex)
			{
				case 0: // (auto-detect)
				case 1: // String
				case 2: // Binary
				case 3: // DWORD
				case 4: // Expanded String
				case 5: // Link
				case 8: // QWORD
				{
					lblPropertyValue.Visible = true;
					txtPropertyValue.Visible = true;
					txtPropertyValue.Multiline = false;
					break;
				}
				case 6: // String List
				{
					lblPropertyValue.Visible = true;
					txtPropertyValue.Visible = true;
					txtPropertyValue.Multiline = true;
					break;
				}
				case 9: // Unknown
				case 7: // None
				{
					lblPropertyValue.Visible = false;
					txtPropertyValue.Visible = false;
					break;
				}
			}
		}
	}
}
