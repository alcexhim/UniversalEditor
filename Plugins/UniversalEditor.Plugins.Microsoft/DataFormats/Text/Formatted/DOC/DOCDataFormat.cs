using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.FileSystem.Microsoft.CompoundDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Text.Formatted;

namespace UniversalEditor.DataFormats.Text.Formatted.DOC
{
	public class DOCDataFormat : CompoundDocumentDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
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
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);

		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);
			FileSystemObjectModel fsom = new FileSystemObjectModel();	



			objectModels.Push(fsom);
			base.BeforeSaveInternal(objectModels);
		}
	}
}
