using System;
namespace MBS.Framework.UserInterface.Controls.Docking
{
	public class DockingContainer : DockingItem, IDockingItemContainer
	{
		public DockingItem.DockingItemCollection Items { get; private set; } = null;

		public DockingContainer()
		{
			Items = new DockingItemCollection(this);
		}
	}
}
