using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
