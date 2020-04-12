//
//  ModelMorphBone.cs - represents a 3D model morph that controls bone rotation
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
	/// Represents a 3D model morph that controls bone rotation.
	/// </summary>
	public class ModelMorphBone : ModelMorph
	{
		/// <summary>
		/// Gets or sets the index of the bone affected by this morph.
		/// </summary>
		/// <value>The index of the bone affected by this morph.</value>
		public long BoneIndex { get; set; } = 0L;
		/// <summary>
		/// Gets or sets the travel distance for this morph.
		/// </summary>
		/// <value>The travel distance for this morph.</value>
		public PositionVector3 TravelDistance { get; set; } = default(PositionVector3);
		/// <summary>
		/// Gets or sets the rotation for this morph.
		/// </summary>
		/// <value>The rotation for this morph.</value>
		public PositionVector4 Rotation { get; set; } = default(PositionVector4);

		public override object Clone()
		{
			return new ModelMorphBone
			{
				BoneIndex = this.BoneIndex,
				Name = base.Name,
				Rotation = new PositionVector4(this.Rotation.X, this.Rotation.Y, this.Rotation.Z, this.Rotation.W),
				TravelDistance = new PositionVector3(this.TravelDistance.X, this.Rotation.Y, this.Rotation.Z)
			};
		}
	}
}
