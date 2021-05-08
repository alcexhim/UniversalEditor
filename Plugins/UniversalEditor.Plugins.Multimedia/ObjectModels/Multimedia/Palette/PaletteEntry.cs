//
//  PaletteEntry.cs - represents a color entry in a palette
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia.Palette
{
	/// <summary>
	/// Represents a color entry in a palette.
	/// </summary>
	public class PaletteEntry : ICloneable
	{
		public class PaletteEntryCollection
			: System.Collections.ObjectModel.Collection<PaletteEntry>
		{
			public PaletteEntry Add(Color color, string colorName = "")
			{
				PaletteEntry entry = new PaletteEntry();
				entry.Name = colorName;
				entry.Color = color;

				Add(entry);
				return entry;
			}
		}

		public PaletteEntry(Color color = default(Color), string name = "")
		{
			Name = name;
			Color = color;
		}

		/// <summary>
		/// Gets or sets the name of this <see cref="PaletteEntry" />.
		/// </summary>
		/// <value>The name of this <see cref="PaletteEntry" />.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the color of this <see cref="PaletteEntry" />.
		/// </summary>
		/// <value>The color of this <see cref="PaletteEntry" />.</value>
		public Color Color { get; set; } = Color.Empty;

		public object Clone()
		{
			PaletteEntry clone = new PaletteEntry();
			clone.Name = Name;
			clone.Color = Color;
			return clone;
		}
	}
}
