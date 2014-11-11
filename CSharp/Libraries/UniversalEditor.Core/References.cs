using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public interface References<TRef>
	{
		TRef MakeReference();
	}
	public interface ReferencedBy<TObj>
	{
		TObj Create();

		string[] GetDetails();
		bool ShouldFilterObject(string filter);
	}
}
