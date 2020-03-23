using System;
using MBS.Framework.UserInterface.Controls.Docking;
using System.Collections.Generic;
using MBS.Framework.UserInterface.Controls.Docking.Native;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	// [ControlImplementation(typeof(DockingContainerControl))]
	public class DockingContainerImplementation : GTKNativeImplementation, IDockingContainerNativeImplementation
	{
		public DockingContainerImplementation(Engine engine, DockingContainerControl control)
			: base(engine, control)
		{
			DockingItem_Selected_Handler = new Internal.GObject.Delegates.GCallback (DockingItem_Selected);
			DockingItem_MoveFocusChild_Handler = new Internal.GDL.Delegates.GdlMoveFocusChildCallback (DockingItem_MoveFocusChild);
		}

		public void UpdateDockingItemName(DockingItem item, string title)
		{
			if (!_HandlesForDockingItem.ContainsKey(item))
				return;

			IntPtr handle = _HandlesForDockingItem[item];
			Internal.GDL.Methods.gdl_dock_object_set_name(handle, title);
		}
		public void UpdateDockingItemTitle(DockingItem item, string title)
		{
			if (!_HandlesForDockingItem.ContainsKey(item))
				return;

			IntPtr handle = _HandlesForDockingItem[item];
			Internal.GDL.Methods.gdl_dock_object_set_long_name(handle, title);
		}

		public void ClearDockingItems()
		{
		}


		private Dictionary<IntPtr, DockingItem> _DockingItemsForHandle = new Dictionary<IntPtr, DockingItem>();
		private Dictionary<DockingItem, IntPtr> _HandlesForDockingItem = new Dictionary<DockingItem, IntPtr>();
		private void RegisterDockingItemHandle (DockingItem item, IntPtr handle)
		{
			_DockingItemsForHandle [handle] = item;
			_HandlesForDockingItem [item] = handle;
		}
		private DockingItem DockingItemForHandle(IntPtr handle) {
			if (_DockingItemsForHandle.ContainsKey (handle)) {
				return _DockingItemsForHandle [handle];
			}
			return null;
		}

		public void InsertDockingItem(DockingItem item, int index)
		{
			InsertDockingItemRecursive(mvarDockHandle, item as DockingWindow, index);
		}
		public void RemoveDockingItem(DockingItem item)
		{
			IntPtr handle = _HandlesForDockingItem[item];
			Internal.GDL.Methods.gdl_dock_item_unbind(handle);
		}

		private DockingItem mvarCurrentItem = null;

		public void SetDockingItem(int index, DockingItem item)
		{
		}

		public DockingItem GetCurrentItem()
		{
			IntPtr handle = (Handle as GTKNativeControl).Handle;
			if (handle == IntPtr.Zero)
				return null;
			
			return mvarCurrentItem;
		}
		public void SetCurrentItem(DockingItem item)
		{
		}

		private Internal.GDL.Constants.GdlDockItemBehavior UwtDockItemBehaviorToGtkDockItemBehavior(DockingItemBehavior value)
		{
			Internal.GDL.Constants.GdlDockItemBehavior retval = Internal.GDL.Constants.GdlDockItemBehavior.BEH_NORMAL;
			if ((value & DockingItemBehavior.Normal) == DockingItemBehavior.Normal) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_NORMAL;
			if ((value & DockingItemBehavior.Locked) == DockingItemBehavior.Locked) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_LOCKED;
			if ((value & DockingItemBehavior.NeverFloating) == DockingItemBehavior.NeverFloating) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_NEVER_FLOATING;
			if ((value & DockingItemBehavior.NeverHorizontal) == DockingItemBehavior.NeverHorizontal) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_NEVER_HORIZONTAL;
			if ((value & DockingItemBehavior.NeverVertical) == DockingItemBehavior.NeverVertical) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_NEVER_VERTICAL;
			if ((value & DockingItemBehavior.NoClose) == DockingItemBehavior.NoClose) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_CLOSE;
			if ((value & DockingItemBehavior.NoDockBottom) == DockingItemBehavior.NoDockBottom) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_DOCK_BOTTOM;
			if ((value & DockingItemBehavior.NoDockCenter) == DockingItemBehavior.NoDockCenter) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_DOCK_CENTER;
			if ((value & DockingItemBehavior.NoDockLeft) == DockingItemBehavior.NoDockLeft) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_DOCK_LEFT;
			if ((value & DockingItemBehavior.NoDockRight) == DockingItemBehavior.NoDockRight) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_DOCK_RIGHT;
			if ((value & DockingItemBehavior.NoDockTop) == DockingItemBehavior.NoDockTop) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_DOCK_TOP;
			if ((value & DockingItemBehavior.NoGrip) == DockingItemBehavior.NoGrip) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_NO_GRIP;
			if ((value & DockingItemBehavior.NoMinimize) == DockingItemBehavior.NoMinimize) retval |= Internal.GDL.Constants.GdlDockItemBehavior.BEH_CANT_ICONIFY;
			return retval;
		}

		public static Internal.GDL.Constants.GdlDockPlacement UwtDockItemPlacementToGdlDockPlacement(DockingItemPlacement placement)
		{
			switch (placement)
			{
				case DockingItemPlacement.Top: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_TOP;
				case DockingItemPlacement.Left: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_LEFT;
				case DockingItemPlacement.None: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_NONE;
				case DockingItemPlacement.Right: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_RIGHT;
				case DockingItemPlacement.Bottom: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_BOTTOM;
				case DockingItemPlacement.Center: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_CENTER;
				case DockingItemPlacement.Floating: return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_FLOATING;
			}
			return Internal.GDL.Constants.GdlDockPlacement.GDL_DOCK_NONE;
		}

		private IntPtr mvarDockHandle = IntPtr.Zero;
		private IntPtr mvarDockBarHandle = IntPtr.Zero;

		private IntPtr CreateDockingItem(DockingWindow item)
		{
			IntPtr handle = Internal.GDL.Methods.gdl_dock_item_new(item.Name, item.Title, UwtDockItemBehaviorToGtkDockItemBehavior(item.Behavior));
			Internal.GObject.Methods.g_signal_connect (handle, "selected", DockingItem_Selected_Handler);
			Internal.GObject.Methods.g_signal_connect (handle, "move-focus-child", DockingItem_MoveFocusChild_Handler);
			return handle;
		}

		private Internal.GObject.Delegates.GCallback DockingItem_Selected_Handler = null;
		private Internal.GDL.Delegates.GdlMoveFocusChildCallback DockingItem_MoveFocusChild_Handler = null;
		private void DockingItem_Selected(IntPtr hDockItem, IntPtr user_data)
		{
			DockingItem item = DockingItemForHandle (hDockItem);
			// HACK HACK HACK !!!
			mvarCurrentItem = item;

			InvokeMethod (Control, "OnSelectionChanged", EventArgs.Empty);
		}
		private void DockingItem_MoveFocusChild(IntPtr hDockItem, Internal.GTK.Constants.GtkDirectionType dir, IntPtr user_data)
		{
		}

		private void InsertDockingItemRecursive(IntPtr hDock, DockingItem item, int position)
		{
			if (item is DockingWindow)
			{
				DockingWindow dw = (item as DockingWindow);
				IntPtr childHandle = CreateDockingItem(dw);

				// TODO: fix this!
				if (!Engine.IsControlCreated(dw.ChildControl))
					Engine.CreateControl(dw.ChildControl);

				IntPtr childWidget = (dw.ChildControl.ControlImplementation.Handle as GTKNativeControl).Handle;
				if (childWidget != IntPtr.Zero)
				{
					Internal.GTK.Methods.GtkContainer.gtk_container_add(childHandle, childWidget);
				}
				else
				{
					IntPtr chdhclm = Internal.GTK.Methods.GtkLabel.gtk_label_new("Content not specified");
					Internal.GTK.Methods.GtkContainer.gtk_container_add(childHandle, chdhclm);
				}
				Internal.GDL.Methods.gdl_dock_add_item(hDock, childHandle, UwtDockItemPlacementToGdlDockPlacement(item.Placement));
				Internal.GDL.Methods.gdl_dock_object_dock(hDock, childHandle, UwtDockItemPlacementToGdlDockPlacement(item.Placement), IntPtr.Zero);

				// HACK: until we can figure out how to properly detect when a doc tab is switched
				mvarCurrentItem = item;

				RegisterDockingItemHandle(item, childHandle);
			}	
			else if (item is DockingContainer)
			{
				DockingContainer dcParent = (item as DockingContainer);

				IntPtr hDockParent = Internal.GDL.Methods.gdl_dock_item_new(item.Name, item.Title, UwtDockItemBehaviorToGtkDockItemBehavior(item.Behavior));
				for (int i = 0; i < dcParent.Items.Count; i++)
				{
					InsertDockingItemRecursive(hDockParent, dcParent.Items[i], dcParent.Items.Count - 1);
				}

				Internal.GDL.Methods.gdl_dock_add_item(hDock, hDockParent, UwtDockItemPlacementToGdlDockPlacement(item.Placement));
				Internal.GDL.Methods.gdl_dock_object_dock(hDock, hDockParent, UwtDockItemPlacementToGdlDockPlacement(item.Placement), IntPtr.Zero);
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			DockingContainerControl dock = (control as DockingContainerControl);
			// IntPtr hBox = Internal.GTK.Methods.GtkBox.gtk_box_new (Internal.GTK.Constants.GtkOrientation.Horizontal, false, 0);

			IntPtr handle = Internal.GDL.Methods.gdl_dock_new();

			foreach (DockingItem item in dock.Items)
			{
				InsertDockingItemRecursive(handle, item, dock.Items.Count - 1);
			}

			// IntPtr hDockBar = Internal.GDL.Methods.gdl_dock_bar_new(handle);

			// Internal.GTK.Methods.GtkBox.gtk_box_pack_start(hBox, hDockBar, false, false, 0);
			// Internal.GTK.Methods.GtkBox.gtk_box_pack_end(hBox, handle, true, true, 0);

			Internal.GTK.Methods.GtkWidget.gtk_widget_show(handle);

			mvarDockHandle = handle;
			// mvarDockBarHandle = hDockBar;
			return new GTKNativeControl(handle);
		}
	}
}
