using System;
namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocument
{
	public class FixedDocumentObjectModel : ObjectModel
	{
		public PageContent.PageContentCollection PageContents { get; } = new PageContent.PageContentCollection();

		public override void Clear()
		{
			PageContents.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			FixedDocumentObjectModel clone = (where as FixedDocumentObjectModel);
			if (clone == null) return;

			foreach (PageContent pcoc in PageContents)
			{
				clone.PageContents.Add(pcoc.Clone() as PageContent);
			}
		}
	}
}
