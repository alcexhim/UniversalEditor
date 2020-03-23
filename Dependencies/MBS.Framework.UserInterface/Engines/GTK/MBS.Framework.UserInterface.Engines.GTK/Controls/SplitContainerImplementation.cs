using System;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(SplitContainer))]
	public class SplitContainerImplementation : GTKNativeImplementation, ISplitContainerImplementation
	{
		public SplitContainerImplementation(Engine engine, Control control) : base(engine, control)
		{

		}

		public void SetSplitterPosition(int value)
		{
			Internal.GTK.Methods.GtkPaned.gtk_paned_set_position((Handle as GTKNativeControl).Handle, value);
		}
		public int GetSplitterPosition()
		{
			return Internal.GTK.Methods.GtkPaned.gtk_paned_get_position((Handle as GTKNativeControl).Handle);
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is SplitContainer);

			SplitContainer ctl = (control as SplitContainer);
			Internal.GTK.Constants.GtkOrientation orientation = Internal.GTK.Constants.GtkOrientation.Horizontal;
			switch (ctl.Orientation)
			{
				case Orientation.Horizontal:
				{
					orientation = Internal.GTK.Constants.GtkOrientation.Vertical;
					break;
				}
				case Orientation.Vertical:
				{
					orientation = Internal.GTK.Constants.GtkOrientation.Horizontal;
					break;
				}
			}
			IntPtr handle = Internal.GTK.Methods.GtkPaned.gtk_paned_new(orientation);

			Container ct1 = new Container ();
			ct1.Layout = ctl.Panel1.Layout;
			foreach (Control ctl1 in ctl.Panel1.Controls)
			{
				ct1.Controls.Add (ctl1, ctl.Panel1.Layout.GetControlConstraints(ctl1));
			}
			Engine.CreateControl (ct1);

			Container ct2 = new Container ();
			ct2.Layout = ctl.Panel2.Layout;
			foreach (Control ctl1 in ctl.Panel2.Controls)
			{
				ct2.Controls.Add (ctl1, ctl.Panel2.Layout.GetControlConstraints(ctl1));
			}
			Engine.CreateControl (ct2);

			Internal.GTK.Methods.GtkPaned.gtk_paned_pack1(handle, (Engine.GetHandleForControl(ct1) as GTKNativeControl).Handle, true, true);
			Internal.GTK.Methods.GtkPaned.gtk_paned_pack2(handle, (Engine.GetHandleForControl(ct2) as GTKNativeControl).Handle, true, true);
			Internal.GTK.Methods.GtkPaned.gtk_paned_set_position (handle, ctl.SplitterPosition);
			Internal.GTK.Methods.GtkPaned.gtk_paned_set_wide_handle (handle, true);
			return new GTKNativeControl(handle);
		}
	}
}
