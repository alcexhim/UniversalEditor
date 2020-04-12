//
//  Costume.cs - represents a 3D costume model for a Character in a Concertroid Performance
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

namespace UniversalEditor.ObjectModels.Concertroid
{
	/// <summary>
	/// Indicates whether the 3D model associated with the <see cref="Costume" /> is rendered in addition to, or in place of, the 3D base model
	/// associated with the <see cref="Character" />.
	/// </summary>
	public enum CostumeBehavior
	{
		Overlay = 0,
		Replace = 1
	}
	/// <summary>
	/// Represents a 3D costume model for a <see cref="Character" /> in a Concertroid <see cref="Concert.Performance" />.
	/// </summary>
	public class Costume : ICloneable
	{
		public class CostumeCollection
			: System.Collections.ObjectModel.Collection<Costume>
		{
		}
		/// <summary>
		/// Gets or sets a value indicating whether the 3D model associated with the <see cref="Costume" /> is rendered in addition to, or in place of,
		/// the 3D base model associated with the <see cref="Character" />.
		/// </summary>
		/// <value>
		/// Indicates whether the 3D model associated with the <see cref="Costume" /> is rendered in addition to, or in place of, the 3D base model
		/// associated with the <see cref="Character" />.
		/// </value>
		public CostumeBehavior Behavior { get; set; } = CostumeBehavior.Replace;
		/// <summary>
		/// Gets or sets the title of this <see cref="Costume" />.
		/// </summary>
		/// <value>The title of this <see cref="Costume" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the full path to the 3D model file associated with this <see cref="Costume" />.
		/// </summary>
		/// <value>The full path to the 3D model file associated with this <see cref="Costume" />.</value>
		public string ModelFileName { get; set; } = String.Empty;

		public object Clone()
		{
			Costume clone = new Costume();
			clone.Title = (Title.Clone() as string);
			clone.ModelFileName = (ModelFileName.Clone() as string);
			clone.Behavior = Behavior;
			return clone;
		}
	}
}
