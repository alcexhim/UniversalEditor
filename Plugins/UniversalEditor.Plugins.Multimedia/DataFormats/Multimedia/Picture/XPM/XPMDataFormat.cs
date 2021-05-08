//
//  XPMDataFormat.cs
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.XPM
{
	public class XPMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice("FormatVersion", "Format _version", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("XBM", XPMFormatVersion.XBM, false),
					new CustomOptionFieldChoice("XPM1", XPMFormatVersion.XPM1, false),
					new CustomOptionFieldChoice("XPM2", XPMFormatVersion.XPM2, true),
					new CustomOptionFieldChoice("XPM3", XPMFormatVersion.XPM3, false)
				}));
			}
			return _dfr;
		}

		public XPMFormatVersion FormatVersion { get; set; } = XPMFormatVersion.XPM2;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				if (line == "! XPM2")
				{
					FormatVersion = XPMFormatVersion.XPM2;
				}
				else
				{
					FormatVersion = XPMFormatVersion.XPM1;
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
					// FIXME: THIS IS BROKEN
					writer.WriteLine(String.Format("#define width {0}", pic.Width));
					writer.WriteLine(String.Format("#define height {0}", pic.Height));
					writer.WriteLine("static unsigned char bits[] = {");
					uint bits = 0;
					uint bitmask = 0x0000000f;
					for (int x = 0; x < pic.Width; x++)
					{
						bool ysent = false;
						for (int y = 0; y < pic.Height; y++)
						{
							Color color = pic.GetPixel(x, y).ToBlackAndWhite();
							if (color.Hue > 0.5)
							{
								// white
								bits |= bitmask;
							}
							else
							{
								// black
								// bits |= ~bitmask;
							}
							bitmask <<= 1;

							if (bitmask == 0xf0000000)
							{
								writer.Write(String.Format("0x{0}", bits.ToString("x").PadLeft(2, '0')));
								if (y < pic.Height - 1)
								{
									writer.Write(", ");
									ysent = true;
								}

								bitmask = 0x0000000f;
								bits = 0;
							}
						}

						if (x < pic.Width - 1 && ysent)
							writer.Write(", ");
					}
					writer.WriteLine("};");
					break;
				}
			}
		}
	}
}
