//
//  TLCSequence.cs - internal structure representing a sequence in a TLCD archive
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

namespace UniversalEditor.DataFormats.FileSystem.TLCD.Internal
{
	/// <summary>
	/// Internal structure representing a sequence in a TLCD archive.
	/// </summary>
	internal struct TLCSequence
	{
		public string filetype;
		public uint offset;
		public uint length;
		public uint id;
		public uint unknown4;

		public override string ToString()
		{
			return id.ToString() + "." + filetype + ": { " + offset.ToString() + ", " + length.ToString() + " }";
		}
	}
}
