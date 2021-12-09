//
//  WMFLayout.cs
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
	[Flags()]
	public enum WMFLayout
	{
		/// <summary>
		/// Sets the default horizontal layout to be left-to-right.
		/// </summary>
		LeftToRight = 0x0000,
		/// <summary>
		/// Sets the default horizontal layout to be right-to-left. Switching to this layout SHOULD
		/// cause the mapping mode in the playback device context to become MM_ISOTROPIC.
		/// </summary>
		RightToLeft = 0x0001,
		/// <summary>
		/// Disables mirroring of bitmaps that are drawn by
		/// META_BITBLT Record (section 2.3.1.1) and META_STRETCHBLT Record (section 2.3.1.5)
		/// operations, when the layout is right-to-left.
		/// </summary>
		BitmapOrientationPreserved = 0x0008
	}
}
