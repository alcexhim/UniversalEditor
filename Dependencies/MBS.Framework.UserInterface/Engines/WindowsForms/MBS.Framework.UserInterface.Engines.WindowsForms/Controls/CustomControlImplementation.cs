using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(CustomControl))]
	public class CustomControlImplementation : WindowsFormsNativeImplementation
	{
		public CustomControlImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		private class BufferedUserControl : System.Windows.Forms.UserControl
		{
			public BufferedUserControl()
			{
				DoubleBuffered = true;
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			BufferedUserControl handle = new BufferedUserControl();
			handle.Paint += handle_Paint;
			return new WindowsFormsNativeControl(handle);
		}

		private void handle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// FIXME: UWT gets confused if we do this, probably because something's not quite right with the IntPtr comparison down the road
			// CustomControl ctl = (Engine as GTKEngine).GetControlByHandle(widget) as CustomControl;

			// doing it this way works though (probably because we don't compare any IntPtrs...)
			CustomControl ctl = Control as CustomControl;

			Contract.Assert(ctl != null);

			WindowsFormsNativeGraphics graphics = new WindowsFormsNativeGraphics(e.Graphics);

			PaintEventArgs ee = new PaintEventArgs(graphics);
			InvokeMethod(ctl, "OnPaint", ee);
		}
	}
}
