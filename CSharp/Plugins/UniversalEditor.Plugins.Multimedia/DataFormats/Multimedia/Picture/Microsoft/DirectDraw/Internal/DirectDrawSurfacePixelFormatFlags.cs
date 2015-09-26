using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Picture.Microsoft.DirectDraw.Internal
{
	public enum DirectDrawSurfacePixelFormatFlags : uint
	{
		None = 0x00000000,
		AlphaPixels = 0x00000001,
		FourCC = 0x00000004,
		Indexed = 0x00000020,
		RGB = 0x00000040
	}
}
