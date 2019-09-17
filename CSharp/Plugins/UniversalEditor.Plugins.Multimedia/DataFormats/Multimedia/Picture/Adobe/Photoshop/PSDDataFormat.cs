//
//  PSDDataFormat.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop.Internal;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Adobe.Photoshop
{
	public class PSDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private PSDHeader ReadPSDHeader(Reader reader)
		{
			PSDHeader header = new PSDHeader();
			// Signature: always equal to '8BPS'.Do not try to read the file if the signature does not match this value.
			header.signature = reader.ReadFixedLengthString(4);
			if (header.signature != "8BPS") throw new InvalidDataFormatException();

			// Version: always equal to 1.Do not try to read the file if the version does not match this value. (**PSB * *version is 2.)
			header.version = reader.ReadInt16();
			if (header.version > 2) throw new InvalidDataFormatException();

			header.reserved = reader.ReadBytes(6); // Reserved: must be zero.

			header.channelCount = reader.ReadInt16(); // The number of channels in the image, including any alpha channels.Supported range is 1 to 56.

			header.imageHeight = reader.ReadInt32(); // The height of the image in pixels.Supported range is 1 to 30,000. (**PSB * *max of 300, 000.)
			header.imageWidth = reader.ReadInt32(); // The width of the image in pixels.Supported range is 1 to 30,000. (*PSB * *max of 300, 000)

			header.bitDepth = reader.ReadInt16(); // Depth: the number of bits per channel. Supported values are 1, 8, 16 and 32.
			header.colorMode = (PSDColorMode)reader.ReadInt16(); // The color mode of the file. Supported values are: Bitmap = 0; Grayscale = 1; Indexed = 2; RGB = 3; CMYK = 4; Multichannel = 7; Duotone = 8; Lab = 9.
			return header;
		}

		private PSDImageResourceBlock ReadPSDImageResourceBlock(Reader reader)
		{
			PSDImageResourceBlock block = new PSDImageResourceBlock();
			block.signature = reader.ReadFixedLengthString(4);
			if (block.signature != "8BIM") throw new InvalidDataFormatException("PSD image resource block does not begin with '8BIM'");
			block.id = reader.ReadInt16();
			block.name = reader.ReadNullTerminatedString();
			reader.Align(2);
			block.datalength = reader.ReadInt32();
			block.data = reader.ReadBytes(block.datalength);
			reader.Align(2);
			return block;
		}

		private List<PSDImageResourceBlock> mvarImageResourceBlocks = new List<PSDImageResourceBlock>();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader reader = base.Accessor.Reader;
			reader.Endianness = Endianness.BigEndian;

			PSDHeader header = ReadPSDHeader(reader);

			List<PSDLayer> layers = new List<PSDLayer>();

			int colorModeDataSectionLength = reader.ReadInt32();
			// Indexed color images: length is 768; color data contains the color table for the image, in non-interleaved order.
			// Duotone images: color data contains the duotone specification(the format of which is not documented). Other applications that read Photoshop files can treat a duotone image as a gray image, and just preserve the contents of the duotone information when reading and writing the file.
			reader.Seek(colorModeDataSectionLength, SeekOrigin.Current);

			int imageResourceSectionLength = reader.ReadInt32();
			long end = reader.Accessor.Position + imageResourceSectionLength;
			while (reader.Accessor.Position < end)
			{
				PSDImageResourceBlock block = ReadPSDImageResourceBlock(reader);
				Reader br = new Reader(new MemoryAccessor(block.data));
				if (block.KnownId == PSDKnownImageResourceBlockId.PSD4GridGuidesInformation)
				{
					// shop stores grid and guides information for an image in an image
					// resource block. Each of these resource blocks consists of an initial 16 - byte
					// grid and guide header, which is always present, followed by 5 - byte blocks of
					// specific guide information for guide direction and location, which are present
					// if there are guides(fGuideCount > 0)
					int version = br.ReadInt32();
					int horizontalGridSize = br.ReadInt32();
					int verticalGridSize = br.ReadInt32();
					int fGuideCount = br.ReadInt32(); // number of guide resource blocks (can be 0)
					for (int i = 0; i < fGuideCount; i++)
					{
						// Location of guide in document coordinates. Since the guide is
						// either vertical or horizontal, this only has to be one component of
						// the coordinate.
						int guideLocation = br.ReadInt32();
						// Direction of guide. VHSelect is a system type of unsigned char where
						// 0 = vertical, 1 = horizontal.
						int guideDirection = br.ReadInt32();

						// Grid and guide information may be modified using the Property suite. See the
						// Callbacks chapter in Photoshop API Guide.pdf for more information.
					}
				}
				else if (block.KnownId == PSDKnownImageResourceBlockId.PSD4ThumbnailResource || block.KnownId == PSDKnownImageResourceBlockId.PSD5ThumbnailResource)
				{
					// Adobe Photoshop (version 5.0 and later) stores thumbnail information for
					// preview display in an image resource block that consists of an initial 28 - byte
					// header, followed by a JFIF thumbnail in RGB(red, green, blue) order for both
					// Macintosh and Windows.

					// Adobe Photoshop 4.0 stored the thumbnail information in the same format
					// except the data section is BGR (blue, green, red).The 4.0 format is at
					// resource ID 1033 and the 5.0 format is at resource ID 1036.

					// Format. 1 = kJpegRGB . Also supports kRawRGB (0).
					int format = br.ReadInt32();
					// Width of thumbnail in pixels.
					int thumbnailWidth = br.ReadInt32();
					// Height of thumbnail in pixels.
					int thumbnailHeight = br.ReadInt32();
					// Widthbytes: Padded row bytes = (width * bits per pixel + 31) / 32 * 4.
					int thumbnailWidthBytes = br.ReadInt32();
					// Total size = widthbytes * height * planes
					int totalSize = br.ReadInt32();
					// Size after compression. Used for consistency check.
					int sizeAfterCompression = br.ReadInt32();
					// Bits per pixel. = 24
					short bitsPerPixel = br.ReadInt16();
					// Number of planes. = 1
					short planeCount = br.ReadInt16();

					// JFIF data in RGB format.
					// For resource ID 1033 the data is in BGR format.
					if (block.KnownId == PSDKnownImageResourceBlockId.PSD4ThumbnailResource)
					{

					}
					else if (block.KnownId == PSDKnownImageResourceBlockId.PSD5ThumbnailResource)
					{

					}
				}
				else if (block.KnownId == PSDKnownImageResourceBlockId.ColorSamplersPSD5 || block.KnownId == PSDKnownImageResourceBlockId.ColorSamplersCS3)
				{
					// Adobe Photoshop(version 5.0 and later) stores color samplers information for
					// an image in an image resource block that consists of an initial 8 - byte color
					// samplers header followed by a variable length block of specific color samplers
					// information.
					int version = br.ReadInt32();
					int colorSamplerCount = br.ReadInt32();
					for (int i = 0; i < colorSamplerCount; i++)
					{
						// Version of color samplers, 1 for version 3. ( Version 3 only ) .
						int colorSamplerVersion = br.ReadInt32();

						// The horizontal and vertical position of the point (4 bytes each).
						// Version 1 is a fixed value.Version 2 is a float value.
						float horizontalPosition = 0.0f;  // br.ReadInt32();
						float verticalPosition = 0.0f;  // br.ReadInt32();
						if (version == 1)
						{
							horizontalPosition = br.ReadInt32();
							verticalPosition = br.ReadInt32();
						}
						else if (version >= 2)
						{
							horizontalPosition = br.ReadSingle();
							verticalPosition = br.ReadSingle();
						}

						PSDColorSamplerColorSpace colorSpace = (PSDColorSamplerColorSpace)br.ReadInt16();
						short depth = br.ReadInt16();
					}
				}
				else if (block.KnownId >= PSDKnownImageResourceBlockId.PathInformationMaskBegin && block.KnownId <= PSDKnownImageResourceBlockId.PathInformationMaskEnd)
				{
					// Path resource format
					// Photoshop stores the paths saved with an image in an image resource block.
					// These resource blocks consist of a series of 26 - byte path point records, so the
					// resource length should always be a multiple of 26.
					// Photoshop stores its paths as resources of type 8BIM , with IDs in the range
					// 2000 through 2997.These numbers should be reserved for Photoshop.The
					// name of the resource is the name given to the path when it was saved.
					// If the file contains a resource of type 8BIM with an ID of 2999, then this
					// resource contains a Pascal - style string containing the name of the clipping
					// path to use with this image when saving it as an EPS file. 4 byte fixed value
					// for flatness and 2 byte fill rule. 0 = same fill rule, 1 = even odd fill rule, 2 =
					// non zero winding fill rule.The fill rule is ignored by Photoshop.
					// The path format returned by GetProperty() call is identical to what is described
					// below. Refer to the IllustratorExport sample plug -in code to see how this
					// resource data is constructed.

					// Path points
					// All points used in defining a path are stored in eight bytes as a pair of 32 - bit
					// components, vertical component first.
					// The two components are signed, fixed point numbers with 8 bits before the
					// binary point and 24 bits after the binary point.Three guard bits are reserved
					// in the points to eliminate most concerns over arithmetic overflow.Hence, the
					// range for each component is 0xF0000000 to 0x0FFFFFFF representing a range
					// of - 16 to 16.The lower bound is included, but not the upper bound.
					// This limited range is used because the points are expressed relative to the
					// image size.The vertical component is given with respect to the image height,
					// and the horizontal component is given with respect to the image width. [0, 0]
					// represents the top - left corner of the image; [ 1,1 ]
					// 		([0x01000000, 0x01000000])
					//  represents the bottom-right.
					// In Windows, the byte order of the path point components are reversed; you
					// should swap the bytes when accessing each 32 - bit value.

					// Path records
					// The data in a path resource consists of one or more 26-byte records. The first
					// two bytes of each record is a selector to indicate what kind of path it is.For
					// Windows, you should swap the bytes before accessing it as a short.

					PSDPathType type = (PSDPathType)br.ReadInt16();

					// The first 26 - byte path record contains a selector value of 6, path fill rule
					// record.The remaining 24 bytes of the first record are zeroes.Paths use
					// even / odd ruling.Subpath length records, selector value 0 or 3, contain the
					// number of Bezier knot records in bytes 2 and 3.The remaining 22 bytes are
					// unused, and should be zeroes. Each length record is then immediately
					// followed by the Bezier knot records describing the knots of the subpath.
					// In Bezier knot records, the 24 bytes following the selector field contain three
					// path points(described above) for:
					// the control point for the Bezier segment preceding the knot,
					// the anchor point for the knot, and
					// the control point for the Bezier segment leaving the knot.
					// Linked knots have their control points linked.Editing one point modifies the
					// other to preserve collinearity.Knots should only be marked as having linked
					// controls if their control points are collinear with their anchor.The control
					// points on unlinked knots are independent of each other.Refer to the Adobe
					// Photoshop User Guide for more information.
					// Clipboard records, selector = 7, contain four fixed-point numbers for the
					//  bounding rectangle(top, left, bottom, right), and a single fixed-point number
					// indicating the resolution.
					// Initial fill records, selector = 8 , contain one two byte record. A value of 1
				}
				else if (block.KnownId == PSDKnownImageResourceBlockId.Slices)
				{
					// Slices resource format
					// Adobe Photoshop 6.0 stores slices information for an image in an image
					// resource block.
					// Adobe Photoshop 7.0 added a descriptor at the end of the block for the
					// individual slice info.
					// Adobe Photoshop CS and later changed to version 7 or 8 and uses a
					// Descriptor to defined the Slices data.
					int version = br.ReadInt32();

					if (version == 6)
					{
						// Bounding rectangle for all of the slices: top, left, bottom, right of all
						// the slices
						int boundingRectangleTop = br.ReadInt32();
						int boundingRectangleLeft = br.ReadInt32();
						int boundingRectangleBottom = br.ReadInt32();
						int boundingRectangleRight = br.ReadInt32();

						// Name of group of slices: Unicode string
						string groupName = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);

						// Number of slices to follow. See Slices resource block in the next
						// table.
						int sliceCount = br.ReadInt32();

						for (int i = 0; i < sliceCount; i++)
						{
							int sliceID = br.ReadInt32();
							int groupID = br.ReadInt32();
							int origin = br.ReadInt32();
							if (origin == 1)
							{
								int associatedLayerId = br.ReadInt32();
							}
							string sliceName = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);
							int sliceType = br.ReadInt32();

							int sliceLeft = br.ReadInt32();
							int sliceTop = br.ReadInt32();
							int sliceRight = br.ReadInt32();
							int sliceBottom = br.ReadInt32();

							string url = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);
							string target = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);
							string message = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);
							string altText = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);

							bool cellTextIsHTML = br.ReadBoolean();
							string cellText = br.ReadNullTerminatedString(Encoding.UTF16BigEndian);
							int horizontalAlignment = br.ReadInt32();
							int verticalAlignment = br.ReadInt32();

							byte alpha = br.ReadByte();
							byte red = br.ReadByte();
							byte green = br.ReadByte();
							byte blue = br.ReadByte();

							// Additional data as length allows. See comment above.

							int descriptorVersion = br.ReadInt32();
							// descriptor...
						}
					}
					else if (version >= 7)
					{
						// Descriptor version ( = 16 for Photoshop 6.0).
						int descriptorVersion = br.ReadInt32();
					}
				}
				else if (block.KnownId == PSDKnownImageResourceBlockId.VanishingPointMacintosh || block.KnownId == PSDKnownImageResourceBlockId.VanishingPointWindows)
				{
					/*
					version = 101
					number of relations to follow.
					-- for each relation--
					grid resolution for the root plane
					number of planes to follow
					-- for each plane in calibration order--
					ID of the plane
					ID of the plane that calibrates this plane 0 if none
										-- for 4 rays--
					origin position of the ray.Point
					VP location - must be consistent across all planes in the relation unless it is an
					endpoint. Point
					true if the VP location is an endpoint
					ID that this ray points at.
					Ray DI(see below)
					++++++++++++++++++++
					I / O appendix
					Point - two doubles; h endl, v endl
					VPID - int(enum value) 0,1,2 identifing 1 of 3 possible VPs
					RayID - 1, One of the primary rays directly connected to the shared origin
					3, a non-primary ray parallel to 7
					5, a non-primary ray parallel to 1
					7, One of the primary rays directly connected to the shared origin.
					*/
				}
				mvarImageResourceBlocks.Add(block);
			}

			long layerAndMaskSectionLength = (header.version >= 2 ? reader.ReadInt64() : reader.ReadInt32());

			// layer info
			long layerInfoLength = (header.version >= 2 ? reader.ReadInt64() : reader.ReadInt32());

			short layerCount = reader.ReadInt16();
			if (layerCount < 0)
			{
				// If it is a negative number, its absolute value is the
				// number of layers and the first alpha channel contains the
				// transparency data for the merged result.
				layerCount = Math.Abs(layerCount);
			}
			for (short i = 0; i < layerCount; i++)
			{
				PSDLayer layer = new PSDLayer();
				int layerTop = reader.ReadInt32();
				int layerLeft = reader.ReadInt32();
				int layerBottom = reader.ReadInt32();
				int layerRight = reader.ReadInt32();
				layer.Rectangle = new Rectangle(layerLeft, layerTop, layerRight - layerLeft, layerBottom - layerTop);

				layer.ChannelCount = reader.ReadInt16();
				for (short j = 0; j < layer.ChannelCount; j++)
				{
					short channelID = reader.ReadInt16(); // 0 = red, 1 = green, etc.
														  // -1 = transparency mask; -2 = user supplied layer mask, -3 real
														  // user supplied layer mask(when both a user mask and a vector
														  // mask are present)

					long channelDataLength = (header.version >= 2 ? reader.ReadInt64() : reader.ReadInt32());
					// channel data...
				}

				string blendModeSignature = reader.ReadFixedLengthString(4); // 8BIM
				layer.BlendMode = (PSDBlendModeKey)reader.ReadInt32();
				layer.Opacity = reader.ReadByte(); // 0...255
				layer.ClippingMode = reader.ReadByte(); // 0=base, 1=non-base
				layer.Flags = (PSDLayerFlags)reader.ReadByte();
				byte zero = reader.ReadByte(); // filler
				int extraDataFieldLength = reader.ReadInt32(); // the total length of the next five fields

				// layer mask data
				int layerMaskDataSize = reader.ReadInt32();
				if (layerMaskDataSize > 0)
				{
					// Rectangle enclosing layer mask: Top, left, bottom, right
					int enclosingLayerMaskTop = reader.ReadInt32();
					int enclosingLayerMaskLeft = reader.ReadInt32();
					int enclosingLayerMaskBottom = reader.ReadInt32();
					int enclosingLayerMaskRight = reader.ReadInt32();

					byte defaultColor = reader.ReadByte(); // 0 or 255
					PSDLayerMaskFlags maskFlags = (PSDLayerMaskFlags) reader.ReadByte();

					if ((maskFlags & PSDLayerMaskFlags.ContainsParameter) == PSDLayerMaskFlags.ContainsParameter)
					{
						PSDLayerMaskParameterFlags paramFlags = (PSDLayerMaskParameterFlags)reader.ReadByte();
					}

					if (layerMaskDataSize == 20)
					{
						short padding = reader.ReadInt16();
					}
					else
					{
						PSDLayerMaskParameterFlags realFlags = (PSDLayerMaskParameterFlags)reader.ReadByte();
						byte realUserMaskBackground = reader.ReadByte(); // 0 or 255

						int realEnclosingLayerMaskTop = reader.ReadInt32();
						int realEnclosingLayerMaskLeft = reader.ReadInt32();
						int realEnclosingLayerMaskBottom = reader.ReadInt32();
						int realEnclosingLayerMaskRight = reader.ReadInt32();
					}
				}

				// blending ranges
				int layerBlendingRangesDataSize = reader.ReadInt32();
				if (layerBlendingRangesDataSize > 0)
				{
					// Composite gray blend source. Contains 2 black values followed by 2
					// white values. Present but irrelevant for Lab & Grayscale.
					byte compositeGrayBlendSourceBlack1 = reader.ReadByte();
					byte compositeGrayBlendSourceBlack2 = reader.ReadByte();
					byte compositeGrayBlendSourceWhite1 = reader.ReadByte();
					byte compositeGrayBlendSourceWhite2 = reader.ReadByte();

					// Composite gray blend destination range
					int compositeGrayBlendDestinationRange = reader.ReadInt32();

					for (short j = 0; j > layer.ChannelCount; j++)
					{
						int channelSourceRange = reader.ReadInt32();
						int channelDestinationRange = reader.ReadInt32();
					}
				}

				// layer name padded to 4 bytes
				layer.Name = reader.ReadNullTerminatedString();
				reader.Align(4);
				layers.Add(layer);
			}
			for (int i = 0; i < layerCount; i++)
			{
				PSDLayer layer = layers[i];
				// Image data.
				PSDLayerCompressionMode compressionMode = (PSDLayerCompressionMode) reader.ReadInt16();
				switch (compressionMode)
				{
					case PSDLayerCompressionMode.Raw:
					{
						// If the compression code is 0, the image data is just the raw image
						// data, whose size is calculated as: (from the first field in See Layer records).
						int dataSize = (int)((layer.Rectangle.Bottom - layer.Rectangle.Y) * (layer.Rectangle.Right - layer.Rectangle.X));

						break;
					}
					case PSDLayerCompressionMode.RLE:
					{
						// If the compression code is 1, the image data starts with the byte
						// counts for all the scan lines in the channel(LayerBottom - LayerTop),
						// with each count stored as a two - byte value.(**PSB * *each count
						// stored as a four - byte value.) The RLE compressed data follows,
						// with each scan line compressed separately.The RLE compression is
						// the same compression algorithm used by the Macintosh ROM
						// routine PackBits, and the TIFF standard.
						break;
					}
				}
				// If the layer's size, and therefore the data, is odd, a pad byte will be
				// inserted at the end of the row.
				// If the layer is an adjustment layer, the channel data is undefined
				// (probably all white.)
			}

			// global layer mask info
			int globalLayerMaskInfoSectionLength = reader.ReadInt32();
			short globalOverlayColorSpace = reader.ReadInt16();
			short[] globalColorComponents = reader.ReadInt16Array(4);
			short globalOpacity = reader.ReadInt16(); // 0 = transparent, 100 = opaque
			byte globalKind = reader.ReadByte();
			// Kind. 0 = Color selected--i.e. inverted; 1 = Color protected;128 =
			// use value stored per layer.This value is preferred.The others are
			// for backward compatibility with beta versions.

			// tagged blocks (PSB4.0 >)


		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
