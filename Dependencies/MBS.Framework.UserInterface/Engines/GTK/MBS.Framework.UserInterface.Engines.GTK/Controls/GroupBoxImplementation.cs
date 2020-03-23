using System;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(GroupBox))]
	public class GroupBoxImplementation : ContainerImplementation
	{
		public GroupBoxImplementation(Engine engine, GroupBox control) : base(engine, control)
		{
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			GTKNativeControl ncContainer = (base.CreateControlInternal(control) as GTKNativeControl);

			IntPtr h = Internal.GTK.Methods.GtkFrame.gtk_frame_new();
			Internal.GTK.Methods.GtkFrame.gtk_frame_set_label(h, control.Text);

			Internal.GTK.Methods.GtkContainer.gtk_container_add(h, ncContainer.Handle);
			return new GTKNativeControl(h);
		}
	}
}
