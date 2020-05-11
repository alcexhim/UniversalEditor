//
//  FieldDefinitionPropertiesDialog.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Layouts;

namespace UniversalEditor.Editors.Binary
{
	[ContainerLayout("~/Editors/Binary/Dialogs/FieldDefinitionPropertiesDialog.glade", "GtkDialog")]
	public class FieldDefinitionPropertiesDialog2 : Dialog
	{
		private TextBox txtName;
		private NumericTextBox txtOffset;
		private ComboBox cboDataType;
		private DefaultTreeModel tmDataType;
		private Label lblLength;
		private NumericTextBox txtLength;
		private Button cmdColor;
		// private Button cmdOK;

		public FieldDefinition FieldDefinition { get; set; } = new FieldDefinition();

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			DefaultButton = Buttons[0];

			txtName.Text = FieldDefinition.Name;
			cmdColor.BackgroundBrush = new SolidBrush(FieldDefinition.Color);
			txtOffset.Value = FieldDefinition.Offset;
			txtLength.Value = FieldDefinition.Length;

			// TODO: perhaps we should put this in 
			for (int i = 0; i < tmDataType.Rows.Count; i++)
			{
				string dataTypeName = tmDataType.Rows[i].RowColumns[1].Value?.ToString();
				Type type = MBS.Framework.Reflection.FindType(dataTypeName);
				tmDataType.Rows[i].SetExtraData<Type>("type", type);

				if (type == FieldDefinition.DataType)
				{
					cboDataType.SelectedItem = tmDataType.Rows[i];
				}
			}
		}

		[EventHandler(nameof(cboDataType), "Changed")]
		private void cboDataType_Changed(object sender, EventArgs e)
		{
			if (cboDataType.SelectedItem == null) return;

			Type type = cboDataType.SelectedItem.GetExtraData<Type>("type");
			if (type == null) return;

			if (type == typeof(string) || type == typeof(byte[]))
			{
				lblLength.Visible = true;
				txtLength.Visible = true;
			}
			else
			{
				lblLength.Visible = false;
				txtLength.Visible = false;
			}
		}

