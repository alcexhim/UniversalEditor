//
//  ASEDataFormat.cs - provides a DataFormat for manipulating color palettes in Adobe ASE format
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

namespace UniversalEditor.DataFormats.Multimedia.Palette.Adobe
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating color palettes in Adobe ASE format.
	/// </summary>
	public class ASEDataFormat : DataFormat
	{
		// http://www.selapa.net/swatches/colors/fileformats.php#adobe_acb

		private static DataFormatReference _dfr = null;
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

			IO.Reader br = base.Accessor.Reader;
			br.Endianness = IO.Endianness.BigEndian;
			br.Accessor.DefaultEncoding = Encoding.UTF8;

			string ASEF = br.ReadFixedLengthString(4);
			if (ASEF != "ASEF") throw new InvalidDataFormatException("File does not begin with \"ASEF\"");

			short versionMajor = br.ReadInt16();
			short versionMinor = br.ReadInt16();

			int blockCount = br.ReadInt32();
			for (int i = 0; i < blockCount; i++)
			{
				ushort blockType = br.ReadUInt16();
				int blockLength = br.ReadInt32();
				ushort blockNameLength = br.ReadUInt16();

				string blockName = br.ReadFixedLengthString(blockNameLength).TrimNull();

				switch (blockType)
				{
					case 0xC001:
					{
						// Group start
						break;
					}
					case 0xC002:
					{
						// Group end
						break;
					}
					case 0x0001:
					{
						// Color entry
						string colorModel = br.ReadFixedLengthString(4);
						switch (colorModel)
						{
							case "CMYK":
							{
								break;
							}
							case "LAB ":
							{
								float r = br.ReadSingle();
								float g = br.ReadSingle();
								float b = br.ReadSingle();
								break;
							}
							case "RGB ":
							{
								float r = br.ReadSingle();
								float g = br.ReadSingle();
								float b = br.ReadSingle();

								byte _r = (byte)(Math.Round(r * 255));
								byte _g = (byte)(Math.Round(g * 255));
								byte _b = (byte)(Math.Round(b * 255));

								Color color = Color.FromRGBAByte(_r, _g, _b);
								palette.Entries.Add(color);
								break;
							}
							case "Gray":
							{
								break;
							}
						}

						ushort colorType = br.ReadUInt16();
						// 0 = Global, 1 = Spot, 2 = Normal
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
