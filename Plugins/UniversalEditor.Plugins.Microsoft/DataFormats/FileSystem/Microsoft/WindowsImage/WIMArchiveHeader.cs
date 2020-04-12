//
//  WIMArchiveHeader.cs - represents a Windows Image (WIM) archive header
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	/// <summary>
	/// Represents a Windows Image (WIM) archive header.
	/// </summary>
	public struct WIMArchiveHeader
	{
		/// <summary>
		/// Signature that identifies the file as a .wim file. Value is set to “MSWIM\0\0”.
		/// </summary>
		public string magic;
		/// <summary>
		/// Size of the WIM header in bytes.
		/// </summary>
		public uint cbSize;
		/// <summary>
		/// The current version of the .wim file. This number will increase if the format of the .wim file changes.
		/// </summary>
		public uint dwVersion;
		public WIMArchiveFlags dwFlags;
		/// <summary>
		/// Size of the compressed .wim file in bytes.
		/// </summary>
		public uint dwCompressionSize;
		/// <summary>
		/// A unique identifier for this WIM archive.
		/// </summary>
		public Guid gWIMGuid;
		/// <summary>
		/// The part number of the current .wim file in a spanned set. This value is 1, unless the data of the .wim file was split into multiple parts (.swm).
		/// </summary>
		public ushort usPartNumber;
		/// <summary>
		/// The total number of .wim file parts in a spanned set.
		/// </summary>
		public ushort usTotalParts;
		/// <summary>
		/// The number of images contained in the .wim file.
		/// </summary>
		public uint dwImageCount;
		public WIMResourceHeaderDiskShort rhOffsetTable;
		public WIMResourceHeaderDiskShort rhXmlData;
		public WIMResourceHeaderDiskShort rhBootMetadata;
		public uint dwBootIndex;
		public WIMResourceHeaderDiskShort rhIntegrity;
		public byte[/*60*/] bUnused;
	}
}
