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
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Dialogs;
using UniversalWidgetToolkit.Drawing;
using UniversalWidgetToolkit.Layouts;

namespace UniversalEditor.Editors.Binary
{
	public class FieldDefinitionPropertiesDialog : Dialog
	{
		private Label lblName;
		internal TextBox txtName;
		private Label lblOffset;
		internal TextBox txtOffset;
		private Label lblLength;
		internal TextBox txtLength;
		private Label lblColor;
		internal Button cmdColor;

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

			this.lblLength = new Label();
			this.lblLength.Text = "_Length";
			this.Controls.Add(this.lblLength, new GridLayout.Constraints(2, 0));

			this.txtLength = new TextBox();
			this.Controls.Add(this.txtLength, new GridLayout.Constraints(2, 1));

			this.lblColor = new Label();
			this.lblColor.Text = "_Color";
			this.Controls.Add(this.lblColor, new GridLayout.Constraints(3, 0));

			this.cmdColor = new Button();
			this.Controls.Add(this.cmdColor, new GridLayout.Constraints(3, 1));

			this.Buttons.Add(new Button(ButtonStockType.OK));
			this.Buttons[this.Buttons.Count - 1].Click += cmdOK_Click;
			this.Buttons[this.Buttons.Count - 1].ResponseValue = (int)DialogResult.OK;

			this.Buttons.Add(new Button(ButtonStockType.Cancel));
			this.Buttons[this.Buttons.Count - 1].ResponseValue = (int)DialogResult.Cancel;
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
			if (!Int32.TryParse(txtLength.Text, out length))
			{
				MessageDialog.ShowDialog("length must be a 32-bit integer");
				this.DialogResult = DialogResult.None;
				return;
			}
			FieldDefinition.Offset = offset;
			FieldDefinition.Length = length;
		}

	}
}
