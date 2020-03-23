using System;
namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocument
{
	public class PageContent : ICloneable
	{
		public class PageContentCollection
			: System.Collections.ObjectModel.Collection<PageContent>
		{
		}

		public string Source { get; set; } = String.Empty;

		public PageContent(string source)
		{
			Source = source;
		}

		public object Clone()
		{
			PageContent clone = new PageContent(Source.Clone() as string);
			return clone;
		}
	}
}
