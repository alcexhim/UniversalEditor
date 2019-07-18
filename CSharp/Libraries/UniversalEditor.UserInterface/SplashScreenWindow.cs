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
using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Drawing;

namespace UniversalEditor.UserInterface
{
	public class SplashScreenWindow : Window
	{
		public SplashScreenWindow()
		{
			
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
