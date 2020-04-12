//
//  ModelMorphGroup.cs - represents a 3D model morph group
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	/// <summary>
	/// Represents a 3D model morph group.
	/// </summary>
	public class ModelMorphGroup : ModelMorph
	{
		/// <summary>
		/// Gets or sets the morph rate for this group.
		/// </summary>
		/// <value>The morph rate for this group.</value>
		public float MorphRate { get; set; } = 0f;

		public override object Clone()
		{
			return new ModelMorphGroup
			{
				MorphRate = this.MorphRate,
				Name = base.Name
			};
		}
	}
}
