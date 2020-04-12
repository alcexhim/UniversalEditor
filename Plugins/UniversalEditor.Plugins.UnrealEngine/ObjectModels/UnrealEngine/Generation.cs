//
//  Generation.cs - indicates export count and name count for a generation in an Unreal Engine package file
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

namespace UniversalEditor.ObjectModels.UnrealEngine
{
	/// <summary>
	/// Indicates export count and name count for a generation in an Unreal Engine package file.
	/// </summary>
	public class Generation
	{
		public class GenerationCollection
			: System.Collections.ObjectModel.Collection<Generation>
		{
		}

		/// <summary>
		/// Gets or sets the number of exports in this <see cref="Generation" />.
		/// </summary>
		/// <value>The export count for this <see cref="Generation" />.</value>
		public uint ExportCount { get; set; } = 0;
		/// <summary>
		/// Gets or sets the number of names in this <see cref="Generation" />.
		/// </summary>
		/// <value>The name count for this <see cref="Generation" />.</value>
		public uint NameCount { get; set; } = 0;

		public override string ToString()
		{
			return ExportCount.ToString() + " exports, " + NameCount.ToString() + " names";
		}
	}
}
