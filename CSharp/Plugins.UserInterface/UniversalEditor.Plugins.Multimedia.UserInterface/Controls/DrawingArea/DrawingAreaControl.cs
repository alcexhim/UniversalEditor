//
//  DrawingAreaControl.cs
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
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Drawing;

using MBS.Framework.Drawing;

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.Controls.DrawingArea
{
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
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (mvarPicture == null)
			{
			}
			else
			{
				Console.WriteLine("new picture dimensions {0}x{1}", mvarPicture.Width, mvarPicture.Height);
				for (int x = 0; x < mvarPicture.Width; x++)
				{
					for (int y = 0; y < mvarPicture.Height; y++)
					{
						Color c = mvarPicture.GetPixel(x, y);
						e.Graphics.DrawLine(new Pen(c), x, y, x + 1, y + 1);
					}
				}
				// e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, mvarPicture.Width, mvarPicture.Height));
			}
		}
	}
}
