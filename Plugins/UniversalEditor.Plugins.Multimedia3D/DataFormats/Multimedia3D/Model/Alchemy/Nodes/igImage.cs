//
//  igImage.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igImage : igBase
	{
		public int px;
		public int py;
		public int pz;
		public bool orderPreservation;
		public int order;
		public int bitsRed;
		public int bitsGreen;
		public int bitsBlue;
		public int bitsAlpha;
		public int pfmt;
		public int imageSize;
		public bool localImage;
		public int bitsInt;
		public igClut pClut;
		public int bitsIdx;
		public int bytesPerRow;
		public bool compressed;
		public int bitsDepth;
		public string pNameString;
	}
}
