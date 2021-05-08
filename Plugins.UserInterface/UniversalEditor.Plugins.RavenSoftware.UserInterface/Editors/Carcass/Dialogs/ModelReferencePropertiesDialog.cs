//
//  ModelReferencePropertiesDialog.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.Plugins.RavenSoftware.ObjectModels.Carcass;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Editors.Carcass.Dialogs
{
	[ContainerLayout("~/Editors/RavenSoftware/Carcass/Dialogs/ModelReferencePropertiesDialog.glade")]
	public class ModelReferencePropertiesDialog : CustomDialog
	{
		private Button cmdCancel;
		private Button cmdOK;
		private Container ct;

		private FileChooserButton fcbFilePath;

		private NumericTextBox txtStart1;
		private NumericTextBox txtFrame1;
		private NumericTextBox txtLoop1;
		private NumericTextBox txtSpeed1;
		private TextBox txtEnum1;
		private Button cmdChoose1;
		private Button cmdClear1;

		private NumericTextBox[] txtStart = new NumericTextBox[MAX_ITEMS];
		private NumericTextBox[] txtFrame = new NumericTextBox[MAX_ITEMS];
		private NumericTextBox[] txtLoop = new NumericTextBox[MAX_ITEMS];
		private NumericTextBox[] txtSpeed = new NumericTextBox[MAX_ITEMS];
		private TextBox[] txtEnum = new TextBox[MAX_ITEMS];
		private Button[] cmdChoose = new Button[MAX_ITEMS];
		private Button[] cmdClear = new Button[MAX_ITEMS];

		public string SelectedFileName { get; set; } = null;

		public ModelReference Item { get; set; } = null;

		private const int MAX_ITEMS = 18;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			fcbFilePath.RequireExistingFile = false;
			DefaultButton = cmdOK;

			txtStart[0] = txtStart1;
			txtFrame[0] = txtFrame1;
			txtLoop[0] = txtLoop1;
			txtSpeed[0] = txtSpeed1;
			txtEnum[0] = txtEnum1;
			txtEnum[0].Editable = false;

			cmdChoose[0] = cmdChoose1;
			cmdClear[0] = cmdClear1;

			for (int i = 1; i < MAX_ITEMS; i++)
			{
				Label lblAdditional = new Label();
				lblAdditional.Text = "Additional " + i.ToString();
				ct.Controls.Add(lblAdditional, new GridLayout.Constraints(i + 2, 0, 1, 1));

				txtStart[i] = new NumericTextBox();
				txtStart[i].Enabled = false;
				ct.Controls.Add(txtStart[i], new GridLayout.Constraints(i + 2, 1, 1, 1));

				txtFrame[i] = new NumericTextBox();
				txtFrame[i].Enabled = false;
				ct.Controls.Add(txtFrame[i], new GridLayout.Constraints(i + 2, 2, 1, 1));

				txtLoop[i] = new NumericTextBox();
				txtLoop[i].Enabled = false;
				ct.Controls.Add(txtLoop[i], new GridLayout.Constraints(i + 2, 3, 1, 1));

				txtSpeed[i] = new NumericTextBox();
				txtSpeed[i].Enabled = false;
				ct.Controls.Add(txtSpeed[i], new GridLayout.Constraints(i + 2, 4, 1, 1));

				txtEnum[i] = new TextBox();
				txtEnum[i].Enabled = false;
				ct.Controls.Add(txtEnum[i], new GridLayout.Constraints(i + 2, 6, 1, 1));

				cmdChoose[i] = new Button();
				cmdChoose[i].Text = "Choose";
				cmdChoose[i].Enabled = false;
				ct.Controls.Add(cmdChoose[i], new GridLayout.Constraints(i + 2, 7, 1, 1));

				cmdClear[i] = new Button();
				cmdClear[i].Text = "Clear";
				cmdClear[i].Enabled = false;
				ct.Controls.Add(cmdClear[i], new GridLayout.Constraints(i + 2, 8, 1, 1));

				if (Item != null)
				{
					fcbFilePath.SelectedFileName = Item.FileName;
					if (i - 1 < Item.AdditionalFrames.Count)
					{
						txtStart[i].Enabled = true;
						txtStart[i].Value = Item.AdditionalFrames[i - 1].Target;

						txtFrame[i].Enabled = true;
						txtFrame[i].Value = Item.AdditionalFrames[i - 1].Count;

						txtLoop[i].Enabled = true;
						txtLoop[i].Value = Item.AdditionalFrames[i - 1].Loop;

						txtSpeed[i].Enabled = true;
						txtSpeed[i].Value = Item.AdditionalFrames[i - 1].Speed;

						txtEnum[i].Enabled = true;
						txtEnum[i].Editable = false;
						txtEnum[i].Text = Item.AdditionalFrames[i - 1].Animation;

						cmdChoose[i].Enabled = true;
						cmdClear[i].Enabled = true;
					}
				}

			}
		}
	}
}
