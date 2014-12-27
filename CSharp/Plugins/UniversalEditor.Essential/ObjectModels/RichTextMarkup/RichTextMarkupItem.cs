using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public abstract class RichTextMarkupItem : ICloneable
	{
		public class RichTextMarkupItemCollection
			: System.Collections.ObjectModel.Collection<RichTextMarkupItem>
		{

		}

		public abstract object Clone();
	}
}
