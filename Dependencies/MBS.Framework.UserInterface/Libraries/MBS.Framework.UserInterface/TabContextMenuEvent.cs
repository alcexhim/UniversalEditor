using System;
using System.ComponentModel;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface
{
	public class BeforeTabContextMenuEventArgs : CancelEventArgs
	{
		public TabContainer TabContainer { get; private set; } = null;
		public TabPage TabPage { get; private set; } = null;

		public Menu ContextMenu { get; set; } = null;
		public string ContextMenuCommandID { get; set; } = null;

		public BeforeTabContextMenuEventArgs(TabContainer tabContainer, TabPage tabPage)
		{
			TabContainer = tabContainer;
			TabPage = tabPage;
		}
	}
	public delegate void BeforeTabContextMenuEventHandler(object sender, BeforeTabContextMenuEventArgs e);
}
