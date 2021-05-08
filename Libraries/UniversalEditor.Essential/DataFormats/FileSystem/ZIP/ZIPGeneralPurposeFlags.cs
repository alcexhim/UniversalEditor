//
//  ZIPGeneralPurposeFlags.cs - indicates general-purpose attributes for a ZIP archive
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

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	/// <summary>
	/// Indicates general-purpose attributes for a ZIP archive.
	/// </summary>
	public enum ZIPGeneralPurposeFlags : short
	{
		None = 0x00,
		/// <summary>
		/// The CRC-32 and file sizes are not known when the header is written. The fields in the local header are filled with zero,
		/// and the CRC-32 and size are appended in a 12-byte structure (optionally preceded by a 4-byte signature) immediately after
		/// the compressed data.
		/// </summary>
		UnknownCRCAndFileSize = 0x08
	}
}
