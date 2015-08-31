using System;
using System.Collections.Generic;

using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.GraphicsInterchange
{
	public class GraphicsInterchangeDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("CompuServe Graphics Interchange Format", new byte?[][] { new byte?[] { (byte)'G', (byte)'I', (byte)'F', (byte)'8', (byte)'7', (byte)'a' }, new byte?[] { (byte)'G', (byte)'I', (byte)'F', (byte)'8', (byte)'9', (byte)'a' } }, new string[] { "*.gif" });
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			return dfr;
		}

		private GraphicsInterchangeExtensionBlock.GraphicsInterchangeExtensionBlockCollection mvarExtensions = new GraphicsInterchangeExtensionBlock.GraphicsInterchangeExtensionBlockCollection();
		public GraphicsInterchangeExtensionBlock.GraphicsInterchangeExtensionBlockCollection Extensions
		{
			get { return mvarExtensions; }
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.BinaryReader br = base.Stream.BinaryReader;
			string magic = br.ReadFixedLengthString(6);
			if (!(magic == "GIF87a" || magic == "GIF89a")) throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

			List<System.Drawing.Color> palette = new List<System.Drawing.Color>();

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
				palette.Add(System.Drawing.Color.FromArgb(r, g, b));
			}
			#endregion

			while (!br.EndOfStream)
			{
				byte sentinel = br.ReadByte();
				switch (sentinel)
				{
					case 0x2C: // ',' image
						break;
					case 0x21: // '!' extension
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

						mvarExtensions.Add(extension);
						break;
					case 0x3B: // ';' end of file
						return;
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
