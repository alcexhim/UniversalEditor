/*
 * Created by SharpDevelop.
 * User: Mike Becker
 * Date: 8/18/2013
 * Time: 6:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Description of Toolbox.
	/// </summary>
	public sealed class Toolbox
	{
		private ToolboxItem.ToolboxItemCollection mvarItems = new ToolboxItem.ToolboxItemCollection();
		public ToolboxItem.ToolboxItemCollection Items { get { return mvarItems; } }
	}
	public abstract class ToolboxItem
	{
		public sealed class ToolboxItemCollection
			: System.Collections.ObjectModel.Collection<ToolboxItem>
		{
			public ToolboxCommandItem AddCommand(string name)
			{
				return AddCommand(name, name);
			}
			public ToolboxCommandItem AddCommand(string name, string title)
			{
				return AddCommand(name, title, null);
			}
			public ToolboxCommandItem AddCommand(string name, string title, string imageFileName)
			{
				ToolboxCommandItem item = new ToolboxCommandItem(name, title, imageFileName);
				Add(item);
				return item;
			}

			public ToolboxGroupItem AddGroup(string name)
			{
				return AddGroup(name, name);
			}
			public ToolboxGroupItem AddGroup(string name, string title)
			{
				ToolboxGroupItem group = new ToolboxGroupItem(name, title);
				Add(group);
				return group;
			}
		}
		
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private ToolboxGroupItem mvarParent = null;
		public ToolboxGroupItem Parent { get { return mvarParent; } set { mvarParent = value; } }
		
		public ToolboxItem(string name)
		{
			mvarName = name;
		}
	}
	public class ToolboxGroupItem : ToolboxItem
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private ToolboxItem.ToolboxItemCollection mvarItems = new ToolboxItem.ToolboxItemCollection();
		public ToolboxItem.ToolboxItemCollection Items { get { return mvarItems; } }
		
		public ToolboxGroupItem(string name, string title) : base(name)
		{
			mvarTitle = title;
		}
	}
	public class ToolboxCommandItem : ToolboxItem
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
		
		private string mvarImageFileName = String.Empty;
		public string ImageFileName { get { return mvarImageFileName; } set { mvarImageFileName = value; } }
		
		public ToolboxCommandItem(string name, string title, string imageFileName = "") : base(name)
		{
			mvarTitle = title;
			mvarImageFileName = imageFileName;
		}
	}
	
	public delegate void ToolboxItemEventHandler(object sender, ToolboxItemEventArgs e);
	/// <summary>
	/// Contains the toolbox item that is responsible for the toolbox item event.
	/// </summary>
	public class ToolboxItemEventArgs : CancelEventArgs
	{
		private ToolboxItem mvarItem = null;
		public ToolboxItem Item { get { return mvarItem; } }
		
		public ToolboxItemEventArgs(ToolboxItem item)
		{
			mvarItem = item;
		}
	}
}
