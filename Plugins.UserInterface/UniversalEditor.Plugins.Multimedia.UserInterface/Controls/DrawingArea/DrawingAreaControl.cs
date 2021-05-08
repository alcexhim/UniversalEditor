//
//  DrawingAreaControl.cs - provides a UWT CustomControl that facilitates drawing and other image manipulation operations
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Drawing;
using MBS.Framework.UserInterface.Input.Mouse;

using UniversalEditor.ObjectModels.Multimedia.Picture;

using UniversalEditor.Plugins.Multimedia.UserInterface;

namespace UniversalEditor.Controls.DrawingArea
{
	/// <summary>
	/// Provides a UWT <see cref="CustomControl" /> that facilitates drawing and other image manipulation operations.
	/// </summary>
	public class DrawingAreaControl : CustomControl
	{
		private PictureObjectModel mvarPicture = null;
		public PictureObjectModel Picture
		{
			get { return mvarPicture; }
			set
			{
				mvarPicture = value;
				this.Refresh();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			Focus();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (mvarPicture == null)
			{
			}
			else
			{
				Console.WriteLine("new picture dimensions {0}x{1}", mvarPicture.Width, mvarPicture.Height);
				if (mvarPicture.Width == 0 || mvarPicture.Height == 0) return;

				Image image = mvarPicture.ToImage();
				e.Graphics.DrawImage(image, 0, 0);
			}

			if (Focused)
			{
				e.Graphics.DrawFocus(0, 0, Size.Width, Size.Height);
			}
		}
	}
}
