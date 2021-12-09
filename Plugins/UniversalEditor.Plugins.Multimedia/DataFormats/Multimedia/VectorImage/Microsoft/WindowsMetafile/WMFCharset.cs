//
//  WMFDataFormat.cs
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
namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	public enum WMFCharset : byte
	{
		ANSI = 0x00000000,
		Default = 0x00000001,
		Symbol = 0x00000002,
		Mac = 0x0000004D,
		ShiftJIS = 0x00000080,
		Hangul = 0x00000081,
		Johab = 0x00000082,
		GB2312 = 0x00000086,
		ChineseBig5 = 0x00000088,
		Greek = 0x000000A1,
		Turkish = 0x000000A2,
		Vietnamese = 0x000000A3,
		Hebrew = 0x000000B1,
		Arabic = 0x000000B2,
		Baltic = 0x000000BA,
		Russian = 0x000000CC,
		Thai = 0x000000DE,
		EastEurope = 0x000000EE,
		OEM = 0x000000FF
	}
}
