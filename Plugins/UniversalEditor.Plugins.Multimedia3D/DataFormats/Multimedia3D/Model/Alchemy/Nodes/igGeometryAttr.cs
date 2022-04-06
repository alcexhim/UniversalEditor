//
//  igGeometryAttr.cs
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
	public class igGeometryAttr : igAttr
	{
		public igDx9VertexArray1_1 VertexArray { get; set; }
		public IGBPrimitiveType PrimitiveType { get; set; }
		public uint PrimitiveCount { get; set; }
		public uint Offset { get; set; }
		public uint PrimitiveLengths { get; set; }
		public uint Handle { get; set; }
		public uint dPds { get; set; }
		public uint dPdt { get; set; }
	}
}
