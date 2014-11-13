using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.FileSystem.ZIP;

using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Michelangelo.Canvas;

namespace UniversalEditor.DataFormats.Michelangelo.Canvas
{
	public class MichelangeloCanvasDataFormat : ZIPDataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(CanvasObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Filters.Add("Michelangelo canvas", new string[] { "*.mcv" });
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
			CanvasObjectModel canvas = (objectModels.Pop() as CanvasObjectModel);

		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			CanvasObjectModel canvas = (objectModels.Pop() as CanvasObjectModel);

			FileSystemObjectModel fsom = new FileSystemObjectModel();


			objectModels.Push(fsom);
		}

	}
}
