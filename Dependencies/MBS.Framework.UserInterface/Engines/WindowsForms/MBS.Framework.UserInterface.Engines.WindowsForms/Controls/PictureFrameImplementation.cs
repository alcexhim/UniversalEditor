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

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(PictureFrame))]
	public class PictureFrameImplementation : WindowsFormsNativeImplementation, IPictureFrameControlImplementation
	{
		public PictureFrameImplementation(Engine engine, Control control) : base(engine, control)
		{
		}

		public void SetImage(Image value)
		{
			if (value is WindowsFormsNativeImage)
			{
				System.Drawing.Image hImage = (value as WindowsFormsNativeImage).Handle;
				((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.PictureBox).Image = hImage;
			}
		}

		protected override NativeControl CreateControlInternal(Control control)
		{
			PictureFrame ctl = (control as PictureFrame);
			System.Windows.Forms.PictureBox handle = new System.Windows.Forms.PictureBox();
			if (ctl.Image != null)
			{
				System.Drawing.Image hpixbuf = (ctl.Image as WindowsFormsNativeImage).Handle;
				handle.Image = hpixbuf;
			}
			/*
			else if (ctl.IconName != null)
			{
				handle = Internal.GTK.Methods.GtkImage.gtk_image_new_from_icon_name(ctl.IconName);
			}
			*/
			handle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			return new WindowsFormsNativeControl(handle);
		}
	}
}
