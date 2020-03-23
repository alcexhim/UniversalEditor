//
//  ImageImplementation.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.DragDrop;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.GTK
{
	[ControlImplementation(typeof(PictureFrame))]
	public class PictureFrameImplementation : GTKNativeImplementation, IPictureFrameControlImplementation
	{
		public PictureFrameImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		public void SetImage(Image value)
		{
			if (value is GDKPixbufImage)
			{
				IntPtr hImage = (value as GDKPixbufImage).Handle;
				Internal.GTK.Methods.GtkImage.gtk_image_set_from_pixbuf((Handle as GTKNativeControl).Handle, hImage);
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			PictureFrame ctl = (control as PictureFrame);
			IntPtr handle = IntPtr.Zero;
			if (ctl.Image != null)
			{
				if (ctl.Image is GDKPixbufImage)
				{
					IntPtr hpixbuf = (ctl.Image as GDKPixbufImage).Handle;
					handle = Internal.GTK.Methods.GtkImage.gtk_image_new_from_pixbuf(hpixbuf);
				}
			}
			/*
			else if (ctl.IconName != null)
			{
				handle = Internal.GTK.Methods.GtkImage.gtk_image_new_from_icon_name(ctl.IconName);
			}
			*/

			Internal.GTK.Methods.GtkWidget.gtk_widget_add_events(handle, Internal.GDK.Constants.GdkEventMask.ButtonPress | Internal.GDK.Constants.GdkEventMask.ButtonRelease | Internal.GDK.Constants.GdkEventMask.PointerMotion | Internal.GDK.Constants.GdkEventMask.PointerMotionHint);
			return new GTKNativeControl(handle);
		}
	}
}
