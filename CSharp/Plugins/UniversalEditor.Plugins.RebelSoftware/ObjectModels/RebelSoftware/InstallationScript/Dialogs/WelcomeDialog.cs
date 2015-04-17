using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class WelcomeDialog : Dialog
	{
		public override object Clone()
		{
			WelcomeDialog clone = new WelcomeDialog();
			return clone;
		}
	}
}
