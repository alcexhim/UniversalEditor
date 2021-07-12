//
//  EXRFlags.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.EXR
{
	[Flags()]
	public enum EXRFlags
	{
		/// <summary>
		/// If set, this is a regular single-part image and the pixels are stored
		/// as tiles, and <see cref="NonImage" /> and <see cref="Multipart"/>
		/// flags must NOT be set.
		///
		/// This bit is for backwards compatibility with older libraries: it is
		/// only set when there is one "normal" tiled image in the file.
		/// </summary>
		SingleTile = 0x200,
		/// <summary>
		/// If set, the maximum length of attribute names, attribute type names,
		/// and channel names is 255 bytes. If not set, the maximum length is
		/// 31 bytes.
		/// </summary>
		LongName = 0x400,
		/// <summary>
		/// If set, there is at least one part which is not a regular scan line
		/// image or regular tiled image (that is, it is a deep format). If not
		/// set, all parts are entirely single or multiple scan line or tiled
		/// images.
		/// </summary>
		NonImage = 0x800,
		/// <summary>
		/// If set, the file does not contain exactly 1 part and the 'end of
		/// header' byte must be included at the end of each header part, and
		/// the part number fields must be added to the chunks. If not set, this
		/// is not a multi-part file and the 'end of header' byte and part number
		/// fields in chunks must be omitted. New in 2.0.
		/// </summary>
		Multipart = 0x1000
	}
}
