using System;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface
{
	public class TabContainerSelectedTabChangedEventArgs : EventArgs
	{
		public TabPage OldTab { get; private set; } = null;
		public TabPage NewTab { get; private set; } = null;

		public TabContainerSelectedTabChangedEventArgs(TabPage oldTab, TabPage newTab)
		{
			OldTab = oldTab;
			NewTab = newTab;
		}
	}
	public class TabContainerSelectedTabChangingEventArgs : System.ComponentModel.CancelEventArgs
	{
		public TabPage OldTab { get; private set; } = null;
		public TabPage NewTab { get; set; } = null;

		public TabContainerSelectedTabChangingEventArgs(TabPage oldTab, TabPage newTab)
		{
			OldTab = oldTab;
			NewTab = newTab;
		}
	}
	public delegate void TabContainerSelectedTabChangedEventHandler(object sender, TabContainerSelectedTabChangedEventArgs e);
	public delegate void TabContainerSelectedTabChangingEventHandler(object sender, TabContainerSelectedTabChangingEventArgs e);
}
