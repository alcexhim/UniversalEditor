//
//  CELDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using MBS.Framework.Drawing;
using MBS.Framework.Settings;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Palette.KISS;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.KISS
{
	public class CELDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.SettingsGroups[0].Settings.Add(new FileSetting("ExternalPaletteFileName", "External _palette file name", null, true, "*KiSS palette (.kcf)|*.kcf")
				{
					Required = true
				});

				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting("ImageType", "_Image type", CELImageType.RGBA32bppVersion2, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("RGBA32bppVersion2", "Indexed, 4 bits per pixel (0x20 0x04)", CELImageType.Indexed4bpp),
					new ChoiceSetting.ChoiceSettingValue("RGBA32bppVersion2", "Indexed, 8 bits per pixel (0x20 0x08)", CELImageType.Indexed8bpp),
					new ChoiceSetting.ChoiceSettingValue("RGBA32bppVersion2", "RGBA, 32 bits per pixel, version 1 (0x21 0x20)", CELImageType.RGBA32bppVersion1),
					new ChoiceSetting.ChoiceSettingValue("RGBA32bppVersion2", "RGBA, 32 bits per pixel, version 2 (0x20 0x20)", CELImageType.RGBA32bppVersion2)
				}));
			}
			return _dfr;
		}

		public CELImageType ImageType { get; set; } = CELImageType.RGBA32bppVersion2;
		public string ExternalPaletteFileName { get; set; } = null;
		public PaletteObjectModel ExternalPalette { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			string signature = Accessor.Reader.ReadFixedLengthString(4);
			if (signature != "KiSS")
				throw new InvalidDataFormatException("file does not begin with 'KiSS'");

			string externalPaletteFileName = null;
			if (!String.IsNullOrEmpty(ExternalPaletteFileName))
			{
				externalPaletteFileName = ExternalPaletteFileName;
			}
			else
			{
				externalPaletteFileName = System.IO.Path.ChangeExtension(Accessor.GetFileName(), "kcf");
			}
			if (!System.IO.File.Exists(externalPaletteFileName))
				externalPaletteFileName = null;

			if (ExternalPalette == null && externalPaletteFileName != null)
			{
				PaletteObjectModel palette = new PaletteObjectModel();
				Document.Load(palette, new KCFDataFormat(), new FileAccessor(externalPaletteFileName));
				ExternalPalette = palette;
			}

			byte formatCode = Accessor.Reader.ReadByte();
			byte pixelDepth = Accessor.Reader.ReadByte();
			if (formatCode == 0x20 && pixelDepth == 0x04)
			{
				ImageType = CELImageType.Indexed4bpp;
				if (ExternalPalette == null)
				{
					throw new ArgumentException("external palette file required but not found");
				}
			}
			else if (formatCode == 0x20 && pixelDepth == 0x08)
			{
				ImageType = CELImageType.Indexed8bpp;
				if (ExternalPalette == null)
				{
					throw new ArgumentException("external palette file required but not found");
				}
			}
			else if (formatCode == 0x21 && pixelDepth == 0x20)
			{
				ImageType = CELImageType.RGBA32bppVersion1;
			}
			else if (formatCode == 0x20 && pixelDepth == 0x20)
			{
				ImageType = CELImageType.RGBA32bppVersion2;
			}
			else if (formatCode == 0x10)
			{
				throw new InvalidDataFormatException("this is probably a KCF palette file");
			}

			ushort unknown = Accessor.Reader.ReadUInt16(); // 8224

			ushort width = Accessor.Reader.ReadUInt16();
			ushort height = Accessor.Reader.ReadUInt16();
			pic.Width = width;
			pic.Height = height;

			Accessor.Reader.Seek(20, IO.SeekOrigin.Current); // unknown

			byte bit = 0;
			byte lastindex = 0;
			for (ushort y = 0; y < height; y++)
			{
				for (ushort x = 0; x < width; x++)
				{
					switch (ImageType)
					{
						case CELImageType.Indexed4bpp:
						{
							byte index = 0;
							if (bit == 0)
							{
								lastindex = Accessor.Reader.ReadByte();
								index = (byte)lastindex.GetBits(0, 4);
								bit = 1;
							}
							else if (bit == 1)
							{
								index = (byte)lastindex.GetBits(4, 4);
								bit = 0;
							}

							Color color = ExternalPalette.Entries[index].Color;
							if (index == 0)
								color = Colors.Transparent;

							pic.SetPixel(color, x, y);
							break;
						}
						case CELImageType.Indexed8bpp:
						{
							byte index = Accessor.Reader.ReadByte();

							Color color = ExternalPalette.Entries[index].Color;
							if (index == 0)
								color = Colors.Transparent;

							pic.SetPixel(color, x, y);
							break;
						}
						case CELImageType.RGBA32bppVersion1:
						case CELImageType.RGBA32bppVersion2:
						{
							byte b = Accessor.Reader.ReadByte();
							byte g = Accessor.Reader.ReadByte();
							byte r = Accessor.Reader.ReadByte();
							byte a = Accessor.Reader.ReadByte();

							Color color = Color.FromRGBAByte(r, g, b, a);
							pic.SetPixel(color, x, y);
							break;
						}
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			Accessor.Writer.WriteFixedLengthString("KiSS");

			switch (ImageType)
			{
				case CELImageType.Indexed4bpp:
				{
					Accessor.Writer.WriteByte(0x20);
					Accessor.Writer.WriteByte(0x04);
					break;
				}
				case CELImageType.Indexed8bpp:
				{
					Accessor.Writer.WriteByte(0x20);
					Accessor.Writer.WriteByte(0x08);
					break;
				}
				case CELImageType.RGBA32bppVersion1:
				{
					Accessor.Writer.WriteByte(0x21);
					Accessor.Writer.WriteByte(0x20);
					break;
				}
				case CELImageType.RGBA32bppVersion2:
				{
					Accessor.Writer.WriteByte(0x20);
					Accessor.Writer.WriteByte(0x20);
					break;
				}
			}

			Accessor.Writer.WriteUInt16(0); // unknown

			ushort width = (ushort)pic.Width;
			ushort height = (ushort)pic.Height;
			Accessor.Writer.WriteUInt16(width);
			Accessor.Writer.WriteUInt16(height);

			Accessor.Writer.WriteBytes(new byte[20]); // unknown

			for (ushort y = 0; y < height; y++)
			{
				for (ushort x = 0; x < width; x++)
				{
					Color color = pic.GetPixel(x, y);
					Accessor.Writer.WriteByte(color.GetBlueByte());
					Accessor.Writer.WriteByte(color.GetGreenByte());
					Accessor.Writer.WriteByte(color.GetRedByte());
					Accessor.Writer.WriteByte(color.GetAlphaByte());
				}
			}
		}
	}
}
