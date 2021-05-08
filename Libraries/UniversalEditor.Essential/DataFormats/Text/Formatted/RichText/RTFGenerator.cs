//
//  RTFGenerator.cs - predefined RTF generator tag values
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

namespace UniversalEditor.DataFormats.Text.Formatted.RichText
{
	/// <summary>
	/// Predefined RTF generator tag values.
	/// </summary>
	public static class RTFGenerator
	{
		private static string mvarWordPadWindows7 = "Msftedit 5.41.21.2509";
		/// <summary>
		/// Gets the RTF generator tag value for Microsoft WordPad on Windows 7.
		/// </summary>
		public static string WordPadWindows7 { get { return mvarWordPadWindows7; } }
	}
}
