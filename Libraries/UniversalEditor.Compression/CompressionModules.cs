//
//  CompressionModules.cs - provides standard accessors for known CompressionModule implementations
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

namespace UniversalEditor.Compression
{
	/// <summary>
	/// Provides standard accessors for known <see cref="CompressionModule" /> implementations.
	/// </summary>
	public static class CompressionModules
	{
		/// <summary>
		/// Gets a <see cref="CompressionModule" /> for handling bzip2 compression.
		/// </summary>
		/// <value>A <see cref="CompressionModule" /> for handling bzip2 compression.</value>
		public static Modules.Bzip2.Bzip2CompressionModule Bzip2 { get; } = new Modules.Bzip2.Bzip2CompressionModule();
		/// <summary>
		/// Gets a <see cref="CompressionModule" /> for handling DEFLATE compression.
		/// </summary>
		/// <value>A <see cref="CompressionModule" /> for handling DEFLATE compression.</value>
		public static Modules.Deflate.DeflateCompressionModule Deflate { get; } = new Modules.Deflate.DeflateCompressionModule();
		/// <summary>
		/// Gets a <see cref="CompressionModule" /> for handling gzip compression.
		/// </summary>
		/// <value>A <see cref="CompressionModule" /> for handling gzip compression.</value>
		public static Modules.Gzip.GzipCompressionModule Gzip { get; } = new Modules.Gzip.GzipCompressionModule();
		/// <summary>
		/// Gets a <see cref="CompressionModule" /> for handling zlib compression.
		/// </summary>
		/// <value>A <see cref="CompressionModule" /> for handling zlib compression.</value>
		public static Modules.Zlib.ZlibCompressionModule Zlib { get; } = new Modules.Zlib.ZlibCompressionModule();
	}
}
