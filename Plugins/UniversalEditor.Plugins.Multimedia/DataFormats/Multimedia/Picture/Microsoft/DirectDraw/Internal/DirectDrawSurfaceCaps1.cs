//
//  DirectDrawSurfaceCaps1.cs - specifies the complexity of the surfaces stored
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

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
	/// <summary>
	/// Specifies the complexity of the surfaces stored.
	/// </summary>
	/// <remarks>When you write .dds files, you should set the DDSCAPS_TEXTURE flag, and for multiple surfaces you should also set the DDSCAPS_COMPLEX flag. However, when you read a .dds file, you should not rely on the DDSCAPS_TEXTURE and DDSCAPS_COMPLEX flags being set because some writers of such a file might not set these flags.</remarks>
	internal enum DirectDrawSurfaceCaps1
	{
		/// <summary>
		/// Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
		/// </summary>
		Complex = 0x8,
		/// <summary>
		/// Optional; should be used for a mipmap.
		/// </summary>
		Mipmap = 0x400000,
		/// <summary>
		/// Required
		/// </summary>
		Texture = 0x1000
	}
}
