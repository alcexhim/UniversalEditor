//
//  CFHEADER.cs - internal structure representing a Microsoft Cabinet archive header
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
	/// <summary>
	/// Internal structure representing a Microsoft Cabinet archive header.
	/// </summary>
	internal struct CFHEADER
	{
		public string signature;
		public uint reserved1;
		public uint cabinetFileSize;
		public uint reserved2;
		public uint firstFileOffset;
		public uint reserved3;
		public byte versionMinor;
		public byte versionMajor;

		public ushort folderCount;
		public ushort fileCount;
		public CABFlags flags;

		public ushort setID;
		public ushort iCabinet;

		public ushort cabinetReservedAreaSize;
		public byte folderReservedAreaSize;
		public byte datablockReservedAreaSize;

		public byte[] reservedArea;

		public string previousCabinetName;
		public string previousDiskName;

		public string nextCabinetName;
		public string nextDiskName;
	}
}
