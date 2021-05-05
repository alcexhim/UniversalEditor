using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression.Modules.Explode.Internal
{
	/// <summary>
	/// Stores information about input and output state.
	/// </summary>
	public struct ExplodeState
	{
		// input state
		/// <summary>
		/// Input function provided by user
		/// </summary>
		public System.IO.Stream inputStream;
		/// <summary>
		///
		/// </summary>
		public Ptr<byte> indata;
		/// <summary>
		/// Available input at in
		/// </summary>
		public uint left;
		/// <summary>
		/// Bit buffer
		/// </summary>
		public int bitbuf;
		/// <summary>
		/// Number of bits in bit buffer
		/// </summary>
		public int bitcnt;

		// input limit error return state for bits() and decode()
		// jmp_buf env;

		/* output state */
		/// <summary>
		/// Output function provided by user
		/// </summary>
		public System.IO.Stream outputStream;
		/// <summary>
		/// Index of next write location in outdata[]
		/// </summary>
		public int next;
		/// <summary>
		/// True to check distances (for first 4K)
		/// </summary>
		public int first;
		/// <summary>
		/// Output buffer and sliding window
		/// </summary>
		public Ptr<byte> outdata;
	}
}
