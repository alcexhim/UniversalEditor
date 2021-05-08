//
//  ByteOrderMark.cs - indicates the type of byte-order mark to write at the beginning of a plain text file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Text.Plain
{
	/// <summary>
	/// Indicates the type of byte-order mark to write at the beginning of a plain text file.
	/// </summary>
	public enum ByteOrderMark
	{
		None,
		UTF8,
		UTF16LittleEndian,
		UTF16BigEndian
	}
}
