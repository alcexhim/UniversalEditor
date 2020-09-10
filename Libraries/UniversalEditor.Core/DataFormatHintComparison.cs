//
//  DataFormatHintComparison.cs - when guessing DataFormats based on accessor, what the priority should be
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

namespace UniversalEditor
{
	/// <summary>
	/// Indicates how to determine the priority of a <see cref="DataFormat" /> when guessing based on an <see cref="Accessor" />.
	/// </summary>
	public enum DataFormatHintComparison
	{
		/// <summary>
		/// The hint comparison is unspecified for this <see cref="DataFormat" />.
		/// </summary>
		Unspecified = -1,
		/// <summary>
		/// Never return this <see cref="DataFormat" /> as a result for the given <see cref="Association" />.
		/// </summary>
		Never = 0,
		/// <summary>
		/// Return this <see cref="DataFormat" /> if and only if the file name filters listed in the <see cref="Association" /> match the file name of the <see cref="Accessor" />.
		/// </summary>
		FilterOnly = 1,
		/// <summary>
		/// Return this <see cref="DataFormat" /> if and only if the magic bytes listed in the <see cref="Association" /> match the magic bytes of the <see cref="Accessor" />.
		/// </summary>
		MagicOnly = 2,
		/// <summary>
		/// Return this <see cref="DataFormat" /> if the file name filters listed in the <see cref="Association" /> match the file name of the <see cref="Accessor" />. If no match
		/// is found, then return this <see cref="DataFormat" /> if the magic bytes listed in the <see cref="Association" /> match the magic bytes of the <see cref="Accessor" />.
		/// </summary>
		FilterThenMagic = 3,
		/// <summary>
		/// Return this <see cref="DataFormat" /> if the magic bytes listed in the <see cref="Association" /> match the magic bytes of the <see cref="Accessor" />. If no match
		/// is found, then return this <see cref="DataFormat" /> if the file name filters listed in the <see cref="Association" /> match the file name of the
		/// <see cref="Accessor" />.
		/// </summary>
		MagicThenFilter = 4,
		/// <summary>
		/// Return this <see cref="DataFormat" /> if both the magic bytes listed in the <see cref="Association" /> match the magic bytes of the
		/// <see cref="Accessor" /> AND the file name filters listed in the <see cref="Association" /> match the file name of the
		/// <see cref="Accessor" />.
		/// </summary>
		Both = 5,
		/// <summary>
		/// Always return this <see cref="DataFormat" /> as a result for the given <see cref="Association" />.
		/// </summary>
		Always = 6
	}
}
