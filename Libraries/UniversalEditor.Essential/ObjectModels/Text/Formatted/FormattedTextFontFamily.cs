//
//  FormattedTextFontFamily.cs - indicates the font family for a defined font in a FormattedTextObjectModel
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

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	/// <summary>
	/// Indicates the font family for a defined font in a <see cref="FormattedTextObjectModel" />.
	/// </summary>
	public enum FormattedTextFontFamily
	{
		/// <summary>
		/// Unknown or default fonts
		/// </summary>
		None = 0,
		/// <summary>
		/// Roman, proportionally spaced serif fonts (e.g. Times New Roman, Palatino)
		/// </summary>
		Roman = 1,
		/// <summary>
		/// Swiss, proportionally spaced sans serif fonts (e.g. Arial)
		/// </summary>
		Swiss = 2,
		/// <summary>
		/// Fixed-pitch serif and sans serif fonts (e.g. Courier New, Pica)
		/// </summary>
		Modern = 3,
		/// <summary>
		/// Script fonts (e.g. Cursive)
		/// </summary>
		Script = 4,
		/// <summary>
		/// Decorative fonts (e.g. Old English, ITC Zapf Chancery)
		/// </summary>
		Decor = 5,
		/// <summary>
		/// Technical, symbol, and mathematical fonts (e.g. Symbol)
		/// </summary>
		Tech = 6,
		/// <summary>
		/// Arabic, Hebrew, or other bidirectional font (e.g. Miriam)
		/// </summary>
		Bidi = 7
	}
}
