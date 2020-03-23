//
//  PictureObjectModelExtensions.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.Plugins.Multimedia.UserInterface
{
	public static class PictureObjectModelExtensions
	{
		public static Image ToImage(this PictureObjectModel pic)
		{
			Image image = Image.Create(pic.Width, pic.Height);
			Graphics g = Graphics.FromImage(image);

			for (int x = 0; x < pic.Width; x++)
			{
				for (int y = 0; y < pic.Height; y++)
				{
					Color c = pic.GetPixel(x, y);
					g.DrawLine(new Pen(c), x, y, x + 1, y + 1);
				}
			}
			return image;
		}
	}
}
