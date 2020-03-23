//
//  N64CountryCode.cs
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
namespace UniversalEditor.DataFormats.Executable.Nintendo.N64
{
	public enum N64CountryCode : byte
	{
		/// <summary>
		/// Beta - '7'
		/// </summary>
		Beta = 0x37,
		/// <summary>
		/// Asian (NTSC) - 'A'
		/// </summary>
		Asian = 0x41,
		/// <summary>
		/// Brazilian - 'B'
		/// </summary>
		Brazilian = 0x42,
		/// <summary>
		/// Chinese - 'C'
		/// </summary>
		Chinese,
		/// <summary>
		/// German - 'D'
		/// </summary>
		German = 0x44,
		/// <summary>
		/// North America - 'E'
		/// </summary>
		NorthAmerica = 0x45,
		/// <summary>
		/// French - 'F'
		/// </summary>
		French = 0x46,
		/// <summary>
		/// LodgeNet Gateway64 (NTSC) - 'G'
		/// </summary>
		Gateway64NTSC = 0x47,
		/// <summary>
		/// Dutch - 'H'
		/// </summary>
		Dutch = 0x48,
		/// <summary>
		/// Italian - 'I'
		/// </summary>
		Italian = 0x49,
		/// <summary>
		/// Japanese - 'J'
		/// </summary>
		Japanese = 0x4A,
		/// <summary>
		/// Korean - 'K'
		/// </summary>
		Korean = 0x4B,
		/// <summary>
		/// LodgeNet Gateway64 (PAL) - 'L'
		/// </summary>
		Gateway64PAL = 0x4C,
		/// <summary>
		/// Canadian ('N')
		/// </summary>
		Canadian = 0x4E,
		/// <summary>
		/// European (basic spec) - 'P'
		/// </summary>
		EuropeanBasic = 0x50,
		/// <summary>
		/// Spanish - 'S'
		/// </summary>
		Spanish = 0x53,
		/// <summary>
		/// Australian - 'U'
		/// </summary>
		Australian = 0x55,
		/// <summary>
		/// Scandinavian - 'W'
		/// </summary>
		Scandinavian = 0x57,
		/// <summary>
		/// European - 'X'
		/// </summary>
		European1 = 0x58,
		/// <summary>
		/// European - 'Y'
		/// </summary>
		European2 = 0x59
	}
}
