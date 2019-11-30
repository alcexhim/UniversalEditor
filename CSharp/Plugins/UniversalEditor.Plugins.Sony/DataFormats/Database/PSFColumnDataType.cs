//
//  PSFColumnDataType.cs
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
namespace UniversalEditor.Plugins.Sony.DataFormats.Database
{
	public enum PSFColumnDataType : ushort
	{
		/// <summary>
		/// UTF-8 Special mode, NOT <see langword="null"/>-terminated
		/// </summary>
		UTF8S = 4,
		/// <summary>
		/// UTF-8 character string, <see langword="null"/>-terminated
		/// </summary>
		UTF8Z = 516,
		/// <summary>
		/// 32-bit integer, unsigned
		/// </summary>
		UInt32 = 1028
	}
}
