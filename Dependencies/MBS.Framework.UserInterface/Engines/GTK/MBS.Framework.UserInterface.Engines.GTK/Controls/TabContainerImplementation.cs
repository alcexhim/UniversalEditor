using System;
using MBS.Framework.Collections.Generic;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(TabContainer))]
	public class TabContainerImplementation : GTKNativeImplementation, ITabContainerControlImplementation
	{
		public TabContainerImplementation(Engine engine, Control control) : base(engine, control)
		{
		}
		static TabContainerImplementation()
		{
			create_window_d = new Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr>(create_window);
			page_reordered_d = new Action<IntPtr, IntPtr, uint>(page_reordered);
			change_current_tab_d = new Func<IntPtr, int, IntPtr, bool>(change_current_tab);
			switch_page_d = new Action<IntPtr, IntPtr, uint>(switch_page);
		}

		static Random rnd = new Random();

		private static BidirectionalDictionary<TabPage, IntPtr> _TabPageHandles = new BidirectionalDictionary<TabPage, IntPtr>();
		private static void RegisterTabPage(TabPage page, IntPtr handle)
		{
			if (_TabPageHandles.ContainsValue1(page))
			{
				Console.WriteLine("TabContainer: unregistering TabPage {0} with handle {1}", page, _TabPageHandles.GetValue2(page));
				_TabPageHandles.RemoveByValue1(page);
			}

			Console.WriteLine("TabContainer: registering TabPage {0} with handle {1}", page, handle);
			_TabPageHandles.Add(page, handle);
		}
		private static TabPage GetTabPageByHandle(IntPtr handle)
		{
			if (_TabPageHandles.ContainsValue2(handle))
			{
				return _TabPageHandles.GetValue1(handle);
			}
			return null;
		}

		internal static Internal.GTK.Constants.GtkPositionType TabPositionToGtkPositionType(TabPosition value)
		{
			switch (value)
			{
				case TabPosition.Bottom: return Internal.GTK.Constants.GtkPositionType.Bottom;
				case TabPosition.Left: return Internal.GTK.Constants.GtkPositionType.Left;
				case TabPosition.Right: return Internal.GTK.Constants.GtkPositionType.Right;
				case TabPosition.Top: return Internal.GTK.Constants.GtkPositionType.Top;
			}
			throw new NotSupportedException();
		}

		public void SetTabPosition(TabPosition position)
		{
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_tab_pos((Handle as GTKNativeControl).Handle, TabPositionToGtkPositionType(position));
		}

		public void SetTabText(TabPage page, string text)
		{
			TabContainer tc = page.Parent;
			IntPtr hNotebook = (Application.Engine.GetHandleForControl(tc) as GTKNativeControl).Handle;

			IntPtr hPage = Internal.GTK.Methods.GtkNotebook.gtk_notebook_get_nth_page(hNotebook, tc.TabPages.IndexOf(page));
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_tab_label_text(hNotebook, hPage, text);
		}

		public void NotebookAppendPage(TabContainer ctl, IntPtr handle, TabPage page, int indexAfter = -1)
		{
			Container tabControlContainer = new Container();
			tabControlContainer.Layout = new BoxLayout(Orientation.Horizontal, 0);
			tabControlContainer.BeforeContextMenu += lblTabText_BeforeContextMenu;

			Label lblTabText = new Label(page.Text);
			// lblTabText.BeforeContextMenu += lblTabText_BeforeContextMenu;
			lblTabText.WordWrap = WordWrapMode.Never;

			tabControlContainer.Controls.Add(lblTabText, new BoxLayout.Constraints(true, true, 0));

			System.Reflection.FieldInfo fiParent = tabControlContainer.GetType().BaseType.GetField("mvarParent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			fiParent.SetValue(tabControlContainer, page);

			foreach (Control ctlTabButton in ctl.TabTitleControls)
			{
				tabControlContainer.Controls.Add(ctlTabButton, new BoxLayout.Constraints(false, false));
			}

			Engine.CreateControl(tabControlContainer);
			IntPtr hTabLabel = (Engine.GetHandleForControl(tabControlContainer) as GTKNativeControl).Handle;

			ContainerImplementation cimpl = new ContainerImplementation(Engine, page);
			cimpl.CreateControl(page);
			IntPtr container = (cimpl.Handle as GTKNativeControl).Handle;

			string rndgroupname = ctl.GroupName;
			if (rndgroupname == null)
			{
				rndgroupname = Application.ID.ToString() + "_TabContainer_" + (rnd.Next().ToString());
			}
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_group_name(handle, rndgroupname);

			int index = -1;
			if (indexAfter == -1)
			{
				index = Internal.GTK.Methods.GtkNotebook.gtk_notebook_append_page(handle, container, hTabLabel);
			}
			else
			{
				index = Internal.GTK.Methods.GtkNotebook.gtk_notebook_insert_page(handle, container, hTabLabel, indexAfter);
			}
			IntPtr hTabPage = Internal.GTK.Methods.GtkNotebook.gtk_notebook_get_nth_page(handle, index);
			RegisterTabPage(page, hTabPage);
			(Engine as GTKEngine).RegisterControlHandle(page, new GTKNativeControl(hTabPage));

			(ctl.ControlImplementation as TabContainerImplementation).SetTabPageDetachable(handle, hTabPage, page.Detachable);
			(ctl.ControlImplementation as TabContainerImplementation).SetTabPageReorderable(handle, hTabPage, page.Reorderable);

			Internal.GTK.Methods.GtkWidget.gtk_widget_show_all(hTabLabel);
			Internal.GTK.Methods.GtkWidget.gtk_widget_show_all(handle);
		}

		void lblTabText_BeforeContextMenu(object sender, EventArgs e)
		{
			Control lbl = (sender as Control);

			TabPage page = (lbl.Parent as TabPage);
			TabContainer tbs = page.Parent;

			BeforeTabContextMenuEventArgs ee = new BeforeTabContextMenuEventArgs(tbs, page);
			ee.ContextMenu = lbl.ContextMenu;
			ee.ContextMenuCommandID = lbl.ContextMenuCommandID;
			(tbs.ControlImplementation as TabContainerImplementation)?.OnBeforeTabContextMenu(ee);

			if (ee.Cancel) return;

			if (ee.ContextMenu != null)
			{
				lbl.ContextMenu = ee.ContextMenu;
			}
			else if (ee.ContextMenuCommandID != null)
			{
				lbl.ContextMenuCommandID = ee.ContextMenuCommandID;
			}
		}

		protected virtual void OnBeforeTabContextMenu(BeforeTabContextMenuEventArgs e)
		{
			InvokeMethod((Control as TabContainer), "OnBeforeTabContextMenu", new object[] { e });
		}


		public void ClearTabPages()
		{
			if (!Control.IsCreated)
				return;

			IntPtr handle = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
			int pageCount = Internal.GTK.Methods.GtkNotebook.gtk_notebook_get_n_pages(handle);
			for (int i = 0; i < pageCount; i++)
			{
				Internal.GTK.Methods.GtkNotebook.gtk_notebook_remove_page(handle, i);
			}
		}

		public void SetTabPageReorderable(TabPage page, bool value)
		{
			IntPtr? hptrParent = (page.Parent?.ControlImplementation.Handle as GTKNativeControl)?.Handle;
			IntPtr? hptr = (page.ControlImplementation.Handle as GTKNativeControl)?.Handle;
			SetTabPageReorderable(hptrParent, hptr, value);
		}
		public void SetTabPageDetachable(TabPage page, bool value)
		{
			IntPtr? hptrParent = (page.Parent?.ControlImplementation.Handle as GTKNativeControl)?.Handle;
			IntPtr? hptr = (page.ControlImplementation.Handle as GTKNativeControl)?.Handle;
			SetTabPageDetachable(hptrParent, hptr, value);
		}
		public void SetTabPageReorderable(IntPtr? hptrParent, IntPtr? hptr, bool value)
		{
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_tab_reorderable(hptrParent.GetValueOrDefault(), hptr.GetValueOrDefault(), value);
		}
		public void SetTabPageDetachable(IntPtr? hptrParent, IntPtr? hptr, bool value)
		{
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_tab_detachable(hptrParent.GetValueOrDefault(), hptr.GetValueOrDefault(), value);
		}

		public void InsertTabPage(int index, TabPage item)
		{
			if (!Control.IsCreated)
				return;

			IntPtr handle = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
			NotebookAppendPage((Control as TabContainer), handle, item, index);
		}

		public void RemoveTabPage(TabPage tabPage)
		{
			IntPtr handle = (Engine.GetHandleForControl(Control) as GTKNativeControl).Handle;
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_remove_page(handle, (Control as TabContainer).TabPages.IndexOf(tabPage));
		}

		private static Action<IntPtr /*GtkNotebook*/, IntPtr /*GtkWidget*/, uint> page_reordered_d = null;
		private static void page_reordered(IntPtr /*GtkNotebook*/ notebook, IntPtr /*GtkWidget*/ child, uint page_num)
		{
			TabContainer tbsParent = ((Application.Engine as GTKEngine).GetControlByHandle(notebook) as TabContainer);
			TabPage tabPage = GetTabPageByHandle(child);

			if (tbsParent == null) return;

			int oldIndex = tbsParent.TabPages.IndexOf(tabPage);
			int newIndex = (int)page_num;

			Application.DoEvents();

			tbsParent.TabPages.Reorder(oldIndex, newIndex);
		}


		private static Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr> create_window_d = null;
		private static IntPtr /*GtkNotebook*/ create_window(IntPtr /*GtkNotebook*/ notebook, IntPtr /*GtkWidget*/ page, int x, int y, IntPtr user_data)
		{
			TabContainer tbsParent = ((Application.Engine as GTKEngine).GetControlByHandle(notebook) as TabContainer);
			TabPage tabPage = GetTabPageByHandle(page);
			if (tbsParent == null) return IntPtr.Zero;

			TabPageDetachedEventArgs ee = new TabPageDetachedEventArgs(tbsParent, tabPage);
			InvokeMethod(tbsParent, "OnTabPageDetached", new object[] { ee });

			if (ee.Handled)
			{
				return IntPtr.Zero; // replace with GetControlHandle...
			}


			string groupName = Internal.GTK.Methods.GtkNotebook.gtk_notebook_get_group_name(notebook);

			Window window = new Window();
			window.Layout = new BoxLayout(Orientation.Vertical);
			window.Text = Internal.GTK.Methods.GtkNotebook.gtk_notebook_get_tab_label_text(notebook, page);
			window.Location = new Framework.Drawing.Vector2D(x, y);
			window.StartPosition = WindowStartPosition.Manual;

			TabContainer tbs = new TabContainer();
			tbs.TabPageDetached += tbsOurWindow_TabPageDetached;
			window.Controls.Add(tbs, new BoxLayout.Constraints(true, true));

			Application.Engine.CreateControl(tbs);
			Application.Engine.CreateControl(window);

			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_group_name((Application.Engine.GetHandleForControl(tbs) as GTKNativeControl).Handle, groupName);
			return (Application.Engine.GetHandleForControl(tbs) as GTKNativeControl).Handle;
		}

		static void tbsOurWindow_TabPageDetached(object sender, TabPageDetachedEventArgs e)
		{
			TabContainer parent = (sender as TabContainer);
			if (parent.TabPages.Count == 0)
			{
				parent.ParentWindow.Close();
			}
		}

		public void SetSelectedTab(TabPage page)
		{
			TabContainer tc = (Control as TabContainer);
			IntPtr hTabContainer = (Handle as GTKNativeControl).Handle;
			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_current_page(hTabContainer, tc.TabPages.IndexOf(page));
		}

		private static Action<IntPtr, IntPtr, uint> switch_page_d = null;
		private static Func<IntPtr, int, IntPtr, bool> change_current_tab_d = null;

		private static void switch_page(IntPtr /*GtkNotebook*/ notebook, IntPtr /*GtkWidget*/ page, uint page_num)
		{
			// FIXME: this gets called during a tab detach, apparently, and we haven't yet updated the new TabContainer's TabPages collection yet
			TabContainer tc = ((Application.Engine as GTKEngine).GetControlByHandle(notebook) as TabContainer);
			TabPage oldTab = tc.SelectedTab;
			TabPage newTab = tc.TabPages[(int)page_num];


			System.Reflection.FieldInfo fiSelectedTab = Framework.Reflection.GetField(tc.GetType(), "_SelectedTab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
			fiSelectedTab.SetValue(tc, newTab);

			TabContainerSelectedTabChangedEventArgs ee = new TabContainerSelectedTabChangedEventArgs(oldTab, newTab);
			InvokeMethod(tc, "OnSelectedTabChanged", new object[] { ee });
		}
		private static bool change_current_tab(IntPtr /*GtkNotebook*/ notebook, int index, IntPtr user_data)
		{
			GTKNativeControl nc = new GTKNativeControl(notebook);
			TabContainer tc = (Application.Engine.GetControlByHandle(nc) as TabContainer);
			TabPage oldTab = tc.SelectedTab;
			TabPage newTab = tc.TabPages[index];

			TabContainerSelectedTabChangingEventArgs ee = new TabContainerSelectedTabChangingEventArgs(oldTab, newTab);
			InvokeMethod(tc, "OnSelectedTabChanging", new object[] { ee });
			if (ee.Cancel) return false;

			return true;
		}


		protected override NativeControl CreateControlInternal(Control control)
		{
			TabContainer ctl = (control as TabContainer);
			IntPtr handle = Internal.GTK.Methods.GtkNotebook.gtk_notebook_new();

			foreach (TabPage tabPage in ctl.TabPages)
			{
				NotebookAppendPage(ctl, handle, tabPage);
			}

			Internal.GTK.Methods.GtkNotebook.gtk_notebook_set_tab_pos(handle, TabPositionToGtkPositionType(ctl.TabPosition));

			Internal.GObject.Methods.g_signal_connect(handle, "create_window", create_window_d, IntPtr.Zero);
			Internal.GObject.Methods.g_signal_connect(handle, "page_reordered", page_reordered_d, IntPtr.Zero);
			Internal.GObject.Methods.g_signal_connect(handle, "change_current_tab", change_current_tab_d, IntPtr.Zero);
			Internal.GObject.Methods.g_signal_connect(handle, "switch_page", switch_page_d, IntPtr.Zero);

			return new GTKNativeControl(handle);
		}
	}
}
