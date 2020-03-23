using System;
using System.Diagnostics.Contracts;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;
using System.Runtime.InteropServices;

namespace MBS.Framework.UserInterface.Engines.GTK.Controls
{
	[ControlImplementation(typeof(Button))]
	public class ButtonImplementation : GTKNativeImplementation, IButtonControlImplementation
	{
		private Internal.GObject.Delegates.GCallback gc_Button_Clicked = null;
		public ButtonImplementation(Engine engine, Control control) : base(engine, control)
		{
			gc_Button_Clicked = new Internal.GObject.Delegates.GCallback(Button_Clicked);
		}

		private void Button_Clicked(IntPtr handle, IntPtr data)
		{
			EventArgs e = new EventArgs();
			base.OnClick(e);
		}

		protected override void OnClick(EventArgs e)
		{
			// Button clicks get handled by the OS in Button_Clicked handler
			// base.OnClick(e);
		}

		protected override string GetControlTextInternal(Control control)
		{
			IntPtr handle = (Engine.GetHandleForControl(control) as GTKNativeControl).Handle;
			IntPtr hTitle = Internal.GTK.Methods.GtkButton.gtk_button_get_label (handle);
			return Marshal.PtrToStringAuto (hTitle);
		}
		protected override void SetControlTextInternal(Control control, string text)
		{
			IntPtr handle = (Handle as GTKNativeControl).Handle;
			IntPtr hTitle = Marshal.StringToHGlobalAuto (text);
			Internal.GTK.Methods.GtkButton.gtk_button_set_label(handle, hTitle);
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			Button ctl = (control as Button);
			Contract.Assert(ctl != null);

			IntPtr handle = Internal.GTK.Methods.GtkButton.gtk_button_new();
			if (Internal.GTK.Methods.Gtk.LIBRARY_FILENAME == Internal.GTK.Methods.Gtk.LIBRARY_FILENAME_V2)
			{
			}
			else
			{
				Internal.GTK.Methods.GtkButton.gtk_button_set_always_show_image(handle, ctl.AlwaysShowImage);
			}
			switch (ctl.BorderStyle)
			{
				case ButtonBorderStyle.None:
				{ 
					Internal.GTK.Methods.GtkButton.gtk_button_set_relief(handle, Internal.GTK.Constants.GtkReliefStyle.None);
					break;
				}
				case ButtonBorderStyle.Half:
				{ 
					Internal.GTK.Methods.GtkButton.gtk_button_set_relief(handle, Internal.GTK.Constants.GtkReliefStyle.Half);
					break;
				}
				case ButtonBorderStyle.Normal:
				{ 
					Internal.GTK.Methods.GtkButton.gtk_button_set_relief(handle, Internal.GTK.Constants.GtkReliefStyle.Normal);
					break;
				}
			}

			if (ctl.StockType != StockType.None) {
				PictureFrame image = new PictureFrame ();
				image.Image = UserInterface.Drawing.Image.FromName(Engine.StockTypeToString (ctl.StockType), (int) ctl.ImageSize.Width);
				if (Engine.CreateControl (image)) {
					IntPtr hImage = (Engine.GetHandleForControl(image) as GTKNativeControl).Handle;
					Internal.GTK.Methods.GtkButton.gtk_button_set_image(handle, hImage);
				}
			}

			// DON'T SET THIS... only Dialog buttons should get this by default
			// Internal.GTK.Methods.Methods.gtk_widget_set_can_default (handle, true);

			Internal.GObject.Methods.g_signal_connect(handle, "clicked", gc_Button_Clicked, new IntPtr(0xDEADBEEF));

			Internal.GTK.Methods.GtkButton.gtk_button_set_image_position (handle, (Engine as GTKEngine).RelativePositionToGtkPositionType(ctl.ImagePosition));

			if (Internal.GTK.Methods.Gtk.LIBRARY_FILENAME == Internal.GTK.Methods.Gtk.LIBRARY_FILENAME_V3)
			{
				switch (ctl.HorizontalAlignment)
				{
					case HorizontalAlignment.Left:
					{
						Internal.GTK.Methods.GtkWidget.gtk_widget_set_halign(handle, Internal.GTK.Constants.GtkAlign.Start);
						break;
					}
					case HorizontalAlignment.Center:
					{
						Internal.GTK.Methods.GtkWidget.gtk_widget_set_halign(handle, Internal.GTK.Constants.GtkAlign.Center);
						break;
					}
					case HorizontalAlignment.Right:
					{
						Internal.GTK.Methods.GtkWidget.gtk_widget_set_halign(handle, Internal.GTK.Constants.GtkAlign.End);
						break;
					}
				}
			}

			// we do this to support older versions of Gtk+ that may not handle gtk_widget_set_focus_on_click
			Internal.GTK.Methods.GtkButton.gtk_button_set_focus_on_click (handle, ctl.FocusOnClick);

			return new GTKNativeControl(handle);
		}

		public RelativePosition GetImagePosition()
		{
			if (Handle == null) return RelativePosition.Default;

			IntPtr handle = (Handle as GTKNativeControl).Handle;
			Internal.GTK.Constants.GtkPositionType value = Internal.GTK.Methods.GtkButton.gtk_button_get_image_position (handle);
			return (Engine as GTKEngine).GtkPositionTypeToRelativePosition(value);
		}
		public void SetImagePosition(RelativePosition value)
		{
			if (Handle == null) return;

			IntPtr handle = (Handle as GTKNativeControl).Handle;
			Internal.GTK.Constants.GtkPositionType value2 = (Engine as GTKEngine).RelativePositionToGtkPositionType(value);
			Internal.GTK.Methods.GtkButton.gtk_button_set_image_position (handle, value2);
		}
	}
}