		[EventHandler(nameof(cmdColor), "Click")]
		private void cmdColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.SelectedColor = (cmdColor.BackgroundBrush as SolidBrush).Color;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmdColor.BackgroundBrush = new SolidBrush(dlg.SelectedColor);
			}
		}

		// [EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{	
			FieldDefinition.Name = txtName.Text;

			if (cboDataType.SelectedItem != null)
			{
				FieldDefinition.DataType = cboDataType.SelectedItem.GetExtraData<Type>("type");
			}
			else
			{
				MessageDialog.ShowDialog("please select a data type");
				this.DialogResult = DialogResult.None;
				return;
			}

			if (FieldDefinition.DataType == typeof(string))
			{
				FieldDefinition.Length = (int)txtLength.Value;
			}
			FieldDefinition.Color = (cmdColor.BackgroundBrush as SolidBrush).Color;
			FieldDefinition.Offset = (int)txtOffset.Value;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}

	public class FieldDefinitionPropertiesDialog : Dialog
	{
		private Label lblName;
		private TextBox txtName;
		private Label lblOffset;
		private TextBox txtOffset;
		private Label lblDataType;
		private ComboBox cboDataType;
		private Label lblLength;
		private TextBox txtLength;
		private Label lblColor;
		private Button cmdColor;

		private DefaultTreeModel tmDataType = null;

		public FieldDefinition FieldDefinition = new FieldDefinition();

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			cmdColor.BackgroundBrush = new SolidBrush(Colors.LightSalmon);
			txtOffset.Text = FieldDefinition.Offset.ToString();
		}

		public FieldDefinitionPropertiesDialog()
		{
			this.Text = "Field Definition Properties";

			this.Layout = new GridLayout();

			this.lblName = new Label();
			this.lblName.Text = "_Name";
			this.Controls.Add(this.lblName, new GridLayout.Constraints(0, 0));

			this.txtName = new TextBox();
			this.Controls.Add(this.txtName, new GridLayout.Constraints(0, 1));

			this.lblOffset = new Label();
			this.lblOffset.Text = "_Offset";
			this.Controls.Add(this.lblOffset, new GridLayout.Constraints(1, 0));

			this.txtOffset = new TextBox();
			this.Controls.Add(this.txtOffset, new GridLayout.Constraints(1, 1));

			this.lblDataType = new Label();
			this.lblDataType.Text = "_Data type";
			this.Controls.Add(this.lblDataType, new GridLayout.Constraints(2, 0));

			tmDataType = new DefaultTreeModel(new Type[] { typeof(string) });

			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Signed 8-bit integer (SByte)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(sbyte));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Unsigned 8-bit integer (Byte)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(byte));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Signed 16-bit integer (Short)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(short));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Unsigned 16-bit integer (UShort)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(ushort));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Signed 32-bit integer (Int)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(int));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Unsigned 32-bit integer (UInt)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(uint));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Signed 64-bit integer (Long)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(long));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Unsigned 64-bit integer (ULong)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(long));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "32-bit floating-point (Float/Single)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(float));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "64-bit floating-point (Double)")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(double));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Fixed-length string")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(string));
			tmDataType.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(tmDataType.Columns[0], "Length-prefixed string")
			}));
			tmDataType.Rows[tmDataType.Rows.Count - 1].SetExtraData<Type>("type", typeof(string));

			this.cboDataType = new ComboBox();
			this.cboDataType.ReadOnly = true;
			this.cboDataType.Model = tmDataType;
			this.Controls.Add(this.cboDataType, new GridLayout.Constraints(2, 1));

			this.lblLength = new Label();
			this.lblLength.Text = "_Length";
			this.Controls.Add(this.lblLength, new GridLayout.Constraints(3, 0));

			this.txtLength = new TextBox();
			this.Controls.Add(this.txtLength, new GridLayout.Constraints(3, 1));

			this.lblColor = new Label();
			this.lblColor.Text = "_Color";
			this.Controls.Add(this.lblColor, new GridLayout.Constraints(4, 0));

			this.cmdColor = new Button();
			this.cmdColor.Click += cmdColor_Click;
			this.Controls.Add(this.cmdColor, new GridLayout.Constraints(4, 1));

			this.Buttons.Add(new Button(StockType.OK));
			this.Buttons[this.Buttons.Count - 1].Click += cmdOK_Click;
			this.DefaultButton = this.Buttons[this.Buttons.Count - 1];

			this.Buttons.Add(new Button(StockType.Cancel));
			this.Buttons[this.Buttons.Count - 1].ResponseValue = (int)DialogResult.Cancel;
		}

		void cmdColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.SelectedColor = (cmdColor.BackgroundBrush as SolidBrush).Color;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				cmdColor.BackgroundBrush = new SolidBrush(dlg.SelectedColor);
			}
		}


		void cmdOK_Click(object sender, EventArgs e)
		{
			FieldDefinition.Name = txtName.Text;

			int offset = 0, length = 0;
			if (!Int32.TryParse(txtOffset.Text, out offset))
			{
				MessageDialog.ShowDialog("offset must be a 32-bit integer");
				this.DialogResult = DialogResult.None;
				return;
			}

			if (cboDataType.SelectedItem != null)
			{
				FieldDefinition.DataType = cboDataType.SelectedItem.GetExtraData<Type>("type");
			}
			else
			{
				MessageDialog.ShowDialog("please select a data type");
				this.DialogResult = DialogResult.None;
				return;
			}

			if (FieldDefinition.DataType == typeof(string))
			{
				if (!Int32.TryParse(txtLength.Text, out length))
				{
					MessageDialog.ShowDialog("length must be a 32-bit integer for fixed-length string types");
					this.DialogResult = DialogResult.None;
					return;
				}
				else
				{
					FieldDefinition.Length = length;
				}
			}
			FieldDefinition.Color = (cmdColor.BackgroundBrush as SolidBrush).Color;
			FieldDefinition.Offset = offset;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
