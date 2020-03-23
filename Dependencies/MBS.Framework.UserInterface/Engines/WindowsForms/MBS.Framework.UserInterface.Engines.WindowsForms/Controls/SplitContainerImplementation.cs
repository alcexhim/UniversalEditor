using System;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(SplitContainer))]
	public class SplitContainerImplementation : WindowsFormsNativeImplementation, ISplitContainerImplementation
	{
		public SplitContainerImplementation(Engine engine, Control control) : base(engine, control)
		{

		}

		public void SetSplitterPosition(int value)
		{
			((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.SplitContainer).SplitterDistance = value;
		}
		public int GetSplitterPosition()
		{
			return ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.SplitContainer).SplitterDistance;
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is SplitContainer);

			SplitContainer ctl = (control as SplitContainer);
			System.Windows.Forms.SplitContainer handle = new System.Windows.Forms.SplitContainer();
			handle.SplitterDistance = ctl.SplitterPosition;
			handle.Orientation = WindowsFormsEngine.OrientationToWindowsFormsOrientation(ctl.Orientation);

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

			System.Windows.Forms.Control ct1native = (Engine.GetHandleForControl(ct1) as WindowsFormsNativeControl).Handle;
			ct1native.Dock = System.Windows.Forms.DockStyle.Fill;
			handle.Panel1.Controls.Add(ct1native);

			System.Windows.Forms.Control ct2native = (Engine.GetHandleForControl(ct2) as WindowsFormsNativeControl).Handle;
			ct2native.Dock = System.Windows.Forms.DockStyle.Fill;
			handle.Panel2.Controls.Add(ct2native);

			return new WindowsFormsNativeControl(handle);
		}
	}
}
