//
//  HFSDInfo.cs - internal structure representing the HFS_DINFO for an HFS filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	/// <summary>
	/// Attributes used with the <see cref="HFSDInfo" /> structure.
	/// </summary>
	internal enum HFSDInfoFlags : short
	{
		Inited = 0x0100,
		Locked = 0x1000,
		Invisible = 0x4000
	}
	/// <summary>
	/// Internal structure representing the HFS_DINFO for an HFS filesystem.
	/// </summary>
	internal struct HFSDInfo
	{
		// hfs_finfo
		public HFSRect rect;
		public HFSDInfoFlags flags;
		public HFSPoint location;
		public short view;
	}
}
