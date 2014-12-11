using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public class FormattedTextObjectModel : ObjectModel
	{
		private ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Formatted Text Document";
				_omr.Path = new string[] { "General", "Text", "Formatted" };
			}
			return _omr;
		}

		private FormattedTextItem.FormattedTextItemCollection mvarSegments = new FormattedTextItem.FormattedTextItemCollection();
		public FormattedTextItem.FormattedTextItemCollection Items { get { return mvarSegments; } }

		public override void Clear()
		{
			mvarSegments.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FormattedTextObjectModel clone = (where as FormattedTextObjectModel);
			foreach (FormattedTextItem segment in mvarSegments)
			{
				clone.Items.Add(segment.Clone() as FormattedTextItem);
			}
		}
	}
}
