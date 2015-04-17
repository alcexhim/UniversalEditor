using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs;

using ISDialog = UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialog;

namespace UniversalEditor.Editors.RebelSoftware.InstallationScript
{
	public partial class InstallationScriptEditor : Editor
	{
		public InstallationScriptEditor()
		{
			InitializeComponent();
		}

		private static UserInterface.EditorReference _er = null;
		public override UserInterface.EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(InstallationScriptObjectModel));
			}
			return _er;
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tvExplorer.Nodes.Clear();

			InstallationScriptObjectModel script = (ObjectModel as InstallationScriptObjectModel);
			if (script == null) return;

			TreeNode nodeDialogs = new TreeNode("Dialogs");

			foreach (ISDialog dialog in script.Dialogs)
			{
				TreeNode tn = new TreeNode();
				if (dialog is WelcomeDialog)
				{
					tn.Text = "Welcome";
				}
				nodeDialogs.Nodes.Add(tn);
			}

			tvExplorer.Nodes.Add(nodeDialogs);

		}
	}
}
