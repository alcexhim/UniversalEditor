//
//  JSONPresetSettings.cs - indicates the preset settings to use for JSONSettings for a JSONDataFormat
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

namespace UniversalEditor.DataFormats.PropertyList.JSON
{
	/// <summary>
	/// Indicates the preset settings to use for <see cref="JSONSettings" /> for a <see cref="JSONDataFormat" />.
	/// </summary>
	public enum JSONPresetSettings
	{
		JSON = 0,
		ExtendedINI = 1
	}
}
