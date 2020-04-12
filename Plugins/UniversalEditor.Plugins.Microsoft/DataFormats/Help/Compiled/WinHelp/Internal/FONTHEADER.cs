//
//  FONTHEADER.cs - internal structure representing FONTHEADER for WinHelp files
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

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Internal structure representing FONTHEADER for WinHelp files.
	/// </summary>
	internal struct FONTHEADER
	{
		/// <summary>
		/// Number of face names
		/// </summary>
		public ushort NumFacenames;
		/// <summary>
		/// Number of font descriptors
		/// </summary>
		public ushort NumDescriptors;
		/// <summary>
		/// Start of array of face names relative to &NumFacenames
		/// </summary>
		public ushort FacenamesOffset;
		/// <summary>
		/// Start of array of font descriptors relative to &NumFacenames
		/// </summary>
		public ushort DescriptorsOffset;

		// only if FacenamesOffset >= 12
		/// <summary>
		/// Number of style descriptors
		/// </summary>
		public ushort NumStyles;
		/// <summary>
		/// Start of array of style descriptors relative to &NumFacenames
		/// </summary>
		public ushort StyleOffset;

		// only if FacenamesOffset >= 16
		/// <summary>
		/// Number of character mapping tables
		/// </summary>
		public ushort NumCharMapTables;
		/// <summary>
		/// Start of array of character mapping table names relative to &NumFacenames
		/// </summary>
		public ushort CharMapTableOffset;
	}
}
