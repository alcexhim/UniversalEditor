using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.Explode.Internal
{
	/// <summary>
	/// Huffman code decoding tables.  count[1..MAXBITS] is the number of symbols of each length,
	/// which for a canonical code are stepped through in order. symbol[] are the symbol values in
	/// canonical order, where the number of entries is the sum of the counts in count[].
	/// </summary>
	public struct ExplodeHuffman
	{
		/// <summary>
		/// Number of symbols of each length
		/// </summary>
		public Ptr<short> count;

		/// <summary>
		/// Canonically ordered symbols
		/// </summary>
		public Ptr<short> symbol;
	}
}
