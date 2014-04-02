using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	public enum CHDCompressionType
	{
		None = 0,
		Zlib = 1,
		ZlibPlus = 2,
		/// <summary>
		/// Supported as of V4.
		/// </summary>
		AV = 3
	}
}
