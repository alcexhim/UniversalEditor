//
//  MPQFormatVersion.cs - indicates the format version of an MPQ archive
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

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
	/// <summary>
	/// Indicates the format version of an MPQ archive.
	/// </summary>
	public enum MPQFormatVersion
	{
		/// <summary>
		/// Format 1 (up to The Burning Crusade)
		/// </summary>
		Format1 = 0,
		/// <summary>
		/// Format 2 (The Burning Crusade and newer)
		/// </summary>
		Format2 = 1,
		/// <summary>
		/// Format 3 (WoW - Cataclysm beta or newer)
		/// </summary>
		Format3 = 2,
		/// <summary>
		/// Format 4 (WoW - Cataclysm beta or newer)
		/// </summary>
		Format4 = 3
	}
}
