//
//  TIFFTag.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public enum TIFFTag
	{
		/// <summary>
		/// SHORT or LONG. The number of columns in the image, i.e., the number of pixels per scanline.
		/// </summary>
		ImageWidth = 256,
		/// <summary>
		/// SHORT or LONG. The number of rows (sometimes described as scanlines) in the image.
		/// </summary>
		ImageLength = 257,
		/// <summary>
		/// SHORT. The number of bits per component. Allowable values for Baseline TIFF grayscale images are 4 and 8, allowing either 16 or 256 distinct shades of gray.
		/// </summary>
		BitsPerSample = 258,
		/// <summary>
		/// SHORT. See <see cref="TIFFCompression" />.
		/// </summary>
		Compression = 259,
		/// <summary>
		/// SHORT. See <see cref="TIFFPhotometricInterpretation" />.
		/// </summary>
		PhotometricInterpretation = 262,

		/// <summary>
		/// SHORT. The width of the dithering or halftoning matrix used to create a dithered or halftoned bilevel file.
		/// </summary>
		CellWidth = 264,
		/// <summary>
		/// SHORT. The length of the dithering or halftoning matrix used to create a dithered or halftoned bilevel file. This field should only be present if
		/// <see cref="Threshholding"/> = 2.
		/// </summary>
		CellLength = 265,

		/// <summary>
		/// SHORT. The logical order of bits within a byte. See <see cref="TIFFFillOrder" />. Default is <see cref="TIFFFillOrder.Normal" />.
		/// </summary>
		FillOrder = 266,

		/// <summary>
		/// ASCII. The name of the document from which this image was scanned.
		/// </summary>
		/// <seealso cref="PageName" />
		DocumentName = 269,

		/// <summary>
		/// SHORT or LONG. For each strip, the byte offset of that strip.
		/// </summary>
		StripOffsets = 273,
		/// <summary>
		/// SHORT. The orientation of the image with respect to the rows and columns.
		/// </summary>
		Orientation = 274,
		/// <summary>
		/// SHORT. The number of components per pixel. This number is 3 for RGB images, unless extra samples are present. See the ExtraSamples field for further information.
		/// </summary>
		SamplesPerPixel = 277,
		/// <summary>
		/// SHORT or LONG. The number of rows in each strip (except possibly the last strip.) For example, if ImageLength is 24, and RowsPerStrip is 10, then there are 3
		/// strips, with 10 rows in the first strip, 10 rows in the second strip, and 4 rows in the third strip. (The data in the last strip is not padded with 6 extra rows
		/// of dummy data.)
		/// </summary>
		RowsPerStrip = 278,
		/// <summary>
		/// SHORT or LONG. For each strip, the number of bytes in that strip AFTER ANY COMPRESSION.
		/// </summary>
		StripByteCounts = 279,
		/// <summary>
		/// RATIONAL. The number of pixels per ResolutionUnit in the ImageWidth (typically, horizontal - see Orientation) direction.
		/// </summary>
		XResolution = 282,
		/// <summary>
		/// RATIONAL. The number of pixels per ResolutionUnit in the ImageLength (typically, vertical) direction.
		/// </summary>
		YResolution = 283,
		/// <summary>
		/// SHORT. How the components of each pixel are stored.
		/// </summary>
		PlanarConfiguration = 284,
		/// <summary>
		/// ASCII. The name of the page from which this image was scanned.
		/// </summary>
		/// <seealso cref="DocumentName" />
		PageName = 285,

		/// <summary>
		/// RATIONAL. X position of the image, i.e. the X offset in ResolutionUnits of the left side of the image, with respect to the left side of the page.
		/// </summary>
		/// <seealso cref="YPosition" />
		XPosition = 286,
		/// <summary>
		/// RATIONAL. Y position of the image, i.e. the Y offset in ResolutionUnits of the top of the image, with respect to the top of the page. In the TIFF coordinate scheme,
		/// the positive Y direction is down, so that <see cref="YPosition" /> is always positive.
		/// </summary>
		/// <seealso cref="XPosition" />
		YPosition = 287,

		/// <summary>
		/// SHORT. See <see cref="TIFFResolutionUnit" />. Default = 2 (<see cref="TIFFResolutionUnit.Inch" />).
		/// </summary>
		ResolutionUnit = 296,
		/// <summary>
		/// SHORT. The page number of the page from which this image was scanned. This field is used to specify page numbers of a multiple page (e.g. facsimile) document.
		/// PageNumber[0] is the page number; PageNumber[1] is the total number of pages in the document. If PageNumber[1] is 0, the total number of pages in the document is
		/// not available. Pages need not appear in numerical order. The first page is numbered 0 (zero).
		/// </summary>
		PageNumber = 297,

		/// <summary>
		/// ASCII. Name and version number of the software package(s) used to create the image.
		/// </summary>
		/// <seealso cref="Make" />
		/// <seealso cref="Model" />
		Software = 305,
		/// <summary>
		/// ASCII. Date and time of image creation. The format is: “YYYY:MM:DD HH:MM:SS”, with hours like those on a 24-hour clock, and one space character between the date and
		/// the time. The length of the string, including the terminating NUL, is 20 bytes.
		/// </summary>
		DateTime = 306,

		/// <summary>
		/// SHORT. Description of extra components. Specifies that each pixel has m extra components whose interpretation is defined by one of the values listed below.
		/// When this field is used, the SamplesPerPixel field has a value greater than the PhotometricInterpretation field suggests. For example, full-color RGB data normally
		/// has SamplesPerPixel = 3. If SamplesPerPixel is greater than 3, then the ExtraSamples field describes the meaning of the extra samples. If SamplesPerPixel is, say, 5
		/// then ExtraSamples will contain 2 values, one for each extra sample. ExtraSamples is typically used to include non-color information, such as opacity, in an image.
		/// The possible values for each item in the field's value are defined by <see cref="TIFFExtraSamplesType" />. By convention, extra components that are present must be
		/// stored as the "last components" in each pixel. For example, if SamplesPerPixel is 4 and there is 1 extra component, then it is located in the last component location
		/// (SamplesPerPixel-1) in each pixel. Components designated as "extra" are just like other components in a pixel. In particular, the size of such components is defined
		/// by the value of the <see cref="BitsPerSample" /> field. With the introduction of this field, TIFF readers must not assume a particular <see cref="SamplesPerPixel" />
		/// value based on the value of the <see cref="PhotometricInterpretation" /> field. For example, if the file is an RGB file, <see cref="SamplesPerPixel" /> may be
		/// greater than 3. The default is no extra samples. This field must be present if there are extra samples.
		/// </summary>
		/// <seealso cref="SamplesPerPixel" />
		/// <seealso cref="AssociatedAlpha" />
		ExtraSamples = 338,
		/// <summary>
		/// SHORT[SamplesPerPixel]. This field specifies how to interpret each data sample in a pixel. Possible values are defined in <see cref="TIFFSampleFormat" />.
		/// </summary>
		SampleFormat = 339,

		/// <summary>
		/// ASCII. Person who created the image.
		/// </summary>
		Artist = 315,

		/// <summary>
		/// SHORT. This field defines a Red-Green-Blue color map (often called a lookup table) for palette color images. In a palette-color image, a pixel value is used to index
		/// into an RGB-lookup table. For example, a palette-color pixel having a value of 0 would be displayed according to the 0th Red, Green, Blue triplet. In a TIFF ColorMap,
		/// all the Red values come first, followed by the Green values, then the Blue values. In the ColorMap, black is represented by 0,0,0 and white is represented by 65535,
		/// 65535, 65535.
		/// </summary>
		ColorMap = 320,

		XMPMetadata = 700,

		/// <summary>
		/// ASCII. Copyright notice of the person or organization that claims the copyright to the image. The complete copyright statement should be listed in this field
		/// including any dates and statements of claims. For example, "Copyright, John Smith, 19xx. All rights reserved."
		/// </summary>
		Copyright = 33432,

		ICCProfile = 34675
	}
}
