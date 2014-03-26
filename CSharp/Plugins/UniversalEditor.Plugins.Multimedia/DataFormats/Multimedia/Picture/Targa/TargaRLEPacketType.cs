using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
	/// <summary>
	/// The RLE packet type used in a RLE compressed image.
	/// </summary>
	public enum TargaRLEPacketType
	{
		/// <summary>
		/// A raw RLE packet type.
		/// </summary>
		Uncompressed = 0,
		/// <summary>
		/// A run-length RLE packet type.
		/// </summary>
		Compressed = 1
	}
}
