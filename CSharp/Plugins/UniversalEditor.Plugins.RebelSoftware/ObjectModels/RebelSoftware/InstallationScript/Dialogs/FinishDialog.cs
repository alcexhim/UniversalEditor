using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	public class FinishDialog : Dialog
	{
		private string mvarReadmeFileName = String.Empty;
		/// <summary>
		/// The file name of the Readme file to offer upon installation completion, relative to the installation directory.
		/// </summary>
		public string ReadmeFileName { get { return mvarReadmeFileName; } set { mvarReadmeFileName = value; } }

		private bool mvarRequireReboot = false;
		/// <summary>
		/// True if a reboot is required after installation.
		/// </summary>
		public bool RequireReboot { get { return mvarRequireReboot; } set { mvarRequireReboot = value; } }

		private string mvarExecutableFileName = String.Empty;
		/// <summary>
		/// The file name of the executable file to launch upon installation completion, relative to the installation directory.
		/// </summary>
		public string ExecutableFileName { get { return mvarExecutableFileName; } set { mvarExecutableFileName = value; } }

		private string mvarExecutableWorkingDirectory = String.Empty;
		/// <summary>
		/// The file name of the executable file to launch upon installation completion, relative to the installation directory.
		/// </summary>
		public string ExecutableWorkingDirectory { get { return mvarExecutableWorkingDirectory; } set { mvarExecutableWorkingDirectory = value; } }

		public override object Clone()
		{
			FinishDialog clone = new FinishDialog();
			clone.ReadmeFileName = (mvarReadmeFileName.Clone() as string);
			clone.RequireReboot = mvarRequireReboot;
			clone.ExecutableFileName = (mvarExecutableFileName.Clone() as string);
			clone.ExecutableWorkingDirectory = (mvarExecutableWorkingDirectory.Clone() as string);
			return clone;
		}
	}
}
