//
//  NewLineSequence.cs - define CR, LF, and CR/LF new line sequences
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

namespace UniversalEditor.IO
{
	public enum NewLineSequence
	{
		/// <summary>
		/// Determines the new line sequence based on the system default (CR on Mac OS up to version 9, LF on Linux,
		/// CRLF on Windows).
		/// </summary>
		Default = -1,
		/// <summary>
		/// Uses the carriage return ('\r', ^M, 0x0D) as the new line sequence.
		/// </summary>
		CarriageReturn = 1,
		/// <summary>
		/// Uses the line feed ('\n', ^J, 0x0A) as the new line sequence.
		/// </summary>
		LineFeed = 2,
		/// <summary>
		/// Uses a combination of carriage return and line feed ("\r\n", ^M^J, {0x0D, 0x0A}) as the new line sequence.
		/// </summary>
		CarriageReturnLineFeed = 3,
		/// <summary>
		/// Uses a combination of line feed and carriage return ("\n\r", ^J^M, {0x0A, 0x0D}) as the new line sequence.
		/// </summary>
		LineFeedCarriageReturn = 4
	}
}
