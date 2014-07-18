using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public class Toolbar
	{
		public class ToolbarCollection
			: System.Collections.ObjectModel.Collection<Toolbar>
		{
			public Toolbar Add(string name)
			{
				return Add(name, name);
			}
			public Toolbar Add(string name, string title)
			{
				Toolbar tb = new Toolbar();
				tb.Name = name;
				tb.Title = title;
				Add(tb);
				return tb;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private MenuItem.MenuItemCollection mvarItems = new MenuItem.MenuItemCollection();
		public MenuItem.MenuItemCollection Items { get { return mvarItems; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }
	}
}
