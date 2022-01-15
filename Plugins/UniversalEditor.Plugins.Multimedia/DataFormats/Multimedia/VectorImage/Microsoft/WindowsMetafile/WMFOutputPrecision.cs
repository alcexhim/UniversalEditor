//
//  WMFOutputPrecision.cs - represents the WMF OutPrecision enumeration
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

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	/// <summary>
	/// The OutPrecision enumeration defines values for output precision, which is the requirement for the
	/// font mapper to match specific font parameters, including height, width, character orientation,
	/// escapement, pitch, and font type.
	/// </summary>
	public enum WMFOutputPrecision
	{
		/// <summary>
		/// A value that specifies default behavior.
		/// </summary>
		Default = 0x00000000,
		/// <summary>
		/// A value that is returned when rasterized fonts are enumerated.
		/// </summary>
		String = 0x00000001,
		/// <summary>
		/// A value that is returned when TrueType and other outline fonts, and
		/// vector fonts are enumerated.
		/// </summary>
		Stroke = 0x00000003,
		/// <summary>
		/// A value that specifies the choice of a TrueType font when the system contains
		/// multiple fonts with the same name.
		/// </summary>
		TrueType = 0x00000004,
		/// <summary>
		/// A value that specifies the choice of a device font when the system contains
		/// multiple fonts with the same name.
		/// </summary>
		Device = 0x00000005,
		/// <summary>
		/// A value that specifies the choice of a rasterized font when the system
		/// contains multiple fonts with the same name.
		/// </summary>
		Raster = 0x00000006,
		/// <summary>
		/// A value that specifies the requirement for only TrueType fonts. If there are
		/// no TrueType fonts installed in the system, default behavior is specified.
		/// </summary>
		TrueTypeOnly = 0x00000007,
		/// <summary>
		/// A value that specifies the requirement for TrueType and other outline fonts.
		/// </summary>
		Outline = 0x00000008,
		/// <summary>
		/// A value that specifies a preference for TrueType and other
		/// outline fonts.
		/// </summary>
		ScreenOutline = 0x00000009,
		/// <summary>
		/// A value that specifies a requirement for only PostScript fonts. If there are
		/// no PostScript fonts installed in the system, default behavior is specified.
		/// </summary>
		PostScriptOnly = 0x0000000A
	}
}
