using System;
using System.Collections.Generic;

namespace UniversalEditor.Common
{
	public static class Strings
	{
		/// <summary>
		/// Replaces a collection of named variables in a given string.
		/// </summary>
		/// <param name="value">The string in which to replace variables.</param>
		/// <param name="variables">A <see cref="T:Dictionary`2" /> containing name-value pairs of variables to replace.</param>
		/// <returns>
		///	A <see cref="String" /> with all instances of variables with the given name in $(VariableName) replaced
		///	with the associated value.
		/// </returns>
		public static string ReplaceVariables(this string value, Dictionary<string, object> variables)
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

