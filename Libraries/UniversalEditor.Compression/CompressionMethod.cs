//
//  CompressionMethod.cs - an enumeration of available known compression methods
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

namespace UniversalEditor.Compression
{
	/// <summary>
	/// An enumeration of available known compression methods.
	/// </summary>
	public enum CompressionMethod : sbyte
	{
		Unknown = -1,
		None = 0,
		Bzip2,
		Bzip2Solid,
		Deflate,
		Deflate64,
		Gzip,
		LZMA,
		LZMASolid,
		LZSS,
		LZH,
		LZRW1,
		LZW,
		LZX,
		PPPMd,
		Quantum,
		XMemLZX,
		Zlib,
		ZlibSolid
	}
}
