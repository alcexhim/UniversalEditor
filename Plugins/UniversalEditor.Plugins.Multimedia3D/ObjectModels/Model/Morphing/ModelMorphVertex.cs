using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphVertex : ModelMorph
	{
		private long mvarTopIndex = 0L;
		private PositionVector3 mvarOffset = default(PositionVector3);
		public long TopIndex
		{
			get
			{
				return this.mvarTopIndex;
			}
			set
			{
				this.mvarTopIndex = value;
			}
		}
		public PositionVector3 Offset
		{
			get
			{
				return this.mvarOffset;
			}
			set
			{
				this.mvarOffset = value;
			}
		}
		public override object Clone()
		{
			return new ModelMorphVertex
			{
				Name = base.Name,
				Offset = this.mvarOffset,
				TopIndex = this.mvarTopIndex
			};
		}
	}
}
