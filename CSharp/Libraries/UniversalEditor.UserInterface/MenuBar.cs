using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public class MenuBar
	{
        private MenuItem.MenuItemCollection mvarItems = new MenuItem.MenuItemCollection();
        public MenuItem.MenuItemCollection Items { get { return mvarItems; } }
    }

    public abstract class MenuItem
    {
        public class MenuItemCollection
            : System.Collections.ObjectModel.Collection<MenuItem>
        {
            private System.Collections.Generic.Dictionary<string, MenuItem> itemsByName = new Dictionary<string, MenuItem>();

            public ActionMenuItem Add(string name, string title)
            {
                return Add(name, title, null);
            }
            public ActionMenuItem Add(string name, string title, EventHandler onClick)
            {
                return Add(name, title, onClick, Count);
            }
            public ActionMenuItem Add(string name, string title, int position)
            {
                return Add(name, title, null, position);
            }
            public ActionMenuItem Add(string name, string title, EventHandler onClick, int position)
            {
                ActionMenuItem item = new ActionMenuItem(name, title);
                if (onClick != null)
                {
                    item.Click += onClick;
                }
                item.Position = position;
                Add(item);
                return item;
            }
            public SeparatorMenuItem AddSeparator()
            {
                return AddSeparator(Count);
            }
            public SeparatorMenuItem AddSeparator(int position)
            {
                SeparatorMenuItem item = new SeparatorMenuItem();
                item.Position = position;
                item.Name = "separator__" + (Count + 1).ToString();
                Add(item);
                return item;
            }

            public MenuItem this[string name]
            {
                get
                {
                    return itemsByName[name];
                }
            }

            public bool Contains(string name)
            {
                return itemsByName.ContainsKey(name);
            }
            public bool Remove(string name)
            {
                if (itemsByName.ContainsKey(name))
                {
                    base.Remove(itemsByName[name]);
                    return true;
                }
                return false;
            }

            protected override void InsertItem(int index, MenuItem item)
            {
                item.mvarParent = this;
                base.InsertItem(index, item);
                itemsByName.Add(item.Name, item);
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarParent = null;
                itemsByName.Remove(this[index].Name);
                base.RemoveItem(index);
            }

            internal void UpdateName(MenuItem item, string oldName)
            {
                itemsByName.Remove(oldName);
                itemsByName.Add(item.Name, item);
            }
        }

        private List<object> mvarNativeControls = new List<object>();
        public List<object> NativeControls { get { return mvarNativeControls; } }

        private MenuItemCollection mvarParent = null;

        private string mvarName = String.Empty;
        public string Name
        {
            get { return mvarName; }
            set
            {
                string oldName = mvarName; mvarName = value;
                if (mvarParent != null)
                {
                    mvarParent.UpdateName(this, oldName);
                }
            }
        }

        private int mvarPosition = -1;
        public int Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private bool mvarEnabled = true;
        public bool Enabled
        {
            get { return mvarEnabled; }
            set
            {
                mvarEnabled = value;
                foreach (object NativeControl in mvarNativeControls)
                {
                    HostApplication.CurrentWindow.RefreshCommand(NativeControl);
                }
            }
        }

        private bool mvarVisible = true;
        public bool Visible { get { return mvarVisible; }
            set
            {
                mvarVisible = value;
                foreach (object NativeControl in mvarNativeControls)
                {
                    HostApplication.CurrentWindow.RefreshCommand(NativeControl);
                }
            }
        }
    }
    public class ActionMenuItem : MenuItem
    {
        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private MenuItem.MenuItemCollection mvarItems = new MenuItem.MenuItemCollection();
        public MenuItem.MenuItemCollection Items { get { return mvarItems; } }

        private CommandDisplayStyle mvarDisplayStyle = CommandDisplayStyle.ImageOnly;
        public CommandDisplayStyle DisplayStyle { get { return mvarDisplayStyle; } set { mvarDisplayStyle = value; } }

        public event EventHandler Click;

        public ActionMenuItem(string title)
        {
            base.Name = title;
            mvarTitle = title;
        }
        public ActionMenuItem(string name, string title)
        {
            base.Name = name;
            mvarTitle = title;
        }


        public void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
    }
    public class SeparatorMenuItem : MenuItem
    {

    }
}
