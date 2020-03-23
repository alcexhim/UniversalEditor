using System;
using System.IO;

namespace UniversalEditor.Compression.Modules.Store
{
	/// <summary>
	/// Doesn't do anything.
	/// </summary>
	public class StoreCompressionModule : CompressionModule
	{
		public override string Name => "Store";

		protected override void CompressInternal (Stream inputStream, Stream outputStream)
		{
			byte [] input = new byte [inputStream.Length];
			inputStream.Read (input, 0, (int) inputStream.Length);
			outputStream.Write (input, 0, input.Length);
		}

		protected override void DecompressInternal (Stream inputStream, Stream outputStream, int inputLength, int outputLength)
		{
			byte [] input = new byte [inputStream.Length];
			inputStream.Read (input, 0, (int) inputStream.Length);
			outputStream.Write (input, 0, input.Length);
		}
	}
}
