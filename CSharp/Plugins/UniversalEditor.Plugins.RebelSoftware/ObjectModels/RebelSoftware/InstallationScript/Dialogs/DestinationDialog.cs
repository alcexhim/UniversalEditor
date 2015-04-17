using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class DestinationDialog : Dialog
	{
		private string mvarDefaultDirectory = String.Empty;
		public string DefaultDirectory {  get { return mvarDefaultDirectory; } set { mvarDefaultDirectory = value; } }

		public override object Clone()
		{
			DestinationDialog clone = new DestinationDialog();
			clone.DefaultDirectory = (mvarDefaultDirectory.Clone() as string);
			return clone;
		}
	}
}
