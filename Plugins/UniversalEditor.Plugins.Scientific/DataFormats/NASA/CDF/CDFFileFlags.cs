//
//  CDFFileFlags.cs - flags describing the various aspects of a CDF file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.Plugins.Scientific.DataFormats.NASA.CDF
{
	/// <summary>
	/// Flags describing the various aspects of a CDF file.
	/// </summary>
	[Flags()]
	public enum CDFFileFlags
	{
		/// <summary>
		/// The majority of variable values within a variable record. Set indicates row - majority. Clear indicates column-majority.
		/// </summary>
		RowMajority = 0x00000001,
		/// <summary>
		/// The file format of the CDF. Set indicates single-file. Clear indicates multi-file.
		/// </summary>
		SingleFile = 0x00000002
	}
}
