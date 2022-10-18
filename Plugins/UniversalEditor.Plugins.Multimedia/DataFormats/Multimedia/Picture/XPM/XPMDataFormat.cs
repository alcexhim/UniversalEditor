//
//  XPMDataFormat.cs - provides a DataFormat to load and save X BitMap / X PixMap images
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
using MBS.Framework.Drawing;
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.XPM
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to load and save X BitMap / X PixMap
	/// images.
	/// </summary>
	public class XPMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(XBMIdentifier), "XBM/XPM1/XPM3 _identifier"));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting(nameof(FormatVersion), "Format _version", XPMFormatVersion.XPM2, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("XBM", "XBM", XPMFormatVersion.XBM),
					new ChoiceSetting.ChoiceSettingValue("XPM1", "XPM1", XPMFormatVersion.XPM1),
					new ChoiceSetting.ChoiceSettingValue("XPM2", "XPM2", XPMFormatVersion.XPM2),
					new ChoiceSetting.ChoiceSettingValue("XPM3", "XPM3", XPMFormatVersion.XPM3)
				}));

				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new BooleanSetting(nameof(X10FormatBitmap), "_Write X10 format bitmap") { Description = "Use 16-bit unsigned short values instead of unsigned char" });

				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new BooleanSetting(nameof(IncludeHotspotCoordinates), "Include _hotspot coordinates"));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(HotspotCoordinatesX), "Hotspot _X"));
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new RangeSetting(nameof(HotspotCoordinatesY), "Hotspot _Y"));
				// FIXME: need a way to specify a Settings editor for a PositionVector2
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the default background color for a monochrome X BitMap.
		/// </summary>
		/// <value>The background color.</value>
		public Color BackgroundColor { get; set; } = Colors.White;
		/// <summary>
		/// Gets or sets the default foreground color for a monochrome X BitMap.
		/// </summary>
		/// <value>The foreground color.</value>
		public Color ForegroundColor { get; set; } = Colors.Black;

		public XPMFormatVersion FormatVersion { get; set; } = XPMFormatVersion.XPM2;
		public string XBMIdentifier { get; set; } = null;

		public bool IncludeHotspotCoordinates { get; set; } = false;

		// FIXME: for Settings editor compatibility only...
		private double HotspotCoordinatesX { get { return HotspotCoordinates.X; } set { HotspotCoordinates = new PositionVector2(value, HotspotCoordinates.Y); } }
		private double HotspotCoordinatesY { get { return HotspotCoordinates.Y; } set { HotspotCoordinates = new PositionVector2(HotspotCoordinates.X, value); } }

		public PositionVector2 HotspotCoordinates { get; set; } = new PositionVector2(0, 0);

		/// <summary>
		/// Determines whether this is an X10 format bitmap; i.e., the coordinates
		/// are defined as "unsigned short" rather than "unsigned char". X10 format
		/// bitmaps are not supported by Eye of GNOME, but can be viewed with GIMP.
		/// </summary>
		/// <value><c>true</c> if X10 format (unsigned short) should be used; otherwise, <c>false</c>.</value>
		public bool X10FormatBitmap { get; set; } = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			FormatVersion = XPMFormatVersion.Unknown; // let's detect it

			int w = 0, h = 0, x = 0, y = 0;
			Reader reader = Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (FormatVersion == XPMFormatVersion.Unknown)
				{
					if (line.StartsWith("#define "))
					{
						FormatVersion = XPMFormatVersion.XBM;
					}
					else if (line == "! XPM2")
					{
						FormatVersion = XPMFormatVersion.XPM2;
					}
					else
					{
						FormatVersion = XPMFormatVersion.XPM1;
					}
				}

				if (FormatVersion == XPMFormatVersion.XBM)
				{
					if (line.StartsWith("#define "))
					{
						// FIXME: this should support "#define xxx_width" or "#define width" but NOT "#define xxxwidth"
						int windex = line.IndexOf("_width");
						int hindex = line.IndexOf("_height");
						if (windex != -1)
						{
							windex += "_width".Length;
							string wstr = line.Substring(windex + 1);
							w = Int32.Parse(wstr);

							if (XBMIdentifier == null)
							{
								XBMIdentifier = line.Substring("#define ".Length, windex - "#define ".Length - "_width".Length);
							}
						}
						else if (hindex != -1)
						{
							hindex += "_height".Length;
							string hstr = line.Substring(hindex + 1);
							h = Int32.Parse(hstr);

							if (XBMIdentifier == null)
							{
								XBMIdentifier = line.Substring("#define ".Length, hindex - "#define ".Length - "_height".Length);
							}
						}
					}
					else if (line.StartsWith("static "))
					{
						string dataType = line.Substring("static ".Length);
						if (dataType.StartsWith("unsigned "))
						{
							dataType = dataType.Substring("unsigned ".Length);
						}
						dataType = dataType.Substring(0, dataType.IndexOf(' '));

						if (dataType.Equals("char"))
						{
							X10FormatBitmap = false;
						}
						else if (dataType.Equals("short"))
						{
							X10FormatBitmap = true;
						}
						else
						{
							throw new InvalidDataFormatException(String.Format("unexpected datatype {0}", dataType));
						}

						pic.Size = new Dimension2D(w, h);
						int maxBytes = w * h;

						// could be "static char", "static unsigned char", etc.
						// just look for something like xxx_bits[] = {

						// NOTE: according to eog(1), XBM MUST:
						// 		* declare the bits[] as static, and
						//		* the variable name MUST either BE "bits" OR END IN "_bits"

						// The file does not necessarily have to have all its punctuation on the same line.
						// e.g.
						// static char bits[] = { ... };
						// OR
						// static char bits[]
						// =
						// {
						// ...
						// };

						// ... AND, the final semicolon AND closing brace are OPTIONAL.

						// In fact, it seems as though as long as there are enough elements
						// in the array to satisfy the length*width requirement, any additional
						// JUNK is IGNORED.
						int indexOfOpenBrace = -1;
						bool found = false;
						while (!String.IsNullOrEmpty(line))
						{
							if (!found)
							{
								indexOfOpenBrace = line.IndexOf('{');
								if (indexOfOpenBrace != -1)
								{
									found = true;
									line = line.Substring(indexOfOpenBrace + 1);

									// read any bytes on this line, then continue to the next
									// control falls through to "reading byte arrays"
								}
								else
								{
									// try reading another line until we can find
									line = reader.ReadLine();
									continue;
								}
							}

							// we are reading byte arrays
							string[] bytes = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

							for (int i = 0; i < bytes.Length; i++)
							{
								byte[] bits = null;

								string szbyt = bytes[i].Trim();
								System.Globalization.NumberStyles numberStyles = System.Globalization.NumberStyles.None;
								if (szbyt.StartsWith("0x"))
								{
									szbyt = szbyt.Substring(2);
									numberStyles = System.Globalization.NumberStyles.HexNumber;
								}

								if (X10FormatBitmap)
								{
									szbyt = szbyt.Substring(0, 4);

									ushort byt = UInt16.Parse(szbyt.Trim(), numberStyles);
									bits = byt.ToBits();
								}
								else
								{
									szbyt = szbyt.Substring(0, 2);

									byte byt = Byte.Parse(szbyt.Trim(), numberStyles);
									bits = byt.ToBits();
								}

								for (int j = 0; j < bits.Length; j++)
								{
									pic.SetPixel(bits[j] == 1 ? ForegroundColor : BackgroundColor, x, y);
									x++;
									if (x >= w)
									{
										y++;
										x = 0;
									}
									if (y >= h)
									{
										// fin
										break;
									}
								}
							}

							if (reader.EndOfStream)
								break;

							line = reader.ReadLine();
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

			List<Color> list = pic.GenerateColorMap();

			Writer writer = Accessor.Writer;
			switch (FormatVersion)
			{
				case XPMFormatVersion.XPM1:
				{
					writer.WriteLine(String.Format("#define XFACE_format {0}", 1));
					writer.WriteLine(String.Format("#define XFACE_width {0}", pic.Width.ToString()));
					writer.WriteLine(String.Format("#define XFACE_height {0}", pic.Height.ToString()));
					writer.WriteLine(String.Format("#define XFACE_ncolors {0}", list.Count.ToString()));
					writer.WriteLine(String.Format("#define XFACE_chars_per_pixel {0}", 1));

					writer.WriteLine("static char *XFACE_colors[] = {");
					for (int i = 0; i < list.Count; i++)
					{
						writer.Write(String.Format("\"{0}\", \"{1}\"", (char)((int)'A' + i), list[i].ToHexadecimalHTML()));
						if (i < list.Count - 1)
						{
							writer.Write(",");
						}
						writer.WriteLine();
					}
					writer.WriteLine("};");

					writer.WriteLine("static char *XFACE_pixels[] = {");
					for (int y = 0; y < pic.Height; y++)
					{
						writer.Write("\"");
						for (int x = 0; x < pic.Width; x++)
						{
							writer.WriteChar((char)((int)'A' + list.IndexOf(pic.GetPixel(x, y))));
						}
						writer.Write("\"");
						if (y < pic.Height - 1)
						{
							writer.Write(",");
						}
						writer.WriteLine();
					}
					writer.WriteLine("};");
					break;
				}
				case XPMFormatVersion.XPM2:
				{
					writer.WriteLine("! XPM2");
					writer.WriteLine(String.Format("{0} {1} {2} {3}", pic.Width.ToString(), pic.Height.ToString(), list.Count, 1));
					for (int i = 0; i < list.Count; i++)
					{
						writer.WriteLine(String.Format("{0} {1} {2}", (char)((int)'A' + i), 'c', list[i].ToHexadecimalHTML()));
					}
					for (int y = 0; y < pic.Height; y++)
					{
						for (int x = 0; x < pic.Width; x++)
						{
							writer.WriteChar((char)((int)'A' + list.IndexOf(pic.GetPixel(x, y))));
						}
						writer.WriteLine();
					}
					break;
				}
				case XPMFormatVersion.XPM3:
				{
					writer.WriteLine("/* XPM */");
					writer.WriteLine("static char * XFACE[] = {");
					writer.WriteLine("/* <Values> */");
					writer.WriteLine("/* <width/columns> <height/rows> <colors> <chars per pixel>*/");
					writer.WriteLine(String.Format("\"{0} {1} {2} {3}\",", pic.Width.ToString(), pic.Height.ToString(), list.Count.ToString(), 1));
					writer.WriteLine("/* <Colors> */");

					for (int i = 0; i < list.Count; i++)
					{
						writer.Write(String.Format("\"{0} {1} {2}\"", (char)((int)'A' + i), 'c', list[i].ToHexadecimalHTML()));
						if (i < list.Count - 1)
						{
							writer.Write(",");
						}
						writer.WriteLine();
					}

					writer.WriteLine("/* <Pixels> */");
					for (int y = 0; y < pic.Height; y++)
					{
						writer.Write("\"");
						for (int x = 0; x < pic.Width; x++)
						{
							writer.WriteChar((char)((int)'A' + list.IndexOf(pic.GetPixel(x, y))));
						}
						writer.Write("\"");
						if (y < pic.Height - 1)
						{
							writer.Write(",");
						}
						writer.WriteLine();
					}

					writer.WriteLine("};");
					break;
				}
				case XPMFormatVersion.XBM:
				{
					string preamble = String.Empty;
					if (XBMIdentifier != null)
					{
						preamble = String.Concat(XBMIdentifier, "_");
					}

					writer.WriteLine(String.Format("#define {0}width {1}", preamble, pic.Width));
					writer.WriteLine(String.Format("#define {0}height {1}", preamble, pic.Height));
					if (IncludeHotspotCoordinates)
					{
						writer.WriteLine(String.Format("#define {0}x_hot {1}", preamble, HotspotCoordinates.X));
						writer.WriteLine(String.Format("#define {0}y_hot {1}", preamble, HotspotCoordinates.Y));
					}

					writer.WriteLine(String.Format("static unsigned char {0}bits[] = {{", preamble));
					uint bits = 0;
					uint bitmask = 0x00000001;
					for (int y = 0; y < pic.Height; y++)
					{
						bool xsent = false;
						for (int x = 0; x < pic.Width; x++)
						{
							Color color = pic.GetPixel(x, y).ToBlackAndWhite();
							if (color.Luminosity > 0.5)
							{
								// white
								// bits |= bitmask;
							}
							else
							{
								// black
								bits |= bitmask;
							}
							bitmask <<= 1;

							if (bitmask == 0x100)
							{
								writer.Write(String.Format("0x{0}", bits.ToString("x").PadLeft(2, '0')));
								if (x < pic.Width - 1)
								{
									writer.Write(", ");
									xsent = true;
								}

								bitmask = 0x00000001;
								bits = 0;
							}
						}

						if (y < pic.Height - 1 && xsent)
							writer.Write(", ");
					}
					writer.WriteLine("};");
					break;
				}
			}
		}
	}
}
