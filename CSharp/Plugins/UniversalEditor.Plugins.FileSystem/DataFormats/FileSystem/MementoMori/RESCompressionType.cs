using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.MementoMori
{
	/// <summary>
	/// The compression type field is known to have the values 0 (uncompressed data) or 2 (ZLib
	/// compression), in which case the data size field specifies the compressed size of the file. 
	/// </summary>
	public enum RESCompressionType : uint
	{
		None = 0,
		Zlib = 2
	}
}
