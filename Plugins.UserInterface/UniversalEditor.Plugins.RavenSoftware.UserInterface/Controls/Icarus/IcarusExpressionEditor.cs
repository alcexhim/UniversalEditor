//
//  IcarusExpressionEditor.cs - provides a UWT-based Container with controls for editing an expression in an Icarus script
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework.UserInterface.Dialogs;

using UniversalEditor.ObjectModels.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Controls.Icarus
{
	/// <summary>
	/// Provides a UWT-based <see cref="Container" /> with controls for editing an expression in an Icarus script.
	/// </summary>
	[ContainerLayout("~/Editors/RavenSoftware/Icarus/Controls/ExpressionEditor.glade")]
	public class IcarusExpressionEditor : Container
	{
		// filled in by uwt container layout loader
		private Label lblParameterName;
		internal TextBox txtParameterValue;
		private Button cmdGET;
		private Button cmdRND;
		private Button cmdTAG;
		internal ComboBox cboExpressionType;
		private ComboBox cboGETType;
		private ComboBox cboGETName;
		private ComboBox cboTAGType;
		private NumericTextBox txtRangeStart;
		private Label lblRange;
		private NumericTextBox txtRangeEnd;
		private Button cmdBrowse;
		private Container ctFileChooser = null;
		private Button cmdExecute;
		private DefaultTreeModel lsGETName;

		private IcarusParameter _Parameter = null;
		public IcarusParameter Parameter
		{
			get { return _Parameter; }
			set
			{
				_Parameter = value;
				UpdateControls();
			}
		}

		private void UpdateControls()
		{
			if (lblParameterName == null) return;

			lblParameterName.Text = _Parameter.Name;

			string pval = _Parameter.Value?.ToString();
			if (!String.IsNullOrEmpty(pval))
			{
				if (pval.StartsWith("\"") && pval.EndsWith("\""))
					pval = pval.Substring(1, pval.Length - 2);

				txtParameterValue.Text = pval;
			}
			txtParameterValue.Editable = !_Parameter.ReadOnly;

			switch (cboExpressionType.Text)
			{
				case "Constant":
				{
					cboGETType.Visible = false;
					cboGETName.Visible = false;
					cmdGET.Visible = false;

					cmdTAG.Visible = false;
					cboTAGType.Visible = false;

					cmdRND.Visible = false;
					txtRangeStart.Visible = false;
					lblRange.Visible = false;
					txtRangeEnd.Visible = false;

					cmdBrowse.Visible = true;
					cmdExecute.Visible = true;
					break;
				}
				case "Expression":
				{
					cboGETType.Visible = true;
					cboGETName.Visible = true;
					cmdGET.Visible = true;

					cmdTAG.Visible = true;
					cboTAGType.Visible = true;

					cmdRND.Visible = true;
					txtRangeStart.Visible = true;
					lblRange.Visible = true;
					txtRangeEnd.Visible = true;

					cmdBrowse.Visible = false;
					cmdExecute.Visible = false;
					break;
				}
			}
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			cmdGET.Click += cmdGET_Click;
			cmdTAG.Click += cmdTAG_Click;
			cmdRND.Click += cmdRND_Click;
			cboExpressionType.Changed += cboExpressionType_Changed;
			cboExpressionType.SelectedItem = (cboExpressionType.Model as DefaultTreeModel).Rows[0];
			cmdBrowse.Click += cmdBrowse_Click;
			cmdExecute.Click += cmdExecute_Click;

			lsGETName.Rows.Clear();
			lsGETName.Rows.Add(new TreeModelRow(new TreeModelRowColumn[]
			{
				new TreeModelRowColumn(lsGETName.Columns[0], "tryb debuggingu")
			}));
			cboGETName.Model = lsGETName;

			UpdateControls();
		}

		private void cmdExecute_Click(object sender, EventArgs e)
		{
			if (System.IO.File.Exists(txtParameterValue.Text))
			{
				System.Diagnostics.Process.Start(txtParameterValue.Text);
			}
			else
			{
				MessageDialog.ShowDialog(String.Format("Unable to execute file for preview!\r\n\r\n\t{0}", txtParameterValue.Text), "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
			}
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			FileDialog dlg = new FileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				txtParameterValue.Text = dlg.SelectedFileNames[dlg.SelectedFileNames.Count - 1];
			}
		}


		void cboExpressionType_Changed(object sender, EventArgs e)
		{
			UpdateControls();
		}

		private void cmdTAG_Click(object sender, EventArgs e)
		{
			string tagType = cboTAGType.Text?.ToUpper();
			txtParameterValue.Text = String.Format("tag( \"targetname\", {0})", tagType);
		}

		private void cmdGET_Click(object sender, EventArgs e)
		{
			string getType = cboGETType.Text?.ToUpper();
			string getName = cboGETName.Text?.ToUpper();
			txtParameterValue.Text = String.Format("get( {0}, \"{1}\")", getType, getName);
		}

		private void cmdRND_Click(object sender, EventArgs e)
		{
			txtParameterValue.Text = String.Format("random( {0}, {1} )", txtRangeStart.Value.ToString(), txtRangeEnd.Value.ToString());
		}

	}
}
