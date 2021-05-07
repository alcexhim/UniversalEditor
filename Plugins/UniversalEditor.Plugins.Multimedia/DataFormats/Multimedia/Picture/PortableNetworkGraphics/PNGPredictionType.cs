using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
	public enum PNGPredictionType
	{
		/// <summary>
		/// Zero (so that the raw byte value passes through unaltered)
		/// </summary>
		None = 0,
		/// <summary>
		/// Byte A (to the left)
		/// </summary>
		Sub = 1,
		/// <summary>
		/// Byte B (above)
		/// </summary>
		Up = 2,
		/// <summary>
		/// Mean of bytes A and B, rounded down
		/// </summary>
		Average = 3,
		/// <summary>
		/// A, B, or C, whichever is closest to p = A + B − C
		/// </summary>
		Paeth = 4
	}
}
