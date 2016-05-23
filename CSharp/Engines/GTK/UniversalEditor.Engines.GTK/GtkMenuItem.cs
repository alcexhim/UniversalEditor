using System;

namespace UniversalEditor.Engines.GTK
{
	public class GtkMenuItem : GtkWidget
	{
		public class GtkMenuItemCollection
			: System.Collections.ObjectModel.Collection<GtkMenuItem>
		{
			private GtkMenuShell _parent = null;
			internal GtkMenuItemCollection(GtkMenuShell parent)
			{
				_parent = parent;
			}

			protected override void InsertItem (int index, GtkMenuItem item)
			{
				base.InsertItem (index, item);
				Internal.GTK.Methods.gtk_menu_shell_insert (_parent.Handle, item.Handle, index);
			}
		}

		protected override IntPtr CreateInternal ()
		{
			IntPtr handle = Internal.GTK.Methods.gtk_menu_item_new ();
			return handle;
		}
		protected override void AfterCreateInternal ()
		{
			this.UseMnemonic = true;
			base.AfterCreateInternal ();
		}

		private GtkMenu mvarMenu = null;
		public GtkMenu Menu
		{
			get { return mvarMenu; }
			set
			{
				Internal.GTK.Methods.gtk_menu_item_set_submenu (this.Handle, value.Handle);
				IntPtr checkHandle = Internal.GTK.Methods.gtk_menu_item_get_submenu (this.Handle);
				if (checkHandle == value.Handle) mvarMenu = value;
			}
		}

		public string Text
		{
			get { return Internal.GTK.Methods.gtk_menu_item_get_label (this.Handle); }
			set { Internal.GTK.Methods.gtk_menu_item_set_label (this.Handle, value); }
		}
		public bool UseMnemonic
		{
			get { return Internal.GTK.Methods.gtk_menu_item_get_use_underline (this.Handle); }
			set { Internal.GTK.Methods.gtk_menu_item_set_use_underline (this.Handle, value); }
		}
	}
}

