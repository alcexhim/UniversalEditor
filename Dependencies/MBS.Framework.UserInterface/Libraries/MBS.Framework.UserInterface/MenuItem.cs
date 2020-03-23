using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public abstract class MenuItem
	{
		public class MenuItemCollection
			: System.Collections.ObjectModel.Collection<MenuItem>
		{
			public void AddRange (MenuItem[] menuItems)
			{
				foreach (MenuItem mi in menuItems) {
					Add (mi);
				}
			}

			private Dictionary<string, MenuItem> _itemsByName = new Dictionary<string, MenuItem>();

			private Menu _parent = null;
			public MenuItemCollection(Menu parent = null)
			{
				_parent = parent;
			}

			public MenuItem this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}

			protected override void InsertItem(int index, MenuItem item)
			{
				base.InsertItem(index, item);
				if (item != null)
				{
					_itemsByName[item.Name] = item;
					item.Parent = _parent;
					_parent?.InsertMenuItem(index, item);
				}
			}
			protected override void ClearItems()
			{
				foreach (MenuItem item in this)
				{
					item.Parent = null;
				}
				_itemsByName.Clear();
				base.ClearItems();
				_parent?.ClearMenuItems();
			}
			protected override void RemoveItem(int index)
			{
				_itemsByName.Remove(this[index].Name);
				this[index].Parent = null;
				_parent?.RemoveMenuItem(this[index]);
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, MenuItem item)
			{
				base.SetItem(index, item);
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public Menu Parent { get; private set; } = null;

		private object mvarData = null;
		/// <summary>
		/// Extra data to attach to this <see cref="MenuItem" />.
		/// </summary>
		/// <value>The data.</value>
		public object Data { get { return mvarData; } set { mvarData = value; } }

		private MenuItemHorizontalAlignment mvarHorizontalAlignment = MenuItemHorizontalAlignment.Left;
		public MenuItemHorizontalAlignment HorizontalAlignment { get { return mvarHorizontalAlignment; } set { mvarHorizontalAlignment = value; } }

		private bool _Visible = true;
		public bool Visible { get { return _Visible; } set { _Visible = value; Application.Engine.SetMenuItemVisibility(this, value); } }

		public static MenuItem[] LoadMenuItem(CommandItem ci, EventHandler onclick = null)
		{
			System.Diagnostics.Contracts.Contract.Assert(ci != null);

			if (ci is CommandReferenceCommandItem)
			{
				CommandReferenceCommandItem crci = (ci as CommandReferenceCommandItem);

				Command cmd = Application.Commands[crci.CommandID];
				if (cmd != null)
				{
					CommandMenuItem mi = new CommandMenuItem(cmd.Title);
					mi.Name = cmd.ID;
					mi.Shortcut = cmd.Shortcut;
					if (cmd.Items.Count > 0)
					{
						foreach (CommandItem ci1 in cmd.Items)
						{
							MBS.Framework.UserInterface.MenuItem[] mi1 = LoadMenuItem(ci1, onclick);
							if (mi1 == null) continue;

							for (int i = 0; i < mi1.Length; i++)
								mi.Items.Add(mi1[i]);
						}
					}
					else
					{
						if (onclick != null)
							mi.Click += onclick;
					}
					return new MenuItem[] { mi };
				}
				else
				{
					Console.WriteLine("attempted to load unknown cmd '" + crci.CommandID + "'");
				}
				return null;
			}
			else if (ci is SeparatorCommandItem)
			{
				return new MenuItem[] { new MBS.Framework.UserInterface.SeparatorMenuItem() };
			}
			else if (ci is CommandPlaceholderCommandItem)
			{

			}
			else if (ci is GroupCommandItem)
			{
				GroupCommandItem gci = (ci as GroupCommandItem);
				List<MenuItem> list = new List<MenuItem>();
				for (int i = 0; i < gci.Items.Count; i++)
				{
					MenuItem[] mis = LoadMenuItem(gci.Items[i], onclick);
					list.AddRange(mis);
				}
				return list.ToArray();
			}
			else
			{
				if (System.Diagnostics.Debugger.IsAttached)
				{
					throw new NotImplementedException(String.Format("CommandItem type '{0}' not implemented", ci.GetType().FullName));
				}
			}
			return null;
		}
	}
}

