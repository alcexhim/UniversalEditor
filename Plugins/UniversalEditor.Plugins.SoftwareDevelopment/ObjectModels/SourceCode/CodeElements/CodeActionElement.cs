using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
	public abstract class CodeActionElement : CodeElement
	{
		public class CodeActionElementCollection
			: System.Collections.ObjectModel.Collection<CodeActionElement>
		{
		}
	}
}
