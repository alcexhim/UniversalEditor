//
//  BMLHeaderr.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using System.Runtime.InteropServices;

namespace UniversalEditor.DataFormats.Markup.BML.Internal
{
	internal struct BMLHeader
	{
		[MarshalAs(UnmanagedType.LPStr, SizeConst = 4)]
		public string signature;
		public uint formatVersion;
		public BMLFlags flags;
		public uint textOffset;
		public uint textLength;
		public uint stringTableOffset;
		public uint stringTableLength;
		public uint dataOffset;
		public uint dataLength;
	}
}
