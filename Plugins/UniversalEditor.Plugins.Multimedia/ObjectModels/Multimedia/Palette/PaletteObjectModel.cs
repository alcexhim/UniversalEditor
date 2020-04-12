//
//  PaletteObjectModel.cs - provides an ObjectModel for manipulating color palettes
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

using System;

namespace UniversalEditor.ObjectModels.Multimedia.Palette
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating color palettes.
	/// </summary>
	public class PaletteObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Color palette";
				_omr.Path = new string[] { "Multimedia", "Color palette" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Entries.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			PaletteObjectModel clone = (where as PaletteObjectModel);
			clone.Name = (Name.Clone() as string);
			for (int i = 0; i < Entries.Count; i++)
			{
				clone.Entries.Add(Entries[i].Clone() as PaletteEntry);
			}
		}

		/// <summary>
		/// Gets or sets the name of this palette.
		/// </summary>
		/// <value>The name of this palette.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="PaletteEntry" /> instances representing the color entries in this palette.
		/// </summary>
		/// <value>The color entries in this palette.</value>
		public PaletteEntry.PaletteEntryCollection Entries { get; } = new PaletteEntry.PaletteEntryCollection();
	}
}
