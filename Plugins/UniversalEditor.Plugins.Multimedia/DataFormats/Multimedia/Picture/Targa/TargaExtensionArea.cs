//
//  TargaExtensionArea.cs - represents an extension area in a TrueVision Targa (TGA) image file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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
using System.Collections.Generic;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
	/// <summary>
	/// Represents an extension area in a TrueVision Targa (TGA) image file.
	/// </summary>
	public class TargaExtensionArea
	{
		public bool Enabled { get; set; } = false;
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public TimeSpan JobTime { get; set; } = TimeSpan.Zero;
		public string SoftwareID { get; set; } = String.Empty;
		public string VersionString { get; set; } = String.Empty;

		public Color ColorKey { get; set; } = Color.Empty;
		public int PixelAspectRatioNumerator { get; set; } = 0;
		public int PixelAspectRatioDenominator { get; set; } = 0;
		public int GammaNumerator { get; set; } = 0;
		public int GammaDenominator { get; set; } = 0;
		public List<int> ScanLineTable { get; } = new List<int>();
		public List<Color> ColorCorrectionTable { get; } = new List<Color>();
		public int AttributesType { get; set; } = 0;

		public string AuthorName { get; set; } = null;
		public string AuthorComments { get; set; } = null;
		public string JobName { get; set; } = null;
	}
}
