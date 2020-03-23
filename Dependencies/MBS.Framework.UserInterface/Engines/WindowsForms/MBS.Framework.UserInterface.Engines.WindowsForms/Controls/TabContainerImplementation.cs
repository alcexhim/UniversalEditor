//
//  TabContainerImplementation.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.UserInterface.Input.Mouse;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(TabContainer))]
	public class TabContainerImplementation : WindowsFormsNativeImplementation, ITabContainerControlImplementation
	{
		public TabContainerImplementation(Engine engine, TabContainer control)
			: base(engine, control)
		{
		}

		public void ClearTabPages()
		{
			(Control as TabContainer).TabPages.Clear();
		}

		public void InsertTabPage(int index, TabPage item)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TabControl).TabPages.Insert(index, CreateTabPage(item));
		}

		private System.Windows.Forms.TabPage CreateTabPage(TabPage item)
		{
			System.Windows.Forms.TabPage tab = new System.Windows.Forms.TabPage();
			tab.UseVisualStyleBackColor = true;
			tab.Text = item.Text;

			NativeControl nc = (new ContainerImplementation(Engine, item)).CreateControl(item);
			System.Windows.Forms.Control tabCtl = (nc as WindowsFormsNativeControl).Handle;
			tabCtl.Dock = System.Windows.Forms.DockStyle.Fill;
			tab.Controls.Add(tabCtl);

			return tab;
		}

		public void RemoveTabPage(TabPage tabPage)
		{
			(Control as TabContainer).TabPages.Remove(tabPage);
		}

		public void SetTabPageDetachable(TabPage page, bool value)
		{
			throw new NotImplementedException();
		}

		public void SetSelectedTab(TabPage page)
		{
			TabContainer tc = (Control as TabContainer);
			if (!tc.TabPages.Contains(page))
			{
				return;
			}

			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TabControl).SelectedTab = GetWFTabForTabPage(page);
		}

		private System.Collections.Generic.Dictionary<TabPage, System.Windows.Forms.TabPage> _WFTabsForTabPage = new System.Collections.Generic.Dictionary<TabPage, System.Windows.Forms.TabPage>();
		private System.Windows.Forms.TabPage GetWFTabForTabPage(TabPage page)
		{
			if (!_WFTabsForTabPage.ContainsKey(page))
				return null;
			return _WFTabsForTabPage[page];
		}

		public void SetTabPageReorderable(TabPage page, bool value)
		{
			throw new NotImplementedException();
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			TabContainer ctl = (control as TabContainer);

			System.Windows.Forms.TabControl tbs = new System.Windows.Forms.TabControl();
			for (int i = 0; i < ctl.TabPages.Count; i++)
			{
				System.Windows.Forms.TabPage tab = CreateTabPage(ctl.TabPages[i]);
				tbs.TabPages.Add(tab);
			}
			return new WindowsFormsNativeControl(tbs);
		}

		public void SetTabText(TabPage page, string text)
		{
			GetWFTabForTabPage(page).Text = text;
		}

		public void SetTabPosition(TabPosition position)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.TabControl).Alignment = TabPositionToWFTabAlignment(position);
		}

		public static System.Windows.Forms.TabAlignment TabPositionToWFTabAlignment(TabPosition position)
		{
			switch (position)
			{
				case TabPosition.Bottom: return System.Windows.Forms.TabAlignment.Bottom;
				case TabPosition.Left: return System.Windows.Forms.TabAlignment.Left;
				case TabPosition.Right: return System.Windows.Forms.TabAlignment.Right;
				case TabPosition.Top: return System.Windows.Forms.TabAlignment.Top;
			}
			throw new NotSupportedException();
		}
	}
}
