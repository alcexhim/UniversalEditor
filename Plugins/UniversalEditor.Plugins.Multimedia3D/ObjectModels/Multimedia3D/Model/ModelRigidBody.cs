//
//  ModelRigidBody.cs - describes rigid body collision information for a 3D model
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
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Describes rigid body collision information for a 3D model.
	/// </summary>
	public class ModelRigidBody : ICloneable
	{
		public class ModelRigidBodyCollection : Collection<ModelRigidBody>
		{
		}

		public ModelRigidBodyCollisionShape CollisionShape { get; set; } = null;
		public int IType { get; set; } = 0;

		public object Clone()
		{
			ModelRigidBody clone = new ModelRigidBody();
			if (CollisionShape != null)
			{
				clone.CollisionShape = (CollisionShape.Clone() as ModelRigidBodyCollisionShape);
			}
			clone.IType = IType;
			return clone;
		}

		public string Name { get; set; }

		public ModelBone Bone { get; set; }

		public byte GroupID { get; set; }

		public bool[] PassGroupFlags { get; set; }

		public byte BoxType { get; set; }

		public PositionVector3 BoxSize { get; set; }

		public PositionVector3 Position { get; set; }

		public PositionVector3 Rotation { get; set; }

		public float Mass { get; set; }

		public float PositionDamping { get; set; }

		public float RotationDamping { get; set; }

		public float Restitution { get; set; }

		public float Friction { get; set; }

		public byte Mode { get; set; }
	}
}
