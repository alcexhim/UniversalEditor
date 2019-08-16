namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	public enum ZIPCompressionMethod : short
	{
		None = 0,
		Shrunk = 1,
		/// <summary>
		/// The file is Reduced with compression factor 1
		/// </summary>
		Reduced1 = 2,
		/// <summary>
		/// The file is Reduced with compression factor 2
		/// </summary>
		Reduced2 = 3,
		/// <summary>
		/// The file is Reduced with compression factor 3
		/// </summary>
		Reduced3 = 4,
		/// <summary>
		/// The file is Reduced with compression factor 4
		/// </summary>
		Reduced4 = 5,
		Implode = 6,
		Tokenizing = 7,
		Deflate = 8,
		Deflate64 = 9,
		/// <summary>
		/// PKWARE Data Compression Library Imploding (old IBM TERSE)
		/// </summary>
		PKWAREDataCompressionLibrary = 10,
		/// <summary>
		/// Reserved by PKWARE
		/// </summary>
		Reserved11 = 11,
		BZip2 = 12,
		/// <summary>
		/// Reserved by PKWARE
		/// </summary>
		Reserved13 = 13,
		LZMA = 14,
		/// <summary>
		/// Reserved by PKWARE
		/// </summary>
		Reserved15 = 15,
		/// <summary>
		/// IBM z/OS CMPSC Compression
		/// </summary>
		CMPSC = 16,
		/// <summary>
		/// Reserved by PKWARE
		/// </summary>
		Reserved17 = 17,
		/// <summary>
		/// File is compressed using IBM TERSE (new)
		/// </summary>
		IBMTerse = 18,
		/// <summary>
		/// IBM LZ77 z Architecture (PFS)
		/// </summary>
		LZ77 = 19,
		/// <summary>
		/// JPEG variant
		/// </summary>
		JPEGVariant = 96,
		/// <summary>
		/// WavPack compressed data
		/// </summary>
		WavPack = 97,
		/// <summary>
		/// PPMd version I, Rev 1
		/// </summary>
		PPMd = 98,
		/// <summary>
		/// AE-x encryption marker (see APPENDIX E)
		/// </summary>
		AEX = 99
	}
}