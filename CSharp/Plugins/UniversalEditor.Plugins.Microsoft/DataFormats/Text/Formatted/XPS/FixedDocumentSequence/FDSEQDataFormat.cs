using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocumentSequence
{
	public class FDSEQDataFormat : XMLDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			FixedDocumentSequenceObjectModel fdseq = (objectModels.Pop() as FixedDocumentSequenceObjectModel);

			MarkupTagElement tagFixedDocumentSequence = (mom.Elements["FixedDocumentSequence"] as MarkupTagElement);
			foreach (MarkupElement elDocumentReference in tagFixedDocumentSequence.Elements)
			{
				MarkupTagElement tagDocumentReference = (elDocumentReference as MarkupTagElement);
				if (tagDocumentReference == null) continue;
				if (tagDocumentReference.FullName != "DocumentReference") continue;

				MarkupAttribute attSource = tagDocumentReference.Attributes["Source"];
				if (attSource == null) continue;

				DocumentReference docref = new DocumentReference();
				docref.Source = attSource.Value;

				fdseq.DocumentReferences.Add(docref);
			}
		}
	}
}
