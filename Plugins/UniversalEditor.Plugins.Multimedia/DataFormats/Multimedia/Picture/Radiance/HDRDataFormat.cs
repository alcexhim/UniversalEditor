//
//  HDRDataFormat.cs
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
using System.Collections.Generic;
using MBS.Framework.Drawing;
using MBS.Framework.Settings;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.Radiance
{
	public class HDRDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.SettingsGroups[0].Settings.Add(new ChoiceSetting("Compression", "Compression", HDRCompression.None, new ChoiceSetting.ChoiceSettingValue[]
				{
					new ChoiceSetting.ChoiceSettingValue("None", "None", HDRCompression.None),
					new ChoiceSetting.ChoiceSettingValue("RunLengthEncoding", "Run-length encoding (standard)", HDRCompression.RunLengthEncoding),
					new ChoiceSetting.ChoiceSettingValue("AdaptiveRunLengthEncoding", "Run-length encoding (adaptive)", HDRCompression.AdaptiveRunLengthEncoding)
				}));
			}
			return _dfr;
		}

		public HDRCompression Compression { get; set; } = HDRCompression.None;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

			// pixels are stored one byte each R,G,B plus one byte shared exponent
			string signature = Accessor.Reader.ReadFixedLengthString(11);
			if (!signature.Equals("#?RADIANCE\n"))
				throw new InvalidDataFormatException("file does not begin with '#?RADIANCE\\n'");

			while (!Accessor.Reader.EndOfStream)
			{
				string line = ReadLine(Accessor.Reader);
				if (String.IsNullOrEmpty(line))
					break;

				string[] parts = line.Split(new char[] { '=' });

			}

			int xs = 0, ys = 0;
			int ydir = 0, xdir = 0;
			int width = 0, height = 0;

			string args = ReadLine(Accessor.Reader);
			string[] argsparts = args.Split(new char[] { ' ' });
			for (int i = 0; i < argsparts.Length; i++)
			{
				if (argsparts[i].Equals("-Y"))
				{
					ydir = -1;
					height = Int32.Parse(argsparts[i + 1]);
					ys = height - 1;
					i++;
				}
				else if (argsparts[i].Equals("+Y"))
				{
					ydir = +1;
					height = Int32.Parse(argsparts[i + 1]);
					ys = 0;
					i++;
				}
				else if (argsparts[i].Equals("-X"))
				{
					xdir = -1;
					width = Int32.Parse(argsparts[i + 1]);
					xs = width - 1;
					i++;
				}
				else if (argsparts[i].Equals("+X"))
				{
					xdir = +1;
					width = Int32.Parse(argsparts[i + 1]);
					xs = 0;
					i++;
				}
			}

			pic.Width = width;
			pic.Height = height;

			Color lastColor = Color.Empty; // for run-length encoding
			for (int y = ys; ec(y, height, ydir); y += ydir)
			{
				for (int x = xs; ec(x, width, xdir); x += xdir)
				{
					byte rz = Accessor.Reader.ReadByte();
					byte gz = Accessor.Reader.ReadByte();
					byte bz = Accessor.Reader.ReadByte();
					byte exponent = Accessor.Reader.ReadByte();

					if (rz == 255 && gz == 255 && bz == 255)
					{
						// run length encoding, exponent is run count
						for (int i = 0; i < exponent; i++)
						{
							pic.SetPixel(lastColor, x, y);
							x++;

							if (ec(x, width, xdir))
							{
								x = xs;
								y++;
							}
						}
					}

					float r = 0, g = 0, b = 0;
					if (exponent != 0)
					{
						float f = MBS.Framework.MathExtensions.ldexp(1.0f, exponent - (int)(128 + 8));
						r = rz * f;
						g = gz * f;
						b = bz * f;
					}

					Color color = Color.FromRGBASingle(r, g, b);
					pic.SetPixel(color, x, y);
					lastColor = color;
				}
			}
		}

		private static bool ec(int val, int max, int pixelDir)
		{
			if (pixelDir == 1)
				return (val < max);
			return (val >= 0);
		}

		private static string ReadLine(IO.Reader reader)
		{
			string value = System.Text.Encoding.UTF8.GetString(reader.ReadUntil(new byte[] { 0x0A }));
			reader.ReadByte(); // clear the 0x0A
			return value;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null)
				throw new ObjectModelNotSupportedException();

		}
	}
}
