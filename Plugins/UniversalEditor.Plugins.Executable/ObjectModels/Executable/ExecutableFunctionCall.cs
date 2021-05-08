//
//  ExecutableFunctionCall.cs - represents a call to a function (possibly in another library)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Represents a call to a function (possibly in another library).
	/// </summary>
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
