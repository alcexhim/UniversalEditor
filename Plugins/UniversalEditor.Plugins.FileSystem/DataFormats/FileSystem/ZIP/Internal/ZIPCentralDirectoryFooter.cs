//
//  ZIPCentralDirectoryFooter.cs - internal structure representing the central directory footer in a ZIP archive
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

namespace UniversalEditor.DataFormats.FileSystem.ZIP.Internal
{
	/// <summary>
	/// Internal structure representing the central directory footer in a ZIP archive.
	/// </summary>
	internal struct ZIPCentralDirectoryFooter
	{
		public uint unknown1;
		public ushort unknown2;
		public ushort unknown3;
		public uint centralDirectoryLength;
		public uint centralDirectoryOffset;
		public ushort unknown4;
	}
}
