using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework.Drawing;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.Plugins.ChaosWorks.DataFormats.Multimedia.PictureCollection
{
	public class CWESpriteDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionFile(nameof(ExternalPaletteFileName), "External _palette file name"));
			}
			return _dfr;
		}

		public string ExternalPaletteFileName { get; set; } = null;
		public PaletteObjectModel EmbeddedPalette { get; set; } = null;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);

			IO.Reader br = base.Accessor.Reader;
			br.Accessor.Position = 0;

			string CWE_sprite = br.ReadNullTerminatedString();
			if (CWE_sprite != "CWE sprite") throw new InvalidDataFormatException();

			uint versionMajor = br.ReadUInt32();        // always the same?
			uint versionMinor = br.ReadUInt32();        // always the same?

			long SPRHEADERSIZE = 51;
			bool isSPX = false;
			if (versionMajor == 36 && versionMinor == 12)
			{
				isSPX = false;
			}
			else if (versionMajor == 44 && versionMinor == 18)
			{
				isSPX = true;
			}

			uint key = br.ReadUInt32();
			uint unknown1 = br.ReadUInt32();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();
			uint unknown5 = br.ReadUInt32();

			if (isSPX)
			{
				uint unknown6 = br.ReadUInt32();
				uint unknown7 = br.ReadUInt32();
				SPRHEADERSIZE = 59;
			}

			uint length = br.ReadUInt32();
			uint frameCount = br.ReadUInt32();

			br.Accessor.SavePosition();
			br.Seek(length + SPRHEADERSIZE, IO.SeekOrigin.Begin);

			// frame definition data?
			uint m_frameWidth = 0;
			for (uint i = 0; i < frameCount; i++)
			{
				uint a = br.ReadUInt32();
				uint b = br.ReadUInt32();
				ushort x = br.ReadUInt16();
				ushort y = br.ReadUInt16();
				ushort frameWidth = br.ReadUInt16();
				ushort frameHeight = br.ReadUInt16();

				ushort c = 0, d = 0;
				if (isSPX)
				{
					c = br.ReadUInt16();
					d = br.ReadUInt16();
				}

				PictureObjectModel pic = new PictureObjectModel();
				pic.Width = frameWidth;
				pic.Height = frameHeight;
				coll.Pictures.Add(pic);

				if (m_frameWidth == 0)
					m_frameWidth = frameWidth;

				Console.WriteLine("cwe-sprite: added picture for sprite frame {0}\t{1}\t({2}, {3})\t{4}x{5}  {6}", a, b, x, y, frameWidth, frameHeight, (isSPX ? String.Format("(spx: {0} {1})", c, d) : String.Empty));
			}

			PaletteObjectModel palette = new PaletteObjectModel();
			if (ExternalPaletteFileName != null)
			{
				if (System.IO.File.Exists(ExternalPaletteFileName))
					Document.Load(palette, new Palette.SPPDataFormat(), new FileAccessor(ExternalPaletteFileName));
			}

			// now we're at the embedded palette
			if (!br.EndOfStream)// just in case this file doesn't include one; they all seem to have it though
			{
				EmbeddedPalette = new PaletteObjectModel();
				while (!br.EndOfStream)
				{
					byte r = br.ReadByte();
					byte g = br.ReadByte();
					byte b = br.ReadByte();
					byte a = 255;

					if (!isSPX)
					{
						a = br.ReadByte();
						a = (byte)(255 - a);
					}

					Color color = Color.FromRGBAByte(r, g, b, a);
					EmbeddedPalette.Entries.Add(color);
					palette.Entries.Add(color);
				}
			}
			br.Accessor.LoadPosition();

			// now that we've loaded the frame definitions and embedded color palette,
			// we can go back and read the pixel data
			List<List<byte>> lists = new List<List<byte>>();
			int paletteSelector = 0;

			for (uint i = 0; i < frameCount; i++)
			{
				int x = 0, y = 0;
				PictureObjectModel pic = coll.Pictures[(int)i];
				while (!br.EndOfStream)
				{
					ushort blockLength = br.ReadUInt16();
					if (blockLength == ushort.MaxValue)
						break;

					if (blockLength == 0)
						continue;

					long blockEnd = br.Accessor.Position + blockLength;
					ushort chunkFrameCount = br.ReadUInt16();

					for (ushort j = 0; j < chunkFrameCount; j++)
					{
						if (isSPX)
						{
							// TODO: figure this out!
						}
						else
						{
							ushort skip_count = br.ReadUInt16();
							ushort size_count = br.ReadUInt16();

							y += skip_count;

							if ((short)size_count < 0)
							{
								// if it is negative a single byte follows, and is repeated -size_count times
								size_count = (ushort)(-(short)size_count);

								byte index = br.ReadByte();
								Color color = palette.Entries[paletteSelector + index].Color;

								for (int k = 0; k < size_count; k++)
								{
									// Console.WriteLine("cwe-sprite: setting pixel ({0}, {1}) to color {2}", x, y, color);
									pic.SetPixel(color, y, x);
									y++;
								}
							}
							else
							{
								for (int k = 0; k < size_count; k++)
								{
									byte index = br.ReadByte();
									Color color = palette.Entries[paletteSelector + index].Color;

									// Console.WriteLine("cwe-sprite: setting pixel ({0}, {1}) to color {2}", x, y, color);
									pic.SetPixel(color, y, x);
									y++;
								}
							}
						}
					}

					if (br.Accessor.Position != blockEnd)
					{
						Console.WriteLine("cwe-sprite ERROR: finished reading a block, but not at block end! skipping to end of block anyway...");
					}

					br.Seek(blockEnd, IO.SeekOrigin.Begin);
					x++;
					y = 0;
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
