using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Compression
{
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
		public byte[] Decompress(byte[] inputData)
		{
			System.IO.MemoryStream inputStream = new System.IO.MemoryStream(inputData);
			System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
			Decompress(inputStream, outputStream);
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
				/*
				case CompressionMethod.Deflate: return new Modules.Deflate.DeflateCompressionModule();
				case CompressionMethod.Gzip: return new Modules.Gzip.GzipCompressionModule();
				case CompressionMethod.LZSS: return new Modules.LZSS.LZSSCompressionModule();
				case CompressionMethod.LZX: return new Modules.LZX.LZXCompressionModule();
				case CompressionMethod.XMemLZX: return new Modules.XMemLZX.XMemLZXCompressionModule();
				*/
				case CompressionMethod.Zlib: return new Modules.Zlib.ZlibCompressionModule();
			}
			throw new ArgumentException("Could not identify compression module for method " + method.ToString());
		}
	}
}
