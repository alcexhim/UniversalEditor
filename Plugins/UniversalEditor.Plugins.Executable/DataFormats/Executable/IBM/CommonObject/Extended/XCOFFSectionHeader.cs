//
//  XCOFFSectionHeader.cs - describes a header for a section in an Extended Common Object File Format executable
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

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	/// <summary>
	/// Describes a header for a section in an Extended Common Object File Format executable.
	/// </summary>
	public abstract class XCOFFSectionHeader
	{
		private string mvarSectionName = "";
		public string SectionName { get { return mvarSectionName; } set { mvarSectionName = value; } }
	}
}
