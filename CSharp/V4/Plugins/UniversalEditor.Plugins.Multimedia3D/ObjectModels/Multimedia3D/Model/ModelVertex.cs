using System;
using System.Collections.ObjectModel;

using Neo;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelVertex
	{
		public class ModelVertexCollection : Collection<ModelVertex>
		{
		}

        public ModelVertex()
        {
        }
        public ModelVertex(float vx, float vy, float vz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
        }
        public ModelVertex(double vx, double vy, double vz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
        }
        public ModelVertex(float vx, float vy, float vz, float tu, float tv)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarTexture = new TextureVector2(tu, tv);
        }
        public ModelVertex(double vx, double vy, double vz, double tu, double tv)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarTexture = new TextureVector2(tu, tv);
        }
        public ModelVertex(float vx, float vy, float vz, float nx, float ny, float nz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarNormal = new PositionVector3(nx, ny, nz);
            mvarOriginalNormal = new PositionVector3(nx, ny, nz);
        }
        public ModelVertex(double vx, double vy, double vz, double nx, double ny, double nz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarNormal = new PositionVector3(nx, ny, nz);
            mvarOriginalNormal = new PositionVector3(nx, ny, nz);
        }
        public ModelVertex(float vx, float vy, float vz, float tu, float tv, float nx, float ny, float nz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarTexture = new TextureVector2(tu, tv);
            mvarNormal = new PositionVector3(nx, ny, nz);
            mvarOriginalNormal = new PositionVector3(nx, ny, nz);
        }
        public ModelVertex(double vx, double vy, double vz, double tu, double tv, double nx, double ny, double nz)
        {
            mvarPosition = new PositionVector3(vx, vy, vz);
            mvarOriginalPosition = new PositionVector3(vx, vy, vz);
            mvarTexture = new TextureVector2(tu, tv);
            mvarNormal = new PositionVector3(nx, ny, nz);
            mvarOriginalNormal = new PositionVector3(nx, ny, nz);
        }

        private ModelBone mvarBone0 = null;
        public ModelBone Bone0 { get { return mvarBone0; } set { mvarBone0 = value; } }

        private ModelBone mvarBone1 = null;
        public ModelBone Bone1 { get { return mvarBone1; } set { mvarBone1 = value; } }

		private PositionVector3 mvarPosition = default(PositionVector3);
		private PositionVector3 mvarNormal = default(PositionVector3);
		private TextureVector2 mvarTexture = default(TextureVector2);
		public PositionVector3 Position
		{
			get
			{
				return this.mvarPosition;
			}
			set
			{
				this.mvarPosition = value;
			}
		}
		public PositionVector3 Normal
		{
			get
			{
				return this.mvarNormal;
			}
			set
			{
				this.mvarNormal = value;
			}
		}
		public TextureVector2 Texture
		{
			get
			{
				return this.mvarTexture;
			}
			set
			{
				this.mvarTexture = value;
			}
		}

        private float mvarBone0Weight = 1.0f;
        public float Bone0Weight { get { return this.mvarBone0Weight; } set { mvarBone0Weight = value; } }
        public float Bone1Weight
        {
            get { return 1.0f - mvarBone0Weight; }
            set { mvarBone0Weight = 1.0f - value; }
        }


		private bool mvarEdgeFlag = false;
		public bool EdgeFlag
		{
			get { return mvarEdgeFlag; }
			set { mvarEdgeFlag = value; }
		}
		public object Clone()
		{
			return new ModelVertex
			{
				Bone0 = (mvarBone0.Clone() as ModelBone), 
				Bone1 = (mvarBone1.Clone() as ModelBone), 
				EdgeFlag = mvarEdgeFlag, 
				Normal = mvarNormal, 
				Position = mvarPosition, 
				Texture = mvarTexture, 
				Bone0Weight = mvarBone0Weight
			};
		}

        private PositionVector3 mvarOriginalPosition;
        public PositionVector3 OriginalPosition { get { return mvarOriginalPosition; } set { mvarOriginalPosition = value; } }

        /// <summary>
        /// Resets the position of the vertex.
        /// </summary>
        public void Reset()
        {
            mvarPosition = mvarOriginalPosition;
            mvarNormal = mvarOriginalNormal;
        }

        private PositionVector3 mvarOriginalNormal;
        public PositionVector3 OriginalNormal { get { return mvarOriginalNormal; } set { mvarOriginalNormal = value; } }
    }
}
