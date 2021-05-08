using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression.Puyo
{
	public class Compression
	{
		public PuyoCompressionModule Compressor;
		public PuyoCompressionModule Decompressor;
		private Stream Data;
		private string Filename;
		public CompressionFormat Format
		{
			get;
			private set;
		}
		public string Name
		{
			get;
			private set;
		}
		public static Dictionary<CompressionFormat, PuyoCompressionModule> Dictionary
		{
			get;
			private set;
		}
		public string DecompressFilename
		{
			get
			{
				return this.Decompressor.DecompressFilename(ref this.Data, this.Filename);
			}
		}
		public string CompressFilename
		{
			get
			{
				return this.Compressor.CompressFilename(ref this.Data, this.Filename);
			}
		}
		public string OutputDirectory
		{
			get
			{
				string result;
				if (this.Compressor != null)
				{
					result = (this.Name ?? "File Data") + " Compressed";
				}
				else
				{
					result = (this.Name ?? "File Data") + " Decompressed";
				}
				return result;
			}
		}
		public Compression(Stream data, string filename)
		{
			this.Format = CompressionFormat.NULL;
			this.Name = null;
			this.Data = data;
			this.Filename = filename;
			this.InitalizeDecompressor();
		}
		public Compression(Stream data, string filename, CompressionFormat format)
		{
			this.Name = null;
			this.Data = data;
			this.Filename = filename;
			this.Format = format;
			this.InitalizeCompressor();
		}
		public MemoryStream Decompress()
		{
			if (this.Decompressor == null)
			{
				throw new Exception("Could not decompress because no decompressor was initalized.");
			}
			return this.Decompressor.Decompress(ref this.Data);
		}
		public MemoryStream Compress()
		{
			if (this.Compressor == null)
			{
				throw new Exception("Could not compress because no compressor was initalized.");
			}
			return this.Compressor.Compress(ref this.Data, Path.GetFileName(this.Filename));
		}
		private void InitalizeDecompressor()
		{
			this.Format = CompressionFormat.NULL;
			this.Decompressor = null;
			this.Name = null;
			foreach (KeyValuePair<CompressionFormat, PuyoCompressionModule> current in Compression.Dictionary)
			{
				if (current.Value.Check(ref this.Data, this.Filename))
				{
					if (current.Value.CanDecompress)
					{
						this.Format = current.Key;
						this.Decompressor = current.Value;
						this.Name = this.Decompressor.Name;
						break;
					}
					break;
				}
			}
		}
		private void InitalizeCompressor()
		{
			if (Compression.Dictionary.ContainsKey(this.Format) && Compression.Dictionary[this.Format].CanCompress)
			{
				this.Compressor = Compression.Dictionary[this.Format];
				this.Name = this.Compressor.Name;
			}
		}
		static Compression()
		{
			Compression.Dictionary = new Dictionary<CompressionFormat, PuyoCompressionModule>();
			// Compression.Dictionary.Add(CompressionFormat.CNX, new Internal.Compressors.CNX());
			// Compression.Dictionary.Add(CompressionFormat.CXLZ, new Internal.Compressors.CXLZ());
			Compression.Dictionary.Add(CompressionFormat.LZ00, new Internal.Compressors.LZ00());
			Compression.Dictionary.Add(CompressionFormat.LZ01, new Internal.Compressors.LZ01());
			Compression.Dictionary.Add(CompressionFormat.LZSS, new Internal.Compressors.LZSS());
			Compression.Dictionary.Add(CompressionFormat.ONZ, new Internal.Compressors.ONZ());
			Compression.Dictionary.Add(CompressionFormat.PRS, new Internal.Compressors.PRS());
		}
	}
}
