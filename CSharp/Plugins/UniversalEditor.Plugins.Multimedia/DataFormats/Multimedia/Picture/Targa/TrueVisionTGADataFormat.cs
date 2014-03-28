using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Targa
{
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

		private TargaExtensionArea mvarExtensionArea = new TargaExtensionArea();
		public TargaExtensionArea ExtensionArea { get { return mvarExtensionArea; } }

		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("TrueVision TARGA", new string[] { "*.tga" });
			dfr.ContentTypes.Add("image/x-targa");
			dfr.ContentTypes.Add("image/x-tga");
			return dfr;
		}

		private int mvarFormatVersion = 100; // ORIGINAL_TGA
		public int FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

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
					Bytes = 2;
					break;
				case 16:
					Bytes = 2;
					break;
				case 24:
					Bytes = 3;
					break;
				case 32:
					Bytes = 4;
					break;
			}

			// add the length of the color map
			intImageDataOffset += ((int)colorMapLength * (int)Bytes);

			// return result
			return intImageDataOffset;
		}

		private byte mvarPixelDepth = 0;
		public byte PixelDepth { get { return mvarPixelDepth; } set { mvarPixelDepth = value; } }


		public int BytesPerPixel
		{
			get { return (int)(mvarPixelDepth / 8); }
			set
			{
				if ((value * 8) > 255)
				{
					throw new ArgumentOutOfRangeException("Bytes per pixel exceeds maximum allowed bits per pixel depth of 255");
				}
				mvarPixelDepth = (byte)(value * 8);
			}
		}


		private int mvarPadding = 0;

		private TargaImageType mvarImageType = TargaImageType.None;
		public TargaImageType ImageType
		{
			get { return mvarImageType; }
			set { mvarImageType = value; }
		}

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
					mvarFormatVersion = 200;

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
					mvarFormatVersion = 100;
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
				mvarImageType = (TargaImageType)br.ReadByte();

				short colorMapFirstEntryIndex = br.ReadInt16();
				short colorMapLength = br.ReadInt16();
				byte colorMapEntrySize = br.ReadByte();

				short originX = br.ReadInt16();
				short originY = br.ReadInt16();
				imageWidth = br.ReadInt16();
				imageHeight = br.ReadInt16();

				pic.Width = imageWidth;
				pic.Height = imageHeight;

				mvarPixelDepth = br.ReadByte();
				switch (mvarPixelDepth)
				{
					case 8:
					case 16:
					case 24:
					case 32:
						break;
					default:
						throw new InvalidOperationException("Targa image file format only supports 8, 16, 24, or 32 bit pixel depths");
				}


				byte ImageDescriptor = br.ReadByte();
				int attributeBits = ImageDescriptor.GetBits(0, 4);

				verticalTransferOrder = (TargaVerticalTransferOrder)ImageDescriptor.GetBits(5, 1);
				horizontalTransferOrder = (TargaHorizontalTransferOrder)ImageDescriptor.GetBits(4, 1);

				// load ImageID value if any
				if (imageIDLength > 0)
				{
					string ImageID = br.ReadNullTerminatedString(imageIDLength);
				}

				imageDataOffset = GetImageDataOffset(colorMapLength, imageIDLength, colorMapEntrySize);

				#region Load Colormap
				{
					// load color map if it's included and/or needed
					// Only needed for UNCOMPRESSED_COLOR_MAPPED and RUN_LENGTH_ENCODED_COLOR_MAPPED
					// image types. If color map is included for other file types we can ignore it.
					if (colorMapEnabled)
					{
						if (mvarImageType == TargaImageType.UncompressedIndexed || mvarImageType == TargaImageType.CompressedIndexed)
						{
							if (colorMapLength > 0)
							{
								for (int i = 0; i < colorMapLength; i++)
								{
									int a = 0;
									int r = 0;
									int g = 0;
									int b = 0;

									// load each color map entry based on the ColorMapEntrySize value
									switch (colorMapEntrySize)
									{
										case 15:
										{
											byte[] color15 = br.ReadBytes(2);
											// remember that the bytes are stored in reverse order
											pic.ColorMap.Add(GetColorFrom2Bytes(color15[1], color15[0]));
											break;
										}
										case 16:
										{
											byte[] color16 = br.ReadBytes(2);
											// remember that the bytes are stored in reverse order
											pic.ColorMap.Add(GetColorFrom2Bytes(color16[1], color16[0]));
											break;
										}
										case 24:
										{
											b = Convert.ToInt32(br.ReadByte());
											g = Convert.ToInt32(br.ReadByte());
											r = Convert.ToInt32(br.ReadByte());
											pic.ColorMap.Add(Color.FromRGBA(r, g, b));
											break;
										}
										case 32:
										{
											a = Convert.ToInt32(br.ReadByte());
											b = Convert.ToInt32(br.ReadByte());
											g = Convert.ToInt32(br.ReadByte());
											r = Convert.ToInt32(br.ReadByte());
											pic.ColorMap.Add(Color.FromRGBA(a, r, g, b));
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


					}
					else
					{
						if (mvarImageType == TargaImageType.UncompressedIndexed || mvarImageType == TargaImageType.CompressedIndexed)
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
					mvarExtensionArea.Enabled = true;

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
						mvarExtensionArea.DateCreated = dtstamp;
					}


					string JobName = br.ReadNullTerminatedString(EXTENSION_AREA_JOB_NAME_LENGTH);


					// get the job time of the file
					iHour = br.ReadInt16();
					iMinute = br.ReadInt16();
					iSecond = br.ReadInt16();
					TimeSpan ts = new TimeSpan((int)iHour, (int)iMinute, (int)iSecond);
					mvarExtensionArea.JobTime = ts;

					string softwareID = br.ReadNullTerminatedString(EXTENSION_AREA_SOFTWARE_ID_LENGTH);
					mvarExtensionArea.SoftwareID = softwareID;


					// get the version number and letter from file
					short sVersionNumber = br.ReadInt16();
					float iVersionNumber = (float)sVersionNumber / 100.0F;

					string strVersionLetter = br.ReadNullTerminatedString(EXTENSION_AREA_SOFTWARE_VERSION_LETTER_LENGTH);

					mvarExtensionArea.VersionString = (iVersionNumber.ToString(@"F2") + strVersionLetter);


					// get the color key of the file
					int a = (int)br.ReadByte();
					int r = (int)br.ReadByte();
					int b = (int)br.ReadByte();
					int g = (int)br.ReadByte();
					mvarExtensionArea.ColorKey = (Color.FromRGBA(a, r, g, b));


					mvarExtensionArea.PixelAspectRatioNumerator = (int)br.ReadInt16();
					mvarExtensionArea.PixelAspectRatioDenominator = (int)br.ReadInt16();
					mvarExtensionArea.GammaNumerator = (int)br.ReadInt16();
					mvarExtensionArea.GammaDenominator = (int)br.ReadInt16();
					
					int extensionAreaColorCorrectionOffset = br.ReadInt32();
					int extensionAreaPostageStampOffset = br.ReadInt32();
					int extensionAreaScanLineOffset = br.ReadInt32();
					mvarExtensionArea.AttributesType = (int)br.ReadByte();


					// load Scan Line Table from file if any
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
				int intStride = (((int)imageWidth * (int)mvarPixelDepth + 31) & ~31) >> 3; // width in bytes

				// calculate the padding, in bytes, of the image 
				// number of bytes to add to make each row a 32bit aligned row
				// padding in bytes
				mvarPadding = intStride - ((((int)imageWidth * (int)mvarPixelDepth) + 7) / 8);

				// get the image data bytes
				byte[] bimagedata = null;

				#region Image Data Bytes
				{

					// read the image data into a byte array
					// take into account stride has to be a multiple of 4
					// use padding to make sure multiple of 4    

					byte[] data = null;

					// padding bytes
					byte[] padding = new byte[mvarPadding];
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
					if (mvarImageType == TargaImageType.CompressedGrayscale || mvarImageType == TargaImageType.CompressedIndexed || mvarImageType == TargaImageType.CompressedTrueColor)
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
							System.Collections.Generic.List<byte> row = new System.Collections.Generic.List<byte>();

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

						int x = 0, y = 0;
						for (int z = 0; z < data.Length; z += 3)
						{
							int r = data[z + 2];
							int g = data[z + 1];
							int b = data[z];
							int a = 255;

							if (mvarPixelDepth == 32)
							{
								a = data[z + 3];
								z++;
							}

							Color color = Color.FromRGBA(r, g, b, a);
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
				}
				#endregion
			}
			#endregion
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
			return Color.FromRGBA(a, r, g, b);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			IO.Writer bw = base.Accessor.Writer;
			PictureObjectModel pic = (objectModel as PictureObjectModel);

			/*
			#region Targa Header
			{
				// set the cursor at the beginning of the file.
				br.Accessor.Seek(0, SeekOrigin.Begin);

				// read the header properties from the file
				byte imageIDLength = br.ReadByte();
				bool colorMapEnabled = br.ReadBoolean();
				mvarImageType = (TargaImageType)br.ReadByte();

				short colorMapFirstEntryIndex = br.ReadInt16();
				short colorMapLength = br.ReadInt16();
				byte colorMapEntrySize = br.ReadByte();

				short originX = br.ReadInt16();
				short originY = br.ReadInt16();
				imageWidth = br.ReadInt16();
				imageHeight = br.ReadInt16();

				pic.Width = imageWidth;
				pic.Height = imageHeight;

				mvarPixelDepth = br.ReadByte();
				switch (mvarPixelDepth)
				{
					case 8:
					case 16:
					case 24:
					case 32:
					break;
					default:
					throw new InvalidOperationException("Targa image file format only supports 8, 16, 24, or 32 bit pixel depths");
				}


				byte ImageDescriptor = br.ReadByte();
				int attributeBits = ImageDescriptor.GetBits(0, 4);

				verticalTransferOrder = (TargaVerticalTransferOrder)ImageDescriptor.GetBits(5, 1);
				horizontalTransferOrder = (TargaHorizontalTransferOrder)ImageDescriptor.GetBits(4, 1);

				// load ImageID value if any
				if (imageIDLength > 0)
				{
					string ImageID = br.ReadNullTerminatedString(imageIDLength);
				}

				imageDataOffset = GetImageDataOffset(colorMapLength, imageIDLength, colorMapEntrySize);

				#region Load Colormap
				{
					// load color map if it's included and/or needed
					// Only needed for UNCOMPRESSED_COLOR_MAPPED and RUN_LENGTH_ENCODED_COLOR_MAPPED
					// image types. If color map is included for other file types we can ignore it.
					if (colorMapEnabled)
					{
						if (mvarImageType == TargaImageType.UncompressedIndexed || mvarImageType == TargaImageType.CompressedIndexed)
						{
							if (colorMapLength > 0)
							{
								for (int i = 0; i < colorMapLength; i++)
								{
									int a = 0;
									int r = 0;
									int g = 0;
									int b = 0;

									// load each color map entry based on the ColorMapEntrySize value
									switch (colorMapEntrySize)
									{
										case 15:
										{
											byte[] color15 = br.ReadBytes(2);
											// remember that the bytes are stored in reverse order
											pic.ColorMap.Add(GetColorFrom2Bytes(color15[1], color15[0]));
											break;
										}
										case 16:
										{
											byte[] color16 = br.ReadBytes(2);
											// remember that the bytes are stored in reverse order
											pic.ColorMap.Add(GetColorFrom2Bytes(color16[1], color16[0]));
											break;
										}
										case 24:
										{
											b = Convert.ToInt32(br.ReadByte());
											g = Convert.ToInt32(br.ReadByte());
											r = Convert.ToInt32(br.ReadByte());
											pic.ColorMap.Add(Color.FromRGBA(r, g, b));
											break;
										}
										case 32:
										{
											a = Convert.ToInt32(br.ReadByte());
											b = Convert.ToInt32(br.ReadByte());
											g = Convert.ToInt32(br.ReadByte());
											r = Convert.ToInt32(br.ReadByte());
											pic.ColorMap.Add(Color.FromRGBA(a, r, g, b));
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


					}
					else
					{
						if (mvarImageType == TargaImageType.UncompressedIndexed || mvarImageType == TargaImageType.CompressedIndexed)
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
				int intStride = (((int)pic.Width * (int)mvarPixelDepth + 31) & ~31) >> 3; // width in bytes

				// calculate the padding, in bytes, of the image 
				// number of bytes to add to make each row a 32bit aligned row
				// padding in bytes
				mvarPadding = intStride - ((((int)pic.Width * (int)mvarPixelDepth) + 7) / 8);

				// get the image data bytes
				byte[] bimagedata = null;

				#region Image Data Bytes
				{

					// read the image data into a byte array
					// take into account stride has to be a multiple of 4
					// use padding to make sure multiple of 4    

					byte[] data = null;

					// padding bytes
					byte[] padding = new byte[mvarPadding];
					System.IO.MemoryStream msData = null;

					// get the size in bytes of each row in the image
					int intImageRowByteSize = (int)pic.Width * ((int)BytesPerPixel);

					// get the size in bytes of the whole image
					int intImageByteSize = intImageRowByteSize * (int)pic.Height;

					List<List<byte>> rows = new List<List<byte>>();

					// is this a RLE compressed image type
					#region COMPRESSED
					if (mvarImageType == TargaImageType.CompressedGrayscale || mvarImageType == TargaImageType.CompressedIndexed || mvarImageType == TargaImageType.CompressedTrueColor)
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
						for (int i = 0; i < (int)pic.Height; i++)
						{
							// create a new row
							System.Collections.Generic.List<byte> row = new System.Collections.Generic.List<byte>();

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

						int x = 0, y = 0;
						for (int z = 0; z < data.Length; z += 3)
						{
							int r = data[z + 2];
							int g = data[z + 1];
							int b = data[z];
							int a = 255;

							if (mvarPixelDepth == 32)
							{
								a = data[z + 3];
								z++;
							}

							Color color = Color.FromRGBA(a, r, g, b);
							pic.SetPixel(color, x, y);

							x++;
							if (x == pic.Width)
							{
								x = 0;
								y++;

								if (y == pic.Height)
								{
									break;
								}
							}
						}
					}
				}
				#endregion
			}
			#endregion
			#region Targa extension area
			{
				// is there an Extension Area in file
				if (mvarExtensionArea.Enabled)
				{
					short extensionAreaSize = 164;
					bw.Write(extensionAreaSize);

					bw.WriteFixedLengthString(mvarExtensionArea.AuthorName, EXTENSION_AREA_AUTHOR_NAME_LENGTH);
					bw.WriteFixedLengthString(mvarExtensionArea.AuthorComments, EXTENSION_AREA_AUTHOR_NAME_LENGTH);

					bw.Write((short)mvarExtensionArea.DateCreated.Month);
					bw.Write((short)mvarExtensionArea.DateCreated.Day);
					bw.Write((short)mvarExtensionArea.DateCreated.Year);
					bw.Write((short)mvarExtensionArea.DateCreated.Hour);
					bw.Write((short)mvarExtensionArea.DateCreated.Minute);
					bw.Write((short)mvarExtensionArea.DateCreated.Second);

					bw.WriteFixedLengthString(mvarExtensionArea.JobName, EXTENSION_AREA_JOB_NAME_LENGTH);

					bw.Write((short)mvarExtensionArea.JobTime.Hours);
					bw.Write((short)mvarExtensionArea.JobTime.Minutes);
					bw.Write((short)mvarExtensionArea.JobTime.Seconds);

					bw.WriteFixedLengthString(mvarExtensionArea.SoftwareID, EXTENSION_AREA_SOFTWARE_ID_LENGTH);

					// get the version number and letter from file
					float iVersionNumber = 1.0f;
					short sVersionNumber = (short)(iVersionNumber * 100.0F);
					bw.Write(sVersionNumber);
					bw.Write((char)'A');
					// bw.WriteFixedLengthString(mvarExtensionArea.VersionString.Substring(mvarExtensionArea.VersionString.Length - 1, 1));
					// mvarExtensionArea.VersionString = (iVersionNumber.ToString(@"F2") + strVersionLetter);


					// get the color key of the file
					bw.Write((byte)(mvarExtensionArea.ColorKey.Alpha * 255));
					bw.Write((byte)(mvarExtensionArea.ColorKey.Red * 255));
					bw.Write((byte)(mvarExtensionArea.ColorKey.Blue * 255));
					bw.Write((byte)(mvarExtensionArea.ColorKey.Green * 255));


					bw.Write((short)mvarExtensionArea.PixelAspectRatioNumerator);
					bw.Write((short)mvarExtensionArea.PixelAspectRatioDenominator);
					bw.Write((short)mvarExtensionArea.GammaNumerator);
					bw.Write((short)mvarExtensionArea.GammaDenominator);


					int extensionAreaColorCorrectionOffset = 0;
					bw.Write(extensionAreaColorCorrectionOffset);
					int extensionAreaPostageStampOffset = 0;
					bw.Write(extensionAreaPostageStampOffset);
					int extensionAreaScanLineOffset = 0;
					bw.Write(extensionAreaScanLineOffset);
					bw.Write((byte)mvarExtensionArea.AttributesType);

					// load Scan Line Table from file if any
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
				}
			}
			#endregion
			#region Targa Footer
			{
				if (mvarFormatVersion == 200)
				{
					int extensionAreaOffset = 0;
					bw.Write(extensionAreaOffset);

					int developerDirectoryOffset = 0;
					bw.Write(developerDirectoryOffset);

					bw.WriteFixedLengthString("TRUEVISION-XFILE");
				}
			}
			#endregion
			bw.Flush();
			*/
			throw new NotImplementedException();
		}
	}
}
