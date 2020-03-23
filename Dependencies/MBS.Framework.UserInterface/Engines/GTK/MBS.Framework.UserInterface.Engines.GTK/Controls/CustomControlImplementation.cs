using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Engines.GTK.Drawing;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(CustomControl))]
	public class CustomControlImplementation : GTKNativeImplementation
	{
		public CustomControlImplementation(Engine engine, Control control) : base(engine, control)
		{
			DrawHandler_Handler = new Internal.GObject.Delegates.DrawFunc(DrawHandler);
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			IntPtr hAdjust = Internal.GTK.Methods.GtkAdjustment.gtk_adjustment_new(0.0, 0.0, 1.0, 0.1, 0.5, 0.5);
			IntPtr vAdjust = Internal.GTK.Methods.GtkAdjustment.gtk_adjustment_new(0.0, 0.0, 1.0, 0.1, 0.5, 0.5);
			IntPtr handle = Internal.GTK.Methods.GtkLayout.gtk_layout_new(hAdjust, vAdjust);

			Internal.GObject.Methods.g_signal_connect(handle, "draw", DrawHandler_Handler);
			Internal.GTK.Methods.GtkWidget.gtk_widget_set_can_focus(handle, true);
			Internal.GTK.Methods.GtkWidget.gtk_widget_add_events(handle, Internal.GDK.Constants.GdkEventMask.ButtonPress | Internal.GDK.Constants.GdkEventMask.ButtonRelease | Internal.GDK.Constants.GdkEventMask.KeyPress | Internal.GDK.Constants.GdkEventMask.KeyRelease | Internal.GDK.Constants.GdkEventMask.PointerMotion | Internal.GDK.Constants.GdkEventMask.PointerMotionHint);

			return new GTKNativeControl(handle);
		}

		private Internal.GObject.Delegates.DrawFunc DrawHandler_Handler;
		/// <summary>
		/// Handler for draw signal
		/// </summary>
		/// <param name="widget">the object which received the signal</param>
		/// <param name="cr">the cairo context to draw to</param>
		/// <param name="user_data">user data set when the signal handler was connected.</param>
		/// <returns>TRUE to stop other handlers from being invoked for the event. FALSE to propagate the event further.</returns>
		private bool DrawHandler(IntPtr /*GtkWidget*/ widget, IntPtr /*CairoContext*/ cr, IntPtr user_data)
		{
			// FIXME: UWT gets confused if we do this, probably because something's not quite right with the IntPtr comparison down the road
			// CustomControl ctl = (Engine as GTKEngine).GetControlByHandle(widget) as CustomControl;

			// doing it this way works though (probably because we don't compare any IntPtrs...)
			CustomControl ctl = Control as CustomControl;

			Contract.Assert(ctl != null);

			GTKGraphics graphics = new GTKGraphics(cr);

			Dimension2D size = Control.Size;
			Internal.GTK.Methods.GtkLayout.gtk_layout_set_size((Handle as GTKNativeControl).Handle, (uint)size.Width, (uint)size.Height);

			PaintEventArgs e = new PaintEventArgs(graphics);
			InvokeMethod(ctl, "OnPaint", e);
			if (e.Handled) return true;

			return false;
		}
	}
}
