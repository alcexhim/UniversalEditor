//
//  WMFMixMode.cs
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
	/// Specifies the background mix mode for text, hatched brushes, and other
	/// nonsolid pen styles.
	/// </summary>
	public enum WMFMixMode
	{
		None = 0x0000,
		/// <summary>
		/// The background remains untouched.
		/// </summary>
		Transparent = 0x0001,
		/// <summary>
		/// The background is filled with the background color that is currently defined in the playback
		/// device context before the text, hatched brush, or pen is drawn.
		/// </summary>
		Opaque = 0x0002
	}
}
