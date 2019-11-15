//
//  CPKColumnDataType.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK
{
	enum CPKColumnDataType : byte
	{
		Mask = 0x0f,
		Data = 0x0b,
		String = 0x0a,
		Float = 0x08,
		Long2 = 0x07,
		Long = 0x06,
		Int2 = 0x05,
		Int = 0x04,
		Short2 = 0x03,
		Short = 0x02,
		Byte2 = 0x01,
		Byte = 0x00
	}
}
