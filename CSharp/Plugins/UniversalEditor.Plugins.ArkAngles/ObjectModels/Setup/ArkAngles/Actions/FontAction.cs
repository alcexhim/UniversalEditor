using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles.Actions
{
	/// <summary>
	/// Installs a font to the user's computer.
	/// </summary>
	public class FontAction : Action
	{

		private string mvarFileName = String.Empty;
		/// <summary>
		/// The file name of the font to install.
		/// </summary>
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the font to install.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override object Clone()
		{
			FontAction clone = new FontAction();
			clone.FileName = (mvarFileName.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
