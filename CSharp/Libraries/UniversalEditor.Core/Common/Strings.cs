using System;
using System.Collections.Generic;

namespace UniversalEditor.Common
{
	public static class Strings
	{
		public static string ReplaceVariables(string value, Dictionary<string, object> variables)
		{
			string retval = value;
			foreach (string key in variables.Keys)
			{
				retval = retval.Replace("$(" + key + ")", variables[key].ToString());
			}
			return retval;
		}
	}
}

