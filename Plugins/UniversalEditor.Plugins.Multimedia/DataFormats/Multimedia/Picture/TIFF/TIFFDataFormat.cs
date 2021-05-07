//
//  TIFFDataFormat.cs
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
using System.Collections.Generic;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.DataFormats.Multimedia.Picture.TIFF
{
	public class TIFFDataFormat : TIFFDataFormatBase
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Type = GetType();

				_dfr.Capabilities.Clear();
				_dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);

				_dfr.ExportOptions.Add(new CustomOptionChoice("CompressionMethod", "Compression _method", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("None", TIFFCompression.None),
					new CustomOptionFieldChoice("Run-length encoding", TIFFCompression.RunLengthEncoding),
					new CustomOptionFieldChoice("Group 3 fax", TIFFCompression.Group3Fax),
					new CustomOptionFieldChoice("Group 4 fax", TIFFCompression.Group4Fax),
					new CustomOptionFieldChoice("LZW", TIFFCompression.LZW),
					new CustomOptionFieldChoice("JPEG", TIFFCompression.JPEG),
					new CustomOptionFieldChoice("Deflate", TIFFCompression.Deflate),
					new CustomOptionFieldChoice("PackBits", TIFFCompression.PackBits)
				}));
			}
			return _dfr;
		}

		public TIFFCompression CompressionMethod { get; set; } = TIFFCompression.None;

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new TIFFObjectModelBase());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			TIFFObjectModelBase tiff = (objectModels.Pop() as TIFFObjectModelBase);
			PictureCollectionObjectModel picc = (objectModels.Pop() as PictureCollectionObjectModel);

			Reader reader = Accessor.Reader;

			for (int i = 0; i < tiff.ImageFileDirectories.Count; i++)
			{
				PictureObjectModel pic = new PictureObjectModel();

				TIFFImageFileDirectory ifd = tiff.ImageFileDirectories[i];

				TIFFImageFileDirectoryEntry entryWidth = ifd.Entries.GetByTag(TIFFTag.ImageWidth);
				TIFFImageFileDirectoryEntry entryHeight = ifd.Entries.GetByTag(TIFFTag.ImageLength);
				if (entryWidth == null || entryHeight == null)
				{
					Console.WriteLine("tiff: skipping invalid IFD which does not contain both ImageWidth and ImageLength fields");
					continue;
				}

				long width = entryWidth.GetNumericValue(), height = entryHeight.GetNumericValue();

				TIFFImageFileDirectoryEntry entryCompression = ifd.Entries.GetByTag(TIFFTag.Compression);
				if (entryCompression != null)
				{
					CompressionMethod = (TIFFCompression)entryCompression.GetNumericValue();
				}

				pic.Width = (int)width;
				pic.Height = (int)height;

				TIFFImageFileDirectoryEntry entryRowsPerStrip = ifd.Entries.GetByTag(TIFFTag.RowsPerStrip);
				// The number of rows in each strip (except possibly the last strip.)
				// For example, if ImageLength is 24, and RowsPerStrip is 10, then there are 3 strips, with 10 rows in the first strip, 10 rows in the second strip,
				// and 4 rows in the third strip. (The data in the last strip is not padded with 6 extra rows of dummy data.)
				long rowsPerStrip = entryRowsPerStrip.GetNumericValue();

				long rpsEvenLength = (long)((double)pic.Height / rowsPerStrip);
				long remaining = (pic.Height - (rpsEvenLength * rowsPerStrip));

				TIFFImageFileDirectoryEntry entryStripOffsets = ifd.Entries.GetByTag(TIFFTag.StripOffsets);
				if (entryStripOffsets == null)
				{
					continue;
				}

				uint[] stripOffsets = (uint[])entryStripOffsets.Value;

				// 288000 bytes in strip
				// 288000 / 128 rows per strip = 2250 bytes per row
				// 2250 / 750 (Width) = 3 bytes per pixel

				int yoffset = 0;
				for (uint j = 0; j < stripOffsets.Length; j++)
				{
					// for each strip
					uint stripOffset = stripOffsets[j];
					reader.Seek(stripOffset, SeekOrigin.Begin);

					long rowcount = rowsPerStrip;
					if (j == stripOffsets.Length - 1 && remaining > 0)
					{
						rowcount = remaining;
					}

					for (int y = 0; y < rowcount; y++)
					{
						for (int x = 0; x < pic.Width; x++)
						{
							byte pixelR = reader.ReadByte();
							byte pixelG = reader.ReadByte();
							byte pixelB = reader.ReadByte();
							pic.SetPixel(MBS.Framework.Drawing.Color.FromRGBAByte(pixelR, pixelG, pixelB), x, yoffset + y);
						}
					}
					yoffset += (int)rowcount;
					reader.Align(4);
				}

				picc.Pictures.Add(pic);
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PictureCollectionObjectModel picc = (objectModels.Pop() as PictureCollectionObjectModel);
			TIFFObjectModelBase tiff = new TIFFObjectModelBase();

			for (int i = 0; i < picc.Pictures.Count; i++)
			{
				PictureObjectModel pic = picc.Pictures[i];

				TIFFImageFileDirectory ifd = new TIFFImageFileDirectory();

				ifd.Entries.Add(TIFFTag.ImageWidth, pic.Width);
				ifd.Entries.Add(TIFFTag.ImageLength, pic.Height);
				ifd.Entries.Add(TIFFTag.Compression, (ushort)CompressionMethod);

				ifd.Entries.Add(TIFFTag.RowsPerStrip, 128);

				tiff.ImageFileDirectories.Add(ifd);
			}

			objectModels.Push(tiff);
		}
	}
}
