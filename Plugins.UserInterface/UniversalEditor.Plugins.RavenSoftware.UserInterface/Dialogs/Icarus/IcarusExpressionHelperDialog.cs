//
//  IcarusExpressionHelperDialog.cs - provides a UWT-based CustomDialog with controls for editing an expression in an Icarus script
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
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.Plugins.RavenSoftware.UserInterface.Controls.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Dialogs.Icarus
{
	/// <summary>
	/// Provides a UWT-based <see cref="CustomDialog" /> with controls for editing an expression in an Icarus script.
	/// </summary>
	[ContainerLayout("~/Editors/RavenSoftware/Icarus/Dialogs/IcarusExpressionHelperDialog.glade")]
	public partial class IcarusExpressionHelperDialog : CustomDialog
	{
		private Button cmdOK;
		private Button cmdCancel;

		private Container ct;

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);

			DefaultButton = cmdOK;
			UpdateCommandParameters();
		}

		private void UpdateCommandParameters()
		{
			if (!IsCreated) return;

			ct.Controls.Clear();
			for (int i = 0; i < _Command.Parameters.Count; i++)
			{
				IcarusExpressionEditor ed = new IcarusExpressionEditor();
				ed.Parameter = _Command.Parameters[i];
				ct.Controls.Add(ed, new BoxLayout.Constraints(true, true));
			}
		}

		private IcarusCommand _Command = null;
		public IcarusCommand Command
		{
			get { return _Command; }
			set
			{
				_Command = value;
				UpdateCommandParameters();
			}
		}

		[EventHandler(nameof(cmdOK), "Click")]
		private void cmdOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < _Command.Parameters.Count; i++)
			{
				IcarusExpressionEditor ed = ct.Controls[i] as IcarusExpressionEditor;
				if (ed.cboExpressionType.SelectedItem == (ed.cboExpressionType.Model as DefaultTreeModel).Rows[0])
				{
					// constant
					_Command.Parameters[i].Value = new IcarusConstantExpression(ed.txtParameterValue.Text);
				}
				else
				{
					// expression
					if (ed.txtParameterValue.Text.StartsWith("get("))
					{
						_Command.Parameters[i].Value = new IcarusGetExpression(IcarusVariableDataType.String, ed.txtParameterValue.Text.Substring(4, ed.txtParameterValue.Text.Length - 5));
					}
					else if (ed.txtParameterValue.Text.StartsWith("tag("))
					{
						_Command.Parameters[i].Value = new IcarusTagExpression(ed.txtParameterValue.Text, IcarusTagType.Origin);
					}
				}
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
