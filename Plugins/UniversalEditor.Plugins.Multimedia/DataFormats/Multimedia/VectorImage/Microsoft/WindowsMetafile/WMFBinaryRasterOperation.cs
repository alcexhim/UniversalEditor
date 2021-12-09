//
//  WMFBinaryRasterOperation.cs
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public enum WMFBinaryRasterOperation
	{
		Black = 0x0001,
		NotMergePen = 0x0002,
		MaskNotPen = 0x0003,
		NotCopyPen = 0x0004,
		MaskPenNot = 0x0005,
		Not = 0x0006,
		XorPen = 0x0007,
		NotMaskPen = 0x0008,
		MaskPen = 0x0009,
		NotXorPen = 0x000A,
		Nop = 0x000B,
		MergeNotPen = 0x000C,
		CopyPen = 0x000D,
		MergePenNot = 0x000E,
		MergePen = 0x000F,
		White = 0x0010
	}
}
