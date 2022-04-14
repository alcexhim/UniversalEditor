//
//  DDFDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2022 Mike Becker's Software
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
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia.Palette;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.Plugins.SwarmAssault.DataFormats.Multimedia.PictureCollection
{
	public class DDFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public PaletteObjectModel InternalPalette { get; } = new PaletteObjectModel();

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);
			if (coll == null)
			{
				throw new ObjectModelNotSupportedException();
			}

			uint picsCount = Accessor.Reader.ReadUInt32();
			ushort width = Accessor.Reader.ReadUInt16();
			ushort height = Accessor.Reader.ReadUInt16();

			for (uint i = 0; i < 256; i++)
			{
				byte r = Accessor.Reader.ReadByte();
				byte g = Accessor.Reader.ReadByte();
				byte b = Accessor.Reader.ReadByte();
				byte a = Accessor.Reader.ReadByte();
				a = 255;

				InternalPalette.Entries.Add(Color.FromRGBAByte(r, g, b, a));
			}

			for (uint i = 0; i < picsCount; i++)
			{
				PictureObjectModel pic = new PictureObjectModel();

				Internal.ClipInfo clip = new Internal.ClipInfo();
				clip.Name = Accessor.Reader.ReadFixedLengthString(32).TrimNull();
				clip.Offset = Accessor.Reader.ReadUInt32();
				clip.Unknown1 = Accessor.Reader.ReadUInt32();
				clip.Length = Accessor.Reader.ReadUInt32();
				clip.Width = Accessor.Reader.ReadUInt32();
				clip.Height = Accessor.Reader.ReadUInt32();
				clip.Reader = Accessor.Reader;

				pic.SetCustomProperty<string>(MakeReference(), "Name", clip.Name);
				pic.Size = new Dimension2D((int)clip.Width, (int)clip.Height);
				pic.SetExtraData<Internal.ClipInfo>("clipinfo", clip);

				pic.DataRequest += pic_DataRequest;
				coll.Pictures.Add(pic);
			}
		}

		private void pic_DataRequest(object sender, DataRequestEventArgs e)
		{
			PictureObjectModel pic = (PictureObjectModel)sender;

			Internal.ClipInfo clip = pic.GetExtraData<Internal.ClipInfo>("clipinfo");
			clip.Reader.Accessor.Seek(clip.Offset, IO.SeekOrigin.Begin);

			for (uint y = 0; y < clip.Height; y++)
			{
				for (uint x = 0; x < clip.Width; x++)
				{
					byte index = clip.Reader.ReadByte();
					pic.SetPixel(InternalPalette.Entries[index].Color, (int)x, (int)y);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
