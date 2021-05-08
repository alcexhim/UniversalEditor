//
//  VHDHardDiskGeometry.cs - describes the geometry of a virtual hard disk in Microsoft Virtual PC VHD format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.VirtualHardDisk
{
	/// <summary>
	/// Describes the geometry of a virtual hard disk in Microsoft Virtual PC VHD format.
	/// </summary>
	public class VHDHardDiskGeometry
	{
		private short mvarCylinders = 0;
		/// <summary>
		/// This field stores the cylinder value for the hard disk.
		/// </summary>
		public short Cylinders { get { return mvarCylinders; } set { mvarCylinders = value; } }

		private byte mvarHeads = 0;
		/// <summary>
		/// This field stores the heads value for the hard disk.
		/// </summary>
		public byte Heads { get { return mvarHeads; } set { mvarHeads = value; } }
		private byte mvarSectors = 0;
		/// <summary>
		/// This field stores the sectors per track value for the hard disk.
		/// </summary>
		public byte Sectors { get { return mvarSectors; } set { mvarSectors = value; } }
	}
}
