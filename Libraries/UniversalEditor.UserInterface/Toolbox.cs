//
//  Toolbox.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2021 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using MBS.Framework;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Description of Toolbox.
	/// </summary>
	public sealed class Toolbox
	{
		public Toolbox()
		{
			Items = new ToolboxItem.ToolboxItemCollection(this);
		}

		public ToolboxItem.ToolboxItemCollection Items { get; private set; } = null;


		internal void ClearItems()
		{
		}
		internal void InsertItem(ToolboxItem item)
		{
		}
		internal void RemoveItem(ToolboxItem item)
		{
		}
	}
	public abstract class ToolboxItem : ISupportsExtraData
	{
		public sealed class ToolboxItemCollection
			: System.Collections.ObjectModel.Collection<ToolboxItem>
		{
			private Toolbox _toolbox = null;
			internal ToolboxItemCollection(Toolbox parent)
			{
				_toolbox = parent;
			}

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

			protected override void ClearItems()
			{
				base.ClearItems();
				_toolbox.ClearItems();
			}
			protected override void InsertItem(int index, ToolboxItem item)
			{
				base.InsertItem(index, item);
				_toolbox.InsertItem(item);
			}
			protected override void RemoveItem(int index)
			{
				_toolbox.RemoveItem(this[index]);
				base.RemoveItem(index);
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

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key) && _ExtraData[key] is T)
				return (T)_ExtraData[key];
			return defaultValue;
		}

		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData[key] = value;
		}

		public object GetExtraData(string key, object defaultValue = null)
		{
			if (_ExtraData.ContainsKey(key))
				return _ExtraData[key];
			return defaultValue;
		}

		public void SetExtraData(string key, object value)
		{
			_ExtraData[key] = value;
		}
	}
	public class ToolboxGroupItem : ToolboxItem
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public ToolboxItem.ToolboxItemCollection Items { get; internal set; }

		public ToolboxGroupItem(string name, string title) : base(name)
		{
			mvarTitle = title;
			Items = new ToolboxItemCollection(null);
			throw new NotImplementedException();
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
