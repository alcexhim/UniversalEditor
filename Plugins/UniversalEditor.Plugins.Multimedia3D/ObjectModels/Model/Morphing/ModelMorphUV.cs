using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphUV : ModelMorph
	{
		private long mvarVertexIndex = 0L;
		private PositionVector4 mvarUVOffset = default(PositionVector4);
		public long VertexIndex
		{
			get
			{
				return this.mvarVertexIndex;
			}
			set
			{
				this.mvarVertexIndex = value;
			}
		}
		public PositionVector4 UVOffset
		{
			get
			{
				return this.mvarUVOffset;
			}
			set
			{
				this.mvarUVOffset = value;
			}
		}
		public override object Clone()
		{
			return new ModelMorphUV
			{
				Name = base.Name,
				UVOffset = this.mvarUVOffset,
				VertexIndex = this.mvarVertexIndex
			};
		}
	}
}
