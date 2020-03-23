using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocumentSequence
{
	public class DocumentReference : ICloneable
	{
		public class DocumentReferenceCollection
			: System.Collections.ObjectModel.Collection<DocumentReference>
		{

		}

		private string mvarSource = String.Empty;
		public string Source { get { return mvarSource; } set { mvarSource = value; } }

		public object Clone()
		{
			DocumentReference clone = new DocumentReference();
			clone.Source = (mvarSource.Clone() as string);
			return clone;
		}
	}
}
