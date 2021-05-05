//
//  ZIPCompressionMethod.cs - indicates the type of compression used in a ZIP archive
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	/// <summary>
	/// Indicates the type of compression used in a ZIP archive.
	/// </summary>
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
