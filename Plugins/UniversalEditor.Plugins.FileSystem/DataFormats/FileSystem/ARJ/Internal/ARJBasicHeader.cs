//
//  ARJBasicHeader.cs - internal structure representing an ARJ basic header
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

namespace UniversalEditor.DataFormats.FileSystem.ARJ.Internal
{
	/// <summary>
	/// Internal structure representing an ARJ basic header.
	/// </summary>
	internal struct ARJBasicHeader
	{
		/// <summary>
		/// Size of header including extra data.
		/// </summary>
		public byte HeaderSize;
		/// <summary>
		/// Archiver version number.
		/// </summary>
		public byte VersionNumber;
		/// <summary>
		/// Minimum version needed to extract.
		/// </summary>
		public byte MinimumRequiredVersion;
		/// <summary>
		/// Host operating system.
		/// </summary>
		public ARJHostOperatingSystem HostOperatingSystem;
		public ARJInternalFlags InternalFlags;
		public ARJCompressionMethod CompressionMethod;
		public ARJFileType FileType;
		public byte Reserved;
		/// <summary>
		/// Date/Time of original file in MS-DOS format.
		/// </summary>
		public int Timestamp;
		public int CompressedSize;
		public int OriginalSize;
		public int OriginalCRC32;
		/// <summary>
		/// Filespec position in filename.
		/// </summary>
		public short FileSpecPosition;
		public short FileAttributes;
		public short HostData;
		public int BasicHeaderCRC32;
		public string OriginalFileName;
	}
}
