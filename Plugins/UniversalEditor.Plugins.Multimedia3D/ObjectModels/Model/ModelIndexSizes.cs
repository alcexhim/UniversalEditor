using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelIndexSizes
	{
		private byte mvarVertex = 0;
		public byte Vertex
		{
			get { return mvarVertex; }
            set { mvarVertex = value; }
		}

        private byte mvarTexture = 0;
        public byte Texture
		{
			get { return mvarTexture; }
			set { mvarTexture = value; }
		}

        private byte mvarMaterial = 0;
        public byte Material
		{
			get { return mvarMaterial; }
			set { mvarMaterial = value; }
		}

        private byte mvarBone = 0;
        public byte Bone
		{
			get { return mvarBone; }
			set { mvarBone = value; }
		}

        private byte mvarMorph = 0;
        public byte Morph
		{
			get { return mvarMorph; }
			set { mvarMorph = value; }
		}

        private byte mvarRigidBody = 0;
        public byte RigidBody
		{
			get { return mvarRigidBody; }
			set { mvarRigidBody = value; }
		}

        public void Clear()
        {
            mvarVertex = 0;
            mvarTexture = 0;
            mvarMaterial = 0;
            mvarBone = 0;
            mvarMorph = 0;
            mvarRigidBody = 0;
        }
    }
}
