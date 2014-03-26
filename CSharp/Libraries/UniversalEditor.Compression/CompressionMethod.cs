using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression
{
	public enum CompressionMethod : sbyte
	{
		Unknown = -1,
		None,
		Bzip2,
		Bzip2Solid,
		Deflate,
		Deflate64,
		Gzip,
		LZMA,
		LZMASolid,
        LZSS,
        LZH,
		LZW,
		LZX,
		PPPMd,
		Quantum,
		XMemLZX,
		Zlib,
		ZlibSolid
	}
}
