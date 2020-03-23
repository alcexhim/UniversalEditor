//
//  DockingContainerImplementation.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Controls.Docking;
using MBS.Framework.UserInterface.Controls.Docking.Native;
using WeifenLuo.WinFormsUI.Docking;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(DockingContainerControl))]
	public class DockingContainerImplementation : WindowsFormsNativeImplementation, IDockingContainerNativeImplementation
	{
		public DockingContainerImplementation(Engine engine, DockingContainerControl control) : base(engine, control)
		{
		}

		public void ClearDockingItems()
		{
			DockPanel dp = ((Handle as WindowsFormsNativeControl).Handle as DockPanel);

		}

		public DockingItem GetCurrentItem()
		{
			DockPanel dp = ((Handle as WindowsFormsNativeControl).Handle as DockPanel);
			IDockContent idc = dp.ActiveDocument;
			if (_DockingItemForIDockContent.ContainsKey(idc))
				return _DockingItemForIDockContent[idc];

			return null;
		}

		private Dictionary<IDockContent, DockingItem> _DockingItemForIDockContent = new Dictionary<IDockContent, DockingItem>();
		private Dictionary<DockingItem, DockContent> _DockContentForDockingItem = new Dictionary<DockingItem, DockContent>();
		private void RegisterDockContent(DockingItem item, DockContent content)
		{
			_DockingItemForIDockContent[content] = item;
			_DockContentForDockingItem[item] = content;
		}

		public void InsertDockingItem(DockingItem item, int index)
		{
			InsertDockingItem(item, index, null);
		}
		private void InsertDockingItem(DockingItem item, int index, DockPanel parent)
		{
			if (item is DockingWindow)
			{
				InsertDockingWindow(item as DockingWindow, index, parent);
			}
			else if (item is DockingContainer)
			{
				InsertDockingContainer(item as DockingContainer, index, parent);
			}
		}

		private void InsertDockingContainer(DockingContainer item, int index, DockPanel parent)
		{
			for (int i = 0;  i < item.Items.Count; i++)
			{
				InsertDockingItem(item.Items[i], index, parent);
			}
		}

		private void InsertDockingWindow(DockingWindow item, int index, DockPanel parent)
		{
			if (parent == null)
				parent = (Handle as WindowsFormsNativeControl).Handle as WeifenLuo.WinFormsUI.Docking.DockPanel;

			if (!item.ChildControl.IsCreated)
			{
				Console.WriteLine("child control of type {0} not yet created", item.ChildControl.GetType());

				bool created = Engine.CreateControl(item.ChildControl);
				if (!created) return;
			}

			NativeControl ncChild = (Engine.GetHandleForControl(item.ChildControl) as NativeControl);
			if (ncChild is WindowsFormsNativeControl)
			{
				Console.WriteLine("adding dockpanel item");

				System.Windows.Forms.Control wfcChild = (ncChild as WindowsFormsNativeControl).Handle;

				DockContent dcontent = new DockContent();
				dcontent.TabPageContextMenuStrip = (Application.Engine as WindowsFormsEngine).BuildContextMenuStrip(Menu.FromCommand(Application.Commands["DockingWindowTabPageContextMenu"]));
				dcontent.Text = item.Title;
				wfcChild.Dock = System.Windows.Forms.DockStyle.Fill;
				dcontent.Controls.Add(wfcChild);

				RegisterDockContent(item, dcontent);

				dcontent.Show(parent, DockingItemPlacementToDockState(item.Placement, item.AutoHide));
			}
		}

		public static DockState DockingItemPlacementToDockState(DockingItemPlacement placement, bool autoHide)
		{
			switch (placement)
			{
				case DockingItemPlacement.Bottom:
				{
					if (autoHide)
					{
						return DockState.DockBottomAutoHide;
					}
					else
					{
						return DockState.DockBottom;
					}
				}
				case DockingItemPlacement.Center:
				{
					return DockState.Document;
				}
				case DockingItemPlacement.Floating:
				{
					return DockState.Float;
				}
				case DockingItemPlacement.Left:
				{
					if (autoHide)
					{
						return DockState.DockLeftAutoHide;
					}
					else
					{
						return DockState.DockLeft;
					}
				}
				case DockingItemPlacement.None:
				{
					return DockState.Hidden;
				}
				case DockingItemPlacement.Right:
				{
					if (autoHide)
					{
						return DockState.DockRightAutoHide;
					}
					else
					{
						return DockState.DockRight;
					}
				}
				case DockingItemPlacement.Top:
				{
					if (autoHide)
					{
						return DockState.DockTopAutoHide;
					}
					else
					{
						return DockState.DockTop;
					}
				}
			}
			throw new NotSupportedException();
		}

		public void RemoveDockingItem(DockingItem item)
		{
			// throw new NotImplementedException();
		}

		public void SetCurrentItem(DockingItem item)
		{
			// throw new NotImplementedException();
		}

		public void SetDockingItem(int index, DockingItem item)
		{
			// throw new NotImplementedException();
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			DockingContainerControl dcc = (control as DockingContainerControl);

			WeifenLuo.WinFormsUI.Docking.DockPanel panel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			panel.Theme = new VS2012DarkTheme();
			panel.DocumentStyle = DocumentStyle.DockingWindow;
			panel.Text = "EZMdiTabs";
			panel.Dock = System.Windows.Forms.DockStyle.Fill;
			for (int i = 0; i < dcc.Items.Count; i++)
			{
				InsertDockingItem(dcc.Items[i], i, panel);
			}

			return new WindowsFormsNativeControl(panel);
		}

		public void UpdateDockingItemName(DockingItem item, string text)
		{
			_DockContentForDockingItem[item].Name = text;
		}

		public void UpdateDockingItemTitle(DockingItem item, string text)
		{
			_DockContentForDockingItem[item].Text = text;
		}
	}
}
