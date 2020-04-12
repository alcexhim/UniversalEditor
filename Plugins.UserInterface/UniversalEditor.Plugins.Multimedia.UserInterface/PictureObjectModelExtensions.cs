//
//  PictureObjectModelExtensions.cs - UWT extensions to PictureObjectModel
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

using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Drawing;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.Plugins.Multimedia.UserInterface
{
	/// <summary>
	/// UWT extensions to <see cref="PictureObjectModel" />.
	/// </summary>
	public static class PictureObjectModelExtensions
	{
		/// <summary>
		/// Converts the image data stored in the <see cref="PictureObjectModel" /> to a UWT <see cref="Image" /> that can be displayed onscreen.
		/// </summary>
		/// <returns>A UWT <see cref="Image" /> that can be displayed onscreen.</returns>
		/// <param name="pic">The <see cref="PictureObjectModel" /> containing the image data to convert.</param>
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
