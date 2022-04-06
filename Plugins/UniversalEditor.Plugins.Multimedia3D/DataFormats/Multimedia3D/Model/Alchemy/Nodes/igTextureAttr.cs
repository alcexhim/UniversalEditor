//
//  igTextureAttr.cs
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
	public class igTextureAttr : igAttr
	{
		public uint bColor { get; set; }
		public IGBTextureFilter MagFilter { get; set; }
		public IGBTextureFilter MinFilter { get; set; }
		public IGBTextureWrap WrapS { get; set; }
		public IGBTextureWrap WrapT { get; set; }
		public IGBTextureMipmapMode MipmapMode { get; set; }
		public IGBTextureSource Source { get; set; }
		public igImage Image { get; set; } = null;
		public bool Paging { get; set; } = false;
		public object tu { get; set; }
		public uint ImageCount { get; set; }
		public igImageMipMapList ImageMipmaps { get; set; }
	}
}
