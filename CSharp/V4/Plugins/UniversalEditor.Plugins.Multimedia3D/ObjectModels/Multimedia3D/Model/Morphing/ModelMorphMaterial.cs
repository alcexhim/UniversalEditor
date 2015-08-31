using System;
using System.Drawing;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphMaterial : ModelMorph
	{
		private long mvarMaterialIndex = 0L;
		private Color mvarDiffuseColor = Color.Empty;
		private Color mvarSpecularColor = Color.Empty;
		private float mvarSpecularCoefficient = 0f;
		private Color mvarAmbientColor = Color.Empty;
		private Color mvarEdgeColor;
		private float mvarEdgeSize = 0f;
		private Color mvarTextureCoefficient = Color.Empty;
		private Color mvarSphereCoefficient = Color.Empty;
		private Color mvarToonTextureCoefficient = Color.Empty;
		public long MaterialIndex
		{
			get
			{
				return this.mvarMaterialIndex;
			}
			set
			{
				this.mvarMaterialIndex = value;
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
		public float SpecularCoefficient
		{
			get
			{
				return this.mvarSpecularCoefficient;
			}
			set
			{
				this.mvarSpecularCoefficient = value;
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
		public Color TextureCoefficient
		{
			get
			{
				return this.mvarTextureCoefficient;
			}
			set
			{
				this.mvarTextureCoefficient = value;
			}
		}
		public Color SphereCoefficient
		{
			get
			{
				return this.mvarSphereCoefficient;
			}
			set
			{
				this.mvarSphereCoefficient = value;
			}
		}
		public Color ToonTextureCoefficient
		{
			get
			{
				return this.mvarToonTextureCoefficient;
			}
			set
			{
				this.mvarToonTextureCoefficient = value;
			}
		}
		public override object Clone()
		{
			return new ModelMorphMaterial
			{
				AmbientColor = this.mvarAmbientColor, 
				DiffuseColor = this.mvarDiffuseColor, 
				EdgeColor = this.mvarEdgeColor, 
				EdgeSize = this.mvarEdgeSize, 
				MaterialIndex = this.mvarMaterialIndex, 
				Name = base.Name, 
				SpecularCoefficient = this.mvarSpecularCoefficient, 
				SpecularColor = this.mvarSpecularColor, 
				SphereCoefficient = this.mvarSphereCoefficient, 
				TextureCoefficient = this.mvarTextureCoefficient, 
				ToonTextureCoefficient = this.mvarToonTextureCoefficient
			};
		}
	}
}
