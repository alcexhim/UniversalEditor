//
//  Image.cs
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
using MBS.Framework.UserInterface.Controls.Native;
using MBS.Framework.UserInterface.Drawing;

namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface IPictureFrameControlImplementation
		{
			void SetImage(Image value);
		}
	}
	public class PictureFrame : SystemControl
	{
		private Image _Image = null;
		public Image Image
		{
			get { return _Image; }
			set
			{
				_Image = value;
				if (IsCreated)
					(ControlImplementation as IPictureFrameControlImplementation)?.SetImage(value);
			}
		}
	}
}
