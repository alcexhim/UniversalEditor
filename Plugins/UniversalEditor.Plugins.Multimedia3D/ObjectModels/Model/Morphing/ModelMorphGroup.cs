using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphGroup : ModelMorph
	{
		private float mvarMorphRate = 0f;
		public float MorphRate
		{
			get
			{
				return this.mvarMorphRate;
			}
			set
			{
				this.mvarMorphRate = value;
			}
		}
		public override object Clone()
		{
			return new ModelMorphGroup
			{
				MorphRate = this.mvarMorphRate,
				Name = base.Name
			};
		}
	}
}
