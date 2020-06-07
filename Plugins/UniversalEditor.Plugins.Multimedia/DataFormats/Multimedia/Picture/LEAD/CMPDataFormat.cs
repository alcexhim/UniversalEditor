//
//  CMPDataFormat.cs - provides a DataFormat for manipulating images in LEADTOOLS CMP format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.LEAD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in LEADTOOLS CMP format.
	/// </summary>
	public class CMPDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			if (pic == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string LEAD = br.ReadFixedLengthString(4);
			if (LEAD != "LEAD") throw new InvalidDataFormatException("File does not begin with \"LEAD\"");

			ushort unknown1 = br.ReadUInt16();
			ushort unknown2 = br.ReadUInt16();
			ushort unknown3 = br.ReadUInt16();
			ushort unknown4 = br.ReadUInt16();
			ushort unknown5 = br.ReadUInt16();
			ushort unknown6 = br.ReadUInt16();

			pic.Width = br.ReadUInt16();
			pic.Height = br.ReadUInt16();

			ushort unknown9 = br.ReadUInt16();
			ushort unknown10 = br.ReadUInt16();

			// starts the compressed data
			byte[] compressedData = br.ReadToEnd();
			byte[] decompressedData = null; // UniversalEditor.Compression.CompressionStream.Decompress(UniversalEditor.Compression.CompressionMethod.Deflate, compressedData);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
