using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Setup.ArkAngles.Actions
{
	public class PromptAction
	{
		private int mvarOrder = 0;
		/// <summary>
		/// 9 = before install directory, 11 = after install directory
		/// </summary>
		public int Order { get { return mvarOrder; } set { mvarOrder = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
