using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UniversalEditor.UserInterface.WindowsForms;

using UniversalEditor.Plugins.RebelSoftware.ObjectModels.InstallationScript;
using UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs;

namespace UniversalEditor.Editors.RebelSoftware.InstallationScript
{
	public partial class InstallationScriptEditor : Editor
	{
		public InstallationScriptEditor()
		{
			InitializeComponent();
		}

		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			tvExplorer.Nodes.Clear();

			InstallationScriptObjectModel script = (ObjectModel as InstallationScriptObjectModel);
			if (script == null) return;

			TreeNode nodeDialogs = new TreeNode("Dialogs");

			foreach (Dialog dialog in script.Dialogs)
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
