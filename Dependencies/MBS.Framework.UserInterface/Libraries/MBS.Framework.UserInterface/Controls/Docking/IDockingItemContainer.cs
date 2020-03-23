using System;
namespace MBS.Framework.UserInterface.Controls.Docking
{
	public interface IDockingItemContainer
	{
		DockingItem.DockingItemCollection Items { get; }
	}
}
