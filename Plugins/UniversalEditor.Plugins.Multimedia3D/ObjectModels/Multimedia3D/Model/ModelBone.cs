//
//  ModelBone.cs - represents a bone in a 3D model
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
using System.Collections.Generic;

using Neo;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a bone in a 3D model.
	/// </summary>
	public class ModelBone : IModelObject, ICloneable
	{
		public class ModelBoneCollection : Collection<ModelBone>
		{
			internal ModelObjectModel mvarParent = null;
			public ModelBoneCollection(ModelObjectModel parent)
			{
				mvarParent = parent;
			}

			private Dictionary<string, ModelBone> bonesByName = new Dictionary<string, ModelBone>();
			protected override void InsertItem(int index, ModelBone item)
			{
				if (!bonesByName.ContainsKey(item.Name))
				{
					bonesByName.Add(item.Name, item);
				}
				item.Parent = mvarParent;
				base.InsertItem(index, item);
			}
			protected override void RemoveItem(int index)
			{
				string name = this[index].Name;
				if (bonesByName.ContainsKey(name))
				{
					bonesByName.Remove(name);
				}
				this[index].Parent = null;
				base.RemoveItem(index);
			}

			public ModelBone this[string Name]
			{
				get
				{
					if (bonesByName.ContainsKey(Name))
					{
						return bonesByName[Name];
					}
					else
					{
						if (mvarParent.StringTable.ContainsKey(1033) && mvarParent.StringTable.ContainsKey(1041))
						{
							for (int i = 0; i < mvarParent.StringTable[1033].BoneNames.Count; i++)
							{
								if (mvarParent.StringTable[1033].BoneNames[i] == Name)
								{
									return mvarParent.Bones[i];
								}
							}
						}
					}
					return null;
				}
			}
		}

		public string Name { get; set; } = String.Empty;

		private ModelBone mvarParentBone = null;
		private byte mvarCBRigidType = 0;
		public ModelAngleLimit AngleLimit { get; } = new ModelAngleLimit();
		public PositionVector3 Vector3Offset { get; set; } = default(PositionVector3);

		public ModelBone ParentBone { get { return mvarParentBone; } set { mvarParentBone = value; RecalculateOffset(); } }
		public ModelBone ChildBone { get; set; } = null;

		public PositionVector3 Position { get; set; } = default(PositionVector3);
		private PositionVector4 mvarRotation = default(PositionVector4);
		public PositionVector4 Rotation
		{
			get { return mvarRotation; }
			set { mvarRotation = value; Parent.Update(); }
		}
		public PositionVector4 OriginalRotation { get; set; } = default(PositionVector4);

		private PositionVector3 mvarOriginalPosition = default(PositionVector3);
		public PositionVector3 OriginalPosition { get { return mvarOriginalPosition; } set { mvarOriginalPosition = value; UpdateInvTransformMatrix(); } }
		public ModelBoneType BoneType { get; set; } = ModelBoneType.Unknown;

		public short IKNumber { get; set; } = 0;
		public object Clone()
		{
			ModelBone clone = new ModelBone();
			#region Angle Limit
			clone.AngleLimit.Enabled = AngleLimit.Enabled;
			clone.AngleLimit.Lower = (PositionVector3)AngleLimit.Lower.Clone();
			clone.AngleLimit.Upper = (PositionVector3)AngleLimit.Upper.Clone();
			#endregion
			clone.ChildBone = ChildBone;
			clone.IKNumber = IKNumber;
			clone.BoneType = BoneType;
			clone.Name = (Name.Clone() as string);
			clone.ParentBone = mvarParentBone;
			clone.Vector3Offset = (PositionVector3)Vector3Offset.Clone();
			clone.OriginalPosition = (PositionVector3)mvarOriginalPosition.Clone();
			clone.OriginalRotation = (PositionVector4)OriginalRotation.Clone();
			clone.Position = (PositionVector3)Position.Clone();
			clone.Rotation = (PositionVector4)mvarRotation.Clone();
			return clone;
		}
		public ModelObjectModel Parent { get; private set; } = null;

		private Matrix mvarLocalMatrix = new Matrix(4, 4);
		private Matrix mvarSkinningMatrix = new Matrix(4, 4);
		private Matrix mvarInvTransformMatrix = new Matrix(4, 4);

		public Matrix GetLocalMatrix()
		{
			UpdateLocalMatrix();
			return mvarLocalMatrix;
		}

		private void UpdateInvTransformMatrix()
		{
			mvarInvTransformMatrix = Matrix.Identity();
			mvarInvTransformMatrix[3, 0] = -mvarOriginalPosition.X;
			mvarInvTransformMatrix[3, 1] = -mvarOriginalPosition.Y;
			mvarInvTransformMatrix[3, 2] = -mvarOriginalPosition.Z;
		}

		/// <summary>
		/// ボーンの行列を更新。 Update the matrix of the bone.
		/// </summary>
		private void UpdateLocalMatrix()
		{
			// クォータニオンと移動値からボーンのローカルマトリックスを作成
			// Create a local matrix of bones from quaternion value and move
			mvarLocalMatrix = mvarRotation.ToMatrix();
			mvarLocalMatrix[3, 0] = Position.X + Vector3Offset.X;
			mvarLocalMatrix[3, 1] = Position.Y + Vector3Offset.Y;
			mvarLocalMatrix[3, 2] = Position.Z + Vector3Offset.Z;

			// 親があるなら親の回転を受け継ぐE
			// Inherit the rotation of the parent if there is a parent
			if (mvarParentBone != null)
			{
				mvarLocalMatrix = mvarLocalMatrix * mvarParentBone.GetLocalMatrix();
			}
		}
		/// <summary>
		/// スキニング用行列を更新。 Update the matrix for skinning.
		/// </summary>
		public void UpdateSkinningMatrix()
		{
			UpdateInvTransformMatrix();

			Matrix localMatrix = GetLocalMatrix();
			mvarSkinningMatrix = mvarInvTransformMatrix * localMatrix;

			/*
            mvarSkinningMatrix[3, 0] -= mvarOriginalPosition.X;
            mvarSkinningMatrix[3, 1] -= mvarOriginalPosition.Y;
            mvarSkinningMatrix[3, 2] -= mvarOriginalPosition.Z;
            */
		}

		public void Reset()
		{
			Position = mvarOriginalPosition;
			mvarRotation = OriginalRotation;

			mvarLocalMatrix = Matrix.Identity();
			mvarLocalMatrix[3, 0] = mvarOriginalPosition.X;
			mvarLocalMatrix[3, 1] = mvarOriginalPosition.Y;
			mvarLocalMatrix[3, 2] = mvarOriginalPosition.Z;
		}

		public Matrix GetSkinningMatrix()
		{
			UpdateSkinningMatrix();
			return mvarSkinningMatrix;
		}

		public void RecalculateOffset()
		{
			if (mvarParentBone != null)
			{
				Vector3Offset = mvarOriginalPosition - mvarParentBone.OriginalPosition;
			}
			else
			{
				Vector3Offset = mvarOriginalPosition;
			}
		}
	}
}
