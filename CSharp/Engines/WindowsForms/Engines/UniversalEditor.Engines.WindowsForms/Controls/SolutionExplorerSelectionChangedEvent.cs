using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor.Engines.WindowsForms.Controls
{
	public delegate void SolutionExplorerSelectionChangedEventHandler(object sender, SolutionExplorerSelectionChangedEventArgs e);
	public class SolutionExplorerSelectionChangedEventArgs : EventArgs
	{
		private object mvarSelectedItem = null;
		public object SelectedItem { get { return mvarSelectedItem; } set { mvarSelectedItem = value; } }

		public SolutionExplorerSelectionChangedEventArgs(object selectedItem)
		{
			mvarSelectedItem = selectedItem;
		}
	}
}
