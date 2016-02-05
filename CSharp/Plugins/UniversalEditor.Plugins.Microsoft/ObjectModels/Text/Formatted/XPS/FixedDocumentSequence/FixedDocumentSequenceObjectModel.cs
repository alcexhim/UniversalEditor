using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence
{
	public class FixedDocumentSequenceObjectModel : ObjectModel
	{
		public override void Clear()
		{
			mvarDocumentReferences.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FixedDocumentSequenceObjectModel clone = (where as FixedDocumentSequenceObjectModel);
			if (clone == null) throw new ObjectModelNotSupportedException();

			foreach (DocumentReference item in mvarDocumentReferences)
			{
				clone.DocumentReferences.Add(item.Clone() as DocumentReference);
			}
		}

		private DocumentReference.DocumentReferenceCollection mvarDocumentReferences = new DocumentReference.DocumentReferenceCollection();
		public DocumentReference.DocumentReferenceCollection DocumentReferences { get { return mvarDocumentReferences; } }
	}
}
