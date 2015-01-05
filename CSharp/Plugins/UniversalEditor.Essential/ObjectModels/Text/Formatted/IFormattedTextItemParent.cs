using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public interface IFormattedTextItemParent
	{
		FormattedTextItem.FormattedTextItemCollection Items { get; }
	}
}
