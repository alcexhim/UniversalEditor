//
//  ClipInfo.cs
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
using UniversalEditor.IO;

namespace UniversalEditor.Plugins.SwarmAssault.DataFormats.Multimedia.PictureCollection.Internal
{
	internal struct ClipInfo
	{
		public string Name { get; set; }
		public uint Offset { get; set; }
		public uint Unknown1 { get; set; }
		public uint Length { get; set; }
		public uint Width { get; set; }
		public uint Height { get; set; }

		public Reader Reader { get; set; }

		public override string ToString()
		{
			return String.Format("'{0}' @ {1} : {2} ({3} x {4})", Name, Offset, Length, Width, Height);
		}
	}
}
