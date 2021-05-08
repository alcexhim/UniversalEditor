//
//  SPC700ID666Tag.cs - provides ID666 metadata tag information for a synthesized audio file in SPC700 format
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

using System;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.SPC
{
	/// <summary>
	/// Indicates the format of the ID666 tag in the SPC700 synthesized audio file.
	/// </summary>
	public enum SPC700ID666Format
	{
		None = 0,
		Binary = 1,
		Text = 2
	}

	/// <summary>
	/// Provides ID666 metadata tag information for a synthesized audio file in SPC700 format.
	/// </summary>
	public class SPC700ID666Tag
	{
		/// <summary>
		/// Gets or sets a value indicating the format of the ID666 tag in the SPC700 synthesized audio file.
		/// </summary>
		/// <value>The format of the ID666 tag in the SPC700 synthesized audio file.</value>
		public SPC700ID666Format Format { get; set; } = SPC700ID666Format.Text;
		/// <summary>
		/// Gets or sets the title of the song.
		/// </summary>
		/// <value>The song title.</value>
		public string SongTitle { get; set; } = "";
		/// <summary>
		/// Gets or sets the title of the game from which the SPC700 audio file is dumped.
		/// </summary>
		/// <value>The title of the game from which the SPC700 audio file is dumped.</value>
		public string GameTitle { get; set; } = "";
		/// <summary>
		/// Gets or sets the name of the person who dumped the SPC700 audio file.
		/// </summary>
		/// <value>The name of the person who dumped the SPC700 audio file.</value>
		public string DumperName { get; set; } = "";
		/// <summary>
		/// Gets or sets additional text comments stored in the SPC700 audio file.
		/// </summary>
		/// <value>The additional text comments stored in the SPC700 audio file.</value>
		public string Comments { get; set; } = "";
		/// <summary>
		/// Gets or sets the date this SPC700 file was dumped.
		/// </summary>
		/// <value>The date this SPC700 file was dumped.</value>
		public DateTime DumpDate { get; set; } = DateTime.Now;
		/// <summary>
		/// Gets or sets a value indicating the seconds for fade out.
		/// </summary>
		/// <value>The fade out seconds.</value>
		public byte[] fFadeOutSeconds { get; set; } = new byte[3];
		/// <summary>
		/// Gets or sets a value indicating the length of fade out.
		/// </summary>
		/// <value>The length of fade out.</value>
		public byte[] fFadeLength { get; set; } = new byte[5];
		/// <summary>
		/// Gets or sets the name of the artist who produced this song.
		/// </summary>
		/// <value>The song artist.</value>
		public string SongArtist { get; set; } = "";
		/// <summary>
		/// Gets or sets a value indicating the default disables for this SPC700 audio file.
		/// </summary>
		/// <value><c>true</c> if default disables; otherwise, <c>false</c>.</value>
		public bool DefaultDisables { get; set; } = false;
		/// <summary>
		/// Gets or sets a value indicating the emulator used to generate the SPC700 synthesized audio file.
		/// </summary>
		/// <value>The emulator used to generate the SPC700 synthesized audio file.</value>
		public SPC700Emulator Emulator { get; set; } = SPC700Emulator.Unknown;
		/// <summary>
		/// Gets or sets 45 additional reserved bytes for this SPC700 audio file.
		/// </summary>
		/// <value>Additional 45 reserved bytes for this SPC700 audio file.</value>
		public byte[] fReserved2 { get; set; } = new byte[45];

	}
}
