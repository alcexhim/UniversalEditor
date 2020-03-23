using System;
namespace MBS.Framework.UserInterface.Controls.Docking
{
	public class DockingWindow : DockingItem
	{
		private Control mvarChildControl = null;
		public Control ChildControl { get { return mvarChildControl; } set { mvarChildControl = value; mvarChildControl.SetParent((Parent as DockingContainerControl)?.Parent); } }

		public bool AutoHide { get; set; } = false;

		public DockingWindow(string title, Control child)
			: this(title, title, child)
		{
		}
		public DockingWindow(string name, string title, Control child)
		{
			Name = name;
			Title = title;
			ChildControl = child;
		}
	}
}
