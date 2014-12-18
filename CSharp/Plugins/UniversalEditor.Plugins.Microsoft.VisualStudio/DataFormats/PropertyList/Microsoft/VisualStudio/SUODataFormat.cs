using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.PropertyList.Microsoft.VisualStudio
{
	public class SUODataFormat : CompoundDocumentDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(PropertyListObjectModel), DataFormatCapabilities.All);
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
			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);

			for (int i = 0; i < fsom.Files.Count; i++ )
				fsom.Files[i].Save(@"C:\Temp\SUO\" + fsom.Files[i].Name);
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			PropertyListObjectModel plom = (objectModels.Pop() as PropertyListObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();



			objectModels.Push(fsom);
		}
	}
}
