using System;
namespace UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedPage
{
	public class FixedPageObjectModel : ObjectModel
	{
		// public Measurement Width { get; set; }
		// public Measurement Height { get; set; }

		public FixedPageItem.FixedPageItemCollection Items { get; } = new FixedPageItem.FixedPageItemCollection();

		public override void Clear()
		{
			throw new NotImplementedException();
		}

		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}
	}
}
