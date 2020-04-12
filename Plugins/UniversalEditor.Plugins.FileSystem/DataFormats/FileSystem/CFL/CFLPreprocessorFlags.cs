//
//  CFLPreprocessorFlags.cs - indicates preprocessor attributes for a CFL archive
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

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
	/// <summary>
	/// Indicates preprocessor attributes for a CFL archive.
	/// </summary>
	public enum CFLPreprocessorFlags
	{
		/// <summary>
		/// No preprocess
		/// </summary>
		None = 0x00000000,
		/// <summary>
		/// 8-bit delta encoding
		/// </summary>
		DeltaEncoding8Bit = 0x00010000,
		/// <summary>
		/// 16-bit delta encoding
		/// </summary>
		DeltaEncoding16Bit = 0x00020000,
		/// <summary>
		/// 32-bit delta encoding
		/// </summary>
		DeltaEncoding32Bit = 0x00030000,
		/// <summary>
		/// Burrows-Wheeler transform
		/// </summary>
		BurrowsWheelerTransform = 0x00040000,
		/// <summary>
		/// 8-bit 'turn' encoding
		/// </summary>
		TurnEncoding8Bit = 0x00050000,
		/// <summary>
		/// 16-bit 'turn' encoding
		/// </summary>
		TurnEncoding16Bit = 0x00060000,
		/// <summary>
		/// 24-bit 'turn' encoding
		/// </summary>
		TurnEncoding24Bit = 0x00070000,
		/// <summary>
		/// 32-bit 'turn' encoding
		/// </summary>
		TurnEncoding32Bit = 0x00080000,
	}
}
