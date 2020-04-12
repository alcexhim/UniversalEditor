//
//  ModelJoint.cs - represents a bone joint in a 3D model
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

using System;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a bone joint in a 3D model.
	/// </summary>
	public class ModelJoint : ICloneable
	{
		public class ModelJointCollection
			: System.Collections.ObjectModel.Collection<ModelJoint>
		{
		}

		/// <summary>
		/// Gets or sets the name of this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The name of this <see cref="ModelJoint" />.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the position of this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The position of this <see cref="ModelJoint" />.</value>
		public PositionVector3 Position { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the rotation of this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The rotation of this <see cref="ModelJoint" />.</value>
		public PositionVector3 Rotation { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the lower limit for the <see cref="Position" /> property on this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The lower limit for the <see cref="Position" /> property on this <see cref="ModelJoint" />.</value>
		public PositionVector3 LimitMoveLow { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the upper limit for the <see cref="Position" /> property on this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The upper limit for the <see cref="Position" /> property on this <see cref="ModelJoint" />.</value>
		public PositionVector3 LimitMoveHigh { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the lower limit for the <see cref="Rotation" /> property on this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The lower limit for the <see cref="Rotation" /> property on this <see cref="ModelJoint" />.</value>
		public PositionVector3 LimitAngleLow { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Gets or sets the upper limit for the <see cref="Rotation" /> property on this <see cref="ModelJoint" />.
		/// </summary>
		/// <value>The upper limit for the <see cref="Rotation" /> property on this <see cref="ModelJoint" />.</value>
		public PositionVector3 LimitAngleHigh { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Movement stiffness for spring constraint.
		/// </summary>
		public PositionVector3 SpringConstraintMovementStiffness { get; set; } = new PositionVector3(0, 0, 0);
		/// <summary>
		/// Rotation stiffness for spring constraint.
		/// </summary>
		public PositionVector3 SpringConstraintRotationStiffness { get; set; } = new PositionVector3(0, 0, 0);

		public object Clone()
		{
			ModelJoint clone = new ModelJoint();
			clone.LimitAngleHigh = (PositionVector3)(LimitAngleHigh.Clone());
			clone.LimitAngleLow = (PositionVector3)(LimitAngleLow.Clone());
			clone.LimitMoveHigh = (PositionVector3)(LimitMoveHigh.Clone());
			clone.LimitMoveLow = (PositionVector3)(LimitMoveLow.Clone());
			clone.Name = Name.Clone() as string;
			clone.Position = ((PositionVector3)Position.Clone());
			clone.Rotation = ((PositionVector3)Rotation.Clone());
			clone.SpringConstraintMovementStiffness = ((PositionVector3)SpringConstraintMovementStiffness.Clone());
			clone.SpringConstraintRotationStiffness = ((PositionVector3)SpringConstraintRotationStiffness.Clone());
			return clone;
		}
	}
}
