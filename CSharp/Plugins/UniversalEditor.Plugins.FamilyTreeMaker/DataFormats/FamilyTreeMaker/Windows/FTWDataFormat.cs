using System.Collections.Generic;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.ObjectModels.FamilyTree;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FamilyTreeMaker.Windows
{
	public class FTWDataFormat : CompoundDocumentDataFormat
	{
		protected override void BeforeLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal (objectModels);
			objectModels.Push (new FileSystemObjectModel ());
		}
		protected override void AfterLoadInternal (Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal (objectModels);

			FileSystemObjectModel fsom = (objectModels.Pop () as FileSystemObjectModel);
			FamilyTreeObjectModel ft = (objectModels.Pop () as FamilyTreeObjectModel);
		}
		protected override void BeforeSaveInternal (Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal (objectModels);
		}
	}
}
