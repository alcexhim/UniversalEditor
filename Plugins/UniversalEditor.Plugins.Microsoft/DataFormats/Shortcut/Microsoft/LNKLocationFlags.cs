//
//  LNKLocationFlags.cs - indicates attributes related to shortcut target location
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Shortcut.Microsoft
{
	/// <summary>
	/// Indicates attributes related to shortcut target location.
	/// </summary>
	[Flags()]
	public enum LNKLocationFlags
	{
		None = 0x00000000,
		/// <summary>
		/// The linked file is on a volume. If set, the volume information and the local path
		/// contain data.
		/// </summary>
		VolumeIDAndLocalBasePath = 0x0001,
		/// <summary>
		/// The linked file is on a network share. If set, the network share information and
		/// common path contain data.
		/// </summary>
		/// <remarks>
		/// Although [MS-SHLLINK] states that when the CommonNetworkRelativeLinkAndPathSuffix
		/// location flag is not set the offset to the network share information should be zero,
		/// the value can still be set, but is not necessarily valid. This behavior was seen on
		/// Windows 95.
		/// </remarks>
		CommonNetworkRelativeLinkAndPathSuffix = 0x0002
	}
}
