using System;
using System.Collections.Generic;

namespace MBS.Framework
{
	public static class StringExtensions
	{
		public static string Capitalize(this string value)
		{
			if (String.IsNullOrEmpty(value)) return value;
			if (value.Length == 1) return value.ToUpper();
			return value[0].ToString().ToUpper() + value.Substring(1);
		}
		public static string ReplaceVariables(this string value, Dictionary<string, object> dict)
		{
			string retval = value;
			foreach (KeyValuePair<string, object> kvp in dict)
			{
				retval = retval.Replace("$(" + kvp.Key + ")", kvp.Value == null ? String.Empty : kvp.Value.ToString());
			}
			return retval;
		}
	}
}
