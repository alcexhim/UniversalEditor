//
//  ToolbarImplementation.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System.Runtime.InteropServices;

using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(MBS.Framework.UserInterface.Controls.Toolbar))]
	public class ToolbarImplementation : GTKNativeImplementation
	{
		private Internal.GObject.Delegates.GCallbackV1I gc_clicked_handler = null;
		public ToolbarImplementation(Engine engine, Control control)
			: base(engine, control)
		{
			gc_clicked_handler = new Internal.GObject.Delegates.GCallbackV1I(gc_clicked);
		}

		private Dictionary<IntPtr, ToolbarItem> _itemsByHandle = new Dictionary<IntPtr, ToolbarItem>();
		private Dictionary<ToolbarItem, IntPtr> _handlesByItem = new Dictionary<ToolbarItem, IntPtr>();
		protected void RegisterToolbarItemHandle(ToolbarItem item, IntPtr handle)
		{
			_itemsByHandle[handle] = item;
			_handlesByItem[item] = handle;
		}

		protected IntPtr GetHandleForItem(ToolbarItem item)
		{
			if (!_handlesByItem.ContainsKey(item)) return IntPtr.Zero;
			return _handlesByItem[item];
		}
		protected ToolbarItem GetItemByHandle(IntPtr handle)
		{
			if (!_itemsByHandle.ContainsKey(handle)) return null;
			return _itemsByHandle[handle];
		}

		private void gc_clicked(IntPtr handle)
		{
			ToolbarItemButton tsb = (GetItemByHandle(handle) as ToolbarItemButton);
			if (tsb != null) InvokeMethod(tsb, "OnClick", EventArgs.Empty);
		}

		private IntPtr CreateHandleBox(IntPtr child)
		{
			return IntPtr.Zero;

			IntPtr hHandleBox = IntPtr.Zero;
			try
			{
				hHandleBox = Internal.GTK.Methods.GtkHandleBox.gtk_handle_box_new();

				Internal.GTK.Methods.GtkContainer.gtk_container_add(hHandleBox, child);
				Internal.GTK.Methods.GtkContainer.gtk_container_set_border_width(child, 5);
				Internal.GTK.Methods.GtkWidget.gtk_widget_show_all(hHandleBox);
			}
			catch (EntryPointNotFoundException ex)
			{
				Console.WriteLine("GtkHandleBox unsupported, you'll have to do it yourself now!");
			}
			return hHandleBox;
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Toolbar ctl = (control as Toolbar);

			IntPtr handle = Internal.GTK.Methods.GtkToolbar.gtk_toolbar_new();
			Internal.GTK.Methods.GtkToolbar.gtk_toolbar_set_show_arrow(handle, false);

			foreach (ToolbarItem item in ctl.Items)
			{
				IntPtr hItem = IntPtr.Zero;
				if (item is ToolbarItemButton)
				{
					ToolbarItemButton tsb = (item as ToolbarItemButton);
					if (tsb.CheckOnClick)
					{
						hItem = Internal.GTK.Methods.GtkToggleToolButton.gtk_toggle_tool_button_new();  // IntPtr.Zero, item.Title);
					}
					else
					{
						IntPtr iconWidget = IntPtr.Zero;
						string stockTypeID = Engine.StockTypeToString(tsb.StockType);

						string title = null;
						switch (tsb.DisplayStyle)
						{
							case ToolbarItemDisplayStyle.Default:
							case ToolbarItemDisplayStyle.Image:
							case ToolbarItemDisplayStyle.ImageAndText:
							{
								if (tsb.StockType != StockType.None)
								{
									iconWidget = Internal.GTK.Methods.GtkImage.gtk_image_new_from_icon_name(stockTypeID);

									int size = -1;
									if ((item as ToolbarItemButton).IconSize == ToolbarItemIconSize.Large) {
										size = 32;
									} else if ((item as ToolbarItemButton).IconSize == ToolbarItemIconSize.Small) {
										size = 16;
									}
									Internal.GTK.Methods.GtkImage.gtk_image_set_pixel_size (iconWidget, size);
								}
								break;
							}
						}
						switch (tsb.DisplayStyle)
						{
							case ToolbarItemDisplayStyle.Text:
							case ToolbarItemDisplayStyle.ImageAndText:
							{
								title = tsb.Title;
								if (tsb.StockType != StockType.None) {
									Internal.GTK.Structures.GtkStockItem stock = new Internal.GTK.Structures.GtkStockItem ();
									bool hasStock = Internal.GTK.Methods.GtkStock.gtk_stock_lookup (stockTypeID, ref stock);
									if (hasStock) {
										// fill info from GtkStockItem struct
										title = Marshal.PtrToStringAuto (stock.label);
									}
								}
								break;
							}
						}

						if (title != null) {
							title = title.Replace ("_", String.Empty);
							hItem = Internal.GTK.Methods.GtkToolButton.gtk_tool_button_new(iconWidget, title);
						} else {
							hItem = Internal.GTK.Methods.GtkToolButton.gtk_tool_button_new(iconWidget, IntPtr.Zero);
						}
						if (hItem != IntPtr.Zero)
						{
							Internal.GObject.Methods.g_signal_connect(hItem, "clicked", gc_clicked_handler);
						}
					}
					if (hItem != IntPtr.Zero)
					{
						RegisterToolbarItemHandle(item, hItem);
					}
				}
				else if (item is ToolbarItemSeparator)
				{
					hItem = Internal.GTK.Methods.GtkSeparatorToolItem.gtk_separator_tool_item_new();
				}
				if (hItem != IntPtr.Zero)
				{
					int index = ctl.Items.IndexOf(item);
					Internal.GTK.Methods.GtkToolbar.gtk_toolbar_insert(handle, hItem, index);
				}
			}

			// HandleBox f*cks up now that they stopped supporting it... ;-( BUT I WANT COMMAND BARS ON LINUX!!!
			IntPtr hHandleBox = CreateHandleBox(handle);
			if (hHandleBox != IntPtr.Zero)
			{
				return new GTKNativeControl(hHandleBox, new KeyValuePair<string, IntPtr>[]
				{
					new KeyValuePair<string, IntPtr>("HandleBox", hHandleBox),
					new KeyValuePair<string, IntPtr>("Toolbar", handle)
				});
			}

			return new GTKNativeControl(handle);
		}
	}
}
