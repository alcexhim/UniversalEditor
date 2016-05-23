using System;

namespace UniversalEditor.Engines.GTK
{
	public class GtkWindow : GtkContainer
	{
		protected override IntPtr CreateInternal ()
		{
			IntPtr handle = Internal.GTK.Methods.gtk_window_new (Internal.GTK.Constants.GtkWindowType.TopLevel);
			return handle;
		}

		private Internal.GLib.Delegates.GCallback _this_quit = null;
		private void _this_quit_impl()
		{
			OnClosed (EventArgs.Empty);
		}

		public event EventHandler Closed;
		protected virtual void OnClosed (EventArgs e)
		{
			if (Closed != null)
				Closed (this, e);
		}

		protected override void AfterCreateInternal ()
		{
			base.AfterCreateInternal ();

			this.Visible = true;
			Internal.GTK.Methods.gtk_widget_set_size_request (this.Handle, 600, 400);

			_this_quit = new Internal.GLib.Delegates.GCallback (_this_quit_impl);
			Internal.GLib.Methods.g_signal_connect (this.Handle, "destroy", _this_quit, IntPtr.Zero);
		}

		public string Text
		{
			get { return Internal.GTK.Methods.gtk_window_get_title (this.Handle); }
			set { Internal.GTK.Methods.gtk_window_set_title (this.Handle, value); }
		}


		public void Show()
		{
			this.Visible = true;
		}
	}
}

