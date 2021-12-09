//
//  WMFClipPrecision.cs - provides the WMF ClipPrecision flags
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

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	/// <summary>
	/// ClipPrecision Flags specify clipping precision, which defines how to clip characters that are partially
	/// outside a clipping region.These flags can be combined to specify multiple options.
	/// </summary>
	[Flags()]
	public enum WMFClipPrecision
	{
		/// <summary>
		/// Specifies that default clipping MUST be used.
		/// </summary>
		Default = 0x00000000,
		/// <summary>
		/// This value SHOULD NOT be used.
		/// </summary>
		Character = 0x00000001,
		/// <summary>
		/// This value MAY be returned when enumerating rasterized, TrueType
		/// and vector fonts.
		/// </summary>
		Stroke = 0x00000002,
		/// <summary>
		/// This value is used to control font rotation, as follows:
		/// <list type="bullet">
		/// 	<item>
		/// 		If set, the rotation for all fonts SHOULD be determined by the
		///			orientation of the coordinate system; that is, whether the orientation
		/// 		is left-handed or right-handed.
		/// 	</item>
		/// 	<item>
		/// 		If clear, device fonts SHOULD rotate counterclockwise, but the
		/// 		rotation of other fonts SHOULD be determined by the orientation of
		/// 		the coordinate system.
		/// 	</item>
		/// </list>
		/// </summary>
		LHAngles = 0x00000010,
		/// <summary>
		/// This value SHOULD NOT be used.
		/// </summary>
		Always = 0x00000020,
		/// <summary>
		/// This value specifies that font association SHOULD<33> be turned off.
		/// </summary>
		DFADisable = 0x00000040,
		/// <summary>
		/// This value specifies that font embedding MUST be used to render
		/// document content; embedded fonts are read-only.
		/// </summary>
		Embedded = 0x00000080
	}
}
