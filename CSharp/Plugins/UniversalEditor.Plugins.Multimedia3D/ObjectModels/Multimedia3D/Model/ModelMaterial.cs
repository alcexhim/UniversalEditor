using System;
using System.Collections.ObjectModel;
using System.Drawing;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelMaterial : ICloneable
	{
		public class ModelMaterialCollection : Collection<ModelMaterial>
		{
		}
		private string mvarName = string.Empty;
		private Color mvarDiffuseColor = Color.FromRGBA(0, 0, 0, 0);
        private Color mvarEmissiveColor = Color.FromRGBA(0, 0, 0, 0);
        private Color mvarSpecularColor = Color.FromRGBA(0, 0, 0, 0);
		private Color mvarAmbientColor = Color.FromRGBA(0, 0, 0, 0);
		private sbyte mvarToonNumber = 0;
		private float mvarShininess = 0f;
		private uint? mvarMapID = null;
		private bool mvarEdgeFlag = false;
		private uint mvarIndexCount = 0u;
		private string mvarComment = string.Empty;
		private Color mvarEdgeColor = Color.Empty;
		private float mvarEdgeSize = 0f;
		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}
		public Color DiffuseColor
		{
			get
			{
				return this.mvarDiffuseColor;
			}
			set
			{
				this.mvarDiffuseColor = value;
			}
		}
		public Color SpecularColor
		{
			get
			{
				return this.mvarSpecularColor;
			}
			set
			{
				this.mvarSpecularColor = value;
			}
		}
        public Color EmissiveColor
        {
            get
            {
                return this.mvarEmissiveColor;
            }
            set
            {
                this.mvarEmissiveColor = value;
            }
        }
		public Color AmbientColor
		{
			get
			{
				return this.mvarAmbientColor;
			}
			set
			{
				this.mvarAmbientColor = value;
			}
		}
		public sbyte ToonNumber
		{
			get
			{
				return this.mvarToonNumber;
			}
			set
			{
				this.mvarToonNumber = value;
			}
		}
		public float Shininess
		{
			get
			{
				return this.mvarShininess;
			}
			set
			{
				this.mvarShininess = value;
			}
		}

		public bool EdgeFlag
		{
			get
			{
				return this.mvarEdgeFlag;
			}
			set
			{
				this.mvarEdgeFlag = value;
			}
		}
		public uint IndexCount
		{
			get
			{
				return this.mvarIndexCount;
			}
			set
			{
				this.mvarIndexCount = value;
			}
		}
		
        public string Comment
		{
			get
			{
				return this.mvarComment;
			}
			set
			{
				this.mvarComment = value;
			}
		}
		public Color EdgeColor
		{
			get
			{
				return this.mvarEdgeColor;
			}
			set
			{
				this.mvarEdgeColor = value;
			}
		}
		public float EdgeSize
		{
			get
			{
				return this.mvarEdgeSize;
			}
			set
			{
				this.mvarEdgeSize = value;
			}
		}
		public object Clone()
		{
            ModelMaterial clone = new ModelMaterial();
            clone.AmbientColor = mvarAmbientColor;
            clone.DiffuseColor = mvarDiffuseColor;
            clone.EdgeColor = mvarEdgeColor;
            clone.EdgeFlag = mvarEdgeFlag;
            clone.EdgeSize = mvarEdgeSize;
            clone.IndexCount = mvarIndexCount;
            clone.Name = mvarName;
            clone.Shininess = mvarShininess;
            clone.SpecularColor = mvarSpecularColor;

            foreach (ModelTexture texture in mvarTextures)
            {
                clone.Textures.Add(texture.Clone() as ModelTexture);
            }

            return clone;
		}

        private ModelTexture.ModelTextureCollection mvarTextures = new ModelTexture.ModelTextureCollection();
        public ModelTexture.ModelTextureCollection Textures { get { return mvarTextures; } }

        private int mvarTextureIndex = 0;
        public int TextureIndex { get { return mvarTextureIndex; } set { mvarTextureIndex = value; } }

        public override string ToString()
        {
            return mvarName + " (" + ((mvarTextures.Count > 0) ? (!String.IsNullOrEmpty(mvarTextures[0].TextureFileName) ? mvarTextures[0].TextureFileName : mvarTextures[0].MapFileName) : mvarAmbientColor.ToString()) + ")";
        }

        private bool mvarAlwaysLight = false;
        public bool AlwaysLight { get { return mvarAlwaysLight; } set { mvarAlwaysLight = value; } }

        private ModelTriangle.ModelTriangleCollection mvarTriangles = new ModelTriangle.ModelTriangleCollection();
        public ModelTriangle.ModelTriangleCollection Triangles { get { return mvarTriangles; } }

		private bool mvarEnableGlow = false;
		public bool EnableGlow { get { return mvarEnableGlow; } set { mvarEnableGlow = value; } }

		private bool mvarEnableAnimation = false;
		public bool EnableAnimation { get { return mvarEnableAnimation; } set { mvarEnableAnimation = value; } }
	}
}
