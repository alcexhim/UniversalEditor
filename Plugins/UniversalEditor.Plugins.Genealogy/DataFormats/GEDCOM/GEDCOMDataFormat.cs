using System;
using System.Collections.Generic;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree;

namespace UniversalEditor.Plugins.Genealogy.DataFormats.GEDCOM
{
	public class GEDCOMDataFormat : GEDCOMChunkedDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FamilyTreeObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new ChunkedObjectModel());
		}

		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			ChunkedObjectModel chunked = (objectModels.Pop() as ChunkedObjectModel);
			FamilyTreeObjectModel ftom = (objectModels.Pop() as FamilyTreeObjectModel);

		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FamilyTreeObjectModel ftom = (objectModel as FamilyTreeObjectModel);
		}
	}
}
