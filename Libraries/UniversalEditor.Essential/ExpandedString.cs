//
//  ExpandedString.cs - provides a way to represent a String that can contain variable or constant references
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
using System.Text;

using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor
{
	public class ExpandedStringVariableStore
	{

		private ExpandedStringVariable.ExpandedStringVariableCollection mvarConstants = new ExpandedStringVariable.ExpandedStringVariableCollection();
		public ExpandedStringVariable.ExpandedStringVariableCollection Constants { get { return mvarConstants; } }
		private ExpandedStringVariable.ExpandedStringVariableCollection mvarVariables = new ExpandedStringVariable.ExpandedStringVariableCollection();
		public ExpandedStringVariable.ExpandedStringVariableCollection Variables { get { return mvarVariables; } }
	}
	public class ExpandedStringVariable
	{
		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarID);
			sb.Append("=");
			if (mvarValue == null)
			{
				sb.Append("(null)");
			}
			else
			{
				sb.Append("\"");
				sb.Append(mvarValue.ToString());
				sb.Append("\"");
			}
			return sb.ToString();
		}

		public class ExpandedStringVariableCollection
		{
			private Dictionary<ExpandedStringSegmentVariableScope, Dictionary<string, ExpandedStringVariable>> variables = new Dictionary<ExpandedStringSegmentVariableScope, Dictionary<string, ExpandedStringVariable>>();
			public ExpandedStringVariable this[ExpandedStringSegmentVariableScope scope, string id]
			{
				get
				{
					if (!variables.ContainsKey(scope)) variables.Add(scope, new Dictionary<string, ExpandedStringVariable>());
					if (!variables[scope].ContainsKey(id)) return null;
					return variables[scope][id];
				}
				set
				{
					if (!variables.ContainsKey(scope)) variables.Add(scope, new Dictionary<string, ExpandedStringVariable>());
					variables[scope][id] = value;
				}
			}
		}
	}
	/// <summary>
	/// Provides a way to represent a String that can contain variable or constant references.
	/// </summary>
	public class ExpandedString
	{
		public static readonly ExpandedString Empty = new ExpandedString();

		public ExpandedString()
		{

		}
		public ExpandedString(params ExpandedStringSegment[] segments)
		{
			foreach (ExpandedStringSegment segment in segments)
			{
				mvarSegments.Add(segment);
			}
		}

		private ExpandedStringSegment.ExpandedStringSegmentCollection mvarSegments = new ExpandedStringSegment.ExpandedStringSegmentCollection();
		public ExpandedStringSegment.ExpandedStringSegmentCollection Segments { get { return mvarSegments; } }

		public override string ToString()
		{
			return mvarSegments.Count.ToString() + " segments";
		}
		public string ToString(ExpandedStringVariableStore variables)
		{
			StringBuilder sb = new StringBuilder();
			foreach (ExpandedStringSegment segment in mvarSegments)
			{
				sb.Append(segment.ToString(variables));
			}
			return sb.ToString();
		}

		public static ExpandedString FromMarkup(MarkupTagElement markup)
		{
			ExpandedString value = new ExpandedString();
			foreach (MarkupElement el in markup.Elements)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				if (tag == null) continue;
				switch (tag.FullName)
				{
					case "VariableReference":
					case "ConstantReference":
					{
						MarkupAttribute attScope = tag.Attributes["Scope"];
						ExpandedStringSegmentVariableScope scope = ExpandedStringSegmentVariableScope.None;
						if (attScope != null)
						{
							switch (attScope.Value.ToLower())
							{
								case "file": scope = ExpandedStringSegmentVariableScope.File; break;
								case "project": scope = ExpandedStringSegmentVariableScope.Project; break;
								case "solution": scope = ExpandedStringSegmentVariableScope.Solution; break;
								case "global": scope = ExpandedStringSegmentVariableScope.Global; break;
							}
						}

						MarkupAttribute attID = tag.Attributes["ID"];
						if (attID == null) continue;

						string separator = ";";
						MarkupAttribute attSeparator = tag.Attributes["Separator"];
						if (attSeparator != null)
						{
							separator = attSeparator.Value;
						}

						if (tag.FullName == "VariableReference")
						{
							value.Segments.Add(new ExpandedStringSegmentVariable(attID.Value, scope, separator));
						}
						else if (tag.FullName == "ConstantReference")
						{
							value.Segments.Add(new ExpandedStringSegmentConstant(attID.Value, scope, separator));
						}
						break;
					}
					case "Literal":
					{
						MarkupAttribute attValue = tag.Attributes["Value"];
						if (attValue == null) continue;

						value.Segments.Add(new ExpandedStringSegmentLiteral(attValue.Value));
						break;
					}
				}
			}
			return value;
		}
	}
	public abstract class ExpandedStringSegment
	{
		public class ExpandedStringSegmentCollection
			: System.Collections.ObjectModel.Collection<ExpandedStringSegment>
		{

		}

		private static readonly ExpandedStringVariableStore _empty = new ExpandedStringVariableStore();

		public abstract string ToString(ExpandedStringVariableStore variables);
		public override string ToString()
		{
			return ToString(_empty);
		}
	}
	public class ExpandedStringSegmentLiteral : ExpandedStringSegment
	{
		public ExpandedStringSegmentLiteral()
		{
			mvarValue = String.Empty;
		}
		public ExpandedStringSegmentLiteral(string value)
		{
			mvarValue = value;
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString(ExpandedStringVariableStore variables)
		{
			return mvarValue;
		}
	}

	/// <summary>
	/// The scope of an <see cref="ExpandedStringSegmentVariable" />.
	/// </summary>
	public enum ExpandedStringSegmentVariableScope
	{
		/// <summary>
		/// No scope is defined for this variable. Automatically searches for the variable in this scope order: first
		/// file scope, then project scope, then solution scope, and finally global scope.
		/// </summary>
		None = 0,
		/// <summary>
		/// The variable is defined at the file level.
		/// </summary>
		File = 1,
		/// <summary>
		/// The variable is defined at the project level.
		/// </summary>
		Project = 2,
		/// <summary>
		/// The variable is defined at the solution level.
		/// </summary>
		Solution = 3,
		/// <summary>
		/// The variable is defined at the global level.
		/// </summary>
		Global = 4
	}
	/// <summary>
	/// Represents a string segment whose value is populated by a variable.
	/// </summary>
	public class ExpandedStringSegmentVariable : ExpandedStringSegment
	{
		private string mvarVariableName = String.Empty;
		/// <summary>
		/// The name of the variable in the variables dictionary from which to retrieve the value for this <see cref="ExpandedStringSegment" />.
		/// </summary>
		public string VariableName { get { return mvarVariableName; } set { mvarVariableName = value; } }

		private ExpandedStringSegmentVariableScope mvarScope = ExpandedStringSegmentVariableScope.None;
		/// <summary>
		/// The scope of the variable to search for, or <see cref="ExpandedStringSegmentVariableScope.None" /> to
		/// search all scopes in the order of file, project, solution, global.
		/// </summary>
		public ExpandedStringSegmentVariableScope Scope { get { return mvarScope; } set { mvarScope = value; } }

		private string mvarSeparator = String.Empty;
		/// <summary>
		/// The array separator to use when the value of the variable is an array.
		/// </summary>
		public string Separator { get { return mvarSeparator; } set { mvarSeparator = value; } }

		/// <summary>
		/// Creates a new instance of <see cref="ExpandedStringSegmentVariable" /> with the given variable name.
		/// </summary>
		/// <param name="variableName">
		/// The name of the variable in the variables dictionary from which to retrieve the value for this
		/// <see cref="ExpandedStringSegment" />.
		/// </param>
		/// <param name="scope">
		/// The scope of the variable to search for, or <see cref="ExpandedStringSegmentVariableScope.None" /> to
		/// search all scopes in the order of file, project, solution, global.
		/// </param>
		/// <param name="separator">The array separator to use when the value of the variable is an array.</param>
		public ExpandedStringSegmentVariable(string variableName, ExpandedStringSegmentVariableScope scope = ExpandedStringSegmentVariableScope.None, string separator = null)
		{
			mvarVariableName = variableName;
			mvarScope = scope;
			mvarSeparator = separator;
		}

		/// <summary>
		/// Returns the value of this <see cref="ExpandedStringSegment" /> with the given variables.
		/// </summary>
		/// <param name="variables">The variables to pass into the <see cref="ExpandedStringSegment" />.</param>
		/// <returns>A <see cref="String" /> that contains the value of this <see cref="ExpandedStringSegment" />.</returns>
		public override string ToString(ExpandedStringVariableStore variables)
		{
			ExpandedStringVariable varr = variables.Variables[mvarScope, mvarVariableName];
			if (varr != null && varr.Value != null) return varr.Value.ToString();
			return String.Empty;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Variable: \"");
			sb.Append(mvarVariableName);
			sb.Append("\" (");
			sb.Append(mvarScope.ToString());
			sb.Append(" scope)");

			if (!String.IsNullOrEmpty(mvarSeparator))
			{
				sb.Append(" (Array; separator=\"");
				sb.Append(mvarSeparator);
				sb.Append("\")");
			}
			return sb.ToString();
		}
	}
	public class ExpandedStringSegmentConstant : ExpandedStringSegmentVariable
	{
		/// <summary>
		/// Creates a new instance of <see cref="ExpandedStringSegmentVariable" /> with the given variable name.
		/// </summary>
		/// <param name="variableName">
		/// The name of the variable in the variables dictionary from which to retrieve the value for this
		/// <see cref="ExpandedStringSegment" />.
		/// </param>
		/// <param name="scope">
		/// The scope of the variable to search for, or <see cref="ExpandedStringSegmentVariableScope.None" /> to
		/// search all scopes in the order of file, project, solution, global.
		/// </param>
		/// <param name="separator">The array separator to use when the value of the variable is an array.</param>
		public ExpandedStringSegmentConstant(string variableName, ExpandedStringSegmentVariableScope scope = ExpandedStringSegmentVariableScope.None, string separator = null)
			: base(variableName, scope, separator)
		{
		}
		public override string ToString(ExpandedStringVariableStore variables)
		{
			ExpandedStringVariable varr = variables.Constants[Scope, VariableName];
			if (varr != null && varr.Value != null) return varr.Value.ToString();
			return String.Empty;
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Constant: \"");
			sb.Append(VariableName);
			sb.Append("\" (");
			sb.Append(Scope.ToString());
			sb.Append(" scope)");

			if (!String.IsNullOrEmpty(Separator))
			{
				sb.Append(" (Array; separator=\"");
				sb.Append(Separator);
				sb.Append("\")");
			}
			return sb.ToString();
		}
	}
}
