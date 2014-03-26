using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniversalEditor.Compression
{
	public abstract class CompressionStream : Stream
	{
		public static byte[] Compress(CompressionMethod method, byte[] inputData)
		{
			byte[] result;
			if (method != CompressionMethod.None)
			{
				switch (method)
				{
					case CompressionMethod.Deflate:
					{
                        CompressionModule module = new Modules.Deflate.DeflateCompressionModule();
						result = module.Compress(inputData);
						return result;
					}
					case CompressionMethod.Bzip2:
                    {
                        Bzip2.Bzip2CompressionModule module = new Bzip2.Bzip2CompressionModule();
                        return module.Compress(inputData);
					}
					case CompressionMethod.Gzip:
					{
						result = Gzip.GzipStream.Compress(inputData);
						return result;
                    }
                    case CompressionMethod.Zlib:
                    {
                        result = Zlib.ZlibStream.Compress(inputData);
                        return result;
                    }
					case CompressionMethod.LZSS:
					{
						result = LZSS.LZSSStream.Compress(inputData);
						return result;
					}
                    case CompressionMethod.LZH:
                    {
                        result = LZH.LZHStream.Compress(inputData);
                        return result;
                    }
					case CompressionMethod.LZW:
					{
						result = LZW.LZWStream.Compress(inputData);
						return result;
                    }
				}
				throw new NotImplementedException();
			}
			result = inputData;
			return result;
		}
		public static byte[] Decompress(CompressionMethod method, byte[] inputData)
		{
			byte[] result;
			if (method != CompressionMethod.None)
			{
				switch (method)
				{
					case CompressionMethod.Deflate:
					{
                        Modules.Deflate.DeflateCompressionModule module = new Modules.Deflate.DeflateCompressionModule();
                        result = module.Decompress(inputData);
						return result;
					}
                    case CompressionMethod.Bzip2:
                    {
                        Bzip2.Bzip2CompressionModule module = new Bzip2.Bzip2CompressionModule();
                        return module.Decompress(inputData);
                    }
					case CompressionMethod.Gzip:
					{
						result = Gzip.GzipStream.Decompress(inputData);
						return result;
					}
                    case CompressionMethod.Zlib:
                    {
                        result = Zlib.ZlibStream.Decompress(inputData);
                        return result;
                    }
					case CompressionMethod.LZSS:
                    {
                        result = LZSS.LZSS2.LZSS.Decompress(inputData);
                        return result;
                    }
                    case CompressionMethod.LZH:
                    {
                        result = LZH.LZHStream.Decompress(inputData);
                        return result;
                    }
					case CompressionMethod.LZW:
					{
						result = LZW.LZWStream.Decompress(inputData);
						return result;
					}
				}
				throw new NotImplementedException();
			}
			result = inputData;
			return result;
		}
		public static byte[] Decompress(CompressionMethod method, byte[] inputData, uint outputLength)
		{
			byte[] result;
			if (method != CompressionMethod.None)
			{
				switch (method)
				{
					case CompressionMethod.Deflate:
					{
                        Modules.Deflate.DeflateCompressionModule module = new Modules.Deflate.DeflateCompressionModule();
                        result = module.Decompress(inputData);
						return result;
					}
					case CompressionMethod.Gzip:
					{
						result = Gzip.GzipStream.Decompress(inputData, 0, (int)outputLength);
						return result;
					}
					case CompressionMethod.LZSS:
					{
						result = LZSS.LZSSStream.Decompress(inputData, outputLength);
						return result;
					}
					case CompressionMethod.LZW:
					{
						result = LZW.LZWStream.Decompress(inputData);
						return result;
					}
				}
				throw new NotImplementedException();
			}
			result = inputData;
			return result;
		}
	}
}
