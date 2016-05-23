using System;

namespace UniversalEditor.Engines.GTK
{
	public abstract class GtkContainer : GtkWidget
	{
		private GtkWidget.GtkWidgetCollection mvarControls = null;
		public GtkWidget.GtkWidgetCollection Controls { get { return mvarControls; } }

		protected override void BeforeCreateInternal ()
		{
			mvarControls = new GtkWidget.GtkWidgetCollection (this);
			base.BeforeCreateInternal ();
		}
	}
}

