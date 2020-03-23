using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(Label))]
	public class LabelImplementation : WindowsFormsNativeImplementation
	{
		public LabelImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			(Handle as WindowsFormsNativeControl).Handle.Text = Control.Text?.Replace('_', '&');
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is Label);

			Label ctl = (control as Label);

			System.Windows.Forms.Label handle = new System.Windows.Forms.Label();
			handle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			handle.UseMnemonic = ctl.UseMnemonic;
			handle.TextAlign = WindowsFormsEngine.HorizontalVerticalAlignmentToContentAlignment(ctl.HorizontalAlignment, ctl.VerticalAlignment);

			/*
			if (ctl.WordWrap == WordWrapMode.Always)
			{
				Internal.GTK.Methods.GtkLabel.gtk_label_set_line_wrap(handle, true);
			}
			else if (ctl.WordWrap == WordWrapMode.Never)
			{
				Internal.GTK.Methods.GtkLabel.gtk_label_set_line_wrap(handle, false);
			}
			*/

			return new WindowsFormsNativeControl(handle);
		}
	}
}
