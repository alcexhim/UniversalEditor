using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Concertroid
{
	public class MDLDataFormat : UniversalEditor.DataFormats.FileSystem.ZIP.ZIPDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			dfr.Filters.Add("Concertroid all-in-one model package", new string[] { "*.mdl" });
			return dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);

			objectModels.Push(new FileSystemObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			
			FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
			ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
		}
	}
}
