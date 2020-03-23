using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface.Controls;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(Label))]
	public class LabelImplementation : GTKNativeImplementation
	{
		public LabelImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		protected override string GetControlTextInternal(Control control)
		{
			IntPtr handle = (Engine.GetHandleForControl(control) as GTKNativeControl).Handle;
			IntPtr hLabelText = Internal.GTK.Methods.GtkLabel.gtk_label_get_label(handle);
			
			string value = System.Runtime.InteropServices.Marshal.PtrToStringAuto(hLabelText);
			return value;
		}
		private Dictionary<Control, IntPtr> _ctlTextHandles = new Dictionary<Control, IntPtr>();
		protected override void SetControlTextInternal(Control control, string text)
		{
			IntPtr handle = (Engine.GetHandleForControl(control) as GTKNativeControl).GetNamedHandle("Control");

			// GTK fucks this up by passing a pointer directly to the guts of the GtkLabel
			// so, we cannot simply implicitly pass strings to and from GTK
			// 
			// we need to go through this rigamarole to ensure that *we* own the pointer to the label text
			// unfortunately, this means we are also responsible for free()ing it...
			if (_ctlTextHandles.ContainsKey(control))
			{
				System.Runtime.InteropServices.Marshal.FreeHGlobal(_ctlTextHandles[control]);
			}
			_ctlTextHandles[control] = System.Runtime.InteropServices.Marshal.StringToHGlobalAuto(text);
			
			Internal.GTK.Methods.GtkLabel.gtk_label_set_label(handle, _ctlTextHandles[control]);
		}
		protected override NativeControl CreateControlInternal(Control control)
		{
			Contract.Assert(control is Label);

			Label ctl = (control as Label);
			IntPtr handle = Internal.GTK.Methods.GtkLabel.gtk_label_new_with_mnemonic(ctl.Text);

			IntPtr hAttrList = Internal.Pango.Methods.pango_attr_list_new();
			if (ctl.Attributes.ContainsKey("scale"))
			{
				double scale_factor = (double)ctl.Attributes["scale"];
				IntPtr hAttr = Internal.Pango.Methods.pango_attr_scale_new(scale_factor);
				Internal.Pango.Methods.pango_attr_list_insert(hAttrList, hAttr);
			}

			if (ctl.WordWrap == WordWrapMode.Always)
			{
				Internal.GTK.Methods.GtkLabel.gtk_label_set_line_wrap(handle, true);
			}
			else if (ctl.WordWrap == WordWrapMode.Never)
			{
				Internal.GTK.Methods.GtkLabel.gtk_label_set_line_wrap(handle, false);
			}

			Internal.GTK.Methods.GtkLabel.gtk_label_set_attributes(handle, hAttrList);

			IntPtr hEventBox = Internal.GTK.Methods.GtkEventBox.gtk_event_box_new();
			Internal.GTK.Methods.GtkContainer.gtk_container_add(hEventBox, handle);

			return new GTKNativeControl(hEventBox, new KeyValuePair<string, IntPtr>[]
			{
				new KeyValuePair<string, IntPtr>("EventBox", hEventBox),
				new KeyValuePair<string, IntPtr>("Control", handle)
			});
		}
	}
}
