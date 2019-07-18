using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin.Mfc;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Palette;

using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin
{
	/// <summary>
	/// Represents a Microsoft Merlin Game Engine (Hover!) .TEX file,
	/// containing a shared palette and up to 65535 textures.
	/// </summary>
	public class TEXDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				// _dfr.Filters.Add("Microsoft Merlin Game Engine (Hover!) texture pack", new string[] { "*.tex" });
			}
			return _dfr;
		}

		private int mvarPaletteEntryCount = 256;
		private PaletteObjectModel mvarPalette = new PaletteObjectModel();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			reader.Accessor.Position = 0;
			fsom.Clear();
			mvarPalette.Clear();

			for (int i = 0; i < mvarPaletteEntryCount; i++)
			{
				byte r = reader.ReadByte();
				byte g = reader.ReadByte();
				byte b = reader.ReadByte();
				byte a = reader.ReadByte();
				mvarPalette.Entries.Add(Color.FromRGBAByte(r, g, b, a));
			}

			MemoryAccessor ma = new MemoryAccessor();
			Document.Save(mvarPalette, new DataFormats.Multimedia.Palette.Adobe.ACODataFormat(), ma, true);

			fsom.Files.Add("PALETTE.ACO", ma.ToArray());

			// Read a List<CMerlinTexture>
			List<CMerlinTexture> list = reader.ReadMfcList<CMerlinTexture>();
			for (ushort i = 0; i < list.Count; i++)
			{
				/*
				ushort tag = reader.ReadUInt16();
				uint objectTag = ((uint)(tag & ClassTag) << 16) | (uint)(tag & ~ClassTag);
				if (tag == BigObjectTag)
				{
					objectTag = reader.ReadUInt32();
				}

				if (tag == NewClassTag)
				{
					// Not a class we've seen before; read it in
					ushort schemaVersion = reader.ReadUInt16();
					string className = reader.ReadUInt16String();
				}

				Folder textureI = new Folder();

				string name = ReadMFCString(reader);
				textureI.Name = name;

				TEXTextureFlags flags = (TEXTextureFlags)reader.ReadUInt16();
				ushort mipmapCount = reader.ReadUInt16();
				for (ushort j = 0; j < mipmapCount; j++)
				{
					// TextureMipmap mipmap = new TextureMipmap();
					var nextLargestHeight = reader.ReadUInt16();
					var imageHeight = reader.ReadUInt16();
					var nextLargestWidth = reader.ReadUInt16();
					var imageWidth = reader.ReadUInt16();

					// mipmap.ImageDimensionsMinusOne = new Size(imageWidth, imageHeight);
					// mipmap.ImageDimensions = new Size(nextLargestWidth, nextLargestHeight);
					// mipmap.Level = archive.DeserialiseUInt16();
					ushort mipmapLevel = reader.ReadUInt16();

					var imageDataLength = reader.ReadUInt32();
					byte[] ImageData = reader.ReadBytes((int)imageDataLength);

					textureI.Files.Add("MIPMAP" + j.ToString() + ".BMP", ImageData);

					int totalSpanCount = (int)reader.ReadUInt32();
					// mipmap.PixelSpans = new List<List<PixelSpan>>(totalSpanCount);
					for (int y = 0; y < nextLargestHeight; y++)
					{
						ushort rowSpanCount = reader.ReadUInt16();
						// var rowSpans = new List<PixelSpan>(rowSpanCount);
						for (int k = 0; k < rowSpanCount; k++)
						{
							ushort startIndex = reader.ReadUInt16();
							ushort endIndex = reader.ReadUInt16();
							// rowSpans.Add(new PixelSpan(startIndex, endIndex));
						}
						// mipmap.PixelSpans.Add(rowSpans);
					}

					ushort trailingByteCount = reader.ReadUInt16();
					byte[] trailingBytes = reader.ReadBytes(trailingByteCount);
				}
				fsom.Folders.Add(textureI);
				*/
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
