using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public abstract class ModelMorph : ICloneable
	{
		public class ModelMorphCollection : Collection<ModelMorph>
		{
		}
		private string mvarName = string.Empty;
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
		public abstract object Clone();
	}
}
