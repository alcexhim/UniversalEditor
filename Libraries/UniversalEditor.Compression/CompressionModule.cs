//
//  CompressionModule.cs - the abstract base class from which all compression modules derive
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

using System;

namespace UniversalEditor.Compression
{
	/// <summary>
	/// The abstract base class from which all compression modules derive.
	/// </summary>
	public abstract class CompressionModule
	{
		public abstract string Name { get; }

		public virtual bool CanCompress { get { return true; } }
		public virtual bool CanDecompress { get { return true; } }

		private bool mvarInitialized = false;
		protected virtual void InitializeInternal()
		{
		}

		private void Initialize()
		{
			if (mvarInitialized) return;
			InitializeInternal();
			mvarInitialized = true;
		}

		public byte[] Compress(byte[] inputData)
		{
			System.IO.MemoryStream inputStream = new System.IO.MemoryStream(inputData);
			System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
			Compress(inputStream, outputStream);
			outputStream.Close();
			return outputStream.ToArray();
		}
		public byte[] Decompress(byte[] inputData, int outputLength = 0)
		{
			System.IO.MemoryStream inputStream = new System.IO.MemoryStream(inputData);
			System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
			Decompress(inputStream, outputStream, 0, outputLength);
			outputStream.Close();
			return outputStream.ToArray();
		}
		public void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			int inputLength = (int)inputStream.Length;
			int outputLength = (int)outputStream.Length;
			Decompress(inputStream, outputStream, inputLength, outputLength);
		}

		public void Compress(System.IO.Stream inputStream, System.IO.Stream outputStream)
		{
			Initialize();
			CompressInternal(inputStream, outputStream);
		}
		public void Decompress(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength)
		{
			Initialize();
			DecompressInternal(inputStream, outputStream, inputLength, outputLength);
		}

		protected abstract void CompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream);
		protected abstract void DecompressInternal(System.IO.Stream inputStream, System.IO.Stream outputStream, int inputLength, int outputLength);

		public static CompressionModule FromKnownCompressionMethod(CompressionMethod method)
		{
			switch (method)
			{
				case CompressionMethod.None: return new Modules.Store.StoreCompressionModule();
				case CompressionMethod.Bzip2: return new Modules.Bzip2.Bzip2CompressionModule();
				case CompressionMethod.Deflate: return new Modules.Deflate.DeflateCompressionModule();
				case CompressionMethod.Gzip: return new Modules.Gzip.GzipCompressionModule();
				case CompressionMethod.LZSS: return new Modules.LZSS.LZSSCompressionModule();
				case CompressionMethod.LZX: return new Modules.LZX.LZXCompressionModule();
				case CompressionMethod.XMemLZX: return new Modules.XMemLZX.XMemLZXCompressionModule();
				case CompressionMethod.Zlib: return new Modules.Zlib.ZlibCompressionModule();
			}
			throw new ArgumentException("Could not identify compression module for method " + method.ToString());
		}
	}
}
