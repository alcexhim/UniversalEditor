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

        private ModelBone mvarBone0 = null;
        public ModelBone Bone0 { get { return mvarBone0; } set { mvarBone0 = value; } }

        private ModelBone mvarBone1 = null;
        public ModelBone Bone1 { get { return mvarBone1; } set { mvarBone1 = value; } }

		private PositionVector3 mvarPosition = default(PositionVector3);
		private PositionVector3 mvarNormal = default(PositionVector3);
		private TextureVector2 mvarTexture = default(TextureVector2);
		private float mvarWeight = 0f;
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
		
        public float Weight
		{
			get
			{
				return this.mvarWeight;
			}
			set
			{
				this.mvarWeight = value;
			}
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
				Weight = mvarWeight
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
