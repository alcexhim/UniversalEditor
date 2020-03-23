using System;
namespace MBS.Framework.UserInterface.Controls.Docking
{
	/// <summary>
	/// A dock item is a container widget that can be docked at different place.
	/// It accepts a single child and adds a grip allowing the user to click on
	/// it to drag and drop the widget.
	/// </summary>
	public abstract class DockingItem
	{
		public class DockingItemCollection :  System.Collections.ObjectModel.Collection<DockingItem>
		{
			private IDockingItemContainer _parent = null;
			public DockingItemCollection(IDockingItemContainer parent)
			{
				_parent = parent;
			}

			/// <summary>
			/// Gets the <see cref="DockingItem"/> that contains the specified <see cref="Control" />.
			/// </summary>
			/// <param name="childControl">The <see cref="Control" /> to look for.</param>
			public DockingItem this[Control childControl]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i] is DockingWindow && (this[i] as DockingWindow).ChildControl == childControl)
						{
							return this[i];
						}
						else if (this[i] is DockingContainer)
						{
							DockingItem itmfind = (this[i] as DockingContainer).Items[childControl];
							if (itmfind != null)
								return itmfind;
						}
					}
					return null;
				}
			}

			protected override void ClearItems()
			{
				((_parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation).ClearDockingItems();
				base.ClearItems();
			}
			protected override void InsertItem(int index, DockingItem item)
			{
				if ((_parent as DockingContainerControl)?.ControlImplementation != null) ((_parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation).InsertDockingItem(item, index);
				item.Parent = _parent;
				base.InsertItem(index, item);
			}
			protected override void RemoveItem(int index)
			{
				if ((_parent as DockingContainerControl)?.ControlImplementation != null) ((_parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation).RemoveDockingItem(this[index]);
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, DockingItem item)
			{
				if ((_parent as DockingContainerControl)?.ControlImplementation != null) ((_parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation).SetDockingItem(index, item);
				this[index].Parent = null;
				item.Parent = _parent;
				base.SetItem(index, item);
			}
		}

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set
			{
				mvarName = value;
				((Parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation)?.UpdateDockingItemName(this, value);
			}
		}

		private string mvarTitle = String.Empty;
		public string Title
		{
			get { return mvarTitle; }
			set
			{
				mvarTitle = value;
				((Parent as DockingContainerControl)?.ControlImplementation as Native.IDockingContainerNativeImplementation)?.UpdateDockingItemTitle(this, value);
			}
		}

		private DockingItemBehavior mvarBehavior = DockingItemBehavior.Normal;
		public DockingItemBehavior Behavior { get { return mvarBehavior; } set { mvarBehavior = value; } }

		public IDockingItemContainer Parent { get; private set; } = null;

		private DockingItemPlacement mvarPlacement = DockingItemPlacement.Center;
		public DockingItemPlacement Placement { get { return mvarPlacement; } set { mvarPlacement = value; } }
	}
}
