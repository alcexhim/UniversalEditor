using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	public class ExecutableFunctionCall : ICloneable
	{
		public class ExecutableFunctionCallCollection
			: System.Collections.ObjectModel.Collection<ExecutableFunctionCall>
		{
		}

		private string mvarLibraryName = String.Empty;
		public string LibraryName { get { return mvarLibraryName; } set { mvarLibraryName = value; } }

		private string mvarFunctionName = String.Empty;
		public string FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

		private List<object> mvarParameterValues = new List<object>();
		public List<object> ParameterValues { get { return mvarParameterValues; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarLibraryName);
			sb.Append("!");
			sb.Append(mvarFunctionName);
			sb.Append("(");
			foreach (object obj in mvarParameterValues)
			{
				if (obj is string) sb.Append("\"");
				sb.Append(obj.ToString());
				if (obj is string) sb.Append("\"");
				if (mvarParameterValues.IndexOf(obj) < mvarParameterValues.Count - 1) sb.Append(", ");
			}
			sb.Append(");");
			return sb.ToString();
		}

		public object Clone()
		{
			ExecutableFunctionCall clone = new ExecutableFunctionCall();
			clone.FunctionName = mvarFunctionName;
			clone.LibraryName = mvarLibraryName;
			foreach (object obj in mvarParameterValues)
			{
				clone.ParameterValues.Add(obj);
			}
			return clone;
		}
	}
}
