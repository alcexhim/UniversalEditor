using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.WindowsForms.Controls
{
	public delegate void SolutionExplorerSelectionChangingEventHandler(object sender, SolutionExplorerSelectionChangingEventArgs e);
	public class SolutionExplorerSelectionChangingEventArgs : CancelEventArgs
	{
		private object mvarSelectedItem = null;
		public object SelectedItem { get { return mvarSelectedItem; } set { mvarSelectedItem = value; } }

		public SolutionExplorerSelectionChangingEventArgs(object selectedItem)
		{
			mvarSelectedItem = selectedItem;
		}
	}
}
