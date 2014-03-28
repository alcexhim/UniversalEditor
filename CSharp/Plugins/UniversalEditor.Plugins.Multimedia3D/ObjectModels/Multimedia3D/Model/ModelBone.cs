using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Neo;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
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
                item.mvarParent = mvarParent;
                base.InsertItem(index, item);
            }
            protected override void RemoveItem(int index)
            {
                string name = this[index].Name;
                if (bonesByName.ContainsKey(name))
                {
                    bonesByName.Remove(name);
                }
                this[index].mvarParent = null;
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

		private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }
        
        private ModelBone mvarParentBone = null;
		private ModelBone mvarChildBone = null;
        private byte mvarCBRigidType = 0;
		private short mvarIKNumber = 0;

        private ModelAngleLimit mvarAngleLimit = new ModelAngleLimit();
        public ModelAngleLimit AngleLimit { get { return mvarAngleLimit; } }

        private PositionVector3 mvarOffset = default(PositionVector3);
        public PositionVector3 Vector3Offset
		{
			get
			{
				return this.mvarOffset;
			}
			set
			{
				this.mvarOffset = value;
			}
		}
		
        public ModelBone ParentBone { get { return mvarParentBone; } set { mvarParentBone = value; RecalculateOffset(); } }
		public ModelBone ChildBone { get { return mvarChildBone; } set { mvarChildBone = value; } }

        private PositionVector3 mvarPosition = default(PositionVector3);
        public PositionVector3 Position
        {
            get { return mvarPosition; }
            set { mvarPosition = value; }
        }
        private PositionVector4 mvarRotation = default(PositionVector4);
        public PositionVector4 Rotation
        {
            get { return mvarRotation; }
            set { mvarRotation = value; Parent.Update(); }
        }

        private PositionVector4 mvarOriginalRotation = default(PositionVector4);
        public PositionVector4 OriginalRotation { get { return mvarOriginalRotation; } set { mvarOriginalRotation = value; } }

        private PositionVector3 mvarOriginalPosition = default(PositionVector3);
		public PositionVector3 OriginalPosition { get { return mvarOriginalPosition; } set { mvarOriginalPosition = value; UpdateInvTransformMatrix(); } }

		private ModelBoneType mvarBoneType = ModelBoneType.Unknown;
        public ModelBoneType BoneType { get { return mvarBoneType; } set { mvarBoneType = value; } }

		public short IKNumber
		{
			get
			{
				return this.mvarIKNumber;
			}
			set
			{
				this.mvarIKNumber = value;
			}
		}
		public object Clone()
		{
            ModelBone clone = new ModelBone();
            #region Angle Limit
            clone.AngleLimit.Enabled = mvarAngleLimit.Enabled;
            clone.AngleLimit.Lower = (PositionVector3)mvarAngleLimit.Lower.Clone();
            clone.AngleLimit.Upper = (PositionVector3)mvarAngleLimit.Upper.Clone();
            #endregion
            clone.ChildBone = mvarChildBone;
            clone.IKNumber = mvarIKNumber;
            clone.BoneType = mvarBoneType;
            clone.Name = (mvarName.Clone() as string);
            clone.ParentBone = mvarParentBone;
            clone.Vector3Offset = (PositionVector3)mvarOffset.Clone();
            clone.OriginalPosition = (PositionVector3)mvarOriginalPosition.Clone();
            clone.OriginalRotation = (PositionVector4)mvarOriginalRotation.Clone();
            clone.Position = (PositionVector3)mvarPosition.Clone();
            clone.Rotation = (PositionVector4)mvarRotation.Clone();
            return clone;
		}

        private ModelObjectModel mvarParent = null;
        public ModelObjectModel Parent { get { return mvarParent; } }

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
            mvarLocalMatrix[3, 0] = mvarPosition.X + mvarOffset.X;
            mvarLocalMatrix[3, 1] = mvarPosition.Y + mvarOffset.Y;
            mvarLocalMatrix[3, 2] = mvarPosition.Z + mvarOffset.Z;
            
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
            mvarPosition = mvarOriginalPosition;
            mvarRotation = mvarOriginalRotation;

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
                mvarOffset = mvarOriginalPosition - mvarParentBone.OriginalPosition;
            }
            else
            {
                mvarOffset = mvarOriginalPosition;
            }
        }
    }
}
