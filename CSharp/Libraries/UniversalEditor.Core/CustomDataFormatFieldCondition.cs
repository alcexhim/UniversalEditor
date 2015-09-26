using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor
{
	public class CustomDataFormatFieldCondition
	{
		private string mvarVariable = String.Empty;
		public string Variable { get { return mvarVariable; } set { mvarVariable = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private string mvarTrueResult = String.Empty;
		public string TrueResult { get { return mvarTrueResult; } set { mvarTrueResult = value; } }

		private string mvarFalseResult = String.Empty;
		public string FalseResult { get { return mvarFalseResult; } set { mvarFalseResult = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("`");
			sb.Append(mvarVariable);
			sb.Append("` == `");
			sb.Append(mvarValue);
			sb.Append("` ? `");
			sb.Append(mvarTrueResult);
			sb.Append("` : `");
			sb.Append(mvarFalseResult);
			sb.Append("`");
			return sb.ToString();
		}
	}
}
