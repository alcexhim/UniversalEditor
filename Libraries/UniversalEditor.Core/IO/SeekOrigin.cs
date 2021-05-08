//
//  SeekOrigin.cs - provide equivalent to System.IO.SeekOrigin (may be removed)
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
	/// <summary>
	/// Provides various options for determining where seeking within a stream should start.
	/// </summary>
	public enum SeekOrigin
	{
		/// <summary>
		/// Indicates that seeking should start from the beginning of a stream.
		/// </summary>
		Begin = 0,
		/// <summary>
		/// Indicates that seeking should start from the current position within a stream.
		/// </summary>
		Current = 1,
		/// <summary>
		/// Indicates that seeking should start from the end of a stream. This usually necessiates that the amount
		/// be specified as a negative value.
		/// </summary>
		End = 2
	}
}
