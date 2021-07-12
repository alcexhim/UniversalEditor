//
//  EXRChromaticities.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.EXR
{
	public class EXRChromaticities
	{
		public float RedX { get; set; }
		public float RedY { get; set; }
		public float GreenX { get; set; }
		public float GreenY { get; set; }
		public float BlueX { get; set; }
		public float BlueY { get; set; }
		public float WhiteX { get; set; }
		public float WhiteY { get; set; }
	}
}
