//
//  RTMLSettings.cs - represents settings for the RTMLDataFormat parser
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

namespace UniversalEditor.DataFormats.RichTextMarkup.RTML
{
	/// <summary>
	/// Represents settings for the RTMLDataFormat parser.
	/// </summary>
	public class RTMLSettings
	{
		/// <summary>
		/// The character that defines the beginning of an RTML group.
		/// </summary>
		public char GroupBeginChar { get; set; }
		/// <summary>
		/// The character that defines the end of an RTML group.
		/// </summary>
		public char GroupEndChar { get; set; }
		/// <summary>
		/// The character that defines the beginning of an RTML tag.
		/// </summary>
		public char TagBeginChar { get; set; }
	}
}
