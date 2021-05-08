//
//  N64MediaFormat.cs - indicates the media format for a Nintendo 64 media
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

namespace UniversalEditor.DataFormats.Executable.Nintendo.N64
{
	/// <summary>
	/// Indicates the media format for a Nintendo 64 media.
	/// </summary>
	public enum N64MediaFormat : uint
	{
		CartridgeExpandable = 67,
		SixtyFourDD = 68,
		SixtyFourDDExpansion = 69,
		Cartridge = 78,
		Aleck64 = 90
	}
}
