using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Scene
{
	public class SceneModelReference : ICloneable
	{
		public class SceneModelReferenceCollection : Collection<SceneModelReference>
		{
			public SceneModelReference Add(string ModelName)
			{
				SceneModelReference model = new SceneModelReference();
				model.ModelName = ModelName;
				base.Add(model);
				return model;
			}
		}
		private string mvarModelName = string.Empty;
		private string mvarFileName = string.Empty;
		public string ModelName
		{
			get
			{
				return this.mvarModelName;
			}
			set
			{
				this.mvarModelName = value;
			}
		}
		public string FileName
		{
			get
			{
				return this.mvarFileName;
			}
			set
			{
				this.mvarFileName = value;
			}
		}
		public object Clone()
		{
			return new SceneModelReference
			{
				FileName = this.mvarFileName, 
				ModelName = this.mvarModelName
			};
		}
	}
}
