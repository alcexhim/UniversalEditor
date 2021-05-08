//
//  CodeMethodCallActionSearch.cs - ???
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

using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.ObjectModels.SourceCode.SearchExpressions
{
	/// <summary>
	/// ???
	/// </summary>
	public class CodeMethodCallActionSearch
	{
		private string[] mvarObjectName = new string[0];
		public string[] ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

		private string mvarMethodName = String.Empty;
		public string MethodName { get { return mvarMethodName; } set { mvarMethodName = value; } }

		private CodeMethodSearchParameter.CodeMethodSearchParameterCollection mvarParameters = new CodeMethodSearchParameter.CodeMethodSearchParameterCollection();
		public CodeMethodSearchParameter.CodeMethodSearchParameterCollection Parameters { get { return mvarParameters; } }

		public CodeMethodCallActionSearch(string[] objectName, string methodName)
		{
			mvarObjectName = objectName;
			mvarMethodName = methodName;
		}
	}
	public class CodeMethodSearchParameter
	{
		public class CodeMethodSearchParameterCollection
			: System.Collections.ObjectModel.Collection<CodeMethodSearchParameter>
		{
		}
	}
	public class CodeMethodSearchParameterValue : CodeMethodSearchParameter
	{
		private CodeLiteralElement mvarValue = new CodeLiteralElement(null);
		public CodeLiteralElement Value { get { return mvarValue; } set { mvarValue = value; } }

		public CodeMethodSearchParameterValue(CodeLiteralElement value)
		{
			mvarValue = value;
		}
	}
	public class CodeMethodSearchParameterReference : CodeMethodSearchParameter
	{

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private CodeLiteralElement mvarDefaultValue = new CodeLiteralElement(null);
		public CodeLiteralElement DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

		public CodeMethodSearchParameterReference(string name, CodeLiteralElement defaultValue = null)
		{
			mvarName = name;
			if (defaultValue != null) mvarDefaultValue = defaultValue;
		}
	}
}
