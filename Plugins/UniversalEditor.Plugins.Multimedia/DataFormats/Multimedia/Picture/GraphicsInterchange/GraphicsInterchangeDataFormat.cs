//
//  GraphicsInterchangeDataFormat.cs - provides a DataFormat for manipulating images in CompuServe GIF format
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

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.GraphicsInterchange
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in CompuServe GIF format.
	/// </summary>
	public class GraphicsInterchangeDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		/// <summary>
		/// Gets a collection of <see cref="GraphicsInterchangeExtensionBlock" /> instances representing the extensions to the GIF format used in this image.
		/// </summary>
		/// <value>The extensions to the GIF format used in this image.</value>
		public GraphicsInterchangeExtensionBlock.GraphicsInterchangeExtensionBlockCollection Extensions { get; } = new GraphicsInterchangeExtensionBlock.GraphicsInterchangeExtensionBlockCollection();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader br = base.Accessor.Reader;
			string magic = br.ReadFixedLengthString(6);
			if (!(magic == "GIF87a" || magic == "GIF89a")) throw new InvalidDataFormatException("File does not begin with \"GIF87a\" or \"GIF89a\"");

			List<Color> palette = new List<Color>();

			#region Logical Screen Descriptor
			short logicalWidth = br.ReadInt16();
			short logicalHeight = br.ReadInt16();
			byte gct = br.ReadByte();
			byte backgroundColorPaletteID = br.ReadByte();
			byte defaultPixelAspectRatio = br.ReadByte();

			int maxColors = gct + 9;
			for (int i = 0; i < maxColors; i++)
			{
				byte r = br.ReadByte();
				byte g = br.ReadByte();
				byte b = br.ReadByte();
				palette.Add(Color.FromRGBAByte(r, g, b));
			}
			#endregion

			while (!br.EndOfStream)
			{
				byte sentinel = br.ReadByte();
				switch (sentinel)
				{
					case 0x2C: // ',' image
					{
						break;
					}
					case 0x21: // '!' extension
					{
						GraphicsInterchangeExtensionBlock extension = new GraphicsInterchangeExtensionBlock();
						extension.ID = br.ReadByte();

						// The linked lists used by the image data and the extension blocks consist of series
						// of sub-blocks, each sub-block beginning with a byte giving the number of subsequent
						// data bytes in the sub-block (1 to 255). The series of sub-blocks is terminated by
						// an empty sub-block (a 0 byte).
						while (!br.EndOfStream)
						{
							byte extSize = br.ReadByte();
							if (extSize == 0) break;

							byte[] extData = br.ReadBytes(extSize);
							extension.DataBlocks.Add(extData);
						}

						Extensions.Add(extension);
						break;
					}
					case 0x3B: // ';' end of file
					{
						return;
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
