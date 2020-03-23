using System;
using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(GroupBox))]
	public class GroupBoxImplementation : WindowsFormsNativeImplementation
	{
		public GroupBoxImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			GroupBox grp = (control as GroupBox);
			System.Windows.Forms.GroupBox native = new System.Windows.Forms.GroupBox();

			native.Text = grp.Text;

			return new WindowsFormsNativeControl(native);
		}
	}
}
