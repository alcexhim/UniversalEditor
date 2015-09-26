using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupPreprocessorElement : MarkupElement
	{
        public MarkupPreprocessorElement()
        {
        }
        public MarkupPreprocessorElement(string fullName, string value)
        {
            base.FullName = fullName;
            base.Value = value;
        }
		public override object Clone()
		{
			return new MarkupPreprocessorElement
			{
				Name = base.Name,
				Namespace = base.Namespace,
				Value = base.Value
			};
		}
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"<?", 
				base.Name, 
				" ", 
				base.Value, 
				"?>"
			});
		}
	}
}
