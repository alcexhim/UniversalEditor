using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelSkin : ICloneable
	{
		public class ModelSkinCollection : Collection<ModelSkin>
		{
		}
		private string mvarName = string.Empty;
		private byte mvarCategory = 0;
		private ModelSkinVertex.ModelSkinVertexCollection mvarVertices = new ModelSkinVertex.ModelSkinVertexCollection();
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
		public byte Category
		{
			get
			{
				return this.mvarCategory;
			}
			set
			{
				this.mvarCategory = value;
			}
		}
		public ModelSkinVertex.ModelSkinVertexCollection Vertices
		{
			get
			{
				return this.mvarVertices;
			}
		}
		public object Clone()
		{
			ModelSkin clone = new ModelSkin();
			clone.Name = this.mvarName;
			clone.Category = this.mvarCategory;
			foreach (ModelSkinVertex vtx in this.mvarVertices)
			{
				clone.Vertices.Add(vtx.Clone() as ModelSkinVertex);
			}
			return clone;
		}
	}
}
