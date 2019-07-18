using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.ObjectModels.FileSystem.FileSources
{
	public class CompressedEmbeddedFileSource : FileSource
	{
		public Reader Reader { get; private set; }
		public long Offset { get; private set; }
		public long DecompressedLength { get; private set; }
		public long CompressedLength { get; private set; }

		public override byte[] GetData(long offset, long length)
		{
			Reader.Seek(Offset, SeekOrigin.Begin);
			byte[] sourceData = Reader.ReadBytes(DecompressedLength);

			long realLength = Math.Min(length, sourceData.Length);
			byte[] data = new byte[realLength];
			Array.Copy(sourceData, 0, data, 0, realLength);
			return data;
		}

		public override long GetLength()
		{
			return DecompressedLength;
		}

		public CompressedEmbeddedFileSource(Reader reader, long offset, long decompressedLength, long compressedLength, FileSourceTransformation[] transformations)
		{
			Reader = reader;
			Offset = offset;
			DecompressedLength = decompressedLength;
			CompressedLength = compressedLength;

			Contract.Assert(!(transformations.Length < 0 && decompressedLength != compressedLength), "Must provide a decompression transformation");

			foreach (FileSourceTransformation transformation in transformations)
			{
				base.Transformations.Add(transformation);
			}
		}
	}
}
