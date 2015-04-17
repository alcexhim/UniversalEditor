using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class LicenseDialog : Dialog
	{
		private string mvarLicenseFileName = String.Empty;
		public string LicenseFileName { get { return mvarLicenseFileName; } set { mvarLicenseFileName = value; } }
	}
}
