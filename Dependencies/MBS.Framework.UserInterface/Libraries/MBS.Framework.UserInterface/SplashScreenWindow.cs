//
//  SplashScreenWindow.cs
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
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Layouts;

namespace MBS.Framework.UserInterface
{
	public class SplashScreenWindow : Window
	{
		public SplashScreenWindow()
		{
			this.Decorated = false;
			this.Layout = new BoxLayout(Orientation.Vertical);
			this.StartPosition = WindowStartPosition.Center;
			
			PictureFrame image = new PictureFrame();
			if (System.IO.File.Exists("splash.bmp"))
			{
				image.Image = Image.FromFile("splash.bmp");
			}
			else
			{
				image.Image = Image.FromName("universal-editor", 300);
				this.Size = new Dimension2D(300, 300);
			}
			
			Label lbl = new Label("Universal Editor");
			lbl.Attributes.Add("scale", 1.4);
			
			this.Controls.Add(image, new BoxLayout.Constraints(true, true));
			// this.Controls.Add(lbl, new BoxLayout.Constraints(true, true));
		}

		protected internal override void OnRealize(EventArgs e)
		{
			base.OnRealize(e);
			OnShown(e);
		}

		private static bool created = false;
		protected internal override void OnMapped(EventArgs e)
		{
			base.OnMapped(e);
if (created) return;
created = true;
		}
		public void SetStatus(string message, int progressValue, int progressMinimum, int progressMaximum)
		{

		}

		
	}
	public class SplashScreenSettings
	{
		public bool Enabled { get; set; }
		public string ImageFileName { get; set; }
		public string SoundFileName { get; set; }

		// private Image mvarImage = null;
		// public Image Image { get { return mvarImage; } set { mvarImage = value; } }

		public System.IO.MemoryStream Sound { get; set; }
	}
}
