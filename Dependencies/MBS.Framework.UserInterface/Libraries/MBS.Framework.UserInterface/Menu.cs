using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public class Menu : ISupportsExtraData
	{
		/// <summary>
		/// Determines whether this <see cref="Menu" /> should provide the ability to be torn off from its parent container and displayed as a floating window.
		/// </summary>
		/// <value><c>true</c> if enable tearoff; otherwise, <c>false</c>.</value>
		public bool EnableTearoff { get; set; } = true;
		public MenuItem.MenuItemCollection Items { get; private set; } = null;

		public bool Visible { get; set; } = true;

		internal Window _Parent { get; private set; } = null;

		internal void InsertMenuItem(int index, MenuItem item)
		{
			(_Parent?.ControlImplementation as Native.IWindowNativeImplementation)?.InsertMenuItem(index, item);
		}
		internal void ClearMenuItems()
		{
			(_Parent?.ControlImplementation as Native.IWindowNativeImplementation)?.ClearMenuItems();
		}
		internal void RemoveMenuItem(MenuItem item)
		{
			(_Parent?.ControlImplementation as Native.IWindowNativeImplementation)?.RemoveMenuItem(item);
		}

		private Dictionary<string, object> _ExtraData = new Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey(key))
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

		public Menu()
		{
			Items = new MenuItem.MenuItemCollection(this);
		}
		internal Menu(Window parent)
		{
			_Parent = parent;
			Items = new MenuItem.MenuItemCollection(this);
		}

		private static void MainWindow_MenuBar_Item_Click(object sender, EventArgs e)
		{
			CommandMenuItem mi = (sender as CommandMenuItem);
			if (mi == null)
				return;

			Application.ExecuteCommand(mi.Name);
		}

		public static Menu FromCommand(Command cmd)
		{
			Menu menu = new Menu();
			foreach (CommandItem ci in cmd.Items)
			{
				MenuItem[] mi = MenuItem.LoadMenuItem(ci, MainWindow_MenuBar_Item_Click);
				if (mi == null || mi.Length == 0)
					continue;

				for (int i = 0; i < mi.Length; i++)
					menu.Items.Add(mi[i]);
			}
			return menu;
		}
	}
}

