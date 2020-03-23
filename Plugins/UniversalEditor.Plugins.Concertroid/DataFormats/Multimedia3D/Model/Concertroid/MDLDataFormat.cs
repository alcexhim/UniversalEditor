using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

using UniversalEditor.DataFormats.FileSystem.ZIP;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Concertroid
{
	public class MDLDataFormat : ZIPDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
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
