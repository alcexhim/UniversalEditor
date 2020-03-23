using System;
using System.Collections.Generic;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls.Docking.Native;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.Controls.Docking
{
	[ControlImplementation(typeof(DockingContainerControl))]
	public class DockingContainerImplementationUWT : CustomImplementation, IDockingContainerNativeImplementation
	{
		public DockingContainerImplementationUWT(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override void InvalidateInternal(int x, int y, int width, int height)
		{
			(Handle as CustomNativeControl).Handle.Invalidate(x, y, width, height);
		}

		protected override void DestroyInternal()
		{
			(Handle as CustomNativeControl).Handle.Destroy();
		}

		private class DockingDockContainer : Container
		{
			private void tbs_SelectedTabChanged(object sender, TabContainerSelectedTabChangedEventArgs e)
			{
				(_dcc?.ControlImplementation as DockingContainerImplementationUWT)._CurrentTabPage = e.NewTab;
				InvokeMethod(_dcc, "OnSelectionChanged", new object[] { e });
			}

			private Menu _DockingContainerContextMenu = null;

			private void _DockingContainerContextMenu_Close(object sender, EventArgs e)
			{
				DockingWindow dw = _DockingContainerContextMenu.GetExtraData<DockingWindow>("dw");
				Application.ExecuteCommand("DockingContainerContextMenu_Close", new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Item", dw) });
			}
			private void _DockingContainerContextMenu_CloseAllButThis(object sender, EventArgs e)
			{
				Application.ExecuteCommand("DockingContainerContextMenu_CloseAllButThis");
			}
			private void _DockingContainerContextMenu_CloseAll(object sender, EventArgs e)
			{
				Application.ExecuteCommand("DockingContainerContextMenu_CloseAll");
			}

			private void tbs_BeforeTabContextMenu(object sender, BeforeTabContextMenuEventArgs e)
			{
				e.ContextMenuCommandID = "DockingWindowTabPageContextMenu";
			}

			private DockingContainerControl _dcc = null;

			public DockingDockContainer(DockingContainerControl dcc)
			{
				_dcc = dcc;
				Layout = new BoxLayout(Orientation.Vertical);

				DockingTabContainer tbsCenterPanel = new DockingTabContainer();
				tbsCenterPanel.SelectedTabChanged += tbs_SelectedTabChanged;
				tbsCenterPanel.BeforeTabContextMenu += tbs_BeforeTabContextMenu;

				Controls.Add(tbsCenterPanel, new BoxLayout.Constraints(true, true));
			}
		}
		private class DockingTabContainer : TabContainer
		{
			public DockingTabContainer()
			{
				// this.TabPosition = TabPosition.Bottom;
				GroupName = "UwtDockingTabContainer";
			}
		}
		private class DockingSplitContainer : SplitContainer
		{
			public DockingSplitContainer()
			{
				this.Panel1.Layout = new BoxLayout(Orientation.Vertical);

				this.Panel2.Layout = new BoxLayout(Orientation.Vertical);

				this.SplitterPosition = 100;
			}
		}
		private class DockingPanelTitleBar : Container
		{
			private Label lblTitleBar = null;
			private Button cmdOptions = null;
			private Button cmdClose = null;

			private DockingTabContainer _tabContainer = null;

			public DockingPanelTitleBar(DockingTabContainer tabContainer)
			{
				this.Layout = new BoxLayout(Orientation.Horizontal);

				lblTitleBar = new Label();
				lblTitleBar.HorizontalAlignment = HorizontalAlignment.Left;
				lblTitleBar.VerticalAlignment = VerticalAlignment.Middle;
				lblTitleBar.Text = "Title bar for docking widget";
				this.Controls.Add(lblTitleBar, new BoxLayout.Constraints(true, true));

				cmdOptions = new Button();
				cmdOptions.BorderStyle = ButtonBorderStyle.None;
				cmdOptions.Text = "O";
				this.Controls.Add(cmdOptions, new BoxLayout.Constraints(false, false));

				cmdClose = new Button();
				cmdClose.BorderStyle = ButtonBorderStyle.None;
				cmdClose.Text = "X";
				this.Controls.Add(cmdClose, new BoxLayout.Constraints(false, false));

				_tabContainer = tabContainer;
				_tabContainer.SelectedTabChanged += (sender, e) => lblTitleBar.Text = _tabContainer.SelectedTab?.Text;
			}
		}

		private DockingDockContainer _ddc = null;
		protected override NativeControl CreateControlInternal(Control control)
		{
			DockingContainerControl dcc = (control as DockingContainerControl);
			DockingDockContainer ddc = new DockingDockContainer(dcc);
			_ddc = ddc;

			for (int i = 0; i < dcc.Items.Count; i++)
			{
				InsertDockingItem(dcc.Items[i], dcc.Items.Count - 1);
			}
			return new CustomNativeControl(ddc);
		}

		protected override Dimension2D GetControlSizeInternal()
		{
			return (Handle as CustomNativeControl).Handle.Size;
		}

		protected override Cursor GetCursorInternal()
		{
			throw new NotImplementedException();
		}

		protected override string GetTooltipTextInternal()
		{
			throw new NotImplementedException();
		}

		protected override bool HasFocusInternal()
		{
			return _ddc.Focused;
		}

		protected override bool IsControlVisibleInternal()
		{
			throw new NotImplementedException();
		}

		protected override void RegisterDragSourceInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			throw new NotImplementedException();
		}

		protected override void RegisterDropTargetInternal(Control control, DragDropTarget[] targets, DragDropEffect actions, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			throw new NotImplementedException();
		}

		protected override void SetControlVisibilityInternal(bool visible)
		{
			throw new NotImplementedException();
		}

		protected override void SetCursorInternal(Cursor value)
		{
			throw new NotImplementedException();
		}

		protected override void SetFocusInternal()
		{
			throw new NotImplementedException();
		}

		protected override void SetTooltipTextInternal(string value)
		{
			throw new NotImplementedException();
		}

		public void ClearDockingItems()
		{
			// _ddc.tbsCenterPanel.TabPages.Clear();
		}

		public void InsertDockingItem(DockingItem item, int index)
		{
			InsertDockingItemRecursive(item, index, null);
		}

		private void InsertDockingItemRecursive(DockingItem item, int index, DockingDockContainer parent = null)
		{
			if (parent == null)
			{
				parent = _ddc;
			}

			if (item is DockingWindow)
			{
				TabPage tab = new TabPage(item.Title);
				tab.Layout = new BoxLayout(Orientation.Horizontal);
				tab.Controls.Add((item as DockingWindow).ChildControl, new BoxLayout.Constraints(true, true));
				tab.Detachable = true;
				tab.Reorderable = true;
				tab.SetExtraData<DockingWindow>("dw", item as DockingWindow);
				FindOrCreateParentControl(tab, parent, item.Placement);

				RegisterDockingItemTabPage(tab, item);
			}
			else if (item is DockingContainer)
			{
				DockingDockContainer ddc = new DockingDockContainer(Control as DockingContainerControl);
				DockingContainer dc = (item as DockingContainer);
				for (int i = 0; i < dc.Items.Count; i++)
				{
					InsertDockingItemRecursive(dc.Items[i], dc.Items.Count - 1, ddc);
				}

				switch (item.Placement)
				{
					case DockingItemPlacement.Center:
					{
						TabPage tab = new TabPage();
						tab.Layout = new BoxLayout(Orientation.Vertical);
						tab.Text = item.Title;
						tab.Controls.Add(ddc, new BoxLayout.Constraints(true, true));
						RegisterDockingItemTabPage(tab, item);
						break;
					}
				}
			}
		}

		private void FindOrCreateParentControl(TabPage tab, DockingDockContainer parent, DockingItemPlacement placement)
		{
			switch (placement)
			{
				case DockingItemPlacement.Left:
				{
					if (parent.Controls[0] is DockingTabContainer)
					{
						DockingTabContainer tbs = (parent.Controls[0] as DockingTabContainer);
						parent.Controls.Remove(tbs);

						DockingSplitContainer dsc = new DockingSplitContainer();
						dsc.Orientation = Orientation.Vertical;

						DockingTabContainer tbs1 = new DockingTabContainer();
						tbs1.TabPosition = TabPosition.Bottom;
						dsc.Panel1.Controls.Add(new DockingPanelTitleBar(tbs1), new BoxLayout.Constraints(false, true));

						dsc.Panel1.Controls.Add(tbs1, new BoxLayout.Constraints(true, true));

						dsc.Panel2.Controls.Add(tbs, new BoxLayout.Constraints(true, true));
						parent.Controls.Add(dsc, new BoxLayout.Constraints(true, true));
						tbs1.TabPages.Add(tab);
					}
					else if (parent.Controls[0] is DockingSplitContainer)
					{
						DockingSplitContainer dsc = (parent.Controls[0] as DockingSplitContainer);
						DockingTabContainer tbs1 = (dsc.Panel1.Controls[1] as DockingTabContainer);
						tbs1.TabPages.Add(tab);
					}
					break;
				}
				case DockingItemPlacement.Bottom:
				{
					if (parent.Controls[0] is DockingTabContainer)
					{
						DockingTabContainer tbs = (parent.Controls[0] as DockingTabContainer);
						parent.Controls.Remove(tbs);

						DockingSplitContainer dsc = new DockingSplitContainer();
						dsc.Orientation = Orientation.Horizontal;

						DockingTabContainer tbs1 = new DockingTabContainer();
						tbs1.TabPosition = TabPosition.Bottom;
						dsc.Panel2.Controls.Add(new DockingPanelTitleBar(tbs1), new BoxLayout.Constraints(false, true));
						dsc.Panel2.Controls.Add(tbs1, new BoxLayout.Constraints(true, true));

						dsc.Panel1.Controls.Add(tbs, new BoxLayout.Constraints(true, true));
						parent.Controls.Add(dsc, new BoxLayout.Constraints(true, true));
						tbs1.TabPages.Add(tab);
					}
					else if (parent.Controls[0] is DockingSplitContainer)
					{
						DockingTabContainer tbs1 = null;
						DockingSplitContainer dsc = (parent.Controls[0] as DockingSplitContainer);
						if (dsc.Orientation == Orientation.Vertical)
						{
							// huh, we already have a vertical SplitContainer
							// must be a left- or right-docked item
							if (dsc.Panel1.Controls[1] is DockingSplitContainer)
							{
							}
							else if (dsc.Panel1.Controls[1] is DockingTabContainer)
							{
								// left-docked item
								if (dsc.Panel2.Controls[0] is DockingTabContainer)
								{
									DockingTabContainer tbs = (dsc.Panel2.Controls[0] as DockingTabContainer);
									dsc.Panel2.Controls.Remove(tbs);

									DockingSplitContainer dsc1 = new DockingSplitContainer();
									dsc1.Orientation = Orientation.Horizontal;
									dsc1.Panel1.Controls.Add(tbs, new BoxLayout.Constraints(true, true));

									tbs1 = new DockingTabContainer();
									tbs1.TabPosition = TabPosition.Bottom;
									dsc1.Panel2.Controls.Add(new DockingPanelTitleBar(tbs1), new BoxLayout.Constraints(false, true));
									dsc1.Panel2.Controls.Add(tbs1, new BoxLayout.Constraints(true, true));

									dsc.Panel2.Controls.Add(dsc1, new BoxLayout.Constraints(true, true));
								}
								else if (dsc.Panel2.Controls.Count > 1 && dsc.Panel2.Controls[1] is DockingTabContainer)
								{
									tbs1 = (dsc.Panel2.Controls[1] as DockingTabContainer);
								}
							}
							else if (dsc.Panel2.Controls[0] is DockingSplitContainer)
							{
								tbs1 = ((dsc.Panel2.Controls[0] as SplitContainer).Panel2.Controls[1] as DockingTabContainer);
							}
						}
						else
						{
							tbs1 = (dsc.Panel2.Controls[1] as DockingTabContainer);
						}

						if (tbs1 != null)
						{
							tbs1.TabPages.Add(tab);
						}
						else
						{
							Console.Error.WriteLine("tbs1 is NULL");
						}
					}
					break;
				}
				case DockingItemPlacement.Center:
				{
					DockingTabContainer tbs = null;
					if (parent.Controls[0] is DockingTabContainer)
					{
						tbs = (parent.Controls[0] as DockingTabContainer);
					}
					else if (parent.Controls[0] is DockingSplitContainer)
					{
						DockingSplitContainer dsc = (parent.Controls[0] as DockingSplitContainer);
						if (dsc.Panel1.Controls[0] is DockingTabContainer)
						{
							tbs = (dsc.Panel1.Controls[0] as DockingTabContainer);
						}
						else if (dsc.Panel2.Controls[0] is DockingTabContainer)
						{
							tbs = (dsc.Panel2.Controls[0] as DockingTabContainer);
						}
						else if (dsc.Panel2.Controls[0] is DockingSplitContainer)
						{
							DockingSplitContainer dsc1 = dsc.Panel2.Controls[0] as DockingSplitContainer;
							if (dsc1.Panel1.Controls[0] is DockingTabContainer)
							{
								tbs = (dsc1.Panel1.Controls[0] as DockingTabContainer);
							}
							else if (dsc1.Panel2.Controls[0] is DockingTabContainer)
							{
								tbs = (dsc1.Panel2.Controls[0] as DockingTabContainer);
							}
						}
					}

					if (tbs != null)
						tbs.TabPages.Add(tab);
					break;
				}
			}
		}

		public void RemoveDockingItem(DockingItem item)
		{
			if (!_TabPagesForDockingItem.ContainsKey(item))
				return;

			TabPage tabPage = _TabPagesForDockingItem[item];
			tabPage.Parent.TabPages.Remove(tabPage);
		}

		public void SetDockingItem(int index, DockingItem item)
		{
			throw new NotImplementedException();
		}

		private TabPage _CurrentTabPage = null;

		private Dictionary<TabPage, DockingItem> _DockingItemsForTabPage = new Dictionary<TabPage, DockingItem>();
		private Dictionary<DockingItem, TabPage> _TabPagesForDockingItem = new Dictionary<DockingItem, TabPage>();

		private void RegisterDockingItemTabPage(TabPage tab, DockingItem item)
		{
			_DockingItemsForTabPage[tab] = item;
			_TabPagesForDockingItem[item] = tab;
		}

		public DockingItem GetCurrentItem()
		{
			if (_CurrentTabPage == null)
				return null;
			return _DockingItemsForTabPage[_CurrentTabPage];
		}

		public void SetCurrentItem(DockingItem item)
		{
			TabPage tab = GetTabPageForDockingItem(item);
			tab.Parent.SelectedTab = tab;
		}

		public void UpdateDockingItemName(DockingItem item, string text)
		{
			TabPage tab = GetTabPageForDockingItem(item);
			tab.Name = text;
		}

		public void UpdateDockingItemTitle(DockingItem item, string text)
		{
			TabPage tab = GetTabPageForDockingItem(item);
			tab.Text = text;
		}

		private TabPage GetTabPageForDockingItem(DockingItem item)
		{
			return (_TabPagesForDockingItem.ContainsKey(item) ? _TabPagesForDockingItem[item] : null);
		}
	}
}
