using System;
using System.Collections.ObjectModel;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelMaterial : ICloneable
	{
		public class ModelMaterialCollection : Collection<ModelMaterial>
		{
		}
		private string mvarName = string.Empty;
		private uint? mvarMapID = null;

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
		public Color DiffuseColor { get; set; } = Colors.Black;
		public Color SpecularColor { get; set; } = Colors.Black;
		public Color EmissiveColor { get; set; } = Colors.Black;
		public Color AmbientColor { get; set; } = Colors.Black;

		public sbyte ToonNumber { get; set; } = 0;
		public float Shininess { get; set; } = 0f;

		public bool EdgeFlag { get; set; } = false;
		public uint IndexCount { get; set; } = 0u;

		public string Comment { get; set; } = string.Empty;
		public Color EdgeColor { get; set; } = Color.Empty;
		public float EdgeSize { get; set; } = 0f;

		public object Clone()
		{
			ModelMaterial clone = new ModelMaterial();
			clone.AmbientColor = AmbientColor;
			clone.DiffuseColor = DiffuseColor;
			clone.EdgeColor = EdgeColor;
			clone.EdgeFlag = EdgeFlag;
			clone.EdgeSize = EdgeSize;
			clone.IndexCount = IndexCount;
            clone.Name = mvarName;
            clone.Shininess = Shininess;
            clone.SpecularColor = SpecularColor;

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
            return mvarName + " (" + ((mvarTextures.Count > 0) ? (!String.IsNullOrEmpty(mvarTextures[0].TextureFileName) ? mvarTextures[0].TextureFileName : mvarTextures[0].MapFileName) : AmbientColor.ToString()) + ")";
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
