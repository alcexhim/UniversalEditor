using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>Methods 1 to 3 use Lempel-Ziv 77 sliding window with static Huffman encoding, method 4 uses Lempel-Ziv 77 sliding window with pointer/length unary encoding.</remarks>
	public enum ARJCompressionMethod : byte
	{
		Store = 0,
		CompressedMost = 1,
		Compressed = 2,
		CompressedFaster = 3,
		CompressedFastest = 4
	}
}
