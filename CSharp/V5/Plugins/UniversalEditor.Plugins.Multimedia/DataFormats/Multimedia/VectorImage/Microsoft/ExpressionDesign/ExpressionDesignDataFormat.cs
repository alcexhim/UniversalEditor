using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.ExpressionDesign
{
	public class ExpressionDesignDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(VectorImageObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			VectorImageObjectModel vector = (objectModel as VectorImageObjectModel);
			if (vector == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			string signature = reader.ReadFixedLengthString(9);
			if (signature != "<XDFV:9>\n") throw new InvalidDataFormatException();

			reader.Accessor.Seek(32, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadToEnd();

			System.IO.File.WriteAllBytes(@"C:\Users\Mike Becker\Documents\Expression\Expression Design\Untitled1.compressed", compressedData);

			UniversalEditor.Compression.CompressionModule module = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Zlib);
			byte[] decompressedData = module.Decompress(compressedData);

			System.IO.File.WriteAllBytes(@"C:\Users\Mike Becker\Documents\Expression\Expression Design\Untitled1.decompressed", decompressedData);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
