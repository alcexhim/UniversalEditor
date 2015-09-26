using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Targa
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
