//
//  TMHDataFormat.cs - provides a DataFormat for manipulating images in PlayStation Portable TMH format
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

// 2013-05-19
using System;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.TMH
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating images in PlayStation Portable TMH format.
	/// </summary>
	public class TMHDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
				_dfr.Sources.Add("https://github.com/svanheulen/mhff/blob/master/psp/tmh.py"); // Python source, GPLv3.0
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			PictureObjectModel pic = (objectModel as PictureObjectModel);
			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(8);
			if (signature != ".TMH0.14") throw new InvalidDataFormatException("File does not begin with \".TMH0.14\"");

			uint unknown1 = br.ReadUInt32();
			uint unknown2 = br.ReadUInt32();
			uint unknown3 = br.ReadUInt32();
			uint unknown4 = br.ReadUInt32();
			uint unknown5 = br.ReadUInt32();
			uint unknown6 = br.ReadUInt32();
			uint unknown7 = br.ReadUInt32();
			uint unknown8 = br.ReadUInt32();
			uint unknown9 = br.ReadUInt32();
			ushort unknown10a = br.ReadUInt16();
			ushort unknown10b = br.ReadUInt16();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
