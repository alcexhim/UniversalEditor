using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
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
