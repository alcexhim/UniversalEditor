//
//  Strings.cs - common methods for strings
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

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

