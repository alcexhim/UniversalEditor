//
//  TextPropertyListSettings.cs - represents settings for the TextPropertyListDataFormat parser
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

namespace UniversalEditor.DataFormats.PropertyList.Text
{
	/// <summary>
	/// Represents settings for the <see cref="TextPropertyListDataFormat" /> parser.
	/// </summary>
	public class TextPropertyListSettings
	{
		/// <summary>
		/// Gets or sets an array of <see cref="string" />s that signal the start of a comment line.
		/// </summary>
		/// <value>The <see cref="string" />s that signal the start of a comment line.</value>
		public string[] CommentSignals { get; set; } = new string[] { ";" };
		/// <summary>
		/// Gets or sets an array of <see cref="string" />s that separate property names from property values.
		/// </summary>
		/// <value>The <see cref="string" />s that separate property names from property values.</value>
		public string[] PropertyNameValueSeparators { get; set; } = new string[] { " ", "\t" };
		/// <summary>
		/// Gets or sets the <see cref="string" /> which begins the delimited area inside which property name separators and comment signals are ignored.
		/// </summary>
		/// <value>The <see cref="string" /> which begins the delimited area inside which property name separators and comment signals are ignored.</value>
		public string IgnoreBegin { get; set; } = "\"";
		/// <summary>
		/// Gets or sets the <see cref="string" /> which ends the delimited area inside which property name separators and comment signals are ignored.
		/// </summary>
		/// <value>The <see cref="string" /> which ends the delimited area inside which property name separators and comment signals are ignored.</value>
		public string IgnoreEnd { get; set; } = "\"";
	}
}
