using System;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Layouts;
using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Expressions;
using UniversalEditor.Plugins.RavenSoftware.UserInterface.Controls.Icarus;

namespace UniversalEditor.Plugins.RavenSoftware.UserInterface.Dialogs.Icarus
{
	public partial class IcarusExpressionHelperDialog : CustomDialog
	{
		public IcarusExpressionHelperDialog()
		{
			InitializeComponent();
		}

		private IcarusCommand _Command = null;
		public IcarusCommand Command
		{
			get { return _Command; }
			set
			{
				_Command = value;

				this.Controls.Clear();
				for (int i = 0; i < _Command.Parameters.Count; i++)
				{
					IcarusExpressionEditor ed = new IcarusExpressionEditor();
					ed.Parameter = _Command.Parameters[i];
					this.Controls.Add(ed, new BoxLayout.Constraints(true, true));
				}
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < _Command.Parameters.Count; i++)
			{
				IcarusExpressionEditor ed = Controls[i] as IcarusExpressionEditor;
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
