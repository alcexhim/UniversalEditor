//
//  RARHeaderTypeV5.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.DataFormats.FileSystem.WinRAR.V5
{
	public enum RARBlockTypeV5
	{
		/// <summary>
		/// Main archive header
		/// </summary>
		Main = 0x01,
		/// <summary>
		/// File header
		/// </summary>
		File = 0x02,
		/// <summary>
		/// Service header
		/// </summary>
		Service = 0x03,
		/// <summary>
		/// Archive encryption header
		/// </summary>
		Encryption = 0x04,
		/// <summary>
		/// End of archive header
		/// </summary>
		End = 0x05
	}
}
