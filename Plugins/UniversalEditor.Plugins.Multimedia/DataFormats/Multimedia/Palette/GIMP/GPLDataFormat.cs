//
//  GPLDataFormat.cs - provides a DataFormat for manipulating color palettes in GIMP GPL format
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

using MBS.Framework.Drawing;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.GIMP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating color palettes in GIMP GPL format.
	/// </summary>
	public class GPLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Reader tr = base.Accessor.Reader;
			bool headerRead = false;
			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (line.Contains(":"))
				{
					string[] parms = line.Split(new char[] { ':' }, 2);

					string name = parms[0].Trim();
					string value = String.Empty;
					if (parms.Length > 1)
						value = parms[1].Trim();

					if (name == "Name")
					{
						palette.Name = value;
					}
					continue;
				}
				if (line.Contains("#")) line = line.Substring(0, line.IndexOf("#"));
				line = line.Trim();
				if (line == String.Empty) continue;

				if (line == "GIMP Palette")
				{
					headerRead = true;
					continue;
				}
				else if (!headerRead)
				{
					throw new InvalidDataFormatException("File does not begin with \"GIMP Palette\"");
				}
				else
				{
					string[] colors = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
					if (colors.Length >= 3)
					{
						string colorName = String.Empty;
						int r = Int32.Parse(colors[0]);
						int g = Int32.Parse(colors[1]);
						int b = Int32.Parse(colors[2]);

						if (colors.Length >= 4)
						{
							colorName = colors[3];
						}

						Color color = Color.FromRGBAInt32(r, g, b);
						palette.Entries.Add(color, colorName);
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			PaletteObjectModel palette = (objectModel as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteLine("GIMP Palette");
			writer.WriteLine(String.Format("Name: {0}", palette.Name));
			writer.WriteLine("#");

			foreach (PaletteEntry entry in palette.Entries)
			{
				writer.WriteLine(String.Format("{0} {1} {2}\t{3}", entry.Color.GetRedByte().ToString(), entry.Color.GetGreenByte().ToString(), entry.Color.GetBlueByte().ToString(), entry.Name));
			}
		}
	}
}
