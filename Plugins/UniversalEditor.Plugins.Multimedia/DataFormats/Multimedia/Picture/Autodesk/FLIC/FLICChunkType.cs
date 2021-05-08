//
//  FLICChunkType.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.Autodesk.FLIC
{
	public enum FLICChunkType : short
	{
		None = 0x00,
		/// <summary>
		/// Compressed color map
		/// </summary>
		Color = 0x11,
		/// <summary>
		/// Line compressed -- the most common type of compression for any but the first
		/// frame. Describes the pixel difference from the previous frame.
		/// </summary>
		LineCompressed = 0x12,
		/// <summary>
		/// Set whole screen to color 0 (only occurs on the first frame).
		/// </summary>
		Black = 0x13,
		/// <summary>
		/// Bytewise run-length compression -- first frame only
		/// </summary>
		BytewiseRunLengthCompressed = 0x14,
		/// <summary>
		/// Indicates uncompressed 64000 bytes soon to follow.  For those times when
		/// compression just doesn't work!
		/// </summary>
		Copy = 0x16
	}
}
