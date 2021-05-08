//
//  SPC700ExtendedID666Tag.cs - provides extended ID666 metadata tag information for a synthesized audio file in SPC700 format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2014-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	/// <summary>
	/// Provides extended ID666 metadata tag information for a synthesized audio file in SPC700 format.
	/// </summary>
	public class SPC700ExtendedID666Tag
	{
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:ExtensibleDataStorage.Media.SPC700.SPC700ExtendedID666Tag"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if the extended ID666 tag is enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; set; } = false;
	}
}
