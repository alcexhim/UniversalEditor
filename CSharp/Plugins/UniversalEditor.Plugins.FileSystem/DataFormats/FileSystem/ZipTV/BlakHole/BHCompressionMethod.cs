using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZipTV.BlakHole
{
	public enum BHCompressionMethod
	{
		None = 0,
		Fuse = 3,
		Deflate = 8,
		Bzip2 = 12
	}
}
