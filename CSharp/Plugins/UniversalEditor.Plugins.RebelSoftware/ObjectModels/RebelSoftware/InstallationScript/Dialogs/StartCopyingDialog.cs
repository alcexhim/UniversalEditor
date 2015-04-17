using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class StartCopyingDialog : Dialog
	{
		public override object Clone()
		{
			StartCopyingDialog clone = new StartCopyingDialog();
			return clone;
		}
	}
}
