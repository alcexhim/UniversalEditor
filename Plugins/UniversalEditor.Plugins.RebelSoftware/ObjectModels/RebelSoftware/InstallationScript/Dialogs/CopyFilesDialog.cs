using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class CopyFilesDialog : Dialog
	{
		private Action.ActionCollection mvarActions = new Action.ActionCollection();
		public Action.ActionCollection Actions { get { return mvarActions; } }

		public override object Clone()
		{
			CopyFilesDialog clone = new CopyFilesDialog();
			foreach (Action action in mvarActions)
			{
				clone.Actions.Add(action.Clone() as Action);
			}
			return clone;
		}
	}
}
