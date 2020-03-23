using System;

namespace MBS.Framework.UserInterface
{
	public class CommandMenuItem : MenuItem
	{
		/// <summary>
		/// Determines whether this <see cref="CommandMenuItem" /> should allow its child menu to be torn off and displayed as a floating window.
		/// </summary>
		/// <value><c>true</c> if enable tearoff; otherwise, <c>false</c>.</value>
		public bool EnableTearoff { get; set; } = true;

		private MenuItem.MenuItemCollection mvarItems = new MenuItem.MenuItemCollection();
		public MenuItem.MenuItemCollection Items { get { return mvarItems; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private Shortcut mvarShortcut = null;
		public Shortcut Shortcut { get { return mvarShortcut; } set { mvarShortcut = value; } }

		public bool Enabled { get; set; } = true;

		public event EventHandler Click;

		public void OnClick(EventArgs e) {
			if (Click != null) {
				Click (this, e);
			}
		}

		public CommandMenuItem(string name, string text, MenuItem[] items = null, EventHandler onClick = null, Shortcut shortcut = null)
			: this(text, items, onClick, shortcut)
		{
			Name = name;
		}
		public CommandMenuItem(string text, MenuItem[] items = null, EventHandler onClick = null, Shortcut shortcut = null)
		{
			mvarText = text;
			if (items != null) {
				foreach (MenuItem item in items) {
					mvarItems.Add (item);
				}
			}
			if (onClick != null) {
				Click += onClick;
			}
			mvarShortcut = shortcut;
		}

	}
}

