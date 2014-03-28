using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelRigidBody : ICloneable
	{
		public class ModelRigidBodyCollection : Collection<ModelRigidBody>
		{
		}
		private ModelRigidBodyCollisionShape mvarCollisionShape = null;
		private int mvarIType = 0;
		public ModelRigidBodyCollisionShape CollisionShape
		{
			get
			{
				return this.mvarCollisionShape;
			}
			set
			{
				this.mvarCollisionShape = value;
			}
		}
		public int IType
		{
			get
			{
				return this.mvarIType;
			}
			set
			{
				this.mvarIType = value;
			}
		}
		public object Clone()
		{
			ModelRigidBody clone = new ModelRigidBody();
			if (mvarCollisionShape != null)
			{
				clone.CollisionShape = (mvarCollisionShape.Clone() as ModelRigidBodyCollisionShape);
			}
			clone.IType = mvarIType;
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
