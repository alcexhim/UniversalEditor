//
//  ExpressionDesignDataFormat.cs - provides a DataFormat for manipulating vector images in Microsoft Expression Design format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.ExpressionDesign
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating vector images in Microsoft Expression Design format.
	/// </summary>
	public class ExpressionDesignDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
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
