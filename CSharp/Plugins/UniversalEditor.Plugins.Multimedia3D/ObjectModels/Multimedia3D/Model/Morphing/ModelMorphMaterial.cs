using System;
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphMaterial : ModelMorph
	{
		private long mvarMaterialIndex = 0L;

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
		public Color DiffuseColor { get; set; } = Color.Empty;
		public Color SpecularColor { get; set; } = Color.Empty;

		public float SpecularCoefficient { get; set; } = 0f;
		public Color AmbientColor { get; set; } = Color.Empty;
		public Color EdgeColor { get; set; }
		public float EdgeSize { get; set; } = 0f;

		public Color TextureCoefficient { get; set; } = Color.Empty;
		public Color SphereCoefficient { get; set; } = Color.Empty;
		public Color ToonTextureCoefficient { get; set; } = Color.Empty;

		public override object Clone()
		{
			return new ModelMorphMaterial
			{
				AmbientColor = this.AmbientColor, 
				DiffuseColor = this.DiffuseColor, 
				EdgeColor = this.EdgeColor, 
				EdgeSize = this.EdgeSize, 
				MaterialIndex = this.mvarMaterialIndex, 
				Name = base.Name, 
				SpecularCoefficient = this.SpecularCoefficient, 
				SpecularColor = this.SpecularColor, 
				SphereCoefficient = this.SphereCoefficient, 
				TextureCoefficient = this.TextureCoefficient, 
				ToonTextureCoefficient = this.ToonTextureCoefficient
			};
		}
	}
}
