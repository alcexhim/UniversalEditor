//
//  DirectDrawSurfaceCaps2.cs - additional detail about the surfaces stored
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.Microsoft.DirectDraw.Internal
{
	/// <summary>
	/// Additional detail about the surfaces stored.
	/// </summary>
	internal enum DirectDrawSurfaceCaps2
	{
		/// <summary>
		/// Required for a cube map.
		/// </summary>
		CubeMap = 0x200,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapPositiveX = 0x400,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapNegativeX = 0x800,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapPositiveY = 0x1000,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapNegativeY = 0x2000,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapPositiveZ = 0x4000,
		/// <summary>
		/// Required when these surfaces are stored in a cube map.
		/// </summary>
		CubeMapNegativeZ = 0x8000,
		/// <summary>
		/// Required for a volume texture.
		/// </summary>
		Volume = 0x200000
	}
}
