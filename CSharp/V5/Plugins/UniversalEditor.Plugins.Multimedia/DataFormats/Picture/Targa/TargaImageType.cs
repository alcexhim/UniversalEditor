using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.Targa
{
	/// <summary>
	/// The type of image read from the file.
	/// </summary>
	public enum TargaImageType
	{
        /// <summary>
        /// No image data was found in file.
        /// </summary>
        None = 0,
        /// <summary>
        /// Image is an uncompressed, indexed color-mapped image.
        /// </summary>
        UncompressedIndexed = 1,
        /// <summary>
        /// Image is an uncompressed, RGB image.
        /// </summary>
        UncompressedTrueColor = 2,
        /// <summary>
        /// Image is an uncompressed, grayscale image.
        /// </summary>
        UncompressedGrayscale = 3,
        /// <summary>
        /// Image is a compressed, indexed color-mapped image.
        /// </summary>
        CompressedIndexed = 9,
        /// <summary>
        /// Image is a compressed, RGB image.
        /// </summary>
		CompressedTrueColor = 10,
        /// <summary>
        /// Image is a compressed, grayscale image.
        /// </summary>
		CompressedGrayscale = 11
	}
}
