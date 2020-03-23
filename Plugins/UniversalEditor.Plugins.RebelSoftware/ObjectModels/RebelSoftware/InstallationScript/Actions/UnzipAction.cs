using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Actions
{
	public class UnzipAction : Action
	{
		private string mvarFileName = String.Empty;
		/// <summary>
		/// The name of the archive file to unzip using ZAP format.
		/// </summary>
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		public override object Clone()
		{
			UnzipAction clone = new UnzipAction();
			clone.FileName = (mvarFileName.Clone() as string);
			return clone;
		}
	}
}
