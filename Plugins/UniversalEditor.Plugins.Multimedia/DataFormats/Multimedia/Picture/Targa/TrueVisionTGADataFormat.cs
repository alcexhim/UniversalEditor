//
//  TrueVisionTGADataFormat.cs - implements a DataFormat for manipulating images in TrueVision Targa (TGA) format
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
using System.Collections.Generic;

using MBS.Framework.Drawing;
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
	/// <summary>
	/// Implements a <see cref="DataFormat" /> for manipulating images in TrueVision Targa (TGA) format.
	/// </summary>
	public class TrueVisionTGADataFormat : DataFormat
	{
		private const int HEADER_BYTE_LENGTH = 18;
		private const int FOOTER_BYTE_LENGTH = 26;

		private const int EXTENSION_AREA_AUTHOR_NAME_LENGTH = 41;
		private const int EXTENSION_AREA_AUTHOR_COMMENTS_LENGTH = 324;
		private const int EXTENSION_AREA_JOB_NAME_LENGTH = 41;
		private const int EXTENSION_AREA_SOFTWARE_ID_LENGTH = 41;
		private const int EXTENSION_AREA_SOFTWARE_VERSION_LETTER_LENGTH = 1;
		private const int EXTENSION_AREA_COLOR_CORRECTION_TABLE_VALUE_LENGTH = 41;

		public TargaExtensionArea ExtensionArea { get; } = new TargaExtensionArea();
		public TargaFirstPixelDestination PixelOrigin { get; } = TargaFirstPixelDestination.TopLeft;
		public TargaFormatVersion FormatVersion { get; set; } = TargaFormatVersion.Original;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(ImageType), "Image _type", TargaImageType.UncompressedTrueColor, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("CompressedGrayscale", "Compressed grayscale", TargaImageType.CompressedGrayscale),
					new ChoiceSetting.ChoiceSettingValue("CompressedIndexed", "Compressed indexed", TargaImageType.CompressedIndexed),
					new ChoiceSetting.ChoiceSettingValue("CompressedTrueColor", "Compressed true color", TargaImageType.CompressedTrueColor),
					new ChoiceSetting.ChoiceSettingValue("UncompressedGrayscale", "Uncompressed grayscale", TargaImageType.UncompressedGrayscale),
					new ChoiceSetting.ChoiceSettingValue("UncompressedIndexed", "Uncompressed indexed", TargaImageType.UncompressedIndexed),
					new ChoiceSetting.ChoiceSettingValue("UncompressedTrueColor", "Uncompressed true color", TargaImageType.UncompressedTrueColor)
				}));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(PixelDepth), "Pixel _depth", (byte)32, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("8bpp", "8bpp", (byte)8),
					new ChoiceSetting.ChoiceSettingValue("16bpp", "16bpp", (byte)16),
					new ChoiceSetting.ChoiceSettingValue("24bpp", "24bpp", (byte)24),
					new ChoiceSetting.ChoiceSettingValue("32bpp", "32bpp", (byte)32)
				}));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(PixelOrigin), "Pixel o_rigin", TargaFirstPixelDestination.TopLeft, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("BottomLeft", "Bottom-left", TargaFirstPixelDestination.BottomLeft),
					new ChoiceSetting.ChoiceSettingValue("BottomRight", "Bottom-right", TargaFirstPixelDestination.BottomRight),
					new ChoiceSetting.ChoiceSettingValue("TopLeft", "Top-left", TargaFirstPixelDestination.TopLeft),
					new ChoiceSetting.ChoiceSettingValue("TopRightwa78qa67Y", "Top-right", TargaFirstPixelDestination.TopRight)
				}));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(FormatVersion), "Format _version", TargaFormatVersion.Original, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("Original", "Original (100)", TargaFormatVersion.Original),
					new ChoiceSetting.ChoiceSettingValue("TrueVisionXFile", "TrueVision-XFile (200)", TargaFormatVersion.TrueVisionXFile)
				}));
				_dfr.ContentTypes.Add("image/x-targa");
				_dfr.ContentTypes.Add("image/x-tga");
			}
			return _dfr;
		}

		private int GetImageDataOffset(int colorMapLength, byte imageIDLength, byte colorMapEntrySize)
		{
			// calculate the image data offset

			// start off with the number of bytes holding the header info.
			int intImageDataOffset = HEADER_BYTE_LENGTH;

			// add the Image ID length (could be variable)
			intImageDataOffset += imageIDLength;

			// determine the number of bytes for each Color Map entry
			int Bytes = 0;
			switch (colorMapEntrySize)
			{
				case 15:
				case 16:
				{
					Bytes = 2;
					break;
				}
				case 24:
				{
					Bytes = 3;
					break;
				}
				case 32:
				{
					Bytes = 4;
					break;
				}
			}

			// add the length of the color map
			intImageDataOffset += ((int)colorMapLength * (int)Bytes);

			// return result
			return intImageDataOffset;
		}
		public byte PixelDepth { get; set; } = 0;


		public int BytesPerPixel
		{
			get { return (int)(PixelDepth / 8); }
			set
			{
				if ((value * 8) > 255)
				{
					throw new ArgumentOutOfRangeException("Bytes per pixel exceeds maximum allowed bits per pixel depth of 255");
				}
				PixelDepth = (byte)(value * 8);
			}
		}

		public TargaImageType ImageType { get; set; } = TargaImageType.None;
		public string ImageID { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			short imageWidth = 0;
			short imageHeight = 0;
			int extensionAreaOffset = 0;
			int developerDirectoryOffset = 0;
			TargaVerticalTransferOrder verticalTransferOrder = TargaVerticalTransferOrder.Unknown;
			TargaHorizontalTransferOrder horizontalTransferOrder = TargaHorizontalTransferOrder.Unknown;
			int imageDataOffset = 0;

			if (br.Accessor.Length < 18) throw new InvalidDataFormatException("File must be at least 18 bytes");

			#region Targa Footer
			{
				// set the cursor at the beginning of the signature string.
				br.Accessor.Seek((-1 * HEADER_BYTE_LENGTH), SeekOrigin.End);

				// read the signature bytes and convert to ASCII string
				string Signature = br.ReadNullTerminatedString(16);

				// do we have a proper signature
				if (Signature == "TRUEVISION-XFILE")
				{
					// this is a NEW targa file.
					FormatVersion = TargaFormatVersion.TrueVisionXFile;

					// set cursor to beginning of footer info
					br.Accessor.Seek((-1 * FOOTER_BYTE_LENGTH), SeekOrigin.End);

					// read the Extension Area Offset value
					extensionAreaOffset = br.ReadInt32();

					// read the Developer Directory Offset value
					developerDirectoryOffset = br.ReadInt32();

					// skip the signature we have already read it.
					string Signature2 = br.ReadNullTerminatedString(16);

					// set all values to our TargaFooter class
					// this.objTargaFooter.SetExtensionAreaOffset(ExtOffset);
					// this.objTargaFooter.SetDeveloperDirectoryOffset(DevDirOff);
					// this.objTargaFooter.SetSignature(Signature);
					// this.objTargaFooter.SetReservedCharacter(ResChar);
				}
				else
				{
					// this is not an ORIGINAL targa file.
					FormatVersion = TargaFormatVersion.Original;
				}
			}
			#endregion
			#region Targa Header
			{
				// set the cursor at the beginning of the file.
				br.Accessor.Seek(0, SeekOrigin.Begin);

				// read the header properties from the file
				byte imageIDLength = br.ReadByte();
				bool colorMapEnabled = br.ReadBoolean();
				ImageType = (TargaImageType)br.ReadByte();

				short colorMapFirstEntryIndex = br.ReadInt16();
				short colorMapLength = br.ReadInt16();
				byte colorMapEntrySize = br.ReadByte();

				short originX = br.ReadInt16();
				short originY = br.ReadInt16();
				imageWidth = br.ReadInt16();
				imageHeight = br.ReadInt16();

				pic.Width = imageWidth;
				pic.Height = imageHeight;

				PixelDepth = br.ReadByte();
				switch (PixelDepth)
				{
					case 8:
					case 16:
					case 24:
					case 32:
					{
						break;
					}
					default:
					{
						throw new InvalidOperationException("Targa image file format only supports 8, 16, 24, or 32 bit pixel depths");
					}
				}


				byte ImageDescriptor = br.ReadByte();
				int attributeBits = ImageDescriptor.GetBits(0, 4);

				verticalTransferOrder = (TargaVerticalTransferOrder)ImageDescriptor.GetBits(5, 1);
				horizontalTransferOrder = (TargaHorizontalTransferOrder)ImageDescriptor.GetBits(4, 1);

				// load ImageID value if any
				if (imageIDLength > 0)
				{
					ImageID = br.ReadNullTerminatedString(imageIDLength);
				}

				imageDataOffset = GetImageDataOffset(colorMapLength, imageIDLength, colorMapEntrySize);

				#region Load Colormap
				{
					// load color map if it's included and/or needed
					// Only needed for UNCOMPRESSED_COLOR_MAPPED and RUN_LENGTH_ENCODED_COLOR_MAPPED
					// image types. If color map is included for other file types we can ignore it.
					if (colorMapEnabled)
					{
						if (ImageType == TargaImageType.UncompressedIndexed || ImageType == TargaImageType.CompressedIndexed)
						{
							if (colorMapLength > 0)
							{
								for (int i = 0; i < colorMapLength; i++)
								{
									pic.ColorMap.Add(DecodeColor(br, colorMapEntrySize));
								}
							}
							else
							{
								throw new InvalidOperationException("Image Type requires a Color Map and Color Map Length is zero.");
							}
						}
					}
					else
					{
						if (ImageType == TargaImageType.UncompressedIndexed || ImageType == TargaImageType.CompressedIndexed)
						{
							throw new InvalidOperationException("Indexed image type requires a colormap and there was not a colormap included in the file.");
						}
					}
				}
				#endregion
			}
			#endregion
			#region Targa extension area
			{
				// is there an Extension Area in file
				if (!br.EndOfStream && (extensionAreaOffset > 0))
				{
					ExtensionArea.Enabled = true;

					// set the cursor at the beginning of the Extension Area using ExtensionAreaOffset.
					br.Accessor.Seek(extensionAreaOffset, SeekOrigin.Begin);

					// load the extension area fields from the file

					short extensionAreaSize = br.ReadInt16();
					string authorName = br.ReadNullTerminatedString(EXTENSION_AREA_AUTHOR_NAME_LENGTH);
					string authorComments = br.ReadNullTerminatedString(EXTENSION_AREA_AUTHOR_COMMENTS_LENGTH);


					// get the date/time stamp of the file
					short iMonth = br.ReadInt16();
					short iDay = br.ReadInt16();
					short iYear = br.ReadInt16();
					short iHour = br.ReadInt16();
					short iMinute = br.ReadInt16();
					short iSecond = br.ReadInt16();
					DateTime dtstamp;
					string strStamp = iMonth.ToString() + @"/" + iDay.ToString() + @"/" + iYear.ToString() + @" ";
					strStamp += iHour.ToString() + @":" + iMinute.ToString() + @":" + iSecond.ToString();
					if (DateTime.TryParse(strStamp, out dtstamp) == true)
					{
						ExtensionArea.DateCreated = dtstamp;
					}


					string JobName = br.ReadNullTerminatedString(EXTENSION_AREA_JOB_NAME_LENGTH);


					// get the job time of the file
					iHour = br.ReadInt16();
					iMinute = br.ReadInt16();
					iSecond = br.ReadInt16();
					TimeSpan ts = new TimeSpan((int)iHour, (int)iMinute, (int)iSecond);
					ExtensionArea.JobTime = ts;

					string softwareID = br.ReadNullTerminatedString(EXTENSION_AREA_SOFTWARE_ID_LENGTH);
					ExtensionArea.SoftwareID = softwareID;


					// get the version number and letter from file
					short sVersionNumber = br.ReadInt16();
					float iVersionNumber = (float)sVersionNumber / 100.0F;

					string strVersionLetter = br.ReadNullTerminatedString(EXTENSION_AREA_SOFTWARE_VERSION_LETTER_LENGTH);

					ExtensionArea.VersionString = (iVersionNumber.ToString(@"F2") + strVersionLetter);


					// get the color key of the file
					int a = (int)br.ReadByte();
					int r = (int)br.ReadByte();
					int b = (int)br.ReadByte();
					int g = (int)br.ReadByte();
					ExtensionArea.ColorKey = (Color.FromRGBAInt32(r, g, b, a));


					ExtensionArea.PixelAspectRatioNumerator = (int)br.ReadInt16();
					ExtensionArea.PixelAspectRatioDenominator = (int)br.ReadInt16();
					ExtensionArea.GammaNumerator = (int)br.ReadInt16();
					ExtensionArea.GammaDenominator = (int)br.ReadInt16();

					int extensionAreaColorCorrectionOffset = br.ReadInt32();
					int extensionAreaPostageStampOffset = br.ReadInt32();
					int extensionAreaScanLineOffset = br.ReadInt32();
					ExtensionArea.AttributesType = (int)br.ReadByte();


					// load Scan Line Table from file if any
					if (extensionAreaScanLineOffset > 0)
					{
						br.Accessor.Seek(extensionAreaScanLineOffset, SeekOrigin.Begin);
						for (int i = 0; i < imageHeight; i++)
						{
							ExtensionArea.ScanLineTable.Add(br.ReadInt32());
						}
					}


					// load Color Correction Table from file if any
					if (extensionAreaColorCorrectionOffset > 0)
					{
						br.Accessor.Seek(extensionAreaColorCorrectionOffset, SeekOrigin.Begin);
						for (int i = 0; i < EXTENSION_AREA_COLOR_CORRECTION_TABLE_VALUE_LENGTH; i++)
						{
							a = (int)br.ReadInt16();
							r = (int)br.ReadInt16();
							b = (int)br.ReadInt16();
							g = (int)br.ReadInt16();
							ExtensionArea.ColorCorrectionTable.Add(Color.FromRGBAInt32(r, g, b, a));
						}
					}
				}
			}
			#endregion
			#region Targa image
			{
				//**************  NOTE  *******************
				// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
				// The stride refers to the number of bytes allocated for one scanline of the bitmap.
				// In your loop, you copy the pixels one scanline at a time and take into
				// consideration the amount of padding that occurs due to memory alignment.
				// calculate the stride, in bytes, of the image (32bit aligned width of each image row)
				int intStride = (((int)imageWidth * (int)PixelDepth + 31) & ~31) >> 3; // width in bytes

				// calculate the padding, in bytes, of the image
				// number of bytes to add to make each row a 32bit aligned row
				// padding in bytes
				int paddingLength = intStride - ((((int)imageWidth * (int)PixelDepth) + 7) / 8);

				// get the image data bytes
				byte[] bimagedata = null;

				#region Image Data Bytes
				{

					// read the image data into a byte array
					// take into account stride has to be a multiple of 4
					// use padding to make sure multiple of 4

					byte[] data = null;

					// padding bytes
					byte[] padding = new byte[paddingLength];
					System.IO.MemoryStream msData = null;

					// seek to the beginning of the image data using the ImageDataOffset value
					br.Accessor.Seek(imageDataOffset, SeekOrigin.Begin);


					// get the size in bytes of each row in the image
					int intImageRowByteSize = (int)pic.Width * ((int)BytesPerPixel);

					// get the size in bytes of the whole image
					int intImageByteSize = intImageRowByteSize * (int)pic.Height;

					List<List<byte>> rows = new List<List<byte>>();

					// is this a RLE compressed image type
					#region COMPRESSED
					if (ImageType == TargaImageType.CompressedGrayscale || ImageType == TargaImageType.CompressedIndexed || ImageType == TargaImageType.CompressedTrueColor)
					{
						// RLE Packet info
						byte bRLEPacket = 0;
						int intRLEPacketType = -1;
						int intRLEPixelCount = 0;
						byte[] bRunLengthPixel = null;

						// used to keep track of bytes read
						int intImageBytesRead = 0;
						int intImageRowBytesRead = 0;

						List<byte> row = new List<byte>();

						// keep reading until we have the all image bytes
						while (intImageBytesRead < intImageByteSize)
						{
							// get the RLE packet
							bRLEPacket = br.ReadByte();
							intRLEPacketType = bRLEPacket.GetBits(7, 1);
							intRLEPixelCount = bRLEPacket.GetBits(0, 7) + 1;

							// check the RLE packet type
							if ((TargaRLEPacketType)intRLEPacketType == TargaRLEPacketType.Compressed)
							{
								// get the pixel color data
								bRunLengthPixel = br.ReadBytes(BytesPerPixel);

								// add the number of pixels specified using the read pixel color
								for (int i = 0; i < intRLEPixelCount; i++)
								{
									foreach (byte b in bRunLengthPixel)
									{
										row.Add(b);
									}

									// increment the byte counts
									intImageRowBytesRead += bRunLengthPixel.Length;
									intImageBytesRead += bRunLengthPixel.Length;

									// if we have read a full image row
									// add the row to the row list and clear it
									// restart row byte count
									if (intImageRowBytesRead == intImageRowByteSize)
									{
										rows.Add(row);
										row = new List<byte>();
										intImageRowBytesRead = 0;
									}
								}
							}
							else if ((TargaRLEPacketType)intRLEPacketType == TargaRLEPacketType.Uncompressed)
							{
								// get the number of bytes to read based on the read pixel count
								int intBytesToRead = intRLEPixelCount * (int)BytesPerPixel;

								// read each byte
								for (int i = 0; i < intBytesToRead; i++)
								{
									row.Add(br.ReadByte());

									// increment the byte counts
									intImageBytesRead++;
									intImageRowBytesRead++;

									// if we have read a full image row
									// add the row to the row list and clear it
									// restart row byte count
									if (intImageRowBytesRead == intImageRowByteSize)
									{
										rows.Add(row);
										row = new System.Collections.Generic.List<byte>();
										intImageRowBytesRead = 0;
									}
								}
							}
						}
					}
					#endregion
					#region NON-COMPRESSED
					else
					{

						// loop through each row in the image
						for (int i = 0; i < (int)imageHeight; i++)
						{
							// create a new row
							List<byte> row = new List<byte>();

							// loop through each byte in the row
							for (int j = 0; j < intImageRowByteSize; j++)
							{
								// add the byte to the row
								row.Add(br.ReadByte());
							}

							// add row to the list of rows
							rows.Add(row);
						}
					}
					#endregion

					// flag that states whether or not to reverse the location of all rows.
					bool blnRowsReverse = false;

					// flag that states whether or not to reverse the bytes in each row.
					bool blnEachRowReverse = false;

					// use FirstPixelDestination to determine the alignment of the
					// image data byte
					switch (GetFirstPixelDestination(verticalTransferOrder, horizontalTransferOrder))
					{
						case TargaFirstPixelDestination.TopLeft:
						{
							blnRowsReverse = false;
							blnEachRowReverse = true;
							break;
						}
						case TargaFirstPixelDestination.TopRight:
						{
							blnRowsReverse = false;
							blnEachRowReverse = false;
							break;
						}
						case TargaFirstPixelDestination.BottomLeft:
						{
							blnRowsReverse = true;
							blnEachRowReverse = true;
							break;
						}
						case TargaFirstPixelDestination.BottomRight:
						case TargaFirstPixelDestination.Unknown:
						{
							blnRowsReverse = true;
							blnEachRowReverse = false;
							break;
						}
					}

					// write the bytes from each row into a memory stream and get the
					// resulting byte array
					using (msData = new System.IO.MemoryStream())
					{
						// do we reverse the rows in the row list.
						if (blnRowsReverse == true)
						{
							rows.Reverse();
						}

						// go through each row
						for (int i = 0; i < rows.Count; i++)
						{
							// do we reverse the bytes in the row
							if (blnEachRowReverse == true)
							{
								rows[i].Reverse();
							}

							// get the byte array for the row
							byte[] brow = rows[i].ToArray();

							// write the row bytes and padding bytes to the memory streem
							msData.Write(brow, 0, brow.Length);
							msData.Write(padding, 0, padding.Length);
						}

						// get the image byte array
						data = msData.ToArray();
					}

					int x = 0, y = 0;
					for (int z = 0; z < data.Length; z += 3)
					{
						int r = data[z + 2];
						int g = data[z + 1];
						int b = data[z];
						int a = 255;

						if (PixelDepth == 32)
						{
							a = data[z + 3];
							z++;
						}

						Color color = Color.FromRGBAInt32(r, g, b, a);
						pic.SetPixel(color, x, y);

						x++;
						if (x == imageWidth)
						{
							x = 0;
							y++;

							if (y == imageHeight)
							{
								break;
							}
						}
					}
				}
				#endregion
			}
			#endregion
		}

		private Color DecodeColor(Reader br, byte colorMapEntrySize)
		{
			// load each color map entry based on the ColorMapEntrySize value
			switch (colorMapEntrySize)
			{
				case 15:
				{
					byte[] color15 = br.ReadBytes(2);
					// remember that the bytes are stored in reverse order
					return GetColorFrom2Bytes(color15[1], color15[0]);
				}
				case 16:
				{
					byte[] color16 = br.ReadBytes(2);
					// remember that the bytes are stored in reverse order
					return GetColorFrom2Bytes(color16[1], color16[0]);
				}
				case 24:
				{
					int b = Convert.ToInt32(br.ReadByte());
					int g = Convert.ToInt32(br.ReadByte());
					int r = Convert.ToInt32(br.ReadByte());
					return Color.FromRGBAInt32(r, g, b);
				}
				case 32:
				{
					int a = Convert.ToInt32(br.ReadByte());
					int b = Convert.ToInt32(br.ReadByte());
					int g = Convert.ToInt32(br.ReadByte());
					int r = Convert.ToInt32(br.ReadByte());
					return Color.FromRGBAInt32(a, r, g, b);
				}
			}
			throw new ArgumentOutOfRangeException(nameof(colorMapEntrySize), "TargaImage only supports ColorMap Entry Sizes of 15, 16, 24 or 32 bits.");
		}

		private TargaFirstPixelDestination GetFirstPixelDestination(TargaVerticalTransferOrder verticalTransferOrder, TargaHorizontalTransferOrder horizontalTransferOrder)
		{
			if (verticalTransferOrder == TargaVerticalTransferOrder.Unknown || horizontalTransferOrder == TargaHorizontalTransferOrder.Unknown)
			{
				return TargaFirstPixelDestination.Unknown;
			}
			else if (verticalTransferOrder == TargaVerticalTransferOrder.BottomToTop && horizontalTransferOrder == TargaHorizontalTransferOrder.LeftToRight)
			{
				return TargaFirstPixelDestination.BottomLeft;
			}
			else if (verticalTransferOrder == TargaVerticalTransferOrder.BottomToTop && horizontalTransferOrder == TargaHorizontalTransferOrder.RightToLeft)
			{
				return TargaFirstPixelDestination.BottomRight;
			}
			else if (verticalTransferOrder == TargaVerticalTransferOrder.TopToBottom && horizontalTransferOrder == TargaHorizontalTransferOrder.LeftToRight)
			{
				return TargaFirstPixelDestination.TopLeft;
			}
			return TargaFirstPixelDestination.TopRight;
		}


		/// <summary>
		/// Reads ARGB values from the 16 bits of two given Bytes in a 1555 format.
		/// </summary>
		/// <param name="one">The first Byte.</param>
		/// <param name="two">The Second Byte.</param>
		/// <returns>A Color with a ARGB values read from the two given Bytes</returns>
		/// <remarks>
		/// Gets the ARGB values from the 16 bits in the two bytes based on the below diagram
		/// |   BYTE 1   |  BYTE 2   |
		/// | A RRRRR GG | GGG BBBBB |
		/// </remarks>
		private static Color GetColorFrom2Bytes(byte one, byte two)
		{
			// get the 5 bits used for the RED value from the first byte
			int r1 = one.GetBits(2, 5);
			int r = r1 << 3;

			// get the two high order bits for GREEN from the from the first byte
			int bit = one.GetBits(0, 2);
			// shift bits to the high order
			int g1 = bit << 6;

			// get the 3 low order bits for GREEN from the from the second byte
			bit = two.GetBits(5, 3);
			// shift the low order bits
			int g2 = bit << 3;
			// add the shifted values together to get the full GREEN value
			int g = g1 + g2;

			// get the 5 bits used for the BLUE value from the second byte
			int b1 = two.GetBits(0, 5);
			int b = b1 << 3;

			// get the 1 bit used for the ALPHA value from the first byte
			int a1 = one.GetBits(7, 1);
			int a = a1 * 255;

			// return the resulting Color
			return Color.FromRGBAInt32(r, g, b, a);
		}
		private static byte[] EncodeR5G5B5A1(Color color)
		{
			// FIXME: not implemented
			ushort u = 0;

			byte b1 = 0, b2 = 0;
			b1 |= color.GetRedByte();
			b1 <<= 5;
			b1 |= color.GetGreenByte();

			return new byte[] { b1, b2 };
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			#region Targa Header
			{
				// read the header properties from the file
				if (ImageID != null)
				{
					bw.WriteByte((byte)ImageID.Length);
				}
				else
				{
					bw.WriteByte(0);
				}

				bool colorMapEnabled = true;
				bw.WriteBoolean(colorMapEnabled);
				bw.WriteByte((byte)ImageType);

				short colorMapFirstEntryIndex = 0;
				bw.WriteInt16(colorMapFirstEntryIndex);

				short colorMapLength = 0;
				bw.WriteInt16(colorMapLength);

				byte colorMapEntrySize = 0;
				bw.WriteByte(colorMapEntrySize);

				short originX = 0;
				bw.WriteInt16(originX);

				short originY = 0;
				bw.WriteInt16(originY);

				bw.WriteInt16((short)pic.Width);
				bw.WriteInt16((short)pic.Height);

				if (!(PixelDepth == 8 || PixelDepth == 16 || PixelDepth == 24 || PixelDepth == 32))
				{
					throw new InvalidOperationException("Targa image file format only supports 8, 16, 24, or 32 bit pixel depths");
				}
				bw.WriteByte(PixelDepth);

				byte ImageDescriptor = GetImageDescriptor(0, PixelOrigin);
				bw.WriteByte(ImageDescriptor);
				// int attributeBits = ImageDescriptor.GetBits(0, 4);

				// verticalTransferOrder = (TargaVerticalTransferOrder)ImageDescriptor.GetBits(5, 1);
				// horizontalTransferOrder = (TargaHorizontalTransferOrder)ImageDescriptor.GetBits(4, 1);

				// load ImageID value if any
				if (ImageID != null)
				{
					bw.WriteNullTerminatedString(ImageID);
				}
				else
				{
					bw.WriteNullTerminatedString(String.Empty);
				}

				#region Load Colormap
				{
					// load color map if it's included and/or needed
					// Only needed for UNCOMPRESSED_COLOR_MAPPED and RUN_LENGTH_ENCODED_COLOR_MAPPED
					// image types. If color map is included for other file types we can ignore it.
					if (colorMapEnabled)
					{
						if (ImageType == TargaImageType.UncompressedIndexed || ImageType == TargaImageType.CompressedIndexed)
						{
							for (int i = 0; i < pic.ColorMap.Count; i++)
							{
								// load each color map entry based on the ColorMapEntrySize value
								switch (colorMapEntrySize)
								{
									case 15:
									{
										Console.Error.WriteLine("ERROR: TGA R5G5B5A1 not supported yet");
										// byte[] color15 = EncodeR5G5B5A1(pic.ColorMap[i]);
										// remember that the bytes are stored in reverse order
										// bw.WriteBytes(new byte[] { color15[1], color15[0] });
										bw.WriteBytes(new byte[] { 0, 0 });
										break;
									}
									case 16:
									{
										Console.Error.WriteLine("ERROR: TGA R5G6B5 not supported yet");
										// byte[] color16 = EncodeR5G6B5(pic.ColorMap[i]);
										// remember that the bytes are stored in reverse order
										// bw.WriteBytes(new byte[] { color15[1], color15[0] });
										bw.WriteBytes(new byte[] { 0, 0 });
										break;
									}
									case 24:
									{
										bw.WriteByte(pic.ColorMap[i].GetBlueByte());
										bw.WriteByte(pic.ColorMap[i].GetGreenByte());
										bw.WriteByte(pic.ColorMap[i].GetRedByte());
										break;
									}
									case 32:
									{
										bw.WriteByte(pic.ColorMap[i].GetAlphaByte());
										bw.WriteByte(pic.ColorMap[i].GetBlueByte());
										bw.WriteByte(pic.ColorMap[i].GetGreenByte());
										bw.WriteByte(pic.ColorMap[i].GetRedByte());
										break;
									}
									default:
									{
										throw new ArgumentOutOfRangeException("TargaImage only supports ColorMap Entry Sizes of 15, 16, 24 or 32 bits.");
									}
								}
							}
						}
						else
						{
							throw new InvalidOperationException("Image Type requires a Color Map and Color Map Length is zero.");
						}
					}
					else
					{
						if (ImageType == TargaImageType.UncompressedIndexed || ImageType == TargaImageType.CompressedIndexed)
						{
							throw new InvalidOperationException("Indexed image type requires a colormap and there was not a colormap included in the file.");
						}
					}
				}
				#endregion
			}
			#endregion

			#region Targa image
			{
				// **************  NOTE  *******************
				// The memory allocated for Microsoft Bitmaps must be aligned on a 32bit boundary.
				// The stride refers to the number of bytes allocated for one scanline of the bitmap.
				// In your loop, you copy the pixels one scanline at a time and take into
				// consideration the amount of padding that occurs due to memory alignment.
				// calculate the stride, in bytes, of the image (32bit aligned width of each image row)
				int intStride = (((int)pic.Width * (int)PixelDepth + 31) & ~31) >> 3; // width in bytes

				// calculate the padding, in bytes, of the image
				// number of bytes to add to make each row a 32bit aligned row
				// padding in bytes
				int paddingLength = intStride - ((((int)pic.Width * (int)PixelDepth) + 7) / 8);

				#region Image Data Bytes
				{
					// read the image data into a byte array
					// take into account stride has to be a multiple of 4
					// use padding to make sure multiple of 4

					byte[] data = null;

					// padding bytes
					byte[] padding = new byte[paddingLength];
					System.IO.MemoryStream msData = null;

					// get the size in bytes of each row in the image
					int intImageRowByteSize = (int)pic.Width * ((int)BytesPerPixel);

					// get the size in bytes of the whole image
					int intImageByteSize = intImageRowByteSize * (int)pic.Height;

					List<List<byte>> rows = new List<List<byte>>();

					// is this a RLE compressed image type
					#region COMPRESSED
					if (ImageType == TargaImageType.CompressedGrayscale || ImageType == TargaImageType.CompressedIndexed || ImageType == TargaImageType.CompressedTrueColor)
					{
						Console.Error.WriteLine("ERROR: TGA RLE compression not implemented yet");
						/*
						// RLE Packet info
						byte bRLEPacket = 0;
						int intRLEPacketType = -1;
						int intRLEPixelCount = 0;
						byte[] bRunLengthPixel = null;

						// used to keep track of bytes read
						int intImageBytesRead = 0;
						int intImageRowBytesRead = 0;

						List<byte> row = new List<byte>();

						// keep reading until we have the all image bytes
						while (intImageBytesRead < intImageByteSize)
						{
							// get the RLE packet
							bRLEPacket = br.ReadByte();
							intRLEPacketType = bRLEPacket.GetBits(7, 1);
							intRLEPixelCount = bRLEPacket.GetBits(0, 7) + 1;

							// check the RLE packet type
							if ((TargaRLEPacketType)intRLEPacketType == TargaRLEPacketType.Compressed)
							{
								// get the pixel color data
								bRunLengthPixel = br.ReadBytes(BytesPerPixel);

								// add the number of pixels specified using the read pixel color
								for (int i = 0; i < intRLEPixelCount; i++)
								{
									foreach (byte b in bRunLengthPixel)
									{
										row.Add(b);
									}

									// increment the byte counts
									intImageRowBytesRead += bRunLengthPixel.Length;
									intImageBytesRead += bRunLengthPixel.Length;

									// if we have read a full image row
									// add the row to the row list and clear it
									// restart row byte count
									if (intImageRowBytesRead == intImageRowByteSize)
									{
										rows.Add(row);
										row = new List<byte>();
										intImageRowBytesRead = 0;
									}
								}
							}
							else if ((TargaRLEPacketType)intRLEPacketType == TargaRLEPacketType.Uncompressed)
							{
								// get the number of bytes to read based on the read pixel count
								int intBytesToRead = intRLEPixelCount * (int)BytesPerPixel;

								// read each byte
								for (int i = 0; i < intBytesToRead; i++)
								{
									row.Add(br.ReadByte());

									// increment the byte counts
									intImageBytesRead++;
									intImageRowBytesRead++;

									// if we have read a full image row
									// add the row to the row list and clear it
									// restart row byte count
									if (intImageRowBytesRead == intImageRowByteSize)
									{
										rows.Add(row);
										row = new System.Collections.Generic.List<byte>();
										intImageRowBytesRead = 0;
									}
								}
							}
						}
						*/
					}
					#endregion
					#region NON-COMPRESSED
					else
					{
						// loop through each row in the image
						for (int i = 0; i < (int)pic.Height; i++)
						{
							List<byte> row = new List<byte>();
							// loop through each byte in the row
							for (int j = 0; j < (int)pic.Width; j++)
							{
								// add the byte to the row
								Color color = pic.GetPixel(j, i);

								row.Add(color.GetRedByte());
								row.Add(color.GetGreenByte());
								row.Add(color.GetBlueByte());
							}
							rows.Add(row);
						}
					}
					#endregion

					// flag that states whether or not to reverse the location of all rows.
					bool blnRowsReverse = false;

					// flag that states whether or not to reverse the bytes in each row.
					bool blnEachRowReverse = false;

					// use FirstPixelDestination to determine the alignment of the
					// image data byte
					switch (PixelOrigin)
					{
						case TargaFirstPixelDestination.TopLeft:
						{
							blnRowsReverse = false;
							blnEachRowReverse = true;
							break;
						}
						case TargaFirstPixelDestination.TopRight:
						{
							blnRowsReverse = false;
							blnEachRowReverse = false;
							break;
						}
						case TargaFirstPixelDestination.BottomLeft:
						{
							blnRowsReverse = true;
							blnEachRowReverse = true;
							break;
						}
						case TargaFirstPixelDestination.BottomRight:
						case TargaFirstPixelDestination.Unknown:
						{
							blnRowsReverse = true;
							blnEachRowReverse = false;
							break;
						}
					}

					// write the bytes from each row into a memory stream and get the
					// resulting byte array
					using (msData = new System.IO.MemoryStream())
					{
						// do we reverse the rows in the row list.
						if (blnRowsReverse == true)
						{
							rows.Reverse();
						}

						// go through each row
						for (int i = 0; i < rows.Count; i++)
						{
							// do we reverse the bytes in the row
							if (blnEachRowReverse == true)
							{
								rows[i].Reverse();
							}

							// get the byte array for the row
							byte[] brow = rows[i].ToArray();

							// write the row bytes and padding bytes to the memory streem
							msData.Write(brow, 0, brow.Length);
							msData.Write(padding, 0, padding.Length);
						}

						// get the image byte array
						data = msData.ToArray();
						bw.WriteBytes(data);
					}
				}
				#endregion
			}
			#endregion
			#region Targa extension area
			{
				// is there an Extension Area in file
				if (ExtensionArea.Enabled)
				{
					short extensionAreaSize = 164;
					bw.WriteInt16(extensionAreaSize);

					bw.WriteFixedLengthString(ExtensionArea.AuthorName, EXTENSION_AREA_AUTHOR_NAME_LENGTH);
					bw.WriteFixedLengthString(ExtensionArea.AuthorComments, EXTENSION_AREA_AUTHOR_NAME_LENGTH);

					bw.WriteInt16((short)ExtensionArea.DateCreated.Month);
					bw.WriteInt16((short)ExtensionArea.DateCreated.Day);
					bw.WriteInt16((short)ExtensionArea.DateCreated.Year);
					bw.WriteInt16((short)ExtensionArea.DateCreated.Hour);
					bw.WriteInt16((short)ExtensionArea.DateCreated.Minute);
					bw.WriteInt16((short)ExtensionArea.DateCreated.Second);

					bw.WriteFixedLengthString(ExtensionArea.JobName, EXTENSION_AREA_JOB_NAME_LENGTH);

					bw.WriteInt16((short)ExtensionArea.JobTime.Hours);
					bw.WriteInt16((short)ExtensionArea.JobTime.Minutes);
					bw.WriteInt16((short)ExtensionArea.JobTime.Seconds);

					bw.WriteFixedLengthString(ExtensionArea.SoftwareID, EXTENSION_AREA_SOFTWARE_ID_LENGTH);

					// get the version number and letter from file
					float iVersionNumber = 1.0f;
					short sVersionNumber = (short)(iVersionNumber * 100.0F);
					bw.WriteInt16(sVersionNumber);
					bw.WriteChar((char)'A');
					// bw.WriteFixedLengthString(mvarExtensionArea.VersionString.Substring(mvarExtensionArea.VersionString.Length - 1, 1));
					// mvarExtensionArea.VersionString = (iVersionNumber.ToString(@"F2") + strVersionLetter);


					// get the color key of the file
					bw.WriteByte((byte)(ExtensionArea.ColorKey.A * 255));
					bw.WriteByte((byte)(ExtensionArea.ColorKey.R * 255));
					bw.WriteByte((byte)(ExtensionArea.ColorKey.B * 255));
					bw.WriteByte((byte)(ExtensionArea.ColorKey.G * 255));


					bw.WriteInt16((short)ExtensionArea.PixelAspectRatioNumerator);
					bw.WriteInt16((short)ExtensionArea.PixelAspectRatioDenominator);
					bw.WriteInt16((short)ExtensionArea.GammaNumerator);
					bw.WriteInt16((short)ExtensionArea.GammaDenominator);


					int extensionAreaColorCorrectionOffset = 0;
					bw.WriteInt32(extensionAreaColorCorrectionOffset);
					int extensionAreaPostageStampOffset = 0;
					bw.WriteInt32(extensionAreaPostageStampOffset);
					int extensionAreaScanLineOffset = 0;
					bw.WriteInt32(extensionAreaScanLineOffset);
					bw.WriteByte((byte)ExtensionArea.AttributesType);

					// load Scan Line Table from file if any
					/*
					if (extensionAreaScanLineOffset > 0)
					{
						br.Accessor.Seek(extensionAreaScanLineOffset, SeekOrigin.Begin);
						for (int i = 0; i < imageHeight; i++)
						{
							mvarExtensionArea.ScanLineTable.Add(br.ReadInt32());
						}
					}


					// load Color Correction Table from file if any
					if (extensionAreaColorCorrectionOffset > 0)
					{
						br.Accessor.Seek(extensionAreaColorCorrectionOffset, SeekOrigin.Begin);
						for (int i = 0; i < EXTENSION_AREA_COLOR_CORRECTION_TABLE_VALUE_LENGTH; i++)
						{
							a = (int)br.ReadInt16();
							r = (int)br.ReadInt16();
							b = (int)br.ReadInt16();
							g = (int)br.ReadInt16();
							mvarExtensionArea.ColorCorrectionTable.Add(Color.FromRGBA(a, r, g, b));
						}
					}
					*/
				}
			}
			#endregion
			#region Targa Footer
			{
				if (FormatVersion == TargaFormatVersion.TrueVisionXFile)
				{
					int extensionAreaOffset = 0;
					bw.WriteInt32(extensionAreaOffset);

					int developerDirectoryOffset = 0;
					bw.WriteInt32(developerDirectoryOffset);

					bw.WriteFixedLengthString("TRUEVISION-XFILE");
				}
			}
			#endregion
			bw.Flush();

			throw new NotImplementedException();
		}

		private byte GetImageDescriptor(byte alphaChannelDepth, TargaFirstPixelDestination pixelOrigin)
		{
			TargaVerticalTransferOrder verticalTransferOrder = TargaVerticalTransferOrder.Unknown;
			TargaHorizontalTransferOrder horizontalTransferOrder = TargaHorizontalTransferOrder.Unknown;
			switch (PixelOrigin)
			{
				case TargaFirstPixelDestination.TopLeft:
				{
					verticalTransferOrder = TargaVerticalTransferOrder.TopToBottom;
					horizontalTransferOrder = TargaHorizontalTransferOrder.LeftToRight;
					break;
				}
				case TargaFirstPixelDestination.TopRight:
				{
					verticalTransferOrder = TargaVerticalTransferOrder.TopToBottom;
					horizontalTransferOrder = TargaHorizontalTransferOrder.RightToLeft;
					break;
				}
				case TargaFirstPixelDestination.BottomLeft:
				{
					verticalTransferOrder = TargaVerticalTransferOrder.BottomToTop;
					horizontalTransferOrder = TargaHorizontalTransferOrder.LeftToRight;
					break;
				}
				case TargaFirstPixelDestination.BottomRight:
				{
					verticalTransferOrder = TargaVerticalTransferOrder.BottomToTop;
					horizontalTransferOrder = TargaHorizontalTransferOrder.RightToLeft;
					break;
				}
			}
			return GetImageDescriptor(alphaChannelDepth, verticalTransferOrder, horizontalTransferOrder);
		}
		private byte GetImageDescriptor(byte alphaChannelDepth, TargaVerticalTransferOrder verticalTransferOrder, TargaHorizontalTransferOrder horizontalTransferOrder)
		{
			byte ImageDescriptor = 0;

			return ImageDescriptor;
		}
	}
}
